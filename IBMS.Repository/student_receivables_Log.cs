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
    
    public partial class student_receivables_Log
    {
        public int LogID { get; set; }
        public string ID { get; set; }
        public string StudentRunningNumber { get; set; }
        public string StudentID { get; set; }
        public string TransactionType { get; set; }
        public string ReferenceNo { get; set; }
        public string TransactionDate { get; set; }
        public string ReceiptAmount { get; set; }
        public string EnterUser { get; set; }
        public string EnterDate { get; set; }
        public string ForeignValue { get; set; }
        public string Currency { get; set; }
        public string Refund { get; set; }
        public string PaymentType { get; set; }
        public System.DateTime LogDate { get; set; }
    }
}
