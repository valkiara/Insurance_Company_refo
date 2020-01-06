using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
   public class PaymentMethodsVM
    {
        public int PolicyMemberID { get; set; }
        public string PolicyMemberName { get; set; }
        public int? ClientID { get; set; }

    }
}
