using IBMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class ClientRequestVM
    {
        //public int ClientRequestID { get; set; }
        //public bool IsClientExist { get; set; }
        //public int ClientID { get; set; }
        public bool IsClientUpdated { get; set; }
        public bool IsClientAdded { get; set; }

        public int CustomerType { get; set; }
        public ClientVM ClientDetails { get; set; }
        public ClientRequestHeaderVM ClientRequestHeaderDetails { get; set; }
        public int UserID { get; set; }

        public int TitleID { get; set; }

    }

    public class ClientVM
    {
        public int ClientID { get; set; }

        public int CustomerType { get; set; }
        public string ClientName { get; set; }

        public string ClientOtherName { get; set; }

        public string ClientAddress { get; set; }
        public string NIC { get; set; }
        public string ContactNo { get; set; }
        public string FixedLine { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string PPID { get; set; }

        public decimal FamilyDiscount { get; set; }
        public string AdditionalNote { get; set; }

        public string InspectionDate { get; set; }
        public int HomeCountryID { get; set; }
        public string HomeCountryName { get; set; }
        public int ResidentCountryID { get; set; }
        public string ResidentCountryName { get; set; }
        public int BusinessUnitID { get; set; }
        public string BusinessUnitName { get; set; }
        public int PaymentID { get; set; }
        public decimal? PaymentAmount { get; set; }
        public decimal? LoadnigRate { get; set; }
        public decimal? DeductionRate { get; set; }
        public int DeductionID { get; set; }
        public int PolicyInfoID { get; set; }
        public int Province { get; set; }
        public int DistrictId { get; set; }
        public string Description { get; set; }
        public string MemberID { get; set; }
        public int? PolicyMethod { get; set; }
        public string Premium { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public int UserID { get; set; }
        public int districtId { get; set; }
        public string ModifiedDate { get; set; }
        public bool IsClientUpdated { get; set; }
        public bool IsClientAdded { get; set; }
        public string Other { get; set; }
        public string BRNo { get; set; }
        public string FileNo { get; set; }
        public int RelationShipID { get; set; }
        public string Relationship { get; set; }

        public string Deductible { get; set; }

        public int GenderID { get; set; }
        public string Gender { get; set; }

        public int TitleID { get; set; }
        public string TitleName { get; set; }

        public string MemberOtherName { get; set; }

        public string InceptionDate { get; set; }

        public int PremiumID { get; set; }
        public string PremiumName { get; set; }

        public int CurrencyID { get; set; }
        public int frequncyID { get; set; }
        public string CurrencyName { get; set; }

        public int AgentID { get; set; }
        public string ExtraText { get; set; }

        public string PolicyStartDate { get; set; }

        public string PolicyEndDate { get; set; }
        public string RequestedDate { get; set; }
        public List<FamilyMembersVM> FamilyDetails { get; set; }
        public List<PaymentVM> PaymentDetails { get; set; }
        public List<BankTransactionVM> BankTransactionDetails { get; set; }
        public List<DeductionDetailsVM> DeductionDetails { get; set; }
        public List<PolicyInfoBUPAVM> PolicyInfoBUPADetails { get; set; }
      //  public List<BUPAPremiumVM> BUPAPremiumVM { get; set; }
        public float Exclusions { get; set; }
        public string OptionalCovers { get; set; }
        public string Occupation { get; set; }
        public int?  Status { get; set; }
        public string PremiumAccept { get; set; }
        public string JoinDate { get; set; }

        public decimal PaiedAmount { get; set; }
        public string MembershipID { get; set; }
        public string Exclu { get; set; }
        public int SchemeID { get; set; }
        public string GroupID { get; set; }
        public int ClientStatus { get; set; }
        public string FrequncyDID { get; set; }

        public decimal Outstanding { get; set; }
        public decimal COutstanding { get; set; }

        public string SeqNo { get; set; }
    }

    public class ChildrenDetailsVM
    {
        public int ChildID { get; set; }
        public string ChildName { get; set; }
        public int? ChildAge { get; set; }
        public int ClientID { get; set; }

    }

    public class ClientRequestHeaderVM
    {
        public int ClientRequestHeaderID { get; set; }
        public int ClientID { get; set; }
        public string ClientName { get; set; }


        public string ClientOtherName { get; set; }
        public int PartnerID { get; set; }
        public int? AgentID { get; set; }
        public string PartnerName { get; set; }
        public string AgentName { get; set; }
        public string PremiumName { get; set; }
        public int PaymentID { get; set; }
        public decimal? PaymentAmount { get; set; }
        public int BusinessUnitID { get; set; }
        public string BusinessUnitName { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string RequestedDate { get; set; }
        public bool IsQuotationCreated { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }

        public string JoinDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string Other { get; set; }
        public int transactionType { get; set; }

        public int EmployeeID { get; set; }

        public int IntroducerID { get; set; }

        public int AccountHandlerID { get; set; }

        public string EmployeeName { get; set; }

        public string IntroducerName { get; set; }

        public string AccountHandlerName { get; set; }


        public int RelationShipID { get; set; }
        public string Relationship { get; set; }

        public string GroupID { get; set; }

        public float Exclusions { get; set; }
        public string OptionalCovers { get; set; }
        public string Occupation { get; set; }
        

        public int GenderID { get; set; }
        public string Gender { get; set; }

        public int PilotPremiumID { get; set; }

        public int DeductibleID { get; set; }


        public List<ClientRequestLineVM> ClientRequestLineDetails { get; set; }
        public List<FamilyMembersVM> FamilyDetails { get; set; }
        public List<PaymentVM> PaymentDetails { get; set; }
        public List<DebitNoteVM> DebitNoteDetails { get; set; }

        public string InspectionDate { get; set; }

        public string PolicyStartDate { get; set; }

        public string PolicyEndDate { get; set; }

        public string FileNo { get; set; }
        public string AdditionalNote { get; set; }

        public int FrequncyID { get; set; }

        public int CurrancyID { get; set; }
        public int SchemeID { get; set; }
      
        public string MembershipID { get; set; }
        public string Exclu { get; set; }
        

        //   public CurrencyVM currancyvm { get; set; }

        public string  CurrancyCode { get; set; }
        public string Year { get; set; }
        public string FrequncyDID { get; set; }
        public string Frequncy { get; set; }
        public string FrequncyCat { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? InActiveEffectiveDate { get; set; }
    }

    public class ClientRequestLineVM
    {
        public int ClientRequestLineID { get; set; }
        public int InsSubClassID { get; set; }
        public string InsSubClassName { get; set; }
        public int InsClassID { get; set; }
        public string InsClassName { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }

        public List<ClientPropertyVM> ClientPropertyDetails { get; set; }
        public List<ClientRequestInsSubClassScopeVM> ClientRequestInsSubClassScopeDetails { get; set; }
    }

    public class BankTransactionVM
    {
        public int BankDetailID { get; set; }
        public int? BankID { get; set; }

        public string BankName { get; set; }
        public string DraftNo { get; set; }
        public decimal? BankAmount { get; set; }
        public int? ClientID { get; set; }
        public int? PolicyInfoRecID { get; set; }
        public int? PaymentMethodID { get; set; }
        public int? BankRate { get; set; }
        public string RequestDate { get; set; }
        public int? AgentID { get; set; }
        public decimal? AgentAmount { get; set; }
        public decimal? SGSAmount { get; set; }
        public decimal? IBSAmount { get; set; }
        public string AdditionalInfo { get; set; }

        public string RequestingDate { get; set; }

        public string PaymentDate { get; set; }
        public int currencyType { get; set; }
        public decimal? PaiedAmount { get; set; }

        public decimal? ExchangeRate { get; set; }
        public decimal? BalanceAmount { get; set; }
        public string FrequncyID { get; set; }
        public string FrequncyDID { get; set; }


    }

    public class BUPABankTransactionVM
    {
        public int BankDetailID { get; set; }
        public int? BankID { get; set; }

        public string BankName { get; set; }
        public string DraftNo { get; set; }
        public decimal? BankAmount { get; set; }
        public int? ClientID { get; set; }
        public int? PolicyInfoRecID { get; set; }
        public int? PaymentMethodID { get; set; }
        public int? BankRate { get; set; }
        public string RequestDate { get; set; }
        public int? AgentID { get; set; }
        public decimal? AgentAmount { get; set; }
        public decimal? SGSAmount { get; set; }
        public decimal? IBSAmount { get; set; }
        public string AdditionalInfo { get; set; }

        public string RequestingDate { get; set; }

        public string PaymentDate { get; set; }
        public int currencyType { get; set; }
        public decimal? PaiedAmount { get; set; }

        public decimal? ExchangeRate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string CurrancyCode { get; set; }
        public string Year { get; set; }
        public string AgentName { get; set; }
        public string PayMode { get; set; }
        public string Frequncy { get; set; }
        public string FrequncyCat { get; set; }



    }
    public class DistrictVM
    {
        public int DistrictId { get; set; }
        public string Description { get; set; }
        public int? ProvinceID { get; set; }

    }
    public class FrequncyDetailsVM
    {
        public int RowID { get; set; }
        public string FrequncyID { get; set; }
        public string  Code { get; set; }
        public string Description { get; set; }

    }

    public class PolicyInfoBUPAVM
    {
        public int PolicyInfoID { get; set; }
        public string Premium { get; set; }
        public string MemberID { get; set; }
        public int? PolicyMethod { get; set; }
        public int? ClientID { get; set; }
    }
    public class DeductionDetailsVM
    {
        public int DeductionID { get; set; }


        public string PremiumHolder { get; set; }
        public decimal? LoadingRate { get; set; }
        public decimal? DeductionRate { get; set; }
        public decimal? PremiumAmount { get; set; }
        public decimal? NetPremium { get; set; }
        public int? ClientID { get; set; }

        public int? FamilyMemberID { get; set; }

        public int? GroupFamilyMemberID { get; set; }

        public int? PremiumHolderType { get; set; }

        public int? PremiumID { get; set; }

        public decimal? Other { get; set; }

        public string Deductible { get; set; }

        public decimal totPremium { get; set; }

        public string JoinDate { get; set; }
        public string MI { get; set; }
        public string SeqNo { get; set; }
        public string SNo { get; set; }
        public string SeqSubNo { get; set; }
        //public List<DeductionDetailsLineVM> deductionDetailsLine { get; set; }
    }

    public class DeductionDetailsLineVM
    {
        public int DeductionID { get; set; }


        public string PremiumHolder { get; set; }


        public decimal? LoadingRate { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? PremiumAmount { get; set; }
        public decimal? NetPremium { get; set; }
        public int? ClientID { get; set; }

        public int? FamilyMemberID { get; set; }

        public int? GroupFamilyMemberID { get; set; }
    }


    public class AllYearVM
    {
        public int Year { get; set; }
        public string Desc { get; set; }
      
    }



    public class ClientPropertyVM
    {
        public int ClientPropertyID { get; set; }
        public string ClientPropertyName { get; set; }
        public string BRNo { get; set; }
        public string VATNo { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }

    public class ClientRequestInsSubClassScopeVM
    {
        public int ClientRequestInsSubClassScopeID { get; set; }
        public int CommonInsScopeID { get; set; }
        public string CommonInsScopeName { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }

    public class ClientPaymentsVM
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public decimal PaymentAmount { get; set; }

        public int NoOfClientRequest { get; set; }

        public int NoOfQuitation { get; set; }

        public virtual tblPayment tblPayment { get; set; }

        public List<tblPayment> PaymentList { get; set; }


    }

    public class TitleVM
    {
        public int TitleID { get; set; }
        public string TitleName { get; set; }


    }

    public class HospitalVM
    {
        public int HospitalID { get; set; }
        public string HospitalName { get; set; }

        public string HospitalAddress { get; set; }
        public int CountryID { get; set; }
    }

    public class RelatioshipVM
    {
        public int RelationShipID { get; set; }
        public string Relationship { get; set; }


    }

    public class GenderVM
    {
        public int GenderID { get; set; }
        public string Gender { get; set; }


    }

    public class PilotPRMVM
    {
        public int PID { get; set; }
        public string Description { get; set; }


    }

    public class DeductionTypeVM
    {
        public int DeductionID { get; set; }
        public string DeductionName { get; set; }

        public int? BusinessUnit { get; set; }

        public decimal? DeductionAmount { get; set; }

        public decimal? DeductionRate { get; set; }


    }

    public class PremiumHolderTypeVM
    {
        public int PremiumHolderTypeID { get; set; }
        public string PremiumHolderType { get; set; }


    }

    public class AgeWisePremiumVM
    {
        public int AgeID { get; set; }
        public int? FromDate { get; set; }

        public int? ToDate { get; set; }

        public decimal? PremiumValue { get; set; }

        public int? PremiumID { get; set; }


    }

    public class PilotPremiumVM
    {
        public int PremiumID { get; set; }
        public int? DedctibleType { get; set; }

        public int? PremiumType { get; set; }

        public decimal? Premium { get; set; }

        public string PremiumName { get; set; }


    }

    public class CustomerRequestDetailsVM
    {
        public List<FamilyDetailVM> FamilyDetails { get; set; }
        public int? AgentType { get; set; }
        public int? TitleID { get; set; }
        public string ClientName { get; set; }
        public string ClientOtherName { get; set; }
        public string ClientAddress { get; set; }
        public string ContactNo { get; set; }
        public string FixedLine { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string PartnerID { get; set; }
        public string JoinDate { get; set; }
        public int? GroupID { get; set; }
        public string AdditionalNote { get; set; }
        public int? HomeCountryID { get; set; }
        public int? ResidentCountryID { get; set; }
        public int? CurrencyID { get; set; }
        public int? FrequncyID { get; set; }
        public int? Exclusions { get; set; }
        public string Exclu { get; set; }
        public int? MembershipID { get; set; }
        public string OptionalCovers { get; set; }
        public string Occupation { get; set; }
        public int? SchemeID { get; set; }
        public string PremiumAccept { get; set; }
        public string ClientStatus { get; set; }
        public string RequestedDate { get; set; }
        public string PolicyStartDate { get; set; }
        public string PolicyEndDate { get; set; }
        public decimal? FamilyDiscount { get; set; }
        public string NIC { get; set; }
        public string PPID { get; set; }
        public int? BusinessUnitID { get; set; }
    }

    public class FamilyDetailVM
    {
        public string MemberName { get; set; }
        public string MemberDOB { get; set; }
        public string NIC { get; set; }
        public string Contact  { get; set; }
        public string JoinDate { get; set; }
        public decimal? Exclusions { get; set; }
    }



}
