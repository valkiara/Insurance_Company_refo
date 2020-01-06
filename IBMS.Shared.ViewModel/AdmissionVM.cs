using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class AdmissionVM
    {
        
        public int PatientAdmissionId { get; set; }
        public string ReferenceNo { get; set; }
        public string PatientName { get; set; }
        public string DateOfBirth { get; set; }
        public string PassportNumber { get; set; }
        public string Scheme { get; set; }
        public string InceptionDate { get; set; }
        public decimal Deductible { get; set; }
        public decimal DeductibleUsedForTheYear { get; set; }
        public decimal Exclusions { get; set; }
        public string Hospital { get; set; }
        public string AdmissionDate { get; set; }
        public string DischargedDate { get; set; }
        public string BHTNumber { get; set; }
        public string Illness { get; set; }
        public string ConsultantName { get; set; }
        public string InformedBy { get; set; }
        public decimal GOPAmount { get; set; }
        public string GOPConfirmBy { get; set; }
        public string GOPIssueDate { get; set; }
        public decimal ExtendedGOP { get; set; }
        public string HandledBy { get; set; }
        public decimal FinalAmount { get; set; }
        public decimal ConsultantFee { get; set; }
        public string FinalBillRecievedDate { get; set; }
        public decimal FinalBillGivenToSgs { get; set; }
        public decimal ClaimsDcsSentToAviva { get; set; }
        public string ClaimSettledDate { get; set; }
        public decimal ReferalFee { get; set; }
        public string ReferalFeeReceivedDate { get; set; }
        public string ReferalFeeReceivedBank { get; set; }
        public string ReferalFeeReceivedChequeNumber { get; set; }
        public string ReferalFeeReceivedTtTransfer { get; set; }
        public decimal PaymentGivenToAccount { get; set; }
        public string Remark { get; set; }

        public int PatientID { get; set; }

        public decimal IncurredAmount { get; set; }

        public string ClaimDocumentReceivedDate { get; set; }

        public string ClaimDocumentsEmailedDate { get; set; }

        public string PaymentAdviceReceviedDate { get; set; }

        public string PaymentAdviceEmailedDate { get; set; }

        public decimal TotalAmountPaid { get; set; }

        public int CurrencyType { get; set; }

        public string OriginalDocumentscourieredDate { get; set; }

        public string AirwayBillNo { get; set; }

        public int BUID { get; set; }

        public int DeductionID { get; set; }

        public int CreateBy { get; set; }

        public string CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }

        public int PremiumHolderType { get; set; }
        public int CurrancyID { get; set; }

        public string  CurrancyCode { get; set; }


        public string InimatedDate { get; set; }
        public string ExtendedGOPDate { get; set; }

        public decimal FinalBillAmount { get; set; }

        public string CMAInvoiceNumber { get; set; }
        public string CaseNumber { get; set; }
        public string CountryID { get; set; }
        public string MembershipID { get; set; }
        

    }
}
