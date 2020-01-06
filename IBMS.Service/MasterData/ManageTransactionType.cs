using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManageTransactionType
    {
        private UnitOfWork unitOfWork;
        public ManageTransactionType()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool SaveTransactionType(TransactionTypeVM transactionTypeVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblTransactionType transactionType = new tblTransactionType();
                    transactionType.Description = transactionTypeVM.Description;
                    transactionType.BUID = transactionTypeVM.BusinessUnitID;
                    transactionType.CreatedDate = DateTime.Now;
                    transactionType.CreatedBy = transactionTypeVM.CreatedBy;
                    unitOfWork.TblTransactionTypeRepository.Insert(transactionType);
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

        public bool IsTransactionAvailable(int? TransactionTypeID, string Description)
        {
            try
            {
                if (TransactionTypeID != null && unitOfWork.TblTransactionTypeRepository.Get().Any(x => x.Description.ToLower() == Description.ToLower() && x.TransactionTypeID != TransactionTypeID))
                {
                    return true;
                }
                else if (TransactionTypeID == null && unitOfWork.TblTransactionTypeRepository.Get().Any(x => x.Description.ToLower() == Description.ToLower()))
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
        public bool UpdateTransactionType(TransactionTypeVM transactionTypeVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblTransactionType transactionType = unitOfWork.TblTransactionTypeRepository.GetByID(transactionTypeVM.TransactionTypeID);
                    transactionType.Description = transactionTypeVM.Description;
                    transactionType.BUID = transactionTypeVM.BusinessUnitID;
                    transactionType.ModifiedDate = DateTime.Now;
                    transactionType.ModifiedBy = transactionTypeVM.ModifiedBy;
                    unitOfWork.TblTransactionTypeRepository.Update(transactionType);
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

        public bool DeleteTransactionType(int transactionTypeID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblTransactionType transactionType = unitOfWork.TblTransactionTypeRepository.GetByID(transactionTypeID);
                    unitOfWork.TblTransactionTypeRepository.Delete(transactionType);
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

        public List<TransactionTypeVM> GetAllTransactionTypes()
        {
            try
            {
                var transactionTypeData = unitOfWork.TblTransactionTypeRepository.Get().ToList();

                List<TransactionTypeVM> transactionTypeList = new List<TransactionTypeVM>();

                foreach (var transactionType in transactionTypeData)
                {
                    TransactionTypeVM transactionTypeVM = new TransactionTypeVM();
                    transactionTypeVM.TransactionTypeID = transactionType.TransactionTypeID;
                    transactionTypeVM.Description = transactionType.Description;
                    transactionTypeVM.BusinessUnitID = transactionType.BUID != null ? Convert.ToInt32(transactionType.BUID) : 0;

                    if (transactionTypeVM.BusinessUnitID > 0)
                    {
                        transactionTypeVM.BusinessUnitName = transactionType.tblBussinessUnit.BussinessUnit;
                    }

                    transactionTypeVM.CreatedDate = transactionType.CreatedDate != null ? transactionType.CreatedDate.ToString() : string.Empty;
                    transactionTypeVM.ModifiedDate = transactionType.ModifiedDate != null ? transactionType.ModifiedDate.ToString() : string.Empty;
                    transactionTypeVM.CreatedBy = transactionType.CreatedBy != null ? Convert.ToInt32(transactionType.CreatedBy) : 0;
                    transactionTypeVM.ModifiedBy = transactionType.ModifiedBy != null ? Convert.ToInt32(transactionType.ModifiedBy) : 0;

                    transactionTypeList.Add(transactionTypeVM);
                }

                return transactionTypeList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<TransactionTypeVM> GetAllTransactionTypesByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var transactionTypeData = unitOfWork.TblTransactionTypeRepository.Get(x => x.BUID == businessUnitID).ToList();

                List<TransactionTypeVM> transactionTypeList = new List<TransactionTypeVM>();

                foreach (var transactionType in transactionTypeData)
                {
                    TransactionTypeVM transactionTypeVM = new TransactionTypeVM();
                    transactionTypeVM.TransactionTypeID = transactionType.TransactionTypeID;
                    transactionTypeVM.Description = transactionType.Description;
                    transactionTypeVM.BusinessUnitID = transactionType.BUID != null ? Convert.ToInt32(transactionType.BUID) : 0;

                    if (transactionTypeVM.BusinessUnitID > 0)
                    {
                        transactionTypeVM.BusinessUnitName = transactionType.tblBussinessUnit.BussinessUnit;
                    }

                    transactionTypeVM.CreatedDate = transactionType.CreatedDate != null ? transactionType.CreatedDate.ToString() : string.Empty;
                    transactionTypeVM.ModifiedDate = transactionType.ModifiedDate != null ? transactionType.ModifiedDate.ToString() : string.Empty;
                    transactionTypeVM.CreatedBy = transactionType.CreatedBy != null ? Convert.ToInt32(transactionType.CreatedBy) : 0;
                    transactionTypeVM.ModifiedBy = transactionType.ModifiedBy != null ? Convert.ToInt32(transactionType.ModifiedBy) : 0;

                    transactionTypeList.Add(transactionTypeVM);
                }

                return transactionTypeList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public TransactionTypeVM GetTransactionTypeByID(int transactionTypeID)
        {
            try
            {
                var transactionTypeData = unitOfWork.TblTransactionTypeRepository.GetByID(transactionTypeID);

                TransactionTypeVM transactionTypeVM = new TransactionTypeVM();
                transactionTypeVM.TransactionTypeID = transactionTypeData.TransactionTypeID;
                transactionTypeVM.Description = transactionTypeData.Description;
                transactionTypeVM.BusinessUnitID = transactionTypeData.BUID != null ? Convert.ToInt32(transactionTypeData.BUID) : 0;

                if (transactionTypeVM.BusinessUnitID > 0)
                {
                    transactionTypeVM.BusinessUnitName = transactionTypeData.tblBussinessUnit.BussinessUnit;
                }

                transactionTypeVM.CreatedDate = transactionTypeData.CreatedDate != null ? transactionTypeData.CreatedDate.ToString() : string.Empty;
                transactionTypeVM.ModifiedDate = transactionTypeData.ModifiedDate != null ? transactionTypeData.ModifiedDate.ToString() : string.Empty;
                transactionTypeVM.CreatedBy = transactionTypeData.CreatedBy != null ? Convert.ToInt32(transactionTypeData.CreatedBy) : 0;
                transactionTypeVM.ModifiedBy = transactionTypeData.ModifiedBy != null ? Convert.ToInt32(transactionTypeData.ModifiedBy) : 0;

                return transactionTypeVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
