using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManageRateCategory
    {
        private UnitOfWork unitOfWork;
        public ManageRateCategory()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool SaveRateCategory(RateCategoryVM rateCategoryVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblRateCategory rateCategory = new tblRateCategory();
                    rateCategory.RateCategory = rateCategoryVM.RateCategoryName;
                    rateCategory.CreatedDate = DateTime.Now;
                    rateCategory.CreatedBy = rateCategoryVM.CreatedBy;
                    unitOfWork.TblRateCategoryRepository.Insert(rateCategory);
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

        public bool UpdateRateCategory(RateCategoryVM rateCategoryVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblRateCategory rateCategory = unitOfWork.TblRateCategoryRepository.GetByID(rateCategoryVM.RateCategoryID);
                    rateCategory.RateCategory = rateCategoryVM.RateCategoryName;
                    rateCategory.ModifiedDate = DateTime.Now;
                    rateCategory.ModifiedBy = rateCategoryVM.ModifiedBy;
                    unitOfWork.TblRateCategoryRepository.Update(rateCategory);
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

        public bool DeleteRateCategory(int rateCategoryID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblRateCategory rateCategory = unitOfWork.TblRateCategoryRepository.GetByID(rateCategoryID);
                    unitOfWork.TblRateCategoryRepository.Delete(rateCategory);
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

        public List<RateCategoryVM> GetAllRateCategories()
        {
            try
            {
                var rateCategoryData = unitOfWork.TblRateCategoryRepository.Get().ToList();

                List<RateCategoryVM> rateCategoryList = new List<RateCategoryVM>();

                foreach (var rateCategory in rateCategoryData)
                {
                    RateCategoryVM rateCategoryVM = new RateCategoryVM();
                    rateCategoryVM.RateCategoryID = rateCategory.RateCategoryID;
                    rateCategoryVM.RateCategoryName = rateCategory.RateCategory;
                    rateCategoryVM.CreatedDate = rateCategory.CreatedDate != null ? rateCategory.CreatedDate.ToString() : string.Empty;
                    rateCategoryVM.ModifiedDate = rateCategory.ModifiedDate != null ? rateCategory.ModifiedDate.ToString() : string.Empty;
                    rateCategoryVM.CreatedBy = rateCategory.CreatedBy != null ? Convert.ToInt32(rateCategory.CreatedBy) : 0;
                    rateCategoryVM.ModifiedBy = rateCategory.ModifiedBy != null ? Convert.ToInt32(rateCategory.ModifiedBy) : 0;

                    rateCategoryList.Add(rateCategoryVM);
                }

                return rateCategoryList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public RateCategoryVM GetRateCategoryByID(int rateCategoryID)
        {
            try
            {
                var rateCategoryData = unitOfWork.TblRateCategoryRepository.GetByID(rateCategoryID);

                RateCategoryVM rateCategoryVM = new RateCategoryVM();
                rateCategoryVM.RateCategoryID = rateCategoryData.RateCategoryID;
                rateCategoryVM.RateCategoryName = rateCategoryData.RateCategory;
                rateCategoryVM.CreatedDate = rateCategoryData.CreatedDate != null ? rateCategoryData.CreatedDate.ToString() : string.Empty;
                rateCategoryVM.ModifiedDate = rateCategoryData.ModifiedDate != null ? rateCategoryData.ModifiedDate.ToString() : string.Empty;
                rateCategoryVM.CreatedBy = rateCategoryData.CreatedBy != null ? Convert.ToInt32(rateCategoryData.CreatedBy) : 0;
                rateCategoryVM.ModifiedBy = rateCategoryData.ModifiedBy != null ? Convert.ToInt32(rateCategoryData.ModifiedBy) : 0;

                return rateCategoryVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsRateCategoryAvailable(int? rateCategoryID, string rateCategoryName)
        {
            try
            {
                if (rateCategoryID != null && unitOfWork.TblRateCategoryRepository.Get().Any(x => x.RateCategory.ToLower() == rateCategoryName.ToLower() && x.RateCategoryID != rateCategoryID))
                {
                    return true;
                }
                else if (rateCategoryID == null && unitOfWork.TblRateCategoryRepository.Get().Any(x => x.RateCategory.ToLower() == rateCategoryName.ToLower()))
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
    }
}
