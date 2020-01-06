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
    public class RateCategoryController : ApiController
    {
        ManageRateCategory manageRateCategory = new ManageRateCategory();

        [HttpPost()]
        [ActionName("SaveRateCategory")]
        public IHttpActionResult SaveRateCategory([FromBody]JObject data)
        {
            try
            {
                string rateCategory = !string.IsNullOrEmpty(data.SelectToken("RateCategory").Value<string>()) ? data.SelectToken("RateCategory").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageRateCategory.IsRateCategoryAvailable(null, rateCategory))
                {
                    RateCategoryVM rateCategoryVM = new RateCategoryVM();
                    rateCategoryVM.RateCategoryName = rateCategory;
                    rateCategoryVM.CreatedBy = userID;

                    bool status = manageRateCategory.SaveRateCategory(rateCategoryVM);

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
                    return Json(new { status = false, message = "Rate Category Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateRateCategory")]
        public IHttpActionResult UpdateRateCategory([FromBody]JObject data)
        {
            try
            {
                int rateCategoryID = !string.IsNullOrEmpty(data.SelectToken("RateCategoryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("RateCategoryID").Value<string>()) : 0;
                string rateCategory = !string.IsNullOrEmpty(data.SelectToken("RateCategory").Value<string>()) ? data.SelectToken("RateCategory").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageRateCategory.IsRateCategoryAvailable(rateCategoryID, rateCategory))
                {
                    RateCategoryVM rateCategoryVM = new RateCategoryVM();
                    rateCategoryVM.RateCategoryID = rateCategoryID;
                    rateCategoryVM.RateCategoryName = rateCategory;
                    rateCategoryVM.ModifiedBy = userID;

                    bool status = manageRateCategory.UpdateRateCategory(rateCategoryVM);

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
                    return Json(new { status = false, message = "Rate Category Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteRateCategory")]
        public IHttpActionResult DeleteRateCategory([FromBody]JObject data)
        {
            try
            {
                int rateCategoryID = !string.IsNullOrEmpty(data.SelectToken("RateCategoryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("RateCategoryID").Value<string>()) : 0;
                bool status = manageRateCategory.DeleteRateCategory(rateCategoryID);

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
        [ActionName("GetAllRateCategories")]
        public IHttpActionResult GetAllRateCategories()
        {
            try
            {
                var rateCategoryList = manageRateCategory.GetAllRateCategories();
                return Json(new
                {
                    status = true,
                    data = rateCategoryList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetRateCategoryByID")]
        public IHttpActionResult GetRateCategoryByID([FromBody]JObject data)
        {
            try
            {
                int rateCategoryID = !string.IsNullOrEmpty(data.SelectToken("RateCategoryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("RateCategoryID").Value<string>()) : 0;
                var rateCategory = manageRateCategory.GetRateCategoryByID(rateCategoryID);
                return Json(new
                {
                    status = true,
                    data = rateCategory
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
