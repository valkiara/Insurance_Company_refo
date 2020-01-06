using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManageCommonInsuranceScope
    {
        private UnitOfWork unitOfWork;
        public ManageCommonInsuranceScope()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool SaveCommonInsScope(CommonInsuranceScopeVM commonInsuranceScopeVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCommonInsScope commonInsScope = new tblCommonInsScope();
                    commonInsScope.Description = commonInsuranceScopeVM.Description;
                    commonInsScope.InsClassID = commonInsuranceScopeVM.InsuranceClassID;
                    commonInsScope.InsSubClassID = commonInsuranceScopeVM.InsuranceSubClassID;
                    commonInsScope.CreatedDate = DateTime.Now;
                    commonInsScope.CreatedBy = commonInsuranceScopeVM.CreatedBy;
                    unitOfWork.TblCommonInsScopeRepository.Insert(commonInsScope);
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

        public bool UpdateCommonInsScope(CommonInsuranceScopeVM commonInsuranceScopeVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCommonInsScope commonInsScope = unitOfWork.TblCommonInsScopeRepository.GetByID(commonInsuranceScopeVM.CommonInsuranceScopeID);
                    commonInsScope.Description = commonInsuranceScopeVM.Description;
                    commonInsScope.InsClassID = commonInsuranceScopeVM.InsuranceClassID;
                    commonInsScope.InsSubClassID = commonInsuranceScopeVM.InsuranceSubClassID;
                    commonInsScope.ModifiedDate = DateTime.Now;
                    commonInsScope.ModifiedBy = commonInsuranceScopeVM.ModifiedBy;
                    unitOfWork.TblCommonInsScopeRepository.Update(commonInsScope);
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

        public bool DeleteCommonInsScope(int commonInsScopeID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCommonInsScope commonInsScope = unitOfWork.TblCommonInsScopeRepository.GetByID(commonInsScopeID);
                    unitOfWork.TblCommonInsScopeRepository.Delete(commonInsScope);
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

        public List<CommonInsuranceScopeVM> GetAllCommonInsScopes()
        {
            try
            {
                var commonInsScopeData = unitOfWork.TblCommonInsScopeRepository.Get().ToList();

                List<CommonInsuranceScopeVM> commonInsScopeList = new List<CommonInsuranceScopeVM>();

                foreach (var commonInsScope in commonInsScopeData)
                {
                    CommonInsuranceScopeVM commonInsuranceScopeVM = new CommonInsuranceScopeVM();
                    commonInsuranceScopeVM.CommonInsuranceScopeID = commonInsScope.CommonInsScopeID;
                    commonInsuranceScopeVM.Description = commonInsScope.Description;
                    commonInsuranceScopeVM.InsuranceClassID = commonInsScope.InsClassID != null ? Convert.ToInt32(commonInsScope.InsClassID) : 0;

                    if (commonInsuranceScopeVM.InsuranceClassID > 0)
                    {
                        commonInsuranceScopeVM.InsuranceClassCode = commonInsScope.tblInsClass.Code;
                    }

                    commonInsuranceScopeVM.InsuranceSubClassID = commonInsScope.InsSubClassID != null ? Convert.ToInt32(commonInsScope.InsSubClassID) : 0;

                    if (commonInsuranceScopeVM.InsuranceSubClassID > 0)
                    {
                        commonInsuranceScopeVM.InsuranceSubClassName = commonInsScope.tblInsSubClass.Description;
                    }

                    commonInsuranceScopeVM.CreatedDate = commonInsScope.CreatedDate != null ? commonInsScope.CreatedDate.ToString() : string.Empty;
                    commonInsuranceScopeVM.ModifiedDate = commonInsScope.ModifiedDate != null ? commonInsScope.ModifiedDate.ToString() : string.Empty;
                    commonInsuranceScopeVM.CreatedBy = commonInsScope.CreatedBy != null ? Convert.ToInt32(commonInsScope.CreatedBy) : 0;
                    commonInsuranceScopeVM.ModifiedBy = commonInsScope.ModifiedBy != null ? Convert.ToInt32(commonInsScope.ModifiedBy) : 0;

                    commonInsScopeList.Add(commonInsuranceScopeVM);
                }

                return commonInsScopeList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CommonInsuranceScopeVM> GetAllCommonInsScopesByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var commonInsScopeData = unitOfWork.TblCommonInsScopeRepository.Get(x => x.tblInsClass.BUID == businessUnitID).ToList();

                List<CommonInsuranceScopeVM> commonInsScopeList = new List<CommonInsuranceScopeVM>();

                foreach (var commonInsScope in commonInsScopeData)
                {
                    CommonInsuranceScopeVM commonInsuranceScopeVM = new CommonInsuranceScopeVM();
                    commonInsuranceScopeVM.CommonInsuranceScopeID = commonInsScope.CommonInsScopeID;
                    commonInsuranceScopeVM.Description = commonInsScope.Description;
                    commonInsuranceScopeVM.InsuranceClassID = commonInsScope.InsClassID != null ? Convert.ToInt32(commonInsScope.InsClassID) : 0;

                    if (commonInsuranceScopeVM.InsuranceClassID > 0)
                    {
                        commonInsuranceScopeVM.InsuranceClassCode = commonInsScope.tblInsClass.Code;
                    }

                    commonInsuranceScopeVM.InsuranceSubClassID = commonInsScope.InsSubClassID != null ? Convert.ToInt32(commonInsScope.InsSubClassID) : 0;

                    if (commonInsuranceScopeVM.InsuranceSubClassID > 0)
                    {
                        commonInsuranceScopeVM.InsuranceSubClassName = commonInsScope.tblInsSubClass.Description;
                    }

                    commonInsuranceScopeVM.CreatedDate = commonInsScope.CreatedDate != null ? commonInsScope.CreatedDate.ToString() : string.Empty;
                    commonInsuranceScopeVM.ModifiedDate = commonInsScope.ModifiedDate != null ? commonInsScope.ModifiedDate.ToString() : string.Empty;
                    commonInsuranceScopeVM.CreatedBy = commonInsScope.CreatedBy != null ? Convert.ToInt32(commonInsScope.CreatedBy) : 0;
                    commonInsuranceScopeVM.ModifiedBy = commonInsScope.ModifiedBy != null ? Convert.ToInt32(commonInsScope.ModifiedBy) : 0;

                    commonInsScopeList.Add(commonInsuranceScopeVM);
                }

                return commonInsScopeList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CommonInsuranceScopeVM> GetAllCommonInsScopesByInsClass(int insClassID)
        {
            try
            {
                var commonInsScopeData = unitOfWork.TblCommonInsScopeRepository.Get(x => x.InsClassID == insClassID).ToList();

                List<CommonInsuranceScopeVM> commonInsScopeList = new List<CommonInsuranceScopeVM>();

                foreach (var commonInsScope in commonInsScopeData)
                {
                    CommonInsuranceScopeVM commonInsuranceScopeVM = new CommonInsuranceScopeVM();
                    commonInsuranceScopeVM.CommonInsuranceScopeID = commonInsScope.CommonInsScopeID;
                    commonInsuranceScopeVM.Description = commonInsScope.Description;
                    commonInsuranceScopeVM.InsuranceClassID = commonInsScope.InsClassID != null ? Convert.ToInt32(commonInsScope.InsClassID) : 0;

                    if (commonInsuranceScopeVM.InsuranceClassID > 0)
                    {
                        commonInsuranceScopeVM.InsuranceClassCode = commonInsScope.tblInsClass.Code;
                    }

                    commonInsuranceScopeVM.InsuranceSubClassID = commonInsScope.InsSubClassID != null ? Convert.ToInt32(commonInsScope.InsSubClassID) : 0;

                    if (commonInsuranceScopeVM.InsuranceSubClassID > 0)
                    {
                        commonInsuranceScopeVM.InsuranceSubClassName = commonInsScope.tblInsSubClass.Description;
                    }

                    commonInsuranceScopeVM.CreatedDate = commonInsScope.CreatedDate != null ? commonInsScope.CreatedDate.ToString() : string.Empty;
                    commonInsuranceScopeVM.ModifiedDate = commonInsScope.ModifiedDate != null ? commonInsScope.ModifiedDate.ToString() : string.Empty;
                    commonInsuranceScopeVM.CreatedBy = commonInsScope.CreatedBy != null ? Convert.ToInt32(commonInsScope.CreatedBy) : 0;
                    commonInsuranceScopeVM.ModifiedBy = commonInsScope.ModifiedBy != null ? Convert.ToInt32(commonInsScope.ModifiedBy) : 0;

                    commonInsScopeList.Add(commonInsuranceScopeVM);
                }

                return commonInsScopeList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CommonInsuranceScopeVM> GetAllCommonInsScopesByInsSubClass(int insSubClassID)
        {
            try
            {
                var commonInsScopeData = unitOfWork.TblCommonInsScopeRepository.Get(x => x.InsSubClassID == insSubClassID).ToList();

                List<CommonInsuranceScopeVM> commonInsScopeList = new List<CommonInsuranceScopeVM>();

                foreach (var commonInsScope in commonInsScopeData)
                {
                    CommonInsuranceScopeVM commonInsuranceScopeVM = new CommonInsuranceScopeVM();
                    commonInsuranceScopeVM.CommonInsuranceScopeID = commonInsScope.CommonInsScopeID;
                    commonInsuranceScopeVM.Description = commonInsScope.Description;
                    commonInsuranceScopeVM.InsuranceClassID = commonInsScope.InsClassID != null ? Convert.ToInt32(commonInsScope.InsClassID) : 0;

                    if (commonInsuranceScopeVM.InsuranceClassID > 0)
                    {
                        commonInsuranceScopeVM.InsuranceClassCode = commonInsScope.tblInsClass.Code;
                    }

                    commonInsuranceScopeVM.InsuranceSubClassID = commonInsScope.InsSubClassID != null ? Convert.ToInt32(commonInsScope.InsSubClassID) : 0;

                    if (commonInsuranceScopeVM.InsuranceSubClassID > 0)
                    {
                        commonInsuranceScopeVM.InsuranceSubClassName = commonInsScope.tblInsSubClass.Description;
                    }

                    commonInsuranceScopeVM.CreatedDate = commonInsScope.CreatedDate != null ? commonInsScope.CreatedDate.ToString() : string.Empty;
                    commonInsuranceScopeVM.ModifiedDate = commonInsScope.ModifiedDate != null ? commonInsScope.ModifiedDate.ToString() : string.Empty;
                    commonInsuranceScopeVM.CreatedBy = commonInsScope.CreatedBy != null ? Convert.ToInt32(commonInsScope.CreatedBy) : 0;
                    commonInsuranceScopeVM.ModifiedBy = commonInsScope.ModifiedBy != null ? Convert.ToInt32(commonInsScope.ModifiedBy) : 0;

                    commonInsScopeList.Add(commonInsuranceScopeVM);
                }

                return commonInsScopeList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public CommonInsuranceScopeVM GetCommonInsScopeByID(int commonInsScopeID)
        {
            try
            {
                var commonInsScopeData = unitOfWork.TblCommonInsScopeRepository.GetByID(commonInsScopeID);

                CommonInsuranceScopeVM commonInsuranceScopeVM = new CommonInsuranceScopeVM();
                commonInsuranceScopeVM.CommonInsuranceScopeID = commonInsScopeData.CommonInsScopeID;
                commonInsuranceScopeVM.Description = commonInsScopeData.Description;
                commonInsuranceScopeVM.InsuranceClassID = commonInsScopeData.InsClassID != null ? Convert.ToInt32(commonInsScopeData.InsClassID) : 0;

                if (commonInsuranceScopeVM.InsuranceClassID > 0)
                {
                    commonInsuranceScopeVM.InsuranceClassCode = commonInsScopeData.tblInsClass.Code;
                }

                commonInsuranceScopeVM.InsuranceSubClassID = commonInsScopeData.InsSubClassID != null ? Convert.ToInt32(commonInsScopeData.InsSubClassID) : 0;

                if (commonInsuranceScopeVM.InsuranceSubClassID > 0)
                {
                    commonInsuranceScopeVM.InsuranceSubClassName = commonInsScopeData.tblInsSubClass.Description;
                }

                commonInsuranceScopeVM.CreatedDate = commonInsScopeData.CreatedDate != null ? commonInsScopeData.CreatedDate.ToString() : string.Empty;
                commonInsuranceScopeVM.ModifiedDate = commonInsScopeData.ModifiedDate != null ? commonInsScopeData.ModifiedDate.ToString() : string.Empty;
                commonInsuranceScopeVM.CreatedBy = commonInsScopeData.CreatedBy != null ? Convert.ToInt32(commonInsScopeData.CreatedBy) : 0;
                commonInsuranceScopeVM.ModifiedBy = commonInsScopeData.ModifiedBy != null ? Convert.ToInt32(commonInsScopeData.ModifiedBy) : 0;

                return commonInsuranceScopeVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
