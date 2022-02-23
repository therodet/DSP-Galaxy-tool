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
    public class Star : XPObject
    {
        public Star(Session session) : base(session) { }

        string starType;
        StarType spectr;
        int seed;
        double orbitScalar;
        string name;
        int maxOrbit;
        int level;
        bool decorative;
        Star binaryCompanion;
        double positionZ;
        double positionY;
        double positionX;
        double acDiscRadius;
        double resourceCoef;
        double classFactor;
        double physicsRadius;
        double lightBalanceRadius;
        double dysonRadius;
        double habitableRadius;
        double radius;
        double luminosity;
        double color;
        double lifetime;
        double temperature;
        double mass;
        double age;
        Cluster cluster;
        [Association("Cluster-Stars")]
        public Cluster Cluster
        {
            get => cluster;
            set => SetPropertyValue(nameof(Cluster), ref cluster, value);
        }

        private string notes;
        [Size(SizeAttribute.Unlimited)]
        public string Notes
        {
            get => notes;
            set => SetPropertyValue(nameof(Notes), ref notes, value);
        }

        public double Age
        {
            get => age;
            set => SetPropertyValue(nameof(Age), ref age, value);
        }

        public double Mass
        {
            get => mass;
            set => SetPropertyValue(nameof(Mass), ref mass, value);
        }

        public double Temperature
        {
            get => temperature;
            set => SetPropertyValue(nameof(Temperature), ref temperature, value);
        }

        public double Lifetime
        {
            get => lifetime;
            set => SetPropertyValue(nameof(Lifetime), ref lifetime, value);
        }

        public double Color
        {
            get => color;
            set => SetPropertyValue(nameof(Color), ref color, value);
        }

        public double Luminosity
        {
            get => luminosity;
            set => SetPropertyValue(nameof(Luminosity), ref luminosity, value);
        }

        public double Radius
        {
            get => radius;
            set => SetPropertyValue(nameof(Radius), ref radius, value);
        }

        public double HabitableRadius
        {
            get => habitableRadius;
            set => SetPropertyValue(nameof(HabitableRadius), ref habitableRadius, value);
        }

        public double DysonRadius
        {
            get => dysonRadius;
            set => SetPropertyValue(nameof(DysonRadius), ref dysonRadius, value);
        }

        public double LightBalanceRadius
        {
            get => lightBalanceRadius;
            set => SetPropertyValue(nameof(LightBalanceRadius), ref lightBalanceRadius, value);
        }

        public double PhysicsRadius
        {
            get => physicsRadius;
            set => SetPropertyValue(nameof(PhysicsRadius), ref physicsRadius, value);
        }

        public double ClassFactor
        {
            get => classFactor;
            set => SetPropertyValue(nameof(ClassFactor), ref classFactor, value);
        }

        public double ResourceCoef
        {
            get => resourceCoef;
            set => SetPropertyValue(nameof(ResourceCoef), ref resourceCoef, value);
        }

        public double AcDiscRadius
        {
            get => acDiscRadius;
            set => SetPropertyValue(nameof(AcDiscRadius), ref acDiscRadius, value);
        }

        public double PositionX
        {
            get => positionX;
            set => SetPropertyValue(nameof(PositionX), ref positionX, value);
        }

        public double PositionY
        {
            get => positionY;
            set => SetPropertyValue(nameof(PositionY), ref positionY, value);
        }

        public double PositionZ
        {
            get => positionZ;
            set => SetPropertyValue(nameof(PositionZ), ref positionZ, value);
        }

        [Aggregated]
        public Star BinaryCompanion
        {
            get => binaryCompanion;
            set => SetPropertyValue(nameof(BinaryCompanion), ref binaryCompanion, value);
        }

        public bool Decorative
        {
            get => decorative;
            set => SetPropertyValue(nameof(Decorative), ref decorative, value);
        }

        public int Level
        {
            get => level;
            set => SetPropertyValue(nameof(Level), ref level, value);
        }

        public int MaxOrbit
        {
            get => maxOrbit;
            set => SetPropertyValue(nameof(MaxOrbit), ref maxOrbit, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        public double OrbitScalar
        {
            get => orbitScalar;
            set => SetPropertyValue(nameof(OrbitScalar), ref orbitScalar, value);
        }

        public int Seed
        {
            get => seed;
            set => SetPropertyValue(nameof(Seed), ref seed, value);
        }

        public StarType Spectr
        {
            get => spectr;
            set => SetPropertyValue(nameof(Spectr), ref spectr, value);
        }
        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string StarType
        {
            get => starType;
            set => SetPropertyValue(nameof(StarType), ref starType, value);
        }

        [PersistentAlias("Not IsNull(BinaryCompanion) Or Cluster.Stars[BinaryCompanion.Oid = ^.^.Oid]")]
        public bool IsBinary
        { get => (bool)EvaluateAlias(); }

        [Association("Star-Planets"), Aggregated]
        public XPCollection<Planet> Planets
        { get => GetCollection<Planet>(); }
    }
}
