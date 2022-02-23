using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSP_Galaxy_tool.Module.BusinessObjects
{
    [DevExpress.ExpressApp.DC.DomainComponent]
    [DevExpress.Persistent.Base.FileAttachment(nameof(File))]
    public class FileSelectorDialog : DevExpress.ExpressApp.NonPersistentBaseObject
    {
        DevExpress.Persistent.BaseImpl.FileData file;
        public DevExpress.Persistent.BaseImpl.FileData File
        {
            get => file;
            set => SetPropertyValue(ref file, value);
        }
    }
}
