using IBMS.Service.MasterData;
using IBMS.Shared.ViewModel;
using IBMS.Web.API.Security;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace IBMS.Web.API.Controllers
{
    [BasicAuthentication]
    public class DocumentController : ApiController
    {
        ManageDocument manageDocument = new ManageDocument();

        #region Document
        [HttpPost()]
        [ActionName("SaveDocument")]
        public IHttpActionResult SaveDocument()
        {
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    int insSubClassID = Convert.ToInt32(HttpContext.Current.Request.Form["insSubClassID"]);
                    string documentName = HttpContext.Current.Request.Form["documentName"] + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    HttpPostedFile uploadedDocument = HttpContext.Current.Request.Files[0];
                    string description = HttpContext.Current.Request.Form["description"];
                    int userID = Convert.ToInt32(HttpContext.Current.Request.Form["userID"]);

                    string newDocument = System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/Documents/") + documentName + Path.GetExtension(uploadedDocument.FileName);
                    string newDocumentURL = Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority + "/Uploads/Documents/" + documentName + Path.GetExtension(uploadedDocument.FileName);

                    DocumentVM documentVM = new DocumentVM();
                    documentVM.InsuranceSubClassID = insSubClassID;
                    documentVM.DocumentPath = newDocumentURL;
                    documentVM.Description = description;
                    documentVM.CreatedBy = userID;

                    bool status = manageDocument.SaveDocument(documentVM);

                    if (status)
                    {
                        //Save file in the directory
                        uploadedDocument.SaveAs(newDocument);

                        return Json(new { status = true, message = "Successfully Saved", data = newDocumentURL });
                    }
                    else
                    {
                        return Json(new { status = false, message = "Save Failed" });
                    }
                }
                else
                {
                    return Json(new { status = false, message = "Please Upload the Document" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateDocument")]
        public IHttpActionResult UpdateDocument()
        {
            try
            {
                int documentID = Convert.ToInt32(HttpContext.Current.Request.Form["documentID"]);
                int insSubClassID = Convert.ToInt32(HttpContext.Current.Request.Form["insSubClassID"]);
                string description = HttpContext.Current.Request.Form["description"];
                int userID = Convert.ToInt32(HttpContext.Current.Request.Form["userID"]);

                DocumentVM existingDocument = manageDocument.GetDocumentByID(documentID);

                string documentName = string.Empty;
                HttpPostedFile uploadedDocument = null;
                string newDocument = string.Empty;
                string newDocumentURL = string.Empty;

                DocumentVM documentVM = new DocumentVM();
                documentVM.DocumentID = documentID;
                documentVM.InsuranceSubClassID = insSubClassID;
                documentVM.Description = description;
                documentVM.ModifiedBy = userID;

                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    documentName = HttpContext.Current.Request.Form["documentName"] + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    uploadedDocument = HttpContext.Current.Request.Files[0];

                    newDocument = System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/Documents/") + documentName + Path.GetExtension(uploadedDocument.FileName);
                    newDocumentURL = Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority + "/Uploads/Documents/" + documentName + Path.GetExtension(uploadedDocument.FileName);

                    documentVM.DocumentPath = newDocumentURL;

                    bool status = manageDocument.UpdateDocument(documentVM);

                    if (status)
                    {
                        //Save new file in the directory
                        uploadedDocument.SaveAs(newDocument);

                        //Delete existing file from the directory
                        string[] filePathItemArray = existingDocument.DocumentPath.Split('/');
                        string existingFileName = filePathItemArray[filePathItemArray.Length - 1];
                        File.Delete(System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/Documents/" + existingFileName));

                        return Json(new { status = true, message = "Successfully Updated", data = documentVM.DocumentPath });
                    }
                    else
                    {
                        return Json(new { status = false, message = "Update Failed" });
                    }
                }
                else
                {
                    documentVM.DocumentPath = existingDocument.DocumentPath;

                    bool status = manageDocument.UpdateDocument(documentVM);

                    if (status)
                    {
                        return Json(new { status = true, message = "Successfully Updated", data = documentVM.DocumentPath });
                    }
                    else
                    {
                        return Json(new { status = false, message = "Update Failed" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteDocument")]
        public IHttpActionResult DeleteDocument([FromBody]JObject data)
        {
            try
            {
                int documentID = !string.IsNullOrEmpty(data.SelectToken("documentID").Value<string>()) ? Convert.ToInt32(data.SelectToken("documentID").Value<string>()) : 0;
                DocumentVM existingDocument = manageDocument.GetDocumentByID(documentID);
                bool status = manageDocument.DeleteDocument(documentID);

                if (status)
                {
                    //Delete document from the directory
                    string[] filePathItemArray = existingDocument.DocumentPath.Split('/');
                    string existingFileName = filePathItemArray[filePathItemArray.Length - 1];
                    File.Delete(System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/Documents/" + existingFileName));

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
        [ActionName("GetAllDocuments")]
        public IHttpActionResult GetAllDocuments()
        {
            try
            {
                var documentList = manageDocument.GetAllDocuments();
                return Json(new
                {
                    status = true,
                    data = documentList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllDocumentsByBUID")]
        public IHttpActionResult GetAllDocumentsByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var documentList = manageDocument.GetAllDocumentsByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = documentList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetDocumentByID")]
        public IHttpActionResult GetDocumentByID([FromBody]JObject data)
        {
            try
            {
                int documentID = !string.IsNullOrEmpty(data.SelectToken("documentID").Value<string>()) ? Convert.ToInt32(data.SelectToken("documentID").Value<string>()) : 0;
                var document = manageDocument.GetDocumentByID(documentID);
                return Json(new
                {
                    status = true,
                    data = document
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        #endregion

        #region Document Category
        [HttpPost()]
        [ActionName("SaveDocumentCategory")]
        public IHttpActionResult SaveDocumentCategory([FromBody]JObject data)
        {
            try
            {
                string categoryName = !string.IsNullOrEmpty(data.SelectToken("CategoryName").Value<string>()) ? data.SelectToken("CategoryName").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageDocument.IsDocumentCategoryAvailable(null, categoryName))
                {
                    DocCategoryVM docCategoryVM = new DocCategoryVM();
                    docCategoryVM.CategoryName = categoryName;
                    docCategoryVM.CreatedBy = userID;

                    bool status = manageDocument.SaveDocumentCategory(docCategoryVM);

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
                    return Json(new { status = false, message = "Document Category Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateDocumentCategory")]
        public IHttpActionResult UpdateDocumentCategory([FromBody]JObject data)
        {
            try
            {
                int docCategoryID = !string.IsNullOrEmpty(data.SelectToken("DocCategoryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("DocCategoryID").Value<string>()) : 0;
                string categoryName = !string.IsNullOrEmpty(data.SelectToken("CategoryName").Value<string>()) ? data.SelectToken("CategoryName").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;


                if (!manageDocument.IsDocumentCategoryAvailable(docCategoryID, categoryName))
                {
                    DocCategoryVM docCategoryVM = new DocCategoryVM();
                    docCategoryVM.DocCategoryID = docCategoryID;
                    docCategoryVM.CategoryName = categoryName;
                    docCategoryVM.ModifiedBy = userID;

                    bool status = manageDocument.UpdateDocumentCategory(docCategoryVM);

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
                    return Json(new { status = false, message = "Document Category Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteDocumentCategory")]
        public IHttpActionResult DeleteDocumentCategory([FromBody]JObject data)
        {
            try
            {
                int docCategoryID = !string.IsNullOrEmpty(data.SelectToken("DocCategoryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("DocCategoryID").Value<string>()) : 0;
                bool status = manageDocument.DeleteDocumentCategory(docCategoryID);

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
        [ActionName("GetAllDocumentCategories")]
        public IHttpActionResult GetAllDocumentCategories()
        {
            try
            {
                var docCategoryList = manageDocument.GetAllDocumentCategories();
                return Json(new
                {
                    status = true,
                    data = docCategoryList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetDocumentCategoryByID")]
        public IHttpActionResult GetDocumentCategoryByID([FromBody]JObject data)
        {
            try
            {
                int docCategoryID = !string.IsNullOrEmpty(data.SelectToken("DocCategoryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("DocCategoryID").Value<string>()) : 0;
                var documentCategory = manageDocument.GetDocumentCategoryByID(docCategoryID);
                return Json(new
                {
                    status = true,
                    data = documentCategory
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }
        #endregion

        #region Required Document
        [HttpPost()]
        [ActionName("SaveRequiredDocument")]
        public IHttpActionResult SaveRequiredDocument([FromBody]JObject data)
        {
            try
            {
                int insClassID = !string.IsNullOrEmpty(data.SelectToken("insClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("insClassID").Value<string>()) : 0;
                int insSubClassID = !string.IsNullOrEmpty(data.SelectToken("insSubClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("insSubClassID").Value<string>()) : 0;
                int docCategoryID = !string.IsNullOrEmpty(data.SelectToken("DocCategoryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("DocCategoryID").Value<string>()) : 0;
                string documentName = !string.IsNullOrEmpty(data.SelectToken("DocumentName").Value<string>()) ? data.SelectToken("DocumentName").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageDocument.IsRequiredDocumentAvailable(null, documentName))
                {
                    RequiredDocumentVM requiredDocumentVM = new RequiredDocumentVM();
                    requiredDocumentVM.InsuranceClassID = insClassID;
                    requiredDocumentVM.InsuranceSubClassID = insSubClassID;
                    requiredDocumentVM.DocCategoryID = docCategoryID;
                    requiredDocumentVM.DocumentName = documentName;
                    requiredDocumentVM.CreatedBy = userID;

                    bool status = manageDocument.SaveRequiredDocument(requiredDocumentVM);

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
                    return Json(new { status = false, message = "Required Document Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdateRequiredDocument")]
        public IHttpActionResult UpdateRequiredDocument([FromBody]JObject data)
        {
            try
            {
                int requiredDocID = !string.IsNullOrEmpty(data.SelectToken("ReqDocID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ReqDocID").Value<string>()) : 0;
                int insClassID = !string.IsNullOrEmpty(data.SelectToken("insClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("insClassID").Value<string>()) : 0;
                int insSubClassID = !string.IsNullOrEmpty(data.SelectToken("insSubClassID").Value<string>()) ? Convert.ToInt32(data.SelectToken("insSubClassID").Value<string>()) : 0;
                int docCategoryID = !string.IsNullOrEmpty(data.SelectToken("DocCategoryID").Value<string>()) ? Convert.ToInt32(data.SelectToken("DocCategoryID").Value<string>()) : 0;
                string documentName = !string.IsNullOrEmpty(data.SelectToken("DocumentName").Value<string>()) ? data.SelectToken("DocumentName").Value<string>() : string.Empty;
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                if (!manageDocument.IsRequiredDocumentAvailable(requiredDocID, documentName))
                {
                    RequiredDocumentVM requiredDocumentVM = new RequiredDocumentVM();
                    requiredDocumentVM.RequiredDocID = requiredDocID;
                    requiredDocumentVM.InsuranceClassID = insClassID;
                    requiredDocumentVM.InsuranceSubClassID = insSubClassID;
                    requiredDocumentVM.DocCategoryID = docCategoryID;
                    requiredDocumentVM.DocumentName = documentName;
                    requiredDocumentVM.ModifiedBy = userID;

                    bool status = manageDocument.UpdateRequiredDocument(requiredDocumentVM);

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
                    return Json(new { status = false, message = "Required Document Name already exists" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("DeleteRequiredDocument")]
        public IHttpActionResult DeleteRequiredDocument([FromBody]JObject data)
        {
            try
            {
                int requiredDocID = !string.IsNullOrEmpty(data.SelectToken("ReqDocID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ReqDocID").Value<string>()) : 0;
                bool status = manageDocument.DeleteRequiredDocument(requiredDocID);

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
        [ActionName("GetAllRequiredDocuments")]
        public IHttpActionResult GetAllRequiredDocuments()
        {
            try
            {
                var requiredDocList = manageDocument.GetAllRequiredDocuments();
                return Json(new
                {
                    status = true,
                    data = requiredDocList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllRequiredDocumentsByBUID")]
        public IHttpActionResult GetAllRequiredDocumentsByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var requiredDocList = manageDocument.GetAllRequiredDocumentsByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = requiredDocList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetRequiredDocumentByID")]
        public IHttpActionResult GetRequiredDocumentByID([FromBody]JObject data)
        {
            try
            {
                int requiredDocID = !string.IsNullOrEmpty(data.SelectToken("ReqDocID").Value<string>()) ? Convert.ToInt32(data.SelectToken("ReqDocID").Value<string>()) : 0;
                var requiredDocument = manageDocument.GetRequiredDocumentByID(requiredDocID);
                return Json(new
                {
                    status = true,
                    data = requiredDocument
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
