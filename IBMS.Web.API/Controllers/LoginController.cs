using IBMS.Service.MasterData;
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
    public class LoginController : ApiController
    {
        ManageLogin manageLogin = new ManageLogin();

        [HttpPost()]
        [ActionName("AuthenticateUser")]
        public IHttpActionResult AuthenticateUser([FromBody]JObject data)
        {
            try
            {
                string loginName = !string.IsNullOrEmpty(data.SelectToken("loginName").Value<string>()) ? data.SelectToken("loginName").Value<string>() : string.Empty;
                string password = !string.IsNullOrEmpty(data.SelectToken("password").Value<string>()) ? data.SelectToken("password").Value<string>() : string.Empty;
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("businessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("businessUnitID").Value<string>()) : 0;

                int? userID = null;
                string loginStatus = manageLogin.AuthenticateUser(loginName, password, businessUnitID, out userID);

                if (loginStatus.Equals("PASS"))
                {
                    //Get Logged User Details
                    var user = manageLogin.GetLoggedUserByID(Convert.ToInt32(userID));

                    //Update User Last Login
                    manageLogin.UpdateUserLastLoginDetails(Convert.ToInt32(userID));

                    return Json(new { status = true, message = "Login Successful", data = user });
                }
                else if (loginStatus.Equals("IU"))
                {
                    //return Json(new { status = false, message = "Invalid User" });
                    return Json(new { status = false, message = "Invalid Username or Password" });
                }
                else if (loginStatus.Equals("WP"))
                {
                    //return Json(new { status = false, message = "Wrong Password" });
                    return Json(new { status = false, message = "Invalid Username or Password" });
                }
                else
                {
                    return Json(new { status = false, message = "Please Enter Username and Password" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
