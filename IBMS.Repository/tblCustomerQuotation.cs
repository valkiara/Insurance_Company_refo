//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IBMS.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCustomerQuotation
    {
        public int QuotationHeaderID { get; set; }
        public Nullable<int> ClientRequestHeaderID { get; set; }
        public Nullable<bool> RequestedStatus { get; set; }
        public string QuotationStatusCode { get; set; }
        public Nullable<bool> ReceivedStatus { get; set; }
        public Nullable<System.DateTime> RequestedDate { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string Other { get; set; }
        public Nullable<int> TransactionType { get; set; }
        public string RequestedQuotationPath { get; set; }
        public string ReceivedQuotationPath { get; set; }
        public string RequestedQuotationDescription { get; set; }
        public string ReceivedQuotationDescription { get; set; }
    }
}