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
    
    public partial class tbl_BOQDetails
    {
        public int Indx { get; set; }
        public string BOQNo { get; set; }
        public Nullable<System.DateTime> BOQCreatedDate { get; set; }
        public string BOQDescription { get; set; }
        public string BOQCustomerCode { get; set; }
        public string BOQCustomerName { get; set; }
        public string BOQCreatedUser { get; set; }
        public Nullable<decimal> BOQValue { get; set; }
        public string BOQApprovedUser { get; set; }
        public Nullable<decimal> UTEApprovedValue { get; set; }
        public Nullable<int> UTEApprovedCreditPeriod { get; set; }
    }
}
