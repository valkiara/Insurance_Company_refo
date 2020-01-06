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
    public class TaxController : ApiController
    {
        ManageTax manageTax = new ManageTax();

        #region Tax Type
        [HttpPost()]
        [ActionName("SaveTaxType")]
        public IHttpActionResult SaveTaxType([FromBody]JObject data)
        {
            try
            {
                string taxTypeCode = !string.IsNullOrEmpty(data.SelectToken("TaxTypeCode").Value<string>()) ? data.SelectToken("TaxTypeCode").Value<string>() : string.Empty;
                string description = !string.IsNullOrEmpty(data.SelectToken("Description").Value<string>()) ? data.SelectToken("Description").Value<string>() : string.Empty;
                double percentage = !string.IsNullOrEmpty(data.SelectToken("Percentage").Value<string>()) ? Convert.ToDouble(data.SelectToken("Percentage").Value<string>()) : 0;
                string expiryDate = !string.IsNullOrEmpty(data.SelectToken("ExpiryDate").Value<string>()) ? data.SelectToken("ExpiryDate").Value<string>() : string.Empty;
                bool isActive = !string.IsNullOrEmpty(data.SelectToken("IsActive").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsActive").Value<string>()) : false;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageTax.IsTaxTypeAvailable(null, taxTypeCode))
                {
                    TaxTypeVM taxTypeVM = new TaxTypeVM();
                    taxTypeVM.TaxTypeCode = taxTypeCode;
                    taxTypeVM.Description = description;
                    taxTypeVM.Percentage = percentage;
                    taxTypeVM.ExpiryDate = expiryDate;
                    taxTypeVM.IsActive = isActive;
                    taxTypeVM.CreatedBy = userID;

                    bool status = manageTax.SaveTaxType(taxTypeVM);

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
                    return Json(new { status = false, message = "Tax Type already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateTaxType")]
        public IHttpActionResult UpdateTaxType([FromBody]JObject data)
        {
            try
            {
                int taxTypeID = !string.IsNullOrEmpty(data.SelectToken("TaxTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("TaxTypeID").Value<string>()) : 0;
                string taxTypeCode = !string.IsNullOrEmpty(data.SelectToken("TaxTypeCode").Value<string>()) ? data.SelectToken("TaxTypeCode").Value<string>() : string.Empty;
                string description = !string.IsNullOrEmpty(data.SelectToken("Description").Value<string>()) ? data.SelectToken("Description").Value<string>() : string.Empty;
                double percentage = !string.IsNullOrEmpty(data.SelectToken("Percentage").Value<string>()) ? Convert.ToDouble(data.SelectToken("Percentage").Value<string>()) : 0;
                string expiryDate = !string.IsNullOrEmpty(data.SelectToken("ExpiryDate").Value<string>()) ? data.SelectToken("ExpiryDate").Value<string>() : string.Empty;
                bool isActive = !string.IsNullOrEmpty(data.SelectToken("IsActive").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsActive").Value<string>()) : false;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageTax.IsTaxTypeAvailable(taxTypeID, taxTypeCode))
                {
                    TaxTypeVM taxTypeVM = new TaxTypeVM();
                    taxTypeVM.TaxTypeID = taxTypeID;
                    taxTypeVM.TaxTypeCode = taxTypeCode;
                    taxTypeVM.Description = description;
                    taxTypeVM.Percentage = percentage;
                    taxTypeVM.ExpiryDate = expiryDate;
                    taxTypeVM.IsActive = isActive;
                    taxTypeVM.ModifiedBy = userID;

                    bool status = manageTax.UpdateTaxType(taxTypeVM);

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
                    return Json(new { status = false, message = "Tax Type already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteTaxType")]
        public IHttpActionResult DeleteTaxType([FromBody]JObject data)
        {
            try
            {
                int taxTypeID = !string.IsNullOrEmpty(data.SelectToken("TaxTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("TaxTypeID").Value<string>()) : 0;
                bool status = manageTax.DeleteTaxType(taxTypeID);

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
        [ActionName("GetAllTaxTypes")]
        public IHttpActionResult GetAllTaxTypes()
        {
            try
            {
                var taxTypeList = manageTax.GetAllTaxTypes();
                return Json(new
                {
                    status = true,
                    data = taxTypeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetTaxTypeByID")]
        public IHttpActionResult GetTaxTypeByID([FromBody]JObject data)
        {
            try
            {
                int taxTypeID = !string.IsNullOrEmpty(data.SelectToken("TaxTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("TaxTypeID").Value<string>()) : 0;
                var taxType = manageTax.GetTaxTypeByID(taxTypeID);
                return Json(new
                {
                    status = true,
                    data = taxType
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
