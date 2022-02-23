using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSP_Galaxy_tool.Module.Classes
{
    public interface IResourceOwner
    {
        DevExpress.Xpo.XPCollection<BusinessObjects.VeinTypeVein> AllVeins { get; }
        System.ComponentModel.BindingList<BusinessObjects.FluidResource> AllFluids { get; }
    }
}
