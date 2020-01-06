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
    public class PaymentTypeController : ApiController
    {
        ManagePaymentType managePaymentType = new ManagePaymentType();

        [HttpPost()]
        [ActionName("SavePaymentType")]
        public IHttpActionResult SavePaymentType([FromBody]JObject data)
        {
            try
            {
                string paymentTypeName = !string.IsNullOrEmpty(data.SelectToken("PaymentTypeName").Value<string>()) ? data.SelectToken("PaymentTypeName").Value<string>() : string.Empty;
                string description = !string.IsNullOrEmpty(data.SelectToken("Description").Value<string>()) ? data.SelectToken("Description").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!managePaymentType.IsPaymentTypeAvailable(null, paymentTypeName))
                {
                    PaymentTypeVM paymentTypeVM = new PaymentTypeVM();
                    paymentTypeVM.PaymentTypeName = paymentTypeName;
                    paymentTypeVM.Description = description;
                    paymentTypeVM.CreatedBy = userID;

                    bool status = managePaymentType.SavePaymentType(paymentTypeVM);

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
                    return Json(new { status = false, message = "Payment Type Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdatePaymentType")]
        public IHttpActionResult UpdatePaymentType([FromBody]JObject data)
        {
            try
            {
                int paymentTypeID = !string.IsNullOrEmpty(data.SelectToken("PaymentTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PaymentTypeID").Value<string>()) : 0;
                string paymentTypeName = !string.IsNullOrEmpty(data.SelectToken("PaymentTypeName").Value<string>()) ? data.SelectToken("PaymentTypeName").Value<string>() : string.Empty;
                string description = !string.IsNullOrEmpty(data.SelectToken("Description").Value<string>()) ? data.SelectToken("Description").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!managePaymentType.IsPaymentTypeAvailable(paymentTypeID, paymentTypeName))
                {
                    PaymentTypeVM paymentTypeVM = new PaymentTypeVM();
                    paymentTypeVM.PaymentTypeID = paymentTypeID;
                    paymentTypeVM.PaymentTypeName = paymentTypeName;
                    paymentTypeVM.Description = description;
                    paymentTypeVM.ModifiedBy = userID;

                    bool status = managePaymentType.UpdatePaymentType(paymentTypeVM);

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
                    return Json(new { status = false, message = "Payment Type Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeletePaymentType")]
        public IHttpActionResult DeletePaymentType([FromBody]JObject data)
        {
            try
            {
                int paymentTypeID = !string.IsNullOrEmpty(data.SelectToken("PaymentTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PaymentTypeID").Value<string>()) : 0;
                bool status = managePaymentType.DeletePaymentType(paymentTypeID);

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
        [ActionName("GetAllPaymentTypes")]
        public IHttpActionResult GetAllPaymentTypes()
        {
            try
            {
                var paymentTypeList = managePaymentType.GetAllPaymentTypes();
                return Json(new
                {
                    status = true,
                    data = paymentTypeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetPaymentTypeByID")]
        public IHttpActionResult GetPaymentTypeByID([FromBody]JObject data)
        {
            try
            {
                int paymentTypeID = !string.IsNullOrEmpty(data.SelectToken("PaymentTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PaymentTypeID").Value<string>()) : 0;
                var paymentType = managePaymentType.GetPaymentTypeByID(paymentTypeID);
                return Json(new
                {
                    status = true,
                    data = paymentType
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
