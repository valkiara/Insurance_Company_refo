using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class PolicyInfoRecVM
    {
        public int PolicyInfoRecID { get; set; }
        public string PolicyNumber { get; set; }
        public int QuotationHeaderID { get; set; }
        public int QuotationDetailsInsCompanyLineID { get; set; }
        public decimal SumAssured { get; set; }
        public int SumAssuredCurrencyTypeID { get; set; }
        public string SumAssuredCurrencyCode { get; set; }
        public decimal PremiumIncludingTax { get; set; }
        public decimal? NonCommissionPremium { get; set; }
        public decimal? GrossPremium { get; set; }
        public int PremiumIncludingTaxCurrencyTypeID { get; set; }
        public string PremiumIncludingTaxCurrencyCode { get; set; }
        public string PeriodOfCoverFromDate { get; set; }
        public string PeriodOfCoverToDate { get; set; }
        public string OtherExcessDescription { get; set; }
        public decimal? OtherExcessAmount { get; set; }
        public string TaxInvoiceNumber { get; set; }
        public string FileNumber { get; set; }
        public string InsuranceCompany { get; set; }

        public int? InsuranceSubClassID { get; set; }

        public int? InsuranceClassID { get; set; }
        public string InsuranceSubClass { get; set; }

        public string Description { get; set; }

        public string InsuranceClass { get; set; }
        public string VehicleNumber { get; set; }
        public int? TransactionTypeID { get; set; }
        public int? CommissionStructureHeaderID { get; set; }
        public int? IntroducerID { get; set; }
        public int? AccountExecutiveID { get; set; }
        public decimal? TotalCommission { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public int InsuranceCompanyID { get; set; }
        public string InsuranceCompanyName { get; set; }
        public decimal CurrencyRate { get; set; }

        public string PolicyRequestedDate { get; set; }

        public string EndorsementCharges { get; set; }

        public string OtherCharges { get; set; }
        public string CustomerName { get; set; }


        public List<QuotationDetailsInsCompanyLineVM> quotationDetailsInsCompanyHeaderList;


        public List<PolicyCommissionPaymentVM> PolicyCommissionPaymentDetails { get; set; }

        public List<PolicyInfoChargeVM> policyInfoChargeList { get; set; }


        public List<BankTransactionVM> BankTransactionList { get; set; }

        public List<PolicyNewInfoChargeVM> PolicyNewInfoCharge { get; set; }



    }


    public class PolicyInfoRecNewVM
    {
        public int PolicyInfoRecID { get; set; }
        public string PolicyNumber { get; set; }
        public int QuotationHeaderID { get; set; }
        public int QuotationDetailsInsCompanyLineID { get; set; }
        public decimal SumAssured { get; set; }
        public int SumAssuredCurrencyTypeID { get; set; }
        public string SumAssuredCurrencyCode { get; set; }
        public decimal PremiumIncludingTax { get; set; }
        public decimal? NonCommissionPremium { get; set; }
        public decimal? GrossPremium { get; set; }
        public int PremiumIncludingTaxCurrencyTypeID { get; set; }
        public string PremiumIncludingTaxCurrencyCode { get; set; }
        public string PeriodOfCoverFromDate { get; set; }
        public string PeriodOfCoverToDate { get; set; }
        public string OtherExcessDescription { get; set; }
        public decimal? OtherExcessAmount { get; set; }
        public string TaxInvoiceNumber { get; set; }
        public string FileNumber { get; set; }
        public string InsuranceCompany { get; set; }
        public string InsuranceSubClass { get; set; }
        public string VehicleNumber { get; set; }
        public int? TransactionTypeID { get; set; }
        public int? CommissionStructureHeaderID { get; set; }
        public int? IntroducerID { get; set; }
        public int? AccountExecutiveID { get; set; }
        public decimal? TotalCommission { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public int InsuranceCompanyID { get; set; }
        public string InsuranceCompanyName { get; set; }
        public decimal CurrencyRate { get; set; }

        public string PolicyRequestedDate { get; set; }

        public string PolicyRenewalDate { get; set; }

        public string NotificationDate { get; set; }

        public string EndorsementCharges { get; set; }

        public string OtherCharges { get; set; }

        public bool IsSent { get; set; }
        public bool IsCancel { get; set; }

        public bool IsRenewal { get; set; }



        public List<QuotationDetailsInsCompanyLineVM> quotationDetailsInsCompanyHeaderList;

        public List<PolicyInfoChargeVM> policyInfoChargeList { get; set; }

        public List<PolicyNewInfoChargeVM> PolicyNewInfoCharge{ get; set; }

        //public List<PolicyInfoChargeVM> policyInfoChargeList { get; set; }


        //public List<BankTransactionVM> BankTransactionList { get; set; }
    }
    public class PolicyCommissionPaymentVM
    {
        public int PolicyCommisionPaymentID { get; set; }
        public int PolicyInfoRecID { get; set; }
        public int CommisionTypeID { get; set; }
        public string CommissionTypeName { get; set; }
        public decimal CommisionValue { get; set; }
        public decimal Amount { get; set; }
        public int ComStructLineID { get; set; }
        public double RateValue { get; set; }
        public int PartnerID { get; set; }
        public string PartnerName { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }

    public class PolicyNewCommissionPaymentVM
    {
        public int PolicyCommisionPaymentID { get; set; }
        public int PolicyInfoRecID { get; set; }
        public int CommisionTypeID { get; set; }
        
        public decimal Amount { get; set; }
      
        public int? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }


    public class PolicyNewInfoChargeVM
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
