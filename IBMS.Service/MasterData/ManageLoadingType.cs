using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManageLoadingType
    {
        private UnitOfWork unitOfWork;
        public ManageLoadingType()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool SaveLoadingType(LoadingTypeVM loadingTypeVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblLoadingType loadingType = new tblLoadingType();
                    loadingType.Description = loadingTypeVM.Description;
                    loadingType.CreatedDate = DateTime.Now;
                    loadingType.CreatedBy = loadingTypeVM.CreatedBy;
                    unitOfWork.TblLoadingTypeRepository.Insert(loadingType);
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

        public bool IsLoadingAvailable(int? LoadingTypeID, string Description)
        {
            try
            {
                if (LoadingTypeID != null && unitOfWork.TblLoadingTypeRepository.Get().Any(x => x.Description.ToLower() == Description.ToLower() && x.LoadingTypeID != LoadingTypeID))
                {
                    return true;
                }
                else if (LoadingTypeID == null && unitOfWork.TblLoadingTypeRepository.Get().Any(x => x.Description.ToLower() == Description.ToLower()))
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

        public bool UpdateLoadingType(LoadingTypeVM loadingTypeVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblLoadingType loadingType = unitOfWork.TblLoadingTypeRepository.GetByID(loadingTypeVM.LoadingTypeID);
                    loadingType.Description = loadingTypeVM.Description;
                    loadingType.ModifiedDate = DateTime.Now;
                    loadingType.ModifiedBy = loadingTypeVM.ModifiedBy;
                    unitOfWork.TblLoadingTypeRepository.Update(loadingType);
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

        public bool DeleteLoadingType(int loadingTypeID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblLoadingType loadingType = unitOfWork.TblLoadingTypeRepository.GetByID(loadingTypeID);
                    unitOfWork.TblLoadingTypeRepository.Delete(loadingType);
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

        public List<LoadingTypeVM> GetAllLoadingTypes()
        {
            try
            {
                var loadingTypeData = unitOfWork.TblLoadingTypeRepository.Get().ToList();

                List<LoadingTypeVM> loadingTypeList = new List<LoadingTypeVM>();

                foreach (var loadingType in loadingTypeData)
                {
                    LoadingTypeVM loadingTypeVM = new LoadingTypeVM();
                    loadingTypeVM.LoadingTypeID = loadingType.LoadingTypeID;
                    loadingTypeVM.Description = loadingType.Description;
                    loadingTypeVM.CreatedBy = loadingType.CreatedBy != null ? Convert.ToInt32(loadingType.CreatedBy) : 0;
                    loadingTypeVM.CreatedDate = loadingType.CreatedDate != null ? loadingType.CreatedDate.ToString() : string.Empty;
                    loadingTypeVM.ModifiedBy = loadingType.ModifiedBy != null ? Convert.ToInt32(loadingType.ModifiedBy) : 0;
                    loadingTypeVM.ModifiedDate = loadingType.ModifiedDate != null ? loadingType.ModifiedDate.ToString() : string.Empty;

                    loadingTypeList.Add(loadingTypeVM);
                }

                return loadingTypeList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public LoadingTypeVM GetLoadingTypeByID(int loadingTypeID)
        {
            try
            {
                var loadingTypeData = unitOfWork.TblLoadingTypeRepository.GetByID(loadingTypeID);

                LoadingTypeVM loadingTypeVM = new LoadingTypeVM();
                loadingTypeVM.LoadingTypeID = loadingTypeData.LoadingTypeID;
                loadingTypeVM.Description = loadingTypeData.Description;
                loadingTypeVM.CreatedBy = loadingTypeData.CreatedBy != null ? Convert.ToInt32(loadingTypeData.CreatedBy) : 0;
                loadingTypeVM.CreatedDate = loadingTypeData.CreatedDate != null ? loadingTypeData.CreatedDate.ToString() : string.Empty;
                loadingTypeVM.ModifiedBy = loadingTypeData.ModifiedBy != null ? Convert.ToInt32(loadingTypeData.ModifiedBy) : 0;
                loadingTypeVM.ModifiedDate = loadingTypeData.ModifiedDate != null ? loadingTypeData.ModifiedDate.ToString() : string.Empty;

                return loadingTypeVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
