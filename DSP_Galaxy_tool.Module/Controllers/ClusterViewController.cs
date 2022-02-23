using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using DSP_Galaxy_tool.Module.BusinessObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSP_Galaxy_tool.Module.Controllers
{
    public class ClusterViewController : ObjectViewController<ObjectView, Cluster>
    {
        public ClusterViewController()
        {
            InitComponents();
        }

        private Container components;
        PopupWindowShowAction importClusterFileAction;
        private void InitComponents()
        {
            components = new Container();

            importClusterFileAction = new PopupWindowShowAction(components);
            importClusterFileAction.Id = nameof(importClusterFileAction);
            importClusterFileAction.Caption = "Import";
            importClusterFileAction.Category = "Edit";
            importClusterFileAction.SelectionDependencyType = SelectionDependencyType.Independent;
            importClusterFileAction.CustomizePopupWindowParams += ImportClusterFileAction_CustomizePopupWindowParams;
            importClusterFileAction.Execute += ImportClusterFileAction_Execute;

            RegisterActions(components);
        }

        private void ImportClusterFileAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            using (MemoryStream stream = new())
            {
                ((FileSelectorDialog)e.PopupWindowViewCurrentObject).File.SaveToStream(stream);
                stream.Position = 0;
                using(StreamReader reader = new StreamReader(stream))
                {
                    string content = reader.ReadToEnd();
                    ImportJSON(content);
                }
            }
        }

        private void ImportJSON(string data)
        {
            //var reader = new JsonTextReader(new StringReader(data));
            JObject root = JObject.Parse(data);
            using (IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(Cluster)))
            {
                oreTypeCache?.Clear();
                oreTypeCache = new Dictionary<string, OreType>();

                Cluster cluster = objectSpace.CreateObject<Cluster>();
                cluster.Name = (string)root["Seed"];
                cluster.Seed = (int)root["Seed"];
                cluster.BirthIronCount = (int)((JObject)root["BirthIron"])["count"];
                cluster.BirthIronRichness = (double)((JObject)root["BirthIron"])["richness"];
                cluster.BirthCopperCount = (int)((JObject)root["BirthCopper"])["count"];
                cluster.BirthCopperRichness = (double)((JObject)root["BirthCopper"])["richness"];
                JObject gParams = (JObject)root["GalaxyParams"];
                ImportSimpleProperties(cluster.GalaxyParams, gParams);

                string birthPlanet = (string)root["BirthPlanet"];

                foreach (JProperty jThemeProp in root["ThemeLibrary"])
                {
                    JObject jTheme = (JObject)jThemeProp.Children().First();
                    Theme theme = objectSpace.CreateObject<Theme>();
                    cluster.ThemeLibrary.Add(theme);
                    ImportSimpleProperties(theme, jTheme);

                    foreach (JValue jStarType in jTheme["StarTypes"])
                    {
                        StarType starType = objectSpace.FindObject<StarType>(new BinaryOperator(nameof(StarType.Name), (string)jStarType));
                        if (starType == null)
                        {
                            starType = objectSpace.CreateObject<StarType>();
                            starType.Name = (string)jStarType;
                        }
                        starType.Themes.Add(theme);
                    }

                    ImportSimpleProperties(theme.TerrainSettings, (JObject)jTheme["TerrainSettings"]);

                    JObject jVeinSettings = (JObject)jTheme["VeinSettings"];
                    AddVeinSettings(objectSpace, theme.VeinSettings, jVeinSettings);

                    JObject jVegeSettings = (JObject)jTheme["VegeSettings"];
                    theme.VegeSettings.Algorithm = (string)jVegeSettings["Algorithm"];
                    for (int index = 1; index <= 6; index++)
                    {
                        int itemIndex = 0;
                        foreach (JToken value in jVegeSettings["Group" + index])
                        {
                            VegeSettingsItem item = objectSpace.CreateObject<VegeSettingsItem>();
                            if (value is JValue jv)
                            { item.Value = (string)jv; }
                            else
                            { item.Value = "Error"; }
                            item.ItemIndex = itemIndex;
                            item.GroupIndex = index;
                            itemIndex++;
                            theme.VegeSettings.GroupItems.Add(item);
                        }
                    }

                    JArray jGasItems = (JArray)jTheme["GasItems"];
                    JArray jGasSpeeds = (JArray)jTheme["GasSpeeds"];
                    for (int index = 0; index < jGasItems.Count; index++)
                    {
                        GasResource gasResource = objectSpace.FindObject<GasResource>(new BinaryOperator(nameof(GasResource.ResourceID), (int)jGasItems[index]));
                        if (gasResource == null)
                        {
                            gasResource = objectSpace.CreateObject<GasResource>();
                            gasResource.ResourceID = (int)jGasItems[index];
                        }
                        ThemeGas gas = objectSpace.CreateObject<ThemeGas>();
                        gas.Gas = gasResource;
                        gas.Rate = (double)jGasSpeeds[index];
                        theme.GasItems.Add(gas);
                    }

                    JArray jMusics = (JArray)jTheme["Musics"];
                    theme.Musics = string.Join(';', jMusics.Select(m => (string)m));
                    theme.TerrainMaterial = GetMaterial(objectSpace, (JObject)jTheme["terrainMaterial"]);
                    theme.OceanMaterial = GetMaterial(objectSpace, (JObject)jTheme["oceanMaterial"]);
                    theme.AtmosphereMaterial = GetMaterial(objectSpace, (JObject)jTheme["atmosphereMaterial"]);
                    theme.ThumbMaterial = GetMaterial(objectSpace, (JObject)jTheme["thumbMaterial"]);
                    theme.MinimapMaterial = GetMaterial(objectSpace, (JObject)jTheme["minimapMaterial"]);

                    ImportSimpleProperties(theme.AmbientSettings, (JObject)jTheme["AmbientSettings"]);
                    theme.AmbientSettings.Color1 = GetColor(jTheme["AmbientSettings"]["Color1"]);
                    theme.AmbientSettings.Color2 = GetColor(jTheme["AmbientSettings"]["Color2"]);
                    theme.AmbientSettings.Color3 = GetColor(jTheme["AmbientSettings"]["Color3"]);
                    theme.AmbientSettings.BiomeColor1 = GetColor(jTheme["AmbientSettings"]["BiomeColor1"]);
                    theme.AmbientSettings.BiomeColor2 = GetColor(jTheme["AmbientSettings"]["BiomeColor2"]);
                    theme.AmbientSettings.BiomeColor3 = GetColor(jTheme["AmbientSettings"]["BiomeColor3"]);
                    theme.AmbientSettings.DustColor1 = GetColor(jTheme["AmbientSettings"]["DustColor1"]);
                    theme.AmbientSettings.DustColor2 = GetColor(jTheme["AmbientSettings"]["DustColor2"]);
                    theme.AmbientSettings.DustColor3 = GetColor(jTheme["AmbientSettings"]["DustColor3"]);
                    theme.AmbientSettings.WaterColor1 = GetColor(jTheme["AmbientSettings"]["WaterColor1"]);
                    theme.AmbientSettings.WaterColor2 = GetColor(jTheme["AmbientSettings"]["WaterColor2"]);
                    theme.AmbientSettings.WaterColor3 = GetColor(jTheme["AmbientSettings"]["WaterColor3"]);
                    theme.AmbientSettings.Reflections = GetColor(jTheme["AmbientSettings"]["Reflections"]);
                }

                Dictionary<Star, string> binaryPairs = new Dictionary<Star, string>();
                JArray jStars = (JArray)root["Stars"];
                foreach (JObject jStar in jStars)
                {
                    Star star = objectSpace.CreateObject<Star>();
                    star.PositionX = (double)jStar["position"]["x"];
                    star.PositionY = (double)jStar["position"]["y"];
                    star.PositionZ = (double)jStar["position"]["z"];
                    star.StarType = (string)jStar["Type"];
                    star.Spectr = objectSpace.FindObject<StarType>(new BinaryOperator(nameof(StarType.Name), (string)jStar["Spectr"]));
                    ImportSimpleProperties(star, jStar);

                    if(!string.IsNullOrWhiteSpace((string)jStar["BinaryCompanion"]))
                    { binaryPairs.Add(star, (string)jStar["BinaryCompanion"]); }

                    cluster.Stars.Add(star);

                    foreach (JObject jPlanet in jStar["Planets"])
                    {
                        Planet planet = AddPlanet(objectSpace, star, jPlanet, null);

                        if (planet.Name == birthPlanet)
                        { cluster.BirthPlanet = planet; }
                    }
                }
                foreach(var pair in binaryPairs)
                {
                    Star star = objectSpace.FindObject<Star>(new BinaryOperator(nameof(Star.Name), pair.Value));
                    if(star != null)
                    { pair.Key.BinaryCompanion = star; }
                }

                objectSpace.CommitChanges();
            }
        }

        private void AddVeinSettings(IObjectSpace objectSpace, VeinSettingsBase veinSettings, JToken jVeinSettings)
        {
            if (jVeinSettings is not JObject)
            { return; }
            veinSettings.Algorithm = (string)jVeinSettings["Algorithm"];
            veinSettings.VeinPadding = (double)jVeinSettings["VeinPadding"];
            foreach (JObject jVeinType in jVeinSettings["VeinTypes"].Where(t => t["type"] is not null))
            {
                VeinType veinType = objectSpace.CreateObject<VeinType>();
                veinType.OreType = GetOreType(objectSpace, (string)jVeinType["type"]);
                veinType.Rare = (bool)jVeinType["rare"];
                veinSettings.VeinTypes.Add(veinType);
                foreach (JValue jVein in jVeinType["veins"])
                {
                    VeinTypeVein veinTypeVein = objectSpace.CreateObject<VeinTypeVein>();
                    string[] parts = ((string)jVein).Split('x');
                    veinTypeVein.ClusterCount = Convert.ToInt32(parts[0]);
                    veinTypeVein.ClusterSize = Convert.ToInt32(parts[1]);
                    veinTypeVein.Richness = Convert.ToDouble(parts[2]);
                    veinType.Veins.Add(veinTypeVein);
                }
            }
        }

        Dictionary<string, OreType> oreTypeCache;
        private OreType GetOreType(IObjectSpace objectSpace, string name)
        {
            OreType oreType = null;
            if(oreTypeCache.ContainsKey(name))
            { oreType = oreTypeCache[name]; }
            else
            { oreType = objectSpace.FindObject<OreType>(new BinaryOperator(nameof(OreType.Name), name)); }
            if(oreType == null)
            {
                oreType = objectSpace.CreateObject<OreType>();
                oreType.Name = name;
                oreTypeCache.Add(name, oreType);
            }
            return oreType;
        }

        private Planet AddPlanet(IObjectSpace objectSpace, Star star, JObject jPlanet, Planet parent)
        {
            Planet planet = objectSpace.CreateObject<Planet>();
            ImportSimpleProperties(planet, jPlanet);
            foreach (JObject jMoon in jPlanet["Moons"])
            { planet.Moons.Add(AddPlanet(objectSpace, star, jMoon, planet)); }
            planet.Theme = objectSpace.FindObject<Theme>(new BinaryOperator(nameof(Theme.Name), (string)jPlanet["Theme"]));

            AddVeinSettings(objectSpace, planet.VeinSettings,  jPlanet["veinSettings"]);

            star.Planets.Add(planet);
            return planet;
        }

        private Material GetMaterial(IObjectSpace objectSpace, JObject jMaterial)
        {
            Material material = objectSpace.CreateObject<Material>();
            foreach(JProperty jColor in jMaterial["Colors"])
            {
                MaterialColor color = objectSpace.CreateObject<MaterialColor>();
                color.Name = jColor.Name;
                color.Color = GetColor(jColor);
                material.Colors.Add(color);
            }
            material.CopyFrom = (string)jMaterial["CopyFrom"];
            foreach(JProperty property in jMaterial["Params"].Children<JProperty>())
            {
                MaterialParams param = objectSpace.CreateObject<MaterialParams>();
                param.Name = property.Name;
                param.Value = (double)property.Value;
                material.Params.Add(param);
            }
            material.Path = (string)jMaterial["Path"];
            // todo: Textures?
            material.Tint = GetColor(jMaterial["Tint"]);

            return material;
        }

        private Color GetColor(JToken jColorProperty)
        {
            JToken jColor = jColorProperty is JObject obj ? obj : jColorProperty.Children().First();
            return Color.FromArgb(GetIntColor(jColor["a"]), GetIntColor(jColor["r"]), GetIntColor(jColor["g"]), GetIntColor(jColor["b"]));
        }

        private int GetIntColor(JToken jToken)
        {
            return Math.Min(255, Math.Max(0, (int)(255 * (double)jToken)));
        }

        private void ImportSimpleProperties(XPObject target, JToken obj)
        {
            foreach(JProperty property in obj.Children<JProperty>())
            {
                try
                {
                    //string fieldName = char.ToUpper(property.Name[0]) + property.Name[1..];
                    //var member = target.ClassInfo.FindMember(fieldName);
                    var member = target.ClassInfo.Members.FirstOrDefault(m => string.Equals(m.Name, property.Name, StringComparison.CurrentCultureIgnoreCase));
                    if (member != null)
                    {
                        if (member.MemberType == typeof(double))
                        { member.SetValue(target, (double)property.Value); }
                        else if (member.MemberType == typeof(string))
                        { member.SetValue(target, (string)property.Value); }
                        else if (member.MemberType == typeof(int))
                        { member.SetValue(target, (int)property.Value); }
                    }
                }
                catch (Exception ex)
                { System.Diagnostics.Debug.WriteLine("Unable to convert: " + property.Name); }
            }
        }

        private void ImportClusterFileAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            FileSelectorDialog dialog = new FileSelectorDialog();
            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(FileSelectorDialog));
            e.View = Application.CreateDetailView(objectSpace, dialog);
        }
    }
}
