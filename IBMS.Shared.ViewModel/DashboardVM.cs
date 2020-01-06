using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class DashboardVM
    {
        public int PendingQuotationCount { get; set; }
        public int ApprovedQuotationCount { get; set; }

        public int NotCreatingQuotationCount { get; set; }
        public int TCNIQuotationCount { get; set; }

        public int ClientRequestCount { get; set; }

        public int QuotationTotalCount { get; set; }

        public List<DashboardVM> ClientRequestWithQuotation { get; set; }

        public int count { get; set; }
        public string month { get; set; }

    }
}
