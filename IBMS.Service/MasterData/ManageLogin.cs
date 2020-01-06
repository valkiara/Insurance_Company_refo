using IBMS.Repository;
using IBMS.Shared.Function;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManageLogin
    {
        private UnitOfWork unitOfWork;
        public ManageLogin()
        {
            unitOfWork = new UnitOfWork();
        }

        public string AuthenticateUser(string loginName, string password, int businessUnitID, out int? userID)
        {
            try
            {
                string returnString = null;

                if (!string.IsNullOrEmpty(loginName) && !string.IsNullOrEmpty(password))
                {
                    //tblUser userData = null;

                    ////Admin Login
                    //if (loginName.ToLower() == "admin")
                    //{
                    //    userData = unitOfWork.TblUserRepository.Get(x => x.LoginName.ToLower() == loginName.ToLower()).SingleOrDefault();
                    //}
                    ////User Login
                    //else
                    //{
                    //    userData = unitOfWork.TblUserRepository.Get(x => x.LoginName.ToLower() == loginName.ToLower() && x.tblDesignation.BUID == businessUnitID).SingleOrDefault();
                    //}

                    //if (userData != null)
                    //{
                    //    userID = userData.UserID;
                    //    returnString = HashPassword.CompareHash(password, userData.Password) ? "PASS" : "WP";
                    //}
                    //else
                    //{
                    //    userID = null;
                    //    returnString = "IU";
                    //}

                    tblUser userData = unitOfWork.TblUserRepository.Get(x => x.LoginName.ToLower() == loginName.ToLower()).SingleOrDefault();

                    if (userData != null)
                    {
                        if (userData.tblAccessLevelType.AccessLevelTypeName != "Admin")
                        {
                            if (userData.tblDesignation.BUID == businessUnitID)
                            {
                                userID = userData.UserID;
                                returnString = HashPassword.CompareHash(password, userData.Password) ? "PASS" : "WP";
                            }
                            else
                            {
                                userID = null;
                                returnString = "IU";
                            }
                        }
                        else
                        {
                            userID = userData.UserID;
                            returnString = HashPassword.CompareHash(password, userData.Password) ? "PASS" : "WP";
                        }
                    }
                    else
                    {
                        userID = null;
                        returnString = "IU";
                    }
                }
                else
                {
                    userID = null;
                    returnString = "EMPTY";
                }

                return returnString;
            }
            catch (Exception EX)
            {
                userID = null;
                return null;
            }
        }

        public UserVM GetLoggedUserByID(int userID)
        {
            try
            {
                tblUser userData = unitOfWork.TblUserRepository.GetByID(userID);

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
            catch (Exception EX)
            {
                return null;
            }
        }

        public void UpdateUserLastLoginDetails(int userID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblUser user = unitOfWork.TblUserRepository.GetByID(userID);
                    user.LastLogin = DateTime.Now;
                    unitOfWork.TblUserRepository.Update(user);
                    unitOfWork.Save();

                    //Complete the Transaction
                    dbTransaction.Commit();
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                }
            }
        }
    }
}
