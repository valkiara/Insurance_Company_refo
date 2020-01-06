using IBMS.Service.AdminData;
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
    public class UserController : ApiController
    {
        ManageUser manageUser = new ManageUser();

        [HttpPost()]
        [ActionName("UpdateUser")]
        public IHttpActionResult UpdateUser([FromBody]JObject data)
        {
            try
            {
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;
                string userName = !string.IsNullOrEmpty(data.SelectToken("UserName").Value<string>()) ? data.SelectToken("UserName").Value<string>() : string.Empty;
                string loginName = !string.IsNullOrEmpty(data.SelectToken("LoginName").Value<string>()) ? data.SelectToken("LoginName").Value<string>() : string.Empty;
                int designationID = !string.IsNullOrEmpty(data.SelectToken("DesignationID").Value<string>()) ? Convert.ToInt32(data.SelectToken("DesignationID").Value<string>()) : 0;
                int loggedUserID = !string.IsNullOrEmpty(data.SelectToken("LoggedUserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("LoggedUserID").Value<string>()) : 0;

                if (!manageUser.IsLoginNameAvailable(userID, loginName))
                {
                    UserVM userVM = new UserVM();
                    userVM.UserID = userID;
                    userVM.UserName = userName;
                    userVM.LoginName = loginName;
                    userVM.DesignationID = designationID;
                    userVM.ModifiedBy = loggedUserID;

                    bool status = manageUser.UpdateUser(userVM);

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
                    return Json(new { status = false, message = "Login Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("ChangePassword")]
        public IHttpActionResult ChangePassword([FromBody]JObject data)
        {
            try
            {
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;
                string oldPassword = !string.IsNullOrEmpty(data.SelectToken("OldPassword").Value<string>()) ? data.SelectToken("OldPassword").Value<string>() : string.Empty;
                string newPassword = !string.IsNullOrEmpty(data.SelectToken("NewPassword").Value<string>()) ? data.SelectToken("NewPassword").Value<string>() : string.Empty;

                var status = manageUser.ChangePassword(userID, oldPassword, newPassword);

                if (status == "SUCCESS")
                {
                    return Json(new { status = true, message = "Password is changed successfully" });
                }
                else if (status == "WOP")
                {
                    return Json(new { status = false, message = "Old password is wrong" });
                }
                else
                {
                    return Json(new { status = false, message = "Unknown Error" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetUserByID")]
        public IHttpActionResult GetUserByID([FromBody]JObject data)
        {
            try
            {
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;
                var user = manageUser.GetUserByID(userID);
                return Json(new
                {
                    status = true,
                    data = user
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
