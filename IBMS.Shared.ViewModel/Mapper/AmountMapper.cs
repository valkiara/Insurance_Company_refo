using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel.Mapper
{
    public class AmountMapper
    {
        public int[] Ids { get; set; }
        public List<AmountDetailMapper> AmountDetailMappers { get; set; }
    }

    public class AmountDetailMapper
    {
        public string CurrencyType { get; set; }
        public decimal? Amount { get; set; }
        public decimal? CurrencyRate { get; set; }
        public string AgentCode { get; set; }
    }
    public class CommissionDetailMapper
    {
        public string CurrencyType { get; set; }
        public decimal? Amount { get; set; }
        public decimal? CurrencyRate { get; set; }
        public string AgentCode { get; set; }
        public DateTime? PaidDate { get; set; }
        public string ClientName { get; set; }
    }
}
