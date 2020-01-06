using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class IntegrationViewModel
    {
        public string type { get; set; }
        public DateTime paymentDate { get; set; }
        public int[] ids { get; set; }
        public List<AmountInfo> amountInfo { get; set; }
 
    }
   
    public class AmountInfo
    {
        public string CurrencyType { get; set; }
        public decimal Amount { get; set; }
        public decimal CurrencyRate { get; set; }
        public string AgentCode { get; set; }
    }
}
