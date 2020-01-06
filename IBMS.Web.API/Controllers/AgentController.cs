using IBMS.Service.MasterData;
using IBMS.Shared.ViewModel;
using IBMS.Shared.ViewModel.Mapper;
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
    public class AgentController : ApiController
    {
        ManageAgent manageAgent = new ManageAgent();

        [HttpPost()]
        [ActionName("SaveAgent")]
        public IHttpActionResult SaveAgent([FromBody]JObject data)
        {
            try
            {
                int companyID = !string.IsNullOrEmpty(data.SelectToken("companyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("companyID").Value<string>()) : 0;
                string agentName = !string.IsNullOrEmpty(data.SelectToken("agentName").Value<string>()) ? data.SelectToken("agentName").Value<string>() : string.Empty;
                string address1 = !string.IsNullOrEmpty(data.SelectToken("address1").Value<string>()) ? data.SelectToken("address1").Value<string>() : string.Empty;
                string address2 = !string.IsNullOrEmpty(data.SelectToken("address2").Value<string>()) ? data.SelectToken("address2").Value<string>() : string.Empty;
                string address3 = !string.IsNullOrEmpty(data.SelectToken("address3").Value<string>()) ? data.SelectToken("address3").Value<string>() : string.Empty;
                string agentType = !string.IsNullOrEmpty(data.SelectToken("agentType").Value<string>()) ? data.SelectToken("agentType").Value<string>() : string.Empty;
                //string agentNIC = !string.IsNullOrEmpty(data.SelectToken("agentNIC").Value<string>()) ? data.SelectToken("agentNIC").Value<string>() : string.Empty;
                string agentBR = !string.IsNullOrEmpty(data.SelectToken("agentBR").Value<string>()) ? data.SelectToken("agentBR").Value<string>() : string.Empty;
                double rateValue = !string.IsNullOrEmpty(data.SelectToken("rateValue").Value<string>()) ? Convert.ToDouble(data.SelectToken("rateValue").Value<string>()) : 0;
                int userID = !string.IsNullOrEmpty(data.SelectToken("userID").Value<string>()) ? Convert.ToInt32(data.SelectToken("userID").Value<string>()) : 0;
                string agentCode = !string.IsNullOrEmpty(data.SelectToken("agentCode").Value<string>()) ? data.SelectToken("agentCode").Value<string>() : string.Empty;


                if (!manageAgent.IsAgentAvailable(null, agentName))
                {
                    AgentVM agentVM = new AgentVM();
                    agentVM.CompanyID = companyID;
                    agentVM.AgentName = agentName;
                    agentVM.Address1 = address1;
                    agentVM.Address2 = address2;
                    agentVM.Address3 = address3;
                    agentVM.RateValue = rateValue;
                    agentVM.AgentType = agentType;
                    agentVM.AgentNIC = "Na";
                    agentVM.AgentBR= agentBR;
                    agentVM.CreatedBy = userID;
                    agentVM.AgentCode = agentCode;

                    bool status = manageAgent.SaveAgent(agentVM);

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
                    return Json(new { status = false, message = "Agent Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateAgent")]
        public IHttpActionResult UpdateAgent([FromBody]JObject data)
        {
            try
            {
                int agentID = !string.IsNullOrEmpty(data.SelectToken("agentID").Value<string>()) ? Convert.ToInt32(data.SelectToken("agentID").Value<string>()) : 0;
                int companyID = !string.IsNullOrEmpty(data.SelectToken("companyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("companyID").Value<string>()) : 0;
                string agentName = !string.IsNullOrEmpty(data.SelectToken("agentName").Value<string>()) ? data.SelectToken("agentName").Value<string>() : string.Empty;
                string address1 = !string.IsNullOrEmpty(data.SelectToken("address1").Value<string>()) ? data.SelectToken("address1").Value<string>() : string.Empty;
                string address2 = !string.IsNullOrEmpty(data.SelectToken("address2").Value<string>()) ? data.SelectToken("address2").Value<string>() : string.Empty;
                string address3 = !string.IsNullOrEmpty(data.SelectToken("address3").Value<string>()) ? data.SelectToken("address3").Value<string>() : string.Empty;
                double rateValue = !string.IsNullOrEmpty(data.SelectToken("rateValue").Value<string>()) ? Convert.ToDouble(data.SelectToken("rateValue").Value<string>()) : 0;
                int userID = !string.IsNullOrEmpty(data.SelectToken("userID").Value<string>()) ? Convert.ToInt32(data.SelectToken("userID").Value<string>()) : 0;
                string agentCode = !string.IsNullOrEmpty(data.SelectToken("agentCode").Value<string>()) ? data.SelectToken("agentCode").Value<string>() : string.Empty;
                string agentType = !string.IsNullOrEmpty(data.SelectToken("agentType").Value<string>()) ? data.SelectToken("agentType").Value<string>() : string.Empty;
                string agentNIC = !string.IsNullOrEmpty(data.SelectToken("agentNIC").Value<string>()) ? data.SelectToken("agentNIC").Value<string>() : string.Empty;
                string agentBR = !string.IsNullOrEmpty(data.SelectToken("agentBR").Value<string>()) ? data.SelectToken("agentBR").Value<string>() : string.Empty;

                if (!manageAgent.IsAgentAvailable(agentID, agentName))
                {
                    AgentVM agentVM = new AgentVM();
                    agentVM.AgentID = agentID;
                    agentVM.CompanyID = companyID;
                    agentVM.AgentName = agentName;
                    agentVM.Address1 = address1;
                    agentVM.Address2 = address2;
                    agentVM.Address3 = address3;
                    agentVM.RateValue = rateValue;
                    agentVM.ModifiedBy = userID;
                    agentVM.AgentCode = agentCode;
                    agentVM.AgentNIC = agentNIC;
                    agentVM.AgentBR = agentBR;
                    agentVM.CreatedBy = userID;
                    agentVM.AgentType = agentType;




                    bool status = manageAgent.UpdateAgent(agentVM);

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
                    return Json(new { status = false, message = "Agent Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteAgent")]
        public IHttpActionResult DeleteAgent([FromBody]JObject data)
        {
            try
            {
                int agentID = !string.IsNullOrEmpty(data.SelectToken("agentID").Value<string>()) ? Convert.ToInt32(data.SelectToken("agentID").Value<string>()) : 0;
                bool status = manageAgent.DeleteAgent(agentID);

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
        [ActionName("GetAllAgents")]
        public IHttpActionResult GetAllAgents()
        {
            try
            {
                var agentList = manageAgent.GetAllAgents();
                return Json(new
                {
                    status = true,
                    data = agentList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }


        [HttpPost()]
        [ActionName("GetAgentsByCompanyID")]
        public IHttpActionResult GetAgentsByCompanyID([FromBody]JObject data)
        {
            try
            {
                int companyID = !string.IsNullOrEmpty(data.SelectToken("companyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("companyID").Value<string>()) : 0;
                var agentList = manageAgent.GetAgentsByCompanyID(companyID);
                return Json(new
                {
                    status = true,
                    data = agentList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAgentByID")]
        public IHttpActionResult GetAgentByID([FromBody]JObject data)
        {
            try
            {
                int agentID = !string.IsNullOrEmpty(data.SelectToken("agentID").Value<string>()) ? Convert.ToInt32(data.SelectToken("agentID").Value<string>()) : 0;
                var agent = manageAgent.GetAgentByID(agentID);
                return Json(new
                {
                    status = true,
                    data = agent
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        [HttpPost]
        [ActionName("GetAgentCommission")]
        public IHttpActionResult GetAgentCommission([FromBody]JObject data)
        {
            try
            {
                var mapper = data.SelectToken("rootObj").ToObject<FilterMapper>();
                object obj = manageAgent.GetAgentCommission(mapper);
                return Json(new { status = true, data = obj });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
        }
        [HttpPost]
        [ActionName("GetAgentCommissionByDate")]
        public IHttpActionResult GetAgentCommissionByDate([FromBody]JObject data)
        {
            try
            {
                var filterMapper = data.SelectToken("filterObj").ToObject<FilterMapper>();

                object obj = manageAgent.GetAgentCommissionByDate(filterMapper);
                return Json(new { status = true, data = obj });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message });
            }
        }

    }
}
