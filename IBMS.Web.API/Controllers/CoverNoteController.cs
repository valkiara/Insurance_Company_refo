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

namespace IBMS.Web.API.Controllers
{
    [BasicAuthentication]
    public class CoverNoteController : ApiController
    {
        ManageCoverNote manageCoverNote = new ManageCoverNote();

        [HttpPost()]
        [ActionName("SaveCoverNote")]
        public IHttpActionResult SaveCoverNote([FromBody]JObject data)
        {
            try
            {
                CoverNoteVM coverNoteObj = data.SelectToken("CoverNoteObj").ToObject<CoverNoteVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                coverNoteObj.CreatedBy = userID;

                bool status = manageCoverNote.SaveCoverNote(coverNoteObj);

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
        [ActionName("UpdateCoverNote")]
        public IHttpActionResult UpdateCoverNote([FromBody]JObject data)
        {
            try
            {
                CoverNoteVM coverNoteObj = data.SelectToken("CoverNoteObj").ToObject<CoverNoteVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                coverNoteObj.ModifiedBy = userID;

                bool status = manageCoverNote.UpdateCoverNote(coverNoteObj);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully Updated" });
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
        [ActionName("GetAllCoverNotes")]
        public IHttpActionResult GetAllCoverNotes()
        {
            try
            {
                var coverNoteList = manageCoverNote.GetAllCoverNotes();
                return Json(new
                {
                    status = true,
                    data = coverNoteList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllCoverNotesByBUID")]
        public IHttpActionResult GetAllCoverNotesByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var coverNoteList = manageCoverNote.GetAllCoverNotesByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = coverNoteList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetCoverNoteByID")]
        public IHttpActionResult GetCoverNoteByID([FromBody]JObject data)
        {
            try
            {
                int coverNoteID = !string.IsNullOrEmpty(data.SelectToken("CoverNoteID").Value<string>()) ? Convert.ToInt32(data.SelectToken("CoverNoteID").Value<string>()) : 0;
                var coverNote = manageCoverNote.GetCoverNoteByID(coverNoteID);
                return Json(new
                {
                    status = true,
                    data = coverNote
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetCoverNoteByQuatationID")]
        public IHttpActionResult GetCoverNoteByQuatationID([FromBody]JObject data)
        {
            try
            {
                int quotationID = !string.IsNullOrEmpty(data.SelectToken("QuotationID").Value<string>()) ? Convert.ToInt32(data.SelectToken("QuotationID").Value<string>()) : 0;
                var coverNote = manageCoverNote.GetCoverByQuotationID(quotationID);
                return Json(new
                {
                    status = true,
                    data = coverNote
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
    }
}
