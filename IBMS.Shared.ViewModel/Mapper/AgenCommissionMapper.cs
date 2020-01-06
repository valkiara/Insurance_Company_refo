using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel.Mapper
{
    public class AgenCommissionMapper
    {
        public string AgentCode { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string CurrencyType { get; set; }
        public decimal? Amount { get; set; }
        public decimal? CurrencyRate { get; set; }
    }
    public class AgenCommissionRootMapper
    {
        public int[] Ids { get; set; }
        public List<AgenCommissionMapper> AgenCommissionMappers { get; set; }
    }
}
