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
    public class ChargeTypeController : ApiController
    {
        ManageChargeType manageChargeType = new ManageChargeType();

        [HttpPost()]
        [ActionName("SaveChargeType")]
        public IHttpActionResult SaveChargeType([FromBody]JObject data)
        {
            try
            {
                string chargeTypeName = !string.IsNullOrEmpty(data.SelectToken("ChargeTypeName").Value<string>()) ? data.SelectToken("ChargeTypeName").Value<string>() : string.Empty;
                double percentage = !string.IsNullOrEmpty(data.SelectToken("Percentage").Value<string>()) ? Convert.ToDouble(data.SelectToken("Percentage").Value<string>()) : 0;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageChargeType.IsChargeTypeAvailable(null, chargeTypeName))
                {
                    ChargeTypeVM chargeTypeVM = new ChargeTypeVM();
                    chargeTypeVM.ChargeTypeName = chargeTypeName;
                    chargeTypeVM.Percentage = percentage;
                    chargeTypeVM.CreatedBy = userID;

                    bool status = manageChargeType.SaveChargeType(chargeTypeVM);

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
                    return Json(new { status = false, message = "Charge Type Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateChargeType")]
        public IHttpActionResult UpdateChargeType([FromBody]JObject data)
        {
            try
            {
                int ChargeTypeID = !string.IsNullOrEmpty(data.SelectToken("ChargeTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ChargeTypeID").Value<string>()) : 0;
                string chargeTypeName = !string.IsNullOrEmpty(data.SelectToken("ChargeTypeName").Value<string>()) ? data.SelectToken("ChargeTypeName").Value<string>() : string.Empty;
                double percentage = !string.IsNullOrEmpty(data.SelectToken("Percentage").Value<string>()) ? Convert.ToDouble(data.SelectToken("Percentage").Value<string>()) : 0;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageChargeType.IsChargeTypeAvailable(ChargeTypeID, chargeTypeName))
                {
                    ChargeTypeVM chargeTypeVM = new ChargeTypeVM();
                    chargeTypeVM.ChargeTypeID = ChargeTypeID;
                    chargeTypeVM.ChargeTypeName = chargeTypeName;
                    chargeTypeVM.Percentage = percentage;
                    chargeTypeVM.ModifiedBy = userID;

                    bool status = manageChargeType.UpdateChargeType(chargeTypeVM);

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
                    return Json(new { status = false, message = "Charge Type Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteChargeType")]
        public IHttpActionResult DeleteChargeType([FromBody]JObject data)
        {
            try
            {
                int chargeTypeID = !string.IsNullOrEmpty(data.SelectToken("ChargeTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ChargeTypeID").Value<string>()) : 0;
                bool status = manageChargeType.DeleteChargeType(chargeTypeID);

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
        [ActionName("GetAllChargeTypes")]
        public IHttpActionResult GetAllChargeTypes()
        {
            try
            {
                var chargeTypeList = manageChargeType.GetAllChargeTypes();
                return Json(new
                {
                    status = true,
                    data = chargeTypeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetChargeTypeByID")]
        public IHttpActionResult GetChargeTypeByID([FromBody]JObject data)
        {
            try
            {
                int chargeTypeID = !string.IsNullOrEmpty(data.SelectToken("ChargeTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ChargeTypeID").Value<string>()) : 0;
                var chargeType = manageChargeType.GetChargeTypeByID(chargeTypeID);
                return Json(new
                {
                    status = true,
                    data = chargeType
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        [HttpPost()]
        [ActionName("GetApplicableChargeTypes")]
        public IHttpActionResult GetApplicableChargeTypes()
        {
            try
            {
                var chargeTypeList = manageChargeType.GetApplicableChargeTypes();
                return Json(new
                {
                    status = true,
                    data = chargeTypeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
