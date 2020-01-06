using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBMS.Shared.ViewModel.Mapper;

namespace IBMS.Service.TransactionData
{
    public class ManageClaim
    {
        private UnitOfWork unitOfWork;
        public ManageClaim()
        {
            unitOfWork = new UnitOfWork();
        }

        #region Claim Recording
        public bool SaveClaimRecording(ClaimRecordingVM claimRecordingVM, int userID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    string claim = "";
                    string claimName = "";
                    //Save Claim Recording
                    tblClaimRecording claimRecording = new tblClaimRecording();

                    claimRecording.PolicyInfoRecID = claimRecordingVM.PolicyInfoRecID < 0 ? 0 : claimRecordingVM.PolicyInfoRecID;
                    claimRecording.ClaimNo = !string.IsNullOrEmpty(claimRecordingVM.ClaimNo) ? claimRecordingVM.ClaimNo : "";
                    claimRecording.DateOfLoss = !string.IsNullOrEmpty(claimRecordingVM.DateOfLoss) ? DateTime.ParseExact(claimRecordingVM.DateOfLoss, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.DateOfIntimation = !string.IsNullOrEmpty(claimRecordingVM.DateOfIntimation) ? DateTime.ParseExact(claimRecordingVM.DateOfIntimation, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.CauseOfLoss = !string.IsNullOrEmpty(claimRecordingVM.CauseOfLoss) ? claimRecordingVM.CauseOfLoss : "";
                    claimRecording.DamageDescription = !string.IsNullOrEmpty(claimRecordingVM.DamageDescription) ? claimRecordingVM.DamageDescription : "";
                    claimRecording.AmountClaimed = claimRecordingVM.AmountClaimed;
                    claimRecording.AmountPaid = claimRecordingVM.AmountPaid;
                    claimRecording.FileReferenceNo = claimRecordingVM.FileReferenceNo;
                    claimRecording.FilePath = claimRecordingVM.FilePath;
                    claimRecording.PaymentPendingReason = claimRecordingVM.PaymentPendingReason;
                    claimRecording.WithdrawReason = claimRecordingVM.WithdrawReason;
                    claimRecording.IsOpened = claimRecordingVM.IsOpened;
                    claimRecording.IsRejected = claimRecordingVM.IsRejected;
                    claimRecording.IsWithdrawn = claimRecordingVM.IsWithdrawn;
                    claimRecording.ClaimDocumentsEmailedDate = !string.IsNullOrEmpty(claimRecordingVM.ClaimDocumentsEmailedDate) ? DateTime.ParseExact(claimRecordingVM.ClaimDocumentsEmailedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.ClaimDocumentsReceivedDate = !string.IsNullOrEmpty(claimRecordingVM.ClaimDocumentsReceivedDate) ? DateTime.ParseExact(claimRecordingVM.ClaimDocumentsReceivedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.OriginalDocumentscourieredDate = !string.IsNullOrEmpty(claimRecordingVM.OriginalDocumentscourieredDate) ? DateTime.ParseExact(claimRecordingVM.OriginalDocumentscourieredDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.PaymentAdviceEmailedDate = !string.IsNullOrEmpty(claimRecordingVM.PaymentAdviceEmailedDate) ? DateTime.ParseExact(claimRecordingVM.PaymentAdviceEmailedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.PaymentAdviceReceviedDate = !string.IsNullOrEmpty(claimRecordingVM.PaymentAdviceReceviedDate) ? DateTime.ParseExact(claimRecordingVM.PaymentAdviceReceviedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.DischargedDate = !string.IsNullOrEmpty(claimRecordingVM.DischargedDate) ? DateTime.ParseExact(claimRecordingVM.DischargedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.Status = claimRecordingVM.Status;
                    claimRecording.CreatedBy = userID;
                    claimRecording.CreatedDate = DateTime.Now;
                    claimRecording.Year = claimRecordingVM.YearID;
                    claimRecording.ClaimStatus = claimRecordingVM.ClaimStatus;
                    claimRecording.InsClass = claimRecordingVM.InsClass;
                    claimRecording.InsClassTypes = claimRecordingVM.InsClassTypeID;
                    claimRecording.ClaimentInfo = claimRecordingVM.ClaimentInfo;
                    claimRecording.Insurer = claimRecordingVM.Insurer;
                    claimRecording.VehicleNumber = claimRecordingVM.VehicleNumber;
                    claimRecording.PolicyExcess = claimRecordingVM.PolicyExcess;
                    claimRecording.ChequeDetails = claimRecordingVM.ChequeNo;
                    claimRecording.RejectedReason = claimRecordingVM.RejectedReason;
                    claimRecording.ContactPersonDetails = claimRecordingVM.ContactPersonDetails;
                    claimRecording.Other = claimRecordingVM.Other;
                    claimRecording.CurrencyRate = claimRecordingVM.CurrencyRate;
                    claimRecording.ExtraInt1 = claimRecordingVM.CurrencyID;
                    claimRecording.ClaimPaidStatus = claimRecordingVM.ClaimPaidStatus;
                    claimRecording.DateOfOpen = !string.IsNullOrEmpty(claimRecordingVM.DateOfOpen) ? DateTime.ParseExact(claimRecordingVM.DateOfOpen, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.DateOfWithdraw = !string.IsNullOrEmpty(claimRecordingVM.DateOfWithdraw) ? DateTime.ParseExact(claimRecordingVM.DateOfWithdraw, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.DateOfReject = !string.IsNullOrEmpty(claimRecordingVM.DateOfReject) ? DateTime.ParseExact(claimRecordingVM.DateOfReject, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                    //using (var db = new PERFECTIBSEntities())
                    //{

                    //    var qry = (from ct in db.tblInsClasses
                    //               join comst in db.tblQuotationLines on ct.InsClassID equals comst.InsClassID
                    //               join comstt in db.tblQuotationHeaders on comst.QuotationHeaderID equals comstt.QuotationHeaderID
                    //               join ins in db.tblPolicyInformationRecordings on comstt.QuotationHeaderID equals ins.QuotationHeaderID
                    //               join inss in db.tblClaimRecordings on ins.PolicyInfoRecID equals inss.PolicyInfoRecID
                    //               where inss.PolicyInfoRecID == claimRecording.PolicyInfoRecID
                    //               select ct.Code).FirstOrDefault();
                    //    claim = qry.ToString();
                    //}

                    using (var db = new PERFECTIBSEntities())
                    {

                        var qry = (from ct in db.tblInsClasses
                                   join comst in db.tblQuotationLines on ct.InsClassID equals comst.InsClassID
                                   join comstt in db.tblQuotationHeaders on comst.QuotationHeaderID equals comstt.QuotationHeaderID
                                   join ins in db.tblPolicyInformationRecordings on comstt.QuotationHeaderID equals ins.QuotationHeaderID
                                   where ins.PolicyInfoRecID == claimRecording.PolicyInfoRecID
                                   select ct.Code).FirstOrDefault();
                        claim = qry.ToString();
                    }





                    tblYear yearInfo = unitOfWork.TblYearRepository.GetByID(claimRecording.Year);
                    claimName = yearInfo.Description;






                    //claimRecording.FileName = "";
                    unitOfWork.TblClaimRecordingRepository.Insert(claimRecording);
                    unitOfWork.Save();
                    int index = (int)claimRecording.ClaimRecordingID;


                    string codePrfix = "0000000";
                    int indexLength = index.ToString().Length;
                    codePrfix = codePrfix.Substring(0, (codePrfix.Length - indexLength) - 1);

                    if (index > 0)
                    {
                        tblClaimRecording claimRecordingUpdate = new tblClaimRecording();
                        claimRecordingUpdate = unitOfWork.TblClaimRecordingRepository.GetByID(index);

                        claimRecordingUpdate.FileName = claim + "/" + claimName + "/" + codePrfix + index.ToString();
                        unitOfWork.TblClaimRecordingRepository.Update(claimRecording);
                        unitOfWork.Save();
                    }






                    //Save Claim Recording History Details
                    if (claimRecordingVM.ClaimRecHistoryDetails != null)
                    {


                        tblClaimRecHistory claimRecHistory = new tblClaimRecHistory();
                        claimRecHistory.ClaimRecordingID = claimRecording.ClaimRecordingID;
                        claimRecHistory.Description = claimRecordingVM.ClaimRecHistoryDetails.Description;
                        claimRecHistory.RecordingDate = !string.IsNullOrEmpty(claimRecordingVM.ClaimRecHistoryDetails.RecordingDate) ? DateTime.ParseExact(claimRecordingVM.ClaimRecHistoryDetails.RecordingDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        claimRecHistory.Reason = claimRecordingVM.ClaimRecHistoryDetails.Reason;
                        claimRecHistory.CreatedBy = userID;
                        claimRecHistory.CreatedDate = DateTime.Now;
                        unitOfWork.TblClaimRecHistoryRepository.Insert(claimRecHistory);
                        unitOfWork.Save();
                    }
                    //Save Pending Document Details
                    if (claimRecordingVM.ClaimRecPendingDocDetails != null)
                    {
                        foreach (var pendingDoc in claimRecordingVM.ClaimRecPendingDocDetails)
                        {
                            tblClaimRecPendingDoc claimRecPendingDoc = new tblClaimRecPendingDoc();
                            claimRecPendingDoc.ClaimRecordingID = claimRecording.ClaimRecordingID;
                            claimRecPendingDoc.DocumentID = pendingDoc.DocumentID;
                            claimRecPendingDoc.CreatedBy = userID;
                            claimRecPendingDoc.CreatedDate = DateTime.Now;
                            unitOfWork.TblClaimRecPendingDocRepository.Insert(claimRecPendingDoc);
                            unitOfWork.Save();
                        }
                    }
                    //Complete the Transaction
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }



        public bool SavePilotClaimRecording(ClaimRecordingVM claimRecordingVM, int userID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    string claim = "";
                    string claimName = "";
                    //Save Claim Recording
                    tblClaimRecording claimRecording = new tblClaimRecording();

                    claimRecording.PolicyInfoRecID = claimRecordingVM.PolicyInfoRecID < 0 ? 0 : claimRecordingVM.PolicyInfoRecID;
                    claimRecording.ClaimNo = !string.IsNullOrEmpty(claimRecordingVM.ClaimNo) ? claimRecordingVM.ClaimNo : "";
                    claimRecording.DateOfLoss = !string.IsNullOrEmpty(claimRecordingVM.DateOfLoss) ? DateTime.ParseExact(claimRecordingVM.DateOfLoss, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.DateOfIntimation = !string.IsNullOrEmpty(claimRecordingVM.DateOfIntimation) ? DateTime.ParseExact(claimRecordingVM.DateOfIntimation, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.CauseOfLoss = !string.IsNullOrEmpty(claimRecordingVM.CauseOfLoss) ? claimRecordingVM.CauseOfLoss : "";
                    claimRecording.DamageDescription = !string.IsNullOrEmpty(claimRecordingVM.DamageDescription) ? claimRecordingVM.DamageDescription : "";
                    claimRecording.AmountClaimed = claimRecordingVM.AmountClaimed;
                    claimRecording.AmountPaid = claimRecordingVM.AmountPaid;
                    claimRecording.FileReferenceNo = claimRecordingVM.FileReferenceNo;
                    claimRecording.FilePath = claimRecordingVM.FilePath;
                    claimRecording.PaymentPendingReason = claimRecordingVM.PaymentPendingReason;
                    claimRecording.WithdrawReason = claimRecordingVM.WithdrawReason;
                    claimRecording.IsOpened = claimRecordingVM.IsOpened;
                    claimRecording.IsRejected = claimRecordingVM.IsRejected;
                    claimRecording.IsWithdrawn = claimRecordingVM.IsWithdrawn;
                    claimRecording.ClaimDocumentsEmailedDate = !string.IsNullOrEmpty(claimRecordingVM.ClaimDocumentsEmailedDate) ? DateTime.ParseExact(claimRecordingVM.ClaimDocumentsEmailedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.ClaimDocumentsReceivedDate = !string.IsNullOrEmpty(claimRecordingVM.ClaimDocumentsReceivedDate) ? DateTime.ParseExact(claimRecordingVM.ClaimDocumentsReceivedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.OriginalDocumentscourieredDate = !string.IsNullOrEmpty(claimRecordingVM.OriginalDocumentscourieredDate) ? DateTime.ParseExact(claimRecordingVM.OriginalDocumentscourieredDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.PaymentAdviceEmailedDate = !string.IsNullOrEmpty(claimRecordingVM.PaymentAdviceEmailedDate) ? DateTime.ParseExact(claimRecordingVM.PaymentAdviceEmailedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.PaymentAdviceReceviedDate = !string.IsNullOrEmpty(claimRecordingVM.PaymentAdviceReceviedDate) ? DateTime.ParseExact(claimRecordingVM.PaymentAdviceReceviedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.DischargedDate = !string.IsNullOrEmpty(claimRecordingVM.DischargedDate) ? DateTime.ParseExact(claimRecordingVM.DischargedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.Status = claimRecordingVM.Status;
                    claimRecording.CreatedBy = userID;
                    claimRecording.CreatedDate = DateTime.Now;
                    claimRecording.Year = claimRecordingVM.YearID;
                    claimRecording.ClaimStatus = claimRecordingVM.ClaimStatus;
                    claimRecording.InsClass = claimRecordingVM.InsClass;
                    claimRecording.InsClassTypes = claimRecordingVM.InsClassTypeID;
                    claimRecording.ClaimentInfo = claimRecordingVM.ClaimentInfo;
                    claimRecording.Insurer = claimRecordingVM.Insurer;
                    claimRecording.VehicleNumber = claimRecordingVM.VehicleNumber;
                    claimRecording.PolicyExcess = claimRecordingVM.PolicyExcess;
                    claimRecording.ChequeDetails = claimRecordingVM.ChequeNo;
                    claimRecording.RejectedReason = claimRecordingVM.RejectedReason;
                    claimRecording.ContactPersonDetails = claimRecordingVM.ContactPersonDetails;
                    claimRecording.Other = claimRecordingVM.Other;
                    claimRecording.CurrencyRate = claimRecordingVM.CurrencyRate;
                    claimRecording.ExtraInt1 = claimRecordingVM.CurrencyID;
                    claimRecording.ClaimPaidStatus = claimRecordingVM.ClaimPaidStatus;
                    claimRecording.DateOfOpen = !string.IsNullOrEmpty(claimRecordingVM.DateOfOpen) ? DateTime.ParseExact(claimRecordingVM.DateOfOpen, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.DateOfWithdraw = !string.IsNullOrEmpty(claimRecordingVM.DateOfWithdraw) ? DateTime.ParseExact(claimRecordingVM.DateOfWithdraw, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.DateOfReject = !string.IsNullOrEmpty(claimRecordingVM.DateOfReject) ? DateTime.ParseExact(claimRecordingVM.DateOfReject, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                    //using (var db = new PERFECTIBSEntities())
                    //{

                    //    var qry = (from ct in db.tblInsClasses
                    //               join comst in db.tblQuotationLines on ct.InsClassID equals comst.InsClassID
                    //               join comstt in db.tblQuotationHeaders on comst.QuotationHeaderID equals comstt.QuotationHeaderID
                    //               join ins in db.tblPolicyInformationRecordings on comstt.QuotationHeaderID equals ins.QuotationHeaderID
                    //               join inss in db.tblClaimRecordings on ins.PolicyInfoRecID equals inss.PolicyInfoRecID
                    //               where inss.PolicyInfoRecID == claimRecording.PolicyInfoRecID
                    //               select ct.Code).FirstOrDefault();
                    //    claim = qry.ToString();
                    //}

                    using (var db = new PERFECTIBSEntities())
                    {

                        var qry = (from ct in db.tblInsClasses
                                   join comst in db.tblQuotationLines on ct.InsClassID equals comst.InsClassID
                                   join comstt in db.tblQuotationHeaders on comst.QuotationHeaderID equals comstt.QuotationHeaderID
                                   join ins in db.tblPolicyInformationRecordings on comstt.QuotationHeaderID equals ins.QuotationHeaderID
                                   where ins.PolicyInfoRecID == claimRecording.PolicyInfoRecID
                                   select ct.Code).FirstOrDefault();
                        claim = qry.ToString();
                    }





                    tblYear yearInfo = unitOfWork.TblYearRepository.GetByID(claimRecording.Year);
                    claimName = yearInfo.Description;






                    //claimRecording.FileName = "";
                    unitOfWork.TblClaimRecordingRepository.Insert(claimRecording);
                    unitOfWork.Save();
                    int index = (int)claimRecording.ClaimRecordingID;


                    string codePrfix = "0000000";
                    int indexLength = index.ToString().Length;
                    codePrfix = codePrfix.Substring(0, (codePrfix.Length - indexLength) - 1);

                    if (index > 0)
                    {
                        tblClaimRecording claimRecordingUpdate = new tblClaimRecording();
                        claimRecordingUpdate = unitOfWork.TblClaimRecordingRepository.GetByID(index);

                        claimRecordingUpdate.FileName = claim + "/" + claimName + "/" + codePrfix + index.ToString();
                        unitOfWork.TblClaimRecordingRepository.Update(claimRecording);
                        unitOfWork.Save();
                    }






                    //Save Claim Recording History Details
                    if (claimRecordingVM.ClaimRecHistoryDetails != null)
                    {


                        tblClaimRecHistory claimRecHistory = new tblClaimRecHistory();
                        claimRecHistory.ClaimRecordingID = claimRecording.ClaimRecordingID;
                        claimRecHistory.Description = claimRecordingVM.ClaimRecHistoryDetails.Description;
                        claimRecHistory.RecordingDate = !string.IsNullOrEmpty(claimRecordingVM.ClaimRecHistoryDetails.RecordingDate) ? DateTime.ParseExact(claimRecordingVM.ClaimRecHistoryDetails.RecordingDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        claimRecHistory.Reason = claimRecordingVM.ClaimRecHistoryDetails.Reason;
                        claimRecHistory.CreatedBy = userID;
                        claimRecHistory.CreatedDate = DateTime.Now;
                        unitOfWork.TblClaimRecHistoryRepository.Insert(claimRecHistory);
                        unitOfWork.Save();
                    }
                    //Save Pending Document Details
                    if (claimRecordingVM.ClaimRecPendingDocDetails != null)
                    {
                        foreach (var pendingDoc in claimRecordingVM.ClaimRecPendingDocDetails)
                        {
                            tblClaimRecPendingDoc claimRecPendingDoc = new tblClaimRecPendingDoc();
                            claimRecPendingDoc.ClaimRecordingID = claimRecording.ClaimRecordingID;
                            claimRecPendingDoc.DocumentID = pendingDoc.DocumentID;
                            claimRecPendingDoc.CreatedBy = userID;
                            claimRecPendingDoc.CreatedDate = DateTime.Now;
                            unitOfWork.TblClaimRecPendingDocRepository.Insert(claimRecPendingDoc);
                            unitOfWork.Save();
                        }
                    }
                    //Complete the Transaction
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }



        public bool UpdateClaimRecording(ClaimRecordingVM claimRecordingVM, int userID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    //Update Claim Recording
                    //tblClaimRecording claimRecording = unitOfWork.TblClaimRecordingRepository.GetByID(claimRecordingVM.ClaimRecordingID);
                    //claimRecording.PolicyInfoRecID = claimRecordingVM.PolicyInfoRecID;
                    //claimRecording.ClaimNo = claimRecordingVM.ClaimNo;
                    //claimRecording.DateOfLoss = !string.IsNullOrEmpty(claimRecordingVM.DateOfLoss) ? DateTime.ParseExact(claimRecordingVM.DateOfLoss, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    //claimRecording.DateOfIntimation = !string.IsNullOrEmpty(claimRecordingVM.DateOfIntimation) ? DateTime.ParseExact(claimRecordingVM.DateOfIntimation, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    //claimRecording.CauseOfLoss = claimRecordingVM.CauseOfLoss;
                    //claimRecording.DamageDescription = claimRecordingVM.DamageDescription;
                    //claimRecording.AmountClaimed = claimRecordingVM.AmountClaimed;
                    //claimRecording.AmountPaid = claimRecordingVM.AmountPaid;
                    //claimRecording.FileReferenceNo = claimRecordingVM.FileReferenceNo;
                    //claimRecording.FilePath = claimRecordingVM.FilePath;
                    //claimRecording.PaymentPendingReason = claimRecordingVM.PaymentPendingReason;
                    //claimRecording.WithdrawReason = claimRecordingVM.WithdrawReason;
                    //claimRecording.IsOpened = claimRecordingVM.IsOpened;
                    //claimRecording.IsRejected = claimRecordingVM.IsRejected;
                    //claimRecording.IsWithdrawn = claimRecordingVM.IsWithdrawn;
                    //claimRecording.ClaimDocumentsEmailedDate = !string.IsNullOrEmpty(claimRecordingVM.ClaimDocumentsEmailedDate) ? DateTime.ParseExact(claimRecordingVM.ClaimDocumentsEmailedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    //claimRecording.ClaimDocumentsReceivedDate = !string.IsNullOrEmpty(claimRecordingVM.ClaimDocumentsReceivedDate) ? DateTime.ParseExact(claimRecordingVM.ClaimDocumentsReceivedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    //claimRecording.OriginalDocumentscourieredDate = !string.IsNullOrEmpty(claimRecordingVM.OriginalDocumentscourieredDate) ? DateTime.ParseExact(claimRecordingVM.OriginalDocumentscourieredDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    //claimRecording.PaymentAdviceEmailedDate = !string.IsNullOrEmpty(claimRecordingVM.PaymentAdviceEmailedDate) ? DateTime.ParseExact(claimRecordingVM.PaymentAdviceEmailedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    //claimRecording.PaymentAdviceReceviedDate = !string.IsNullOrEmpty(claimRecordingVM.PaymentAdviceReceviedDate) ? DateTime.ParseExact(claimRecordingVM.PaymentAdviceReceviedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    //claimRecording.DischargedDate = !string.IsNullOrEmpty(claimRecordingVM.DischargedDate) ? DateTime.ParseExact(claimRecordingVM.DischargedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    //claimRecording.Status = claimRecordingVM.Status;
                    //claimRecording.ModifiedBy = userID;
                    //claimRecording.ModifiedDate = DateTime.Now;
                    //claimRecording.Year = claimRecordingVM.YearID;
                    //claimRecording.FileName = claimRecordingVM.FileName;
                    //claimRecording.Year = claimRecordingVM.YearID;
                    //claimRecording.ClaimStatus = claimRecordingVM.ClaimStatus;
                    //claimRecording.InsClass = claimRecordingVM.InsClass;
                    //claimRecording.InsClassTypes = claimRecordingVM.InsClassTypeID;
                    //claimRecording.ClaimentInfo = claimRecordingVM.ClaimentInfo;
                    //claimRecording.Insurer = claimRecordingVM.Insurer;
                    //claimRecording.VehicleNumber = claimRecordingVM.VehicleNumber;
                    //claimRecording.PolicyExcess = claimRecordingVM.PolicyExcess;
                    //claimRecording.ChequeDetails = claimRecordingVM.ChequeDetails;
                    //claimRecording.RejectedReason = claimRecordingVM.RejectedReason;
                    //claimRecording.ContactPersonDetails = claimRecordingVM.ContactPersonDetails;
                    //claimRecording.Other = claimRecordingVM.Other;
                    //unitOfWork.TblClaimRecordingRepository.Update(claimRecording);
                    //unitOfWork.Save();

                    ////Update Claim Recording History Details
                    //tblClaimRecHistory claimRecHistory = unitOfWork.TblClaimRecHistoryRepository.GetByID(claimRecordingVM.ClaimRecHistoryDetails.ClaimRecHistoryID);
                    //claimRecHistory.ClaimRecordingID = claimRecording.ClaimRecordingID;
                    //claimRecHistory.Description = claimRecordingVM.ClaimRecHistoryDetails.Description;
                    //claimRecHistory.RecordingDate = !string.IsNullOrEmpty(claimRecordingVM.ClaimRecHistoryDetails.RecordingDate) ? DateTime.ParseExact(claimRecordingVM.ClaimRecHistoryDetails.RecordingDate, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    //claimRecHistory.Reason = claimRecordingVM.ClaimRecHistoryDetails.Reason;
                    //claimRecHistory.ModifiedBy = userID;
                    //claimRecHistory.ModifiedDate = DateTime.Now;
                    //unitOfWork.TblClaimRecHistoryRepository.Update(claimRecHistory);
                    //unitOfWork.Save();

                    string claim = "";
                    string claimName = "";
                    //Save Claim Recording
                    tblClaimRecording claimRecording = new tblClaimRecording();

                    claimRecording.PolicyInfoRecID = claimRecordingVM.PolicyInfoRecID < 0 ? 0 : claimRecordingVM.PolicyInfoRecID;
                    claimRecording.ClaimNo = !string.IsNullOrEmpty(claimRecordingVM.ClaimNo) ? "" : claimRecordingVM.ClaimNo;
                    claimRecording.DateOfLoss = !string.IsNullOrEmpty(claimRecordingVM.DateOfLoss) ? DateTime.ParseExact(claimRecordingVM.DateOfLoss, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.DateOfIntimation = !string.IsNullOrEmpty(claimRecordingVM.DateOfIntimation) ? DateTime.ParseExact(claimRecordingVM.DateOfIntimation, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.CauseOfLoss = !string.IsNullOrEmpty(claimRecordingVM.CauseOfLoss) ? "" : claimRecordingVM.CauseOfLoss;
                    claimRecording.DamageDescription = !string.IsNullOrEmpty(claimRecordingVM.DamageDescription) ? "" : claimRecordingVM.DamageDescription;
                    claimRecording.AmountClaimed = claimRecordingVM.AmountClaimed;
                    claimRecording.AmountPaid = claimRecordingVM.AmountPaid;
                    claimRecording.FileReferenceNo = claimRecordingVM.FileReferenceNo;
                    claimRecording.FilePath = claimRecordingVM.FilePath;
                    claimRecording.PaymentPendingReason = claimRecordingVM.PaymentPendingReason;
                    claimRecording.WithdrawReason = claimRecordingVM.WithdrawReason;
                    claimRecording.IsOpened = claimRecordingVM.IsOpened;
                    claimRecording.IsRejected = claimRecordingVM.IsRejected;
                    claimRecording.IsWithdrawn = claimRecordingVM.IsWithdrawn;
                    claimRecording.ClaimDocumentsEmailedDate = !string.IsNullOrEmpty(claimRecordingVM.ClaimDocumentsEmailedDate) ? DateTime.ParseExact(claimRecordingVM.ClaimDocumentsEmailedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.ClaimDocumentsReceivedDate = !string.IsNullOrEmpty(claimRecordingVM.ClaimDocumentsReceivedDate) ? DateTime.ParseExact(claimRecordingVM.ClaimDocumentsReceivedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.OriginalDocumentscourieredDate = !string.IsNullOrEmpty(claimRecordingVM.OriginalDocumentscourieredDate) ? DateTime.ParseExact(claimRecordingVM.OriginalDocumentscourieredDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.PaymentAdviceEmailedDate = !string.IsNullOrEmpty(claimRecordingVM.PaymentAdviceEmailedDate) ? DateTime.ParseExact(claimRecordingVM.PaymentAdviceEmailedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.PaymentAdviceReceviedDate = !string.IsNullOrEmpty(claimRecordingVM.PaymentAdviceReceviedDate) ? DateTime.ParseExact(claimRecordingVM.PaymentAdviceReceviedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.DischargedDate = !string.IsNullOrEmpty(claimRecordingVM.DischargedDate) ? DateTime.ParseExact(claimRecordingVM.DischargedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.Status = claimRecordingVM.Status;
                    claimRecording.CreatedBy = userID;
                    claimRecording.CreatedDate = DateTime.Now;
                    claimRecording.Year = claimRecordingVM.YearID;
                    claimRecording.ClaimStatus = claimRecordingVM.ClaimStatus;
                    claimRecording.InsClass = claimRecordingVM.InsClass;
                    claimRecording.InsClassTypes = claimRecordingVM.InsClassTypeID;
                    claimRecording.ClaimentInfo = claimRecordingVM.ClaimentInfo;
                    claimRecording.Insurer = claimRecordingVM.Insurer;
                    claimRecording.VehicleNumber = claimRecordingVM.VehicleNumber;
                    claimRecording.PolicyExcess = claimRecordingVM.PolicyExcess;
                    claimRecording.ChequeDetails = claimRecordingVM.ChequeNo;
                    claimRecording.RejectedReason = claimRecordingVM.RejectedReason;
                    claimRecording.ContactPersonDetails = claimRecordingVM.ContactPersonDetails;
                    claimRecording.Other = claimRecordingVM.Other;
                    claimRecording.CurrencyRate = claimRecordingVM.CurrencyRate;
                    claimRecording.ClaimPaidStatus = claimRecordingVM.ClaimPaidStatus;
                    claimRecording.DateOfOpen = !string.IsNullOrEmpty(claimRecordingVM.DateOfOpen) ? DateTime.ParseExact(claimRecordingVM.DateOfOpen, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.DateOfWithdraw = !string.IsNullOrEmpty(claimRecordingVM.DateOfWithdraw) ? DateTime.ParseExact(claimRecordingVM.DateOfWithdraw, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecording.DateOfReject = !string.IsNullOrEmpty(claimRecordingVM.DateOfReject) ? DateTime.ParseExact(claimRecordingVM.DateOfReject, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                    //using (var db = new PERFECTIBSEntities())
                    //{

                    //    var qry = (from ct in db.tblInsClasses
                    //               join comst in db.tblQuotationLines on ct.InsClassID equals comst.InsClassID
                    //               join comstt in db.tblQuotationHeaders on comst.QuotationHeaderID equals comstt.QuotationHeaderID
                    //               join ins in db.tblPolicyInformationRecordings on comstt.QuotationHeaderID equals ins.QuotationHeaderID
                    //               join inss in db.tblClaimRecordings on ins.PolicyInfoRecID equals inss.PolicyInfoRecID
                    //               where inss.PolicyInfoRecID == claimRecording.PolicyInfoRecID
                    //               select ct.Code).FirstOrDefault();
                    //    claim = qry.ToString();
                    //}

                    using (var db = new PERFECTIBSEntities())
                    {

                        var qry = (from ct in db.tblInsClasses
                                   join comst in db.tblQuotationLines on ct.InsClassID equals comst.InsClassID
                                   join comstt in db.tblQuotationHeaders on comst.QuotationHeaderID equals comstt.QuotationHeaderID
                                   join ins in db.tblPolicyInformationRecordings on comstt.QuotationHeaderID equals ins.QuotationHeaderID
                                   where ins.PolicyInfoRecID == claimRecording.PolicyInfoRecID
                                   select ct.Code).FirstOrDefault();
                        claim = qry.ToString();
                    }

                    tblYear yearInfo = unitOfWork.TblYearRepository.GetByID(claimRecordingVM.Year);
                    claimName = yearInfo.Description;

                    //claimRecording.FileName = "";
                    unitOfWork.TblClaimRecordingRepository.Update(claimRecording);
                    unitOfWork.Save();
                    int index = (int)claimRecording.ClaimRecordingID;


                    string codePrfix = "0000000";
                    int indexLength = index.ToString().Length;
                    codePrfix = codePrfix.Substring(0, (codePrfix.Length - indexLength) - 1);

                    if (index > 0)
                    {
                        tblClaimRecording claimRecordingUpdate = new tblClaimRecording();
                        claimRecordingUpdate = unitOfWork.TblClaimRecordingRepository.GetByID(index);

                        claimRecordingUpdate.FileName = claim + "/" + claimName + "/" + codePrfix + index.ToString();
                        unitOfWork.TblClaimRecordingRepository.Update(claimRecording);
                        unitOfWork.Save();
                    }


                    ////Update Claim Recording History Details
                    tblClaimRecHistory claimRecHistory = unitOfWork.TblClaimRecHistoryRepository.GetByID(claimRecordingVM.ClaimRecHistoryDetails.ClaimRecHistoryID);
                    claimRecHistory.ClaimRecordingID = claimRecording.ClaimRecordingID;
                    claimRecHistory.Description = claimRecordingVM.ClaimRecHistoryDetails.Description;
                    claimRecHistory.RecordingDate = !string.IsNullOrEmpty(claimRecordingVM.ClaimRecHistoryDetails.RecordingDate) ? DateTime.ParseExact(claimRecordingVM.ClaimRecHistoryDetails.RecordingDate, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    claimRecHistory.Reason = claimRecordingVM.ClaimRecHistoryDetails.Reason;
                    claimRecHistory.ModifiedBy = userID;
                    claimRecHistory.ModifiedDate = DateTime.Now;
                    unitOfWork.TblClaimRecHistoryRepository.Update(claimRecHistory);
                    unitOfWork.Save();







                    //Delete Existing Pending Document
                    List<tblClaimRecPendingDoc> existingPendingDocList = unitOfWork.TblClaimRecPendingDocRepository.Get(x => x.ClaimRecordingID == claimRecording.ClaimRecordingID).ToList();

                    foreach (var pendingDoc in existingPendingDocList)
                    {
                        unitOfWork.TblClaimRecPendingDocRepository.Delete(pendingDoc);
                        unitOfWork.Save();
                    }

                    //Save Pending Document Details
                    foreach (var pendingDoc in claimRecordingVM.ClaimRecPendingDocDetails)
                    {
                        tblClaimRecPendingDoc claimRecPendingDoc = new tblClaimRecPendingDoc();
                        claimRecPendingDoc.ClaimRecordingID = claimRecording.ClaimRecordingID;
                        claimRecPendingDoc.DocumentID = pendingDoc.DocumentID;
                        claimRecPendingDoc.CreatedBy = claimRecording.CreatedBy;
                        claimRecPendingDoc.CreatedDate = claimRecording.CreatedDate;
                        claimRecPendingDoc.ModifiedBy = userID;
                        claimRecPendingDoc.ModifiedDate = DateTime.Now;
                        unitOfWork.TblClaimRecPendingDocRepository.Update(claimRecPendingDoc);
                        unitOfWork.Save();
                    }

                    //Complete the Transaction
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }



        public List<ClaimRecordingVM> GetAllClaimRecordings()
        {
            try
            {
                var claimRecordingData = unitOfWork.TblClaimRecordingRepository.Get().ToList();

                List<ClaimRecordingVM> claimRecordingList = new List<ClaimRecordingVM>();

                foreach (var claimRecording in claimRecordingData)
                {
                    ClaimRecordingVM claimRecordingVM = new ClaimRecordingVM();
                    claimRecordingVM.ClaimRecordingID = claimRecording.ClaimRecordingID;
                    claimRecordingVM.PolicyInfoRecID = claimRecording.PolicyInfoRecID != null ? Convert.ToInt32(claimRecording.PolicyInfoRecID) : 0;
                    claimRecordingVM.ClaimNo = claimRecording.ClaimNo;
                    claimRecordingVM.DateOfLoss = claimRecording.DateOfLoss != null ? Convert.ToDateTime(claimRecording.DateOfLoss).ToString("MM/dd/yyyy") : string.Empty;
                    claimRecordingVM.DateOfIntimation = claimRecording.DateOfIntimation != null ? Convert.ToDateTime(claimRecording.DateOfIntimation).ToString("MM/dd/yyyy") : string.Empty;
                    claimRecordingVM.CauseOfLoss = claimRecording.CauseOfLoss;
                    claimRecordingVM.DamageDescription = claimRecording.DamageDescription;
                    claimRecordingVM.AmountClaimed = claimRecording.AmountClaimed != null ? Convert.ToDecimal(claimRecording.AmountClaimed) : 0;
                    claimRecordingVM.AmountPaid = claimRecording.AmountPaid != null ? Convert.ToDecimal(claimRecording.AmountPaid) : 0;
                    claimRecordingVM.FileReferenceNo = claimRecording.FileReferenceNo;
                    claimRecordingVM.FilePath = claimRecording.FilePath;
                    claimRecordingVM.PaymentPendingReason = claimRecording.PaymentPendingReason;
                    claimRecordingVM.WithdrawReason = claimRecording.WithdrawReason;
                    claimRecordingVM.IsOpened = claimRecording.IsOpened;
                    claimRecordingVM.IsRejected = claimRecording.IsRejected;
                    claimRecordingVM.IsWithdrawn = claimRecording.IsWithdrawn;
                    claimRecordingVM.ClaimDocumentsEmailedDate = claimRecording.ClaimDocumentsEmailedDate != null ? Convert.ToDateTime(claimRecording.ClaimDocumentsEmailedDate).ToString("MM/dd/yyyy") : string.Empty;
                    claimRecordingVM.ClaimDocumentsReceivedDate = claimRecording.ClaimDocumentsReceivedDate != null ? Convert.ToDateTime(claimRecording.ClaimDocumentsReceivedDate).ToString("MM/dd/yyyy") : string.Empty;
                    claimRecordingVM.OriginalDocumentscourieredDate = claimRecording.OriginalDocumentscourieredDate != null ? Convert.ToDateTime(claimRecording.OriginalDocumentscourieredDate).ToString("MM/dd/yyyy") : string.Empty;
                    claimRecordingVM.PaymentAdviceEmailedDate = claimRecording.PaymentAdviceEmailedDate != null ? Convert.ToDateTime(claimRecording.PaymentAdviceEmailedDate).ToString("MM/dd/yyyy") : string.Empty;
                    claimRecordingVM.PaymentAdviceReceviedDate = claimRecording.PaymentAdviceReceviedDate != null ? Convert.ToDateTime(claimRecording.PaymentAdviceReceviedDate).ToString("MM/dd/yyyy") : string.Empty;
                    claimRecordingVM.DischargedDate = claimRecording.DischargedDate != null ? Convert.ToDateTime(claimRecording.DischargedDate).ToString("MM/dd/yyyy") : string.Empty;
                    claimRecordingVM.Status = claimRecording.Status;
                    claimRecordingVM.CreatedBy = claimRecording.CreatedBy != null ? Convert.ToInt32(claimRecording.CreatedBy) : 0;
                    claimRecordingVM.CreatedDate = claimRecording.CreatedDate != null ? claimRecording.CreatedDate.ToString() : string.Empty;
                    claimRecordingVM.ModifiedBy = claimRecording.ModifiedBy != null ? Convert.ToInt32(claimRecording.ModifiedBy) : 0;
                    claimRecordingVM.ModifiedDate = claimRecording.ModifiedDate != null ? claimRecording.ModifiedDate.ToString() : string.Empty;

                    //Get Claim Recording History Details
                    tblClaimRecHistory claimRecHistoryData = unitOfWork.TblClaimRecHistoryRepository.Get(x => x.ClaimRecordingID == claimRecording.ClaimRecordingID).FirstOrDefault();

                    ClaimRecHistoryVM ClaimRecHistoryDetails = new ClaimRecHistoryVM();

                    if (claimRecHistoryData != null)
                    {
                        ClaimRecHistoryDetails.ClaimRecHistoryID = claimRecHistoryData.ClaimRecHistoryID;
                        ClaimRecHistoryDetails.Description = claimRecHistoryData.Description;
                        ClaimRecHistoryDetails.RecordingDate = claimRecHistoryData.RecordingDate != null ? claimRecHistoryData.RecordingDate.ToString() : string.Empty;
                        ClaimRecHistoryDetails.Reason = claimRecHistoryData.Reason;
                        ClaimRecHistoryDetails.CreatedBy = claimRecHistoryData.CreatedBy != null ? Convert.ToInt32(claimRecHistoryData.CreatedBy) : 0;
                        ClaimRecHistoryDetails.CreatedDate = claimRecHistoryData.CreatedDate != null ? claimRecHistoryData.CreatedDate.ToString() : string.Empty;
                        ClaimRecHistoryDetails.ModifiedBy = claimRecHistoryData.ModifiedBy != null ? Convert.ToInt32(claimRecHistoryData.ModifiedBy) : 0;
                        ClaimRecHistoryDetails.ModifiedDate = claimRecHistoryData.ModifiedDate != null ? claimRecHistoryData.ModifiedDate.ToString() : string.Empty;
                    }

                    //Get Pending Document Details
                    var pendingDocData = unitOfWork.TblClaimRecPendingDocRepository.Get(x => x.ClaimRecordingID == claimRecording.ClaimRecordingID).ToList();

                    List<ClaimRecPendingDocVM> claimRecPendingDocList = new List<ClaimRecPendingDocVM>();

                    foreach (var pendingDoc in pendingDocData)
                    {
                        ClaimRecPendingDocVM claimRecPendingDocVM = new ClaimRecPendingDocVM();
                        claimRecPendingDocVM.ClaimRecPendingDocID = pendingDoc.ClaimRecPendingDocID;
                        claimRecPendingDocVM.DocumentID = pendingDoc.DocumentID != null ? Convert.ToInt32(pendingDoc.DocumentID) : 0;
                        claimRecPendingDocVM.CreatedBy = pendingDoc.CreatedBy != null ? Convert.ToInt32(pendingDoc.CreatedBy) : 0;
                        claimRecPendingDocVM.CreatedDate = pendingDoc.CreatedDate != null ? pendingDoc.CreatedDate.ToString() : string.Empty;
                        claimRecPendingDocVM.ModifiedBy = pendingDoc.ModifiedBy != null ? Convert.ToInt32(pendingDoc.ModifiedBy) : 0;
                        claimRecPendingDocVM.ModifiedDate = pendingDoc.ModifiedDate != null ? pendingDoc.ModifiedDate.ToString() : string.Empty;

                        claimRecPendingDocList.Add(claimRecPendingDocVM);
                    }

                    claimRecordingVM.ClaimRecHistoryDetails = ClaimRecHistoryDetails;
                    claimRecordingVM.ClaimRecPendingDocDetails = claimRecPendingDocList;

                    claimRecordingList.Add(claimRecordingVM);
                }

                return claimRecordingList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ClaimRecordingVM> GetAllClaimRecordingsByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var claimRecordingData = unitOfWork.TblClaimRecordingRepository.Get(x => x.tblPolicyInformationRecording.tblQuotationHeader.tblClientRequestHeader.tblClient.BUID == businessUnitID).ToList();

                List<ClaimRecordingVM> claimRecordingList = new List<ClaimRecordingVM>();

                foreach (var claimRecording in claimRecordingData)
                {

                    ClaimRecordingVM claimRecordingVM = new ClaimRecordingVM();
                    claimRecordingVM.ClaimRecordingID = claimRecording.ClaimRecordingID;
                    claimRecordingVM.PolicyInfoRecID = claimRecording.PolicyInfoRecID != null ? Convert.ToInt32(claimRecording.PolicyInfoRecID) : 0;
                    claimRecordingVM.ClaimNo = claimRecording.ClaimNo;
                    claimRecordingVM.DateOfLoss = claimRecording.DateOfLoss != null ? Convert.ToDateTime(claimRecording.DateOfLoss).ToString("MM/dd/yyyy") : string.Empty;
                    claimRecordingVM.DateOfIntimation = claimRecording.DateOfIntimation != null ? Convert.ToDateTime(claimRecording.DateOfIntimation).ToString("MM/dd/yyyy") : string.Empty;
                    claimRecordingVM.CauseOfLoss = claimRecording.CauseOfLoss;
                    claimRecordingVM.DamageDescription = claimRecording.DamageDescription;
                    claimRecordingVM.AmountClaimed = claimRecording.AmountClaimed != null ? Convert.ToDecimal(claimRecording.AmountClaimed) : 0;
                    claimRecordingVM.AmountPaid = claimRecording.AmountPaid != null ? Convert.ToDecimal(claimRecording.AmountPaid) : 0;
                    claimRecordingVM.FileReferenceNo = claimRecording.FileReferenceNo;
                    claimRecordingVM.FilePath = claimRecording.FilePath;

                    claimRecordingVM.PaymentPendingReason = claimRecording.PaymentPendingReason;
                    claimRecordingVM.WithdrawReason = claimRecording.WithdrawReason;
                    claimRecordingVM.IsOpened = claimRecording.IsOpened;
                    claimRecordingVM.IsRejected = claimRecording.IsRejected;
                    claimRecordingVM.IsWithdrawn = claimRecording.IsWithdrawn;
                    claimRecordingVM.ClaimDocumentsEmailedDate = claimRecording.ClaimDocumentsEmailedDate != null ? Convert.ToDateTime(claimRecording.ClaimDocumentsEmailedDate).ToString("MM/dd/yyyy") : string.Empty;
                    claimRecordingVM.ClaimDocumentsReceivedDate = claimRecording.ClaimDocumentsReceivedDate != null ? Convert.ToDateTime(claimRecording.ClaimDocumentsReceivedDate).ToString("MM/dd/yyyy") : string.Empty;
                    claimRecordingVM.OriginalDocumentscourieredDate = claimRecording.OriginalDocumentscourieredDate != null ? Convert.ToDateTime(claimRecording.OriginalDocumentscourieredDate).ToString("MM/dd/yyyy") : string.Empty;
                    claimRecordingVM.PaymentAdviceEmailedDate = claimRecording.PaymentAdviceEmailedDate != null ? Convert.ToDateTime(claimRecording.PaymentAdviceEmailedDate).ToString("MM/dd/yyyy") : string.Empty;
                    claimRecordingVM.PaymentAdviceReceviedDate = claimRecording.PaymentAdviceReceviedDate != null ? Convert.ToDateTime(claimRecording.PaymentAdviceReceviedDate).ToString("MM/dd/yyyy") : string.Empty;
                    claimRecordingVM.DischargedDate = claimRecording.DischargedDate != null ? Convert.ToDateTime(claimRecording.DischargedDate).ToString("MM/dd/yyyy") : string.Empty;
                    claimRecordingVM.Status = claimRecording.Status;
                    claimRecordingVM.CreatedBy = claimRecording.CreatedBy != null ? Convert.ToInt32(claimRecording.CreatedBy) : 0;
                    claimRecordingVM.CreatedDate = claimRecording.CreatedDate != null ? claimRecording.CreatedDate.ToString() : string.Empty;
                    claimRecordingVM.ModifiedBy = claimRecording.ModifiedBy != null ? Convert.ToInt32(claimRecording.ModifiedBy) : 0;
                    claimRecordingVM.ModifiedDate = claimRecording.ModifiedDate != null ? claimRecording.ModifiedDate.ToString() : string.Empty;



                    claimRecordingVM.YearID = claimRecording.Year != null ? Convert.ToInt32(claimRecording.Year) : 0;
                    claimRecordingVM.ClaimStatus = claimRecording.ClaimStatus != null ? Convert.ToInt32(claimRecording.ClaimStatus) : 0;
                    claimRecordingVM.InsClass = claimRecording.InsClass != null ? Convert.ToInt32(claimRecording.InsClass) : 0;
                    claimRecordingVM.InsClassTypeID = claimRecording.InsClassTypes != null ? Convert.ToInt32(claimRecording.InsClassTypes) : 0;
                    claimRecordingVM.ClaimentInfo = claimRecording.ClaimentInfo != null ? claimRecording.ClaimentInfo : "";
                    claimRecordingVM.Insurer = claimRecording.Insurer != null ? claimRecording.Insurer : "";
                    claimRecordingVM.VehicleNumber = claimRecording.VehicleNumber != null ? claimRecording.VehicleNumber : "";
                    claimRecordingVM.PolicyExcess = claimRecording.PolicyExcess != null ? Convert.ToDecimal(claimRecording.PolicyExcess) : 0;
                    claimRecordingVM.ChequeNo = claimRecording.ChequeDetails != null ? claimRecording.ChequeDetails : "";
                    claimRecordingVM.RejectedReason = claimRecording.RejectedReason != null ? claimRecording.RejectedReason : "";
                    claimRecordingVM.ContactPersonDetails = claimRecording.ContactPersonDetails != null ? claimRecording.ContactPersonDetails : "";
                    claimRecordingVM.Other = claimRecording.Other != null ? claimRecording.Other : "";
                    claimRecordingVM.CurrencyRate = claimRecording.CurrencyRate != null ? Convert.ToInt32(claimRecording.CurrencyRate) : 0;
                    claimRecordingVM.CurrencyID = claimRecording.ExtraInt1 != null ? Convert.ToInt32(claimRecording.ExtraInt1) : 0;
                    claimRecordingVM.ClaimPaidStatus = claimRecording.ClaimPaidStatus != null ? Convert.ToInt32(claimRecording.ClaimPaidStatus) : 0;
                    claimRecordingVM.DateOfOpen = claimRecording.DateOfOpen != null ? Convert.ToDateTime(claimRecording.DateOfOpen).ToString("MM/dd/yyyy") : string.Empty;
                    claimRecordingVM.DateOfWithdraw = claimRecording.DateOfWithdraw != null ? Convert.ToDateTime(claimRecording.DateOfWithdraw).ToString("MM/dd/yyyy") : string.Empty;
                    claimRecordingVM.DateOfReject = claimRecording.DateOfReject != null ? Convert.ToDateTime(claimRecording.DateOfReject).ToString("MM/dd/yyyy") : string.Empty;
                    claimRecordingVM.FileName = claimRecording.FileName != null ? claimRecording.FileName : "";
                    //Get Claim Recording History Details
                    tblClaimRecHistory claimRecHistoryData = unitOfWork.TblClaimRecHistoryRepository.Get(x => x.ClaimRecordingID == claimRecording.ClaimRecordingID).FirstOrDefault();

                    ClaimRecHistoryVM ClaimRecHistoryDetails = new ClaimRecHistoryVM();

                    if (claimRecHistoryData != null)
                    {
                        ClaimRecHistoryDetails.ClaimRecHistoryID = claimRecHistoryData.ClaimRecHistoryID;
                        ClaimRecHistoryDetails.Description = claimRecHistoryData.Description;
                        ClaimRecHistoryDetails.RecordingDate = claimRecHistoryData.RecordingDate != null ? claimRecHistoryData.RecordingDate.ToString() : string.Empty;
                        ClaimRecHistoryDetails.Reason = claimRecHistoryData.Reason;
                        ClaimRecHistoryDetails.CreatedBy = claimRecHistoryData.CreatedBy != null ? Convert.ToInt32(claimRecHistoryData.CreatedBy) : 0;
                        ClaimRecHistoryDetails.CreatedDate = claimRecHistoryData.CreatedDate != null ? claimRecHistoryData.CreatedDate.ToString() : string.Empty;
                        ClaimRecHistoryDetails.ModifiedBy = claimRecHistoryData.ModifiedBy != null ? Convert.ToInt32(claimRecHistoryData.ModifiedBy) : 0;
                        ClaimRecHistoryDetails.ModifiedDate = claimRecHistoryData.ModifiedDate != null ? claimRecHistoryData.ModifiedDate.ToString() : string.Empty;
                    }

                    //Get Pending Document Details
                    var pendingDocData = unitOfWork.TblClaimRecPendingDocRepository.Get(x => x.ClaimRecordingID == claimRecording.ClaimRecordingID).ToList();

                    List<ClaimRecPendingDocVM> claimRecPendingDocList = new List<ClaimRecPendingDocVM>();

                    foreach (var pendingDoc in pendingDocData)
                    {
                        ClaimRecPendingDocVM claimRecPendingDocVM = new ClaimRecPendingDocVM();
                        claimRecPendingDocVM.ClaimRecPendingDocID = pendingDoc.ClaimRecPendingDocID;
                        claimRecPendingDocVM.DocumentID = pendingDoc.DocumentID != null ? Convert.ToInt32(pendingDoc.DocumentID) : 0;
                        claimRecPendingDocVM.CreatedBy = pendingDoc.CreatedBy != null ? Convert.ToInt32(pendingDoc.CreatedBy) : 0;
                        claimRecPendingDocVM.CreatedDate = pendingDoc.CreatedDate != null ? pendingDoc.CreatedDate.ToString() : string.Empty;
                        claimRecPendingDocVM.ModifiedBy = pendingDoc.ModifiedBy != null ? Convert.ToInt32(pendingDoc.ModifiedBy) : 0;
                        claimRecPendingDocVM.ModifiedDate = pendingDoc.ModifiedDate != null ? pendingDoc.ModifiedDate.ToString() : string.Empty;

                        claimRecPendingDocList.Add(claimRecPendingDocVM);
                    }

                    claimRecordingVM.ClaimRecHistoryDetails = ClaimRecHistoryDetails;
                    claimRecordingVM.ClaimRecPendingDocDetails = claimRecPendingDocList;

                    claimRecordingList.Add(claimRecordingVM);
                }

                return claimRecordingList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        public ClaimRecordingVM GetClaimRecordingByID(int claimRecordingID)
        {
            try
            {
                var claimRecordingData = unitOfWork.TblClaimRecordingRepository.GetByID(claimRecordingID);

                ClaimRecordingVM claimRecordingVM = new ClaimRecordingVM();
                claimRecordingVM.ClaimRecordingID = claimRecordingData.ClaimRecordingID;
                claimRecordingVM.PolicyInfoRecID = claimRecordingData.PolicyInfoRecID != null ? Convert.ToInt32(claimRecordingData.PolicyInfoRecID) : 0;
                claimRecordingVM.ClaimNo = claimRecordingData.ClaimNo;
                claimRecordingVM.DateOfLoss = claimRecordingData.DateOfLoss != null ? Convert.ToDateTime(claimRecordingData.DateOfLoss).ToString("MM/dd/yyyy") : string.Empty;
                claimRecordingVM.DateOfIntimation = claimRecordingData.DateOfIntimation != null ? Convert.ToDateTime(claimRecordingData.DateOfIntimation).ToString("MM/dd/yyyy") : string.Empty;
                claimRecordingVM.CauseOfLoss = claimRecordingData.CauseOfLoss;
                claimRecordingVM.DamageDescription = claimRecordingData.DamageDescription;
                claimRecordingVM.AmountClaimed = claimRecordingData.AmountClaimed != null ? Convert.ToDecimal(claimRecordingData.AmountClaimed) : 0;
                claimRecordingVM.AmountPaid = claimRecordingData.AmountPaid != null ? Convert.ToDecimal(claimRecordingData.AmountPaid) : 0;
                claimRecordingVM.FileReferenceNo = claimRecordingData.FileReferenceNo;
                claimRecordingVM.FilePath = claimRecordingData.FilePath;
                claimRecordingVM.PaymentPendingReason = claimRecordingData.PaymentPendingReason;
                claimRecordingVM.WithdrawReason = claimRecordingData.WithdrawReason;
                claimRecordingVM.IsOpened = claimRecordingData.IsOpened;
                claimRecordingVM.IsRejected = claimRecordingData.IsRejected;
                claimRecordingVM.IsWithdrawn = claimRecordingData.IsWithdrawn;
                claimRecordingVM.ClaimDocumentsEmailedDate = claimRecordingData.ClaimDocumentsEmailedDate != null ? Convert.ToDateTime(claimRecordingData.ClaimDocumentsEmailedDate).ToString("MM/dd/yyyy") : string.Empty;
                claimRecordingVM.ClaimDocumentsReceivedDate = claimRecordingData.ClaimDocumentsReceivedDate != null ? Convert.ToDateTime(claimRecordingData.ClaimDocumentsReceivedDate).ToString("MM/dd/yyyy") : string.Empty;
                claimRecordingVM.OriginalDocumentscourieredDate = claimRecordingData.OriginalDocumentscourieredDate != null ? Convert.ToDateTime(claimRecordingData.OriginalDocumentscourieredDate).ToString("MM/dd/yyyy") : string.Empty;
                claimRecordingVM.PaymentAdviceEmailedDate = claimRecordingData.PaymentAdviceEmailedDate != null ? Convert.ToDateTime(claimRecordingData.PaymentAdviceEmailedDate).ToString("MM/dd/yyyy") : string.Empty;
                claimRecordingVM.PaymentAdviceReceviedDate = claimRecordingData.PaymentAdviceReceviedDate != null ? Convert.ToDateTime(claimRecordingData.PaymentAdviceReceviedDate).ToString("MM/dd/yyyy") : string.Empty;
                claimRecordingVM.DischargedDate = claimRecordingData.DischargedDate != null ? Convert.ToDateTime(claimRecordingData.DischargedDate).ToString("MM/dd/yyyy") : string.Empty;
                claimRecordingVM.Status = claimRecordingData.Status;
                claimRecordingVM.CreatedBy = claimRecordingData.CreatedBy != null ? Convert.ToInt32(claimRecordingData.CreatedBy) : 0;
                claimRecordingVM.CreatedDate = claimRecordingData.CreatedDate != null ? claimRecordingData.CreatedDate.ToString() : string.Empty;
                claimRecordingVM.ModifiedBy = claimRecordingData.ModifiedBy != null ? Convert.ToInt32(claimRecordingData.ModifiedBy) : 0;
                claimRecordingVM.ModifiedDate = claimRecordingData.ModifiedDate != null ? claimRecordingData.ModifiedDate.ToString() : string.Empty;

                //Get Claim Recording History Details
                tblClaimRecHistory claimRecHistoryData = unitOfWork.TblClaimRecHistoryRepository.Get(x => x.ClaimRecordingID == claimRecordingData.ClaimRecordingID).FirstOrDefault();

                ClaimRecHistoryVM ClaimRecHistoryDetails = new ClaimRecHistoryVM();

                if (claimRecHistoryData != null)
                {
                    ClaimRecHistoryDetails.ClaimRecHistoryID = claimRecHistoryData.ClaimRecHistoryID;
                    ClaimRecHistoryDetails.Description = claimRecHistoryData.Description;
                    ClaimRecHistoryDetails.RecordingDate = claimRecHistoryData.RecordingDate != null ? Convert.ToDateTime(claimRecHistoryData.RecordingDate).ToString("MM/dd/yyyy") : string.Empty;
                    ClaimRecHistoryDetails.Reason = claimRecHistoryData.Reason;
                    ClaimRecHistoryDetails.CreatedBy = claimRecHistoryData.CreatedBy != null ? Convert.ToInt32(claimRecHistoryData.CreatedBy) : 0;
                    ClaimRecHistoryDetails.CreatedDate = claimRecHistoryData.CreatedDate != null ? claimRecHistoryData.CreatedDate.ToString() : string.Empty;
                    ClaimRecHistoryDetails.ModifiedBy = claimRecHistoryData.ModifiedBy != null ? Convert.ToInt32(claimRecHistoryData.ModifiedBy) : 0;
                    ClaimRecHistoryDetails.ModifiedDate = claimRecHistoryData.ModifiedDate != null ? claimRecHistoryData.ModifiedDate.ToString() : string.Empty;
                }

                //Get Pending Document Details
                var pendingDocData = unitOfWork.TblClaimRecPendingDocRepository.Get(x => x.ClaimRecordingID == claimRecordingData.ClaimRecordingID).ToList();

                List<ClaimRecPendingDocVM> claimRecPendingDocList = new List<ClaimRecPendingDocVM>();

                foreach (var pendingDoc in pendingDocData)
                {
                    ClaimRecPendingDocVM claimRecPendingDocVM = new ClaimRecPendingDocVM();
                    claimRecPendingDocVM.ClaimRecPendingDocID = pendingDoc.ClaimRecPendingDocID;
                    claimRecPendingDocVM.DocumentID = pendingDoc.DocumentID != null ? Convert.ToInt32(pendingDoc.DocumentID) : 0;
                    claimRecPendingDocVM.CreatedBy = pendingDoc.CreatedBy != null ? Convert.ToInt32(pendingDoc.CreatedBy) : 0;
                    claimRecPendingDocVM.CreatedDate = pendingDoc.CreatedDate != null ? pendingDoc.CreatedDate.ToString() : string.Empty;
                    claimRecPendingDocVM.ModifiedBy = pendingDoc.ModifiedBy != null ? Convert.ToInt32(pendingDoc.ModifiedBy) : 0;
                    claimRecPendingDocVM.ModifiedDate = pendingDoc.ModifiedDate != null ? pendingDoc.ModifiedDate.ToString() : string.Empty;

                    claimRecPendingDocList.Add(claimRecPendingDocVM);
                }

                claimRecordingVM.ClaimRecHistoryDetails = ClaimRecHistoryDetails;
                claimRecordingVM.ClaimRecPendingDocDetails = claimRecPendingDocList;

                return claimRecordingVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Claim Payment
        public bool SaveClaimPayment(ClaimPaymentVM claimPaymentVM, int userID, out string errorMessage)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    decimal paidAmount = claimPaymentVM.ClaimPaymentMethodDetails.Sum(x => x.PaidAmount);

                    if (paidAmount <= claimPaymentVM.ClaimAmount)
                    {
                        bool isCompleted = false;

                        if (claimPaymentVM.ClaimPaymentMethodDetails.Any(x => x.IsFinal == true) && paidAmount == claimPaymentVM.ClaimAmount)
                        {
                            isCompleted = true;
                        }

                        //Save Claim Payment Details
                        tblClaimPayment claimPayment = new tblClaimPayment();
                        claimPayment.ClaimRecordingID = claimPaymentVM.ClaimRecordingID;
                        claimPayment.ClaimAmount = claimPaymentVM.ClaimAmount;
                        claimPayment.PaidAmount = paidAmount;
                        claimPayment.IsCompleted = isCompleted;
                        claimPayment.Notes = claimPaymentVM.Notes;
                        claimPayment.CreatedBy = userID;
                        claimPayment.CreatedDate = DateTime.Now;
                        unitOfWork.TblClaimPaymentRepository.Insert(claimPayment);
                        unitOfWork.Save();

                        //Save Claim Payment Method Details
                        foreach (var claimPaymentMethodVM in claimPaymentVM.ClaimPaymentMethodDetails)
                        {
                            tblClaimPaymentMethod claimPaymentMethod = new tblClaimPaymentMethod();
                            claimPaymentMethod.ClaimPaymentID = claimPayment.ClaimPaymentID;
                            claimPaymentMethod.ChequeNo = claimPaymentMethodVM.ChequeNo;
                            claimPaymentMethod.PaymentTypeID = claimPaymentMethodVM.PaymentTypeID;
                            claimPaymentMethod.PaidAmount = claimPaymentMethodVM.PaidAmount;
                            claimPaymentMethod.PaidDate = !string.IsNullOrEmpty(claimPaymentMethodVM.PaidDate) ? DateTime.ParseExact(claimPaymentMethodVM.PaidDate, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            claimPaymentMethod.IsFinal = claimPaymentMethodVM.IsFinal;
                            claimPaymentMethod.CreatedBy = userID;
                            claimPaymentMethod.CreatedDate = DateTime.Now;
                            unitOfWork.TblClaimPaymentMethodRepository.Insert(claimPaymentMethod);
                            unitOfWork.Save();
                        }

                        //Complete the Transaction
                        dbTransaction.Commit();

                        errorMessage = "No Error";
                        return true;
                    }
                    else
                    {
                        errorMessage = "Total paid amount should not exceed the claim amount";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();

                    errorMessage = "Save Failed";
                    return false;
                }
            }
        }

        public bool UpdateClaimPayment(ClaimPaymentVM claimPaymentVM, int userID, out string errorMessage)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    decimal paidAmount = claimPaymentVM.ClaimPaymentMethodDetails.Sum(x => x.PaidAmount);

                    if (paidAmount <= claimPaymentVM.ClaimAmount)
                    {
                        bool isCompleted = false;

                        if (claimPaymentVM.ClaimPaymentMethodDetails.Any(x => x.IsFinal == true) && paidAmount == claimPaymentVM.ClaimAmount)
                        {
                            isCompleted = true;
                        }

                        //Update Claim Payment Details
                        tblClaimPayment claimPayment = unitOfWork.TblClaimPaymentRepository.GetByID(claimPaymentVM.ClaimPaymentID);
                        claimPayment.ClaimRecordingID = claimPaymentVM.ClaimRecordingID;
                        claimPayment.ClaimAmount = claimPaymentVM.ClaimAmount;
                        claimPayment.PaidAmount = paidAmount;
                        claimPayment.IsCompleted = isCompleted;
                        claimPayment.Notes = claimPaymentVM.Notes;
                        claimPayment.ModifiedBy = userID;
                        claimPayment.ModifiedDate = DateTime.Now;
                        unitOfWork.TblClaimPaymentRepository.Update(claimPayment);
                        unitOfWork.Save();

                        //Delete Existing Claim Payment Method Details
                        List<tblClaimPaymentMethod> existingClaimPaymentMethodList = unitOfWork.TblClaimPaymentMethodRepository.Get(x => x.ClaimPaymentID == claimPaymentVM.ClaimPaymentID).ToList();

                        foreach (var claimPaymentMethod in existingClaimPaymentMethodList)
                        {
                            unitOfWork.TblClaimPaymentMethodRepository.Delete(claimPaymentMethod);
                            unitOfWork.Save();
                        }

                        //Save Claim Payment Method Details
                        foreach (var claimPaymentMethodVM in claimPaymentVM.ClaimPaymentMethodDetails)
                        {
                            tblClaimPaymentMethod claimPaymentMethod = new tblClaimPaymentMethod();
                            claimPaymentMethod.ClaimPaymentID = claimPayment.ClaimPaymentID;
                            claimPaymentMethod.ChequeNo = claimPaymentMethodVM.ChequeNo;
                            claimPaymentMethod.PaymentTypeID = claimPaymentMethodVM.PaymentTypeID;
                            claimPaymentMethod.PaidAmount = claimPaymentMethodVM.PaidAmount;
                            claimPaymentMethod.PaidDate = !string.IsNullOrEmpty(claimPaymentMethodVM.PaidDate) ? DateTime.ParseExact(claimPaymentMethodVM.PaidDate, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            claimPaymentMethod.IsFinal = claimPaymentMethodVM.IsFinal;
                            claimPaymentMethod.CreatedBy = claimPayment.CreatedBy;
                            claimPaymentMethod.CreatedDate = claimPayment.CreatedDate;
                            claimPaymentMethod.ModifiedBy = userID;
                            claimPaymentMethod.ModifiedDate = DateTime.Now;
                            unitOfWork.TblClaimPaymentMethodRepository.Insert(claimPaymentMethod);
                            unitOfWork.Save();
                        }

                        //Complete the Transaction
                        dbTransaction.Commit();

                        errorMessage = "No Error";
                        return true;
                    }
                    else
                    {
                        errorMessage = "Total paid amount should not exceed the claim amount";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();

                    errorMessage = "Update Failed";
                    return false;
                }
            }
        }

        public List<ClaimPaymentVM> GetAllClaimPayments()
        {
            try
            {
                var claimPaymentData = unitOfWork.TblClaimPaymentRepository.Get().ToList();

                List<ClaimPaymentVM> claimPaymentList = new List<ClaimPaymentVM>();

                foreach (var claimPayment in claimPaymentData)
                {
                    ClaimPaymentVM claimPaymentVM = new ClaimPaymentVM();
                    claimPaymentVM.ClaimPaymentID = claimPayment.ClaimPaymentID;
                    claimPaymentVM.ClaimRecordingID = claimPayment.ClaimRecordingID != null ? Convert.ToInt32(claimPayment.ClaimRecordingID) : 0;
                    claimPaymentVM.ClaimAmount = claimPayment.ClaimAmount != null ? Convert.ToDecimal(claimPayment.ClaimAmount) : 0;
                    claimPaymentVM.PaidAmount = claimPayment.PaidAmount != null ? Convert.ToDecimal(claimPayment.PaidAmount) : 0;
                    claimPaymentVM.IsCompleted = claimPayment.IsCompleted;
                    claimPaymentVM.Notes = claimPayment.Notes;
                    claimPaymentVM.CreatedBy = claimPayment.CreatedBy != null ? Convert.ToInt32(claimPayment.CreatedBy) : 0;
                    claimPaymentVM.CreatedDate = claimPayment.CreatedDate != null ? claimPayment.CreatedDate.ToString() : string.Empty;
                    claimPaymentVM.ModifiedBy = claimPayment.ModifiedBy != null ? Convert.ToInt32(claimPayment.ModifiedBy) : 0;
                    claimPaymentVM.ModifiedDate = claimPayment.ModifiedDate != null ? claimPayment.ModifiedDate.ToString() : string.Empty;

                    var claimPaymentMethodData = unitOfWork.TblClaimPaymentMethodRepository.Get(x => x.ClaimPaymentID == claimPayment.ClaimPaymentID).ToList();

                    List<ClaimPaymentMethodVM> ClaimPaymentMethodList = new List<ClaimPaymentMethodVM>();

                    foreach (var claimPaymentMethod in claimPaymentMethodData)
                    {
                        ClaimPaymentMethodVM claimPaymentMethodVM = new ClaimPaymentMethodVM();
                        claimPaymentMethodVM.ClaimPaymentMethodID = claimPaymentMethod.ClaimPaymentMethodID;
                        claimPaymentMethodVM.ChequeNo = claimPaymentMethod.ChequeNo;
                        claimPaymentMethodVM.PaymentTypeID = claimPaymentMethod.PaymentTypeID != null ? Convert.ToInt32(claimPaymentMethod.PaymentTypeID) : 0;

                        if (claimPaymentMethodVM.PaymentTypeID > 0)
                        {
                            claimPaymentMethodVM.PaymentTypeName = claimPaymentMethod.tblPaymentType.PaymentType;
                        }

                        claimPaymentMethodVM.PaidAmount = claimPaymentMethod.PaidAmount != null ? Convert.ToDecimal(claimPaymentMethod.PaidAmount) : 0;
                        claimPaymentMethodVM.PaidDate = claimPaymentMethod.PaidDate != null ? claimPaymentMethod.PaidDate.ToString() : string.Empty;
                        claimPaymentMethodVM.IsFinal = claimPaymentMethod.IsFinal;
                        claimPaymentMethodVM.CreatedBy = claimPaymentMethod.CreatedBy != null ? Convert.ToInt32(claimPaymentMethod.CreatedBy) : 0;
                        claimPaymentMethodVM.CreatedDate = claimPaymentMethod.CreatedDate != null ? claimPaymentMethod.CreatedDate.ToString() : string.Empty;
                        claimPaymentMethodVM.ModifiedBy = claimPaymentMethod.ModifiedBy != null ? Convert.ToInt32(claimPaymentMethod.ModifiedBy) : 0;
                        claimPaymentMethodVM.ModifiedDate = claimPaymentMethod.ModifiedDate != null ? claimPaymentMethod.ModifiedDate.ToString() : string.Empty;

                        ClaimPaymentMethodList.Add(claimPaymentMethodVM);
                    }

                    claimPaymentVM.ClaimPaymentMethodDetails = ClaimPaymentMethodList;

                    claimPaymentList.Add(claimPaymentVM);
                }

                return claimPaymentList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ClaimPaymentVM> GetAllClaimPaymentsByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var claimPaymentData = unitOfWork.TblClaimPaymentRepository.Get(x => x.tblClaimRecording.tblPolicyInformationRecording.tblQuotationHeader.tblClientRequestHeader.tblClient.BUID == businessUnitID).ToList();

                List<ClaimPaymentVM> claimPaymentList = new List<ClaimPaymentVM>();

                foreach (var claimPayment in claimPaymentData)
                {
                    ClaimPaymentVM claimPaymentVM = new ClaimPaymentVM();
                    claimPaymentVM.ClaimPaymentID = claimPayment.ClaimPaymentID;
                    claimPaymentVM.ClaimRecordingID = claimPayment.ClaimRecordingID != null ? Convert.ToInt32(claimPayment.ClaimRecordingID) : 0;
                    claimPaymentVM.ClaimAmount = claimPayment.ClaimAmount != null ? Convert.ToDecimal(claimPayment.ClaimAmount) : 0;
                    claimPaymentVM.PaidAmount = claimPayment.PaidAmount != null ? Convert.ToDecimal(claimPayment.PaidAmount) : 0;
                    claimPaymentVM.IsCompleted = claimPayment.IsCompleted;
                    claimPaymentVM.Notes = claimPayment.Notes;
                    claimPaymentVM.CreatedBy = claimPayment.CreatedBy != null ? Convert.ToInt32(claimPayment.CreatedBy) : 0;
                    claimPaymentVM.CreatedDate = claimPayment.CreatedDate != null ? claimPayment.CreatedDate.ToString() : string.Empty;
                    claimPaymentVM.ModifiedBy = claimPayment.ModifiedBy != null ? Convert.ToInt32(claimPayment.ModifiedBy) : 0;
                    claimPaymentVM.ModifiedDate = claimPayment.ModifiedDate != null ? claimPayment.ModifiedDate.ToString() : string.Empty;

                    var claimPaymentMethodData = unitOfWork.TblClaimPaymentMethodRepository.Get(x => x.ClaimPaymentID == claimPayment.ClaimPaymentID).ToList();

                    List<ClaimPaymentMethodVM> ClaimPaymentMethodList = new List<ClaimPaymentMethodVM>();

                    foreach (var claimPaymentMethod in claimPaymentMethodData)
                    {
                        ClaimPaymentMethodVM claimPaymentMethodVM = new ClaimPaymentMethodVM();
                        claimPaymentMethodVM.ClaimPaymentMethodID = claimPaymentMethod.ClaimPaymentMethodID;
                        claimPaymentMethodVM.ChequeNo = claimPaymentMethod.ChequeNo;
                        claimPaymentMethodVM.PaymentTypeID = claimPaymentMethod.PaymentTypeID != null ? Convert.ToInt32(claimPaymentMethod.PaymentTypeID) : 0;

                        if (claimPaymentMethodVM.PaymentTypeID > 0)
                        {
                            claimPaymentMethodVM.PaymentTypeName = claimPaymentMethod.tblPaymentType.PaymentType;
                        }

                        claimPaymentMethodVM.PaidAmount = claimPaymentMethod.PaidAmount != null ? Convert.ToDecimal(claimPaymentMethod.PaidAmount) : 0;
                        claimPaymentMethodVM.PaidDate = claimPaymentMethod.PaidDate != null ? claimPaymentMethod.PaidDate.ToString() : string.Empty;
                        claimPaymentMethodVM.IsFinal = claimPaymentMethod.IsFinal;
                        claimPaymentMethodVM.CreatedBy = claimPaymentMethod.CreatedBy != null ? Convert.ToInt32(claimPaymentMethod.CreatedBy) : 0;
                        claimPaymentMethodVM.CreatedDate = claimPaymentMethod.CreatedDate != null ? claimPaymentMethod.CreatedDate.ToString() : string.Empty;
                        claimPaymentMethodVM.ModifiedBy = claimPaymentMethod.ModifiedBy != null ? Convert.ToInt32(claimPaymentMethod.ModifiedBy) : 0;
                        claimPaymentMethodVM.ModifiedDate = claimPaymentMethod.ModifiedDate != null ? claimPaymentMethod.ModifiedDate.ToString() : string.Empty;

                        ClaimPaymentMethodList.Add(claimPaymentMethodVM);
                    }

                    claimPaymentVM.ClaimPaymentMethodDetails = ClaimPaymentMethodList;

                    claimPaymentList.Add(claimPaymentVM);
                }

                return claimPaymentList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ClaimPaymentVM GetClaimPaymentByID(int claimPaymentID)
        {
            try
            {
                var claimPaymentData = unitOfWork.TblClaimPaymentRepository.GetByID(claimPaymentID);

                ClaimPaymentVM claimPaymentVM = new ClaimPaymentVM();
                claimPaymentVM.ClaimPaymentID = claimPaymentData.ClaimPaymentID;
                claimPaymentVM.ClaimRecordingID = claimPaymentData.ClaimRecordingID != null ? Convert.ToInt32(claimPaymentData.ClaimRecordingID) : 0;
                claimPaymentVM.ClaimAmount = claimPaymentData.ClaimAmount != null ? Convert.ToDecimal(claimPaymentData.ClaimAmount) : 0;
                claimPaymentVM.PaidAmount = claimPaymentData.PaidAmount != null ? Convert.ToDecimal(claimPaymentData.PaidAmount) : 0;
                claimPaymentVM.IsCompleted = claimPaymentData.IsCompleted;
                claimPaymentVM.Notes = claimPaymentData.Notes;
                claimPaymentVM.CreatedBy = claimPaymentData.CreatedBy != null ? Convert.ToInt32(claimPaymentData.CreatedBy) : 0;
                claimPaymentVM.CreatedDate = claimPaymentData.CreatedDate != null ? claimPaymentData.CreatedDate.ToString() : string.Empty;
                claimPaymentVM.ModifiedBy = claimPaymentData.ModifiedBy != null ? Convert.ToInt32(claimPaymentData.ModifiedBy) : 0;
                claimPaymentVM.ModifiedDate = claimPaymentData.ModifiedDate != null ? claimPaymentData.ModifiedDate.ToString() : string.Empty;

                var claimPaymentMethodData = unitOfWork.TblClaimPaymentMethodRepository.Get(x => x.ClaimPaymentID == claimPaymentData.ClaimPaymentID).ToList();

                List<ClaimPaymentMethodVM> ClaimPaymentMethodList = new List<ClaimPaymentMethodVM>();

                foreach (var claimPaymentMethod in claimPaymentMethodData)
                {
                    ClaimPaymentMethodVM claimPaymentMethodVM = new ClaimPaymentMethodVM();
                    claimPaymentMethodVM.ClaimPaymentMethodID = claimPaymentMethod.ClaimPaymentMethodID;
                    claimPaymentMethodVM.ChequeNo = claimPaymentMethod.ChequeNo;
                    claimPaymentMethodVM.PaymentTypeID = claimPaymentMethod.PaymentTypeID != null ? Convert.ToInt32(claimPaymentMethod.PaymentTypeID) : 0;

                    if (claimPaymentMethodVM.PaymentTypeID > 0)
                    {
                        claimPaymentMethodVM.PaymentTypeName = claimPaymentMethod.tblPaymentType.PaymentType;
                    }

                    claimPaymentMethodVM.PaidAmount = claimPaymentMethod.PaidAmount != null ? Convert.ToDecimal(claimPaymentMethod.PaidAmount) : 0;
                    claimPaymentMethodVM.PaidDate = claimPaymentMethod.PaidDate != null ? claimPaymentMethod.PaidDate.ToString() : string.Empty;
                    claimPaymentMethodVM.IsFinal = claimPaymentMethod.IsFinal;
                    claimPaymentMethodVM.CreatedBy = claimPaymentMethod.CreatedBy != null ? Convert.ToInt32(claimPaymentMethod.CreatedBy) : 0;
                    claimPaymentMethodVM.CreatedDate = claimPaymentMethod.CreatedDate != null ? claimPaymentMethod.CreatedDate.ToString() : string.Empty;
                    claimPaymentMethodVM.ModifiedBy = claimPaymentMethod.ModifiedBy != null ? Convert.ToInt32(claimPaymentMethod.ModifiedBy) : 0;
                    claimPaymentMethodVM.ModifiedDate = claimPaymentMethod.ModifiedDate != null ? claimPaymentMethod.ModifiedDate.ToString() : string.Empty;

                    ClaimPaymentMethodList.Add(claimPaymentMethodVM);
                }

                claimPaymentVM.ClaimPaymentMethodDetails = ClaimPaymentMethodList;

                return claimPaymentVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion


        #region Other Claim Details
        public List<YearVM> GetYear()
        {
            try
            {
                var YearData = unitOfWork.TblYearRepository.Get().ToList();

                List<YearVM> YearDataList = new List<YearVM>();

                foreach (var year in YearData)
                {
                    YearVM yearVM = new YearVM();
                    yearVM.YearID = year.YearID;
                    yearVM.Description = year.Description;
                    //loadingTypeVM.CreatedBy = premium.CreatedBy != null ? Convert.ToInt32(premium.CreatedBy) : 0;
                    //loadingTypeVM.CreatedDate = premium.CreatedDate != null ? premium.CreatedDate.ToString() : string.Empty;
                    //loadingTypeVM.ModifiedBy = premium.ModifiedBy != null ? Convert.ToInt32(premium.ModifiedBy) : 0;
                    //loadingTypeVM.ModifiedDate = premium.ModifiedDate != null ? premium.ModifiedDate.ToString() : string.Empty;

                    YearDataList.Add(yearVM);
                }

                return YearDataList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<ClaimStatusVM> GetClaimStatus()
        {
            try
            {
                var ClaimStatusData = unitOfWork.TblClaimStatuRepository.Get().ToList();

                List<ClaimStatusVM> ClaimStatusList = new List<ClaimStatusVM>();

                foreach (var claimStatus in ClaimStatusData)
                {
                    ClaimStatusVM claimStatusVM = new ClaimStatusVM();
                    claimStatusVM.ClaimStatus = claimStatus.ClaimStatusID;
                    claimStatusVM.ClaimDescription = claimStatus.Description;
                    //loadingTypeVM.CreatedBy = premium.CreatedBy != null ? Convert.ToInt32(premium.CreatedBy) : 0;
                    //loadingTypeVM.CreatedDate = premium.CreatedDate != null ? premium.CreatedDate.ToString() : string.Empty;
                    //loadingTypeVM.ModifiedBy = premium.ModifiedBy != null ? Convert.ToInt32(premium.ModifiedBy) : 0;
                    //loadingTypeVM.ModifiedDate = premium.ModifiedDate != null ? premium.ModifiedDate.ToString() : string.Empty;

                    ClaimStatusList.Add(claimStatusVM);
                }

                return ClaimStatusList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<InsClassTypeVM> GetInsClassType(int InsuranceType)
        {
            try
            {
                //var InsClassTypeData = unitOfWork.TblClaimPaymentRepository.Get(x => x.tblClaimRecording.tblPolicyInformationRecording.tblQuotationHeader.tblClientRequestHeader.tblClient.BUID == businessUnitID).ToList();
                var InsClassTypeData = unitOfWork.TbtblInsClassTypelRepository.Get(x => x.InsClass == InsuranceType).ToList();
                List<InsClassTypeVM> InsClassTypeDataList = new List<InsClassTypeVM>();

                foreach (var insClassType in InsClassTypeData)
                {
                    InsClassTypeVM insClassTypeVM = new InsClassTypeVM();
                    insClassTypeVM.InsClassTypeID = insClassType.InsClassTypeID;
                    insClassTypeVM.InsClassTypeDes = insClassType.InsClassTypeDes;
                    //loadingTypeVM.CreatedBy = premium.CreatedBy != null ? Convert.ToInt32(premium.CreatedBy) : 0;
                    //loadingTypeVM.CreatedDate = premium.CreatedDate != null ? premium.CreatedDate.ToString() : string.Empty;
                    //loadingTypeVM.ModifiedBy = premium.ModifiedBy != null ? Convert.ToInt32(premium.ModifiedBy) : 0;
                    //loadingTypeVM.ModifiedDate = premium.ModifiedDate != null ? premium.ModifiedDate.ToString() : string.Empty;

                    InsClassTypeDataList.Add(insClassTypeVM);
                }

                return InsClassTypeDataList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<StatusVM> GetStatus()
        {
            try
            {
                //var InsClassTypeData = unitOfWork.TblClaimPaymentRepository.Get(x => x.tblClaimRecording.tblPolicyInformationRecording.tblQuotationHeader.tblClientRequestHeader.tblClient.BUID == businessUnitID).ToList();
                var StatusData = unitOfWork.StatusDetailsRepository.Get().ToList();

                List<StatusVM> statusVMList = new List<StatusVM>();

                foreach (var statusData in StatusData)
                {
                    StatusVM statusVM = new StatusVM();
                    statusVM.StatusId = statusData.StatusId;
                    statusVM.StatusName = statusData.StatusName;
                    //loadingTypeVM.CreatedBy = premium.CreatedBy != null ? Convert.ToInt32(premium.CreatedBy) : 0;
                    //loadingTypeVM.CreatedDate = premium.CreatedDate != null ? premium.CreatedDate.ToString() : string.Empty;
                    //loadingTypeVM.ModifiedBy = premium.ModifiedBy != null ? Convert.ToInt32(premium.ModifiedBy) : 0;
                    //loadingTypeVM.ModifiedDate = premium.ModifiedDate != null ? premium.ModifiedDate.ToString() : string.Empty;

                    statusVMList.Add(statusVM);
                }

                return statusVMList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<ClaimListMapper> GetClaimsByDate(FilterMapper filterMapper)
        {
            using (var db = new PERFECTIBSEntities())
            {
                try
                {
                    var qry = (from q in db.tblClaimRecordings
                               join cl in db.tblClients on q.ClientID equals cl.ClientID
                               join cs in db.tblClaimStatus on q.ClaimStatus equals cs.ClaimStatusID
                               where q.DateOfLoss >= filterMapper.fromDate && q.DateOfLoss <= filterMapper.toDate
                               select new ClaimListMapper
                               {
                                   ClientId = q.ClientID,
                                   ClientName = cl.ClientName,
                                   ClaimNo = q.ClaimNo,
                                   DateOfLoss = q.DateOfLoss,
                                   DateOfIntimation = q.DateOfIntimation,
                                   CauseOfLoss = q.CauseOfLoss,
                                   AmountClaim = q.AmountClaimed,
                                   AmountPaid = q.AmountPaid,
                                   Status = cs.Description,
                               }).ToList();

                    return qry;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        #endregion
    }
}
