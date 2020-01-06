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
    public class LoadingTypeController : ApiController
    {
        ManageLoadingType manageLoadingType = new ManageLoadingType();

        [HttpPost()]
        [ActionName("SaveLoadingType")]
        public IHttpActionResult SaveLoadingType([FromBody]JObject data)
        {
            try
            {
                string description = !string.IsNullOrEmpty(data.SelectToken("Description").Value<string>()) ? data.SelectToken("Description").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;
                if (!manageLoadingType.IsLoadingAvailable(null, description))
                {
                    LoadingTypeVM loadingTypeVM = new LoadingTypeVM();
                    loadingTypeVM.Description = description;
                    loadingTypeVM.CreatedBy = userID;

                    bool status = manageLoadingType.SaveLoadingType(loadingTypeVM);

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
                    return Json(new { status = false, message = "Loading Excess Type Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateLoadingType")]
        public IHttpActionResult UpdateLoadingType([FromBody]JObject data)
        {
            try
            {
                int loadingTypeID = !string.IsNullOrEmpty(data.SelectToken("LoadingTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("LoadingTypeID").Value<string>()) : 0;
                string description = !string.IsNullOrEmpty(data.SelectToken("Description").Value<string>()) ? data.SelectToken("Description").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;
                if (!manageLoadingType.IsLoadingAvailable(loadingTypeID, description))
                {
                    LoadingTypeVM loadingTypeVM = new LoadingTypeVM();
                loadingTypeVM.LoadingTypeID = loadingTypeID;
                loadingTypeVM.Description = description;
                loadingTypeVM.ModifiedBy = userID;

                bool status = manageLoadingType.UpdateLoadingType(loadingTypeVM);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully Updated" });
                }
                else
                {
                    return Json(new { status = false, message = "Update Failed" });
                }
                }
                else
                {
                    return Json(new { status = false, message = "Loading Excess Type Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteLoadingType")]
        public IHttpActionResult DeleteLoadingType([FromBody]JObject data)
        {
            try
            {
                int loadingTypeID = !string.IsNullOrEmpty(data.SelectToken("LoadingTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("LoadingTypeID").Value<string>()) : 0;
                bool status = manageLoadingType.DeleteLoadingType(loadingTypeID);

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
        [ActionName("GetAllLoadingTypes")]
        public IHttpActionResult GetAllLoadingTypes()
        {
            try
            {
                var loadingTypeList = manageLoadingType.GetAllLoadingTypes();
                return Json(new
                {
                    status = true,
                    data = loadingTypeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetLoadingTypeByID")]
        public IHttpActionResult GetLoadingTypeByID([FromBody]JObject data)
        {
            try
            {
                int loadingTypeID = !string.IsNullOrEmpty(data.SelectToken("LoadingTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("LoadingTypeID").Value<string>()) : 0;
                var loadingType = manageLoadingType.GetLoadingTypeByID(loadingTypeID);
                return Json(new
                {
                    status = true,
                    data = loadingType
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
