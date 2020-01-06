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
    public class InsClassController : ApiController
    {
        ManageInsuranceClass manageInsuranceClass = new ManageInsuranceClass();

        #region Insurance Class
        [HttpPost()]
        [ActionName("SaveInsClass")]
        public IHttpActionResult SaveInsClass([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("businessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("businessUnitID").Value<string>()) : 0;
                string insuranceCode = !string.IsNullOrEmpty(data.SelectToken("insuranceCode").Value<string>()) ? data.SelectToken("insuranceCode").Value<string>() : string.Empty;
                string description = !string.IsNullOrEmpty(data.SelectToken("description").Value<string>()) ? data.SelectToken("description").Value<string>() : string.Empty;
                bool isActive = !string.IsNullOrEmpty(data.SelectToken("isActive").Value<string>()) ? Convert.ToBoolean(data.SelectToken("isActive").Value<string>()) : false;
                int userID = !string.IsNullOrEmpty(data.SelectToken("userID").Value<string>()) ? Convert.ToInt32(data.SelectToken("userID").Value<string>()) : 0;

                if (!manageInsuranceClass.IsInsClassAvailable(null, insuranceCode))
                {
                    InsuranceClassVM insuranceClassVM = new InsuranceClassVM();
                    insuranceClassVM.BusinessUnitID = businessUnitID;
                    insuranceClassVM.InsuranceCode = insuranceCode;
                    insuranceClassVM.Description = description;
                    insuranceClassVM.IsActive = isActive;
                    insuranceClassVM.CreatedBy = userID;

                    bool status = manageInsuranceClass.SaveInsuranceClass(insuranceClassVM);

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
                    return Json(new { status = false, message = "Insurance Code already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateInsClass")]
        public IHttpActionResult UpdateInsClass([FromBody]JObject data)
        {
            try
            {
                int insClassID = !string.IsNullOrEmpty(data.SelectToken("insClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("insClassID").Value<string>()) : 0;
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("businessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("businessUnitID").Value<string>()) : 0;
                string insuranceCode = !string.IsNullOrEmpty(data.SelectToken("insuranceCode").Value<string>()) ? data.SelectToken("insuranceCode").Value<string>() : string.Empty;
                string description = !string.IsNullOrEmpty(data.SelectToken("description").Value<string>()) ? data.SelectToken("description").Value<string>() : string.Empty;
                bool isActive = !string.IsNullOrEmpty(data.SelectToken("isActive").Value<string>()) ? Convert.ToBoolean(data.SelectToken("isActive").Value<string>()) : false;
                int userID = !string.IsNullOrEmpty(data.SelectToken("userID").Value<string>()) ? Convert.ToInt32(data.SelectToken("userID").Value<string>()) : 0;

                if (!manageInsuranceClass.IsInsClassAvailable(insClassID, insuranceCode))
                {
                    InsuranceClassVM insuranceClassVM = new InsuranceClassVM();
                    insuranceClassVM.InsuranceClassID = insClassID;
                    insuranceClassVM.BusinessUnitID = businessUnitID;
                    insuranceClassVM.InsuranceCode = insuranceCode;
                    insuranceClassVM.Description = description;
                    insuranceClassVM.IsActive = isActive;
                    insuranceClassVM.ModifiedBy = userID;

                    bool status = manageInsuranceClass.UpdateInsuranceClass(insuranceClassVM);

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
                    return Json(new { status = false, message = "Insurance Code already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteInsClass")]
        public IHttpActionResult DeleteInsClass([FromBody]JObject data)
        {
            try
            {
                int insClassID = !string.IsNullOrEmpty(data.SelectToken("insClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("insClassID").Value<string>()) : 0;
                bool status = manageInsuranceClass.DeleteInsuranceClass(insClassID);

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
        [ActionName("GetAllInsClasses")]
        public IHttpActionResult GetAllInsClasses()
        {
            try
            {
                var insClassList = manageInsuranceClass.GetAllInsuranceClasses();
                return Json(new
                {
                    status = true,
                    data = insClassList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetInsClassesByBusinessUnitID")]
        public IHttpActionResult GetInsClassesByBusinessUnitID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("businessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("businessUnitID").Value<string>()) : 0;
                var insClassList = manageInsuranceClass.GetInsuranceClassesByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = insClassList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetInsClassByID")]
        public IHttpActionResult GetInsClassByID([FromBody]JObject data)
        {
            try
            {
                int insClassID = !string.IsNullOrEmpty(data.SelectToken("insClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("insClassID").Value<string>()) : 0;
                var insClass = manageInsuranceClass.GetInsuranceClassByID(insClassID);
                return Json(new
                {
                    status = true,
                    data = insClass
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        #endregion

        #region Insurance Sub Class
        [HttpPost()]
        [ActionName("SaveInsSubClass")]
        public IHttpActionResult SaveInsSubClass([FromBody]JObject data)
        {
            try
            {
                int insClassID = !string.IsNullOrEmpty(data.SelectToken("insClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("insClassID").Value<string>()) : 0;
                string description = !string.IsNullOrEmpty(data.SelectToken("description").Value<string>()) ? data.SelectToken("description").Value<string>() : string.Empty;
                bool isActive = !string.IsNullOrEmpty(data.SelectToken("isActive").Value<string>()) ? Convert.ToBoolean(data.SelectToken("isActive").Value<string>()) : false;
                int userID = !string.IsNullOrEmpty(data.SelectToken("userID").Value<string>()) ? Convert.ToInt32(data.SelectToken("userID").Value<string>()) : 0;

                InsuranceSubClassVM insuranceSubClassVM = new InsuranceSubClassVM();
                insuranceSubClassVM.InsuranceClassID = insClassID;
                insuranceSubClassVM.Description = description;
                insuranceSubClassVM.IsActive = isActive;
                insuranceSubClassVM.CreatedBy = userID;

                bool status = manageInsuranceClass.SaveInsuranceSubClass(insuranceSubClassVM);

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
        [ActionName("UpdateInsSubClass")]
        public IHttpActionResult UpdateInsSubClass([FromBody]JObject data)
        {
            try
            {
                int insSubClassID = !string.IsNullOrEmpty(data.SelectToken("insSubClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("insSubClassID").Value<string>()) : 0;
                int insClassID = !string.IsNullOrEmpty(data.SelectToken("insClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("insClassID").Value<string>()) : 0;
                string description = !string.IsNullOrEmpty(data.SelectToken("description").Value<string>()) ? data.SelectToken("description").Value<string>() : string.Empty;
                bool isActive = !string.IsNullOrEmpty(data.SelectToken("isActive").Value<string>()) ? Convert.ToBoolean(data.SelectToken("isActive").Value<string>()) : false;
                int userID = !string.IsNullOrEmpty(data.SelectToken("userID").Value<string>()) ? Convert.ToInt32(data.SelectToken("userID").Value<string>()) : 0;

                InsuranceSubClassVM insuranceSubClassVM = new InsuranceSubClassVM();
                insuranceSubClassVM.InsuranceSubClassID = insSubClassID;
                insuranceSubClassVM.InsuranceClassID = insClassID;
                insuranceSubClassVM.Description = description;
                insuranceSubClassVM.IsActive = isActive;
                insuranceSubClassVM.ModifiedBy = userID;

                bool status = manageInsuranceClass.UpdateInsuranceSubClass(insuranceSubClassVM);

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
        [ActionName("DeleteInsSubClass")]
        public IHttpActionResult DeleteInsSubClass([FromBody]JObject data)
        {
            try
            {
                int insSubClassID = !string.IsNullOrEmpty(data.SelectToken("insSubClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("insSubClassID").Value<string>()) : 0;
                bool status = manageInsuranceClass.DeleteInsuranceSubClass(insSubClassID);

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
        [ActionName("GetAllInsSubClasses")]
        public IHttpActionResult GetAllInsSubClasses()
        {
            try
            {
                var insSubClassList = manageInsuranceClass.GetAllInsuranceSubClasses();
                return Json(new
                {
                    status = true,
                    data = insSubClassList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllInsSubClassesByInsClass")]
        public IHttpActionResult GetAllInsSubClassesByInsClass([FromBody]JObject data)
        {
            try
            {
                int insClassID = !string.IsNullOrEmpty(data.SelectToken("insClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("insClassID").Value<string>()) : 0;
                var insSubClassList = manageInsuranceClass.GetAllInsuranceSubClassesByInsuranceClass(insClassID);
                return Json(new
                {
                    status = true,
                    data = insSubClassList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllInsSubClassesByBusinessUnit")]
        public IHttpActionResult GetAllInsSubClassesByBusinessUnit([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("businessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("businessUnitID").Value<string>()) : 0;
                var insSubClassList = manageInsuranceClass.GetAllInsuranceSubClassesByBusinessUnit(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = insSubClassList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetInsSubClassByID")]
        public IHttpActionResult GetInsSubClassByID([FromBody]JObject data)
        {
            try
            {
                int insSubClassID = !string.IsNullOrEmpty(data.SelectToken("insSubClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("insSubClassID").Value<string>()) : 0;
                var insSubClass = manageInsuranceClass.GetInsuranceSubClassByID(insSubClassID);
                return Json(new
                {
                    status = true,
                    data = insSubClass
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
