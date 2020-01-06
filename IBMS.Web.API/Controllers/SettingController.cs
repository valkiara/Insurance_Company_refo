using IBMS.Service.MasterData;
using IBMS.Shared.ViewModel;
using IBMS.Web.API.Security;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IBMS.Web.API.Controllers
{
    [BasicAuthentication]
    public class SettingController : ApiController
    {
        ManageSetting manageSetting = new ManageSetting();

        [HttpPost()]
        [ActionName("SaveSetting")]
        public IHttpActionResult SaveSetting([FromBody]JObject data)
        {
            try
            {
                int designationID = !string.IsNullOrEmpty(data.SelectToken("DesignationID").Value<string>()) ? Convert.ToInt32(data.SelectToken("DesignationID").Value<string>()) : 0;
                string settingCode = !string.IsNullOrEmpty(data.SelectToken("SettingCode").Value<string>()) ? data.SelectToken("SettingCode").Value<string>() : string.Empty;
                string settingDesc = !string.IsNullOrEmpty(data.SelectToken("SettingDesc").Value<string>()) ? data.SelectToken("SettingDesc").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageSetting.IsSettingAvailable(null, settingCode))
                {
                    SettingVM settingVM = new SettingVM();
                    settingVM.DesignationID = designationID;
                    settingVM.SettingCode = settingCode;
                    settingVM.SettingDescription = settingDesc;
                    settingVM.CreatedBy = userID;

                    bool status = manageSetting.SaveSetting(settingVM);

                    if (status)
                    {
                        return Json(new { status = true, message = "Successfully Saved" });
                    }
                    else
                    {
                        return Json(new { status = false, message = "Save Failed" });
                    }
                }
                else
                {
                    return Json(new { status = false, message = "Setting Code already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateSetting")]
        public IHttpActionResult UpdateSetting([FromBody]JObject data)
        {
            try
            {
                int settingID = !string.IsNullOrEmpty(data.SelectToken("SettingID").Value<string>()) ? Convert.ToInt32(data.SelectToken("SettingID").Value<string>()) : 0;
                int designationID = !string.IsNullOrEmpty(data.SelectToken("DesignationID").Value<string>()) ? Convert.ToInt32(data.SelectToken("DesignationID").Value<string>()) : 0;
                string settingCode = !string.IsNullOrEmpty(data.SelectToken("SettingCode").Value<string>()) ? data.SelectToken("SettingCode").Value<string>() : string.Empty;
                string settingDesc = !string.IsNullOrEmpty(data.SelectToken("SettingDesc").Value<string>()) ? data.SelectToken("SettingDesc").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageSetting.IsSettingAvailable(settingID, settingCode))
                {
                    SettingVM settingVM = new SettingVM();
                    settingVM.SettingID = settingID;
                    settingVM.DesignationID = designationID;
                    settingVM.SettingCode = settingCode;
                    settingVM.SettingDescription = settingDesc;
                    settingVM.ModifiedBy = userID;

                    bool status = manageSetting.UpdateSetting(settingVM);

                    if (status)
                    {
                        return Json(new { status = true, message = "Successfully updated" });
                    }
                    else
                    {
                        return Json(new { status = false, message = "Update Failed" });
                    }
                }
                else
                {
                    return Json(new { status = false, message = "Setting Code already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteSetting")]
        public IHttpActionResult DeleteSetting([FromBody]JObject data)
        {
            try
            {
                int settingID = !string.IsNullOrEmpty(data.SelectToken("SettingID").Value<string>()) ? Convert.ToInt32(data.SelectToken("SettingID").Value<string>()) : 0;
                bool status = manageSetting.DeleteSetting(settingID);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully Deleted" });
                }
                else
                {
                    return Json(new { status = false, message = "Delete Failed" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllSettings")]
        public IHttpActionResult GetAllSettings()
        {
            try
            {
                var settingList = manageSetting.GetAllSettings();
                return Json(new
                {
                    status = true,
                    data = settingList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllSettingsByBUID")]
        public IHttpActionResult GetAllSettingsByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var settingList = manageSetting.GetAllSettingsByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = settingList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetSettingByID")]
        public IHttpActionResult GetSettingByID([FromBody]JObject data)
        {
            try
            {
                int settingID = !string.IsNullOrEmpty(data.SelectToken("SettingID").Value<string>()) ? Convert.ToInt32(data.SelectToken("SettingID").Value<string>()) : 0;
                var setting = manageSetting.GetSettingByID(settingID);
                return Json(new
                {
                    status = true,
                    data = setting
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
