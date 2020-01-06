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
    public class ClaimIssueController : ApiController
    {
        ManageClaimIssue manageClaimIssue = new ManageClaimIssue();

        #region Claim Reject Reason
        [HttpPost()]
        [ActionName("SaveClaimRejectReason")]
        public IHttpActionResult SaveClaimRejectReason([FromBody]JObject data)
        {
            try
            {
                string claimRejectReason = !string.IsNullOrEmpty(data.SelectToken("ClaimRejectReason").Value<string>()) ? data.SelectToken("ClaimRejectReason").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;
                if (!manageClaimIssue.IsClaimAvailable(null, claimRejectReason))
                {
                    ClaimRejectReasonVM claimRejectReasonVM = new ClaimRejectReasonVM();
                    claimRejectReasonVM.ClaimRejectReason = claimRejectReason;
                    claimRejectReasonVM.CreatedBy = userID;

                    bool status = manageClaimIssue.SaveClaimRejectReason(claimRejectReasonVM);

                    if (status)
                    {
                        return Json(new { status = true, message = "Successfully Saved" });
                    }
                    else
                    {
                        return Json(new { status = false, message = "Save Failed" });
                    }
                }else
                {
                    return Json(new { status = false, message = "Claim Rejection Reason Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateClaimRejectReason")]
        public IHttpActionResult UpdateClaimRejectReason([FromBody]JObject data)
        {
            try
            {
                int claimRejectReasonID = !string.IsNullOrEmpty(data.SelectToken("ClaimRejectReasonID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClaimRejectReasonID").Value<string>()) : 0;
                string claimRejectReason = !string.IsNullOrEmpty(data.SelectToken("ClaimRejectReason").Value<string>()) ? data.SelectToken("ClaimRejectReason").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;
                if (!manageClaimIssue.IsClaimAvailable(claimRejectReasonID, claimRejectReason))
                {
                    ClaimRejectReasonVM claimRejectReasonVM = new ClaimRejectReasonVM();
                    claimRejectReasonVM.ClaimRejectReasonID = claimRejectReasonID;
                    claimRejectReasonVM.ClaimRejectReason = claimRejectReason;
                    claimRejectReasonVM.ModifiedBy = userID;

                    bool status = manageClaimIssue.UpdateClaimRejectReason(claimRejectReasonVM);

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
                    return Json(new { status = false, message = "Claim Rejection Reason Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteClaimRejectReason")]
        public IHttpActionResult DeleteClaimRejectReason([FromBody]JObject data)
        {
            try
            {
                int claimRejectReasonID = !string.IsNullOrEmpty(data.SelectToken("ClaimRejectReasonID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClaimRejectReasonID").Value<string>()) : 0;
                bool status = manageClaimIssue.DeleteClaimRejectReason(claimRejectReasonID);

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
        [ActionName("GetAllClaimRejectReasons")]
        public IHttpActionResult GetAllClaimRejectReasons()
        {
            try
            {
                var claimRejectReasonList = manageClaimIssue.GetAllClaimRejectReasons();
                return Json(new
                {
                    status = true,
                    data = claimRejectReasonList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetClaimRejectReasonByID")]
        public IHttpActionResult GetClaimRejectReasonByID([FromBody]JObject data)
        {
            try
            {
                int claimRejectReasonID = !string.IsNullOrEmpty(data.SelectToken("ClaimRejectReasonID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClaimRejectReasonID").Value<string>()) : 0;
                var claimRejectReason = manageClaimIssue.GetClaimRejectReasonByID(claimRejectReasonID);
                return Json(new
                {
                    status = true,
                    data = claimRejectReason
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        #endregion

        #region Claim Re-Open Reason
        [HttpPost()]
        [ActionName("SaveClaimReOpenReason")]
        public IHttpActionResult SaveClaimReOpenReason([FromBody]JObject data)
        {
            try
            {
                string claimReOpenReason = !string.IsNullOrEmpty(data.SelectToken("ClaimReOpenReason").Value<string>()) ? data.SelectToken("ClaimReOpenReason").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;
                if (!manageClaimIssue.IsClaimReOpenAvailable(null, claimReOpenReason))
                {
                    ClaimReOpenReasonVM claimReOpenReasonVM = new ClaimReOpenReasonVM();
                    claimReOpenReasonVM.ClaimReOpenReason = claimReOpenReason;
                    claimReOpenReasonVM.CreatedBy = userID;

                    bool status = manageClaimIssue.SaveClaimReOpenReason(claimReOpenReasonVM);

                    if (status)
                    {
                        return Json(new { status = true, message = "Successfully Saved" });
                    }
                    else
                    {
                        return Json(new { status = false, message = "Save Failed" });
                    }
                }else
                {
                    return Json(new { status = false, message = "Claim Re-Open Reason Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateClaimReOpenReason")]
        public IHttpActionResult UpdateClaimReOpenReason([FromBody]JObject data)
        {
            try
            {
                int claimReOpenReasonID = !string.IsNullOrEmpty(data.SelectToken("ClaimReOpenReasonID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClaimReOpenReasonID").Value<string>()) : 0;
                string claimReOpenReason = !string.IsNullOrEmpty(data.SelectToken("ClaimReOpenReason").Value<string>()) ? data.SelectToken("ClaimReOpenReason").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;
                if (!manageClaimIssue.IsClaimReOpenAvailable(claimReOpenReasonID, claimReOpenReason))
                {
                    ClaimReOpenReasonVM claimReOpenReasonVM = new ClaimReOpenReasonVM();
                    claimReOpenReasonVM.ClaimReOpenReasonID = claimReOpenReasonID;
                    claimReOpenReasonVM.ClaimReOpenReason = claimReOpenReason;
                    claimReOpenReasonVM.ModifiedBy = userID;

                    bool status = manageClaimIssue.UpdateClaimReOpenReason(claimReOpenReasonVM);

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
                    return Json(new { status = false, message = "Claim Re-Open Reason Name already exists" });
                }
                
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteClaimReOpenReason")]
        public IHttpActionResult DeleteClaimReOpenReason([FromBody]JObject data)
        {
            try
            {
                int claimReOpenReasonID = !string.IsNullOrEmpty(data.SelectToken("ClaimReOpenReasonID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClaimReOpenReasonID").Value<string>()) : 0;
                bool status = manageClaimIssue.DeleteClaimReOpenReason(claimReOpenReasonID);

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
        [ActionName("GetAllClaimReOpenReasons")]
        public IHttpActionResult GetAllClaimReOpenReasons()
        {
            try
            {
                var claimReOpenReasonList = manageClaimIssue.GetAllClaimReOpenReasons();
                return Json(new
                {
                    status = true,
                    data = claimReOpenReasonList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetClaimReOpenReasonByID")]
        public IHttpActionResult GetClaimReOpenReasonByID([FromBody]JObject data)
        {
            try
            {
                int claimReOpenReasonID = !string.IsNullOrEmpty(data.SelectToken("ClaimReOpenReasonID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClaimReOpenReasonID").Value<string>()) : 0;
                var claimReOpenReason = manageClaimIssue.GetClaimReOpenReasonByID(claimReOpenReasonID);
                return Json(new
                {
                    status = true,
                    data = claimReOpenReason
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        #endregion

        #region Cause Of Loss
        [HttpPost()]
        [ActionName("SaveCauseOfLoss")]
        public IHttpActionResult SaveCauseOfLoss([FromBody]JObject data)
        {
            try
            {
                string causeOfLoss = !string.IsNullOrEmpty(data.SelectToken("CauseOfLoss").Value<string>()) ? data.SelectToken("CauseOfLoss").Value<string>() : string.Empty;
                int insSubClassID = !string.IsNullOrEmpty(data.SelectToken("InsSubClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InsSubClassID").Value<string>()) : 0;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;
                if (!manageClaimIssue.IscauseOfLossAvailable(null, causeOfLoss))
                {
                    CauseOfLossVM causeOfLossVM = new CauseOfLossVM();
                    causeOfLossVM.CauseOfLoss = causeOfLoss;
                    causeOfLossVM.InsSubClassID = insSubClassID;
                    causeOfLossVM.CreatedBy = userID;

                    bool status = manageClaimIssue.SaveCauseOfLoss(causeOfLossVM);

                    if (status)
                    {
                        return Json(new { status = true, message = "Successfully Saved" });
                    }
                    else
                    {
                        return Json(new { status = false, message = "Save Failed" });
                    }
                }else
                {
                    return Json(new { status = false, message = "Cause Of Loss Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateCauseOfLoss")]
        public IHttpActionResult UpdateCauseOfLoss([FromBody]JObject data)
        {
            try
            {
                int causeOfLossID = !string.IsNullOrEmpty(data.SelectToken("CauseOfLossID").Value<string>()) ? Convert.ToInt32(data.SelectToken("CauseOfLossID").Value<string>()) : 0;
                string causeOfLoss = !string.IsNullOrEmpty(data.SelectToken("CauseOfLoss").Value<string>()) ? data.SelectToken("CauseOfLoss").Value<string>() : string.Empty;
                int insSubClassID = !string.IsNullOrEmpty(data.SelectToken("InsSubClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InsSubClassID").Value<string>()) : 0;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;
                if (!manageClaimIssue.IscauseOfLossAvailable(causeOfLossID, causeOfLoss))
                {
                    CauseOfLossVM causeOfLossVM = new CauseOfLossVM();
                    causeOfLossVM.CauseOfLossID = causeOfLossID;
                    causeOfLossVM.CauseOfLoss = causeOfLoss;
                    causeOfLossVM.InsSubClassID = insSubClassID;
                    causeOfLossVM.ModifiedBy = userID;

                    bool status = manageClaimIssue.UpdateCauseOfLoss(causeOfLossVM);

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
                    return Json(new { status = false, message = "Cause Of Loss Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteCauseOfLoss")]
        public IHttpActionResult DeleteCauseOfLoss([FromBody]JObject data)
        {
            try
            {
                int causeOfLossID = !string.IsNullOrEmpty(data.SelectToken("CauseOfLossID").Value<string>()) ? Convert.ToInt32(data.SelectToken("CauseOfLossID").Value<string>()) : 0;
                bool status = manageClaimIssue.DeleteCauseOfLoss(causeOfLossID);

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
        [ActionName("GetAllCauseOfLosses")]
        public IHttpActionResult GetAllCauseOfLosses()
        {
            try
            {
                var causeOfLossesList = manageClaimIssue.GetAllCauseOfLosses();
                return Json(new
                {
                    status = true,
                    data = causeOfLossesList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetCauseOfLossByID")]
        public IHttpActionResult GetCauseOfLossByID([FromBody]JObject data)
        {
            try
            {
                int causeOfLossID = !string.IsNullOrEmpty(data.SelectToken("CauseOfLossID").Value<string>()) ? Convert.ToInt32(data.SelectToken("CauseOfLossID").Value<string>()) : 0;
                var causeOfLoss = manageClaimIssue.GetCauseOfLossByID(causeOfLossID);
                return Json(new
                {
                    status = true,
                    data = causeOfLoss
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllCauseOfLossesByBUID")]
        public IHttpActionResult GetAllCauseOfLossesByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var causeOfLossesList = manageClaimIssue.GetAllCauseOfLossesByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = causeOfLossesList
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
