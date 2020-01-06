using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManageBusinessUnit
    {
        private UnitOfWork unitOfWork;
        public ManageBusinessUnit()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool SaveBusinessUnit(BusinessUnitVM businessUnitVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblBussinessUnit bussinessUnit = new tblBussinessUnit();
                    bussinessUnit.BussinessUnit = businessUnitVM.BusinessUnit;
                    bussinessUnit.CompID = businessUnitVM.CompanyID;
                    bussinessUnit.IsActive = businessUnitVM.IsActive;
                    bussinessUnit.CreatedDate = DateTime.Now;
                    bussinessUnit.CreatedBy = businessUnitVM.CreatedBy;
                    unitOfWork.TblBussinessUnitRepository.Insert(bussinessUnit);
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

        public bool UpdateBusinessUnit(BusinessUnitVM businessUnitVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblBussinessUnit bussinessUnit = unitOfWork.TblBussinessUnitRepository.GetByID(businessUnitVM.BusinessUnitID);
                    bussinessUnit.BussinessUnit = businessUnitVM.BusinessUnit;
                    bussinessUnit.CompID = businessUnitVM.CompanyID;
                    bussinessUnit.IsActive = businessUnitVM.IsActive;
                    bussinessUnit.ModifiedDate = DateTime.Now;
                    bussinessUnit.ModifiedBy = businessUnitVM.ModifiedBy;
                    unitOfWork.TblBussinessUnitRepository.Update(bussinessUnit);
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

        public bool DeleteBusinessUnit(int businessUnitID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblBussinessUnit bussinessUnit = unitOfWork.TblBussinessUnitRepository.GetByID(businessUnitID);
                    unitOfWork.TblBussinessUnitRepository.Delete(businessUnitID);
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

        public List<BusinessUnitVM> GetAllBusinessUnits()
        {
            try
            {
                var businessUnitData = unitOfWork.TblBussinessUnitRepository.Get().ToList();

                List<BusinessUnitVM> businessUnitList = new List<BusinessUnitVM>();

                foreach (var businessUnit in businessUnitData)
                {
                    BusinessUnitVM businessUnitVM = new BusinessUnitVM();
                    businessUnitVM.BusinessUnitID = businessUnit.BUID;
                    businessUnitVM.BusinessUnit = businessUnit.BussinessUnit;
                    businessUnitVM.CompanyID = businessUnit.CompID != null ? Convert.ToInt32(businessUnit.CompID) : 0;

                    if (businessUnitVM.CompanyID > 0)
                    {
                        businessUnitVM.CompanyName = businessUnit.tblCompany.CompanyName;
                    }

                    businessUnitVM.IsActive = businessUnit.IsActive;
                    businessUnitVM.CreatedDate = businessUnit.CreatedDate != null ? businessUnit.CreatedDate.ToString() : string.Empty;
                    businessUnitVM.ModifiedDate = businessUnit.ModifiedDate != null ? businessUnit.ModifiedDate.ToString() : string.Empty;
                    businessUnitVM.CreatedBy = businessUnit.CreatedBy != null ? Convert.ToInt32(businessUnit.CreatedBy) : 0;
                    businessUnitVM.ModifiedBy = businessUnit.ModifiedBy != null ? Convert.ToInt32(businessUnit.ModifiedBy) : 0;

                    businessUnitList.Add(businessUnitVM);
                }

                return businessUnitList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<BusinessUnitVM> GetBusinessUnitsByCompanyID(int companyID)
        {
            try
            {
                var businessUnitData = unitOfWork.TblBussinessUnitRepository.Get(x => x.CompID == companyID).ToList();

                List<BusinessUnitVM> businessUnitList = new List<BusinessUnitVM>();

                foreach (var businessUnit in businessUnitData)
                {
                    BusinessUnitVM businessUnitVM = new BusinessUnitVM();
                    businessUnitVM.BusinessUnitID = businessUnit.BUID;
                    businessUnitVM.BusinessUnit = businessUnit.BussinessUnit;
                    businessUnitVM.CompanyID = businessUnit.CompID != null ? Convert.ToInt32(businessUnit.CompID) : 0;

                    if (businessUnitVM.CompanyID > 0)
                    {
                        businessUnitVM.CompanyName = businessUnit.tblCompany.CompanyName;
                    }

                    businessUnitVM.IsActive = businessUnit.IsActive;
                    businessUnitVM.CreatedDate = businessUnit.CreatedDate != null ? businessUnit.CreatedDate.ToString() : string.Empty;
                    businessUnitVM.ModifiedDate = businessUnit.ModifiedDate != null ? businessUnit.ModifiedDate.ToString() : string.Empty;
                    businessUnitVM.CreatedBy = businessUnit.CreatedBy != null ? Convert.ToInt32(businessUnit.CreatedBy) : 0;
                    businessUnitVM.ModifiedBy = businessUnit.ModifiedBy != null ? Convert.ToInt32(businessUnit.ModifiedBy) : 0;

                    businessUnitList.Add(businessUnitVM);
                }

                return businessUnitList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public BusinessUnitVM GetBusinessUnitByID(int businessUnitID)
        {
            try
            {
                var businessUnitData = unitOfWork.TblBussinessUnitRepository.GetByID(businessUnitID);

                BusinessUnitVM businessUnitVM = new BusinessUnitVM();
                businessUnitVM.BusinessUnitID = businessUnitData.BUID;
                businessUnitVM.BusinessUnit = businessUnitData.BussinessUnit;
                businessUnitVM.CompanyID = businessUnitData.CompID != null ? Convert.ToInt32(businessUnitData.CompID) : 0;

                if (businessUnitVM.CompanyID > 0)
                {
                    businessUnitVM.CompanyName = businessUnitData.tblCompany.CompanyName;
                }

                businessUnitVM.IsActive = businessUnitData.IsActive;
                businessUnitVM.CreatedDate = businessUnitData.CreatedDate != null ? businessUnitData.CreatedDate.ToString() : string.Empty;
                businessUnitVM.ModifiedDate = businessUnitData.ModifiedDate != null ? businessUnitData.ModifiedDate.ToString() : string.Empty;
                businessUnitVM.CreatedBy = businessUnitData.CreatedBy != null ? Convert.ToInt32(businessUnitData.CreatedBy) : 0;
                businessUnitVM.ModifiedBy = businessUnitData.ModifiedBy != null ? Convert.ToInt32(businessUnitData.ModifiedBy) : 0;

                return businessUnitVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsBusinessUnitAvailable(int? businessUnitID, string businessUnitName)
        {
            try
            {
                if (businessUnitID != null && unitOfWork.TblBussinessUnitRepository.Get().Any(x => x.BussinessUnit.ToLower() == businessUnitName.ToLower() && x.BUID != businessUnitID))
                {
                    return true;
                }
                else if (businessUnitID == null && unitOfWork.TblBussinessUnitRepository.Get().Any(x => x.BussinessUnit.ToLower() == businessUnitName.ToLower()))
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
