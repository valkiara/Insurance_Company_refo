using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBMS.Repository;
using IBMS.Shared.ViewModel;

namespace IBMS.Service.MasterData
{
    public class ManageAccountHandler
    {
        private UnitOfWork unitOfWork;

        public ManageAccountHandler()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool SaveAccountHandler(AccountHandlerVM accountHandlerVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblAccountHandler accountHandler = new tblAccountHandler();
                    accountHandler.CompID = accountHandler.CompID;
                    accountHandler.AccountHandlerName = accountHandler.AccountHandlerName;
                    accountHandler.Address1 = accountHandler.Address1;
                    accountHandler.Address2 = accountHandler.Address2;
                    accountHandler.Address3 = accountHandler.Address3;
                    accountHandler.ExtraFloat1 = accountHandler.ExtraFloat1;
                    accountHandler.AccountHandlerCode = accountHandler.AccountHandlerCode;
                    accountHandler.AccountHandlerType = accountHandler.AccountHandlerType;
                    accountHandler.AccountHandlerNIC = accountHandler.AccountHandlerNIC;
                    accountHandler.CreatedDate = DateTime.Now;
                    accountHandler.CreatedBy = accountHandler.CreatedBy;
                    accountHandler.AgentBRNo = accountHandler.AgentBRNo;
                    unitOfWork.TblAccountHandlerRepository.Insert(accountHandler);
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

        public bool UpdateAccountHandler(AccountHandlerVM accountHandlerVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblAccountHandler accountHandler = new tblAccountHandler();
                    accountHandler.CompID = accountHandler.CompID;
                    accountHandler.AccountHandlerName = accountHandler.AccountHandlerName;
                    accountHandler.Address1 = accountHandler.Address1;
                    accountHandler.Address2 = accountHandler.Address2;
                    accountHandler.Address3 = accountHandler.Address3;
                    accountHandler.ExtraFloat1 = accountHandler.ExtraFloat1;
                    accountHandler.AccountHandlerCode = accountHandler.AccountHandlerCode;
                    accountHandler.AccountHandlerType = accountHandler.AccountHandlerType;
                    accountHandler.AccountHandlerNIC = accountHandler.AccountHandlerNIC;
                    accountHandler.CreatedDate = DateTime.Now;
                    accountHandler.CreatedBy = accountHandler.CreatedBy;
                    accountHandler.AgentBRNo = accountHandler.AgentBRNo;
                    unitOfWork.TblAccountHandlerRepository.Update(accountHandler);
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


        public bool DeleteAccountHandler(int AccountHandlerID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblAccountHandler accountHandler = unitOfWork.TblAccountHandlerRepository.GetByID(AccountHandlerID);
                    unitOfWork.TblAccountHandlerRepository.Delete(accountHandler);
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

        //public List<AccountHandlerVM> GetAllAccountHandler()
        //{
        //    try
        //    {
        //        var AccountHandlerData = unitOfWork.tblAccountHandlerRepository.Get().ToList();

        //        List<AccountHandlerVM> AccountHandlerList = new List<AccountHandlerVM>();

        //        foreach (var accHandler in AccountHandlerData)
        //        {
        //            AccountHandlerVM accountHandlerVM = new AccountHandlerVM();
        //            accountHandlerVM.AccountHandlerID = accountHandlerVM.AccountHandlerID;
        //            accountHandlerVM.CompID = accountHandlerVM.CompID != null ? Convert.ToInt32(accHandler.CompID) : 0;

        //            if (accountHandlerVM.CompID > 0)
        //            {
        //                accountHandlerVM.CompanyName = accHandler.tblCompany.CompanyName;
        //            }

        //            accountHandlerVM.AccountHandlerName = accHandler.AccountHandlerName;
        //            accountHandlerVM.Address1 = accHandler.Address1;
        //            accountHandlerVM.Address2 = accHandler.Address2;
        //            accountHandlerVM.Address3 = accHandler.Address3;
        //            accountHandlerVM.ExtraFloat1 = accHandler.ExtraFloat1 != null ? Convert.ToDouble(accHandler.ExtraFloat1) : 0;
        //            accountHandlerVM.CreatedDate = accHandler.CreatedDate != null ? accHandler.CreatedDate.ToString() : string.Empty;
        //            accountHandlerVM.ModifiedDate = accHandler.ModifiedDate != null ? accHandler.ModifiedDate.ToString() : string.Empty;
        //            accountHandlerVM.CreatedBy = accHandler.CreatedBy != null ? Convert.ToInt32(accHandler.CreatedBy) : 0;
        //            accountHandlerVM.ModifiedBy = accHandler.ModifiedBy != null ? Convert.ToInt32(accHandler.ModifiedBy) : 0;
        //            accountHandlerVM.AccountHandlerType = accHandler.AccountHandlerType;
        //            accountHandlerVM.AccountHandlerNIC = accHandler.AccountHandlerNIC;
        //            accountHandlerVM.AgentBRNo = accHandler.AgentBRNo;

        //            AccountHandlerList.Add(accountHandlerVM);
        //        }

        //        return AccountHandlerList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public List<AccountHandlerVM> GetAccountHandlerByCompanyID(int companyID)
        //{
        //    try
        //    {
        //        var AccountHandlerData = unitOfWork.tblAccountHandlerRepository.Get(x => x.tblCompany.CompID == companyID).ToList();

        //        List<AccountHandlerVM> AccountHandlerList = new List<AccountHandlerVM>();

        //        foreach (var accHandler in AccountHandlerData)
        //        {
        //            AccountHandlerVM accountHandlerVM = new AccountHandlerVM();
        //            accountHandlerVM.AccountHandlerID = accountHandlerVM.AccountHandlerID;
        //            accountHandlerVM.CompID = accountHandlerVM.CompID != null ? Convert.ToInt32(accHandler.CompID) : 0;

        //            if (accountHandlerVM.CompID > 0)
        //            {
        //                accountHandlerVM.CompanyName = accHandler.tblCompany.CompanyName;
        //            }

        //            accountHandlerVM.AccountHandlerName = accHandler.AccountHandlerName;
        //            accountHandlerVM.Address1 = accHandler.Address1;
        //            accountHandlerVM.Address2 = accHandler.Address2;
        //            accountHandlerVM.Address3 = accHandler.Address3;
        //            accountHandlerVM.ExtraFloat1 = accHandler.ExtraFloat1 != null ? Convert.ToDouble(accHandler.ExtraFloat1) : 0;
        //            accountHandlerVM.CreatedDate = accHandler.CreatedDate != null ? accHandler.CreatedDate.ToString() : string.Empty;
        //            accountHandlerVM.ModifiedDate = accHandler.ModifiedDate != null ? accHandler.ModifiedDate.ToString() : string.Empty;
        //            accountHandlerVM.CreatedBy = accHandler.CreatedBy != null ? Convert.ToInt32(accHandler.CreatedBy) : 0;
        //            accountHandlerVM.ModifiedBy = accHandler.ModifiedBy != null ? Convert.ToInt32(accHandler.ModifiedBy) : 0;
        //            accountHandlerVM.AccountHandlerType = accHandler.AccountHandlerType;
        //            accountHandlerVM.AccountHandlerNIC = accHandler.AccountHandlerNIC;
        //            accountHandlerVM.AgentBRNo = accHandler.AgentBRNo;

        //            AccountHandlerList.Add(accountHandlerVM);
        //        }
        //        return AccountHandlerList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public AccountHandlerVM GetAccountHandleByID(int AccountHandlerID)
        {
            try
            {
                var AccountHandle = unitOfWork.TblAccountHandlerRepository.GetByID(AccountHandlerID);

                AccountHandlerVM accountHandlerVM = new AccountHandlerVM();
                accountHandlerVM.AccountHandlerID = accountHandlerVM.AccountHandlerID;
                accountHandlerVM.CompID = accountHandlerVM.CompID != null ? Convert.ToInt32(AccountHandle.CompID) : 0;

                if (accountHandlerVM.CompID > 0)
                {
                    accountHandlerVM.CompanyName = "";
                }

                accountHandlerVM.AccountHandlerName = accountHandlerVM.AccountHandlerName;
                accountHandlerVM.Address1 = accountHandlerVM.Address1;
                accountHandlerVM.Address2 = accountHandlerVM.Address2;
                accountHandlerVM.Address3 = accountHandlerVM.Address3;
                accountHandlerVM.ExtraFloat1 = accountHandlerVM.ExtraFloat1 != null ? Convert.ToDouble(accountHandlerVM.ExtraFloat1) : 0;
                accountHandlerVM.CreatedDate = accountHandlerVM.CreatedDate != null ? accountHandlerVM.CreatedDate.ToString() : string.Empty;
                accountHandlerVM.ModifiedDate = accountHandlerVM.ModifiedDate != null ? accountHandlerVM.ModifiedDate.ToString() : string.Empty;
                accountHandlerVM.CreatedBy = accountHandlerVM.CreatedBy != null ? Convert.ToInt32(accountHandlerVM.CreatedBy) : 0;
                accountHandlerVM.ModifiedBy = accountHandlerVM.ModifiedBy != null ? Convert.ToInt32(accountHandlerVM.ModifiedBy) : 0;
                accountHandlerVM.AccountHandlerType = accountHandlerVM.AccountHandlerType;
                accountHandlerVM.AccountHandlerNIC = accountHandlerVM.AccountHandlerNIC;
                accountHandlerVM.AgentBRNo = accountHandlerVM.AgentBRNo;


                return accountHandlerVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsAccountHandlerAvailable(int? accountHandler, string accountHandlerName)
        {
            try
            {
                if (accountHandler != null && unitOfWork.TblAccountHandlerRepository.Get().Any(x => x.AccountHandlerName.ToLower() == accountHandlerName.ToLower() && x.AccountHandlerID != accountHandler))
                {
                    return true;
                }
                else if (accountHandler == null && unitOfWork.TblAccountHandlerRepository.Get().Any(x => x.AccountHandlerName.ToLower() == accountHandlerName.ToLower()))
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
