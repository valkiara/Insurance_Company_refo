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
    public class CommonInsScopeController : ApiController
    {
        ManageCommonInsuranceScope manageCommonInsScope = new ManageCommonInsuranceScope();

        [HttpPost()]
        [ActionName("SaveCommonInsScope")]
        public IHttpActionResult SaveCommonInsScope([FromBody]JObject data)
        {
            try
            {
                string description = !string.IsNullOrEmpty(data.SelectToken("Description").Value<string>()) ? data.SelectToken("Description").Value<string>() : string.Empty;
                int insClassID = !string.IsNullOrEmpty(data.SelectToken("InsClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InsClassID").Value<string>()) : 0;
                int insSubClassID = !string.IsNullOrEmpty(data.SelectToken("InsSubClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InsSubClassID").Value<string>()) : 0;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                CommonInsuranceScopeVM commonInsuranceScopeVM = new CommonInsuranceScopeVM();
                commonInsuranceScopeVM.Description = description;
                commonInsuranceScopeVM.InsuranceClassID = insClassID;
                commonInsuranceScopeVM.InsuranceSubClassID = insSubClassID;
                commonInsuranceScopeVM.CreatedBy = userID;

                bool status = manageCommonInsScope.SaveCommonInsScope(commonInsuranceScopeVM);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully Saved" });
                }
                else
                {
                    return Json(new { status = false, message = "Save Failed" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateCommonInsScope")]
        public IHttpActionResult UpdateCommonInsScope([FromBody]JObject data)
        {
            try
            {
                int commonInsScopeID = !string.IsNullOrEmpty(data.SelectToken("CommonInsScopeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("CommonInsScopeID").Value<string>()) : 0;
                string description = !string.IsNullOrEmpty(data.SelectToken("Description").Value<string>()) ? data.SelectToken("Description").Value<string>() : string.Empty;
                int insClassID = !string.IsNullOrEmpty(data.SelectToken("InsClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InsClassID").Value<string>()) : 0;
                int insSubClassID = !string.IsNullOrEmpty(data.SelectToken("InsSubClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InsSubClassID").Value<string>()) : 0;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                CommonInsuranceScopeVM commonInsuranceScopeVM = new CommonInsuranceScopeVM();
                commonInsuranceScopeVM.CommonInsuranceScopeID = commonInsScopeID;
                commonInsuranceScopeVM.Description = description;
                commonInsuranceScopeVM.InsuranceClassID = insClassID;
                commonInsuranceScopeVM.InsuranceSubClassID = insSubClassID;
                commonInsuranceScopeVM.ModifiedBy = userID;

                bool status = manageCommonInsScope.UpdateCommonInsScope(commonInsuranceScopeVM);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully updated" });
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
        [ActionName("DeleteCommonInsScope")]
        public IHttpActionResult DeleteCommonInsScope([FromBody]JObject data)
        {
            try
            {
                int commonInsScopeID = !string.IsNullOrEmpty(data.SelectToken("CommonInsScopeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("CommonInsScopeID").Value<string>()) : 0;
                bool status = manageCommonInsScope.DeleteCommonInsScope(commonInsScopeID);

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
        [ActionName("GetAllCommonInsScopes")]
        public IHttpActionResult GetAllCommonInsScopes()
        {
            try
            {
                var commonInsScopeList = manageCommonInsScope.GetAllCommonInsScopes();
                return Json(new
                {
                    status = true,
                    data = commonInsScopeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllCommonInsScopesByBUID")]
        public IHttpActionResult GetAllCommonInsScopesByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var commonInsScopeList = manageCommonInsScope.GetAllCommonInsScopesByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = commonInsScopeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }


        [HttpPost()]
        [ActionName("GetAllCommonInsScopesByInsClass")]
        public IHttpActionResult GetAllCommonInsScopesByInsClass([FromBody]JObject data)
        {
            try
            {
                int insClassID = !string.IsNullOrEmpty(data.SelectToken("insClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("insClassID").Value<string>()) : 0;
                var commonInsScopeList = manageCommonInsScope.GetAllCommonInsScopesByInsClass(insClassID);
                return Json(new
                {
                    status = true,
                    data = commonInsScopeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllCommonInsScopesByInsSubClass")]
        public IHttpActionResult GetAllCommonInsScopesByInsSubClass([FromBody]JObject data)
        {
            try
            {
                int insSubClassID = !string.IsNullOrEmpty(data.SelectToken("insSubClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("insSubClassID").Value<string>()) : 0;
                var commonInsScopeList = manageCommonInsScope.GetAllCommonInsScopesByInsSubClass(insSubClassID);
                return Json(new
                {
                    status = true,
                    data = commonInsScopeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetCommonInsScopeByID")]
        public IHttpActionResult GetCommonInsScopeByID([FromBody]JObject data)
        {
            try
            {
                int commonInsScopeID = !string.IsNullOrEmpty(data.SelectToken("CommonInsScopeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("CommonInsScopeID").Value<string>()) : 0;
                var commonInsScope = manageCommonInsScope.GetCommonInsScopeByID(commonInsScopeID);
                return Json(new
                {
                    status = true,
                    data = commonInsScope
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
