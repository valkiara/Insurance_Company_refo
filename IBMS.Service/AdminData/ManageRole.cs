using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.AdminData
{
    public class ManageRole
    {
        private UnitOfWork unitOfWork;
        public ManageRole()
        {
            unitOfWork = new UnitOfWork();
        }

        #region Manage Role
        public bool SaveAccessLevelType(AccessLevelTypeVM accessLevelTypeVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblAccessLevelType accessLevelType = new tblAccessLevelType();
                    accessLevelType.AccessLevelTypeName = accessLevelType.AccessLevelTypeName;
                    accessLevelType.Description = accessLevelType.Description;
                    unitOfWork.TblAccessLevelTypeRepository.Insert(accessLevelType);
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

        public bool UpdateAccessLevelType(AccessLevelTypeVM accessLevelTypeVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblAccessLevelType accessLevelType = unitOfWork.TblAccessLevelTypeRepository.GetByID(accessLevelTypeVM.AccessLevelTypeID);
                    accessLevelType.AccessLevelTypeName = accessLevelType.AccessLevelTypeName;
                    accessLevelType.Description = accessLevelType.Description;
                    unitOfWork.TblAccessLevelTypeRepository.Update(accessLevelType);
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

        public bool DeleteAccessLevelType(int accessLevelTypeID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblAccessLevelType accessLevelType = unitOfWork.TblAccessLevelTypeRepository.GetByID(accessLevelTypeID);
                    unitOfWork.TblAccessLevelTypeRepository.Delete(accessLevelType);
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

        public List<AccessLevelTypeVM> GetAllAccessLevelTypes()
        {
            try
            {
                var accessLevelTypeData = unitOfWork.TblAccessLevelTypeRepository.Get().ToList();

                List<AccessLevelTypeVM> accessLevelTypeList = new List<AccessLevelTypeVM>();

                foreach (var accessLevelType in accessLevelTypeData)
                {
                    AccessLevelTypeVM accessLevelTypeVM = new AccessLevelTypeVM();
                    accessLevelTypeVM.AccessLevelTypeID = accessLevelType.AccessLevelTypeID;
                    accessLevelTypeVM.AccessLevelTypeName = accessLevelType.AccessLevelTypeName;
                    accessLevelTypeVM.Description = accessLevelType.Description;

                    accessLevelTypeList.Add(accessLevelTypeVM);
                }

                return accessLevelTypeList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public AccessLevelTypeVM GetAccessLevelTypeByID(int accessLevelTypeID)
        {
            try
            {
                var accessLevelTypeData = unitOfWork.TblAccessLevelTypeRepository.GetByID(accessLevelTypeID);

                AccessLevelTypeVM accessLevelTypeVM = new AccessLevelTypeVM();
                accessLevelTypeVM.AccessLevelTypeID = accessLevelTypeData.AccessLevelTypeID;
                accessLevelTypeVM.AccessLevelTypeName = accessLevelTypeData.AccessLevelTypeName;
                accessLevelTypeVM.Description = accessLevelTypeData.Description;

                return accessLevelTypeVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsAccessLevelTypeAvailable(int? accessLevelTypeID, string accessLevelTypeName)
        {
            try
            {
                if (accessLevelTypeID != null && unitOfWork.TblAccessLevelTypeRepository.Get().Any(x => x.AccessLevelTypeName.ToLower() == accessLevelTypeName.ToLower() && x.AccessLevelTypeID != accessLevelTypeID))
                {
                    return true;
                }
                else if (accessLevelTypeID == null && unitOfWork.TblAccessLevelTypeRepository.Get().Any(x => x.AccessLevelTypeName.ToLower() == accessLevelTypeName.ToLower()))
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

        #region Manage Role Functions
        public bool UpdateFunctionAccessDetails(RoleVM roleVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    //Delete existing function access details
                    List<tblFunctionAccess> existingFunctionAccessList = unitOfWork.TblFunctionAccessRepository.Get(x => x.AccessLevelTypeID == roleVM.AccessLevelTypeID).ToList();

                    foreach (var existingFunctionAccess in existingFunctionAccessList)
                    {
                        unitOfWork.TblFunctionAccessRepository.Delete(existingFunctionAccess);
                        unitOfWork.Save();
                    }

                    //Save master function access details
                    foreach (var masterFunctions in roleVM.MasterFunctionDetails)
                    {
                        if (masterFunctions.IsChecked)
                        {
                            tblFunctionAccess functionAccess = new tblFunctionAccess();
                            functionAccess.AccessLevelTypeID = roleVM.AccessLevelTypeID;
                            functionAccess.FunctionID = masterFunctions.FunctionID;
                            unitOfWork.TblFunctionAccessRepository.Insert(functionAccess);
                            unitOfWork.Save();
                        }
                    }

                    //Save transaction function access details
                    foreach (var transactionFunctions in roleVM.TransactionFunctionDetails)
                    {
                        if (transactionFunctions.IsChecked)
                        {
                            tblFunctionAccess functionAccess = new tblFunctionAccess();
                            functionAccess.AccessLevelTypeID = roleVM.AccessLevelTypeID;
                            functionAccess.FunctionID = transactionFunctions.FunctionID;
                            unitOfWork.TblFunctionAccessRepository.Insert(functionAccess);
                            unitOfWork.Save();
                        }
                    }

                    //Save enquiry function access details
                    foreach (var enquiryFunctions in roleVM.EnquiryFunctionDetails)
                    {
                        if (enquiryFunctions.IsChecked)
                        {
                            tblFunctionAccess functionAccess = new tblFunctionAccess();
                            functionAccess.AccessLevelTypeID = roleVM.AccessLevelTypeID;
                            functionAccess.FunctionID = enquiryFunctions.FunctionID;
                            unitOfWork.TblFunctionAccessRepository.Insert(functionAccess);
                            unitOfWork.Save();
                        }
                    }

                    //Save report function access details
                    foreach (var reportFunctions in roleVM.EnquiryFunctionDetails)
                    {
                        if (reportFunctions.IsChecked)
                        {
                            tblFunctionAccess functionAccess = new tblFunctionAccess();
                            functionAccess.AccessLevelTypeID = roleVM.AccessLevelTypeID;
                            functionAccess.FunctionID = reportFunctions.FunctionID;
                            unitOfWork.TblFunctionAccessRepository.Insert(functionAccess);
                            unitOfWork.Save();
                        }
                    }

                    //Save admin function access details
                    foreach (var adminFunctions in roleVM.AdminFunctionDetails)
                    {
                        if (adminFunctions.IsChecked)
                        {
                            tblFunctionAccess functionAccess = new tblFunctionAccess();
                            functionAccess.AccessLevelTypeID = roleVM.AccessLevelTypeID;
                            functionAccess.FunctionID = adminFunctions.FunctionID;
                            unitOfWork.TblFunctionAccessRepository.Insert(functionAccess);
                            unitOfWork.Save();
                        }
                    }

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

        public RoleVM GetFunctionAccessDetailsByID(int accessLevelTypeID)
        {
            try
            {
                tblAccessLevelType accessLevelType = unitOfWork.TblAccessLevelTypeRepository.GetByID(accessLevelTypeID);
                List<tblFunction> functionList = unitOfWork.TblFunctionRepository.Get().ToList();
                List<tblFunctionAccess> existingFunctionAccessList = unitOfWork.TblFunctionAccessRepository.Get(x => x.AccessLevelTypeID == accessLevelTypeID).ToList();

                RoleVM roleVM = new RoleVM();
                roleVM.AccessLevelTypeID = accessLevelType.AccessLevelTypeID;
                roleVM.AccessLevelTypeName = accessLevelType.AccessLevelTypeName;

                //Master Function Details
                List<FunctionVM> masterFunctionVMList = new List<FunctionVM>();
                List<tblFunction> masterFunctionList = functionList.Where(x => x.tblFunctionType.FunctionTypeName == "Master").ToList();

                foreach (var masterFunction in masterFunctionList)
                {
                    FunctionVM masterFunctionVM = new FunctionVM();
                    masterFunctionVM.FunctionID = masterFunction.FunctionID;
                    masterFunctionVM.FunctionNumber = masterFunction.FunctionNumber;
                    masterFunctionVM.FunctionName = masterFunction.FunctionName;

                    if (existingFunctionAccessList.Any(x => x.FunctionID == masterFunction.FunctionID))
                    {
                        masterFunctionVM.IsChecked = true;
                    }
                    else
                    {
                        masterFunctionVM.IsChecked = false;
                    }

                    masterFunctionVMList.Add(masterFunctionVM);
                }

                roleVM.MasterFunctionDetails = masterFunctionVMList;

                //Transaction Function Details
                List<FunctionVM> transactionFunctionVMList = new List<FunctionVM>();
                List<tblFunction> transactionFunctionList = functionList.Where(x => x.tblFunctionType.FunctionTypeName == "Transaction").ToList();

                foreach (var transactionFunction in transactionFunctionList)
                {
                    FunctionVM transactionFunctionVM = new FunctionVM();
                    transactionFunctionVM.FunctionID = transactionFunction.FunctionID;
                    transactionFunctionVM.FunctionNumber = transactionFunction.FunctionNumber;
                    transactionFunctionVM.FunctionName = transactionFunction.FunctionName;

                    if (existingFunctionAccessList.Any(x => x.FunctionID == transactionFunction.FunctionID))
                    {
                        transactionFunctionVM.IsChecked = true;
                    }
                    else
                    {
                        transactionFunctionVM.IsChecked = false;
                    }

                    transactionFunctionVMList.Add(transactionFunctionVM);
                }

                roleVM.TransactionFunctionDetails = transactionFunctionVMList;

                //Enquiry Function Details
                List<FunctionVM> enquiryFunctionVMList = new List<FunctionVM>();
                List<tblFunction> enquiryFunctionList = functionList.Where(x => x.tblFunctionType.FunctionTypeName == "Enquiries").ToList();

                foreach (var enquiryFunction in enquiryFunctionList)
                {
                    FunctionVM enquiryFunctionVM = new FunctionVM();
                    enquiryFunctionVM.FunctionID = enquiryFunction.FunctionID;
                    enquiryFunctionVM.FunctionNumber = enquiryFunction.FunctionNumber;
                    enquiryFunctionVM.FunctionName = enquiryFunction.FunctionName;

                    if (existingFunctionAccessList.Any(x => x.FunctionID == enquiryFunction.FunctionID))
                    {
                        enquiryFunctionVM.IsChecked = true;
                    }
                    else
                    {
                        enquiryFunctionVM.IsChecked = false;
                    }

                    enquiryFunctionVMList.Add(enquiryFunctionVM);
                }

                roleVM.EnquiryFunctionDetails = enquiryFunctionVMList;

                //Report Function Details
                List<FunctionVM> reportFunctionVMList = new List<FunctionVM>();
                List<tblFunction> reportFunctionList = functionList.Where(x => x.tblFunctionType.FunctionTypeName == "Reports").ToList();

                foreach (var reportFunction in reportFunctionList)
                {
                    FunctionVM reportFunctionVM = new FunctionVM();
                    reportFunctionVM.FunctionID = reportFunction.FunctionID;
                    reportFunctionVM.FunctionNumber = reportFunction.FunctionNumber;
                    reportFunctionVM.FunctionName = reportFunction.FunctionName;

                    if (existingFunctionAccessList.Any(x => x.FunctionID == reportFunction.FunctionID))
                    {
                        reportFunctionVM.IsChecked = true;
                    }
                    else
                    {
                        reportFunctionVM.IsChecked = false;
                    }

                    reportFunctionVMList.Add(reportFunctionVM);
                }

                roleVM.ReportFunctionDetails = reportFunctionVMList;

                //Admin Function Details
                List<FunctionVM> adminFunctionVMList = new List<FunctionVM>();
                List<tblFunction> adminFunctionList = functionList.Where(x => x.tblFunctionType.FunctionTypeName == "Administration").ToList();

                foreach (var adminFunction in adminFunctionList)
                {
                    FunctionVM adminFunctionVM = new FunctionVM();
                    adminFunctionVM.FunctionID = adminFunction.FunctionID;
                    adminFunctionVM.FunctionNumber = adminFunction.FunctionNumber;
                    adminFunctionVM.FunctionName = adminFunction.FunctionName;

                    if (existingFunctionAccessList.Any(x => x.FunctionID == adminFunction.FunctionID))
                    {
                        adminFunctionVM.IsChecked = true;
                    }
                    else
                    {
                        adminFunctionVM.IsChecked = false;
                    }

                    adminFunctionVMList.Add(adminFunctionVM);
                }

                roleVM.AdminFunctionDetails = adminFunctionVMList;

                return roleVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
