using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class BusinessUnitVM
    {
        public int BusinessUnitID { get; set; }
        public string BusinessUnit { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int count { get; set; }
    }
}
