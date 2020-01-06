using IBMS.Service.AdminData;
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
    public class RoleController : ApiController
    {
        ManageRole manageRole = new ManageRole();

        #region Manage Role
        [HttpPost()]
        [ActionName("SaveAccessLevelType")]
        public IHttpActionResult SaveAgent([FromBody]JObject data)
        {
            try
            {
                string accessLevelTypeName = !string.IsNullOrEmpty(data.SelectToken("AccessLevelTypeName").Value<string>()) ? data.SelectToken("AccessLevelTypeName").Value<string>() : string.Empty;
                string description = !string.IsNullOrEmpty(data.SelectToken("Description").Value<string>()) ? data.SelectToken("Description").Value<string>() : string.Empty;

                if (!manageRole.IsAccessLevelTypeAvailable(null, accessLevelTypeName))
                {
                    AccessLevelTypeVM accessLevelTypeVM = new AccessLevelTypeVM();
                    accessLevelTypeVM.AccessLevelTypeName = accessLevelTypeName;
                    accessLevelTypeVM.Description = description;

                    bool status = manageRole.SaveAccessLevelType(accessLevelTypeVM);

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
                    return Json(new { status = false, message = "Access Level Type Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateAccessLevelType")]
        public IHttpActionResult UpdateAccessLevelType([FromBody]JObject data)
        {
            try
            {
                int accessLevelTypeID = !string.IsNullOrEmpty(data.SelectToken("AccessLevelTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("AccessLevelTypeID").Value<string>()) : 0;
                string accessLevelTypeName = !string.IsNullOrEmpty(data.SelectToken("AccessLevelTypeName").Value<string>()) ? data.SelectToken("AccessLevelTypeName").Value<string>() : string.Empty;
                string description = !string.IsNullOrEmpty(data.SelectToken("Description").Value<string>()) ? data.SelectToken("Description").Value<string>() : string.Empty;

                if (!manageRole.IsAccessLevelTypeAvailable(accessLevelTypeID, accessLevelTypeName))
                {
                    AccessLevelTypeVM accessLevelTypeVM = new AccessLevelTypeVM();
                    accessLevelTypeVM.AccessLevelTypeID = accessLevelTypeID;
                    accessLevelTypeVM.AccessLevelTypeName = accessLevelTypeName;
                    accessLevelTypeVM.Description = description;

                    bool status = manageRole.UpdateAccessLevelType(accessLevelTypeVM);

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
                    return Json(new { status = false, message = "Access Level Type Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteAccessLevelType")]
        public IHttpActionResult DeleteAgent([FromBody]JObject data)
        {
            try
            {
                int accessLevelTypeID = !string.IsNullOrEmpty(data.SelectToken("AccessLevelTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("AccessLevelTypeID").Value<string>()) : 0;
                bool status = manageRole.DeleteAccessLevelType(accessLevelTypeID);

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
        [ActionName("GetAllAccessLevelTypes")]
        public IHttpActionResult GetAllAccessLevelTypes()
        {
            try
            {
                var accessLevelTypeList = manageRole.GetAllAccessLevelTypes();
                return Json(new
                {
                    status = true,
                    data = accessLevelTypeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAccessLevelTypeByID")]
        public IHttpActionResult GetAccessLevelTypeByID([FromBody]JObject data)
        {
            try
            {
                int accessLevelTypeID = !string.IsNullOrEmpty(data.SelectToken("AccessLevelTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("AccessLevelTypeID").Value<string>()) : 0;
                var accessLevelType = manageRole.GetAccessLevelTypeByID(accessLevelTypeID);
                return Json(new
                {
                    status = true,
                    data = accessLevelType
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        #endregion

        #region Manage Role Functions
        [HttpPost()]
        [ActionName("UpdateRoleFunctions")]
        public IHttpActionResult UpdateRoleFunctions([FromBody]JObject data)
        {
            try
            {
                RoleVM roleVM = data.SelectToken("RoleObj").ToObject<RoleVM>();

                bool status = manageRole.UpdateFunctionAccessDetails(roleVM);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully Updated" });
                }
                else
                {
                    return Json(new { status = false, message = "Update Failed" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetRoleFunctionsByID")]
        public IHttpActionResult GetRoleFunctionsByID([FromBody]JObject data)
        {
            try
            {
                int accessLevelTypeID = !string.IsNullOrEmpty(data.SelectToken("AccessLevelTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("AccessLevelTypeID").Value<string>()) : 0;
                var roleFunctions = manageRole.GetFunctionAccessDetailsByID(accessLevelTypeID);

                return Json(new
                {
                    status = true,
                    data = roleFunctions
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        #endregion
    }
}
