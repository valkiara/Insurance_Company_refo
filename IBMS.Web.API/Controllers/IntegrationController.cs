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
using IBMS.Service.Integration;
using IBMS.Shared.ViewModel.Mapper;

namespace IBMS.Web.API.Controllers
{
    [BasicAuthentication]
    public class IntegrationController : ApiController
    {
        ManageIntegration manageIntegration = new ManageIntegration();

        [HttpPost]
        [ActionName("GetBupaAmount")]
        public IHttpActionResult GetBupaAmount([FromBody]JObject data)
        {
            try
            {
                var mapper = data.SelectToken("rootObj").ToObject<FilterMapper>();
                object obj = manageIntegration.GetBupaAmount(mapper);
                return Json(new { status = true, data = obj });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
        }

        [HttpPost]
        [ActionName("SaveTransactionDetail")]
        public IHttpActionResult SaveTransactionDetail([FromBody]JObject data)
        {
            try
            {
                var para = data.SelectToken("rootObj").ToObject<IntegrationViewModel>();
                IntegrationViewModel integrationViewModel = new IntegrationViewModel();
                bool saveStatus = manageIntegration.Insert(para);
                return Json(new { status = saveStatus, data = "" });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
        }

        [HttpPost]
        [ActionName("GetBUPACommissionByDate")]
        public IHttpActionResult GetBUPACommissionByDate([FromBody]JObject data)
        {
            try
            {
                var filterMapper = data.SelectToken("filterObj").ToObject<FilterMapper>();
                object obj = manageIntegration.GetBUPACommissionByDate(filterMapper);
                return Json(new { status = true, data = obj });            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
        }
        [HttpPost]
        [ActionName("GetBUPACommissionDetailByDate")]
        public IHttpActionResult GetBUPACommissionDetailByDate([FromBody]JObject data)
        {
            try
            {
                var para = data.SelectToken("rootObj").ToObject<FilterMapper> ();
                object obj = manageIntegration.GetBUPACommissionDetailByDate(para);
                return Json(new { status = true, data = obj });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
        }

    }
}