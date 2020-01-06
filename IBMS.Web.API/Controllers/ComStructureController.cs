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
    public class ComStructureController : ApiController
    {
        ManageCommisionStructure manageCommisionStructure = new ManageCommisionStructure();

        #region Commision Type
        [HttpPost()]
        [ActionName("SaveComType")]
        public IHttpActionResult SaveComType([FromBody]JObject data)
        {
            try
            {
                string commisionType = !string.IsNullOrEmpty(data.SelectToken("CommisionType").Value<string>()) ? data.SelectToken("CommisionType").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageCommisionStructure.IsCommisionTypeAvailable(null, commisionType))
                {
                    CommisionTypeVM commisionTypeVM = new CommisionTypeVM();
                    commisionTypeVM.CommisionTypeName = commisionType;
                    commisionTypeVM.CreatedBy = userID;

                    bool status = manageCommisionStructure.SaveCommisionType(commisionTypeVM);

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
                    return Json(new { status = false, message = "Commission Type already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateComType")]
        public IHttpActionResult UpdateComType([FromBody]JObject data)
        {
            try
            {
                int commisionTypeID = !string.IsNullOrEmpty(data.SelectToken("CommisionTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("CommisionTypeID").Value<string>()) : 0;
                string commisionType = !string.IsNullOrEmpty(data.SelectToken("CommisionType").Value<string>()) ? data.SelectToken("CommisionType").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageCommisionStructure.IsCommisionTypeAvailable(commisionTypeID, commisionType))
                {
                    CommisionTypeVM commisionTypeVM = new CommisionTypeVM();
                    commisionTypeVM.CommisionTypeID = commisionTypeID;
                    commisionTypeVM.CommisionTypeName = commisionType;
                    commisionTypeVM.ModifiedBy = userID;

                    bool status = manageCommisionStructure.UpdateCommisionType(commisionTypeVM);

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
                    return Json(new { status = false, message = "Commission Type already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteComType")]
        public IHttpActionResult DeleteComType([FromBody]JObject data)
        {
            try
            {
                int commisionTypeID = !string.IsNullOrEmpty(data.SelectToken("CommisionTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("CommisionTypeID").Value<string>()) : 0;
                bool status = manageCommisionStructure.DeleteCommisionType(commisionTypeID);

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
        [ActionName("GetAllComTypes")]
        public IHttpActionResult GetAllComTypes()
        {
            try
            {
                var comTypeList = manageCommisionStructure.GetAllCommisionTypes();
                return Json(new
                {
                    status = true,
                    data = comTypeList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetComTypeID")]
        public IHttpActionResult GetComTypeID([FromBody]JObject data)
        {
            try
            {
                int commisionTypeID = !string.IsNullOrEmpty(data.SelectToken("CommisionTypeID").Value<string>()) ? Convert.ToInt32(data.SelectToken("CommisionTypeID").Value<string>()) : 0;
                var comType = manageCommisionStructure.GetCommisionTypeByID(commisionTypeID);
                return Json(new
                {
                    status = true,
                    data = comType
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        #endregion

        #region Commision Structure Header
        [HttpPost()]
        [ActionName("SaveComStructureHeader")]
        public IHttpActionResult SaveComStructureHeader([FromBody]JObject data)
        {
            try
            {
                string comStructName = !string.IsNullOrEmpty(data.SelectToken("ComStructName").Value<string>()) ? data.SelectToken("ComStructName").Value<string>() : string.Empty;
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BUID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BUID").Value<string>()) : 0;
                int partnerID = !string.IsNullOrEmpty(data.SelectToken("PartnerID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PartnerID").Value<string>()) : 0;
                int insCompanyID = !string.IsNullOrEmpty(data.SelectToken("InsCompanyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InsCompanyID").Value<string>()) : 0;
                int insClassID = !string.IsNullOrEmpty(data.SelectToken("InsClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InsClassID").Value<string>()) : 0;
                int insSubClassID = !string.IsNullOrEmpty(data.SelectToken("InsSubClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InsSubClassID").Value<string>()) : 0;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageCommisionStructure.IsCommisionStructureHeaderAvailable(null, comStructName))
                {
                    CommisionStructureHeaderVM commisionStructureHeaderVM = new CommisionStructureHeaderVM();
                    commisionStructureHeaderVM.CommisionStructureName = comStructName;
                    commisionStructureHeaderVM.BusinessUnitID = businessUnitID;
                    commisionStructureHeaderVM.PartnerID = partnerID;
                    commisionStructureHeaderVM.InsuranceCompanyID = insCompanyID;
                    commisionStructureHeaderVM.InsuranceClassID = insClassID;
                    commisionStructureHeaderVM.InsuranceSubClassID = insSubClassID;
                    commisionStructureHeaderVM.CreatedBy = userID;

                    bool status = manageCommisionStructure.SaveCommisionStructureHeader(commisionStructureHeaderVM);

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
                    return Json(new { status = false, message = "Commission Structure Header already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateComStructureHeader")]
        public IHttpActionResult UpdateComStructureHeader([FromBody]JObject data)
        {
            try
            {
                int comStructID = !string.IsNullOrEmpty(data.SelectToken("ComStructID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ComStructID").Value<string>()) : 0;
                string comStructName = !string.IsNullOrEmpty(data.SelectToken("ComStructName").Value<string>()) ? data.SelectToken("ComStructName").Value<string>() : string.Empty;
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BUID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BUID").Value<string>()) : 0;
                int partnerID = !string.IsNullOrEmpty(data.SelectToken("PartnerID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PartnerID").Value<string>()) : 0;
                int insCompanyID = !string.IsNullOrEmpty(data.SelectToken("InsCompanyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InsCompanyID").Value<string>()) : 0;
                int insClassID = !string.IsNullOrEmpty(data.SelectToken("InsClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InsClassID").Value<string>()) : 0;
                int insSubClassID = !string.IsNullOrEmpty(data.SelectToken("InsSubClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InsSubClassID").Value<string>()) : 0;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageCommisionStructure.IsCommisionStructureHeaderAvailable(comStructID, comStructName))
                {
                    CommisionStructureHeaderVM commisionStructureHeaderVM = new CommisionStructureHeaderVM();
                    commisionStructureHeaderVM.CommisionStructureID = comStructID;
                    commisionStructureHeaderVM.CommisionStructureName = comStructName;
                    commisionStructureHeaderVM.BusinessUnitID = businessUnitID;
                    commisionStructureHeaderVM.PartnerID = partnerID;
                    commisionStructureHeaderVM.InsuranceCompanyID = insCompanyID;
                    commisionStructureHeaderVM.InsuranceClassID = insClassID;
                    commisionStructureHeaderVM.InsuranceSubClassID = insSubClassID;
                    commisionStructureHeaderVM.ModifiedBy = userID;

                    bool status = manageCommisionStructure.UpdateCommisionStructureHeader(commisionStructureHeaderVM);

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
                    return Json(new { status = false, message = "Commission Structure Header already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteComStructureHeader")]
        public IHttpActionResult DeleteComStructureHeader([FromBody]JObject data)
        {
            try
            {
                int comStructID = !string.IsNullOrEmpty(data.SelectToken("ComStructID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ComStructID").Value<string>()) : 0;
                bool status = manageCommisionStructure.DeleteCommisionStructureHeader(comStructID);

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
        [ActionName("GetAllComStructureHeaders")]
        public IHttpActionResult GetAllComStructureHeaders()
        {
            try
            {
                var comStructureHeaderList = manageCommisionStructure.GetAllCommisionStructureHeaders();
                
                return Json(new
                {
                    status = true,
                    data = comStructureHeaderList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllComStructureHeadersByBUID")]
        public IHttpActionResult GetAllComStructureHeadersByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var comStructureHeaderList = manageCommisionStructure.GetAllCommisionStructureHeadersByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = comStructureHeaderList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetComStructureHeaderByID")]
        public IHttpActionResult GetComStructureHeaderByID([FromBody]JObject data)
        {
            try
            {
                int comStructID = !string.IsNullOrEmpty(data.SelectToken("ComStructID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ComStructID").Value<string>()) : 0;
                var comStructureHeader = manageCommisionStructure.GetCommisionStructureHeaderByID(comStructID);
                return Json(new
                {
                    status = true,
                    data = comStructureHeader
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        #endregion

        #region Commision Structure Line
        [HttpPost()]
        [ActionName("SaveComStructureLine")]
        public IHttpActionResult SaveComStructureLine([FromBody]JObject data)
        {
            try
            {
                int comStructID = !string.IsNullOrEmpty(data.SelectToken("ComStructID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ComStructID").Value<string>()) : 0;
                int rateCategoryID =  0;
                bool isAgeConsider = !string.IsNullOrEmpty(data.SelectToken("IsAgeConsider").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsAgeConsider").Value<string>()) : false;
                int ageFrom = !string.IsNullOrEmpty(data.SelectToken("AgeFrom").Value<string>()) ? Convert.ToInt32(data.SelectToken("AgeFrom").Value<string>()) : 0;
                int ageTo = !string.IsNullOrEmpty(data.SelectToken("AgeTo").Value<string>()) ? Convert.ToInt32(data.SelectToken("AgeTo").Value<string>()) : 0;
                bool isFixed = !string.IsNullOrEmpty(data.SelectToken("isFixed").Value<string>()) ? Convert.ToBoolean(data.SelectToken("isFixed").Value<string>()) : false;
                double rateValue = !string.IsNullOrEmpty(data.SelectToken("RateValue").Value<string>()) ? Convert.ToDouble(data.SelectToken("RateValue").Value<string>()) : 0;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                CommisionStructureLineVM commisionStructureLineVM = new CommisionStructureLineVM();
                commisionStructureLineVM.CommisionStructureID = comStructID;
                commisionStructureLineVM.RateCategoryID = rateCategoryID;
                commisionStructureLineVM.IsAgeConsider = isAgeConsider;
                commisionStructureLineVM.AgeFrom = ageFrom;
                commisionStructureLineVM.AgeTo = ageTo;
                commisionStructureLineVM.IsFixed = isFixed;
                commisionStructureLineVM.RateValue = rateValue;
                commisionStructureLineVM.CreatedBy = userID;

                bool status = manageCommisionStructure.SaveCommisionStructureLine(commisionStructureLineVM);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully Saved" });
                }
                else
                {
                    return Json(new { status = false, message = "Save Failed" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateComStructureLine")]
        public IHttpActionResult UpdateComStructureLine([FromBody]JObject data)
        {
            try
            {
                int comStructLineID = !string.IsNullOrEmpty(data.SelectToken("ComStructLineID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ComStructLineID").Value<string>()) : 0;
                int comStructID = !string.IsNullOrEmpty(data.SelectToken("ComStructID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ComStructID").Value<string>()) : 0;
                int rateCategoryID = !string.IsNullOrEmpty(data.SelectToken("RateCategoryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("RateCategoryID").Value<string>()) : 0;
                bool isAgeConsider = !string.IsNullOrEmpty(data.SelectToken("IsAgeConsider").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsAgeConsider").Value<string>()) : false;
                int ageFrom = !string.IsNullOrEmpty(data.SelectToken("AgeFrom").Value<string>()) ? Convert.ToInt32(data.SelectToken("AgeFrom").Value<string>()) : 0;
                int ageTo = !string.IsNullOrEmpty(data.SelectToken("AgeTo").Value<string>()) ? Convert.ToInt32(data.SelectToken("AgeTo").Value<string>()) : 0;
                bool isFixed = !string.IsNullOrEmpty(data.SelectToken("isFixed").Value<string>()) ? Convert.ToBoolean(data.SelectToken("isFixed").Value<string>()) : false;
                double rateValue = !string.IsNullOrEmpty(data.SelectToken("RateValue").Value<string>()) ? Convert.ToDouble(data.SelectToken("RateValue").Value<string>()) : 0;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                CommisionStructureLineVM commisionStructureLineVM = new CommisionStructureLineVM();
                commisionStructureLineVM.CommisionStructureLineID = comStructLineID;
                commisionStructureLineVM.CommisionStructureID = comStructID;
                commisionStructureLineVM.RateCategoryID = rateCategoryID;
                commisionStructureLineVM.IsAgeConsider = isAgeConsider;
                commisionStructureLineVM.AgeFrom = ageFrom;
                commisionStructureLineVM.AgeTo = ageTo;
                commisionStructureLineVM.IsFixed = isFixed;
                commisionStructureLineVM.RateValue = rateValue;
                commisionStructureLineVM.ModifiedBy = userID;

                bool status = manageCommisionStructure.UpdateCommisionStructureLine(commisionStructureLineVM);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully updated" });
                }
                else
                {
                    return Json(new { status = false, message = "Update Failed" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteComStructureLine")]
        public IHttpActionResult DeleteComStructureLine([FromBody]JObject data)
        {
            try
            {
                int comStructLineID = !string.IsNullOrEmpty(data.SelectToken("ComStructLineID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ComStructLineID").Value<string>()) : 0;
                bool status = manageCommisionStructure.DeleteCommisionStructureLine(comStructLineID);

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
        [ActionName("GetAllComStructureLines")]
        public IHttpActionResult GetAllComStructureLines()
        {
            try
            {
                var comStructureLineList = manageCommisionStructure.GetAllCommisionStructureLines();
                return Json(new
                {
                    status = true,
                    data = comStructureLineList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllComStructureLinesByComStructureHeaderID")]
        public IHttpActionResult GetAllComStructureLinesByComStructureHeaderID([FromBody]JObject data)
        {
            try
            {
                int comStructureHeaderID = !string.IsNullOrEmpty(data.SelectToken("ComStructureHeaderID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ComStructureHeaderID").Value<string>()) : 0;
                var comStructureLineList = manageCommisionStructure.GetAllCommisionStructureLinesByComStructHeaderID(comStructureHeaderID);
                return Json(new
                {
                    status = true,
                    data = comStructureLineList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllComStructureLinesByBUID")]
        public IHttpActionResult GetAllComStructureLinesByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var comStructureLineList = manageCommisionStructure.GetAllCommisionStructureLinesByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = comStructureLineList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetComStructureLineByID")]
        public IHttpActionResult GetComStructureLineByID([FromBody]JObject data)
        {
            try
            {
                int comStructLineID = !string.IsNullOrEmpty(data.SelectToken("ComStructLineID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ComStructLineID").Value<string>()) : 0;
                var comStructureLine = manageCommisionStructure.GetCommisionStructureLineByID(comStructLineID);
                return Json(new
                {
                    status = true,
                    data = comStructureLine
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        #endregion


        #region Commission upload


        #endregion
    }
}
