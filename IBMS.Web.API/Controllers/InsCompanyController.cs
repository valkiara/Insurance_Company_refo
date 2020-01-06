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
    public class InsCompanyController : ApiController
    {
        ManageInsuranceCompany manageInsCompany = new ManageInsuranceCompany();

        [HttpPost()]
        [ActionName("SaveInsuranceCompany")]
        public IHttpActionResult SaveInsuranceCompany([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("businessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("businessUnitID").Value<string>()) : 0;
                string insCompanyName = !string.IsNullOrEmpty(data.SelectToken("insCompanyName").Value<string>()) ? data.SelectToken("insCompanyName").Value<string>() : string.Empty;
                string address1 = !string.IsNullOrEmpty(data.SelectToken("address1").Value<string>()) ? data.SelectToken("address1").Value<string>() : string.Empty;
                string address2 = !string.IsNullOrEmpty(data.SelectToken("address2").Value<string>()) ? data.SelectToken("address2").Value<string>() : string.Empty;
                string address3 = !string.IsNullOrEmpty(data.SelectToken("address3").Value<string>()) ? data.SelectToken("address3").Value<string>() : string.Empty;
                string contactPerson = !string.IsNullOrEmpty(data.SelectToken("contactPerson").Value<string>()) ? data.SelectToken("contactPerson").Value<string>() : string.Empty;
                string contactNo = !string.IsNullOrEmpty(data.SelectToken("contactNo").Value<string>()) ? data.SelectToken("contactNo").Value<string>() : string.Empty;
                string email = !string.IsNullOrEmpty(data.SelectToken("email").Value<string>()) ? data.SelectToken("email").Value<string>() : string.Empty;
                string fax = !string.IsNullOrEmpty(data.SelectToken("fax").Value<string>()) ? data.SelectToken("fax").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("userID").Value<string>()) ? Convert.ToInt32(data.SelectToken("userID").Value<string>()) : 0;

                if (!manageInsCompany.IsInsCompanyAvailable(null, insCompanyName))
                {
                    InsuranceCompanyVM insCompanyVM = new InsuranceCompanyVM();
                    insCompanyVM.BusinessUnitID = businessUnitID;
                    insCompanyVM.InsuranceCompanyName = insCompanyName;
                    insCompanyVM.Address1 = address1;
                    insCompanyVM.Address2 = address2;
                    insCompanyVM.Address3 = address3;
                    insCompanyVM.ContactPerson = contactPerson;
                    insCompanyVM.ContactNo = contactNo;
                    insCompanyVM.Email = email;
                    insCompanyVM.Fax = fax;
                    insCompanyVM.CreatedBy = userID;

                    bool status = manageInsCompany.SaveInsuranceCompany(insCompanyVM);

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
                    return Json(new { status = false, message = "Insurance Company Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateInsuranceCompany")]
        public IHttpActionResult UpdateInsuranceCompany([FromBody]JObject data)
        {
            try
            {
                int insCompanyID = !string.IsNullOrEmpty(data.SelectToken("insCompanyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("insCompanyID").Value<string>()) : 0;
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("businessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("businessUnitID").Value<string>()) : 0;
                string insCompanyName = !string.IsNullOrEmpty(data.SelectToken("insCompanyName").Value<string>()) ? data.SelectToken("insCompanyName").Value<string>() : string.Empty;
                string address1 = !string.IsNullOrEmpty(data.SelectToken("address1").Value<string>()) ? data.SelectToken("address1").Value<string>() : string.Empty;
                string address2 = !string.IsNullOrEmpty(data.SelectToken("address2").Value<string>()) ? data.SelectToken("address2").Value<string>() : string.Empty;
                string address3 = !string.IsNullOrEmpty(data.SelectToken("address3").Value<string>()) ? data.SelectToken("address3").Value<string>() : string.Empty;
                string contactPerson = !string.IsNullOrEmpty(data.SelectToken("contactPerson").Value<string>()) ? data.SelectToken("contactPerson").Value<string>() : string.Empty;
                string contactNo = !string.IsNullOrEmpty(data.SelectToken("contactNo").Value<string>()) ? data.SelectToken("contactNo").Value<string>() : string.Empty;
                string email = !string.IsNullOrEmpty(data.SelectToken("email").Value<string>()) ? data.SelectToken("email").Value<string>() : string.Empty;
                string fax = !string.IsNullOrEmpty(data.SelectToken("fax").Value<string>()) ? data.SelectToken("fax").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("userID").Value<string>()) ? Convert.ToInt32(data.SelectToken("userID").Value<string>()) : 0;

                if (!manageInsCompany.IsInsCompanyAvailable(insCompanyID, insCompanyName))
                {
                    InsuranceCompanyVM insCompanyVM = new InsuranceCompanyVM();
                    insCompanyVM.InsuranceCompanyID = insCompanyID;
                    insCompanyVM.BusinessUnitID = businessUnitID;
                    insCompanyVM.InsuranceCompanyName = insCompanyName;
                    insCompanyVM.Address1 = address1;
                    insCompanyVM.Address2 = address2;
                    insCompanyVM.Address3 = address3;
                    insCompanyVM.ContactPerson = contactPerson;
                    insCompanyVM.ContactNo = contactNo;
                    insCompanyVM.Email = email;
                    insCompanyVM.Fax = fax;
                    insCompanyVM.ModifiedBy = userID;

                    bool status = manageInsCompany.UpdateInsuranceCompany(insCompanyVM);

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
                    return Json(new { status = false, message = "Insurance Company Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteInsuranceCompany")]
        public IHttpActionResult DeleteInsuranceCompany([FromBody]JObject data)
        {
            try
            {
                int insCompanyID = !string.IsNullOrEmpty(data.SelectToken("insCompanyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("insCompanyID").Value<string>()) : 0;
                bool status = manageInsCompany.DeleteInsuranceCompany(insCompanyID);

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
        [ActionName("GetAllInsuranceCompanies")]
        public IHttpActionResult GetAllInsuranceCompanies()
        {
            try
            {
                var insCompanyList = manageInsCompany.GetAllInsuranceCompanies();
                return Json(new
                {
                    status = true,
                    data = insCompanyList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }


        [HttpPost()]
        [ActionName("GetInsuranceCompaniesByBusinessUnitID")]
        public IHttpActionResult GetInsuranceCompaniesByBusinessUnitID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("businessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("businessUnitID").Value<string>()) : 0;
                var insCompanyList = manageInsCompany.GetInsuranceCompaniesByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = insCompanyList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetInsuranceCompanyByID")]
        public IHttpActionResult GetInsuranceCompanyByID([FromBody]JObject data)
        {
            try
            {
                int insCompanyID = !string.IsNullOrEmpty(data.SelectToken("insCompanyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("insCompanyID").Value<string>()) : 0;
                var insCompany = manageInsCompany.GetInsuranceCompanyByID(insCompanyID);
                return Json(new
                {
                    status = true,
                    data = insCompany
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
