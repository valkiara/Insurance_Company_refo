using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class PaymentVM
    {
        public int PaymentID { get; set; }
        public int? ClientID { get; set; }
        public string ClientName { get; set; }
        public decimal? PaymentAmount { get; set; }
        public int? DeductionID { get; set; }
        public decimal? LoadingRate { get; set; }
        public decimal? DeductionRate { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string RequestingDate { get; set; }

        public string PaymentDate { get; set; }
        public int currencyType { get; set; }

        public string vehicle { get;set; }
        public decimal? ExchangeRate { get; set; }

        public string CurrencyName { get; set; }

        public List<DebitNoteVM> DebitNoteList { get; set; }
        public List<BankTransactionVM> BankTransactionList { get; set; }
    }

    public class DebitNoteVM
    {
        public int DebitNoteID { get; set; }
        public decimal TotalNonCommissionPremium { get; set; }
        public decimal TotalGrossPremium { get; set; }
        public int PolicyInfoRecID { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public List<BankTransactionVM> PolicyInfoPaymentLists { get; set; }
        public List<PolicyInfoPaymentVM> PolicyInfoPaymentList { get; set; }
    }

    public class PolicyInfoPaymentVM
    {
        public int PolicyInfoPaymentID { get; set; }
        public int PolicyInfoRecID { get; set; }
        public PolicyInfoRecVM PolicyInfoRecObj { get; set; }
        public decimal NonCommissionPremium { get; set; }
        public decimal GrossPremium { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public int BankDetailID { get; set; }
        public int? BankID { get; set; }
        public string DraftNo { get; set; }
        public decimal? BankAmount { get; set; }
        public decimal? Payment { get; set; }
        public int? ClientID { get; set; }
        public int? PaymentMethodID { get; set; }
        public int? BankRate { get; set; }
        public string RequestDate { get; set; }
        public int? AgentID { get; set; }
        public decimal? AgentAmount { get; set; }

        public decimal? ExchangeRate { get; set; }

        public int currencyType { get; set; }

        public string PaymentDate { get; set; }
        public List<PolicyInfoChargeVM> PolicyInfoChargeList { get; set; }
    }

    public class PolicyInfoChargeVM
    {
        public int PolicyInfoChargeID { get; set; }

        public int? ChargeTypeID { get; set; }
        public string ChargeTypeName { get; set; }
        public decimal Amount { get; set; }
        public decimal Percentage { get; set; }
        public bool IsCR { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public int ComHeaderID { get; set; }

        public string ModifiedDate { get; set; }
    }
}
