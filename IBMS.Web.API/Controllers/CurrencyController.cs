using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IBMS.Service.MasterData;
namespace IBMS.Web.API.Controllers
{

    public class CurrencyController : ApiController
    {
        ManageCurrency manageCurrency = new ManageCurrency();

        [HttpPost()]
        [ActionName("GetCurrency")]
        public IHttpActionResult GetCurrency()
        {
            var result = manageCurrency.GetCurrency();
            try
            {
                return Json(new
                {
                    status = true,
                    data = result
                });
            }
            catch (Exception)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
