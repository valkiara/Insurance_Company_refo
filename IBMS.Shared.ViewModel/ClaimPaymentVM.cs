using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class ClaimPaymentVM
    {
        public int ClaimPaymentID { get; set; }
        public int ClaimRecordingID { get; set; }
        public decimal ClaimAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public bool IsCompleted { get; set; }
        public string Notes { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public List<ClaimPaymentMethodVM> ClaimPaymentMethodDetails { get; set; }
    }

    public class ClaimPaymentMethodVM
    {
        public int ClaimPaymentMethodID { get; set; }
        public string ChequeNo { get; set; }
        public int PaymentTypeID { get; set; }
        public string PaymentTypeName { get; set; }
        public decimal PaidAmount { get; set; }
        public string PaidDate { get; set; }
        public bool IsFinal { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }
}
