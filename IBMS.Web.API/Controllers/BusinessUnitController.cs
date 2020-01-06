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
    public class BusinessUnitController : ApiController
    {
        ManageBusinessUnit manageBusinessUnit = new ManageBusinessUnit();

        [HttpPost()]
        [ActionName("SaveBusinessUnit")]
        public IHttpActionResult SaveBusinessUnit([FromBody]JObject data)
        {
            try
            {
                string businessUnit = !string.IsNullOrEmpty(data.SelectToken("businessUnit").Value<string>()) ? data.SelectToken("businessUnit").Value<string>() : string.Empty;
                int companyID = !string.IsNullOrEmpty(data.SelectToken("companyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("companyID").Value<string>()) : 0;
                bool isActive = !string.IsNullOrEmpty(data.SelectToken("isActive").Value<string>()) ? Convert.ToBoolean(data.SelectToken("isActive").Value<string>()) : false;
                int userID = !string.IsNullOrEmpty(data.SelectToken("userID").Value<string>()) ? Convert.ToInt32(data.SelectToken("userID").Value<string>()) : 0;

                if (!manageBusinessUnit.IsBusinessUnitAvailable(null, businessUnit))
                {
                    BusinessUnitVM businessUnitVM = new BusinessUnitVM();
                    businessUnitVM.BusinessUnit = businessUnit;
                    businessUnitVM.CompanyID = companyID;
                    businessUnitVM.IsActive = isActive;
                    businessUnitVM.CreatedBy = userID;

                    bool status = manageBusinessUnit.SaveBusinessUnit(businessUnitVM);

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
                    return Json(new { status = false, message = "Business Unit Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateBusinessUnit")]
        public IHttpActionResult UpdateBusinessUnit([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("businessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("businessUnitID").Value<string>()) : 0;
                string businessUnit = !string.IsNullOrEmpty(data.SelectToken("businessUnit").Value<string>()) ? data.SelectToken("businessUnit").Value<string>() : string.Empty;
                int companyID = !string.IsNullOrEmpty(data.SelectToken("companyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("companyID").Value<string>()) : 0;
                bool isActive = !string.IsNullOrEmpty(data.SelectToken("isActive").Value<string>()) ? Convert.ToBoolean(data.SelectToken("isActive").Value<string>()) : false;
                int userID = !string.IsNullOrEmpty(data.SelectToken("userID").Value<string>()) ? Convert.ToInt32(data.SelectToken("userID").Value<string>()) : 0;

                if (!manageBusinessUnit.IsBusinessUnitAvailable(businessUnitID, businessUnit))
                {
                    BusinessUnitVM businessUnitVM = new BusinessUnitVM();
                    businessUnitVM.BusinessUnitID = businessUnitID;
                    businessUnitVM.BusinessUnit = businessUnit;
                    businessUnitVM.CompanyID = companyID;
                    businessUnitVM.IsActive = isActive;
                    businessUnitVM.ModifiedBy = userID;

                    bool status = manageBusinessUnit.UpdateBusinessUnit(businessUnitVM);

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
                    return Json(new { status = false, message = "Business Unit Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteBusinessUnit")]
        public IHttpActionResult DeleteBusinessUnit([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("businessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("businessUnitID").Value<string>()) : 0;
                bool status = manageBusinessUnit.DeleteBusinessUnit(businessUnitID);

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
        [ActionName("GetAllBusinessUnits")]
        public IHttpActionResult GetAllBusinessUnits()
        {
            try
            {
                var businessUnitList = manageBusinessUnit.GetAllBusinessUnits();
                return Json(new
                {
                    status = true,
                    data = businessUnitList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetBusinessUnitsByCompanyID")]
        public IHttpActionResult GetBusinessUnitsByCompanyID([FromBody]JObject data)
        {
            try
            {
                int companyID = !string.IsNullOrEmpty(data.SelectToken("companyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("companyID").Value<string>()) : 0;
                var businessUnitList = manageBusinessUnit.GetBusinessUnitsByCompanyID(companyID);
                return Json(new
                {
                    status = true,
                    data = businessUnitList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetBusinessUnitByID")]
        public IHttpActionResult GetBusinessUnitByID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("businessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("businessUnitID").Value<string>()) : 0;
                var businessUnit = manageBusinessUnit.GetBusinessUnitByID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = businessUnit
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
