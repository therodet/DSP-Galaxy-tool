using DevExpress.Persistent.Validation;
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
    public class Cluster : XPObject
    {
        public Cluster(Session session) : base(session) { }

        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        
        [Size(SizeAttribute.Unlimited)]
        public string Notes
        {
            get => notes;
            set => SetPropertyValue(nameof(Notes), ref notes, value);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GalaxyParams = new GalaxyParameters(Session);
        }

        string notes;
        string name;
        double birthCopperRichness;
        int birthCopperCount;
        double birthIronRichness;
        int birthIronCount;
        int seed;
        public int Seed
        {
            get => seed;
            set => SetPropertyValue(nameof(Seed), ref seed, value);
        }

        Planet birthPlanet;
        [RuleRequiredField]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public Planet BirthPlanet
        {
            get => birthPlanet;
            set => SetPropertyValue(nameof(BirthPlanet), ref birthPlanet, value);
        }

        string stype;
        [RuleRequiredField]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Stype
        {
            get => stype;
            set => SetPropertyValue(nameof(Stype), ref stype, value);
        }

        private GalaxyParameters galaxyParams;
        [Aggregated]
        [Persistent]
        [DevExpress.Persistent.Base.ExpandObjectMembers(DevExpress.Persistent.Base.ExpandObjectMembers.Never)]
        public GalaxyParameters GalaxyParams
        {
            get => galaxyParams;
            private set => SetPropertyValue(nameof(GalaxyParams), ref galaxyParams, value);
        }

        [Association("Cluster-ThemeLibrary")]
        public XPCollection<Theme> ThemeLibrary
        { get => GetCollection<Theme>(); }

        [Association("Cluster-Stars"), Aggregated]
        public XPCollection<Star> Stars
        { get => GetCollection<Star>(); }

        public int BirthIronCount
        {
            get => birthIronCount;
            set => SetPropertyValue(nameof(BirthIronCount), ref birthIronCount, value);
        }

        public double BirthIronRichness
        {
            get => birthIronRichness;
            set => SetPropertyValue(nameof(BirthIronRichness), ref birthIronRichness, value);
        }

        public int BirthCopperCount
        {
            get => birthCopperCount;
            set => SetPropertyValue(nameof(BirthCopperCount), ref birthCopperCount, value);
        }
        
        public double BirthCopperRichness
        {
            get => birthCopperRichness;
            set => SetPropertyValue(nameof(BirthCopperRichness), ref birthCopperRichness, value);
        }
    }

    public class GalaxyParameters : XPObject
    {
        public GalaxyParameters(Session session) : base(session) { }

        Cluster cluster;
        [Persistent]
        public Cluster Cluster
        {
            get => cluster;
            private set => SetPropertyValue(nameof(Cluster), ref cluster, value);
        }

        double flatten;
        public double Flatten
        {
            get => flatten;
            set => SetPropertyValue(nameof(Flatten), ref flatten, value);
        }

        bool forceSpecials;
        public bool ForceSpecials
        {
            get => forceSpecials;
            set => SetPropertyValue(nameof(ForceSpecials), ref forceSpecials, value);
        }

        int graphDistance;
        public int GraphDistance
        {
            get => graphDistance;
            set => SetPropertyValue(nameof(GraphDistance), ref graphDistance, value);
        }

        int graphMaxStars;
        public int GraphMaxStars
        {
            get => graphMaxStars;
            set => SetPropertyValue(nameof(GraphMaxStars), ref graphMaxStars, value);
        }

        int iterations;
        public int Iterations
        {
            get => iterations;
            set => SetPropertyValue(nameof(Iterations), ref iterations, value);
        }

        double minStepLength;
        public double MinStepLength
        {
            get => minStepLength;
            set => SetPropertyValue(nameof(MinStepLength), ref minStepLength, value);
        }

        double maxStepLength;
        public double MaxStepLength
        {
            get => maxStepLength;
            set => SetPropertyValue(nameof(MaxStepLength), ref maxStepLength, value);
        }

        double minDistance;
        public double MinDistance
        {
            get => minDistance;
            set => SetPropertyValue(nameof(MinDistance), ref minDistance, value);
        }

        int resourceMulti;
        public int ResourceMulti
        {
            get => resourceMulti;
            set => SetPropertyValue(nameof(ResourceMulti), ref resourceMulti, value);
        }
    }
}
