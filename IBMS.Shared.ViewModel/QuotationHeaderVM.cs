using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class QuotationHeaderVM
    {
        public int QuotationHeaderID { get; set; }
        public int ClientRequestHeaderID { get; set; }
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public int PartnerID { get; set; }
        public string PartnerName { get; set; }
        public string RequestedDate { get; set; }
        public bool Status { get; set; }
        public string QuotationStatusCode { get; set; }
        public string QuotationStatusCodeDescription { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public string Other { get; set; }
        public int TransactionTypeID { get; set; }
        public string TransactionTypeName { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public List<QuotationLineVM> QuotationLineDetails { get; set; }
        public QuotationHeaderVM()
        {
            QuotationLineDetails = new List<QuotationLineVM>();
        }

        
    }

    public class QuotHeaderVM
    {
        public int QuotationHeaderID { get; set; }
        public int ClientRequestHeaderID { get; set; }
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public int PartnerID { get; set; }
        public string PartnerName { get; set; }
        public string RequestedDate { get; set; }
        public bool Status { get; set; }
        public string QuotationStatusCode { get; set; }
        public string QuotationStatusCodeDescription { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public string Other { get; set; }
        public int TransactionTypeID { get; set; }
        public string TransactionTypeName { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public List<QuoLineVM> QuotLineDetails { get; set; }
        public QuotHeaderVM()
        {
            QuotLineDetails = new List<QuoLineVM>();
        }


    }
    public class QuotationLineVM
    {
        public int QuotationLineID { get; set; }
        public int InsuranceClassID { get; set; }
        public string InsuranceCode { get; set; }
        public int InsuranceSubClassID { get; set; }
        public string InsuranceSubClassDescription { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public List<QuotationRequestedInsCompanyVM> RequestedInsuranceCompanyDetails { get; set; }
    }

    public class QuoLineVM
    {
        public int QuotationLineID { get; set; }
        public int InsuranceClassID { get; set; }
        public string InsuranceCode { get; set; }
        public int InsuranceSubClassID { get; set; }
        public string InsuranceSubClassDescription { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public List<QuotRequestedInsCompanyVM> RequestedQuotInsuranceCompanyDetails { get; set; }
    }

    public class QuotationRequestedInsCompanyVM
    {
        public int QuotationRequestedInsCompanyID { get; set; }
        public int InsuranceCompanyID { get; set; }
        public string InsuranceCompanyName { get; set; }
        public string InsuranceCompanyEmail { get; set; }
        public bool Status { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public List<QuotationDetailsInsCompanyHeaderVM> QuotationDetailsInsCompanyHeaderDetails { get; set; }
    }


    public class QuotRequestedInsCompanyVM
    {
        public int QuotationRequestedInsCompanyID { get; set; }
        public int InsCompanyID { get; set; }
        public string InsCompanyName { get; set; }
        public string InsuranceCompanyEmail { get; set; }
        public bool Status { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int InsSubClassID { get; set; }
        public string InsSubClassName { get; set; }


    }

    public class QuotDetailsInsCompanyHeaderVM
    {
        public int QuotationDetailsInsCompanyHeaderID { get; set; }
        public double PremiumIncludingTax { get; set; }
        public string ExcessDescription { get; set; }
        public decimal ExcessAmount { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        
    }
    public class QuotationDetailsInsCompanyHeaderVM
    {
        public int QuotationDetailsInsCompanyHeaderID { get; set; }
        public double PremiumIncludingTax { get; set; }
        public string ExcessDescription { get; set; }
        public decimal ExcessAmount { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public List<QuotationDetailsInsCompanyLineVM> QuotationDetailsInsCompanyLineDetails { get; set; }
    }

    public class QuotationDetailsInsCompanyLineVM
    {
        public int QuotationDetailsInsCompanyLineID { get; set; }
        public int InsuranceSubClassID { get; set; }
        public string InsuranceSubClassDescription { get; set; }
        public decimal SumInsured { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public List<QuotationDetailsInsCompanyScopeVM> QuotationDetailsInsCompanyScopeDetails { get; set; }
    }

    public class QuotationDetailsInsCompanyScopeVM
    {
        public int QuotationDetailsInsCompanyScopeID { get; set; }
        public string ScopeDescription { get; set; }
        public string ExcessType { get; set; }
        public decimal ExcessAmount { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }

    public enum QuotationStatusCodeEnum
    {
        [Description("Quotation Not Created")]
        QNC,
        [Description("Quotation Pending")]
        QP,
        [Description("Quotation Ready")]
        QR,
        [Description("Not Approved")]
        NA,
        [Description("Customer Approved")]
        CA,
        [Description("Temporary Cover Note Issued")]
        TCNI
    }
}
