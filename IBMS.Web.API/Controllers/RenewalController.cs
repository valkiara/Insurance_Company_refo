using IBMS.Web.API.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IBMS.Service.TransactionData;
using Newtonsoft.Json.Linq;
using IBMS.Shared.ViewModel.Mapper;

namespace IBMS.Web.API.Controllers
{
    [BasicAuthentication]
    public class RenewalController : ApiController
    {
        ManageRenewal manageRenewal = new ManageRenewal();
        [HttpPost]
        public IHttpActionResult GetClientExpireInfo([FromBody]JObject data)
        {
            try
            {
                var filterMapper = data.SelectToken("filterObj").ToObject<FilterMapper>();
                var result = manageRenewal.GetClientExpireInfo(filterMapper);
                return Json(new { status = true, data = result });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
        }
    }
}
