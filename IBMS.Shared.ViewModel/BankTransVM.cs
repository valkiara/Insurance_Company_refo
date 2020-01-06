using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
 public   class BankTransVM
    {


        public string DraftNo { get; set; }
        public int  PaymentID { get; set; }

        public decimal Amount { get; set; }
        public int  AgentID { get; set; }
        public decimal AgentAmount { get; set; }
        public int ClientID { get; set; }
        public decimal IBSAmount { get; set; }
        public int currencyType { get; set; }
        public int BankID { get; set; }
        




    }
}
