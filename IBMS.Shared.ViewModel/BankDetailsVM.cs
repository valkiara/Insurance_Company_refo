using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
   public class BankDetailsVM
    {
        public int BankID { get; set; }
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public string BankAddress { get; set; }
        public int? DiscountRatio { get; set; }
        public int? BUID { get; set; }
    }
}
