using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class BupaPremiumClientVM
    {
        public int? ClientID { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string NIC { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
        public string Occupation { get; set; }
        public string OptionalCovers { get; set; }
        public string Exclu { get; set; }
        public string MembershipID { get; set; }
        public int? ExtraInt1 { get; set; }
        public string ExtraText { get; set; }
        public decimal? Premium { get; set; }
        public decimal? NetPremium { get; set; }
        public string Deductibles { get; set; }
        public decimal?  DeductionAmount { get; set; }
        public string OptionalAmount { get; set; }
        public decimal? LoadingAmount { get; set; }
    }
}
