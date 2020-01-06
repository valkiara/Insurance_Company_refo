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

namespace IBMS.Web.API.Controllers
{
    [BasicAuthentication]
    public class IntroducerController : ApiController
    {
        ManageIntroducer manageIntroducer = new ManageIntroducer();

        [HttpPost()]
        [ActionName("SaveIntroducer")]
        public IHttpActionResult SaveIntroducer([FromBody]JObject data)
        {
            try
            {
                string introducerName = !string.IsNullOrEmpty(data.SelectToken("IntroducerName").Value<string>()) ? data.SelectToken("IntroducerName").Value<string>() : string.Empty;
                string description = !string.IsNullOrEmpty(data.SelectToken("Description").Value<string>()) ? data.SelectToken("Description").Value<string>() : string.Empty;
                string address1 = !string.IsNullOrEmpty(data.SelectToken("Address1").Value<string>()) ? data.SelectToken("Address1").Value<string>() : string.Empty;
                string address2 = !string.IsNullOrEmpty(data.SelectToken("Address2").Value<string>()) ? data.SelectToken("Address2").Value<string>() : string.Empty;
                string address3 = !string.IsNullOrEmpty(data.SelectToken("Address3").Value<string>()) ? data.SelectToken("Address3").Value<string>() : string.Empty;
                List<int> businessUnitIDList = data.SelectToken("BusinessUnitIDList").ToObject<List<int>>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageIntroducer.IsIntroducerAvailable(null, introducerName))
                {
                    IntroducerVM introducerVM = new IntroducerVM();
                    introducerVM.IntroducerName = introducerName;
                    introducerVM.Description = description;
                    introducerVM.Address1 = address1;
                    introducerVM.Address2 = address2;
                    introducerVM.Address3 = address3;
                    introducerVM.BusinessUnitIDList = businessUnitIDList;
                    introducerVM.CreatedBy = userID;

                    bool status = manageIntroducer.SaveIntroducer(introducerVM);

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
                    return Json(new { status = false, message = "Introducer Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateIntroducer")]
        public IHttpActionResult UpdateIntroducer([FromBody]JObject data)
        {
            try
            {
                int introducerID = !string.IsNullOrEmpty(data.SelectToken("IntroducerID").Value<string>()) ? Convert.ToInt32(data.SelectToken("IntroducerID").Value<string>()) : 0;
                string introducerName = !string.IsNullOrEmpty(data.SelectToken("IntroducerName").Value<string>()) ? data.SelectToken("IntroducerName").Value<string>() : string.Empty;
                string description = !string.IsNullOrEmpty(data.SelectToken("Description").Value<string>()) ? data.SelectToken("Description").Value<string>() : string.Empty;
                string address1 = !string.IsNullOrEmpty(data.SelectToken("Address1").Value<string>()) ? data.SelectToken("Address1").Value<string>() : string.Empty;
                string address2 = !string.IsNullOrEmpty(data.SelectToken("Address2").Value<string>()) ? data.SelectToken("Address2").Value<string>() : string.Empty;
                string address3 = !string.IsNullOrEmpty(data.SelectToken("Address3").Value<string>()) ? data.SelectToken("Address3").Value<string>() : string.Empty;
                List<int> businessUnitIDList = data.SelectToken("BusinessUnitIDList").ToObject<List<int>>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageIntroducer.IsIntroducerAvailable(introducerID, introducerName))
                {
                    IntroducerVM introducerVM = new IntroducerVM();
                    introducerVM.IntroducerID = introducerID;
                    introducerVM.IntroducerName = introducerName;
                    introducerVM.Description = description;
                    introducerVM.Address1 = address1;
                    introducerVM.Address2 = address2;
                    introducerVM.Address3 = address3;
                    introducerVM.BusinessUnitIDList = businessUnitIDList;
                    introducerVM.ModifiedBy = userID;

                    bool status = manageIntroducer.UpdateIntroducer(introducerVM);

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
                    return Json(new { status = false, message = "Introducer Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteIntroducer")]
        public IHttpActionResult DeleteIntroducer([FromBody]JObject data)
        {
            try
            {
                int introducerID = !string.IsNullOrEmpty(data.SelectToken("IntroducerID").Value<string>()) ? Convert.ToInt32(data.SelectToken("IntroducerID").Value<string>()) : 0;
                bool status = manageIntroducer.DeleteIntroducer(introducerID);

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
        [ActionName("GetAllIntroducers")]
        public IHttpActionResult GetAllIntroducers()
        {
            try
            {
                var introducerList = manageIntroducer.GetAllIntroducers();
                return Json(new
                {
                    status = true,
                    data = introducerList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllIntroducersByBUID")]
        public IHttpActionResult GetAllIntroducersByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var introducerList = manageIntroducer.GetAllIntroducersByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = introducerList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetIntroducerByID")]
        public IHttpActionResult GetIntroducerByID([FromBody]JObject data)
        {
            try
            {
                int introducerID = !string.IsNullOrEmpty(data.SelectToken("IntroducerID").Value<string>()) ? Convert.ToInt32(data.SelectToken("IntroducerID").Value<string>()) : 0;
                var introducer = manageIntroducer.GetIntroducerByID(introducerID);
                return Json(new
                {
                    status = true,
                    data = introducer
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
