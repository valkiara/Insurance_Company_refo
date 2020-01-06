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
    public class InsComStrcuctureController : ApiController
    {
        
        #region Header and 
        ManageInsCommission manageInsCommission = new ManageInsCommission();

        [HttpPost()]
        [ActionName("SaveInsComStructureHeader")]
        public IHttpActionResult SaveInsComStructureHeader([FromBody]JObject data)
        {
            try
            {
                string comStructName = !string.IsNullOrEmpty(data.SelectToken("ComStructName").Value<string>()) ? data.SelectToken("ComStructName").Value<string>() : string.Empty;
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BUID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BUID").Value<string>()) : 0;
                int insCompanyID = !string.IsNullOrEmpty(data.SelectToken("InsCompanyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InsCompanyID").Value<string>()) : 0;
                int insClassID = !string.IsNullOrEmpty(data.SelectToken("InsClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InsClassID").Value<string>()) : 0;
                int insSubClassID = !string.IsNullOrEmpty(data.SelectToken("InsSubClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("InsSubClassID").Value<string>()) : 0;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;
                List<ChargeTypeVM> chargeType = data.SelectToken("policyInfoRecObj").ToObject<List<ChargeTypeVM>>();
                //ChargeTypeVM chargeType = data.SelectToken("policyInfoChargeList").ToObject<ChargeTypeVM>();
                


                //chargeType.chargeTypeObj = chargeType;



                if (!manageInsCommission.IsInsCommisionStructureHeaderAvailable(null, comStructName))
                {
                    InsCommissionStructureHeaderVM commisionStructureHeaderVM = new InsCommissionStructureHeaderVM();
                    commisionStructureHeaderVM.CommisionStructureName = comStructName;
                    commisionStructureHeaderVM.BusinessUnitID = businessUnitID;
                    commisionStructureHeaderVM.InsuranceCompanyID = insCompanyID;
                    commisionStructureHeaderVM.InsuranceClassID = insClassID;
                    commisionStructureHeaderVM.InsuranceSubClassID = insSubClassID;
                    commisionStructureHeaderVM.CreatedBy = userID;
                    commisionStructureHeaderVM.ChargeTypeList = chargeType;

                    bool status = manageInsCommission.SaveInsCommisionStructureHeader(commisionStructureHeaderVM);

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

        #endregion
        [HttpPost()]
        [ActionName("GetAllComStructureHeaders")]
        public IHttpActionResult GetAllComStructureHeaders([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var InsCommissionList = manageInsCommission.GetAllInsCommisionStructureHeaders(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = InsCommissionList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

    }
}
