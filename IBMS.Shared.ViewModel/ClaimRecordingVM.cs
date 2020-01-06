using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class ClaimRecordingVM
    {
        public int ClaimRecordingID { get; set; }
        public int PolicyInfoRecID { get; set; }
        public string ClaimNo { get; set; }
        public string DateOfLoss { get; set; }
        public string DateOfIntimation { get; set; }
        public string CauseOfLoss { get; set; }
        public string DamageDescription { get; set; }
        public decimal AmountClaimed { get; set; }
        public decimal AmountPaid { get; set; }
        public string FileReferenceNo { get; set; }
        public string FilePath { get; set; }
        public string PaymentPendingReason { get; set; }
        public string WithdrawReason { get; set; }
        public bool IsOpened { get; set; }
        public bool IsRejected { get; set; }
        public bool IsWithdrawn { get; set; }
        public string DischargedDate { get; set; }
        public string ClaimDocumentsReceivedDate { get; set; }
        public string ClaimDocumentsEmailedDate { get; set; }
        public string PaymentAdviceReceviedDate { get; set; }
        public string PaymentAdviceEmailedDate { get; set; }
        public string OriginalDocumentscourieredDate { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public ClaimRecHistoryVM ClaimRecHistoryDetails { get; set; }
        public int YearID { get; set; }
        public string Year { get; set; }
        public string Insurer { get; set; }
        public string VehicleNumber { get; set; }
        public string Description { get; set; }
        public decimal PolicyExcess { get; set; }
        public int InsClass { get; set; }
        public string Code { get; set; }
        public int InsClassTypeID { get; set; }
        public int InsClassTypeDes { get; set; }
        public int ClaimStatus { get; set; }
        public string ClaimDescription { get; set; }
        public string ChequeNo { get; set; }
        public string RejectedReason { get; set; }
        public string ContactPersonDetails { get; set; }
        public string Other { get;set; }
        public  string FileName { get; set; }
        public string  ClaimentInfo { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int ClaimPaidStatus { get; set; }
        public string ClaimPaidStatusDescription { get; set; }
        public decimal CurrencyRate { get; set; }
        public string DateOfOpen { get; set; }
        public string DateOfWithdraw { get; set; }
        public string DateOfReject { get; set; }
        public int CurrencyID { get; set; }
        public string CurrencyCode { get; set; }
        public List<ClaimRecPendingDocVM> ClaimRecPendingDocDetails { get; set; }
    }

    public class YearVM
    {
        public int YearID { get; set; }
        public string Description { get; set; }
    }

    public class InsClassTypeVM
    {
        public int InsClassTypeID { get; set; }
        public string InsClassTypeDes { get; set; }
    }

    public class ClaimStatusVM
    {
        public int ClaimStatus { get; set; }
        public string ClaimDescription { get; set; }
    }
    public class StatusVM
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }

    public class UploadDocVM
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public int File { get; set; }
        public int DocTypeID { get; set; }
        public int DocTypeIndexID { get; set; }
        
    }
    public class ClaimPaidStatusVM
    {
        public int ClaimPaidStatus { get; set; }
        public string ClaimPaidStatusDescription { get; set; }
    }


    public class ClaimRecHistoryVM
    {
        public int ClaimRecHistoryID { get; set; }
        public string Description { get; set; }
        public string RecordingDate { get; set; }
        public string Reason { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }

    public class ClaimRecPendingDocVM
    {
        public int ClaimRecPendingDocID { get; set; }
        public int DocumentID { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }
}
