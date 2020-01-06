using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManageClaimIssue
    {
        private UnitOfWork unitOfWork;
        public ManageClaimIssue()
        {
            unitOfWork = new UnitOfWork();
        }

        #region Claim Reject Reason
        public bool SaveClaimRejectReason(ClaimRejectReasonVM claimRejectReasonVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblClaimRejectReason claimRejectReason = new tblClaimRejectReason();
                    claimRejectReason.ClaimRejectReason = claimRejectReasonVM.ClaimRejectReason;
                    claimRejectReason.CreatedDate = DateTime.Now;
                    claimRejectReason.CreatedBy = claimRejectReasonVM.CreatedBy;
                    unitOfWork.TblClaimRejectReasonRepository.Insert(claimRejectReason);
                    unitOfWork.Save();

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

        public bool IsClaimAvailable(int? ClaimRejectReasonID, string ClaimRejectReason)
        {
            try
            {
                if (ClaimRejectReasonID != null && unitOfWork.TblClaimRejectReasonRepository.Get().Any(x => x.ClaimRejectReason.ToLower() == ClaimRejectReason.ToLower() && x.ClaimRejectReasonID != ClaimRejectReasonID))
                {
                    return true;
                }
                else if (ClaimRejectReasonID == null && unitOfWork.TblClaimRejectReasonRepository.Get().Any(x => x.ClaimRejectReason.ToLower() == ClaimRejectReason.ToLower()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateClaimRejectReason(ClaimRejectReasonVM claimRejectReasonVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblClaimRejectReason claimRejectReason = unitOfWork.TblClaimRejectReasonRepository.GetByID(claimRejectReasonVM.ClaimRejectReasonID);
                    claimRejectReason.ClaimRejectReason = claimRejectReasonVM.ClaimRejectReason;
                    claimRejectReason.ModifiedDate = DateTime.Now;
                    claimRejectReason.ModifiedBy = claimRejectReasonVM.ModifiedBy;
                    unitOfWork.TblClaimRejectReasonRepository.Update(claimRejectReason);
                    unitOfWork.Save();

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

        public bool DeleteClaimRejectReason(int claimRejectReasonID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblClaimRejectReason claimRejectReason = unitOfWork.TblClaimRejectReasonRepository.GetByID(claimRejectReasonID);
                    unitOfWork.TblClaimRejectReasonRepository.Delete(claimRejectReason);
                    unitOfWork.Save();

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

        public List<ClaimRejectReasonVM> GetAllClaimRejectReasons()
        {
            try
            {
                var claimRejectReasonData = unitOfWork.TblClaimRejectReasonRepository.Get().ToList();

                List<ClaimRejectReasonVM> ClaimRejectReasonList = new List<ClaimRejectReasonVM>();

                foreach (var claimRejectReason in claimRejectReasonData)
                {
                    ClaimRejectReasonVM claimRejectReasonVM = new ClaimRejectReasonVM();
                    claimRejectReasonVM.ClaimRejectReasonID = claimRejectReason.ClaimRejectReasonID;
                    claimRejectReasonVM.ClaimRejectReason = claimRejectReason.ClaimRejectReason;
                    claimRejectReasonVM.CreatedBy = claimRejectReason.CreatedBy != null ? Convert.ToInt32(claimRejectReason.CreatedBy) : 0;
                    claimRejectReasonVM.CreatedDate = claimRejectReason.CreatedDate != null ? claimRejectReason.CreatedDate.ToString() : string.Empty;
                    claimRejectReasonVM.ModifiedBy = claimRejectReason.ModifiedBy != null ? Convert.ToInt32(claimRejectReason.ModifiedBy) : 0;
                    claimRejectReasonVM.ModifiedDate = claimRejectReason.ModifiedDate != null ? claimRejectReason.ModifiedDate.ToString() : string.Empty;

                    ClaimRejectReasonList.Add(claimRejectReasonVM);
                }

                return ClaimRejectReasonList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ClaimRejectReasonVM GetClaimRejectReasonByID(int claimRejectReasonID)
        {
            try
            {
                var claimRejectReasonData = unitOfWork.TblClaimRejectReasonRepository.GetByID(claimRejectReasonID);

                ClaimRejectReasonVM claimRejectReasonVM = new ClaimRejectReasonVM();
                claimRejectReasonVM.ClaimRejectReasonID = claimRejectReasonData.ClaimRejectReasonID;
                claimRejectReasonVM.ClaimRejectReason = claimRejectReasonData.ClaimRejectReason;
                claimRejectReasonVM.CreatedBy = claimRejectReasonData.CreatedBy != null ? Convert.ToInt32(claimRejectReasonData.CreatedBy) : 0;
                claimRejectReasonVM.CreatedDate = claimRejectReasonData.CreatedDate != null ? claimRejectReasonData.CreatedDate.ToString() : string.Empty;
                claimRejectReasonVM.ModifiedBy = claimRejectReasonData.ModifiedBy != null ? Convert.ToInt32(claimRejectReasonData.ModifiedBy) : 0;
                claimRejectReasonVM.ModifiedDate = claimRejectReasonData.ModifiedDate != null ? claimRejectReasonData.ModifiedDate.ToString() : string.Empty;

                return claimRejectReasonVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Claim Re-Open Reason
        public bool SaveClaimReOpenReason(ClaimReOpenReasonVM claimReOpenReasonVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblClaimReOpenReason claimReOpenReason = new tblClaimReOpenReason();
                    claimReOpenReason.ClaimReOpenReason = claimReOpenReasonVM.ClaimReOpenReason;
                    claimReOpenReason.CreatedDate = DateTime.Now;
                    claimReOpenReason.CreatedBy = claimReOpenReasonVM.CreatedBy;
                    unitOfWork.TblClaimReOpenReasonRepository.Insert(claimReOpenReason);
                    unitOfWork.Save();

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

        public bool IsClaimReOpenAvailable(int? ClaimRejectReasonID, string ClaimRejectReason)
        {
            try
            {
                if (ClaimRejectReasonID != null && unitOfWork.TblClaimReOpenReasonRepository.Get().Any(x => x.ClaimReOpenReason.ToLower() == ClaimRejectReason.ToLower() && x.ClaimReOpenReasonID != ClaimRejectReasonID))
                {
                    return true;
                }
                else if (ClaimRejectReasonID == null && unitOfWork.TblClaimReOpenReasonRepository.Get().Any(x => x.ClaimReOpenReason.ToLower() == ClaimRejectReason.ToLower()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateClaimReOpenReason(ClaimReOpenReasonVM claimReOpenReasonVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblClaimReOpenReason claimReOpenReason = unitOfWork.TblClaimReOpenReasonRepository.GetByID(claimReOpenReasonVM.ClaimReOpenReasonID);
                    claimReOpenReason.ClaimReOpenReason = claimReOpenReasonVM.ClaimReOpenReason;
                    claimReOpenReason.ModifiedDate = DateTime.Now;
                    claimReOpenReason.ModifiedBy = claimReOpenReasonVM.ModifiedBy;
                    unitOfWork.TblClaimReOpenReasonRepository.Update(claimReOpenReason);
                    unitOfWork.Save();

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

        public bool DeleteClaimReOpenReason(int claimReOpenReasonID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblClaimReOpenReason claimReOpenReason = unitOfWork.TblClaimReOpenReasonRepository.GetByID(claimReOpenReasonID);
                    unitOfWork.TblClaimReOpenReasonRepository.Delete(claimReOpenReason);
                    unitOfWork.Save();

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

        public List<ClaimReOpenReasonVM> GetAllClaimReOpenReasons()
        {
            try
            {
                var claimReOpenReasonData = unitOfWork.TblClaimReOpenReasonRepository.Get().ToList();

                List<ClaimReOpenReasonVM> claimReOpenReasonList = new List<ClaimReOpenReasonVM>();

                foreach (var claimReOpenReason in claimReOpenReasonData)
                {
                    ClaimReOpenReasonVM claimReOpenReasonVM = new ClaimReOpenReasonVM();
                    claimReOpenReasonVM.ClaimReOpenReasonID = claimReOpenReason.ClaimReOpenReasonID;
                    claimReOpenReasonVM.ClaimReOpenReason = claimReOpenReason.ClaimReOpenReason;
                    claimReOpenReasonVM.CreatedBy = claimReOpenReason.CreatedBy != null ? Convert.ToInt32(claimReOpenReason.CreatedBy) : 0;
                    claimReOpenReasonVM.CreatedDate = claimReOpenReason.CreatedDate != null ? claimReOpenReason.CreatedDate.ToString() : string.Empty;
                    claimReOpenReasonVM.ModifiedBy = claimReOpenReason.ModifiedBy != null ? Convert.ToInt32(claimReOpenReason.ModifiedBy) : 0;
                    claimReOpenReasonVM.ModifiedDate = claimReOpenReason.ModifiedDate != null ? claimReOpenReason.ModifiedDate.ToString() : string.Empty;

                    claimReOpenReasonList.Add(claimReOpenReasonVM);
                }

                return claimReOpenReasonList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ClaimReOpenReasonVM GetClaimReOpenReasonByID(int claimReOpenReasonID)
        {
            try
            {
                var claimReOpenReasonData = unitOfWork.TblClaimReOpenReasonRepository.GetByID(claimReOpenReasonID);

                ClaimReOpenReasonVM claimReOpenReasonVM = new ClaimReOpenReasonVM();
                claimReOpenReasonVM.ClaimReOpenReasonID = claimReOpenReasonData.ClaimReOpenReasonID;
                claimReOpenReasonVM.ClaimReOpenReason = claimReOpenReasonData.ClaimReOpenReason;
                claimReOpenReasonVM.CreatedBy = claimReOpenReasonData.CreatedBy != null ? Convert.ToInt32(claimReOpenReasonData.CreatedBy) : 0;
                claimReOpenReasonVM.CreatedDate = claimReOpenReasonData.CreatedDate != null ? claimReOpenReasonData.CreatedDate.ToString() : string.Empty;
                claimReOpenReasonVM.ModifiedBy = claimReOpenReasonData.ModifiedBy != null ? Convert.ToInt32(claimReOpenReasonData.ModifiedBy) : 0;
                claimReOpenReasonVM.ModifiedDate = claimReOpenReasonData.ModifiedDate != null ? claimReOpenReasonData.ModifiedDate.ToString() : string.Empty;

                return claimReOpenReasonVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Cause Of Loss
        public bool SaveCauseOfLoss(CauseOfLossVM causeOfLossVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCauseOfLoss causeOfLoss = new tblCauseOfLoss();
                    causeOfLoss.CauseOfLoss = causeOfLossVM.CauseOfLoss;
                    causeOfLoss.InsSubClassID = causeOfLossVM.InsSubClassID;
                    causeOfLoss.CreatedDate = DateTime.Now;
                    causeOfLoss.CreatedBy = causeOfLossVM.CreatedBy;
                    unitOfWork.TblCauseOfLossRepository.Insert(causeOfLoss);
                    unitOfWork.Save();

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

        public bool IscauseOfLossAvailable(int? ClaimRejectReasonID, string ClaimRejectReason)
        {
            try
            {
                if (ClaimRejectReasonID != null && unitOfWork.TblCauseOfLossRepository.Get().Any(x => x.CauseOfLoss.ToLower() == ClaimRejectReason.ToLower() && x.CauseOfLossID != ClaimRejectReasonID))
                {
                    return true;
                }
                else if (ClaimRejectReasonID == null && unitOfWork.TblCauseOfLossRepository.Get().Any(x => x.CauseOfLoss.ToLower() == ClaimRejectReason.ToLower()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateCauseOfLoss(CauseOfLossVM causeOfLossVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCauseOfLoss causeOfLoss = unitOfWork.TblCauseOfLossRepository.GetByID(causeOfLossVM.CauseOfLossID);
                    causeOfLoss.CauseOfLoss = causeOfLossVM.CauseOfLoss;
                    causeOfLoss.InsSubClassID = causeOfLossVM.InsSubClassID;
                    causeOfLoss.ModifiedDate = DateTime.Now;
                    causeOfLoss.ModifiedBy = causeOfLossVM.ModifiedBy;
                    unitOfWork.TblCauseOfLossRepository.Update(causeOfLoss);
                    unitOfWork.Save();

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

        public bool DeleteCauseOfLoss(int causeOfLossID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCauseOfLoss causeOfLoss = unitOfWork.TblCauseOfLossRepository.GetByID(causeOfLossID);
                    unitOfWork.TblCauseOfLossRepository.Delete(causeOfLoss);
                    unitOfWork.Save();

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

        public List<CauseOfLossVM> GetAllCauseOfLosses()
        {
            try
            {
                var causeOfLossData = unitOfWork.TblCauseOfLossRepository.Get().ToList();

                List<CauseOfLossVM> causeOfLossList = new List<CauseOfLossVM>();

                foreach (var causeOfLoss in causeOfLossData)
                {
                    CauseOfLossVM causeOfLossVM = new CauseOfLossVM();
                    causeOfLossVM.CauseOfLossID = causeOfLoss.CauseOfLossID;
                    causeOfLossVM.CauseOfLoss = causeOfLoss.CauseOfLoss;
                    causeOfLossVM.InsClassID = causeOfLoss.tblInsSubClass.InsClassID != null ? Convert.ToInt32(causeOfLoss.tblInsSubClass.InsClassID) : 0;

                    if (causeOfLossVM.InsClassID > 0)
                    {
                        causeOfLossVM.InsClassCode = causeOfLoss.tblInsSubClass.tblInsClass.Code;
                    }

                    causeOfLossVM.InsSubClassID = causeOfLoss.InsSubClassID != null ? Convert.ToInt32(causeOfLoss.InsSubClassID) : 0;

                    if (causeOfLossVM.InsSubClassID > 0)
                    {
                        causeOfLossVM.InsSubClassDescription = causeOfLoss.tblInsSubClass.Description;
                    }

                    causeOfLossVM.CreatedBy = causeOfLoss.CreatedBy != null ? Convert.ToInt32(causeOfLoss.CreatedBy) : 0;
                    causeOfLossVM.CreatedDate = causeOfLoss.CreatedDate != null ? causeOfLoss.CreatedDate.ToString() : string.Empty;
                    causeOfLossVM.ModifiedBy = causeOfLoss.ModifiedBy != null ? Convert.ToInt32(causeOfLoss.ModifiedBy) : 0;
                    causeOfLossVM.ModifiedDate = causeOfLoss.ModifiedDate != null ? causeOfLoss.ModifiedDate.ToString() : string.Empty;

                    causeOfLossList.Add(causeOfLossVM);
                }

                return causeOfLossList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public CauseOfLossVM GetCauseOfLossByID(int causeOfLossID)
        {
            try
            {
                var causeOfLossData = unitOfWork.TblCauseOfLossRepository.GetByID(causeOfLossID);

                CauseOfLossVM causeOfLossVM = new CauseOfLossVM();
                causeOfLossVM.CauseOfLossID = causeOfLossData.CauseOfLossID;
                causeOfLossVM.CauseOfLoss = causeOfLossData.CauseOfLoss;
                causeOfLossVM.InsClassID = causeOfLossData.tblInsSubClass.InsClassID != null ? Convert.ToInt32(causeOfLossData.tblInsSubClass.InsClassID) : 0;

                if (causeOfLossVM.InsClassID > 0)
                {
                    causeOfLossVM.InsClassCode = causeOfLossData.tblInsSubClass.tblInsClass.Code;
                }

                causeOfLossVM.InsSubClassID = causeOfLossData.InsSubClassID != null ? Convert.ToInt32(causeOfLossData.InsSubClassID) : 0;

                if (causeOfLossVM.InsSubClassID > 0)
                {
                    causeOfLossVM.InsSubClassDescription = causeOfLossData.tblInsSubClass.Description;
                }

                causeOfLossVM.CreatedBy = causeOfLossData.CreatedBy != null ? Convert.ToInt32(causeOfLossData.CreatedBy) : 0;
                causeOfLossVM.CreatedDate = causeOfLossData.CreatedDate != null ? causeOfLossData.CreatedDate.ToString() : string.Empty;
                causeOfLossVM.ModifiedBy = causeOfLossData.ModifiedBy != null ? Convert.ToInt32(causeOfLossData.ModifiedBy) : 0;
                causeOfLossVM.ModifiedDate = causeOfLossData.ModifiedDate != null ? causeOfLossData.ModifiedDate.ToString() : string.Empty;

                return causeOfLossVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CauseOfLossVM> GetAllCauseOfLossesByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var causeOfLossData = unitOfWork.TblCauseOfLossRepository.Get(x => x.tblInsSubClass.tblInsClass.BUID == businessUnitID).ToList();

                List<CauseOfLossVM> causeOfLossList = new List<CauseOfLossVM>();

                foreach (var causeOfLoss in causeOfLossData)
                {
                    CauseOfLossVM causeOfLossVM = new CauseOfLossVM();
                    causeOfLossVM.CauseOfLossID = causeOfLoss.CauseOfLossID;
                    causeOfLossVM.CauseOfLoss = causeOfLoss.CauseOfLoss;
                    causeOfLossVM.InsClassID = causeOfLoss.tblInsSubClass.InsClassID != null ? Convert.ToInt32(causeOfLoss.tblInsSubClass.InsClassID) : 0;

                    if (causeOfLossVM.InsClassID > 0)
                    {
                        causeOfLossVM.InsClassCode = causeOfLoss.tblInsSubClass.tblInsClass.Code;
                    }

                    causeOfLossVM.InsSubClassID = causeOfLoss.InsSubClassID != null ? Convert.ToInt32(causeOfLoss.InsSubClassID) : 0;

                    if (causeOfLossVM.InsSubClassID > 0)
                    {
                        causeOfLossVM.InsSubClassDescription = causeOfLoss.tblInsSubClass.Description;
                    }

                    causeOfLossVM.CreatedBy = causeOfLoss.CreatedBy != null ? Convert.ToInt32(causeOfLoss.CreatedBy) : 0;
                    causeOfLossVM.CreatedDate = causeOfLoss.CreatedDate != null ? causeOfLoss.CreatedDate.ToString() : string.Empty;
                    causeOfLossVM.ModifiedBy = causeOfLoss.ModifiedBy != null ? Convert.ToInt32(causeOfLoss.ModifiedBy) : 0;
                    causeOfLossVM.ModifiedDate = causeOfLoss.ModifiedDate != null ? causeOfLoss.ModifiedDate.ToString() : string.Empty;

                    causeOfLossList.Add(causeOfLossVM);
                }

                return causeOfLossList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
