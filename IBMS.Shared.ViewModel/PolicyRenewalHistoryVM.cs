using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class PolicyRenewalHistoryVM
    {
        public int PolicyRenewalHistoryID { get; set; }
        public int PolicyInfoRecID { get; set; }
        public string RenewalDate { get; set; }
        public string NotificationDate { get; set; }
        public bool IsSent { get; set; }
        public bool IsCancel { get; set; }

        public bool IsRenewal { get; set; }

        public int Agent { get; set; }

        public string RenewalStartDate { get; set; }

        public string RenewalEndDate { get; set; }

        public int Executive { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }
}
