using IBMS.Service.MasterData;
using IBMS.Service.TransactionData;
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
    public class PolicyController : ApiController
    {
        ManagePolicy managePolicy = new ManagePolicy();
        ManagePolicyInfoRecording managePolicyInfoRecording = new ManagePolicyInfoRecording();

        #region Policy Category
        [HttpPost()]
        [ActionName("SavePolicyCategrory")]
        public IHttpActionResult SavePolicyCategrory([FromBody]JObject data)
        {
            try
            {
                string categoryName = !string.IsNullOrEmpty(data.SelectToken("CategoryName").Value<string>()) ? data.SelectToken("CategoryName").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!managePolicy.IsPolicyCategoryAvailable(null, categoryName))
                {
                    PolicyCategoryVM policyCategoryVM = new PolicyCategoryVM();
                    policyCategoryVM.PolicyCategoryName = categoryName;
                    policyCategoryVM.CreatedBy = userID;

                    bool status = managePolicy.SavePolicyCategory(policyCategoryVM);

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
                    return Json(new { status = false, message = "Policy Category Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdatePolicyCategrory")]
        public IHttpActionResult UpdatePolicyCategrory([FromBody]JObject data)
        {
            try
            {
                int policyCategoryID = !string.IsNullOrEmpty(data.SelectToken("PolicyCategoryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PolicyCategoryID").Value<string>()) : 0;
                string categoryName = !string.IsNullOrEmpty(data.SelectToken("CategoryName").Value<string>()) ? data.SelectToken("CategoryName").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!managePolicy.IsPolicyCategoryAvailable(policyCategoryID, categoryName))
                {
                    PolicyCategoryVM policyCategoryVM = new PolicyCategoryVM();
                    policyCategoryVM.PolicyCategoryID = policyCategoryID;
                    policyCategoryVM.PolicyCategoryName = categoryName;
                    policyCategoryVM.ModifiedBy = userID;

                    bool status = managePolicy.UpdatePolicyCategory(policyCategoryVM);

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
                    return Json(new { status = false, message = "Policy Category Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeletePolicyCategory")]
        public IHttpActionResult DeletePolicyCategory([FromBody]JObject data)
        {
            try
            {
                int policyCategoryID = !string.IsNullOrEmpty(data.SelectToken("PolicyCategoryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PolicyCategoryID").Value<string>()) : 0;
                bool status = managePolicy.DeletePolicyCategory(policyCategoryID);

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
        [ActionName("GetAllPolicyCategories")]
        public IHttpActionResult GetAllPolicyCategories()
        {
            try
            {
                var policyCategoryList = managePolicy.GetAllPolicyCategories();
                return Json(new
                {
                    status = true,
                    data = policyCategoryList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }


        [HttpPost()]
        [ActionName("GetPolicyCategoryByID")]
        public IHttpActionResult GetPolicyCategoryByID([FromBody]JObject data)
        {
            try
            {
                int policyCategoryID = !string.IsNullOrEmpty(data.SelectToken("PolicyCategoryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PolicyCategoryID").Value<string>()) : 0;
                var policyCategory = managePolicy.GetPolicyCategoryByID(policyCategoryID);
                return Json(new
                {
                    status = true,
                    data = policyCategory
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        #endregion

        #region Policy
        [HttpPost()]
        [ActionName("SavePolicy")]
        public IHttpActionResult SavePolicy([FromBody]JObject data)
        {
            try
            {
                string policyName = !string.IsNullOrEmpty(data.SelectToken("PolicyName").Value<string>()) ? data.SelectToken("PolicyName").Value<string>() : string.Empty;
                double rate = !string.IsNullOrEmpty(data.SelectToken("Rate").Value<string>()) ? Convert.ToDouble(data.SelectToken("Rate").Value<string>()) : 0;
                int policyCategoryID = !string.IsNullOrEmpty(data.SelectToken("PolicyCategoryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PolicyCategoryID").Value<string>()) : 0;
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BUID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BUID").Value<string>()) : 0;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!managePolicy.IsPolicyAvailable(null, policyName))
                {
                    PolicyVM policyVM = new PolicyVM();
                    policyVM.PolicyName = policyName;
                    policyVM.Rate = rate;
                    policyVM.PolicyCategoryID = policyCategoryID;
                    policyVM.BusinessUnitID = businessUnitID;
                    policyVM.CreatedBy = userID;

                    bool status = managePolicy.SavePolicy(policyVM);

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
                    return Json(new { status = false, message = "Policy Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdatePolicy")]
        public IHttpActionResult UpdatePolicy([FromBody]JObject data)
        {
            try
            {
                int policyID = !string.IsNullOrEmpty(data.SelectToken("PolicyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PolicyID").Value<string>()) : 0;
                string policyName = !string.IsNullOrEmpty(data.SelectToken("PolicyName").Value<string>()) ? data.SelectToken("PolicyName").Value<string>() : string.Empty;
                double rate = !string.IsNullOrEmpty(data.SelectToken("Rate").Value<string>()) ? Convert.ToDouble(data.SelectToken("Rate").Value<string>()) : 0;
                int policyCategoryID = !string.IsNullOrEmpty(data.SelectToken("PolicyCategoryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PolicyCategoryID").Value<string>()) : 0;
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BUID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BUID").Value<string>()) : 0;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!managePolicy.IsPolicyAvailable(policyID, policyName))
                {
                    PolicyVM policyVM = new PolicyVM();
                    policyVM.PolicyID = policyID;
                    policyVM.PolicyName = policyName;
                    policyVM.Rate = rate;
                    policyVM.PolicyCategoryID = policyCategoryID;
                    policyVM.BusinessUnitID = businessUnitID;
                    policyVM.ModifiedBy = userID;

                    bool status = managePolicy.UpdatePolicy(policyVM);

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
                    return Json(new { status = false, message = "Policy Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeletePolicy")]
        public IHttpActionResult DeletePolicy([FromBody]JObject data)
        {
            try
            {
                int policyID = !string.IsNullOrEmpty(data.SelectToken("PolicyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PolicyID").Value<string>()) : 0;
                bool status = managePolicy.DeletePolicy(policyID);

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
        [ActionName("GetAllPolicies")]
        public IHttpActionResult GetAllPolicies()
        {
            try
            {
                var policyList = managePolicy.GetAllPolicies();
                return Json(new
                {
                    status = true,
                    data = policyList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllPoliciesByBUID")]
        public IHttpActionResult GetAllPoliciesByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var policyList = managePolicy.GetAllPoliciesByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = policyList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetPolicyByID")]
        public IHttpActionResult GetPolicyByID([FromBody]JObject data)
        {
            try
            {
                int policyID = !string.IsNullOrEmpty(data.SelectToken("PolicyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PolicyID").Value<string>()) : 0;
                var policy = managePolicy.GetPolicyByID(policyID);
                return Json(new
                {
                    status = true,
                    data = policy
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        #endregion

        #region Policy Information Recording
        [HttpPost()]
        [ActionName("SavePolicyInformationRecording")]
        public IHttpActionResult SavePolicyInformationRecording([FromBody]JObject data)
        {
            try
            {
                int quotationHeaderID = !string.IsNullOrEmpty(data.SelectToken("QuotationHeaderID").Value<string>()) ? Convert.ToInt32(data.SelectToken("QuotationHeaderID").Value<string>()) : 0;
                List<PolicyInfoRecNewVM> policyInfoRecList = data.SelectToken("PolicyInfoRecList").ToObject<List<PolicyInfoRecNewVM>>();
                //List< PolicyInfoRecVM> policyInfoRecList= data.SelectToken("PolicyInfoRecList").ToObject<List<PolicyInfoRecVM>>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;
                //    List<PolicyInfoChargeVM> chrgeType = data.SelectToken("policyInfoChargeList").ToObject<List<PolicyInfoChargeVM>>();

                //List<PolicyNewCommissionPaymentVM> commissionData = new List<PolicyNewCommissionPaymentVM>();
                //List<PolicyNewInfoChargeVM> commissionDatad = data.SelectToken("policyInfoCommissionList").ToObject<List<PolicyNewInfoChargeVM>>();
                bool status = managePolicyInfoRecording.SavePolicyInfoRecording(quotationHeaderID, policyInfoRecList, userID);
                
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
        [ActionName("UpdatePolicyInformationRecording")]
        public IHttpActionResult UpdatePolicyInformationRecording([FromBody]JObject data)
        {
            try
            {
                int quotationHeaderID = !string.IsNullOrEmpty(data.SelectToken("QuotationHeaderID").Value<string>()) ? Convert.ToInt32(data.SelectToken("QuotationHeaderID").Value<string>()) : 0;
                //List<PolicyInfoRecNewVM> policyInfoRecList = data.SelectToken("PolicyInfoRecList").ToObject<List<PolicyInfoRecNewVM>>();
                ////List<PolicyNewCommissionPaymentVM> commissionData = data.SelectToken("policyInfoCommissionList").ToObject<List<PolicyNewCommissionPaymentVM>>();
                //List<PolicyInfoChargeVM> chrgeType = data.SelectToken("policyInfoChgList").ToObject<List<PolicyInfoChargeVM>>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;
                List<PolicyInfoRecNewVM> policyInfoRecList=new List<PolicyInfoRecNewVM> ();
                List<PolicyNewCommissionPaymentVM> commissionData =new List<PolicyNewCommissionPaymentVM>();

                //bool status = managePolicyInfoRecording.UpdatePolicyInfoRecording(quotationHeaderID, policyInfoRecList, commissionData, userID);
                bool status = true;
                if (status)
                {
                    return Json(new { status = true, message = "Successfully Updated" });
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
        [ActionName("GetAllPolicyInfoRecordings")]
        public IHttpActionResult GetAllPolicyInfoRecordings()
        {
            try
            {
                var policyInfoRecList = managePolicyInfoRecording.GetAllPolicyInfoRecordings();
                return Json(new
                {
                    status = true,
                    data = policyInfoRecList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllPolicyInfoRecordingsByBUID")]
        public IHttpActionResult GetAllPolicyInfoRecordingsByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var policyInfoRecList = managePolicyInfoRecording.GetAllPolicyInfoRecordingsByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = policyInfoRecList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetPolicyInfoRecordingsByQuotation")]
        public IHttpActionResult GetPolicyInfoRecordingsByQuotation([FromBody]JObject data)
        {
            try
            {
                int quotationHeaderID = !string.IsNullOrEmpty(data.SelectToken("QuotationHeaderID").Value<string>()) ? Convert.ToInt32(data.SelectToken("QuotationHeaderID").Value<string>()) : 0;
                var policyInfoRecList = managePolicyInfoRecording.GetPolicyInfoRecordingsByQuotation(quotationHeaderID);
                return Json(new
                {
                    status = true,
                    data = policyInfoRecList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetPolicyInfoRecordingsByClient")]
        public IHttpActionResult GetPolicyInfoRecordingsByClient([FromBody]JObject data)
        {
            try
            {
                int clientID = !string.IsNullOrEmpty(data.SelectToken("ClientID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClientID").Value<string>()) : 0;
                var policyInfoRecList = managePolicyInfoRecording.GetPolicyInfoRecordingsByClient(clientID);
                return Json(new
                {
                    status = true,
                    data = policyInfoRecList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetPolicyInfoRecordingByID")]
        public IHttpActionResult GetPolicyInfoRecordingByID([FromBody]JObject data)
        {
            try
            {
                int policyInfoRecID = !string.IsNullOrEmpty(data.SelectToken("PolicyInfoRecID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PolicyInfoRecID").Value<string>()) : 0;
                var policyInfoRec = managePolicyInfoRecording.GetPolicyInfoRecordingByID(policyInfoRecID);
                return Json(new
                {
                    status = true,
                    data = policyInfoRec
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        #endregion

        #region Policy Renewal History
        [HttpPost()]
        [ActionName("SavePolicyRenewalHistory")]
        public IHttpActionResult SavePolicyRenewalHistory([FromBody]JObject data)
        {
            try
            {
                PolicyRenewalHistoryVM policyRenewalHistoryVM = data.SelectToken("PolicyRenewalHistoryVM").ToObject<PolicyRenewalHistoryVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                policyRenewalHistoryVM.CreatedBy = userID;

                bool status = managePolicyInfoRecording.SavePolicyRenewalHistory(policyRenewalHistoryVM);

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
        [ActionName("UpdatePolicyRenewalHistory")]
        public IHttpActionResult UpdatePolicyRenewalHistory([FromBody]JObject data)
        {
            try
            {
                PolicyRenewalHistoryVM policyRenewalHistoryVM = data.SelectToken("PolicyRenewalHistoryVM").ToObject<PolicyRenewalHistoryVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                policyRenewalHistoryVM.ModifiedBy = userID;

                bool status = managePolicyInfoRecording.UpdatePolicyRenewalHistory(policyRenewalHistoryVM);

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
        [ActionName("DeletePolicyRenewalHistory")]
        public IHttpActionResult DeletePolicyRenewalHistory([FromBody]JObject data)
        {
            try
            {
                int policyRenewalHistoryID = !string.IsNullOrEmpty(data.SelectToken("PolicyRenewalHistoryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PolicyRenewalHistoryID").Value<string>()) : 0;
                bool status = managePolicyInfoRecording.DeletePolicyRenewalHistory(policyRenewalHistoryID);

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
        [ActionName("GetAllPolicyRenewalHistories")]
        public IHttpActionResult GetAllPolicyRenewalHistories()
        {
            try
            {
                var policyRenewalHistoryList = managePolicyInfoRecording.GetAllPolicyRenewalHistories();
                return Json(new
                {
                    status = true,
                    data = policyRenewalHistoryList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllPolicyRenewalHistoriesByBUID")]
        public IHttpActionResult GetAllPolicyRenewalHistoriesByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var policyRenewalHistoryList = managePolicyInfoRecording.GetAllPolicyRenewalHistoriesByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = policyRenewalHistoryList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetPolicyRenewalHistoriesByPolicyInfoRecID")]
        public IHttpActionResult GetPolicyRenewalHistoriesByPolicyInfoRecID([FromBody]JObject data)
        {
            try
            {
                int policyInfoRecID = !string.IsNullOrEmpty(data.SelectToken("PolicyInfoRecID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PolicyInfoRecID").Value<string>()) : 0;
                var policyRenewalHistoryList = managePolicyInfoRecording.GetPolicyRenewalHistoriesByPolicyInfoRecID(policyInfoRecID);
                return Json(new
                {
                    status = true,
                    data = policyRenewalHistoryList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetPolicyRenewalHistoryByID")]
        public IHttpActionResult GetPolicyRenewalHistoryByID([FromBody]JObject data)
        {
            try
            {
                int policyRenewalHistoryID = !string.IsNullOrEmpty(data.SelectToken("PolicyRenewalHistoryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PolicyRenewalHistoryID").Value<string>()) : 0;
                var policyRenewalHistory = managePolicyInfoRecording.GetPolicyRenewalHistoryByID(policyRenewalHistoryID);
                return Json(new
                {
                    status = true,
                    data = policyRenewalHistory
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        #endregion


        #region BUPA Policy
        [HttpPost()]
        [ActionName("GetPolicyInfoRecordingByIDBUPA")]
        public IHttpActionResult GetPolicyInfoRecordingByIDBUPA([FromBody]JObject data)
        {
            try
            {
                int policyInfoRecID = !string.IsNullOrEmpty(data.SelectToken("PolicyInfoRecID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PolicyInfoRecID").Value<string>()) : 0;
                var policyInfoRec = managePolicyInfoRecording.GetPolicyInfoRecordingByID(policyInfoRecID);
                return Json(new
                {
                    status = true,
                    data = policyInfoRec
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
