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
    public class TransactionTypeController : ApiController
    {
        ManageTransactionType manageTransactionType = new ManageTransactionType();

        [HttpPost()]
        [ActionName("SaveTransactionType")]
        public IHttpActionResult SaveTransactionType([FromBody]JObject data)
        {
            try
            {
                string description = !string.IsNullOrEmpty(data.SelectToken("Description").Value<string>()) ? data.SelectToken("Description").Value<string>() : string.Empty;
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BUID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BUID").Value<string>()) : 0;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;
                if (!manageTransactionType.IsTransactionAvailable(null, description))
                {
                    TransactionTypeVM transactionTypeVM = new TransactionTypeVM();
                    transactionTypeVM.Description = description;
                    transactionTypeVM.BusinessUnitID = businessUnitID;
                    transactionTypeVM.CreatedBy = userID;

                    bool status = manageTransactionType.SaveTransactionType(transactionTypeVM);

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
                    return Json(new { status = false, message = "Transaction Type Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }


        [HttpPost()]
        [ActionName("UpdateTransactionType")]
        public IHttpActionResult UpdateTransactionType([FromBody]JObject data)
        {
            try
            {
                int transactionTypeID = !string.IsNullOrEmpty(data.SelectToken("TransactionTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("TransactionTypeID").Value<string>()) : 0;
                string description = !string.IsNullOrEmpty(data.SelectToken("Description").Value<string>()) ? data.SelectToken("Description").Value<string>() : string.Empty;
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BUID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BUID").Value<string>()) : 0;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                TransactionTypeVM transactionTypeVM = new TransactionTypeVM();
                transactionTypeVM.TransactionTypeID = transactionTypeID;
                transactionTypeVM.Description = description;
                transactionTypeVM.BusinessUnitID = businessUnitID;
                transactionTypeVM.ModifiedBy = userID;

                bool status = manageTransactionType.UpdateTransactionType(transactionTypeVM);

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
        [ActionName("DeleteTransactionType")]
        public IHttpActionResult DeleteTransactionType([FromBody]JObject data)
        {
            try
            {

                int transactionTypeID = !string.IsNullOrEmpty(data.SelectToken("TransactionTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("TransactionTypeID").Value<string>()) : 0;
                bool status = manageTransactionType.DeleteTransactionType(transactionTypeID);

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
        [ActionName("GetAllTransactionTypes")]
        public IHttpActionResult GetAllTransactionTypes()
        {
            try
            {
                var transactionTypeList = manageTransactionType.GetAllTransactionTypes();
                return Json(new
                {
                    status = true,
                    data = transactionTypeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllTransactionTypesByBUID")]
        public IHttpActionResult GetAllTransactionTypesByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var transactionTypeList = manageTransactionType.GetAllTransactionTypesByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = transactionTypeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetTransactionTypeByID")]
        public IHttpActionResult GetTransactionTypeByID([FromBody]JObject data)
        {
            try
            {
                int transactionTypeID = !string.IsNullOrEmpty(data.SelectToken("TransactionTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("TransactionTypeID").Value<string>()) : 0;
                var transactionType = manageTransactionType.GetTransactionTypeByID(transactionTypeID);
                return Json(new
                {
                    status = true,
                    data = transactionType
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
