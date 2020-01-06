using IBMS.Service.TransactionData;
using IBMS.Shared.ViewModel;
using IBMS.Shared.ViewModel.Mapper;
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
    public class ClaimController : ApiController
    {
        ManageClaim manageClaim = new ManageClaim();

        #region Claim Recording
        [HttpPost()]
        [ActionName("SaveClaimRecording")]
        public IHttpActionResult SaveClaimRecording([FromBody]JObject data)
        {
            try
            {
                ClaimRecordingVM claimRecordingVM = data.SelectToken("ClaimRecordingVM").ToObject<ClaimRecordingVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                bool status = manageClaim.SaveClaimRecording(claimRecordingVM, userID);

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
        [ActionName("SavePilotClaimRecording")]
        public IHttpActionResult SavePilotClaimRecording([FromBody]JObject data)
        {
            try
            {
                ClaimRecordingVM claimRecordingVM = data.SelectToken("ClaimRecordingVM").ToObject<ClaimRecordingVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                bool status = manageClaim.SavePilotClaimRecording(claimRecordingVM, userID);

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
        [ActionName("UpdateClaimRecording")]
        public IHttpActionResult UpdateClaimRecording([FromBody]JObject data)
        {
            try
            {
                ClaimRecordingVM claimRecordingVM = data.SelectToken("ClaimRecordingVM").ToObject<ClaimRecordingVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                bool status = manageClaim.UpdateClaimRecording(claimRecordingVM, userID);

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
        [ActionName("GetAllClaimRecordings")]
        public IHttpActionResult GetAllClaimRecordings()
        {
            try
            {
                var claimRecordingList = manageClaim.GetAllClaimRecordings();
                return Json(new
                {
                    status = true,
                    data = claimRecordingList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllClaimRecordingsByBUID")]
        public IHttpActionResult GetAllClaimRecordingsByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var claimRecordingList = manageClaim.GetAllClaimRecordingsByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = claimRecordingList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetClaimRecordingByID")]
        public IHttpActionResult GetClaimRecordingByID([FromBody]JObject data)
        {
            try
            {
                int claimRecordingID = !string.IsNullOrEmpty(data.SelectToken("ClaimRecordingID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClaimRecordingID").Value<string>()) : 0;
                var claimRecording = manageClaim.GetClaimRecordingByID(claimRecordingID);
                return Json(new
                {
                    status = true,
                    data = claimRecording
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }


        [HttpPost()]
        [ActionName("LoadInsClassType")]
        public IHttpActionResult LoadInsClassType([FromBody]JObject data)
        {
            try
            {
                int insClass = !string.IsNullOrEmpty(data.SelectToken("InsuranceClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InsuranceClassID").Value<string>()) : 0;
                var claimRecording = manageClaim.GetClaimRecordingByID(insClass);
                return Json(new
                {
                    status = true,
                    data = claimRecording
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }




        #endregion

        #region Claim Payment
        [HttpPost()]
        [ActionName("SaveClaimPayment")]
        public IHttpActionResult SaveClaimPayment([FromBody]JObject data)
        {
            try
            {
                ClaimPaymentVM claimPaymentVM = data.SelectToken("ClaimPaymentVM").ToObject<ClaimPaymentVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                string errorMessage = string.Empty;
                bool status = manageClaim.SaveClaimPayment(claimPaymentVM, userID, out errorMessage);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully Saved" });
                }
                else
                {
                    //return Json(new { status = false, message = "Save Failed" });
                    return Json(new { status = false, message = errorMessage });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateClaimPayment")]
        public IHttpActionResult UpdateClaimPayment([FromBody]JObject data)
        {
            try
            {
                ClaimPaymentVM claimPaymentVM = data.SelectToken("ClaimPaymentVM").ToObject<ClaimPaymentVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                string errorMessage = string.Empty;
                bool status = manageClaim.UpdateClaimPayment(claimPaymentVM, userID, out errorMessage);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully Updated" });
                }
                else
                {
                    //return Json(new { status = false, message = "Update Failed" });
                    return Json(new { status = false, message = errorMessage });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllClaimPayments")]
        public IHttpActionResult GetAllClaimPayments()
        {
            try
            {
                var claimPaymentList = manageClaim.GetAllClaimPayments();
                return Json(new
                {
                    status = true,
                    data = claimPaymentList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllClaimPaymentsByBUID")]
        public IHttpActionResult GetAllClaimPaymentsByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var claimPaymentList = manageClaim.GetAllClaimPaymentsByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = claimPaymentList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetClaimPaymentByID")]
        public IHttpActionResult GetClaimPaymentByID([FromBody]JObject data)
        {
            try
            {
                int claimPaymentID = !string.IsNullOrEmpty(data.SelectToken("ClaimPaymentID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClaimPaymentID").Value<string>()) : 0;
                var claimPayment = manageClaim.GetClaimPaymentByID(claimPaymentID);
                return Json(new
                {
                    status = true,
                    data = claimPayment
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetClaimStatus")]
        public IHttpActionResult GetClaimStatus()
        {
            try
            {
                var claimStatusList = manageClaim.GetClaimStatus();
                return Json(new
                {
                    status = true,
                    data = claimStatusList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetPaidClaimStatus")]
        public IHttpActionResult GetPaidClaimStatus()
        {
            try
            {
                var claimStatusList = manageClaim.GetStatus();
                return Json(new
                {
                    status = true,
                    data = claimStatusList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }


        [HttpPost()]
        [ActionName("GetYear")]
        public IHttpActionResult GetYear()
        {
            try
            {
                var YearList = manageClaim.GetYear();
                return Json(new
                {
                    status = true,
                    data = YearList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }


        [HttpPost()]
        [ActionName("GetInsClassType")]
        public IHttpActionResult GetInsClassType([FromBody]JObject data)
        {
            try
            {
                int InsuranceType = !string.IsNullOrEmpty(data.SelectToken("InsuranceClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InsuranceClassID").Value<string>()) : 0;
                
                var InsClassTypeList = manageClaim.GetInsClassType(InsuranceType);
                return Json(new
                {
                    status = true,
                    data = InsClassTypeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetClaimListByDate")]
        public IHttpActionResult GetClaimListByDate([FromBody]JObject data)
        {
            try
            {
                var filterMapper = data.SelectToken("filterObj").ToObject<FilterMapper>();

                var results = manageClaim.GetClaimsByDate(filterMapper);
                return Json(new
                {
                    status = true,
                    data = results
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
        }

        #endregion
    }
}
