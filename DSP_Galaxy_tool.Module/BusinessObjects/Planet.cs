using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSP_Galaxy_tool.Module.BusinessObjects
{
    [DevExpress.ExpressApp.Model.ModelDefault("IsCloneable", "True")]
    [DevExpress.Persistent.Base.NavigationItem("Galaxy")]
    public class Planet : XPObject, Classes.IResourceOwner
    {
        public Planet(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            VeinSettings = new PlanetVeinSettings(Session);
            VeinSettings.SetPlanet(this);
        }

        PlanetVeinSettings veinSettings;
        double scale;
        double rareChance;
        bool randomizeVeinCounts;
        bool randomizeVeinAmounts;
        double luminosity;
        double rotationPhase;
        double rotationPeriod;
        double obliquity;
        double orbitLongitude;
        double orbitPhase;
        double orbitalPeriod;
        double orbitInclination;
        double orbitRadius;
        int radius;
        Theme theme;
        string name;
        int seed;
        Planet parentPlanet;
        Star star;
        [Association("Star-Planets")]
        public Star Star
        {
            get => star;
            set => SetPropertyValue(nameof(Star), ref star, value);
        }

        [Association("Planet-Moons")]
        public Planet ParentPlanet
        {
            get => parentPlanet;
            set => SetPropertyValue(nameof(ParentPlanet), ref parentPlanet, value);
        }

        private string notes;
        [Size(SizeAttribute.Unlimited)]
        public string Notes
        {
            get => notes;
            set => SetPropertyValue(nameof(Notes), ref notes, value);
        }

        [Association("Planet-Moons"), Aggregated]
        public XPCollection<Planet> Moons
        { get => GetCollection<Planet>(); }

        public int Seed
        {
            get => seed;
            set => SetPropertyValue(nameof(Seed), ref seed, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        public Theme Theme
        {
            get => theme;
            set => SetPropertyValue(nameof(Theme), ref theme, value);
        }

        public int Radius
        {
            get => radius;
            set => SetPropertyValue(nameof(Radius), ref radius, value);
        }

        public double OrbitRadius
        {
            get => orbitRadius;
            set => SetPropertyValue(nameof(OrbitRadius), ref orbitRadius, value);
        }

        public double OrbitInclination
        {
            get => orbitInclination;
            set => SetPropertyValue(nameof(OrbitInclination), ref orbitInclination, value);
        }

        public double OrbitalPeriod
        {
            get => orbitalPeriod;
            set => SetPropertyValue(nameof(OrbitalPeriod), ref orbitalPeriod, value);
        }

        public double OrbitPhase
        {
            get => orbitPhase;
            set => SetPropertyValue(nameof(OrbitPhase), ref orbitPhase, value);
        }

        public double OrbitLongitude
        {
            get => orbitLongitude;
            set => SetPropertyValue(nameof(OrbitLongitude), ref orbitLongitude, value);
        }

        public double Obliquity
        {
            get => obliquity;
            set => SetPropertyValue(nameof(Obliquity), ref obliquity, value);
        }

        public double RotationPeriod
        {
            get => rotationPeriod;
            set => SetPropertyValue(nameof(RotationPeriod), ref rotationPeriod, value);
        }

        public double RotationPhase
        {
            get => rotationPhase;
            set => SetPropertyValue(nameof(RotationPhase), ref rotationPhase, value);
        }

        public double Luminosity
        {
            get => luminosity;
            set => SetPropertyValue(nameof(Luminosity), ref luminosity, value);
        }

        public bool RandomizeVeinAmounts
        {
            get => randomizeVeinAmounts;
            set => SetPropertyValue(nameof(RandomizeVeinAmounts), ref randomizeVeinAmounts, value);
        }

        public bool RandomizeVeinCounts
        {
            get => randomizeVeinCounts;
            set => SetPropertyValue(nameof(RandomizeVeinCounts), ref randomizeVeinCounts, value);
        }

        public double RareChance
        {
            get => rareChance;
            set => SetPropertyValue(nameof(RareChance), ref rareChance, value);
        }

        public double Scale
        {
            get => scale;
            set => SetPropertyValue(nameof(Scale), ref scale, value);
        }

        [Aggregated]
        [Persistent]
        [DevExpress.Persistent.Base.ExpandObjectMembers(DevExpress.Persistent.Base.ExpandObjectMembers.Never)]
        [DevExpress.Persistent.Base.VisibleInListView(false)]
        public PlanetVeinSettings VeinSettings
        {
            get => veinSettings;
            private set => SetPropertyValue(nameof(VeinSettings), ref veinSettings, value);
        }

        private XPCollection<VeinTypeVein> allVeins;
        public XPCollection<VeinTypeVein> AllVeins
        {
            get
            {
                if(allVeins == null && !IsLoading && !IsDeleted)
                { allVeins = new XPCollection<VeinTypeVein>(Session, DevExpress.Data.Filtering.CriteriaOperator.Parse("VeinType.VeinSettings.<PlanetVeinSettings>Planet = ?", this)); }
                return allVeins;
            }
        }

        private System.ComponentModel.BindingList<FluidResource> allFluids;
        public System.ComponentModel.BindingList<FluidResource> AllFluids
        {
            get
            {
                if(allFluids == null && !IsLoading && !IsDeleted)
                {
                    allFluids = new System.ComponentModel.BindingList<FluidResource>();
                    foreach(var gasItem in theme.GasItems)
                    { allFluids.Add(gasItem.Gas); }
                    if(theme.WaterItemId != null)
                    { allFluids.Add(theme.WaterItemId); }
                }
                return allFluids;
            }
        }
    }

    [MapInheritance(MapInheritanceType.ParentTable)]
    public class PlanetVeinSettings : VeinSettingsBase
    {
        public PlanetVeinSettings(Session session) : base(session) { }

        public void SetPlanet(Planet planet)
        { Planet = planet; }

        Planet planet;
        [Persistent]
        public Planet Planet
        {
            get => planet;
            private set => SetPropertyValue(nameof(Planet), ref planet, value);
        }
    }
}
