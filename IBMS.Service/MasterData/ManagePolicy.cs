using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManagePolicy
    {
        private UnitOfWork unitOfWork;
        public ManagePolicy()
        {
            unitOfWork = new UnitOfWork();
        }

        #region Policy Category
        public bool SavePolicyCategory(PolicyCategoryVM policyCategoryVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblPolicyCategory policyCategory = new tblPolicyCategory();
                    policyCategory.CategoryName = policyCategoryVM.PolicyCategoryName;
                    policyCategory.CreatedDate = DateTime.Now;
                    policyCategory.CreatedBy = policyCategoryVM.CreatedBy;
                    unitOfWork.TblPolicyCategoryRepository.Insert(policyCategory);
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

        public bool UpdatePolicyCategory(PolicyCategoryVM policyCategoryVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblPolicyCategory policyCategory = unitOfWork.TblPolicyCategoryRepository.GetByID(policyCategoryVM.PolicyCategoryID);
                    policyCategory.CategoryName = policyCategoryVM.PolicyCategoryName;
                    policyCategory.ModifiedDate = DateTime.Now;
                    policyCategory.ModifiedBy = policyCategoryVM.ModifiedBy;
                    unitOfWork.TblPolicyCategoryRepository.Update(policyCategory);
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

        public bool DeletePolicyCategory(int policyCategoryID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblPolicyCategory policyCategory = unitOfWork.TblPolicyCategoryRepository.GetByID(policyCategoryID);
                    unitOfWork.TblPolicyCategoryRepository.Delete(policyCategory);
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

        public List<PolicyCategoryVM> GetAllPolicyCategories()
        {
            try
            {
                var policyCategoryData = unitOfWork.TblPolicyCategoryRepository.Get().ToList();

                List<PolicyCategoryVM> policyCategoryList = new List<PolicyCategoryVM>();

                foreach (var policyCategory in policyCategoryData)
                {
                    PolicyCategoryVM policyCategoryVM = new PolicyCategoryVM();
                    policyCategoryVM.PolicyCategoryID = policyCategory.PolicyCategoryID;
                    policyCategoryVM.PolicyCategoryName = policyCategory.CategoryName;
                    policyCategoryVM.CreatedDate = policyCategory.CreatedDate != null ? policyCategory.CreatedDate.ToString() : string.Empty;
                    policyCategoryVM.ModifiedDate = policyCategory.ModifiedDate != null ? policyCategory.ModifiedDate.ToString() : string.Empty;
                    policyCategoryVM.CreatedBy = policyCategory.CreatedBy != null ? Convert.ToInt32(policyCategory.CreatedBy) : 0;
                    policyCategoryVM.ModifiedBy = policyCategory.ModifiedBy != null ? Convert.ToInt32(policyCategory.ModifiedBy) : 0;

                    policyCategoryList.Add(policyCategoryVM);
                }

                return policyCategoryList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PolicyCategoryVM GetPolicyCategoryByID(int policyCategoryID)
        {
            try
            {
                var policyCategoryData = unitOfWork.TblPolicyCategoryRepository.GetByID(policyCategoryID);

                PolicyCategoryVM policyCategoryVM = new PolicyCategoryVM();
                policyCategoryVM.PolicyCategoryID = policyCategoryData.PolicyCategoryID;
                policyCategoryVM.PolicyCategoryName = policyCategoryData.CategoryName;
                policyCategoryVM.CreatedDate = policyCategoryData.CreatedDate != null ? policyCategoryData.CreatedDate.ToString() : string.Empty;
                policyCategoryVM.ModifiedDate = policyCategoryData.ModifiedDate != null ? policyCategoryData.ModifiedDate.ToString() : string.Empty;
                policyCategoryVM.CreatedBy = policyCategoryData.CreatedBy != null ? Convert.ToInt32(policyCategoryData.CreatedBy) : 0;
                policyCategoryVM.ModifiedBy = policyCategoryData.ModifiedBy != null ? Convert.ToInt32(policyCategoryData.ModifiedBy) : 0;

                return policyCategoryVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsPolicyCategoryAvailable(int? policyCategoryID, string policyCategoryName)
        {
            try
            {
                if (policyCategoryID != null && unitOfWork.TblPolicyCategoryRepository.Get().Any(x => x.CategoryName.ToLower() == policyCategoryName.ToLower() && x.PolicyCategoryID != policyCategoryID))
                {
                    return true;
                }
                else if (policyCategoryID == null && unitOfWork.TblPolicyCategoryRepository.Get().Any(x => x.CategoryName.ToLower() == policyCategoryName.ToLower()))
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
        #endregion

        #region Policy
        public bool SavePolicy(PolicyVM policyVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblPolicy policy = new tblPolicy();
                    policy.PolicyName = policyVM.PolicyName;
                    policy.Rate = policyVM.Rate;
                    policy.PolicyCategoryID = policyVM.PolicyCategoryID;
                    policy.BUID = policyVM.BusinessUnitID;
                    policy.CreatedDate = DateTime.Now;
                    policy.CreatedBy = policyVM.CreatedBy;
                    unitOfWork.TblPolicyRepository.Insert(policy);
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

        public bool UpdatePolicy(PolicyVM policyVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblPolicy policy = unitOfWork.TblPolicyRepository.GetByID(policyVM.PolicyID);
                    policy.PolicyName = policyVM.PolicyName;
                    policy.Rate = policyVM.Rate;
                    policy.PolicyCategoryID = policyVM.PolicyCategoryID;
                    policy.BUID = policyVM.BusinessUnitID;
                    policy.ModifiedDate = DateTime.Now;
                    policy.ModifiedBy = policyVM.ModifiedBy;
                    unitOfWork.TblPolicyRepository.Update(policy);
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

        public bool DeletePolicy(int policyID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblPolicy policy = unitOfWork.TblPolicyRepository.GetByID(policyID);
                    unitOfWork.TblPolicyRepository.Delete(policy);
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

        public List<PolicyVM> GetAllPolicies()
        {
            try
            {
                var policyData = unitOfWork.TblPolicyRepository.Get().ToList();

                List<PolicyVM> policyList = new List<PolicyVM>();

                foreach (var policy in policyData)
                {
                    PolicyVM policyVM = new PolicyVM();
                    policyVM.PolicyID = policy.PolicyID;
                    policyVM.PolicyName = policy.PolicyName;
                    policyVM.Rate = policy.Rate != null ? Convert.ToDouble(policy.Rate) : 0;
                    policyVM.PolicyCategoryID = policy.PolicyCategoryID != null ? Convert.ToInt32(policy.PolicyCategoryID) : 0;

                    if (policyVM.PolicyCategoryID > 0)
                    {
                        policyVM.PolicyCategoryName = policy.tblPolicyCategory.CategoryName;
                    }

                    policyVM.BusinessUnitID = policy.BUID != null ? Convert.ToInt32(policy.BUID) : 0;

                    if (policyVM.BusinessUnitID > 0)
                    {
                        policyVM.BusinessUnitName = policy.tblBussinessUnit.BussinessUnit;
                    }

                    policyVM.CreatedDate = policy.CreatedDate != null ? policy.CreatedDate.ToString() : string.Empty;
                    policyVM.ModifiedDate = policy.ModifiedDate != null ? policy.ModifiedDate.ToString() : string.Empty;
                    policyVM.CreatedBy = policy.CreatedBy != null ? Convert.ToInt32(policy.CreatedBy) : 0;
                    policyVM.ModifiedBy = policy.ModifiedBy != null ? Convert.ToInt32(policy.ModifiedBy) : 0;

                    policyList.Add(policyVM);
                }

                return policyList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<PolicyVM> GetAllPoliciesByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var policyData = unitOfWork.TblPolicyRepository.Get(x => x.BUID == businessUnitID).ToList();

                List<PolicyVM> policyList = new List<PolicyVM>();

                foreach (var policy in policyData)
                {
                    PolicyVM policyVM = new PolicyVM();
                    policyVM.PolicyID = policy.PolicyID;
                    policyVM.PolicyName = policy.PolicyName;
                    policyVM.Rate = policy.Rate != null ? Convert.ToDouble(policy.Rate) : 0;
                    policyVM.PolicyCategoryID = policy.PolicyCategoryID != null ? Convert.ToInt32(policy.PolicyCategoryID) : 0;

                    if (policyVM.PolicyCategoryID > 0)
                    {
                        policyVM.PolicyCategoryName = policy.tblPolicyCategory.CategoryName;
                    }

                    policyVM.BusinessUnitID = policy.BUID != null ? Convert.ToInt32(policy.BUID) : 0;

                    if (policyVM.BusinessUnitID > 0)
                    {
                        policyVM.BusinessUnitName = policy.tblBussinessUnit.BussinessUnit;
                    }

                    policyVM.CreatedDate = policy.CreatedDate != null ? policy.CreatedDate.ToString() : string.Empty;
                    policyVM.ModifiedDate = policy.ModifiedDate != null ? policy.ModifiedDate.ToString() : string.Empty;
                    policyVM.CreatedBy = policy.CreatedBy != null ? Convert.ToInt32(policy.CreatedBy) : 0;
                    policyVM.ModifiedBy = policy.ModifiedBy != null ? Convert.ToInt32(policy.ModifiedBy) : 0;

                    policyList.Add(policyVM);
                }

                return policyList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PolicyVM GetPolicyByID(int policyID)
        {
            try
            {
                var policyData = unitOfWork.TblPolicyRepository.GetByID(policyID);

                PolicyVM policyVM = new PolicyVM();
                policyVM.PolicyID = policyData.PolicyID;
                policyVM.PolicyName = policyData.PolicyName;
                policyVM.Rate = policyData.Rate != null ? Convert.ToDouble(policyData.Rate) : 0;
                policyVM.PolicyCategoryID = policyData.PolicyCategoryID != null ? Convert.ToInt32(policyData.PolicyCategoryID) : 0;

                if (policyVM.PolicyCategoryID > 0)
                {
                    policyVM.PolicyCategoryName = policyData.tblPolicyCategory.CategoryName;
                }

                policyVM.BusinessUnitID = policyData.BUID != null ? Convert.ToInt32(policyData.BUID) : 0;

                if (policyVM.BusinessUnitID > 0)
                {
                    policyVM.BusinessUnitName = policyData.tblBussinessUnit.BussinessUnit;
                }

                policyVM.CreatedDate = policyData.CreatedDate != null ? policyData.CreatedDate.ToString() : string.Empty;
                policyVM.ModifiedDate = policyData.ModifiedDate != null ? policyData.ModifiedDate.ToString() : string.Empty;
                policyVM.CreatedBy = policyData.CreatedBy != null ? Convert.ToInt32(policyData.CreatedBy) : 0;
                policyVM.ModifiedBy = policyData.ModifiedBy != null ? Convert.ToInt32(policyData.ModifiedBy) : 0;

                return policyVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsPolicyAvailable(int? policyID, string policyName)
        {
            try
            {
                if (policyID != null && unitOfWork.TblPolicyRepository.Get().Any(x => x.PolicyName.ToLower() == policyName.ToLower() && x.PolicyID != policyID))
                {
                    return true;
                }
                else if (policyID == null && unitOfWork.TblPolicyRepository.Get().Any(x => x.PolicyName.ToLower() == policyName.ToLower()))
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
        #endregion
    }
}
