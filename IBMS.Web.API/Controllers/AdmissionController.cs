using IBMS.Web.API.Security;
using Newtonsoft.Json.Linq;
using System;


using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using IBMS.Shared.ViewModel;
//using System.Web.Script.Serialization;
using IBMS.Service.TransactionData;


namespace IBMS.Web.API.Controllers
{
    [BasicAuthentication]
    public class AdmissionController : ApiController
    {
        Admission admissionService = new Admission();
        SingaporeAdmission2 singaporeAdmissionService = new SingaporeAdmission2(); 
        public AdmissionController()
        {

        }
        // GET: Admission
        [HttpPost()]
        public object SaveLocalAdmission([FromBody]JObject data)
        {
            try
            {
               

                var admissionVM = data.SelectToken("admission").ToObject<AdmissionVM>();
                var status = admissionService.SaveAdmissionRecording(admissionVM);
                

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
                throw ex;
            }
        }

        [HttpPost()]
        public object SavePilotClameRecording([FromBody]JObject data)
        {
            try
            {


                var admissionVM = data.SelectToken("ClaimRecordingVM").ToObject<AdmissionVM>();
                var status = admissionService.SavePilotClaimRecording(admissionVM);


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
                throw ex;
            }
        }




        [System.Web.Http.HttpPost()]
        public object GetAllLocalAdmission()
        {
            var admissionList = admissionService.GetAllLocalAdmissions();
            return Json(new
            {
                status = true,
                data = admissionList
            });
        }
        

        [System.Web.Http.HttpPost()]
        public object GetAllAdmissions([FromBody]JObject data)
        {
            int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
            var admissionList = admissionService.GetAllAdmissions(businessUnitID);
            return Json(new
            {
                status = true,
                data = admissionList
            });
        }
        [System.Web.Http.HttpPost()]
        public object GetAllPilotAdmissions([FromBody]JObject data)
        {
            int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
            var admissionList = admissionService.GetAllPilotAdmissions(businessUnitID);
            return Json(new
            {
                status = true,
                data = admissionList
            });
        }
        

        [System.Web.Http.HttpPost()]
        public object GetAllSingporeAdmissions([FromBody]JObject data)
        {
            int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
            var admissionList = admissionService.GetAllSingporeAdmissions(businessUnitID);
            return Json(new
            {
                status = true,
                data = admissionList
            });
        }






        [System.Web.Http.HttpPost()]
        public object GetClientBybusinessUnitID([FromBody]JObject data)
        {
            int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
            var admissionList = admissionService.GetClientBybusinessUnitID(businessUnitID);
            return Json(new
            {
                status = true,
                data = admissionList
            });
        }
        [System.Web.Http.HttpPost()]
        public object GetClientByMemebershipID([FromBody]JObject data)
        {
            string MembershipID = !string.IsNullOrEmpty(data.SelectToken("MembershipID").Value<string>()) ?data.SelectToken("MembershipID").Value<string>() : "0";
            var admissionList = admissionService.GetClientByMembershipID(MembershipID);
            return Json(new
            {
                status = true,
                data = admissionList
            });
        }


        [HttpPost()]
        [ActionName("SaveSingaporeAdmission")]
        public IHttpActionResult SaveSingaporeAdmission([FromBody]JObject data)
        {
            try
            {
                //var status = false;
             //   var admissionVM = data.ToObject<AdmissionVM>();
                var admissionVM = data.SelectToken("admission").ToObject<AdmissionVM>();
                
                var status = singaporeAdmissionService.SaveAdmissionRecording(admissionVM);

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

        [System.Web.Http.HttpPost()]
        public object GetAllSingaporeAdmission()
        {
            var admissionList = singaporeAdmissionService.GetAllSingaporeAdmissions();
            return Json(new
            {
                status = true,
                data = admissionList
            });
        }

        [System.Web.Http.HttpPost()]
        public IHttpActionResult GetLocalAdmissionByReferenceNo([FromBody]JObject data)
        {
            try
            {
                string  referenceNo = !string.IsNullOrEmpty(data.SelectToken("refNo").Value<string>()) ? data.SelectToken("refNo").Value<string>() : "0";
                var clientRequest = admissionService.GetLocalAdmissionByReferenceNo(referenceNo);
                return Json(new
                {
                    status = true,
                    data = clientRequest
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [System.Web.Http.HttpPost()]
        public IHttpActionResult GetPIlotAdmissionByReferenceNo([FromBody]JObject data)
        {
            try
            {
                string referenceNo = !string.IsNullOrEmpty(data.SelectToken("ClaimRecordingID").Value<string>()) ? data.SelectToken("ClaimRecordingID").Value<string>() : "0";
                var clientRequest = admissionService.GetPIlotAdmissionByReferenceNo(referenceNo);
                return Json(new
                {
                    status = true,
                    data = clientRequest
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [System.Web.Http.HttpPost()]
        public IHttpActionResult GetSingaporeAdmissionByReferenceNo([FromBody]JObject data)
        {
            try
            {
                string referenceNo = !string.IsNullOrEmpty(data.SelectToken("refNo").Value<string>()) ? data.SelectToken("refNo").Value<string>() : "0";
                var clientRequest = singaporeAdmissionService.GetSingaporeAdmissionByReferenceNo(referenceNo);
                return Json(new
                {
                    status = true,
                    data = clientRequest
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [System.Web.Http.HttpPost()]
        public IHttpActionResult GetSingaporeInvoiceDetailsByReferenceNo([FromBody]JObject data)
        {
            try
            {
                string referenceNo = !string.IsNullOrEmpty(data.SelectToken("refNo").Value<string>()) ? data.SelectToken("refNo").Value<string>() : "0";
                var clientRequest = singaporeAdmissionService.GetSingaporeAdmissionInvoice(referenceNo);
                return Json(new
                {
                    status = true,
                    data = clientRequest
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        //SaveInvoiceService

        [System.Web.Http.HttpPost()]
        public object SaveInvoice([FromBody]JObject data)
        {
            try
            {
                var admissionVM = data.ToObject<SingaporeAdmissionInvoiceVM>();
                var status = singaporeAdmissionService.SaveInvoice(admissionVM);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully Saved" });
                }
                else
                {
                    return Json(new { status = false, message = "Save Failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}