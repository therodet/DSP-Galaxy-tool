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

        Color color;
        [NonPersistent]
        public Color Color
        {
            get
            {
                if(color == Color.Empty)
                { color = Color.FromArgb(GetIntColor(A), GetIntColor(R), GetIntColor(G), GetIntColor(B)); }
                return color;
            }
            set
            {
                color = value;
                A = color.A / 255.0;
                R = color.R / 255.0;
                G = color.G / 255.0;
                B = color.B / 255.0;
                OnChanged(nameof(Color));
            }
        }
        private int GetIntColor(double value)
        {
            return Math.Min(255, Math.Max(0, (int)(255 * value)));
        }

        double a;
        public double A
        {
            get => a;
            set => SetPropertyValue(nameof(A), ref a, value);
        }

        double r;
        public double R
        {
            get => r;
            set => SetPropertyValue(nameof(R), ref r, value);
        }

        double g;
        public double G
        {
            get => g;
            set => SetPropertyValue(nameof(G), ref g, value);
        }

        double b;
        public double B
        {
            get => b;
            set => SetPropertyValue(nameof(B), ref b, value);
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
