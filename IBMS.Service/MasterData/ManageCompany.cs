using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManageCompany
    {
        private UnitOfWork unitOfWork;
        public ManageCompany()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool SaveCompany(CompanyVM companyVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCompany company = new tblCompany();
                    company.CompanyName = companyVM.CompanyName;
                    company.IsActive = companyVM.IsActive;
                    company.CreatedDate = DateTime.Now;
                    company.CreatedBy = companyVM.CreatedBy;
                    unitOfWork.TblCompanyRepository.Insert(company);
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

        public bool UpdateCompany(CompanyVM companyVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCompany company = unitOfWork.TblCompanyRepository.GetByID(companyVM.CompanyID);
                    company.CompanyName = companyVM.CompanyName;
                    company.IsActive = companyVM.IsActive;
                    company.ModifiedDate = DateTime.Now;
                    company.ModifiedBy = companyVM.ModifiedBy;
                    unitOfWork.TblCompanyRepository.Update(company);
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

        public bool DeleteCompany(int companyID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCompany company = unitOfWork.TblCompanyRepository.GetByID(companyID);
                    unitOfWork.TblCompanyRepository.Delete(company);
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

        public List<CompanyVM> GetAllCompanies()
        {
            try
            {
                var companyData = unitOfWork.TblCompanyRepository.Get().ToList();

                List<CompanyVM> companyList = new List<CompanyVM>();

                foreach (var company in companyData)
                {
                    CompanyVM companyVM = new CompanyVM();
                    companyVM.CompanyID = company.CompID;
                    companyVM.CompanyName = company.CompanyName;
                    companyVM.IsActive = company.IsActive;
                    companyVM.CreatedDate = company.CreatedDate != null ? company.CreatedDate.ToString() : string.Empty;
                    companyVM.ModifiedDate = company.ModifiedDate != null ? company.ModifiedDate.ToString() : string.Empty;
                    companyVM.CreatedBy = company.CreatedBy != null ? Convert.ToInt32(company.CreatedBy) : 0;
                    companyVM.ModifiedBy = company.ModifiedBy != null ? Convert.ToInt32(company.ModifiedBy) : 0;

                    companyList.Add(companyVM);
                }

                return companyList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public CompanyVM GetCompanyByID(int companyID)
        {
            try
            {
                var companyData = unitOfWork.TblCompanyRepository.GetByID(companyID);

                CompanyVM companyVM = new CompanyVM();
                companyVM.CompanyID = companyData.CompID;
                companyVM.CompanyName = companyData.CompanyName;
                companyVM.IsActive = companyData.IsActive;
                companyVM.CreatedDate = companyData.CreatedDate != null ? companyData.CreatedDate.ToString() : string.Empty;
                companyVM.ModifiedDate = companyData.ModifiedDate != null ? companyData.ModifiedDate.ToString() : string.Empty;
                companyVM.CreatedBy = companyData.CreatedBy != null ? Convert.ToInt32(companyData.CreatedBy) : 0;
                companyVM.ModifiedBy = companyData.ModifiedBy != null ? Convert.ToInt32(companyData.ModifiedBy) : 0;

                return companyVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsCompanyAvailable(int? companyID, string companyName)
        {
            try
            {
                if (companyID != null && unitOfWork.TblCompanyRepository.Get().Any(x => x.CompanyName.ToLower() == companyName.ToLower() && x.CompID != companyID))
                {
                    return true;
                }
                else if (companyID == null && unitOfWork.TblCompanyRepository.Get().Any(x => x.CompanyName.ToLower() == companyName.ToLower()))
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
