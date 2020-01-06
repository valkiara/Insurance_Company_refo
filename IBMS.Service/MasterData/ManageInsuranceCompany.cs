using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManageInsuranceCompany
    {
        private UnitOfWork unitOfWork;
        public ManageInsuranceCompany()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool SaveInsuranceCompany(InsuranceCompanyVM insuranceCompanyVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblInsCompany insCompany = new tblInsCompany();
                    insCompany.InsCompany = insuranceCompanyVM.InsuranceCompanyName;
                    insCompany.BUID = insuranceCompanyVM.BusinessUnitID;
                    insCompany.Address1 = insuranceCompanyVM.Address1;
                    insCompany.Address2 = insuranceCompanyVM.Address2;
                    insCompany.Address3 = insuranceCompanyVM.Address3;
                    insCompany.ContactPerson = insuranceCompanyVM.ContactPerson;
                    insCompany.ContactNo = insuranceCompanyVM.ContactNo;
                    insCompany.Email = insuranceCompanyVM.Email;
                    insCompany.Fax = insuranceCompanyVM.Fax;
                    insCompany.CreatedDate = DateTime.Now;
                    insCompany.CreatedBy = insuranceCompanyVM.CreatedBy;
                    unitOfWork.TblInsCompanyRepository.Insert(insCompany);
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

        public bool UpdateInsuranceCompany(InsuranceCompanyVM insuranceCompanyVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblInsCompany insCompany = unitOfWork.TblInsCompanyRepository.GetByID(insuranceCompanyVM.InsuranceCompanyID);
                    insCompany.InsCompany = insuranceCompanyVM.InsuranceCompanyName;
                    insCompany.BUID = insuranceCompanyVM.BusinessUnitID;
                    insCompany.Address1 = insuranceCompanyVM.Address1;
                    insCompany.Address2 = insuranceCompanyVM.Address2;
                    insCompany.Address3 = insuranceCompanyVM.Address3;
                    insCompany.ContactPerson = insuranceCompanyVM.ContactPerson;
                    insCompany.ContactNo = insuranceCompanyVM.ContactNo;
                    insCompany.Email = insuranceCompanyVM.Email;
                    insCompany.Fax = insuranceCompanyVM.Fax;
                    insCompany.ModifiedDate = DateTime.Now;
                    insCompany.ModifiedBy = insuranceCompanyVM.ModifiedBy;
                    unitOfWork.TblInsCompanyRepository.Update(insCompany);
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

        public bool DeleteInsuranceCompany(int insuranceCompanyID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblInsCompany insCompany = unitOfWork.TblInsCompanyRepository.GetByID(insuranceCompanyID);
                    unitOfWork.TblInsCompanyRepository.Delete(insCompany);
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

        public List<InsuranceCompanyVM> GetAllInsuranceCompanies()
        {
            try
            {
                var insuranceCompanyData = unitOfWork.TblInsCompanyRepository.Get().ToList();

                List<InsuranceCompanyVM> insuranceCompanyList = new List<InsuranceCompanyVM>();

                foreach (var insuranceCompany in insuranceCompanyData)
                {
                    InsuranceCompanyVM insuranceCompanyVM = new InsuranceCompanyVM();
                    insuranceCompanyVM.InsuranceCompanyID = insuranceCompany.InsCompanyID;
                    insuranceCompanyVM.BusinessUnitID = insuranceCompany.BUID != null ? Convert.ToInt32(insuranceCompany.BUID) : 0;

                    if (insuranceCompanyVM.BusinessUnitID > 0)
                    {
                        insuranceCompanyVM.BusinessUnitName = insuranceCompany.tblBussinessUnit.BussinessUnit;
                    }

                    insuranceCompanyVM.InsuranceCompanyName = insuranceCompany.InsCompany;
                    insuranceCompanyVM.Address1 = insuranceCompany.Address1;
                    insuranceCompanyVM.Address2 = insuranceCompany.Address2;
                    insuranceCompanyVM.Address3 = insuranceCompany.Address3;
                    insuranceCompanyVM.ContactPerson = insuranceCompany.ContactPerson;
                    insuranceCompanyVM.ContactNo = insuranceCompany.ContactNo;
                    insuranceCompanyVM.Email = insuranceCompany.Email;
                    insuranceCompanyVM.Fax = insuranceCompany.Fax;
                    insuranceCompanyVM.CreatedDate = insuranceCompany.CreatedDate != null ? insuranceCompany.CreatedDate.ToString() : string.Empty;
                    insuranceCompanyVM.ModifiedDate = insuranceCompany.ModifiedDate != null ? insuranceCompany.ModifiedDate.ToString() : string.Empty;
                    insuranceCompanyVM.CreatedBy = insuranceCompany.CreatedBy != null ? Convert.ToInt32(insuranceCompany.CreatedBy) : 0;
                    insuranceCompanyVM.ModifiedBy = insuranceCompany.ModifiedBy != null ? Convert.ToInt32(insuranceCompany.ModifiedBy) : 0;

                    insuranceCompanyList.Add(insuranceCompanyVM);
                }

                return insuranceCompanyList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<InsuranceCompanyVM> GetInsuranceCompaniesByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var insuranceCompanyData = unitOfWork.TblInsCompanyRepository.Get(x => x.BUID == businessUnitID).ToList();

                List<InsuranceCompanyVM> insuranceCompanyList = new List<InsuranceCompanyVM>();

                foreach (var insuranceCompany in insuranceCompanyData)
                {
                    InsuranceCompanyVM insuranceCompanyVM = new InsuranceCompanyVM();
                    insuranceCompanyVM.InsuranceCompanyID = insuranceCompany.InsCompanyID;
                    insuranceCompanyVM.BusinessUnitID = insuranceCompany.BUID != null ? Convert.ToInt32(insuranceCompany.BUID) : 0;

                    if (insuranceCompanyVM.BusinessUnitID > 0)
                    {
                        insuranceCompanyVM.BusinessUnitName = insuranceCompany.tblBussinessUnit.BussinessUnit;
                    }

                    insuranceCompanyVM.InsuranceCompanyName = insuranceCompany.InsCompany;
                    insuranceCompanyVM.Address1 = insuranceCompany.Address1;
                    insuranceCompanyVM.Address2 = insuranceCompany.Address2;
                    insuranceCompanyVM.Address3 = insuranceCompany.Address3;
                    insuranceCompanyVM.ContactPerson = insuranceCompany.ContactPerson;
                    insuranceCompanyVM.ContactNo = insuranceCompany.ContactNo;
                    insuranceCompanyVM.Email = insuranceCompany.Email;
                    insuranceCompanyVM.Fax = insuranceCompany.Fax;
                    insuranceCompanyVM.CreatedDate = insuranceCompany.CreatedDate != null ? insuranceCompany.CreatedDate.ToString() : string.Empty;
                    insuranceCompanyVM.ModifiedDate = insuranceCompany.ModifiedDate != null ? insuranceCompany.ModifiedDate.ToString() : string.Empty;
                    insuranceCompanyVM.CreatedBy = insuranceCompany.CreatedBy != null ? Convert.ToInt32(insuranceCompany.CreatedBy) : 0;
                    insuranceCompanyVM.ModifiedBy = insuranceCompany.ModifiedBy != null ? Convert.ToInt32(insuranceCompany.ModifiedBy) : 0;

                    insuranceCompanyList.Add(insuranceCompanyVM);
                }

                return insuranceCompanyList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public InsuranceCompanyVM GetInsuranceCompanyByID(int insuranceCompanyID)
        {
            try
            {
                var insuranceCompanyData = unitOfWork.TblInsCompanyRepository.GetByID(insuranceCompanyID);

                InsuranceCompanyVM insuranceCompanyVM = new InsuranceCompanyVM();
                insuranceCompanyVM.InsuranceCompanyID = insuranceCompanyData.InsCompanyID;
                insuranceCompanyVM.BusinessUnitID = insuranceCompanyData.BUID != null ? Convert.ToInt32(insuranceCompanyData.BUID) : 0;

                if (insuranceCompanyVM.BusinessUnitID > 0)
                {
                    insuranceCompanyVM.BusinessUnitName = insuranceCompanyData.tblBussinessUnit.BussinessUnit;
                }

                insuranceCompanyVM.InsuranceCompanyName = insuranceCompanyData.InsCompany;
                insuranceCompanyVM.Address1 = insuranceCompanyData.Address1;
                insuranceCompanyVM.Address2 = insuranceCompanyData.Address2;
                insuranceCompanyVM.Address3 = insuranceCompanyData.Address3;
                insuranceCompanyVM.ContactPerson = insuranceCompanyData.ContactPerson;
                insuranceCompanyVM.ContactNo = insuranceCompanyData.ContactNo;
                insuranceCompanyVM.Email = insuranceCompanyData.Email;
                insuranceCompanyVM.Fax = insuranceCompanyData.Fax;
                insuranceCompanyVM.CreatedDate = insuranceCompanyData.CreatedDate != null ? insuranceCompanyData.CreatedDate.ToString() : string.Empty;
                insuranceCompanyVM.ModifiedDate = insuranceCompanyData.ModifiedDate != null ? insuranceCompanyData.ModifiedDate.ToString() : string.Empty;
                insuranceCompanyVM.CreatedBy = insuranceCompanyData.CreatedBy != null ? Convert.ToInt32(insuranceCompanyData.CreatedBy) : 0;
                insuranceCompanyVM.ModifiedBy = insuranceCompanyData.ModifiedBy != null ? Convert.ToInt32(insuranceCompanyData.ModifiedBy) : 0;

                return insuranceCompanyVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsInsCompanyAvailable(int? insCompanyID, string insCompanyName)
        {
            try
            {
                if (insCompanyID != null && unitOfWork.TblInsCompanyRepository.Get().Any(x => x.InsCompany.ToLower() == insCompanyName.ToLower() && x.InsCompanyID != insCompanyID))
                {
                    return true;
                }
                else if (insCompanyID == null && unitOfWork.TblInsCompanyRepository.Get().Any(x => x.InsCompany.ToLower() == insCompanyName.ToLower()))
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
