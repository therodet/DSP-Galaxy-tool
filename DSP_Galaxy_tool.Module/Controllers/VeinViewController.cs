using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DSP_Galaxy_tool.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSP_Galaxy_tool.Module.Controllers
{
    public class VeinViewController : ObjectViewController<ListView, Classes.IResourceOwner>
    {
        public VeinViewController()
        {
            InitComponents();
        }
        private void ResourceSearchAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            ResourceFilter filter = new ResourceFilter();
            var ores = ObjectSpace.GetObjects<OreType>();
            foreach (OreType ore in ores)
            {
                ResourceFilterItem item = new ResourceFilterItem();
                item.SolidResource = ore;
                if (selectedOres != null && selectedOres.Contains(ore))
                { item.Selected = true; }
                filter.Resources.Add(item);
            }

            var fluids = ObjectSpace.GetObjects<FluidResource>();
            foreach (FluidResource fluid in fluids)
            {
                ResourceFilterItem item = new ResourceFilterItem();
                item.FluidResource = fluid;
                if (selectedFluids != null && selectedFluids.Contains(fluid))
                { item.Selected = true; }
                filter.Resources.Add(item);
            }

            e.View = Application.CreateDetailView(ObjectSpace, filter, false);
            e.DialogController.SaveOnAccept = false;
        }

        IEnumerable<OreType> selectedOres;
        IEnumerable<FluidResource> selectedFluids;
        private void ResourceSearchAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            selectedOres = ((ResourceFilter)e.PopupWindowViewCurrentObject).Resources
                .Where(r => r.Selected && r.SolidResource != null)
                .Select(r => r.SolidResource);
            selectedFluids = ((ResourceFilter)e.PopupWindowViewCurrentObject).Resources
                .Where(r => r.Selected && r.FluidResource != null)
                .Select(r => r.FluidResource);

            CriteriaOperator resourceCriteria = null;
            foreach(OreType ore in selectedOres)
            { resourceCriteria &= CriteriaOperator.Parse("AllVeins[VeinType.OreType = ?]", ore); }
            foreach (FluidResource fluid in selectedFluids)
            { resourceCriteria &= CriteriaOperator.Parse("AllFluids[ResourceID = ?]", fluid.ResourceID); }

            View.CollectionSource.Criteria["Resources"] = resourceCriteria;
        }

        private Container components;
        PopupWindowShowAction resourceSearchAction;
        private void InitComponents()
        {
            components = new Container();

            resourceSearchAction = new PopupWindowShowAction(components);
            resourceSearchAction.Id = nameof(resourceSearchAction);
            resourceSearchAction.Caption = "Search";
            resourceSearchAction.ImageName = "Action_Search";
            resourceSearchAction.Category = "Edit";
            resourceSearchAction.SelectionDependencyType = SelectionDependencyType.Independent;
            resourceSearchAction.CustomizePopupWindowParams += ResourceSearchAction_CustomizePopupWindowParams;
            resourceSearchAction.Execute += ResourceSearchAction_Execute;

            RegisterActions(components);
        }

    }
}
