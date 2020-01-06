using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class PolicyVM
    {
        public int PolicyID { get; set; }
        public string PolicyName { get; set; }
        public double Rate { get; set; }
        public int PolicyCategoryID { get; set; }
        public string PolicyCategoryName { get; set; }
        public int BusinessUnitID { get; set; }
        public string BusinessUnitName { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
