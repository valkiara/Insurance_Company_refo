using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManageSetting
    {
        private UnitOfWork unitOfWork;
        public ManageSetting()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool SaveSetting(SettingVM settingVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblSetting setting = new tblSetting();
                    setting.DesignationID = settingVM.DesignationID;
                    setting.SettingCode = settingVM.SettingCode;
                    setting.SettingDesc = settingVM.SettingDescription;
                    setting.CreatedDate = DateTime.Now;
                    setting.CreatedBy = settingVM.CreatedBy;
                    unitOfWork.TblSettingRepository.Insert(setting);
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

        public bool UpdateSetting(SettingVM settingVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblSetting setting = unitOfWork.TblSettingRepository.GetByID(settingVM.SettingID);
                    setting.DesignationID = settingVM.DesignationID;
                    setting.SettingCode = settingVM.SettingCode;
                    setting.SettingDesc = settingVM.SettingDescription;
                    setting.ModifiedDate = DateTime.Now;
                    setting.ModifiedBy = settingVM.ModifiedBy;
                    unitOfWork.TblSettingRepository.Update(setting);
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

        public bool DeleteSetting(int settingID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblSetting setting = unitOfWork.TblSettingRepository.GetByID(settingID);
                    unitOfWork.TblSettingRepository.Delete(setting);
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

        public List<SettingVM> GetAllSettings()
        {
            try
            {
                var settingData = unitOfWork.TblSettingRepository.Get().ToList();

                List<SettingVM> settingList = new List<SettingVM>();

                foreach (var setting in settingData)
                {
                    SettingVM settingVM = new SettingVM();
                    settingVM.SettingID = setting.SettingID;
                    settingVM.DesignationID = setting.DesignationID != null ? Convert.ToInt32(setting.DesignationID) : 0;

                    if (settingVM.DesignationID > 0)
                    {
                        settingVM.DesignationName = setting.tblDesignation.Designation;
                    }

                    settingVM.SettingCode = setting.SettingCode;
                    settingVM.SettingDescription = setting.SettingDesc;
                    settingVM.CreatedDate = setting.CreatedDate != null ? setting.CreatedDate.ToString() : string.Empty;
                    settingVM.ModifiedDate = setting.ModifiedDate != null ? setting.ModifiedDate.ToString() : string.Empty;
                    settingVM.CreatedBy = setting.CreatedBy != null ? Convert.ToInt32(setting.CreatedBy) : 0;
                    settingVM.ModifiedBy = setting.ModifiedBy != null ? Convert.ToInt32(setting.ModifiedBy) : 0;

                    settingList.Add(settingVM);
                }

                return settingList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<SettingVM> GetAllSettingsByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var settingData = unitOfWork.TblSettingRepository.Get(x => x.tblDesignation.BUID == businessUnitID).ToList();

                List<SettingVM> settingList = new List<SettingVM>();

                foreach (var setting in settingData)
                {
                    SettingVM settingVM = new SettingVM();
                    settingVM.SettingID = setting.SettingID;
                    settingVM.DesignationID = setting.DesignationID != null ? Convert.ToInt32(setting.DesignationID) : 0;

                    if (settingVM.DesignationID > 0)
                    {
                        settingVM.DesignationName = setting.tblDesignation.Designation;
                    }

                    settingVM.SettingCode = setting.SettingCode;
                    settingVM.SettingDescription = setting.SettingDesc;
                    settingVM.CreatedDate = setting.CreatedDate != null ? setting.CreatedDate.ToString() : string.Empty;
                    settingVM.ModifiedDate = setting.ModifiedDate != null ? setting.ModifiedDate.ToString() : string.Empty;
                    settingVM.CreatedBy = setting.CreatedBy != null ? Convert.ToInt32(setting.CreatedBy) : 0;
                    settingVM.ModifiedBy = setting.ModifiedBy != null ? Convert.ToInt32(setting.ModifiedBy) : 0;

                    settingList.Add(settingVM);
                }

                return settingList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public SettingVM GetSettingByID(int settingID)
        {
            try
            {
                var settingData = unitOfWork.TblSettingRepository.GetByID(settingID);

                SettingVM settingVM = new SettingVM();
                settingVM.SettingID = settingData.SettingID;
                settingVM.DesignationID = settingData.DesignationID != null ? Convert.ToInt32(settingData.DesignationID) : 0;

                if (settingVM.DesignationID > 0)
                {
                    settingVM.DesignationName = settingData.tblDesignation.Designation;
                }

                settingVM.SettingCode = settingData.SettingCode;
                settingVM.SettingDescription = settingData.SettingDesc;
                settingVM.CreatedDate = settingData.CreatedDate != null ? settingData.CreatedDate.ToString() : string.Empty;
                settingVM.ModifiedDate = settingData.ModifiedDate != null ? settingData.ModifiedDate.ToString() : string.Empty;
                settingVM.CreatedBy = settingData.CreatedBy != null ? Convert.ToInt32(settingData.CreatedBy) : 0;
                settingVM.ModifiedBy = settingData.ModifiedBy != null ? Convert.ToInt32(settingData.ModifiedBy) : 0;

                return settingVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsSettingAvailable(int? settingID, string settingCode)
        {
            try
            {
                if (settingID != null && unitOfWork.TblSettingRepository.Get().Any(x => x.SettingCode.ToLower() == settingCode.ToLower() && x.SettingID != settingID))
                {
                    return true;
                }
                else if (settingID == null && unitOfWork.TblSettingRepository.Get().Any(x => x.SettingCode.ToLower() == settingCode.ToLower()))
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
