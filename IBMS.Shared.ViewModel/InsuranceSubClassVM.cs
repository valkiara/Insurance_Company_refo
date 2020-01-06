using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class InsuranceSubClassVM
    {
        public int InsuranceSubClassID { get; set; }
        public int InsuranceClassID { get; set; }
        public string InsuranceClassCode { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
