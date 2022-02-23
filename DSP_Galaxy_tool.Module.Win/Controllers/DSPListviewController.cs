using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ListView = DevExpress.ExpressApp.ListView;

namespace DSP_Galaxy_tool.Module.Win.Controllers
{
    public class DSPListviewController : ObjectViewController<ListView, object>
    {
        GridListEditor listEditor;
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            listEditor = View.Editor as GridListEditor;
            if (listEditor != null)
            {
                listEditor.GridView.MouseDown += GridView_MouseDown;
            }
        }
        protected override void OnViewControlsDestroying()
        {
            if (listEditor != null)
            {
                listEditor.GridView.MouseDown -= GridView_MouseDown;
            }

            base.OnViewControlsDestroying();
        }

        /** Make clicking an editable Check Box cell immediatly toggle the value */
        private void GridView_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hi = view.CalcHitInfo(e.Location);
            if (hi.InRowCell && view.FocusedValue is bool)
            {
                view.FocusedRowHandle = hi.RowHandle;
                view.FocusedColumn = hi.Column;
                view.ShowEditor();
                CheckEdit edit = (view.ActiveEditor as CheckEdit);
                if (edit != null)
                {
                    edit.Toggle();
                    (e as DevExpress.Utils.DXMouseEventArgs).Handled = true;
                }
            }
        }
    }
}
