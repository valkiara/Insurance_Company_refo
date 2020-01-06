using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class InsuranceClassVM
    {
        public int InsuranceClassID { get; set; }
        public int BusinessUnitID { get; set; }
        public string BusinessUnitName { get; set; }
        public string InsuranceCode { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
