using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class CommonInsuranceScopeVM
    {
        public int CommonInsuranceScopeID { get; set; }
        public string Description { get; set; }
        public int InsuranceClassID { get; set; }
        public string InsuranceClassCode { get; set; }
        public int InsuranceSubClassID { get; set; }
        public string InsuranceSubClassName { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
