using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSP_Galaxy_tool.Module.BusinessObjects
{
    [Persistent(nameof(VeinSettings))]
    public abstract class VeinSettingsBase : XPObject
    {
        public VeinSettingsBase(Session session) : base(session) { }

        double veinPadding;
        string algorithm;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Algorithm
        {
            get => algorithm;
            set => SetPropertyValue(nameof(Algorithm), ref algorithm, value);
        }
        
        public double VeinPadding
        {
            get => veinPadding;
            set => SetPropertyValue(nameof(VeinPadding), ref veinPadding, value);
        }

        [Association("VeinSettings-VeinTypes"), Aggregated]
        public XPCollection<VeinType> VeinTypes
        { get => GetCollection<VeinType>(); }
    }

    [DefaultProperty(nameof(OreType))]
    public class VeinType : XPObject
    {
        public VeinType(Session session) : base(session) { }

        bool rare;
        OreType oreType;
        VeinSettingsBase veinSettings;
        [Association("VeinSettings-VeinTypes")]
        [Persistent]
        public VeinSettingsBase VeinSettings
        {
            get => veinSettings;
            private set => SetPropertyValue(nameof(VeinSettings), ref veinSettings, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public OreType OreType
        {
            get => oreType;
            set => SetPropertyValue(nameof(OreType), ref oreType, value);
        }

        public bool Rare
        {
            get => rare;
            set => SetPropertyValue(nameof(Rare), ref rare, value);
        }

        [PersistentAlias("Veins.Sum(ClusterCount)")]
        public int TotalClusters
        { get => (int)EvaluateAlias(); }

        [PersistentAlias("Veins.Sum(ClusterCount * ClusterSize)")]
        public int TotalVeins
        { get => (int)EvaluateAlias(); }

        [PersistentAlias("Veins.Sum(ClusterCount * ClusterSize * Richness)")]
        public double ScaledTotalVeins
        { get => Convert.ToDouble(EvaluateAlias()); }

        [Association("VeinType-Veins"), Aggregated]
        public XPCollection<VeinTypeVein> Veins
        { get => GetCollection<VeinTypeVein>(); }
    }

    [DefaultProperty(nameof(Data))]
    public class VeinTypeVein : XPObject
    {
        public VeinTypeVein(Session session) : base(session) { }

        double richness;
        int clusterSize;
        int clusterCount;
        VeinType veinType;
        [Association("VeinType-Veins")]
        [Persistent]
        public VeinType VeinType
        {
            get => veinType;
            private set => SetPropertyValue(nameof(VeinType), ref veinType, value);
        }

        public int ClusterCount
        {
            get => clusterCount;
            set => SetPropertyValue(nameof(ClusterCount), ref clusterCount, value);
        }

        public int ClusterSize
        {
            get => clusterSize;
            set => SetPropertyValue(nameof(ClusterSize), ref clusterSize, value);
        }

        public double Richness
        {
            get => richness;
            set => SetPropertyValue(nameof(Richness), ref richness, value);
        }

        [PersistentAlias("Concat(ClusterCount, 'x', ClusterSize, 'x', Richness)")]
        [DevExpress.Persistent.Base.VisibleInListView(false)]
        public string Data
        {
            get => (string)EvaluateAlias();
        }
    }

    public class OreType : XPObject
    {
        public OreType(Session session) : base(session) { }

        string name;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }
    }
}
