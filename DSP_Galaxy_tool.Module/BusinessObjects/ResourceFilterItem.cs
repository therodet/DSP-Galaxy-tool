using DevExpress.ExpressApp;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSP_Galaxy_tool.Module.BusinessObjects
{
    [DevExpress.ExpressApp.DC.DomainComponent]
    public class ResourceFilter : NonPersistentBaseObject
    {
        public ResourceFilter()
        { Resources = new BindingList<ResourceFilterItem>(); }
        public BindingList<ResourceFilterItem> Resources { get; }
    }

    [DevExpress.ExpressApp.DC.DomainComponent]
    public class ResourceFilterItem : NonPersistentBaseObject
    {
        private OreType solidResource;
        [DevExpress.Persistent.Base.VisibleInListView(false)]
        public OreType SolidResource
        {
            get => solidResource;
            set => SetPropertyValue(ref solidResource, value);
        }

        private FluidResource fluidResource;
        [DevExpress.Persistent.Base.VisibleInListView(false)]
        public FluidResource FluidResource
        {
            get => fluidResource;
            set => SetPropertyValue(ref fluidResource, value);
        }

        bool selected;
        public bool Selected
        {
            get => selected;
            set => SetPropertyValue(ref selected, value);
        }

        public string ResourceName
        {
            get => SolidResource == null ? FluidResource?.Name : SolidResource.Name;
        }
    }
}
