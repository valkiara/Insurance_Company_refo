using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class CommissionFormatVM
    {
        public int RefNo { get; set; }
        public string Name { get; set; }
        public int PolicyNo { get; set; }
        public string TaxInvoiceNumber { get; set; }
        public string VehicleNumber { get; set; }
        public string InsuranceIssueDate { get; set; }
        public decimal? pre_basic { get; set; }
        public decimal pre_SRCC { get; set; }
        public decimal prm_TC { get; set; }
        public decimal Rcomm { get; set; }
        public string Advice { get; set; }
        public string Insurer { get; set; }
        public int Status { get; set; }
    }
}
