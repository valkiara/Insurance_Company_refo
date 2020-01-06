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
    public class InternalPolicyNumController : ApiController
    {
        ManageInternalPolicyNumSetup manageInternalPolicyNumSetup = new ManageInternalPolicyNumSetup();

        [HttpPost()]
        [ActionName("SaveInternalPolicyNumSetup")]
        public IHttpActionResult SaveInternalPolicyNumSetup([FromBody]JObject data)
        {
            try
            {
                string internalPolicyNumber = !string.IsNullOrEmpty(data.SelectToken("InternalPolicyNumber").Value<string>()) ? data.SelectToken("InternalPolicyNumber").Value<string>() : string.Empty;
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageInternalPolicyNumSetup.IsInternalPolicyNumberAvailable(null, internalPolicyNumber))
                {
                    InternalPolicyNumSetupVM internalPolicyNumSetupVM = new InternalPolicyNumSetupVM();
                    internalPolicyNumSetupVM.InternalPolicyNumber = internalPolicyNumber;
                    internalPolicyNumSetupVM.BusinessUnitID = businessUnitID;
                    internalPolicyNumSetupVM.CreatedBy = userID;

                    bool status = manageInternalPolicyNumSetup.SaveInternalPolicyNumSetup(internalPolicyNumSetupVM);

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
                    return Json(new { status = false, message = "Internal Policy Number already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateInternalPolicyNumSetup")]
        public IHttpActionResult UpdateInternalPolicyNumSetup([FromBody]JObject data)
        {
            try
            {
                int internalPolicyNumSetupID = !string.IsNullOrEmpty(data.SelectToken("InternalPolicyNumSetupID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InternalPolicyNumSetupID").Value<string>()) : 0;
                string internalPolicyNumber = !string.IsNullOrEmpty(data.SelectToken("InternalPolicyNumber").Value<string>()) ? data.SelectToken("InternalPolicyNumber").Value<string>() : string.Empty;
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageInternalPolicyNumSetup.IsInternalPolicyNumberAvailable(internalPolicyNumSetupID, internalPolicyNumber))
                {
                    InternalPolicyNumSetupVM internalPolicyNumSetupVM = new InternalPolicyNumSetupVM();
                    internalPolicyNumSetupVM.InternalPolicyNumSetupID = internalPolicyNumSetupID;
                    internalPolicyNumSetupVM.InternalPolicyNumber = internalPolicyNumber;
                    internalPolicyNumSetupVM.BusinessUnitID = businessUnitID;
                    internalPolicyNumSetupVM.ModifiedBy = userID;

                    bool status = manageInternalPolicyNumSetup.UpdateInternalPolicyNumSetup(internalPolicyNumSetupVM);

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
                    return Json(new { status = false, message = "Internal Policy Number already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteInternalPolicyNumSetup")]
        public IHttpActionResult DeleteInternalPolicyNumSetup([FromBody]JObject data)
        {
            try
            {
                int internalPolicyNumSetupID = !string.IsNullOrEmpty(data.SelectToken("InternalPolicyNumSetupID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InternalPolicyNumSetupID").Value<string>()) : 0;
                bool status = manageInternalPolicyNumSetup.DeleteInternalPolicyNumSetup(internalPolicyNumSetupID);

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
        [ActionName("GetAllInternalPolicyNumSetups")]
        public IHttpActionResult GetAllInternalPolicyNumSetups()
        {
            try
            {
                var internalPolicyNumSetupList = manageInternalPolicyNumSetup.GetAllInternalPolicyNumSetups();
                return Json(new
                {
                    status = true,
                    data = internalPolicyNumSetupList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllInternalPolicyNumSetupsByBUID")]
        public IHttpActionResult GetAllInternalPolicyNumSetupsByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var internalPolicyNumSetupList = manageInternalPolicyNumSetup.GetAllInternalPolicyNumSetupsByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = internalPolicyNumSetupList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetInternalPolicyNumSetupByID")]
        public IHttpActionResult GetInternalPolicyNumSetupByID([FromBody]JObject data)
        {
            try
            {
                int internalPolicyNumSetupID = !string.IsNullOrEmpty(data.SelectToken("InternalPolicyNumSetupID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InternalPolicyNumSetupID").Value<string>()) : 0;
                var internalPolicyNumSetup = manageInternalPolicyNumSetup.GetInternalPolicyNumSetupByID(internalPolicyNumSetupID);
                return Json(new
                {
                    status = true,
                    data = internalPolicyNumSetup
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
