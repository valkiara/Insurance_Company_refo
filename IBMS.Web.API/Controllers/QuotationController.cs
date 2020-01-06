using IBMS.Service.TransactionData;
using IBMS.Shared.ViewModel;
using IBMS.Web.API.Security;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace IBMS.Web.API.Controllers
{
    [BasicAuthentication]
    public class QuotationController : ApiController
    {
        ManageQuotation manageQuotation = new ManageQuotation();


        #region Quatation
        [HttpPost()]
        [ActionName("SaveQuotation")]
        public IHttpActionResult SaveQuotation([FromBody]JObject data)
        {
            try
            {
                QuotationHeaderVM quotationHeaderVM = data.SelectToken("QuotationHeaderObj").ToObject<QuotationHeaderVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                quotationHeaderVM.CreatedBy = userID;

                bool status = manageQuotation.SaveQuotationHeader(quotationHeaderVM);

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
        [ActionName("UpdateQuotation")]
        public IHttpActionResult UpdateQuotation([FromBody]JObject data)
        {
            try
            {
                QuotHeaderVM quotationHeaderVM = data.SelectToken("QuotationHeaderObj").ToObject<QuotHeaderVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;
                List<QuotRequestedInsCompanyVM> quotationRequested = data.SelectToken("RequestedInsuranceCompanyDetails").ToObject<List<QuotRequestedInsCompanyVM>>();

                quotationHeaderVM.ModifiedBy = userID;

                string errorMessage = string.Empty;
                bool status = manageQuotation.UpdateQuotationHeader(quotationHeaderVM, quotationRequested, out errorMessage);

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
        [ActionName("GetAllQuotationHeaders")]
        public IHttpActionResult GetAllQuotationHeaders()
        {
            try
            {
                var quotationHeaderList = manageQuotation.GetAllQuotationHeaders();
                return Json(new
                {
                    status = true,
                    data = quotationHeaderList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllQuotationHeadersByBUID")]
        public IHttpActionResult GetAllQuotationHeadersByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var quotationHeaderList = manageQuotation.GetAllQuotationHeadersByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = quotationHeaderList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetQuotationHeaderByID")]
        public IHttpActionResult GetQuotationHeaderByID([FromBody]JObject data)
        {
            try
            {
                int quotationHeaderID = !string.IsNullOrEmpty(data.SelectToken("QuotationHeaderID").Value<string>()) ? Convert.ToInt32(data.SelectToken("QuotationHeaderID").Value<string>()) : 0;
                var quotationHeader = manageQuotation.GetQuotationHeaderByID(quotationHeaderID);
                return Json(new
                {
                    status = true,
                    data = quotationHeader
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }


        [HttpPost()]
        [ActionName("GetQuotationHeaderInsuranceCompanyByID")]
        public IHttpActionResult GetQuotationHeaderInsuranceCompanyByID([FromBody]JObject data)
        {
            try
            {
                int quotationHeaderID = !string.IsNullOrEmpty(data.SelectToken("QuotationHeaderID").Value<string>()) ? Convert.ToInt32(data.SelectToken("QuotationHeaderID").Value<string>()) : 0;
                var quotationHeader = manageQuotation.GetQuotationHeaderInsuranceCompanyByID(quotationHeaderID);
                return Json(new
                {
                    status = true,
                    data = quotationHeader
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateQuotationStatus")]
        public IHttpActionResult UpdateQuotationStatus([FromBody]JObject data)
        {
            try
            {
                int quotationHeaderID = !string.IsNullOrEmpty(data.SelectToken("QuotationHeaderID").Value<string>()) ? Convert.ToInt32(data.SelectToken("QuotationHeaderID").Value<string>()) : 0;
                string quotationStatusCode = !string.IsNullOrEmpty(data.SelectToken("QuotationStatusCode").Value<string>()) ? data.SelectToken("QuotationStatusCode").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                var status = manageQuotation.UpdateQuotationStatus(quotationHeaderID, quotationStatusCode, userID);

                if (status == "IQSC")
                {
                    return Json(new
                    {
                        status = false,
                        message = "Invalid Quotation Status Code"
                    });
                }
                else if (status == "UF")
                {
                    return Json(new
                    {
                        status = false,
                        message = "Update Failed"
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = true,
                        message = "Quotation Status Updated Successfully"
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetQuoteInfoInsCompanyLineDetailsByQuotation")]
        public IHttpActionResult GetQuoteInfoInsCompanyLineDetailsByQuotation([FromBody]JObject data)
        {
            try
            {
                int quotationHeaderID = !string.IsNullOrEmpty(data.SelectToken("QuotationHeaderID").Value<string>()) ? Convert.ToInt32(data.SelectToken("QuotationHeaderID").Value<string>()) : 0;
                var quoteInfoInsCompanyLineData = manageQuotation.GetQuoteInfoInsCompanyLineDetailsByQuotation(quotationHeaderID);
                return Json(new
                {
                    status = true,
                    data = quoteInfoInsCompanyLineData
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }


        [HttpPost()]
        [ActionName("GetAllQuotationLine")]
        public IHttpActionResult GetAllQuotationLine([FromBody]JObject data)
        {
            try
            {
                int quotationHeaderID = !string.IsNullOrEmpty(data.SelectToken("quotationHeaderID").Value<string>()) ? Convert.ToInt32(data.SelectToken("quotationHeaderID").Value<string>()) : 0;
                int insuranceCompanyID = !string.IsNullOrEmpty(data.SelectToken("insuranceCompanyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("insuranceCompanyID").Value<string>()) : 0;
                var ChrgeList = manageQuotation.GetAllQuotationLine(quotationHeaderID, insuranceCompanyID);
                return Json(new
                {
                    status = true,
                    data = ChrgeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost]
        public IHttpActionResult getInsChargeType([FromBody]JObject data)
        {
            try
            {
                int insuranceCompanyID = !string.IsNullOrEmpty(data.SelectToken("insuranceCompanyIDn").Value<string>()) ? Convert.ToInt32(data.SelectToken("insuranceCompanyIDn").Value<string>()) : 0;
                int insclss = !string.IsNullOrEmpty(data.SelectToken("InsuranceClassIDn").Value<string>()) ? Convert.ToInt32(data.SelectToken("InsuranceClassIDn").Value<string>()) : 0;
                int inssubclass = !string.IsNullOrEmpty(data.SelectToken("InsuranceSubClassIDn").Value<string>()) ? Convert.ToInt32(data.SelectToken("InsuranceSubClassIDn").Value<string>()) : 0;
                var ChrgeList = manageQuotation.getInsChargeType(insuranceCompanyID, insclss, inssubclass);
                return Json(new
                {
                    status = true,
                    data = ChrgeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        #endregion

        #region Premium

        [HttpPost()]
        [ActionName("GetAllPremiums")]
        public IHttpActionResult GetAllPremiums()
        {
            try
            {
                var premiumList = manageQuotation.GetPremium();
                return Json(new
                {
                    status = true,
                    data = premiumList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetPremiumByBUID")]
        public IHttpActionResult GetPremiumByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("businessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("businessUnitID").Value<string>()) : 0;
                var premiumList = manageQuotation.GetPremiumByBUID(businessUnitID.ToString());
                return Json(new
                {
                    status = true,
                    data = premiumList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("getAllFrequncy")]
        public IHttpActionResult getAllFrequncy()
        {
            try
            {
                var premiumList = manageQuotation.GetFrequncy();
                return Json(new
                {
                    status = true,
                    data = premiumList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }





        #endregion

        #region Save Quotaion Line with document upload

        public IHttpActionResult SaveDocument()
        {
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    DateTime requestedDateTime;
                    var requestedDate = HttpContext.Current.Request.Form["requestedDate"].ToString();
                    try
                    {
                        requestedDateTime = DateTime.ParseExact(requestedDate, "dd/MM/yyyy", null);
                    }
                    catch (Exception)
                    {
                        requestedDateTime = Convert.ToDateTime(requestedDate);
                    }

                    List<QuotationLineView> qLines = new List<QuotationLineView>();
                    HttpPostedFile uploadedDocument = HttpContext.Current.Request.Files[0];
                    QuotationLineView qLine = new QuotationLineView();
                    qLine.QuotationHeaderID = Convert.ToInt32(HttpContext.Current.Request.Form["headerId"]);
                    qLine.InsClassID = Convert.ToInt32(HttpContext.Current.Request.Form["insClassId"]);
                    qLine.InsSubClassID = Convert.ToInt32(HttpContext.Current.Request.Form["insSubClassID"]);
                    qLine.CreatedBy = Convert.ToInt32(HttpContext.Current.Request.Form["CreatedBy"]);
                    qLine.CompID = Convert.ToInt32(HttpContext.Current.Request.Form["compId"]);
                    qLine.FileName = uploadedDocument.FileName.Trim();//Convert.ToString(HttpContext.Current.Request.Form["fileName"]);
                    qLine.FilePath = Convert.ToString(HttpContext.Current.Request.Form["filePath"]);
                    qLine.IsRequested = Convert.ToBoolean(HttpContext.Current.Request.Form["isRequired"]);
                    qLine.RequestedDate = requestedDateTime;//Convert.ToDateTime(requestedDate);//DateTime.ParseExact(requestedDate, "dd/MM/yyyy", null);  //System.DateTime.Now;
                    qLine.ModifiedBy = Convert.ToInt32(HttpContext.Current.Request.Form["CreatedBy"]);
                    string newDocument = System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/Documents/") + qLine.FileName;// + Path.GetExtension(uploadedDocument.FileName);
                    string newDocumentURL = Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority + "/Uploads/Documents/" + qLine.FileName;// + Path.GetExtension(uploadedDocument.FileName);
                    qLines.Add(qLine);

                    ManageQuotation manageQuotation = new ManageQuotation();
                    bool status = manageQuotation.SaveQuotationLine(qLines);

                    if (status)
                    {
                        ////Save file in the directory
                        uploadedDocument.SaveAs(newDocument);

                        //return Json(new { status = true, message = "Successfully Saved", data = newDocumentURL });
                        return Json(new { status = false, message = "Save Failed" });
                    }
                    else
                    {
                        return Json(new { status = false, message = "Save Failed" });
                    }
                }
                else
                {
                    return Json(new { status = false, message = "Please Upload the Document" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        #endregion

        #region Get quotation detail

        [HttpPost()]
        [ActionName("GetQuotationDetailById")]
        public IHttpActionResult GetQuotationDetailById([FromBody]JObject data)
        {
            try
            {
                int quotationHeaderID = !string.IsNullOrEmpty(data.SelectToken("QuotationHeaderId").Value<string>()) ? Convert.ToInt32(data.SelectToken("QuotationHeaderId").Value<string>()) : 0;
                var quotationList = manageQuotation.GetQuotationLineDetailByHeaderId(quotationHeaderID);
                return Json(new
                {
                    status = true,
                    data = quotationList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        #endregion

        #region Received Quotation 

        public IHttpActionResult ReceivedQuotation()
        {
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    HttpPostedFile uploadedDocument = HttpContext.Current.Request.Files[0];

                    ReceiveQuotationVM receivedQuotation = new ReceiveQuotationVM();

                    receivedQuotation.ClassId = Convert.ToInt32(HttpContext.Current.Request.Form["insClassId"]);
                    receivedQuotation.CompanyId = Convert.ToInt32(HttpContext.Current.Request.Form["compId"]);
                    receivedQuotation.FileName = uploadedDocument.FileName.Trim();
                    receivedQuotation.FilePath = "";
                    receivedQuotation.QuotationHeaderId = Convert.ToInt32(HttpContext.Current.Request.Form["headerId"]);
                    receivedQuotation.ReceivedDate = System.DateTime.Now;
                    receivedQuotation.ReceivedUser = HttpContext.Current.Request.Form["receivedUser"];
                    receivedQuotation.SubClassId = Convert.ToInt32(HttpContext.Current.Request.Form["insSubClassID"]);

                    string newDocument = System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/Documents/") + receivedQuotation.FileName;
                    string newDocumentURL = Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority + "/Uploads/Documents/" + receivedQuotation.FileName;

                    ManageQuotation manageQuotation = new ManageQuotation();
                    bool status = manageQuotation.ReceiveQuotation(receivedQuotation);

                    if (status)
                    {
                        ////Save file in the directory
                        uploadedDocument.SaveAs(newDocument);

                        return Json(new { status = true, message = "Successfully Saved", data = newDocumentURL });
                        // return Json(new { status = false, message = "Save Failed" });
                    }
                    else
                    {
                        return Json(new { status = false, message = "Save Failed" });
                    }
                }
                else
                {
                    return Json(new { status = false, message = "Please Upload the Document" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        #endregion

        #region Download Received Document

        public IHttpActionResult DownloadReceivedDocment([FromBody]JObject data)
        {

            try
            {
                int quotationHeaderID = !string.IsNullOrEmpty(data.SelectToken("QuotationHeaderId").Value<string>()) ? Convert.ToInt32(data.SelectToken("QuotationHeaderId").Value<string>()) : 0;
                var result = manageQuotation.GetReceivedDocument(quotationHeaderID);

                return Json(new
                {
                    status = true,
                    data = result
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
