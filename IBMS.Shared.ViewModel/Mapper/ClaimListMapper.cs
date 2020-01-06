using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace IBMS.Shared.ViewModel.Mapper
{
    public class ClaimListMapper
    {
        //[JsonProperty("clientId")]
        public int? ClientId  { get; set; }
        //[JsonProperty("clientName")]
        public string ClientName { get; set; }
        //[JsonProperty("claimNo")]
        public string ClaimNo { get; set; }
        //[JsonProperty("dateOfLoss")]
        public DateTime? DateOfLoss { get; set; }
        //[JsonProperty("/*dateOfIntimation*/")]
        public DateTime? DateOfIntimation { get; set; }
        //[JsonProperty("causeOfLoss")]
        public string CauseOfLoss { get; set; }
        //[JsonProperty("amountClaim")]
        public decimal? AmountClaim { get; set; }
        //[JsonProperty("amountPaid")]
        public decimal? AmountPaid { get; set; }
        //[JsonProperty("status")]
        public string Status { get; set; }
    }
}
