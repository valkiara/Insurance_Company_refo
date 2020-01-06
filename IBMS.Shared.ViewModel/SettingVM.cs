using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class SettingVM
    {
        public int SettingID { get; set; }
        public int DesignationID { get; set; }
        public string DesignationName { get; set; }
        public string SettingCode { get; set; }
        public string SettingDescription { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
