using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManageInsuranceClass
    {
        private UnitOfWork unitOfWork;
        public ManageInsuranceClass()
        {
            unitOfWork = new UnitOfWork();
        }

        #region Insurance Class
        public bool SaveInsuranceClass(InsuranceClassVM insuranceClassVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblInsClass insClass = new tblInsClass();
                    insClass.BUID = insuranceClassVM.BusinessUnitID;
                    insClass.Code = insuranceClassVM.InsuranceCode;
                    insClass.Description = insuranceClassVM.Description;
                    insClass.IsActive = insuranceClassVM.IsActive;
                    insClass.CreatedDate = DateTime.Now;
                    insClass.CreatedBy = insuranceClassVM.CreatedBy;
                    unitOfWork.TblInsClassRepository.Insert(insClass);
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

        public bool UpdateInsuranceClass(InsuranceClassVM insuranceClassVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblInsClass insClass = unitOfWork.TblInsClassRepository.GetByID(insuranceClassVM.InsuranceClassID);
                    insClass.BUID = insuranceClassVM.BusinessUnitID;
                    insClass.Code = insuranceClassVM.InsuranceCode;
                    insClass.Description = insuranceClassVM.Description;
                    insClass.IsActive = insuranceClassVM.IsActive;
                    insClass.ModifiedDate = DateTime.Now;
                    insClass.ModifiedBy = insuranceClassVM.ModifiedBy;
                    unitOfWork.TblInsClassRepository.Update(insClass);
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

        public bool DeleteInsuranceClass(int insuranceClassID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblInsClass insClass = unitOfWork.TblInsClassRepository.GetByID(insuranceClassID);
                    unitOfWork.TblInsClassRepository.Delete(insClass);
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

        public List<InsuranceClassVM> GetAllInsuranceClasses()
        {
            try
            {
                var insuranceClassData = unitOfWork.TblInsClassRepository.Get().ToList();

                List<InsuranceClassVM> insuranceClassList = new List<InsuranceClassVM>();

                foreach (var insuranceClass in insuranceClassData)
                {
                    InsuranceClassVM insuranceClassVM = new InsuranceClassVM();
                    insuranceClassVM.InsuranceClassID = insuranceClass.InsClassID;
                    insuranceClassVM.BusinessUnitID = insuranceClass.BUID != null ? Convert.ToInt32(insuranceClass.BUID) : 0;

                    if (insuranceClassVM.BusinessUnitID > 0)
                    {
                        insuranceClassVM.BusinessUnitName = insuranceClass.tblBussinessUnit.BussinessUnit;
                    }

                    insuranceClassVM.InsuranceCode = insuranceClass.Code;
                    insuranceClassVM.Description = insuranceClass.Description;
                    insuranceClassVM.IsActive = insuranceClass.IsActive;
                    insuranceClassVM.CreatedDate = insuranceClass.CreatedDate != null ? insuranceClass.CreatedDate.ToString() : string.Empty;
                    insuranceClassVM.ModifiedDate = insuranceClass.ModifiedDate != null ? insuranceClass.ModifiedDate.ToString() : string.Empty;
                    insuranceClassVM.CreatedBy = insuranceClass.CreatedBy != null ? Convert.ToInt32(insuranceClass.CreatedBy) : 0;
                    insuranceClassVM.ModifiedBy = insuranceClass.ModifiedBy != null ? Convert.ToInt32(insuranceClass.ModifiedBy) : 0;

                    insuranceClassList.Add(insuranceClassVM);
                }

                return insuranceClassList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<InsuranceClassVM> GetInsuranceClassesByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var insuranceClassData = unitOfWork.TblInsClassRepository.Get(x => x.BUID == businessUnitID).ToList();

                List<InsuranceClassVM> insuranceClassList = new List<InsuranceClassVM>();

                foreach (var insuranceClass in insuranceClassData)
                {
                    InsuranceClassVM insuranceClassVM = new InsuranceClassVM();
                    insuranceClassVM.InsuranceClassID = insuranceClass.InsClassID;
                    insuranceClassVM.BusinessUnitID = insuranceClass.BUID != null ? Convert.ToInt32(insuranceClass.BUID) : 0;

                    if (insuranceClassVM.BusinessUnitID > 0)
                    {
                        insuranceClassVM.BusinessUnitName = insuranceClass.tblBussinessUnit.BussinessUnit;
                    }

                    insuranceClassVM.InsuranceCode = insuranceClass.Code;
                    insuranceClassVM.Description = insuranceClass.Description;
                    insuranceClassVM.IsActive = insuranceClass.IsActive;
                    insuranceClassVM.CreatedDate = insuranceClass.CreatedDate != null ? insuranceClass.CreatedDate.ToString() : string.Empty;
                    insuranceClassVM.ModifiedDate = insuranceClass.ModifiedDate != null ? insuranceClass.ModifiedDate.ToString() : string.Empty;
                    insuranceClassVM.CreatedBy = insuranceClass.CreatedBy != null ? Convert.ToInt32(insuranceClass.CreatedBy) : 0;
                    insuranceClassVM.ModifiedBy = insuranceClass.ModifiedBy != null ? Convert.ToInt32(insuranceClass.ModifiedBy) : 0;

                    insuranceClassList.Add(insuranceClassVM);
                }

                return insuranceClassList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public InsuranceClassVM GetInsuranceClassByID(int insuranceClassID)
        {
            try
            {
                var insuranceClassData = unitOfWork.TblInsClassRepository.GetByID(insuranceClassID);

                InsuranceClassVM insuranceClassVM = new InsuranceClassVM();
                insuranceClassVM.InsuranceClassID = insuranceClassData.InsClassID;
                insuranceClassVM.BusinessUnitID = insuranceClassData.BUID != null ? Convert.ToInt32(insuranceClassData.BUID) : 0;

                if (insuranceClassVM.BusinessUnitID > 0)
                {
                    insuranceClassVM.BusinessUnitName = insuranceClassData.tblBussinessUnit.BussinessUnit;
                }

                insuranceClassVM.InsuranceCode = insuranceClassData.Code;
                insuranceClassVM.Description = insuranceClassData.Description;
                insuranceClassVM.IsActive = insuranceClassData.IsActive;
                insuranceClassVM.CreatedDate = insuranceClassData.CreatedDate != null ? insuranceClassData.CreatedDate.ToString() : string.Empty;
                insuranceClassVM.ModifiedDate = insuranceClassData.ModifiedDate != null ? insuranceClassData.ModifiedDate.ToString() : string.Empty;
                insuranceClassVM.CreatedBy = insuranceClassData.CreatedBy != null ? Convert.ToInt32(insuranceClassData.CreatedBy) : 0;
                insuranceClassVM.ModifiedBy = insuranceClassData.ModifiedBy != null ? Convert.ToInt32(insuranceClassData.ModifiedBy) : 0;

                return insuranceClassVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsInsClassAvailable(int? insClassID, string insClassCode)
        {
            try
            {
                if (insClassID != null && unitOfWork.TblInsClassRepository.Get().Any(x => x.Code.ToLower() == insClassCode.ToLower() && x.InsClassID != insClassID))
                {
                    return true;
                }
                else if (insClassID == null && unitOfWork.TblInsClassRepository.Get().Any(x => x.Code.ToLower() == insClassCode.ToLower()))
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

        #region Insurance Sub Class
        public bool SaveInsuranceSubClass(InsuranceSubClassVM insuranceSubClassVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblInsSubClass insSubClass = new tblInsSubClass();
                    insSubClass.InsClassID = insuranceSubClassVM.InsuranceClassID;
                    insSubClass.Description = insuranceSubClassVM.Description;
                    insSubClass.IsActive = insuranceSubClassVM.IsActive;
                    insSubClass.CreatedDate = DateTime.Now;
                    insSubClass.CreatedBy = insuranceSubClassVM.CreatedBy;
                    unitOfWork.TblInsSubClassRepository.Insert(insSubClass);
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

        public bool UpdateInsuranceSubClass(InsuranceSubClassVM insuranceSubClassVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblInsSubClass insSubClass = unitOfWork.TblInsSubClassRepository.GetByID(insuranceSubClassVM.InsuranceSubClassID);
                    insSubClass.InsClassID = insuranceSubClassVM.InsuranceClassID;
                    insSubClass.Description = insuranceSubClassVM.Description;
                    insSubClass.IsActive = insuranceSubClassVM.IsActive;
                    insSubClass.ModifiedDate = DateTime.Now;
                    insSubClass.ModifiedBy = insuranceSubClassVM.ModifiedBy;
                    unitOfWork.TblInsSubClassRepository.Update(insSubClass);
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

        public bool DeleteInsuranceSubClass(int insuranceSubClassID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblInsSubClass insSubClass = unitOfWork.TblInsSubClassRepository.GetByID(insuranceSubClassID);
                    unitOfWork.TblInsSubClassRepository.Delete(insSubClass);
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

        public List<InsuranceSubClassVM> GetAllInsuranceSubClasses()
        {
            try
            {
                var insuranceSubClassData = unitOfWork.TblInsSubClassRepository.Get().ToList();

                List<InsuranceSubClassVM> insuranceSubClassList = new List<InsuranceSubClassVM>();

                foreach (var insuranceSubClass in insuranceSubClassData)
                {
                    InsuranceSubClassVM insuranceSubClassVM = new InsuranceSubClassVM();
                    insuranceSubClassVM.InsuranceSubClassID = insuranceSubClass.InsSubClassID;
                    insuranceSubClassVM.InsuranceClassID = insuranceSubClass.InsClassID != null ? Convert.ToInt32(insuranceSubClass.InsClassID) : 0;

                    if (insuranceSubClassVM.InsuranceClassID > 0)
                    {
                        insuranceSubClassVM.InsuranceClassCode = insuranceSubClass.tblInsClass.Code;
                    }

                    insuranceSubClassVM.Description = insuranceSubClass.Description;
                    insuranceSubClassVM.IsActive = insuranceSubClass.IsActive;
                    insuranceSubClassVM.CreatedDate = insuranceSubClass.CreatedDate != null ? insuranceSubClass.CreatedDate.ToString() : string.Empty;
                    insuranceSubClassVM.ModifiedDate = insuranceSubClass.ModifiedDate != null ? insuranceSubClass.ModifiedDate.ToString() : string.Empty;
                    insuranceSubClassVM.CreatedBy = insuranceSubClass.CreatedBy != null ? Convert.ToInt32(insuranceSubClass.CreatedBy) : 0;
                    insuranceSubClassVM.ModifiedBy = insuranceSubClass.ModifiedBy != null ? Convert.ToInt32(insuranceSubClass.ModifiedBy) : 0;

                    insuranceSubClassList.Add(insuranceSubClassVM);
                }

                return insuranceSubClassList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<InsuranceSubClassVM> GetAllInsuranceSubClassesByInsuranceClass(int insClassID)
        {
            try
            {
                var insuranceSubClassData = unitOfWork.TblInsSubClassRepository.Get(x => x.InsClassID == insClassID).ToList();

                List<InsuranceSubClassVM> insuranceSubClassList = new List<InsuranceSubClassVM>();

                foreach (var insuranceSubClass in insuranceSubClassData)
                {
                    InsuranceSubClassVM insuranceSubClassVM = new InsuranceSubClassVM();
                    insuranceSubClassVM.InsuranceSubClassID = insuranceSubClass.InsSubClassID;
                    insuranceSubClassVM.InsuranceClassID = insuranceSubClass.InsClassID != null ? Convert.ToInt32(insuranceSubClass.InsClassID) : 0;

                    if (insuranceSubClassVM.InsuranceClassID > 0)
                    {
                        insuranceSubClassVM.InsuranceClassCode = insuranceSubClass.tblInsClass.Code;
                    }

                    insuranceSubClassVM.Description = insuranceSubClass.Description;
                    insuranceSubClassVM.IsActive = insuranceSubClass.IsActive;
                    insuranceSubClassVM.CreatedDate = insuranceSubClass.CreatedDate != null ? insuranceSubClass.CreatedDate.ToString() : string.Empty;
                    insuranceSubClassVM.ModifiedDate = insuranceSubClass.ModifiedDate != null ? insuranceSubClass.ModifiedDate.ToString() : string.Empty;
                    insuranceSubClassVM.CreatedBy = insuranceSubClass.CreatedBy != null ? Convert.ToInt32(insuranceSubClass.CreatedBy) : 0;
                    insuranceSubClassVM.ModifiedBy = insuranceSubClass.ModifiedBy != null ? Convert.ToInt32(insuranceSubClass.ModifiedBy) : 0;

                    insuranceSubClassList.Add(insuranceSubClassVM);
                }

                return insuranceSubClassList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<InsuranceSubClassVM> GetAllInsuranceSubClassesByBusinessUnit(int businessUnitID)
        {
            try
            {
                var insuranceSubClassData = unitOfWork.TblInsSubClassRepository.Get(x => x.tblInsClass.BUID == businessUnitID).ToList();

                List<InsuranceSubClassVM> insuranceSubClassList = new List<InsuranceSubClassVM>();

                foreach (var insuranceSubClass in insuranceSubClassData)
                {
                    InsuranceSubClassVM insuranceSubClassVM = new InsuranceSubClassVM();
                    insuranceSubClassVM.InsuranceSubClassID = insuranceSubClass.InsSubClassID;
                    insuranceSubClassVM.InsuranceClassID = insuranceSubClass.InsClassID != null ? Convert.ToInt32(insuranceSubClass.InsClassID) : 0;

                    if (insuranceSubClassVM.InsuranceClassID > 0)
                    {
                        insuranceSubClassVM.InsuranceClassCode = insuranceSubClass.tblInsClass.Code;
                    }

                    insuranceSubClassVM.Description = insuranceSubClass.Description;
                    insuranceSubClassVM.IsActive = insuranceSubClass.IsActive;
                    insuranceSubClassVM.CreatedDate = insuranceSubClass.CreatedDate != null ? insuranceSubClass.CreatedDate.ToString() : string.Empty;
                    insuranceSubClassVM.ModifiedDate = insuranceSubClass.ModifiedDate != null ? insuranceSubClass.ModifiedDate.ToString() : string.Empty;
                    insuranceSubClassVM.CreatedBy = insuranceSubClass.CreatedBy != null ? Convert.ToInt32(insuranceSubClass.CreatedBy) : 0;
                    insuranceSubClassVM.ModifiedBy = insuranceSubClass.ModifiedBy != null ? Convert.ToInt32(insuranceSubClass.ModifiedBy) : 0;

                    insuranceSubClassList.Add(insuranceSubClassVM);
                }

                return insuranceSubClassList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public InsuranceSubClassVM GetInsuranceSubClassByID(int insuranceSubClassID)
        {
            try
            {
                var insuranceSubClassData = unitOfWork.TblInsSubClassRepository.GetByID(insuranceSubClassID);

                InsuranceSubClassVM insuranceSubClassVM = new InsuranceSubClassVM();
                insuranceSubClassVM.InsuranceSubClassID = insuranceSubClassData.InsSubClassID;
                insuranceSubClassVM.InsuranceClassID = insuranceSubClassData.InsClassID != null ? Convert.ToInt32(insuranceSubClassData.InsClassID) : 0;

                if (insuranceSubClassVM.InsuranceClassID > 0)
                {
                    insuranceSubClassVM.InsuranceClassCode = insuranceSubClassData.tblInsClass.Code;
                }

                insuranceSubClassVM.Description = insuranceSubClassData.Description;
                insuranceSubClassVM.IsActive = insuranceSubClassData.IsActive;
                insuranceSubClassVM.CreatedDate = insuranceSubClassData.CreatedDate != null ? insuranceSubClassData.CreatedDate.ToString() : string.Empty;
                insuranceSubClassVM.ModifiedDate = insuranceSubClassData.ModifiedDate != null ? insuranceSubClassData.ModifiedDate.ToString() : string.Empty;
                insuranceSubClassVM.CreatedBy = insuranceSubClassData.CreatedBy != null ? Convert.ToInt32(insuranceSubClassData.CreatedBy) : 0;
                insuranceSubClassVM.ModifiedBy = insuranceSubClassData.ModifiedBy != null ? Convert.ToInt32(insuranceSubClassData.ModifiedBy) : 0;

                return insuranceSubClassVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
