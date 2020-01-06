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
    public class UtilityController : ApiController
    {
        ManageUtility manageUtility = new ManageUtility();


        [HttpPost()]
        [ActionName("GetAllPilotPremium")]
        public IHttpActionResult GetAllPilotPremium()
        {
            try
            {
                var PilotPremium = manageUtility.getAllPilotPremium();
                return Json(new
                {
                    status = true,
                    data = PilotPremium
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }





        [HttpPost()]
        [ActionName("GetAllPAgeWisePremium")]
        public IHttpActionResult GetAllAgeWisePremium()
        {
            try
            {
                var AgeWisePremium = manageUtility.getAllAgeWisePremium();
                return Json(new
                {
                    status = true,
                    data = AgeWisePremium
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }



        [HttpPost()]
        [ActionName("GetAllPremiumHolderType")]
        public IHttpActionResult GetAllPremiumHolderType()
        {
            try
            {
                var PremiumHolderType = manageUtility.getAllPremiumHolderType();
                return Json(new
                {
                    status = true,
                    data = PremiumHolderType
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }


        [HttpPost()]
        [ActionName("GetAllDeductionType")]
        public IHttpActionResult GetAllDeductionType([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("businessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("businessUnitID").Value<string>()) : 0;
                var DeductionType = manageUtility.getAllDeductionType(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = DeductionType
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }









        [HttpPost()]
        [ActionName("GetAllRelationship")]
        public IHttpActionResult GetAllRelationship()
        {
            try
            {
                var relation = manageUtility.getAllRelatioship();
                return Json(new
                {
                    status = true,
                    data = relation
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllGenders")]
        public IHttpActionResult GetAllGenders()
        {
            try
            {
                var genderList = manageUtility.getAllGender();
                return Json(new
                {
                    status = true,
                    data = genderList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        [HttpPost()]
        [ActionName("GetPilotPremium")]
        public IHttpActionResult GetPilotPremium()
        {
            try
            {
                var genderList = manageUtility.getAllPilotPremum();
                return Json(new
                {
                    status = true,
                    data = genderList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        [HttpPost()]

        public IHttpActionResult getStatus()
        {
            try
            {
                var genderList = manageUtility.getStatus();
                return Json(new
                {
                    status = true,
                    data = genderList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }





        [HttpPost()]
        [ActionName("GetAllHospitals")]
        public IHttpActionResult GetAllHospitals()
        {
            try
            {
                var HospitalsList = manageUtility.getAllHospitals();
                return Json(new
                {
                    status = true,
                    data = HospitalsList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllTitles")]
        public IHttpActionResult GetAllTitles()
        {
            try
            {
                var TitleList = manageUtility.getAllTitles();
                return Json(new
                {
                    status = true,
                    data = TitleList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllCountries")]
        public IHttpActionResult GetAllCountries()
        {
            try
            {
                var countryList = manageUtility.GetAllCountries();
                return Json(new
                {
                    status = true,
                    data = countryList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        
        [HttpPost()]
        [ActionName("getAllDistrict")]
        public IHttpActionResult getAllDistrict()
        {
            try
            {
                var DistrictList = manageUtility.getAllDistrict();
                return Json(new
                {
                    status = true,
                    data = DistrictList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }



        [HttpPost()]
        [ActionName("GetCountryByID")]
        public IHttpActionResult GetCountryByID([FromBody]JObject data)
        {
            try
            {
                int countryID = !string.IsNullOrEmpty(data.SelectToken("countryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("countryID").Value<string>()) : 0;
                var country = manageUtility.GetCountryByID(countryID);

                return Json(new
                {
                    status = true,
                    data = country
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllCurrencies")]
        public IHttpActionResult GetAllCurrencies()
        {
            try
            {
                var currencyList = manageUtility.GetAllCurrencies();
                return Json(new
                {
                    status = true,
                    data = currencyList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetCurrencyByID")]
        public IHttpActionResult GetCurrencyByID([FromBody]JObject data)
        {
            try
            {
                int currencyID = !string.IsNullOrEmpty(data.SelectToken("currencyID").Value<string>()) ? Convert.ToInt32(data.SelectToken("currencyID").Value<string>()) : 0;
                var currency = manageUtility.GetCurrencyByID(currencyID);

                return Json(new
                {
                    status = true,
                    data = currency
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetCurrencyByCountryID")]
        public IHttpActionResult GetCurrencyByCountryID([FromBody]JObject data)
        {
            try
            {
                int countryID = !string.IsNullOrEmpty(data.SelectToken("countryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("countryID").Value<string>()) : 0;
                var currency = manageUtility.GetCurrencyByCountryID(countryID);

                return Json(new
                {
                    status = true,
                    data = currency
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
