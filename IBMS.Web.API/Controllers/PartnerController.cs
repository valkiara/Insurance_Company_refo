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
    public class PartnerController : ApiController
    {
        ManagePartner managePartner = new ManagePartner();

        #region Partner
        [HttpPost()]
        [ActionName("SavePartner")]
        public IHttpActionResult SavePartner([FromBody]JObject data)
        {
            try
            {
                string partnerName = !string.IsNullOrEmpty(data.SelectToken("PartnerName").Value<string>()) ? data.SelectToken("PartnerName").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!managePartner.IsPartnerAvailable(null, partnerName))
                {
                    PartnerVM partnerVM = new PartnerVM();
                    partnerVM.PartnerName = partnerName;
                    partnerVM.CreatedBy = userID;

                    bool status = managePartner.SavePartner(partnerVM);

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
                    return Json(new { status = false, message = "Partner Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdatePartner")]
        public IHttpActionResult UpdatePartner([FromBody]JObject data)
        {
            try
            {
                int partnerID = !string.IsNullOrEmpty(data.SelectToken("PartnerID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PartnerID").Value<string>()) : 0;
                string partnerName = !string.IsNullOrEmpty(data.SelectToken("PartnerName").Value<string>()) ? data.SelectToken("PartnerName").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!managePartner.IsPartnerAvailable(partnerID, partnerName))
                {
                    PartnerVM partnerVM = new PartnerVM();
                    partnerVM.PartnerID = partnerID;
                    partnerVM.PartnerName = partnerName;
                    partnerVM.ModifiedBy = userID;

                    bool status = managePartner.UpdatePartner(partnerVM);

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
                    return Json(new { status = false, message = "Partner Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeletePartner")]
        public IHttpActionResult DeletePartner([FromBody]JObject data)
        {
            try
            {
                int partnerID = !string.IsNullOrEmpty(data.SelectToken("PartnerID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PartnerID").Value<string>()) : 0;
                bool status = managePartner.DeletePartner(partnerID);

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
        [ActionName("GetAllPartners")]
        public IHttpActionResult GetAllPartners()
        {
            try
            {
                var partnerList = managePartner.GetAllPartners();
                return Json(new
                {
                    status = true,
                    data = partnerList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetPartnerByID")]
        public IHttpActionResult GetPartnerByID([FromBody]JObject data)
        {
            try
            {
                int partnerID = !string.IsNullOrEmpty(data.SelectToken("PartnerID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PartnerID").Value<string>()) : 0;
                var partner = managePartner.GetPartnerByID(partnerID);
                return Json(new
                {
                    status = true,
                    data = partner
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        #endregion

        //Partner Mapping need to be modified
        #region Partner Mapping
        [HttpPost()]
        [ActionName("SavePartnerMapping")]
        public IHttpActionResult SavePartnerMapping([FromBody]JObject data)
        {
            try
            {
                string partnerName = !string.IsNullOrEmpty(data.SelectToken("PartnerName").Value<string>()) ? data.SelectToken("PartnerName").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!managePartner.IsPartnerMappingAvailable(null, partnerName))
                {
                    PartnerMappingVM partnerMappingVM = new PartnerMappingVM();
                    partnerMappingVM.PartnerName = partnerName;
                    partnerMappingVM.CreatedBy = userID;

                    bool status = managePartner.SavePartnerMapping(partnerMappingVM);

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
                    return Json(new { status = false, message = "Partner Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdatePartnerMapping")]
        public IHttpActionResult UpdatePartnerMapping([FromBody]JObject data)
        {
            try
            {
                int partnerID = !string.IsNullOrEmpty(data.SelectToken("PartnerID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PartnerID").Value<string>()) : 0;
                string partnerName = !string.IsNullOrEmpty(data.SelectToken("PartnerName").Value<string>()) ? data.SelectToken("PartnerName").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!managePartner.IsPartnerMappingAvailable(partnerID, partnerName))
                {
                    PartnerMappingVM partnerMappingVM = new PartnerMappingVM();
                    partnerMappingVM.PartnerID = partnerID;
                    partnerMappingVM.PartnerName = partnerName;
                    partnerMappingVM.ModifiedBy = userID;

                    bool status = managePartner.UpdatePartnerMapping(partnerMappingVM);

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
                    return Json(new { status = false, message = "Partner Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeletePartnerMapping")]
        public IHttpActionResult DeletePartnerMapping([FromBody]JObject data)
        {
            try
            {
                int partnerID = !string.IsNullOrEmpty(data.SelectToken("PartnerID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PartnerID").Value<string>()) : 0;
                bool status = managePartner.DeletePartnerMapping(partnerID);

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
        [ActionName("GetAllPartnerMappings")]
        public IHttpActionResult GetAllPartnerMappings()
        {
            try
            {
                var partnerList = managePartner.GetAllPartnerMappings();
                return Json(new
                {
                    status = true,
                    data = partnerList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetPartnerMappingByID")]
        public IHttpActionResult GetPartnerMappingByID([FromBody]JObject data)
        {
            try
            {
                int partnerID = !string.IsNullOrEmpty(data.SelectToken("PartnerID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PartnerID").Value<string>()) : 0;
                var partner = managePartner.GetPartnerMappingByID(partnerID);
                return Json(new
                {
                    status = true,
                    data = partner
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
