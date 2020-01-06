using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class ChargeTypeVM
    {
        public int ChargeTypeID { get; set; }
        public string ChargeTypeName { get; set; }
        public double Percentage { get; set; }

        public double Amount { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public int comstructID {get ;set;}
        public string ComStructName { get; set; }
        public ChargeTypeVM chargeTypeObj { get; set; }
    }
}
