using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class SingaporeAdmissionVM
    {
        public string ReferenceNo { get; set; }
        public string PatientName { get; set; }


        public string ClientRequestHeaderID { get; set; }
        public int? ClientID { get; set; }

        public int? DeductionID { get; set; }

        public int? PremiumID { get; set; }

        public string PremiumName { get; set; }
        public string ClientName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PassportNumber { get; set; }
        public string Scheme { get; set; }
        public DateTime InimatedDate { get; set; }
        public DateTime InceptionDate { get; set; }
        public decimal Deductible { get; set; }
        public decimal DeductibleUsedForTheYear { get; set; }
        public decimal Exclusions { get; set; }
        public string Hospital { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime DischargedDate { get; set; }
        public string CaseNumber { get; set; }
        public string Illness { get; set; }
        public string ConsultantName { get; set; }
        public string InformedBy { get; set; }
        public decimal GOPAmount { get; set; }
        public decimal ExtendedGOP { get; set; }

        public string ExtendedGOPDate { get; set; }
        public string HandledBy { get; set; }
        public decimal FinalAmount { get; set; }
        public decimal ConsultantFee { get; set; }
        public DateTime FinalBillRecievedDate { get; set; }
        public decimal ClaimsDcsSentToAviva { get; set; }
        public DateTime ClaimSettledDate { get; set; }
        public decimal ReferalFee { get; set; }
        public string CMAInvoiceNumber { get; set; }
        public DateTime ReferalFeeReceivedDate { get; set; }
        public decimal ReferalFeeReceivedBank { get; set; }
        public string ReferalFeeReceivedChequeNumber { get; set; }
        public string ReferalFeeReceivedTtTransfer { get; set; }
        public decimal PaymentGivenToAccount { get; set; }
        public string Remark { get; set; }

        public int HospitalID { get; set; }
        public string HospitalName { get; set; }

        public string HospitalAddress { get; set; }

        public decimal FinalBillAmount { get; set; }

        public int FamilyMemberID { get; set; }
        public string MemberName { get; set; }

        public int MemberID { get; set; }

        public int DependantID { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        
        public int PatientID { get; set; }
        public bool dependentClaim { get; set; }
        public List<FamilyMembersVM> FamilyDetails { get; set; }
    }
}
