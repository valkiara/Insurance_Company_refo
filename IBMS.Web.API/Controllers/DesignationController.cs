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
    public class DesignationController : ApiController
    {
        ManageDesignation manageDesignation = new ManageDesignation();

        [HttpPost()]
        [ActionName("SaveDesignation")]
        public IHttpActionResult SaveDesignation([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("businessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("businessUnitID").Value<string>()) : 0;
                string designationName = !string.IsNullOrEmpty(data.SelectToken("designationName").Value<string>()) ? data.SelectToken("designationName").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("userID").Value<string>()) ? Convert.ToInt32(data.SelectToken("userID").Value<string>()) : 0;

                if (!manageDesignation.IsDesignationAvailable(null, designationName))
                {
                    DesignationVM designationVM = new DesignationVM();
                    designationVM.BusinessUnitID = businessUnitID;
                    designationVM.DesignationName = designationName;
                    designationVM.CreatedBy = userID;

                    bool status = manageDesignation.SaveDesignation(designationVM);

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
                    return Json(new { status = false, message = "Designation Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateDesignation")]
        public IHttpActionResult UpdateDesignation([FromBody]JObject data)
        {
            try
            {
                int designationID = !string.IsNullOrEmpty(data.SelectToken("designationID").Value<string>()) ? Convert.ToInt32(data.SelectToken("designationID").Value<string>()) : 0;
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("businessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("businessUnitID").Value<string>()) : 0;
                string designationName = !string.IsNullOrEmpty(data.SelectToken("designationName").Value<string>()) ? data.SelectToken("designationName").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("userID").Value<string>()) ? Convert.ToInt32(data.SelectToken("userID").Value<string>()) : 0;

                if (!manageDesignation.IsDesignationAvailable(designationID, designationName))
                {
                    DesignationVM designationVM = new DesignationVM();
                    designationVM.DesignationID = designationID;
                    designationVM.BusinessUnitID = businessUnitID;
                    designationVM.DesignationName = designationName;
                    designationVM.ModifiedBy = userID;

                    bool status = manageDesignation.UpdateDesignation(designationVM);

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
                    return Json(new { status = false, message = "Designation Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteDesignation")]
        public IHttpActionResult DeleteDesignation([FromBody]JObject data)
        {
            try
            {
                int designationID = !string.IsNullOrEmpty(data.SelectToken("designationID").Value<string>()) ? Convert.ToInt32(data.SelectToken("designationID").Value<string>()) : 0;
                bool status = manageDesignation.DeleteDesignation(designationID);

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
        [ActionName("GetAllDesignations")]
        public IHttpActionResult GetAllDesignations()
        {
            try
            {
                var designationList = manageDesignation.GetAllDesignations();
                return Json(new
                {
                    status = true,
                    data = designationList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllDesignationsByBUID")]
        public IHttpActionResult GetAllDesignationsByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var designationList = manageDesignation.GetAllDesignationsByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = designationList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetDesignationByID")]
        public IHttpActionResult GetDesignationByID([FromBody]JObject data)
        {
            try
            {
                int designationID = !string.IsNullOrEmpty(data.SelectToken("designationID").Value<string>()) ? Convert.ToInt32(data.SelectToken("designationID").Value<string>()) : 0;
                var designation = manageDesignation.GetDesignationByID(designationID);
                return Json(new
                {
                    status = true,
                    data = designation
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
