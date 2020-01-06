using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IBMS.Service.Dashboard;
using Newtonsoft.Json.Linq;

namespace IBMS.Web.API.Controllers
{
    public class DashboardController : ApiController
    {
        ManageDashboard manageDashboard = new ManageDashboard();

        [HttpPost()]
        [ActionName("GetCountQuotation")]
        public IHttpActionResult GetCountQuotation([FromBody]JObject data)
        {
            try
            {
                var client = manageDashboard.GetQuotationCount();
                return Json(new
                {
                    status = true,
                    data = client
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetClienVSQuotation")]
        public IHttpActionResult GetClienVSQuotation([FromBody]JObject data)
        {
            try
            {
                var client1 = manageDashboard.GetClienVSQuotation();
                return Json(new
                {
                    status = true,
                    data = client1
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetBusinessUnitCount")]

        public IHttpActionResult GetBusinessUnitCount([FromBody]JObject data)
        {
            try
            {
                var buCount = manageDashboard.GetBusinessUnitPer();
                return Json(new
                {
                    status = true,
                    data = buCount
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }



        [HttpPost()]
        [ActionName("GetClientPayment")]

        public IHttpActionResult GetClientPayment([FromBody]JObject data)
        {
            try
            {
                var clientPayment = manageDashboard.GetClientPaymentforDashboard();
                return Json(new
                {
                    status = true,
                    data = clientPayment
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        [HttpPost()]
        [ActionName("GetAllPayment")]

        public IHttpActionResult GetAllPayment([FromBody]JObject Data)
        {
            try
            {
                var Payment = manageDashboard.GetPaymentAll();
                return Json(new
                {
                    status = true,
                    data = Payment


                });
                    
            }
            catch ( Exception ex)
            {

                throw ex;
            }
        }
    }
}
