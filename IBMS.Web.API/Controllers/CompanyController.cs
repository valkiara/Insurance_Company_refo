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
    public class CompanyController : ApiController
    {
        ManageCompany manageCompany = new ManageCompany();

        [HttpPost()]
        [ActionName("SaveCompany")]
        public IHttpActionResult SaveCompany([FromBody]JObject data)
        {
            try
            {
                string companyName = !string.IsNullOrEmpty(data.SelectToken("companyName").Value<string>()) ? data.SelectToken("companyName").Value<string>() : string.Empty;
                bool isActive = !string.IsNullOrEmpty(data.SelectToken("isActive").Value<string>()) ? Convert.ToBoolean(data.SelectToken("isActive").Value<string>()) : false;
                int userID = !string.IsNullOrEmpty(data.SelectToken("userID").Value<string>()) ? Convert.ToInt32(data.SelectToken("userID").Value<string>()) : 0;

                if (!manageCompany.IsCompanyAvailable(null, companyName))
                {
                    CompanyVM companyVM = new CompanyVM();
                    companyVM.CompanyName = companyName;
                    companyVM.IsActive = isActive;
                    companyVM.CreatedBy = userID;

                    bool status = manageCompany.SaveCompany(companyVM);

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
                    return Json(new { status = false, message = "Company Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateCompany")]
        public IHttpActionResult UpdateCompany([FromBody]JObject data)
        {
            try
            {
                int companyID = !string.IsNullOrEmpty(data.SelectToken("companyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("companyID").Value<string>()) : 0;
                string companyName = !string.IsNullOrEmpty(data.SelectToken("companyName").Value<string>()) ? data.SelectToken("companyName").Value<string>() : string.Empty;
                bool isActive = !string.IsNullOrEmpty(data.SelectToken("isActive").Value<string>()) ? Convert.ToBoolean(data.SelectToken("isActive").Value<string>()) : false;
                int userID = !string.IsNullOrEmpty(data.SelectToken("userID").Value<string>()) ? Convert.ToInt32(data.SelectToken("userID").Value<string>()) : 0;

                if (!manageCompany.IsCompanyAvailable(companyID, companyName))
                {
                    CompanyVM companyVM = new CompanyVM();
                    companyVM.CompanyID = companyID;
                    companyVM.CompanyName = companyName;
                    companyVM.IsActive = isActive;
                    companyVM.ModifiedBy = userID;

                    bool status = manageCompany.UpdateCompany(companyVM);

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
                    return Json(new { status = false, message = "Company Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteCompany")]
        public IHttpActionResult DeleteCompany([FromBody]JObject data)
        {
            try
            {
                int companyID = !string.IsNullOrEmpty(data.SelectToken("companyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("companyID").Value<string>()) : 0;
                bool status = manageCompany.DeleteCompany(companyID);

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
        [ActionName("GetAllCompanies")]
        public IHttpActionResult GetAllCompanies()
        {
            try
            {
                var companyList = manageCompany.GetAllCompanies();
                return Json(new
                {
                    status = true,
                    data = companyList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetCompanyByID")]
        public IHttpActionResult GetCompanyByID([FromBody]JObject data)
        {
            try
            {
                int companyID = !string.IsNullOrEmpty(data.SelectToken("companyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("companyID").Value<string>()) : 0;
                var company = manageCompany.GetCompanyByID(companyID);
                return Json(new
                {
                    status = true,
                    data = company
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
