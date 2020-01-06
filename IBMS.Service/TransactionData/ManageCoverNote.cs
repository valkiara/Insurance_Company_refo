using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.TransactionData
{
    public class ManageCoverNote
    {
        private UnitOfWork unitOfWork;
        public ManageCoverNote()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool SaveCoverNote(CoverNoteVM coverNoteVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCoverNote coverNote = new tblCoverNote();
                    coverNote.QuotationHeaderID = coverNoteVM.QuotationHeaderID;
                    coverNote.InsSubClassID = coverNoteVM.InsuranceSubClassID;
                    coverNote.CoverNoteNo = coverNoteVM.CoverNoteNo;
                    coverNote.ConfirmationMethod = coverNoteVM.ConfirmationMethod;
                    coverNote.PendingDocItem = coverNoteVM.PendingDocItem;
                    coverNote.FromDate = !string.IsNullOrEmpty(coverNoteVM.FromDate) ? DateTime.ParseExact(coverNoteVM.FromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    coverNote.ToDate = !string.IsNullOrEmpty(coverNoteVM.ToDate) ? DateTime.ParseExact(coverNoteVM.ToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    coverNote.IssuedDate = !string.IsNullOrEmpty(coverNoteVM.IssuedDate) ? DateTime.ParseExact(coverNoteVM.IssuedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    coverNote.CreatedDate = DateTime.Now;
                    coverNote.CreatedBy = coverNoteVM.CreatedBy;
                    unitOfWork.TblCoverNoteRepository.Insert(coverNote);
                    unitOfWork.Save();

                    //Update Quotation Status Code
                    tblQuotationHeader quotationHeader = unitOfWork.TblQuotationHeaderRepository.GetByID(coverNoteVM.QuotationHeaderID);

                    if (quotationHeader.QuotationStatusCode != QuotationStatusCodeEnum.TCNI.ToString())
                    {
                        quotationHeader.QuotationStatusCode = QuotationStatusCodeEnum.TCNI.ToString();
                        unitOfWork.TblQuotationHeaderRepository.Update(quotationHeader);
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

        public bool UpdateCoverNote(CoverNoteVM coverNoteVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCoverNote coverNote = unitOfWork.TblCoverNoteRepository.GetByID(coverNoteVM.CoverNoteID);
                    coverNote.QuotationHeaderID = coverNoteVM.QuotationHeaderID;
                    coverNote.InsSubClassID = coverNoteVM.InsuranceSubClassID;
                    coverNote.CoverNoteNo = coverNoteVM.CoverNoteNo;
                    coverNote.ConfirmationMethod = coverNoteVM.ConfirmationMethod;
                    coverNote.PendingDocItem = coverNoteVM.PendingDocItem;
                    coverNote.FromDate = !string.IsNullOrEmpty(coverNoteVM.FromDate) ? DateTime.ParseExact(coverNoteVM.FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    coverNote.ToDate = !string.IsNullOrEmpty(coverNoteVM.ToDate) ? DateTime.ParseExact(coverNoteVM.ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    coverNote.IssuedDate = !string.IsNullOrEmpty(coverNoteVM.IssuedDate) ? DateTime.ParseExact(coverNoteVM.IssuedDate, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    coverNote.ModifiedDate = DateTime.Now;
                    coverNote.ModifiedBy = coverNoteVM.ModifiedBy;
                    unitOfWork.TblCoverNoteRepository.Update(coverNote);
                    unitOfWork.Save();

                    //Update Quotation Status Code
                    tblQuotationHeader quotationHeader = unitOfWork.TblQuotationHeaderRepository.GetByID(coverNoteVM.QuotationHeaderID);

                    if (quotationHeader.QuotationStatusCode != QuotationStatusCodeEnum.TCNI.ToString())
                    {
                        quotationHeader.QuotationStatusCode = QuotationStatusCodeEnum.TCNI.ToString();
                        unitOfWork.TblQuotationHeaderRepository.Update(quotationHeader);
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

        public List<CoverNoteVM> GetAllCoverNotes()
        {
            try
            {
                var coverNoteData = unitOfWork.TblCoverNoteRepository.Get().ToList();

                List<CoverNoteVM> coverNoteList = new List<CoverNoteVM>();

                foreach (var coverNote in coverNoteData)
                {
                    CoverNoteVM coverNoteVM = new CoverNoteVM();
                    coverNoteVM.CoverNoteID = coverNote.CoverNoteID;
                    coverNoteVM.QuotationHeaderID = coverNote.QuotationHeaderID != null ? Convert.ToInt32(coverNote.QuotationHeaderID) : 0;
                    coverNoteVM.InsuranceSubClassID = coverNote.InsSubClassID != null ? Convert.ToInt32(coverNote.InsSubClassID) : 0;

                    if (coverNoteVM.InsuranceSubClassID > 0)
                    {
                        coverNoteVM.InsuranceSubClassName = coverNote.tblInsSubClass.Description;
                    }

                    coverNoteVM.CoverNoteNo = coverNote.CoverNoteNo;
                    coverNoteVM.ConfirmationMethod = coverNote.ConfirmationMethod;
                    coverNoteVM.PendingDocItem = coverNote.PendingDocItem;
                    coverNoteVM.FromDate = coverNote.FromDate != null ? coverNote.FromDate.ToString() : string.Empty;
                    coverNoteVM.ToDate = coverNote.ToDate != null ? coverNote.ToDate.ToString() : string.Empty;
                    coverNoteVM.IssuedDate = coverNote.IssuedDate != null ? coverNote.IssuedDate.ToString() : string.Empty;
                    coverNoteVM.CreatedBy = coverNote.CreatedBy != null ? Convert.ToInt32(coverNote.CreatedBy) : 0;
                    coverNoteVM.CreatedDate = coverNote.CreatedDate != null ? coverNote.CreatedDate.ToString() : string.Empty;
                    coverNoteVM.ModifiedBy = coverNote.ModifiedBy != null ? Convert.ToInt32(coverNote.ModifiedBy) : 0;
                    coverNoteVM.ModifiedDate = coverNote.ModifiedDate != null ? coverNote.ModifiedDate.ToString() : string.Empty;

                    coverNoteList.Add(coverNoteVM);
                }

                return coverNoteList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CoverNoteVM> GetAllCoverNotesByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var coverNoteData = unitOfWork.TblCoverNoteRepository.Get(x => x.tblInsSubClass.tblInsClass.BUID == businessUnitID).ToList();

                List<CoverNoteVM> coverNoteList = new List<CoverNoteVM>();

                foreach (var coverNote in coverNoteData)
                {
                    CoverNoteVM coverNoteVM = new CoverNoteVM();
                    coverNoteVM.CoverNoteID = coverNote.CoverNoteID;
                    coverNoteVM.QuotationHeaderID = coverNote.QuotationHeaderID != null ? Convert.ToInt32(coverNote.QuotationHeaderID) : 0;
                    coverNoteVM.InsuranceSubClassID = coverNote.InsSubClassID != null ? Convert.ToInt32(coverNote.InsSubClassID) : 0;

                    if (coverNoteVM.InsuranceSubClassID > 0)
                    {
                        coverNoteVM.InsuranceSubClassName = coverNote.tblInsSubClass.Description;
                    }

                    coverNoteVM.CoverNoteNo = coverNote.CoverNoteNo;
                    coverNoteVM.ConfirmationMethod = coverNote.ConfirmationMethod;
                    coverNoteVM.PendingDocItem = coverNote.PendingDocItem;
                    coverNoteVM.FromDate = coverNote.FromDate != null ? coverNote.FromDate.ToString() : string.Empty;
                    coverNoteVM.ToDate = coverNote.ToDate != null ? coverNote.ToDate.ToString() : string.Empty;
                    coverNoteVM.IssuedDate = coverNote.IssuedDate != null ? coverNote.IssuedDate.ToString() : string.Empty;
                    coverNoteVM.CreatedBy = coverNote.CreatedBy != null ? Convert.ToInt32(coverNote.CreatedBy) : 0;
                    coverNoteVM.CreatedDate = coverNote.CreatedDate != null ? coverNote.CreatedDate.ToString() : string.Empty;
                    coverNoteVM.ModifiedBy = coverNote.ModifiedBy != null ? Convert.ToInt32(coverNote.ModifiedBy) : 0;
                    coverNoteVM.ModifiedDate = coverNote.ModifiedDate != null ? coverNote.ModifiedDate.ToString() : string.Empty;

                    coverNoteList.Add(coverNoteVM);
                }

                return coverNoteList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public CoverNoteVM GetCoverNoteByID(int coverNoteID)
        {
            try
            {
                var coverNoteData = unitOfWork.TblCoverNoteRepository.GetByID(coverNoteID);

                CoverNoteVM coverNoteVM = new CoverNoteVM();
                coverNoteVM.CoverNoteID = coverNoteData.CoverNoteID;
                coverNoteVM.QuotationHeaderID = coverNoteData.QuotationHeaderID != null ? Convert.ToInt32(coverNoteData.QuotationHeaderID) : 0;
                coverNoteVM.InsuranceSubClassID = coverNoteData.InsSubClassID != null ? Convert.ToInt32(coverNoteData.InsSubClassID) : 0;

                if (coverNoteVM.InsuranceSubClassID > 0)
                {
                    coverNoteVM.InsuranceSubClassName = coverNoteData.tblInsSubClass.Description;
                }

                coverNoteVM.CoverNoteNo = coverNoteData.CoverNoteNo;
                coverNoteVM.ConfirmationMethod = coverNoteData.ConfirmationMethod;
                coverNoteVM.PendingDocItem = coverNoteData.PendingDocItem;
                coverNoteVM.FromDate = coverNoteData.FromDate != null ? coverNoteData.FromDate.ToString() : string.Empty;
                coverNoteVM.ToDate = coverNoteData.ToDate != null ? coverNoteData.ToDate.ToString() : string.Empty;
                coverNoteVM.IssuedDate = coverNoteData.IssuedDate != null ? coverNoteData.IssuedDate.ToString() : string.Empty;
                coverNoteVM.CreatedBy = coverNoteData.CreatedBy != null ? Convert.ToInt32(coverNoteData.CreatedBy) : 0;
                coverNoteVM.CreatedDate = coverNoteData.CreatedDate != null ? coverNoteData.CreatedDate.ToString() : string.Empty;
                coverNoteVM.ModifiedBy = coverNoteData.ModifiedBy != null ? Convert.ToInt32(coverNoteData.ModifiedBy) : 0;
                coverNoteVM.ModifiedDate = coverNoteData.ModifiedDate != null ? coverNoteData.ModifiedDate.ToString() : string.Empty;

                return coverNoteVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CoverNoteVM> GetCoverByQuotationID(int quotationID)
        {
            try
            {
                var coverNoteData = unitOfWork.TblCoverNoteRepository.Get(x => x.QuotationHeaderID == quotationID).ToList();

                List<CoverNoteVM> coverNote = new List<CoverNoteVM>();
                foreach (var covernote in coverNoteData)
                {
                    CoverNoteVM coverNoteVM = new CoverNoteVM();
                    coverNoteVM.CoverNoteID = covernote.CoverNoteID;
                    coverNoteVM.QuotationHeaderID = covernote.QuotationHeaderID != null ? Convert.ToInt32(covernote.QuotationHeaderID) : 0;
                    coverNoteVM.InsuranceSubClassID = covernote.InsSubClassID != null ? Convert.ToInt32(covernote.InsSubClassID) : 0;
                    if (coverNoteVM.InsuranceSubClassID > 0)
                    {
                        coverNoteVM.InsuranceSubClassName = covernote.tblInsSubClass.Description;
                    }
                    coverNoteVM.CoverNoteNo = covernote.CoverNoteNo;
                    coverNoteVM.ConfirmationMethod = covernote.ConfirmationMethod;
                    coverNoteVM.PendingDocItem = covernote.PendingDocItem;
                    coverNoteVM.FromDate = covernote.FromDate != null ? covernote.FromDate.ToString() : string.Empty;
                    coverNoteVM.ToDate = covernote.ToDate != null ? covernote.ToDate.ToString() : string.Empty;
                    coverNoteVM.IssuedDate = covernote.IssuedDate != null ? covernote.IssuedDate.ToString() : string.Empty;
                    coverNoteVM.CreatedBy = covernote.CreatedBy != null ? Convert.ToInt32(covernote.CreatedBy) : 0;
                    coverNoteVM.CreatedDate = covernote.CreatedDate != null ? covernote.CreatedDate.ToString() : string.Empty;
                    coverNoteVM.ModifiedBy = covernote.ModifiedBy != null ? Convert.ToInt32(covernote.ModifiedBy) : 0;
                    coverNoteVM.ModifiedDate = covernote.ModifiedDate != null ? covernote.ModifiedDate.ToString() : string.Empty;
                    coverNote.Add(coverNoteVM);
                }
                //CoverNoteVM coverNoteVM = new CoverNoteVM();


                //if (coverNoteVM.InsuranceSubClassID > 0)
                //{
                //    coverNoteVM.InsuranceSubClassName = coverNoteData.tblInsSubClass.Description;
                //}



                return coverNote;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
