using IBMS.Service.TransactionData;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IBMS.Web.API.Controllers
{
    public class EmailController : ApiController
    {
        ManageEmail manageEmail = new ManageEmail();

        [HttpPost()]
        [ActionName("SendGeneralEmail")]
        public IHttpActionResult SendGeneralEmail([FromBody]JObject data)
        {
            try
            {
                string userName = !string.IsNullOrEmpty(data.SelectToken("UserName").Value<string>()) ? data.SelectToken("UserName").Value<string>() : string.Empty;
                string emailAddress = !string.IsNullOrEmpty(data.SelectToken("EmailAddress").Value<string>()) ? data.SelectToken("EmailAddress").Value<string>() : string.Empty;
                string emailHeader = !string.IsNullOrEmpty(data.SelectToken("EmailHeader").Value<string>()) ? data.SelectToken("EmailHeader").Value<string>() : string.Empty;
                string emailContent = !string.IsNullOrEmpty(data.SelectToken("EmailContent").Value<string>()) ? data.SelectToken("EmailContent").Value<string>() : string.Empty;

                bool status = manageEmail.SendGeneralEmail(emailAddress, userName, emailHeader, emailContent);

                if (status)
                {
                    return Json(new { status = true, message = "Email is sent successfully" });
                }
                else
                {
                    return Json(new { status = false, message = "Email is not sent" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
