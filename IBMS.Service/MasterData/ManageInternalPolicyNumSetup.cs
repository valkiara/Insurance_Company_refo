using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManageInternalPolicyNumSetup
    {
        private UnitOfWork unitOfWork;
        public ManageInternalPolicyNumSetup()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool SaveInternalPolicyNumSetup(InternalPolicyNumSetupVM internalPolicyNumSetupVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblInternalPolicyNumSetup internalPolicyNumSetup = new tblInternalPolicyNumSetup();
                    internalPolicyNumSetup.InternalPolicyNumber = internalPolicyNumSetupVM.InternalPolicyNumber;
                    internalPolicyNumSetup.BUID = internalPolicyNumSetupVM.BusinessUnitID;
                    internalPolicyNumSetup.CreatedDate = DateTime.Now;
                    internalPolicyNumSetup.CreatedBy = internalPolicyNumSetupVM.CreatedBy;
                    unitOfWork.TblInternalPolicyNumSetupRepository.Insert(internalPolicyNumSetup);
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

        public bool UpdateInternalPolicyNumSetup(InternalPolicyNumSetupVM internalPolicyNumSetupVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblInternalPolicyNumSetup internalPolicyNumSetup = unitOfWork.TblInternalPolicyNumSetupRepository.GetByID(internalPolicyNumSetupVM.InternalPolicyNumSetupID);
                    internalPolicyNumSetup.InternalPolicyNumber = internalPolicyNumSetupVM.InternalPolicyNumber;
                    internalPolicyNumSetup.BUID = internalPolicyNumSetupVM.BusinessUnitID;
                    internalPolicyNumSetup.ModifiedDate = DateTime.Now;
                    internalPolicyNumSetup.ModifiedBy = internalPolicyNumSetupVM.ModifiedBy;
                    unitOfWork.TblInternalPolicyNumSetupRepository.Update(internalPolicyNumSetup);
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

        public bool DeleteInternalPolicyNumSetup(int internalPolicyNumSetupID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblInternalPolicyNumSetup internalPolicyNumSetup = unitOfWork.TblInternalPolicyNumSetupRepository.GetByID(internalPolicyNumSetupID);
                    unitOfWork.TblInternalPolicyNumSetupRepository.Delete(internalPolicyNumSetup);
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

        public List<InternalPolicyNumSetupVM> GetAllInternalPolicyNumSetups()
        {
            try
            {
                var internalPolicyNumSetupData = unitOfWork.TblInternalPolicyNumSetupRepository.Get().ToList();

                List<InternalPolicyNumSetupVM> internalPolicyNumSetupList = new List<InternalPolicyNumSetupVM>();

                foreach (var internalPolicyNumSetup in internalPolicyNumSetupData)
                {
                    InternalPolicyNumSetupVM internalPolicyNumSetupVM = new InternalPolicyNumSetupVM();
                    internalPolicyNumSetupVM.InternalPolicyNumSetupID = internalPolicyNumSetup.InternalPolicyNumSetupID;
                    internalPolicyNumSetupVM.InternalPolicyNumber = internalPolicyNumSetup.InternalPolicyNumber;
                    internalPolicyNumSetupVM.BusinessUnitID = internalPolicyNumSetup.BUID != null ? Convert.ToInt32(internalPolicyNumSetup.BUID) : 0;

                    if (internalPolicyNumSetupVM.BusinessUnitID > 0)
                    {
                        internalPolicyNumSetupVM.BusinessUnitName = internalPolicyNumSetup.tblBussinessUnit.BussinessUnit;
                    }

                    internalPolicyNumSetupVM.CreatedBy = internalPolicyNumSetup.CreatedBy != null ? Convert.ToInt32(internalPolicyNumSetup.CreatedBy) : 0;
                    internalPolicyNumSetupVM.CreatedDate = internalPolicyNumSetup.CreatedDate != null ? internalPolicyNumSetup.CreatedDate.ToString() : string.Empty;
                    internalPolicyNumSetupVM.ModifiedBy = internalPolicyNumSetup.ModifiedBy != null ? Convert.ToInt32(internalPolicyNumSetup.ModifiedBy) : 0;
                    internalPolicyNumSetupVM.ModifiedDate = internalPolicyNumSetup.ModifiedDate != null ? internalPolicyNumSetup.ModifiedDate.ToString() : string.Empty;

                    internalPolicyNumSetupList.Add(internalPolicyNumSetupVM);
                }

                return internalPolicyNumSetupList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<InternalPolicyNumSetupVM> GetAllInternalPolicyNumSetupsByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var internalPolicyNumSetupData = unitOfWork.TblInternalPolicyNumSetupRepository.Get(x => x.BUID == businessUnitID).ToList();

                List<InternalPolicyNumSetupVM> internalPolicyNumSetupList = new List<InternalPolicyNumSetupVM>();

                foreach (var internalPolicyNumSetup in internalPolicyNumSetupData)
                {
                    InternalPolicyNumSetupVM internalPolicyNumSetupVM = new InternalPolicyNumSetupVM();
                    internalPolicyNumSetupVM.InternalPolicyNumSetupID = internalPolicyNumSetup.InternalPolicyNumSetupID;
                    internalPolicyNumSetupVM.InternalPolicyNumber = internalPolicyNumSetup.InternalPolicyNumber;
                    internalPolicyNumSetupVM.BusinessUnitID = internalPolicyNumSetup.BUID != null ? Convert.ToInt32(internalPolicyNumSetup.BUID) : 0;

                    if (internalPolicyNumSetupVM.BusinessUnitID > 0)
                    {
                        internalPolicyNumSetupVM.BusinessUnitName = internalPolicyNumSetup.tblBussinessUnit.BussinessUnit;
                    }

                    internalPolicyNumSetupVM.CreatedBy = internalPolicyNumSetup.CreatedBy != null ? Convert.ToInt32(internalPolicyNumSetup.CreatedBy) : 0;
                    internalPolicyNumSetupVM.CreatedDate = internalPolicyNumSetup.CreatedDate != null ? internalPolicyNumSetup.CreatedDate.ToString() : string.Empty;
                    internalPolicyNumSetupVM.ModifiedBy = internalPolicyNumSetup.ModifiedBy != null ? Convert.ToInt32(internalPolicyNumSetup.ModifiedBy) : 0;
                    internalPolicyNumSetupVM.ModifiedDate = internalPolicyNumSetup.ModifiedDate != null ? internalPolicyNumSetup.ModifiedDate.ToString() : string.Empty;

                    internalPolicyNumSetupList.Add(internalPolicyNumSetupVM);
                }

                return internalPolicyNumSetupList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public InternalPolicyNumSetupVM GetInternalPolicyNumSetupByID(int internalPolicyNumSetupID)
        {
            try
            {
                var internalPolicyNumSetupData = unitOfWork.TblInternalPolicyNumSetupRepository.GetByID(internalPolicyNumSetupID);

                InternalPolicyNumSetupVM internalPolicyNumSetupVM = new InternalPolicyNumSetupVM();
                internalPolicyNumSetupVM.InternalPolicyNumSetupID = internalPolicyNumSetupData.InternalPolicyNumSetupID;
                internalPolicyNumSetupVM.InternalPolicyNumber = internalPolicyNumSetupData.InternalPolicyNumber;
                internalPolicyNumSetupVM.BusinessUnitID = internalPolicyNumSetupData.BUID != null ? Convert.ToInt32(internalPolicyNumSetupData.BUID) : 0;

                if (internalPolicyNumSetupVM.BusinessUnitID > 0)
                {
                    internalPolicyNumSetupVM.BusinessUnitName = internalPolicyNumSetupData.tblBussinessUnit.BussinessUnit;
                }

                internalPolicyNumSetupVM.CreatedBy = internalPolicyNumSetupData.CreatedBy != null ? Convert.ToInt32(internalPolicyNumSetupData.CreatedBy) : 0;
                internalPolicyNumSetupVM.CreatedDate = internalPolicyNumSetupData.CreatedDate != null ? internalPolicyNumSetupData.CreatedDate.ToString() : string.Empty;
                internalPolicyNumSetupVM.ModifiedBy = internalPolicyNumSetupData.ModifiedBy != null ? Convert.ToInt32(internalPolicyNumSetupData.ModifiedBy) : 0;
                internalPolicyNumSetupVM.ModifiedDate = internalPolicyNumSetupData.ModifiedDate != null ? internalPolicyNumSetupData.ModifiedDate.ToString() : string.Empty;

                return internalPolicyNumSetupVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsInternalPolicyNumberAvailable(int? internalPolicyNumSetupID, string internalPolicyNumber)
        {
            try
            {
                if (internalPolicyNumSetupID != null && unitOfWork.TblInternalPolicyNumSetupRepository.Get().Any(x => x.InternalPolicyNumber.ToLower() == internalPolicyNumber.ToLower() && x.InternalPolicyNumSetupID != internalPolicyNumSetupID))
                {
                    return true;
                }
                else if (internalPolicyNumSetupID == null && unitOfWork.TblInternalPolicyNumSetupRepository.Get().Any(x => x.InternalPolicyNumber.ToLower() == internalPolicyNumber.ToLower()))
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
