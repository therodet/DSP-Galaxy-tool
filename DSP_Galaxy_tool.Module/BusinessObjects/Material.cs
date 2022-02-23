using DevExpress.Xpo;
using System;
using System.Drawing;
using System.Linq;

namespace DSP_Galaxy_tool.Module.BusinessObjects
{
    public class Material : XPObject
    {
        public Material(Session session) : base(session) { }

        [Association("Material-Colors"), Aggregated]
        public XPCollection<MaterialColor> Colors
        {
            get => GetCollection<MaterialColor>();
        }

        string path;
        string copyFrom;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string CopyFrom
        {
            get => copyFrom;
            set => SetPropertyValue(nameof(CopyFrom), ref copyFrom, value);
        }

        [Association("Material-Params"), Aggregated]
        public XPCollection<MaterialParams> Params
        {
            get => GetCollection<MaterialParams>();
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Path
        {
            get => path;
            set => SetPropertyValue(nameof(Path), ref path, value);
        }

        [Persistent(nameof(Tint))]
        private int tint;
        [NonPersistent]
        public Color Tint
        {
            get { return Color.FromArgb(tint); }
            set
            {
                tint = value.ToArgb();
                OnChanged(nameof(Tint));
            }
        }
    }

    public class MaterialColor : XPObject
    {
        public MaterialColor(Session session) : base(session) { }

        string name;
        Material material;
        [Association("Material-Colors")]
        [Persistent]
        public Material Material
        {
            get => material;
            private set => SetPropertyValue(nameof(Material), ref material, value);
        }
        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        [Persistent(nameof(Color))]
        private int color;
        [NonPersistent]
        public Color Color
        {
            get { return Color.FromArgb(color); }
            set
            {
                color = value.ToArgb();
                OnChanged(nameof(Color));
            }
        }
    }

    public class MaterialParams : XPObject
    {
        public MaterialParams(Session session) : base(session) { }

        double paramValue;
        string name;
        Material material;
        [Association("Material-Params")]
        [Persistent]
        public Material Material
        {
            get => material;
            private set => SetPropertyValue(nameof(Material), ref material, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }
        
        public double Value
        {
            get => paramValue;
            set => SetPropertyValue(nameof(Value), ref paramValue, value);
        }
    }
}
