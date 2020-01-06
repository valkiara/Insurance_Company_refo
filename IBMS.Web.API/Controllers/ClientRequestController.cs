using IBMS.Service.TransactionData;
using IBMS.Shared.ViewModel;
using IBMS.Web.API.Security;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using SAPbobsCOM;
using IBMS.Shared.ViewModel.Mapper;

namespace IBMS.Web.API.Controllers
{
    [BasicAuthentication]
    public class ClientRequestController : ApiController
    {
        ManageClientRequest manageClientRequest = new ManageClientRequest();

        [HttpGet()]
        public IHttpActionResult test()
        {
            return Ok("called");
        }

        [HttpPost()]
        [ActionName("SaveClient")]
        public IHttpActionResult SaveClient([FromBody]JObject data)
        {
            try
            {                
                bool isClientUpdated = !string.IsNullOrEmpty(data.SelectToken("IsClientUpdated").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientUpdated").Value<string>()) : false;
                bool isClientAdded = !string.IsNullOrEmpty(data.SelectToken("IsClientAdded").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientAdded").Value<string>()) : false;
                ClientVM clientObj = data.SelectToken("ClientObj").ToObject<ClientVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;


                ClientVM clientVM = new ClientVM();
                //clientRequestVM.IsClientExist = isClientExist;
                //clientRequestVM.ClientID = clientID;
                clientVM = clientObj;
                clientVM.IsClientUpdated = isClientUpdated;
                clientVM.IsClientAdded = isClientAdded;

                clientVM.UserID = userID;
                

                bool status = manageClientRequest.SaveClient(clientVM);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully Saved" });
                }
                else
                {
                    return Json(new { status = false, message = "Save Failed" });
                }
            }
            catch(Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }

        }


        [HttpPost()]
        [ActionName("SaveClientRequest")]
        public IHttpActionResult SaveClientRequest([FromBody]JObject data)
        {
            try
            {
                //bool isClientExist = !string.IsNullOrEmpty(data.SelectToken("IsClientExist").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientExist").Value<string>()) : false;
                //int clientID = !string.IsNullOrEmpty(data.SelectToken("ClientID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClientID").Value<string>()) : 0;
                bool isClientUpdated = !string.IsNullOrEmpty(data.SelectToken("IsClientUpdated").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientUpdated").Value<string>()) : false;
                bool isClientAdded = !string.IsNullOrEmpty(data.SelectToken("IsClientAdded").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientAdded").Value<string>()) : false;
                ClientVM clientObj = data.SelectToken("ClientObj").ToObject<ClientVM>();
                ClientRequestHeaderVM clientRequestHeaderObj = data.SelectToken("ClientRequestHeaderObj").ToObject<ClientRequestHeaderVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                ClientRequestVM clientRequestVM = new ClientRequestVM();
                //clientRequestVM.IsClientExist = isClientExist;
                //clientRequestVM.ClientID = clientID;
                clientRequestVM.IsClientUpdated = isClientUpdated;
                clientRequestVM.IsClientAdded = isClientAdded;
                clientRequestVM.ClientDetails = clientObj;
                clientRequestVM.ClientRequestHeaderDetails = clientRequestHeaderObj;
                clientRequestVM.UserID = userID;

                bool status = manageClientRequest.SaveClientRequest(clientRequestVM);

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
        [ActionName("UpdateClientRequest")]
        public IHttpActionResult UpdateClientRequest([FromBody]JObject data)
        {
            try
            {
                bool isClientUpdated = !string.IsNullOrEmpty(data.SelectToken("IsClientUpdated").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientUpdated").Value<string>()) : false;
                bool isClientAdded = !string.IsNullOrEmpty(data.SelectToken("IsClientAdded").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientAdded").Value<string>()) : false;
                ClientVM clientObj = data.SelectToken("ClientObj").ToObject<ClientVM>();
                ClientRequestHeaderVM clientRequestHeaderObj = data.SelectToken("ClientRequestHeaderObj").ToObject<ClientRequestHeaderVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                clientRequestHeaderObj.ModifiedBy = userID;

                string errorMessage = string.Empty;
                bool status = manageClientRequest.UpdateClientRequest(clientRequestHeaderObj, isClientUpdated, isClientAdded, clientObj, out errorMessage);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully Updated" });
                }
                else
                {
                    //return Json(new { status = false, message = "Update Failed" });
                    return Json(new { status = false, message = errorMessage });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }


        



        [HttpPost()]
        [ActionName("SaveRequest")]
        public IHttpActionResult SaveRequest([FromBody]JObject data)
        {
            try
            {
                //bool isClientExist = !string.IsNullOrEmpty(data.SelectToken("IsClientExist").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientExist").Value<string>()) : false;
                //int clientID = !string.IsNullOrEmpty(data.SelectToken("ClientID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClientID").Value<string>()) : 0;
                bool isClientUpdated = !string.IsNullOrEmpty(data.SelectToken("IsClientUpdated").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientUpdated").Value<string>()) : false;
                bool isClientAdded = !string.IsNullOrEmpty(data.SelectToken("IsClientAdded").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientAdded").Value<string>()) : false;
                CustomerRequestDetailsVM clientObj = data.SelectToken("ClientObj").ToObject<CustomerRequestDetailsVM>();
                ClientRequestHeaderVM clientRequestHeaderObj = data.SelectToken("ClientRequestHeaderObj").ToObject<ClientRequestHeaderVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                ClientRequestVM clientRequestVM = new ClientRequestVM();
                
                clientRequestVM.IsClientUpdated = isClientUpdated;
                clientRequestVM.IsClientAdded = isClientAdded;
                //clientRequestVM.ClientDetails = clientObj;
                clientRequestVM.ClientRequestHeaderDetails = clientRequestHeaderObj;
                clientRequestVM.UserID = userID;

                bool status = manageClientRequest.SaveRequest(clientRequestVM);

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
        [ActionName("SaveAvivaRequest")]
        public IHttpActionResult SaveAvivaRequest([FromBody]JObject data)
        {
            try
            {
                //bool isClientExist = !string.IsNullOrEmpty(data.SelectToken("IsClientExist").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientExist").Value<string>()) : false;
                //int clientID = !string.IsNullOrEmpty(data.SelectToken("ClientID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClientID").Value<string>()) : 0;
                bool isClientUpdated = !string.IsNullOrEmpty(data.SelectToken("IsClientUpdated").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientUpdated").Value<string>()) : false;
                bool isClientAdded = !string.IsNullOrEmpty(data.SelectToken("IsClientAdded").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientAdded").Value<string>()) : false;
                ClientVM clientObj = data.SelectToken("ClientObj").ToObject<ClientVM>();
                ClientRequestHeaderVM clientRequestHeaderObj = data.SelectToken("ClientRequestHeaderObj").ToObject<ClientRequestHeaderVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                ClientRequestVM clientRequestVM = new ClientRequestVM();
                //clientRequestVM.IsClientExist = isClientExist;
                //clientRequestVM.ClientID = clientID;
                clientRequestVM.IsClientUpdated = isClientUpdated;
                clientRequestVM.IsClientAdded = isClientAdded;
                clientRequestVM.ClientDetails = clientObj;
                clientRequestVM.ClientRequestHeaderDetails = clientRequestHeaderObj;
                clientRequestVM.UserID = userID;

                bool status = manageClientRequest.SaveAvivaRequest(clientRequestVM);

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
        [ActionName("SaveBUPARequest")]
        public IHttpActionResult SaveBUPARequest([FromBody]JObject data)
        {
            try
            {

              //  var test = data.SelectToken("ClientObj").ToObject<CustomerRequestDetailsVM>();
                
                bool isClientUpdated = !string.IsNullOrEmpty(data.SelectToken("IsClientUpdated").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientUpdated").Value<string>()) : false;
                bool isClientAdded = !string.IsNullOrEmpty(data.SelectToken("IsClientAdded").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientAdded").Value<string>()) : false;
                ClientVM clientObj = data.SelectToken("ClientObj").ToObject<ClientVM>();
                ClientRequestHeaderVM clientRequestHeaderObj = data.SelectToken("ClientRequestHeaderObj").ToObject<ClientRequestHeaderVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                ClientRequestVM clientRequestVM = new ClientRequestVM();
             
                clientRequestVM.IsClientUpdated = isClientUpdated;
                clientRequestVM.IsClientAdded = isClientAdded;
                clientRequestVM.ClientDetails = clientObj;
                clientRequestVM.ClientRequestHeaderDetails = clientRequestHeaderObj;
                clientRequestVM.UserID = userID;
                

                bool status = manageClientRequest.SaveBUPARequest(clientRequestVM);

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
                return Json(new { status = false, message = ex.Message });
            }
        }


        [HttpPost()]
        [ActionName("SaveBUPARenewelRequest")]
        public IHttpActionResult SaveBUPARenewelRequest([FromBody]JObject data)
        {
            try
            {
                //bool isClientExist = !string.IsNullOrEmpty(data.SelectToken("IsClientExist").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientExist").Value<string>()) : false;
                //int clientID = !string.IsNullOrEmpty(data.SelectToken("ClientID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClientID").Value<string>()) : 0;
                bool isClientUpdated = !string.IsNullOrEmpty(data.SelectToken("IsClientUpdated").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientUpdated").Value<string>()) : false;
                bool isClientAdded = !string.IsNullOrEmpty(data.SelectToken("IsClientAdded").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientAdded").Value<string>()) : false;
                ClientVM clientObj = data.SelectToken("ClientObj").ToObject<ClientVM>();
                ClientRequestHeaderVM clientRequestHeaderObj = data.SelectToken("ClientRequestHeaderObj").ToObject<ClientRequestHeaderVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                ClientRequestVM clientRequestVM = new ClientRequestVM();
                //clientRequestVM.IsClientExist = isClientExist;
                //clientRequestVM.ClientID = clientID;
                clientRequestVM.IsClientUpdated = isClientUpdated;
                clientRequestVM.IsClientAdded = isClientAdded;
                clientRequestVM.ClientDetails = clientObj;
                clientRequestVM.ClientRequestHeaderDetails = clientRequestHeaderObj;
                clientRequestVM.UserID = userID;

                bool status = manageClientRequest.SaveBUPARenewelRequest(clientRequestVM);

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
        [ActionName("UpdateRequest")]
        public IHttpActionResult UpdateRequest([FromBody]JObject data)
        {
            try
            {
                bool isClientUpdated = !string.IsNullOrEmpty(data.SelectToken("IsClientUpdated").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientUpdated").Value<string>()) : false;
                bool isClientAdded = !string.IsNullOrEmpty(data.SelectToken("IsClientAdded").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientAdded").Value<string>()) : false;
                ClientVM clientObj = data.SelectToken("ClientObj").ToObject<ClientVM>();
                ClientRequestHeaderVM clientRequestHeaderObj = data.SelectToken("ClientRequestHeaderObj").ToObject<ClientRequestHeaderVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                clientRequestHeaderObj.ModifiedBy = userID;

                string errorMessage = string.Empty;
                bool status = manageClientRequest.UpdateRequest(clientRequestHeaderObj, isClientUpdated, isClientAdded, clientObj, out errorMessage);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully Updated" });
                }
                else
                {
                    //return Json(new { status = false, message = "Update Failed" });
                    return Json(new { status = false, message = errorMessage });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }



        [HttpPost()]
        [ActionName("UpdatePilotRequest")]
        public IHttpActionResult UpdatePilotRequest([FromBody]JObject data)
        {
            try
            {
                bool isClientUpdated = !string.IsNullOrEmpty(data.SelectToken("IsClientUpdated").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientUpdated").Value<string>()) : false;
                bool isClientAdded = !string.IsNullOrEmpty(data.SelectToken("IsClientAdded").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientAdded").Value<string>()) : false;
                ClientVM clientObj = data.SelectToken("ClientObj").ToObject<ClientVM>();
                ClientRequestHeaderVM clientRequestHeaderObj = data.SelectToken("ClientRequestHeaderObj").ToObject<ClientRequestHeaderVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                clientRequestHeaderObj.ModifiedBy = userID;

                string errorMessage = string.Empty;
                bool status = manageClientRequest.UpdatePilotRequest(clientRequestHeaderObj, isClientUpdated, isClientAdded, clientObj, out errorMessage);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully Updated" });
                }
                else
                {
                    //return Json(new { status = false, message = "Update Failed" });
                    return Json(new { status = false, message = errorMessage });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }


        [HttpPost()]
        [ActionName("UpdateAvivaRequest")]
        public IHttpActionResult UpdateAvivaRequest([FromBody]JObject data)
        {
            try
            {
                bool isClientUpdated = !string.IsNullOrEmpty(data.SelectToken("IsClientUpdated").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientUpdated").Value<string>()) : false;
                bool isClientAdded = !string.IsNullOrEmpty(data.SelectToken("IsClientAdded").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientAdded").Value<string>()) : false;
                ClientVM clientObj = data.SelectToken("ClientObj").ToObject<ClientVM>();
                ClientRequestHeaderVM clientRequestHeaderObj = data.SelectToken("ClientRequestHeaderObj").ToObject<ClientRequestHeaderVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                clientRequestHeaderObj.ModifiedBy = userID;

                string errorMessage = string.Empty;
                bool status = manageClientRequest.UpdateAvivaRequest(clientRequestHeaderObj, isClientUpdated, isClientAdded, clientObj, out errorMessage);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully Updated" });
                }
                else
                {
                    //return Json(new { status = false, message = "Update Failed" });
                    return Json(new { status = false, message = errorMessage });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }


        [HttpPost()]
        [ActionName("UpdateBUPARenewelRequest")]
        public IHttpActionResult UpdateBUPARenewelRequest([FromBody]JObject data)
        {
            try
            {
                bool isClientUpdated = !string.IsNullOrEmpty(data.SelectToken("IsClientUpdated").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientUpdated").Value<string>()) : false;
                bool isClientAdded = !string.IsNullOrEmpty(data.SelectToken("IsClientAdded").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientAdded").Value<string>()) : false;
                ClientVM clientObj = data.SelectToken("ClientObj").ToObject<ClientVM>();
                ClientRequestHeaderVM clientRequestHeaderObj = data.SelectToken("ClientRequestHeaderObj").ToObject<ClientRequestHeaderVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                clientRequestHeaderObj.ModifiedBy = userID;

                string errorMessage = string.Empty;
                bool status = manageClientRequest.UpdateBUPARenewelRequest(clientRequestHeaderObj, isClientUpdated, isClientAdded, clientObj, out errorMessage);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully Updated" });
                }
                else
                {
                    //return Json(new { status = false, message = "Update Failed" });
                    return Json(new { status = false, message = errorMessage });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }



        public IHttpActionResult UpdateBUPARequest([FromBody]JObject data)
        {
            try
            {
                bool isClientUpdated = !string.IsNullOrEmpty(data.SelectToken("IsClientUpdated").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientUpdated").Value<string>()) : false;
                bool isClientAdded = !string.IsNullOrEmpty(data.SelectToken("IsClientAdded").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientAdded").Value<string>()) : false;
                ClientVM clientObj = data.SelectToken("ClientObj").ToObject<ClientVM>();
                ClientRequestHeaderVM clientRequestHeaderObj = data.SelectToken("ClientRequestHeaderObj").ToObject<ClientRequestHeaderVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                clientRequestHeaderObj.ModifiedBy = userID;

                string errorMessage = string.Empty;
                bool status = manageClientRequest.UpdateBUPARequest(clientRequestHeaderObj, isClientUpdated, isClientAdded, clientObj, out errorMessage);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully Updated" });
                }
                else
                {
                    //return Json(new { status = false, message = "Update Failed" });
                    return Json(new { status = false, message = errorMessage });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }



        [HttpPost()]
        [ActionName("GetAllClients")]
        public IHttpActionResult GetAllClients()
        {
            try
            {
                var clientList = manageClientRequest.GetAllClients();
                return Json(new
                {
                    status = true,
                    data = clientList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("SearchClients")]
        public IHttpActionResult SearchClients([FromBody]JObject data)
        {
            try
            {
                int? businessUnitID = !string.IsNullOrEmpty(data.SelectToken("businessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("businessUnitID").Value<string>()) : (int?)null;
                int? homeCountryID = !string.IsNullOrEmpty(data.SelectToken("homeCountryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("homeCountryID").Value<string>()) : (int?)null;
                int? residentCountryID = !string.IsNullOrEmpty(data.SelectToken("residentCountryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("residentCountryID").Value<string>()) : (int?)null;

                businessUnitID = businessUnitID == 0 ? null : businessUnitID;
                homeCountryID = homeCountryID == 0 ? null : homeCountryID;
                residentCountryID = residentCountryID == 0 ? null : residentCountryID;

                var clientList = manageClientRequest.SearchClients(businessUnitID, homeCountryID, residentCountryID);

                return Json(new
                {
                    status = true,
                    data = clientList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllClientsByBUID")]
        public IHttpActionResult GetAllClientsByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var clientList = manageClientRequest.GetAllClientsByBusinessUnitID(businessUnitID);

                return Json(new
                {
                    status = true,
                    data = clientList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }



        [HttpPost()]
        [ActionName("loadPaymentDetails")]
        public IHttpActionResult loadPaymentDetails([FromBody]JObject data)
        {
            try
            {
                int ClientID = !string.IsNullOrEmpty(data.SelectToken("ClientID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClientID").Value<string>()) : 0;
                var clientList = manageClientRequest.GetBUPABankDetailsByClient(ClientID);

                return Json(new
                {
                    status = true,
                    data = clientList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }




        [HttpPost()]
        [ActionName("GetClientByID")]
        public IHttpActionResult GetClientByID([FromBody]JObject data)
        {
            try
            {
                int clientID = !string.IsNullOrEmpty(data.SelectToken("ClientID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClientID").Value<string>()) : 0;
                var client = manageClientRequest.GetClientByID(clientID);
                return Json(new
                {
                    status = true,
                    data = client
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetClientByClientID")]
        public IHttpActionResult GetClientByClientID([FromBody]JObject data)
        {
            try
            {
                int clientID = !string.IsNullOrEmpty(data.SelectToken("ClientID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClientID").Value<string>()) : 0;
                var client = manageClientRequest.GetClientByClientID(clientID);
                return Json(new
                {
                    status = true,
                    data = client
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }


        [HttpPost()]
        [ActionName("GetPilotClientByClientID")]
        public IHttpActionResult GetPilotClientByClientID([FromBody]JObject data)
        {
            try
            {
                int clientID = !string.IsNullOrEmpty(data.SelectToken("ClientID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClientID").Value<string>()) : 0;
                var client = manageClientRequest.GetPilotClientByClientID(clientID);
                return Json(new
                {
                    status = true,
                    data = client
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }





        [HttpPost()]
        [ActionName("GetBUPAClientByClientID")]
        public IHttpActionResult GetBUPAClientByClientID([FromBody]JObject data)
        {
            try
            {
                int clientID = !string.IsNullOrEmpty(data.SelectToken("ClientID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClientID").Value<string>()) : 0;
                var client = manageClientRequest.GetBUPAClientByClientID(clientID);
                return Json(new
                {
                    status = true,
                    data = client
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }



        [HttpPost()]
        [ActionName("GetBUPAClientHistoryByClientID")]
        public IHttpActionResult GetBUPAClientHistoryByClientID([FromBody]JObject data)
        {
            try
            {
                int clientID = !string.IsNullOrEmpty(data.SelectToken("ClientID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClientID").Value<string>()) : 0;
                var  Year = !string.IsNullOrEmpty(data.SelectToken("Year").Value<string>()) ? data.SelectToken("Year").Value<string>() : "0";
                var FrequncyID = !string.IsNullOrEmpty(data.SelectToken("FrequncyID").Value<string>()) ? data.SelectToken("FrequncyID").Value<string>() : "0";
                var FrequncyDID = !string.IsNullOrEmpty(data.SelectToken("FrequncyDID").Value<string>()) ? data.SelectToken("FrequncyDID").Value<string>() : "0";


                var client = manageClientRequest.GetBUPAClientByClientID(clientID, Year, FrequncyID, FrequncyDID);
                return Json(new
                {
                    status = true,
                    data = client
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }







        [HttpPost()]
        [ActionName("GetAllClientRequests")]
        public IHttpActionResult GetAllClientRequests()
        {
            try
            {
                var clientRequestList = manageClientRequest.GetAllClientRequests();
                return Json(new
                {
                    status = true,
                    data = clientRequestList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }




        [HttpPost()]
        [ActionName("GetAllYear")]
        public IHttpActionResult GetAllYear()
        {
            try
            {
                var clientRequestList = manageClientRequest.GetAllYear();
                return Json(new
                {
                    status = true,
                    data = clientRequestList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }






        [HttpPost()]
        [ActionName("GetAllClientRequestsByBUID")]
        public IHttpActionResult GetAllClientRequestsByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var clientRequestList = manageClientRequest.GetAllClientRequestsByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = clientRequestList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }





        [HttpPost()]
        [ActionName("loadFrequncyData")]
        public IHttpActionResult loadFrequncyData([FromBody]JObject data)
        {
            try
            {
                var fequncyID = !string.IsNullOrEmpty(data.SelectToken("fequncyID").Value<string>()) ? data.SelectToken("fequncyID").Value<string>() : "0";
                var clientRequestList = manageClientRequest.GetloadFrequncyData(fequncyID);
                return Json(new
                {
                    status = true,
                    data = clientRequestList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
                                    
        [HttpPost()]
        [ActionName("GetAllRequestsByBusinessUnitID")]
        public IHttpActionResult GetAllRequestsByBusinessUnitID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var clientRequestList = manageClientRequest.GetAllRequestsByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = clientRequestList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }



        [HttpPost()]
        [ActionName("getAllHistoryClientRequestsByBUID")]
        public IHttpActionResult getAllHistoryClientRequestsByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var clientRequestList = manageClientRequest.getAllHistoryClientRequestsByBUID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = clientRequestList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetClientRequestByID")]
        public IHttpActionResult GetClientRequestByID([FromBody]JObject data)
        {
            try
            {
                int clientRequestHeaderID = !string.IsNullOrEmpty(data.SelectToken("ClientRequestHeaderID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClientRequestHeaderID").Value<string>()) : 0;
                var clientRequest = manageClientRequest.GetClientRequestByID(clientRequestHeaderID);
                return Json(new
                {
                    status = true,
                    data = clientRequest
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetRequestByID")]
        public IHttpActionResult GetRequestByID([FromBody]JObject data)
        {
            try
            {
                int clientRequestHeaderID = !string.IsNullOrEmpty(data.SelectToken("ClientRequestHeaderID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClientRequestHeaderID").Value<string>()) : 0;
                var clientRequest = manageClientRequest.GetRequestByID(clientRequestHeaderID);
                return Json(new
                {
                    status = true,
                    data = clientRequest
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }



        [HttpPost()]
        [ActionName("getClientHistoryRequestByID")]
        public IHttpActionResult getClientHistoryRequestByID([FromBody]JObject data)
        {
            try
            {
                int clientRequestHeaderID = !string.IsNullOrEmpty(data.SelectToken("ClientRequestHeaderID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClientRequestHeaderID").Value<string>()) : 0;
                int clientID = !string.IsNullOrEmpty(data.SelectToken("clientID").Value<string>()) ? Convert.ToInt32(data.SelectToken("clientID").Value<string>()) : 0;
                string   Year = !string.IsNullOrEmpty(data.SelectToken("Year").Value<string>()) ?data.SelectToken("Year").Value<string>() :"0";
                string FrequncyID = !string.IsNullOrEmpty(data.SelectToken("FrequncyID").Value<string>()) ? data.SelectToken("FrequncyID").Value<string>() : "0";
                string FrequncyDID = !string.IsNullOrEmpty(data.SelectToken("FrequncyDID").Value<string>()) ? data.SelectToken("FrequncyDID").Value<string>() : "0";
                var clientRequest = manageClientRequest.GetRequestByID(clientRequestHeaderID, clientID, Year, FrequncyID, FrequncyDID);
                return Json(new
                {
                    status = true,
                    data = clientRequest
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }







        [HttpPost()]
        [ActionName("GetPilotRequestByID")]
        public IHttpActionResult GetPilotRequestByID([FromBody]JObject data)
        {
            try
            {
                int clientRequestHeaderID = !string.IsNullOrEmpty(data.SelectToken("ClientRequestHeaderID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClientRequestHeaderID").Value<string>()) : 0;
                var clientRequest = manageClientRequest.GetPilotRequestByID(clientRequestHeaderID);
                return Json(new
                {
                    status = true,
                    data = clientRequest
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }



        [HttpPost()]
        [ActionName("GetAllFamilyDiscount")]
        public IHttpActionResult GetAllFamilyDiscount()
        {
            try
            {
                var FamilyDiscount = manageClientRequest.GetAllFamilyDiscount();
                return Json(new
                {
                    status = true,
                    data = FamilyDiscount
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }



        [HttpPost()]
        [ActionName("GetAllBanksByBusinessUnitID")]
        public IHttpActionResult GetAllBanksByBusinessUnitID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var designationList = manageClientRequest.GetAllBanksByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = designationList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllDeductionMethods")]
        public IHttpActionResult GetAllDeductionMethods([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var DeductionList = manageClientRequest.GetAllDeductionMethods(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = DeductionList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllPaymentMethods")]
        public IHttpActionResult GetDeductionMethodsByID([FromBody]JObject data)
        {
            try
            {
                int id = !string.IsNullOrEmpty(data.SelectToken("id").Value<string>()) ? Convert.ToInt32(data.SelectToken("id").Value<string>()) : 0;
                var designationList = manageClientRequest.GetPaymentMethodsid(id);
                return Json(new
                {
                    status = true,
                    data = designationList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }


        [HttpPost()]
        [ActionName("GetPaymentMethods")]
        public IHttpActionResult GetPaymentMethods([FromBody]JObject data)
        {
            try
            {
                int id = !string.IsNullOrEmpty(data.SelectToken("id").Value<string>()) ? Convert.ToInt32(data.SelectToken("id").Value<string>()) : 0;
                var designationList = manageClientRequest.GetPaymentMethods(id);
                return Json(new
                {
                    status = true,
                    data = designationList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }





        [HttpPost()]
        [ActionName("GetAllPaymentMethodsNew")]
        public IHttpActionResult GetAllPaymentMethodsNew([FromBody]JObject data)
        {
            try
            {
              //  int id = !string.IsNullOrEmpty(data.SelectToken("id").Value<string>()) ? Convert.ToInt32(data.SelectToken("id").Value<string>()) : 0;
                var designationList = manageClientRequest.GetAllPaymentMethods();
                return Json(new
                {
                    status = true,
                    data = designationList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }




        [HttpPost()]
        [ActionName("GetBankByID")]
        public IHttpActionResult GetBankByID([FromBody]JObject data)
        {
            try
            {
                int bankID = !string.IsNullOrEmpty(data.SelectToken("BankID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BankID").Value<string>()) : 0;
                var bank = manageClientRequest.GetBankByID(bankID);
                return Json(new
                {
                    status = true,
                    data = bank
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("SavePayment")]
        public IHttpActionResult SavePayment([FromBody]JObject data)
        {
            try
            {
                //bool isClientExist = !string.IsNullOrEmpty(data.SelectToken("IsClientExist").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientExist").Value<string>()) : false;
                //int clientID = !string.IsNullOrEmpty(data.SelectToken("ClientID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClientID").Value<string>()) : 0;
               // bool isClientUpdated = !string.IsNullOrEmpty(data.SelectToken("IsClientUpdated").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientUpdated").Value<string>()) : false;
               // bool isClientAdded = !string.IsNullOrEmpty(data.SelectToken("IsClientAdded").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientAdded").Value<string>()) : false;
               // ClientVM clientObj = data.SelectToken("ClientObj").ToObject<ClientVM>();
                ClientRequestHeaderVM clientRequestHeaderObj = data.SelectToken("ClientRequestHeaderObj").ToObject<ClientRequestHeaderVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                //ClientRequestVM clientRequestVM = new ClientRequestVM();
                ////clientRequestVM.IsClientExist = isClientExist;
                ////clientRequestVM.ClientID = clientID;
                //clientRequestVM.IsClientUpdated = isClientUpdated;
                //clientRequestVM.IsClientAdded = isClientAdded;
                //clientRequestVM.ClientDetails = clientObj;
                //clientRequestVM.ClientRequestHeaderDetails = clientRequestHeaderObj;
                //clientRequestVM.UserID = userID;

                bool status = manageClientRequest.SavePayment(clientRequestHeaderObj, userID);

                if (status)
                {
                   // AddInvoicetoERP(clientRequestHeaderObj);
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
        [ActionName("UpdatePayment")]
        public IHttpActionResult UpdatePayment([FromBody]JObject data)
        {
            try
            {
                //bool isClientExist = !string.IsNullOrEmpty(data.SelectToken("IsClientExist").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientExist").Value<string>()) : false;
                //int clientID = !string.IsNullOrEmpty(data.SelectToken("ClientID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClientID").Value<string>()) : 0;
                // bool isClientUpdated = !string.IsNullOrEmpty(data.SelectToken("IsClientUpdated").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientUpdated").Value<string>()) : false;
                // bool isClientAdded = !string.IsNullOrEmpty(data.SelectToken("IsClientAdded").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientAdded").Value<string>()) : false;
                // ClientVM clientObj = data.SelectToken("ClientObj").ToObject<ClientVM>();
                ClientRequestHeaderVM clientRequestHeaderObj = data.SelectToken("ClientRequestHeaderObj").ToObject<ClientRequestHeaderVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                //ClientRequestVM clientRequestVM = new ClientRequestVM();
                ////clientRequestVM.IsClientExist = isClientExist;
                ////clientRequestVM.ClientID = clientID;
                //clientRequestVM.IsClientUpdated = isClientUpdated;
                //clientRequestVM.IsClientAdded = isClientAdded;
                //clientRequestVM.ClientDetails = clientObj;
                //clientRequestVM.ClientRequestHeaderDetails = clientRequestHeaderObj;
                //clientRequestVM.UserID = userID;

                bool status = manageClientRequest.UpdatePayment(clientRequestHeaderObj, userID);

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
        [ActionName("SaveBankTransaction")]
        public IHttpActionResult SaveBankTransaction([FromBody]JObject data)
        {
            try
            {
                
                BankTransactionVM baveBankTransaction = data.SelectToken("BankObj").ToObject<BankTransactionVM>();
              

                bool status = manageClientRequest.SaveBankTransaction(baveBankTransaction);

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
        [ActionName("SaveBUPABankTransaction")]
        public IHttpActionResult SaveBUPABankTransaction([FromBody]JObject data)
        {
            try
            {
               
                BankTransactionVM baveBankTransaction = data.SelectToken("BankObj").ToObject<BankTransactionVM>();
               

                bool status = manageClientRequest.SaveBUPABankTransaction(baveBankTransaction);

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
        [ActionName("GetBankDetailsByPolicyID")]
        public IHttpActionResult GetBankDetailsByPolicyID([FromBody]JObject data)
        {
            try
            {
                int policyID = !string.IsNullOrEmpty(data.SelectToken("PolicyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BankID").Value<string>()) : 0;
                var policy = manageClientRequest.GetBankDetailsByPolicyID(policyID);
                return Json(new
                {
                    status = true,
                    data = policy
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        public int AddInvoicetoERP(ClientRequestHeaderVM clientRequestHeaderObj)
        {
            // GET PURCHASE HEADER DATA -- SQL CONNECTION NOT WORKING AFTER SAP CONNECTION SO PLACED THE CONNECTION AND LOCAL DB CONNECTION BEFOR SAP CONNECTION
            DalLogUserEvents _oDalLogUser = new DalLogUserEvents();
            LogUserEvents _oLogUser = new LogUserEvents();
            int tblID = Convert.ToInt32(ConfigurationSettings.AppSettings["PURCHASEORDERMASTERTBLID"].ToString());
            _oLogUser.TableID = tblID.ToString();
            _oLogUser.DocumentID = "SAP-PO Transfer Start";
            _oLogUser.CreateDate = DateTime.Now;
            _oLogUser.UpdateDate = DateTime.Now;
            _oLogUser.CreateUser = 1;
            _oLogUser.UpdateUser = 1;




            var AgentAcoount = manageClientRequest.getAgentAccount(clientRequestHeaderObj.AgentID.ToString());
            var BUDAcoount = manageClientRequest.getBUDAccount(clientRequestHeaderObj.BusinessUnitID.ToString());
            var ErpDB = manageClientRequest.getERPDB(clientRequestHeaderObj.BusinessUnitID.ToString());



            try
            {

                //DataSet ds = new DataSet();
                //PurchaseOrderLayer oPurchaseLayer = new PurchaseOrderLayer();
                //ds = oPurchaseLayer.GetPOInterDB(1, "");

                // if (ds.Tables[0].Rows.Count > 0)
                //   MessageBox.Show(" Header data");


                string Type = "";
                int Count = 0;
                //PurchaseOrderLayer orderservice = new PurchaseOrderLayer();
                int lRetCode = -1;
                SAPObjects.oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2014;
                //SAPObjects.oCompany.Server = @"" + _objPropertyCon.Data_Source;
                //SAPObjects.oCompany.UserName = _objPropertyCon.User_ID;
                //SAPObjects.oCompany.Password = _objPropertyCon.Password;
                SAPObjects.oCompany.language = SAPbobsCOM.BoSuppLangs.ln_English;
                SAPObjects.oCompany.UseTrusted = false;
                 SAPObjects.oCompany.CompanyDB = ErpDB;
                SAPObjects.oCompany.DbUserName = ConfigurationSettings.AppSettings["ERPSERVERDB_USER"].ToString();
                SAPObjects.oCompany.DbPassword = ConfigurationSettings.AppSettings["ERPSERVERDB_PASS"].ToString();
                SAPObjects.oCompany.LicenseServer = ConfigurationSettings.AppSettings["ERPSERVERDB_LICENSE"].ToString();








                //OrderApp.oCompany.DbPassword = "Admin1234";
                //OrderApp.oCompany.LicenseServer = "Win8";

                //   MessageBox.Show(ConfigurationSettings.AppSettings["ERPSERVERDB_LICENSE"].ToString());

                lRetCode = SAPObjects.oCompany.Connect();
                string str = SAPObjects.oCompany.GetLastErrorDescription();

                // MessageBox.Show(str);


                if (lRetCode != 0)
                {
                    //Error("Can't Connect to ERP  " + str);

                    _oLogUser.Messages = "FAILED PO TRANSFER ON CONNECT - " + str;
                  //  _oDalLogUser.SaveTransactionLog(_oLogUser);

                    return lRetCode;

                    // MessageBox.Show(" FAILED PO TRANSFER ON CONNECT");
                }
                else
                {
                    SAPObjects.oCompany.StartTransaction();


                    //START SQL TRANSACTION 
                    int AllTransfered = 1;

                    //Read PURCHASE Header Data

                    //  MessageBox.Show(" Susscced PO TRANSFER ON CONNECT");
                    foreach (var debitNoteVM in clientRequestHeaderObj.DebitNoteDetails)
                    {
                        SAPObjects.oPurchaseOrder = (SAPbobsCOM.Documents)SAPObjects.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);//define sap business object type

                        SAPObjects.oPurchaseOrder.CardCode = clientRequestHeaderObj.ClientID.ToString();
                        SAPObjects.oPurchaseOrder.CardName = clientRequestHeaderObj.ClientName.ToString();
                        SAPObjects.oPurchaseOrder.BPChannelCode = debitNoteVM.DebitNoteID.ToString();
                        //SAPObjects.oPurchaseOrder.NumAtCard = row["POID"].ToString();
                        //SAPObjects.oPurchaseOrder.DocDate = Convert.ToDateTime(row["PODateTime"].ToString());
                        //SAPObjects.oPurchaseOrder.TaxDate = Convert.ToDateTime(row["TaxDate"].ToString());
                        //SAPObjects.oPurchaseOrder.DocDueDate = Convert.ToDateTime(row["DueDate"].ToString());
                        SAPObjects.oPurchaseOrder.Reference1 = "SFA";
                        SAPObjects.oPurchaseOrder.Comments = "SFA";
                        //  MessageBox.Show(" cus data" + row["DistributorID"].ToString());

                        //LOG TRANSACTION
                        DalLogUserEvents _oDalLogUserPO = new DalLogUserEvents();
                        LogUserEvents _oLogUserPO = new LogUserEvents();

                        _oLogUserPO.TableID = tblID.ToString();
                        //_oLogUserPO.DocumentID = row["POID"].ToString();
                        _oLogUserPO.CreateDate = DateTime.Now;
                        _oLogUserPO.UpdateDate = DateTime.Now;
                        _oLogUserPO.CreateUser = 1;
                        _oLogUserPO.UpdateUser = 1;


                        //GET  PURCHASE DETAILS LINES


                        //DataSet dsl = new DataSet();
                        //dsl = oPurchaseLayer.GetPOInterDB(2, row["POID"].ToString());
                        //if (dsl.Tables[0].Rows.Count > 0)
                        //    //     MessageBox.Show(" details ");
                        //    foreach (DataRow rowD in dsl.Tables[0].Rows)
                        //    {
                        //        double price = 0.00;
                        //        double qty = 0;

                        //        SAPObjects.oPurchaseOrder.Lines.ItemCode = rowD["ItemCode"].ToString();
                        //        // MessageBox.Show(rowD["ItemCode"].ToString());
                        //        DataSet _ds = new ProductMasterLayer().GetProductWareHouse(rowD["ItemCode"].ToString());

                        //        string wareHouse = _ds.Tables[0].Rows[0]["DfltWH"].ToString();
                        //        //  MessageBox.Show(wareHouse);
                        //        if (string.IsNullOrEmpty(wareHouse))
                        //        {
                        //            //MessageBox.Show(rowD["ItemCode"].ToString() + "don't have warehouse");
                        //            return 0;
                        //        }


                        //        SAPObjects.oPurchaseOrder.Lines.ItemDescription = rowD["ItemDescription"].ToString();

                        //        price = double.Parse(rowD["Price"].ToString());
                        //        qty = double.Parse(rowD["Qty"].ToString());

                        //        SAPObjects.oPurchaseOrder.Lines.Quantity = Convert.ToDouble(rowD["Qty"].ToString());
                        //        SAPObjects.oPurchaseOrder.Lines.Price = double.Parse(rowD["Price"].ToString());

                        //        //  SAPObjects.oPurchaseOrder.Lines.WarehouseCode = rowD["WarehouseCode"].ToString();
                        //        SAPObjects.oPurchaseOrder.Lines.WarehouseCode = wareHouse;
                        //        //SAPObjects.oPurchaseOrder.Lines.DiscountPercent = Convert.ToDouble(rowD["LineDiscount"].ToString());

                        //        SAPObjects.oPurchaseOrder.Lines.DiscountPercent = 0.00;
                        //        SAPObjects.oPurchaseOrder.Lines.TaxCode = rowD["TaxCode"].ToString() == "" ? "EXEMPT" : rowD["TaxCode"].ToString();
                        //        SAPObjects.oPurchaseOrder.Lines.LineTotal = price * qty;
                        //        SAPObjects.oPurchaseOrder.Lines.Add();
                        //    }

                        //MessageBox.Show(" Line ok ");
                        lRetCode = SAPObjects.oPurchaseOrder.Add();
                        // MessageBox.Show(lRetCode.ToString());
                        string strError = SAPObjects.oCompany.GetLastErrorDescription();
                        // MessageBox.Show(strError);

                        if (lRetCode != 0)
                        {
                            _oLogUserPO.Messages = "PO TRANSFER FAIL - " + lRetCode.ToString() + " - " + strError;
                            _oDalLogUserPO.SaveTransactionLog(_oLogUserPO);

                            //ADD EVENT LOG TO TABLE
                            //var _POERPMsg = new PurchaseOrderERPMsg();

                            //_POERPMsg.POID = row["POID"].ToString();
                            //_POERPMsg.SAPReference = lRetCode.ToString();
                            //_POERPMsg.ErrorMsg = strError;
                            //_POERPMsg.CreateDate = DateTime.Now;
                            ////_POERPMsg.UpdateDate="";
                            //_POERPMsg.CreateUser = 1;//change to dynamic value is a must
                            //_POERPMsg.UpdateUser=;

                            //PurchaseOrderLayer orderservice = new PurchaseOrderLayer();
                            //int lRetLog = orderservice.SaveERPPOTransactionLog(_POERPMsg, 1, tblID);//1 = INSERT OF A NEW RECORD


                            int lErrCode = 0;
                            string sErrMsg = "";

                            int temp_int = lErrCode;
                            string temp_string = sErrMsg;
                            SAPObjects.oCompany.GetLastError(out temp_int, out temp_string);
                            if (lErrCode != -4006)
                            {
                                SAPObjects.oCompany.Disconnect();
                            }
                            //NOT ALL POs going to save user must check error log
                            AllTransfered = 0;
                        }
                        else
                        {
                            _oLogUserPO.Messages = "PO TRANSFER OK";
                            _oDalLogUserPO.SaveTransactionLog(_oLogUserPO);

                            //PurchaseOrderLayer oOrderService = new PurchaseOrderLayer();
                            //POHeader OH = new POHeader();
                            //OH.IsSendtoERP = Convert.ToInt32(TransactionStatusEnum.Proceed);
                            //OH.POID = row["POID"].ToString();
                            //int sDbStatus = oOrderService.UpdatePOHeader(OH);

                        }

                    }
                    SAPObjects.oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                    SAPObjects.oCompany.Disconnect();
                    return AllTransfered;
                }
            }
            catch (Exception ex)
            {

                _oLogUser.Messages = "PO TRANSFER FAILED - " + ex.Message.ToString() + " - " + ex.ToString();
                _oDalLogUser.SaveTransactionLog(_oLogUser);


                return 0;
            }
        }

        //Pilot/Nestle
        [HttpPost()]
        [ActionName("SaveCustomerRequest")]
        public IHttpActionResult SaveCustomerRequest([FromBody]JObject data)
        {
            try
            {
                //bool isClientExist = !string.IsNullOrEmpty(data.SelectToken("IsClientExist").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientExist").Value<string>()) : false;
                //int clientID = !string.IsNullOrEmpty(data.SelectToken("ClientID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClientID").Value<string>()) : 0;
                bool isClientUpdated = !string.IsNullOrEmpty(data.SelectToken("IsClientUpdated").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientUpdated").Value<string>()) : false;
                bool isClientAdded = !string.IsNullOrEmpty(data.SelectToken("IsClientAdded").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientAdded").Value<string>()) : false;
                ClientVM clientObj = data.SelectToken("ClientObj").ToObject<ClientVM>();
                ClientRequestHeaderVM clientRequestHeaderObj = data.SelectToken("ClientRequestHeaderObj").ToObject<ClientRequestHeaderVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                ClientRequestVM clientRequestVM = new ClientRequestVM();
                //clientRequestVM.IsClientExist = isClientExist;
                //clientRequestVM.ClientID = clientID;
                clientRequestVM.IsClientUpdated = isClientUpdated;
                clientRequestVM.IsClientAdded = isClientAdded;
                clientRequestVM.ClientDetails = clientObj;
                clientRequestVM.ClientRequestHeaderDetails = clientRequestHeaderObj;
                clientRequestVM.UserID = userID;

                bool status = manageClientRequest.SaveCustomerRequest(clientRequestVM);

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
        [ActionName("SavePilotCustomerRequest")]
        public IHttpActionResult SavePilotCustomerRequest([FromBody]JObject data)
        {
            try
            {
                //bool isClientExist = !string.IsNullOrEmpty(data.SelectToken("IsClientExist").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientExist").Value<string>()) : false;
                //int clientID = !string.IsNullOrEmpty(data.SelectToken("ClientID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ClientID").Value<string>()) : 0;
                bool isClientUpdated = !string.IsNullOrEmpty(data.SelectToken("IsClientUpdated").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientUpdated").Value<string>()) : false;
                bool isClientAdded = !string.IsNullOrEmpty(data.SelectToken("IsClientAdded").Value<string>()) ? Convert.ToBoolean(data.SelectToken("IsClientAdded").Value<string>()) : false;
                ClientVM clientObj = data.SelectToken("ClientObj").ToObject<ClientVM>();
                ClientRequestHeaderVM clientRequestHeaderObj = data.SelectToken("ClientRequestHeaderObj").ToObject<ClientRequestHeaderVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                ClientRequestVM clientRequestVM = new ClientRequestVM();
                //clientRequestVM.IsClientExist = isClientExist;
                //clientRequestVM.ClientID = clientID;
                clientRequestVM.IsClientUpdated = isClientUpdated;
                clientRequestVM.IsClientAdded = isClientAdded;
                clientRequestVM.ClientDetails = clientObj;
                clientRequestVM.ClientRequestHeaderDetails = clientRequestHeaderObj;
                clientRequestVM.UserID = userID;

                bool status = manageClientRequest.SaveCustomerRequest(clientRequestVM);

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
        [ActionName("GetAllBupaClients")]
        public IHttpActionResult GetAllBupaClients([FromBody]JObject data)
        {
            try
            {
                var clientList = manageClientRequest.GetAllBupaClients();

                return Json(new
                {
                    status = true,
                    data = clientList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetNewClientList")]
        public IHttpActionResult GetNewClientList([FromBody]JObject data)
        {
            try
            {
                var filterMapper = data.SelectToken("filterObj").ToObject<FilterMapper>();
                var clientList = manageClientRequest.GetNewClientList(filterMapper);

                return Json(new
                {
                    status = true,
                    data = clientList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        [HttpPost()]
        [ActionName("ChangeActiveStatus")]
        public IHttpActionResult ChangeActiveStatus([FromBody]JObject data)
        {
            try
            {
                var filterMapper = data.SelectToken("filterObj").ToObject<ChangeActiveStatusMapper>();
                bool status = manageClientRequest.ChangeActiveStatus(filterMapper);

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
        [ActionName("GetCancelledRequest")]
        public IHttpActionResult GetCancelledRequest([FromBody]JObject data)
        {
            try
            {
                var filterMapper = data.SelectToken("filterObj").ToObject<FilterMapper>();
                var result = manageClientRequest.GetCancelledRequest(filterMapper);

                return Json(new
                {
                    status = true,
                    data = result
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

    }
}
