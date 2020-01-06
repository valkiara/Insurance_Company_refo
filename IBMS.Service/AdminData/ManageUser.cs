using IBMS.Repository;
using IBMS.Shared.Function;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.AdminData
{
    public class ManageUser
    {
        private UnitOfWork unitOfWork;
        public ManageUser()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool UpdateUser(UserVM userVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblUser user = unitOfWork.TblUserRepository.GetByID(userVM.UserID);
                    user.UserName = userVM.UserName;
                    user.LoginName = userVM.LoginName;
                    user.DesignationID = userVM.DesignationID;
                    user.ModifiedDate = DateTime.Now;
                    user.ModifiedBy = userVM.ModifiedBy;
                    unitOfWork.TblUserRepository.Update(user);
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

        public string ChangePassword(int userID, string oldPassword, string newPassword)
        {
            try
            {
                tblUser user = unitOfWork.TblUserRepository.GetByID(userID);

                if (HashPassword.CompareHash(oldPassword, user.Password))
                {
                    user.Password = HashPassword.Genaratehash(newPassword);
                    unitOfWork.TblUserRepository.Update(user);
                    unitOfWork.Save();

                    return "SUCCESS";
                }
                else
                {
                    return "WOP";
                }
            }
            catch (Exception ex)
            {
                return "UE";
            }
        }

        public UserVM GetUserByID(int userID)
        {
            try
            {
                var userData = unitOfWork.TblUserRepository.GetByID(userID);

                UserVM userVM = new UserVM();
                userVM.UserID = userData.UserID;
                userVM.UserName = userData.UserName;
                userVM.LoginName = userData.LoginName;
                userVM.AccessLevelType = userData.AccessLevelType;
                userVM.AccessLevelTypeName = userData.tblAccessLevelType.AccessLevelTypeName;
                userVM.DesignationID = userData.DesignationID != null ? Convert.ToInt32(userData.DesignationID) : 0;

                if (userVM.DesignationID > 0)
                {
                    userVM.DesignationName = userData.tblDesignation.Designation;
                    userVM.BusinessUnitID = userData.tblDesignation.BUID != null ? Convert.ToInt32(userData.tblDesignation.BUID) : 0;

                    if (userVM.BusinessUnitID > 0)
                    {
                        userVM.BusinessUnitName = userData.tblDesignation.tblBussinessUnit.BussinessUnit;
                        userVM.CompanyID = userData.tblDesignation.tblBussinessUnit.CompID != null ? Convert.ToInt32(userData.tblDesignation.tblBussinessUnit.CompID) : 0;

                        if (userVM.CompanyID > 0)
                        {
                            userVM.CompanyName = userData.tblDesignation.tblBussinessUnit.tblCompany.CompanyName;
                        }
                    }
                }

                userVM.LastLogin = userData.LastLogin != null ? userData.LastLogin.ToString() : string.Empty;
                userVM.CreatedBy = userData.CreatedBy != null ? Convert.ToInt32(userData.CreatedBy) : 0;
                userVM.CreatedDate = userData.CreatedDate != null ? userData.CreatedDate.ToString() : string.Empty;
                userVM.ModifiedBy = userData.ModifiedBy != null ? Convert.ToInt32(userData.ModifiedBy) : 0;
                userVM.ModifiedDate = userData.ModifiedDate != null ? userData.ModifiedDate.ToString() : string.Empty;

                var allowedList = unitOfWork.TblFunctionAccessRepository.Get(x => x.AccessLevelTypeID == userData.AccessLevelType).ToList();
                List<int> allowedFunctionList = new List<int>();

                foreach (var function in allowedList)
                {
                    allowedFunctionList.Add(function.tblFunction.FunctionNumber);
                }

                userVM.AllowedFunctionList = allowedFunctionList;
                userVM.MasterCount = allowedList.Where(x => x.tblFunction.tblFunctionType.FunctionTypeName == "Master").ToList().Count;
                userVM.TransactionCount = allowedList.Where(x => x.tblFunction.tblFunctionType.FunctionTypeName == "Transaction").ToList().Count;
                userVM.EnquiriesCount = allowedList.Where(x => x.tblFunction.tblFunctionType.FunctionTypeName == "Enquiries").ToList().Count;
                userVM.ReportsCount = allowedList.Where(x => x.tblFunction.tblFunctionType.FunctionTypeName == "Reports").ToList().Count;
                userVM.AdministrationCount = allowedList.Where(x => x.tblFunction.tblFunctionType.FunctionTypeName == "Administration").ToList().Count;

                return userVM;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool IsLoginNameAvailable(int? userID, string loginName)
        {
            try
            {
                if (userID != null && unitOfWork.TblUserRepository.Get().Any(x => x.LoginName.ToLower() == loginName.ToLower() && x.UserID != userID))
                {
                    return true;
                }
                else if (userID == null && unitOfWork.TblUserRepository.Get().Any(x => x.LoginName.ToLower() == loginName.ToLower()))
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
