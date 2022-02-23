using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSP_Galaxy_tool.Module.BusinessObjects
{
    [DevExpress.ExpressApp.Model.ModelDefault("IsCloneable", "True")]
    [RuleCombinationOfPropertiesIsUnique("Cluster;Name")]
    [DevExpress.Persistent.Base.NavigationItem("Settings")]
    public class Theme : XPObject
    {
        public Theme(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            TerrainSettings = new TerrainSettings(Session);
            VeinSettings = new VeinSettings(Session);
//            VegeSettings = new VegeSettings(Session);
            AmbientSettings = new AmbientSettings(Session);
        }

        string vegeSettings;
        AmbientSettings ambientSettings;
        Material minimapMaterial;
        Material thumbMaterial;
        Material atmosphereMaterial;
        Material oceanMaterial;
        Material terrainMaterial;
        string materialPath;
        double sXFVolume;
        string sFXPath;
        string musics;
        FluidResource waterItemId;
        double waterHeight;
        double ionHeight;
        double wind;
        VegeSettings vegeSettings;
        VeinSettings veinSettings;
        TerrainSettings terrainSettings;
        string distribute;
        int temperature;
        string themeType;
        int minRadius;
        int maxRadius;
        string baseName;
        string displayName;
        bool customGeneration;
        int algo;
        string planetType;
        string name;
        Cluster cluster;
        [Association("Cluster-ThemeLibrary")]
        public Cluster Cluster
        {
            get => cluster;
            set => SetPropertyValue(nameof(Cluster), ref cluster, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string PlanetType
        {
            get => planetType;
            set => SetPropertyValue(nameof(PlanetType), ref planetType, value);
        }

        public int Algo
        {
            get => algo;
            set => SetPropertyValue(nameof(Algo), ref algo, value);
        }

        public bool CustomGeneration
        {
            get => customGeneration;
            set => SetPropertyValue(nameof(CustomGeneration), ref customGeneration, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string DisplayName
        {
            get => displayName;
            set => SetPropertyValue(nameof(DisplayName), ref displayName, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string BaseName
        {
            get => baseName;
            set => SetPropertyValue(nameof(BaseName), ref baseName, value);
        }

        public int MinRadius
        {
            get => minRadius;
            set => SetPropertyValue(nameof(MinRadius), ref minRadius, value);
        }

        public int MaxRadius
        {
            get => maxRadius;
            set => SetPropertyValue(nameof(MaxRadius), ref maxRadius, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ThemeType
        {
            get => themeType;
            set => SetPropertyValue(nameof(ThemeType), ref themeType, value);
        }

        [Association("Theme-StarTypes")]
        public XPCollection<StarType> StarTypes
        { get => GetCollection<StarType>(); }


        public int Temperature
        {
            get => temperature;
            set => SetPropertyValue(nameof(Temperature), ref temperature, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Distribute
        {
            get => distribute;
            set => SetPropertyValue(nameof(Distribute), ref distribute, value);
        }

        [Aggregated]
        [Persistent]
        [DevExpress.Persistent.Base.ExpandObjectMembers(DevExpress.Persistent.Base.ExpandObjectMembers.Never)]
        public TerrainSettings TerrainSettings
        {
            get => terrainSettings;
            private set => SetPropertyValue(nameof(TerrainSettings), ref terrainSettings, value);
        }

        [Aggregated]
        [Persistent]
        [DevExpress.Persistent.Base.ExpandObjectMembers(DevExpress.Persistent.Base.ExpandObjectMembers.Never)]
        public VeinSettings VeinSettings
        {
            get => veinSettings;
            private set => SetPropertyValue(nameof(VeinSettings), ref veinSettings, value);
        }
        /*
        [Aggregated]
        [Persistent]
        [DevExpress.Persistent.Base.ExpandObjectMembers(DevExpress.Persistent.Base.ExpandObjectMembers.Never)]
        public VegeSettings VegeSettings
        {
            get => vegeSettings;
            private set => SetPropertyValue(nameof(VegeSettings), ref vegeSettings, value);
        }
        */

        
        [Size(SizeAttribute.Unlimited)]
        public string VegeSettings
        {
            get => vegeSettings;
            set => SetPropertyValue(nameof(VegeSettings), ref vegeSettings, value);
        }

        [Association("Theme-GasItems"), Aggregated]
        public XPCollection<ThemeGas> GasItems
        { get => GetCollection<ThemeGas>(); }

        public double Wind
        {
            get => wind;
            set => SetPropertyValue(nameof(Wind), ref wind, value);
        }

        public double IonHeight
        {
            get => ionHeight;
            set => SetPropertyValue(nameof(IonHeight), ref ionHeight, value);
        }

        public double WaterHeight
        {
            get => waterHeight;
            set => SetPropertyValue(nameof(WaterHeight), ref waterHeight, value);
        }

        public FluidResource WaterItemId
        {
            get => waterItemId;
            set => SetPropertyValue(nameof(WaterItemId), ref waterItemId, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Musics
        {
            get => musics;
            set => SetPropertyValue(nameof(Musics), ref musics, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string SFXPath
        {
            get => sFXPath;
            set => SetPropertyValue(nameof(SFXPath), ref sFXPath, value);
        }

        public double SXFVolume
        {
            get => sXFVolume;
            set => SetPropertyValue(nameof(SXFVolume), ref sXFVolume, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string MaterialPath
        {
            get => materialPath;
            set => SetPropertyValue(nameof(MaterialPath), ref materialPath, value);
        }

        public Material TerrainMaterial
        {
            get => terrainMaterial;
            set => SetPropertyValue(nameof(TerrainMaterial), ref terrainMaterial, value);
        }

        public Material OceanMaterial
        {
            get => oceanMaterial;
            set => SetPropertyValue(nameof(OceanMaterial), ref oceanMaterial, value);
        }

        public Material AtmosphereMaterial
        {
            get => atmosphereMaterial;
            set => SetPropertyValue(nameof(AtmosphereMaterial), ref atmosphereMaterial, value);
        }

        public Material ThumbMaterial
        {
            get => thumbMaterial;
            set => SetPropertyValue(nameof(ThumbMaterial), ref thumbMaterial, value);
        }
        
        public Material MinimapMaterial
        {
            get => minimapMaterial;
            set => SetPropertyValue(nameof(MinimapMaterial), ref minimapMaterial, value);
        }

        [Aggregated]
        [Persistent]
        [DevExpress.Persistent.Base.ExpandObjectMembers(DevExpress.Persistent.Base.ExpandObjectMembers.Never)]
        public AmbientSettings AmbientSettings
        {
            get => ambientSettings;
            private set => SetPropertyValue(nameof(AmbientSettings), ref ambientSettings, value);
        }
    }

    [DevExpress.Persistent.Base.NavigationItem("Settings")]
    public class StarType : XPObject
    {
        public StarType(Session session) : base(session) { }

        string name;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        [Association("Theme-StarTypes")]
        public XPCollection<Theme> Themes
        { get => GetCollection<Theme>(); }
    }

    public class TerrainSettings : XPObject
    {
        public TerrainSettings(Session session) : base(session) { }

        double randomFactor;
        double landModifier;
        double heightMulti;
        bool brightnessFix;
        double biomeHeightMulti;
        double biomeHeightModifier;
        double baseHeight;
        string algorithm;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Algorithm
        {
            get => algorithm;
            set => SetPropertyValue(nameof(Algorithm), ref algorithm, value);
        }

        public double BaseHeight
        {
            get => baseHeight;
            set => SetPropertyValue(nameof(BaseHeight), ref baseHeight, value);
        }

        public double BiomeHeightModifier
        {
            get => biomeHeightModifier;
            set => SetPropertyValue(nameof(BiomeHeightModifier), ref biomeHeightModifier, value);
        }

        public double BiomeHeightMulti
        {
            get => biomeHeightMulti;
            set => SetPropertyValue(nameof(BiomeHeightMulti), ref biomeHeightMulti, value);
        }

        public bool BrightnessFix
        {
            get => brightnessFix;
            set => SetPropertyValue(nameof(BrightnessFix), ref brightnessFix, value);
        }

        public double HeightMulti
        {
            get => heightMulti;
            set => SetPropertyValue(nameof(HeightMulti), ref heightMulti, value);
        }

        public double LandModifier
        {
            get => landModifier;
            set => SetPropertyValue(nameof(LandModifier), ref landModifier, value);
        }
        
        public double RandomFactor
        {
            get => randomFactor;
            set => SetPropertyValue(nameof(RandomFactor), ref randomFactor, value);
        }
    }

    [MapInheritance(MapInheritanceType.ParentTable)]
    public class VeinSettings : VeinSettingsBase
    {
        public VeinSettings(Session session) : base(session) { }

        Theme theme;
        [Persistent]
        public Theme Theme
        {
            get => theme;
            private set => SetPropertyValue(nameof(Theme), ref theme, value);
        }
    }

    public class VegeSettings : XPObject
    {
        public VegeSettings(Session session) : base(session) { }

        string algorithm;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Algorithm
        {
            get => algorithm;
            set => SetPropertyValue(nameof(Algorithm), ref algorithm, value);
        }

        [Association("VegeSettings-GroupItems"), Aggregated]
        public XPCollection<VegeSettingsItem> GroupItems
        {
            get => GetCollection<VegeSettingsItem>(nameof(GroupItems));
        }
    }

    public class VegeSettingsItem : XPObject
    {
        public VegeSettingsItem(Session session) : base(session) { }

        string settingValue;
        int itemIndex;
        int groupIndex;
        VegeSettings settings;
        [Association("VegeSettings-GroupItems")]
        [Persistent]
        public VegeSettings Settings
        {
            get => settings;
            private set => SetPropertyValue(nameof(Settings), ref settings, value);
        }

        public int GroupIndex
        {
            get => groupIndex;
            set => SetPropertyValue(nameof(GroupIndex), ref groupIndex, value);
        }

        public int ItemIndex
        {
            get => itemIndex;
            set => SetPropertyValue(nameof(ItemIndex), ref itemIndex, value);
        }
        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Value
        {
            get => settingValue;
            set => SetPropertyValue(nameof(Value), ref settingValue, value);
        }
    }

    [DevExpress.Persistent.Base.NavigationItem("Settings")]
    public class FluidResource : XPObject
    {
        public FluidResource(Session session) : base(session) { }

        string name;
        int resourceID;

        public int ResourceID
        {
            get => resourceID;
            set => SetPropertyValue(nameof(ResourceID), ref resourceID, value);
        }
        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }
    }

    public class ThemeGas : XPObject
    {
        public ThemeGas(Session session) : base(session) { }

        double rate;
        FluidResource gas;
        Theme theme;
        [Association("Theme-GasItems")]
        [Persistent]
        public Theme Theme
        {
            get => theme;
            private set => SetPropertyValue(nameof(Theme), ref theme, value);
        }

        public FluidResource Gas
        {
            get => gas;
            set => SetPropertyValue(nameof(Gas), ref gas, value);
        }
        
        public double Rate
        {
            get => rate;
            set => SetPropertyValue(nameof(Rate), ref rate, value);
        }
    }

    public class AmbientSettings : XPObject
    {
        public AmbientSettings(Session session) : base(session) { }

        string resourcePath;
        int lutContribution;
        int dustStrength3;
        int dustStrength2;
        int dustStrength1;
        string cubeMap;
        int biomeSound3;
        int biomeSound2;
        int biomeSound1;

        [Persistent(nameof(Color1))]
        private int color1;
        [NonPersistent]
        public Color Color1
        {
            get { return Color.FromArgb(color1); }
            set
            {
                color1 = value.ToArgb();
                OnChanged(nameof(Color1));
            }
        }

        [Persistent(nameof(Color2))]
        private int color2;
        [NonPersistent]
        public Color Color2
        {
            get { return Color.FromArgb(color2); }
            set
            {
                color2 = value.ToArgb();
                OnChanged(nameof(Color2));
            }
        }

        [Persistent(nameof(Color3))]
        private int color3;
        [NonPersistent]
        public Color Color3
        {
            get { return Color.FromArgb(color3); }
            set
            {
                color3 = value.ToArgb();
                OnChanged(nameof(Color3));
            }
        }

        [Persistent(nameof(BiomeColor1))]
        private int biomeColor1;
        [NonPersistent]
        public Color BiomeColor1
        {
            get { return Color.FromArgb(biomeColor1); }
            set
            {
                biomeColor1 = value.ToArgb();
                OnChanged(nameof(BiomeColor1));
            }
        }

        [Persistent(nameof(BiomeColor2))]
        private int biomeColor2;
        [NonPersistent]
        public Color BiomeColor2
        {
            get { return Color.FromArgb(biomeColor2); }
            set
            {
                biomeColor2 = value.ToArgb();
                OnChanged(nameof(BiomeColor2));
            }
        }

        [Persistent(nameof(BiomeColor3))]
        private int biomeColor3;
        [NonPersistent]
        public Color BiomeColor3
        {
            get { return Color.FromArgb(biomeColor3); }
            set
            {
                biomeColor3 = value.ToArgb();
                OnChanged(nameof(BiomeColor3));
            }
        }

        [Persistent(nameof(DustColor1))]
        private int dustColor1;
        [NonPersistent]
        public Color DustColor1
        {
            get { return Color.FromArgb(dustColor1); }
            set
            {
                dustColor1 = value.ToArgb();
                OnChanged(nameof(DustColor1));
            }
        }

        [Persistent(nameof(DustColor2))]
        private int dustColor2;
        [NonPersistent]
        public Color DustColor2
        {
            get { return Color.FromArgb(dustColor2); }
            set
            {
                dustColor2 = value.ToArgb();
                OnChanged(nameof(DustColor2));
            }
        }

        [Persistent(nameof(DustColor3))]
        private int dustColor3;
        [NonPersistent]
        public Color DustColor3
        {
            get { return Color.FromArgb(dustColor3); }
            set
            {
                dustColor3 = value.ToArgb();
                OnChanged(nameof(DustColor3));
            }
        }

        [Persistent(nameof(WaterColor1))]
        private int waterColor1;
        [NonPersistent]
        public Color WaterColor1
        {
            get { return Color.FromArgb(waterColor1); }
            set
            {
                waterColor1 = value.ToArgb();
                OnChanged(nameof(WaterColor1));
            }
        }

        [Persistent(nameof(WaterColor2))]
        private int waterColor2;
        [NonPersistent]
        public Color WaterColor2
        {
            get { return Color.FromArgb(waterColor2); }
            set
            {
                waterColor2 = value.ToArgb();
                OnChanged(nameof(WaterColor2));
            }
        }

        [Persistent(nameof(WaterColor3))]
        private int waterColor3;
        [NonPersistent]
        public Color WaterColor3
        {
            get { return Color.FromArgb(waterColor3); }
            set
            {
                waterColor3 = value.ToArgb();
                OnChanged(nameof(WaterColor3));
            }
        }

        [Persistent(nameof(Reflections))]
        private int reflections;
        [NonPersistent]
        public Color Reflections
        {
            get { return Color.FromArgb(reflections); }
            set
            {
                reflections = value.ToArgb();
                OnChanged(nameof(Reflections));
            }
        }

        public int BiomeSound1
        {
            get => biomeSound1;
            set => SetPropertyValue(nameof(BiomeSound1), ref biomeSound1, value);
        }

        public int BiomeSound2
        {
            get => biomeSound2;
            set => SetPropertyValue(nameof(BiomeSound2), ref biomeSound2, value);
        }

        public int BiomeSound3
        {
            get => biomeSound3;
            set => SetPropertyValue(nameof(BiomeSound3), ref biomeSound3, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string CubeMap
        {
            get => cubeMap;
            set => SetPropertyValue(nameof(CubeMap), ref cubeMap, value);
        }

        public int DustStrength1
        {
            get => dustStrength1;
            set => SetPropertyValue(nameof(DustStrength1), ref dustStrength1, value);
        }

        public int DustStrength2
        {
            get => dustStrength2;
            set => SetPropertyValue(nameof(DustStrength2), ref dustStrength2, value);
        }

        public int DustStrength3
        {
            get => dustStrength3;
            set => SetPropertyValue(nameof(DustStrength3), ref dustStrength3, value);
        }

        public int LutContribution
        {
            get => lutContribution;
            set => SetPropertyValue(nameof(LutContribution), ref lutContribution, value);
        }
        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ResourcePath
        {
            get => resourcePath;
            set => SetPropertyValue(nameof(ResourcePath), ref resourcePath, value);
        }
    }
}
