using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManageDocument
    {
        private UnitOfWork unitOfWork;
        public ManageDocument()
        {
            unitOfWork = new UnitOfWork();
        }

        #region Document
        public bool SaveDocument(DocumentVM documentVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblDocument document = new tblDocument();
                    document.InsSubClassID = documentVM.InsuranceSubClassID;
                    document.DocumentPath = documentVM.DocumentPath;
                    document.Description = documentVM.Description;
                    document.CreatedDate = DateTime.Now;
                    document.CreatedBy = documentVM.CreatedBy;
                    unitOfWork.TblDocumentRepository.Insert(document);
                    unitOfWork.Save();

                    //Complete the Transaction
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }

        public DocumentVM GetDocumentByID(int documentID)
        {
            try
            {
                var documentData = unitOfWork.TblDocumentRepository.GetByID(documentID);

                DocumentVM documentVM = new DocumentVM();
                documentVM.DocumentID = documentData.DocumentID;
                documentVM.InsuranceSubClassID = documentData.InsSubClassID != null ? Convert.ToInt32(documentData.InsSubClassID) : 0;

                if (documentVM.InsuranceSubClassID > 0)
                {
                    documentVM.InsuranceSubClassName = documentData.tblInsSubClass.Description;
                }

                documentVM.DocumentName = GetDocumentName(documentData.DocumentPath);
                documentVM.DocumentPath = documentData.DocumentPath;
                documentVM.Description = documentData.Description;
                documentVM.CreatedDate = documentData.CreatedDate != null ? documentData.CreatedDate.ToString() : string.Empty;
                documentVM.ModifiedDate = documentData.ModifiedDate != null ? documentData.ModifiedDate.ToString() : string.Empty;
                documentVM.CreatedBy = documentData.CreatedBy != null ? Convert.ToInt32(documentData.CreatedBy) : 0;
                documentVM.ModifiedBy = documentData.ModifiedBy != null ? Convert.ToInt32(documentData.ModifiedBy) : 0;

                return documentVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool UpdateDocument(DocumentVM documentVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblDocument document = unitOfWork.TblDocumentRepository.GetByID(documentVM.DocumentID);
                    document.InsSubClassID = documentVM.InsuranceSubClassID;
                    document.DocumentPath = documentVM.DocumentPath;
                    document.Description = documentVM.Description;
                    document.ModifiedDate = DateTime.Now;
                    document.ModifiedBy = documentVM.ModifiedBy;
                    unitOfWork.TblDocumentRepository.Update(document);
                    unitOfWork.Save();

                    //Complete the Transaction
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }

        public bool DeleteDocument(int documentID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblDocument document = unitOfWork.TblDocumentRepository.GetByID(documentID);
                    unitOfWork.TblDocumentRepository.Delete(document);
                    unitOfWork.Save();

                    //Complete the Transaction
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }

        public List<DocumentVM> GetAllDocuments()
        {
            try
            {
                var documentData = unitOfWork.TblDocumentRepository.Get().ToList();

                List<DocumentVM> documentList = new List<DocumentVM>();

                foreach (var document in documentData)
                {
                    DocumentVM documentVM = new DocumentVM();
                    documentVM.DocumentID = document.DocumentID;
                    documentVM.InsuranceSubClassID = document.InsSubClassID != null ? Convert.ToInt32(document.InsSubClassID) : 0;

                    if (documentVM.InsuranceSubClassID > 0)
                    {
                        documentVM.InsuranceSubClassName = document.tblInsSubClass.Description;
                    }

                    documentVM.DocumentName = GetDocumentName(document.DocumentPath);
                    documentVM.DocumentPath = document.DocumentPath;
                    documentVM.Description = document.Description;
                    documentVM.CreatedDate = document.CreatedDate != null ? document.CreatedDate.ToString() : string.Empty;
                    documentVM.ModifiedDate = document.ModifiedDate != null ? document.ModifiedDate.ToString() : string.Empty;
                    documentVM.CreatedBy = document.CreatedBy != null ? Convert.ToInt32(document.CreatedBy) : 0;
                    documentVM.ModifiedBy = document.ModifiedBy != null ? Convert.ToInt32(document.ModifiedBy) : 0;

                    documentList.Add(documentVM);
                }

                return documentList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<DocumentVM> GetAllDocumentsByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var documentData = unitOfWork.TblDocumentRepository.Get(x => x.tblInsSubClass.tblInsClass.BUID == businessUnitID).ToList();

                List<DocumentVM> documentList = new List<DocumentVM>();

                foreach (var document in documentData)
                {
                    DocumentVM documentVM = new DocumentVM();
                    documentVM.DocumentID = document.DocumentID;
                    documentVM.InsuranceSubClassID = document.InsSubClassID != null ? Convert.ToInt32(document.InsSubClassID) : 0;

                    if (documentVM.InsuranceSubClassID > 0)
                    {
                        documentVM.InsuranceSubClassName = document.tblInsSubClass.Description;
                    }

                    documentVM.DocumentName = GetDocumentName(document.DocumentPath);
                    documentVM.DocumentPath = document.DocumentPath;
                    documentVM.Description = document.Description;
                    documentVM.CreatedDate = document.CreatedDate != null ? document.CreatedDate.ToString() : string.Empty;
                    documentVM.ModifiedDate = document.ModifiedDate != null ? document.ModifiedDate.ToString() : string.Empty;
                    documentVM.CreatedBy = document.CreatedBy != null ? Convert.ToInt32(document.CreatedBy) : 0;
                    documentVM.ModifiedBy = document.ModifiedBy != null ? Convert.ToInt32(document.ModifiedBy) : 0;

                    documentList.Add(documentVM);
                }

                return documentList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GetDocumentName(string docURL)
        {
            try
            {
                string[] docURLArray = docURL.Split('/');
                string[] docURLLastItemArray = docURLArray[docURLArray.Length - 1].Split('_');

                return docURLLastItemArray[0];
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Document Category
        public bool SaveDocumentCategory(DocCategoryVM docCategoryVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblDocCategory docCategory = new tblDocCategory();
                    docCategory.CategoryName = docCategoryVM.CategoryName;
                    docCategory.CreatedDate = DateTime.Now;
                    docCategory.CreatedBy = docCategoryVM.CreatedBy;
                    unitOfWork.TblDocCategoryRepository.Insert(docCategory);
                    unitOfWork.Save();

                    //Complete the Transaction
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }

        public bool UpdateDocumentCategory(DocCategoryVM docCategoryVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblDocCategory docCategory = unitOfWork.TblDocCategoryRepository.GetByID(docCategoryVM.DocCategoryID);
                    docCategory.CategoryName = docCategoryVM.CategoryName;
                    docCategory.ModifiedDate = DateTime.Now;
                    docCategory.ModifiedBy = docCategoryVM.ModifiedBy;
                    unitOfWork.TblDocCategoryRepository.Update(docCategory);
                    unitOfWork.Save();

                    //Complete the Transaction
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }

        public bool DeleteDocumentCategory(int docCategoryID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblDocCategory docCategory = unitOfWork.TblDocCategoryRepository.GetByID(docCategoryID);
                    unitOfWork.TblDocCategoryRepository.Delete(docCategory);
                    unitOfWork.Save();

                    //Complete the Transaction
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }

        public List<DocCategoryVM> GetAllDocumentCategories()
        {
            try
            {
                var docCategoryData = unitOfWork.TblDocCategoryRepository.Get().ToList();

                List<DocCategoryVM> docCategoryList = new List<DocCategoryVM>();

                foreach (var docCategory in docCategoryData)
                {
                    DocCategoryVM docCategoryVM = new DocCategoryVM();
                    docCategoryVM.DocCategoryID = docCategory.DocCategoryID;
                    docCategoryVM.CategoryName = docCategory.CategoryName;
                    docCategoryVM.CreatedDate = docCategory.CreatedDate != null ? docCategory.CreatedDate.ToString() : string.Empty;
                    docCategoryVM.ModifiedDate = docCategory.ModifiedDate != null ? docCategory.ModifiedDate.ToString() : string.Empty;
                    docCategoryVM.CreatedBy = docCategory.CreatedBy != null ? Convert.ToInt32(docCategory.CreatedBy) : 0;
                    docCategoryVM.ModifiedBy = docCategory.ModifiedBy != null ? Convert.ToInt32(docCategory.ModifiedBy) : 0;

                    docCategoryList.Add(docCategoryVM);
                }

                return docCategoryList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DocCategoryVM GetDocumentCategoryByID(int docCategoryID)
        {
            try
            {
                var docCategoryData = unitOfWork.TblDocCategoryRepository.GetByID(docCategoryID);

                DocCategoryVM docCategoryVM = new DocCategoryVM();
                docCategoryVM.DocCategoryID = docCategoryData.DocCategoryID;
                docCategoryVM.CategoryName = docCategoryData.CategoryName;
                docCategoryVM.CreatedDate = docCategoryData.CreatedDate != null ? docCategoryData.CreatedDate.ToString() : string.Empty;
                docCategoryVM.ModifiedDate = docCategoryData.ModifiedDate != null ? docCategoryData.ModifiedDate.ToString() : string.Empty;
                docCategoryVM.CreatedBy = docCategoryData.CreatedBy != null ? Convert.ToInt32(docCategoryData.CreatedBy) : 0;
                docCategoryVM.ModifiedBy = docCategoryData.ModifiedBy != null ? Convert.ToInt32(docCategoryData.ModifiedBy) : 0;

                return docCategoryVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsDocumentCategoryAvailable(int? docCategoryID, string docCategoryName)
        {
            try
            {
                if (docCategoryID != null && unitOfWork.TblDocCategoryRepository.Get().Any(x => x.CategoryName.ToLower() == docCategoryName.ToLower() && x.DocCategoryID != docCategoryID))
                {
                    return true;
                }
                else if (docCategoryID == null && unitOfWork.TblDocCategoryRepository.Get().Any(x => x.CategoryName.ToLower() == docCategoryName.ToLower()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Required Document
        public bool SaveRequiredDocument(RequiredDocumentVM requiredDocumentVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblRequiredDocument requiredDocument = new tblRequiredDocument();
                    requiredDocument.insClassID = requiredDocumentVM.InsuranceClassID;
                    requiredDocument.insSubClassID = requiredDocumentVM.InsuranceSubClassID;
                    requiredDocument.DocCategoryID = requiredDocumentVM.DocCategoryID;
                    requiredDocument.DocumentName = requiredDocumentVM.DocumentName;
                    requiredDocument.CreatedDate = DateTime.Now;
                    requiredDocument.CreatedBy = requiredDocumentVM.CreatedBy;
                    unitOfWork.TblRequiredDocumentRepository.Insert(requiredDocument);
                    unitOfWork.Save();

                    //Complete the Transaction
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }

        public bool UpdateRequiredDocument(RequiredDocumentVM requiredDocumentVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblRequiredDocument requiredDocument = unitOfWork.TblRequiredDocumentRepository.GetByID(requiredDocumentVM.RequiredDocID);
                    requiredDocument.insClassID = requiredDocumentVM.InsuranceClassID;
                    requiredDocument.insSubClassID = requiredDocumentVM.InsuranceSubClassID;
                    requiredDocument.DocCategoryID = requiredDocumentVM.DocCategoryID;
                    requiredDocument.DocumentName = requiredDocumentVM.DocumentName;
                    requiredDocument.ModifiedDate = DateTime.Now;
                    requiredDocument.ModifiedBy = requiredDocumentVM.ModifiedBy;
                    unitOfWork.TblRequiredDocumentRepository.Update(requiredDocument);
                    unitOfWork.Save();

                    //Complete the Transaction
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }

        public bool DeleteRequiredDocument(int requiredDocID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblRequiredDocument requiredDocument = unitOfWork.TblRequiredDocumentRepository.GetByID(requiredDocID);
                    unitOfWork.TblRequiredDocumentRepository.Delete(requiredDocument);
                    unitOfWork.Save();

                    //Complete the Transaction
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }

        public List<RequiredDocumentVM> GetAllRequiredDocuments()
        {
            try
            {
                var requiredDocData = unitOfWork.TblRequiredDocumentRepository.Get().ToList();

                List<RequiredDocumentVM> requiredDocList = new List<RequiredDocumentVM>();

                foreach (var requiredDoc in requiredDocData)
                {
                    RequiredDocumentVM requiredDocumentVM = new RequiredDocumentVM();
                    requiredDocumentVM.RequiredDocID = requiredDoc.ReqDocID;
                    requiredDocumentVM.InsuranceClassID = requiredDoc.insClassID != null ? Convert.ToInt32(requiredDoc.insClassID) : 0;

                    if (requiredDocumentVM.InsuranceClassID > 0)
                    {
                        requiredDocumentVM.InsuranceClassName = requiredDoc.tblInsClass.Code;
                    }

                    requiredDocumentVM.InsuranceSubClassID = requiredDoc.insSubClassID != null ? Convert.ToInt32(requiredDoc.insSubClassID) : 0;

                    if (requiredDocumentVM.InsuranceSubClassID > 0)
                    {
                        requiredDocumentVM.InsuranceSubClassName = requiredDoc.tblInsSubClass.Description;
                    }

                    requiredDocumentVM.DocCategoryID = requiredDoc.DocCategoryID != null ? Convert.ToInt32(requiredDoc.DocCategoryID) : 0;

                    if (requiredDocumentVM.DocCategoryID > 0)
                    {
                        requiredDocumentVM.DocCategoryName = requiredDoc.tblDocCategory.CategoryName;
                    }

                    requiredDocumentVM.DocumentName = requiredDoc.DocumentName;
                    requiredDocumentVM.CreatedDate = requiredDoc.CreatedDate != null ? requiredDoc.CreatedDate.ToString() : string.Empty;
                    requiredDocumentVM.ModifiedDate = requiredDoc.ModifiedDate != null ? requiredDoc.ModifiedDate.ToString() : string.Empty;
                    requiredDocumentVM.CreatedBy = requiredDoc.CreatedBy != null ? Convert.ToInt32(requiredDoc.CreatedBy) : 0;
                    requiredDocumentVM.ModifiedBy = requiredDoc.ModifiedBy != null ? Convert.ToInt32(requiredDoc.ModifiedBy) : 0;

                    requiredDocList.Add(requiredDocumentVM);
                }

                return requiredDocList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<RequiredDocumentVM> GetAllRequiredDocumentsByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var requiredDocData = unitOfWork.TblRequiredDocumentRepository.Get(x => x.tblInsClass.BUID == businessUnitID).ToList();

                List<RequiredDocumentVM> requiredDocList = new List<RequiredDocumentVM>();

                foreach (var requiredDoc in requiredDocData)
                {
                    RequiredDocumentVM requiredDocumentVM = new RequiredDocumentVM();
                    requiredDocumentVM.RequiredDocID = requiredDoc.ReqDocID;
                    requiredDocumentVM.InsuranceClassID = requiredDoc.insClassID != null ? Convert.ToInt32(requiredDoc.insClassID) : 0;

                    if (requiredDocumentVM.InsuranceClassID > 0)
                    {
                        requiredDocumentVM.InsuranceClassName = requiredDoc.tblInsClass.Code;
                    }

                    requiredDocumentVM.InsuranceSubClassID = requiredDoc.insSubClassID != null ? Convert.ToInt32(requiredDoc.insSubClassID) : 0;

                    if (requiredDocumentVM.InsuranceSubClassID > 0)
                    {
                        requiredDocumentVM.InsuranceSubClassName = requiredDoc.tblInsSubClass.Description;
                    }

                    requiredDocumentVM.DocCategoryID = requiredDoc.DocCategoryID != null ? Convert.ToInt32(requiredDoc.DocCategoryID) : 0;

                    if (requiredDocumentVM.DocCategoryID > 0)
                    {
                        requiredDocumentVM.DocCategoryName = requiredDoc.tblDocCategory.CategoryName;
                    }

                    requiredDocumentVM.DocumentName = requiredDoc.DocumentName;
                    requiredDocumentVM.CreatedDate = requiredDoc.CreatedDate != null ? requiredDoc.CreatedDate.ToString() : string.Empty;
                    requiredDocumentVM.ModifiedDate = requiredDoc.ModifiedDate != null ? requiredDoc.ModifiedDate.ToString() : string.Empty;
                    requiredDocumentVM.CreatedBy = requiredDoc.CreatedBy != null ? Convert.ToInt32(requiredDoc.CreatedBy) : 0;
                    requiredDocumentVM.ModifiedBy = requiredDoc.ModifiedBy != null ? Convert.ToInt32(requiredDoc.ModifiedBy) : 0;

                    requiredDocList.Add(requiredDocumentVM);
                }

                return requiredDocList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public RequiredDocumentVM GetRequiredDocumentByID(int requiredDocID)
        {
            try
            {
                var requiredDocData = unitOfWork.TblRequiredDocumentRepository.GetByID(requiredDocID);

                RequiredDocumentVM requiredDocumentVM = new RequiredDocumentVM();
                requiredDocumentVM.RequiredDocID = requiredDocData.ReqDocID;
                requiredDocumentVM.InsuranceClassID = requiredDocData.insClassID != null ? Convert.ToInt32(requiredDocData.insClassID) : 0;

                if (requiredDocumentVM.InsuranceClassID > 0)
                {
                    requiredDocumentVM.InsuranceClassName = requiredDocData.tblInsClass.Code;
                }

                requiredDocumentVM.InsuranceSubClassID = requiredDocData.insSubClassID != null ? Convert.ToInt32(requiredDocData.insSubClassID) : 0;

                if (requiredDocumentVM.InsuranceSubClassID > 0)
                {
                    requiredDocumentVM.InsuranceSubClassName = requiredDocData.tblInsSubClass.Description;
                }

                requiredDocumentVM.DocCategoryID = requiredDocData.DocCategoryID != null ? Convert.ToInt32(requiredDocData.DocCategoryID) : 0;

                if (requiredDocumentVM.DocCategoryID > 0)
                {
                    requiredDocumentVM.DocCategoryName = requiredDocData.tblDocCategory.CategoryName;
                }

                requiredDocumentVM.DocumentName = requiredDocData.DocumentName;
                requiredDocumentVM.CreatedDate = requiredDocData.CreatedDate != null ? requiredDocData.CreatedDate.ToString() : string.Empty;
                requiredDocumentVM.ModifiedDate = requiredDocData.ModifiedDate != null ? requiredDocData.ModifiedDate.ToString() : string.Empty;
                requiredDocumentVM.CreatedBy = requiredDocData.CreatedBy != null ? Convert.ToInt32(requiredDocData.CreatedBy) : 0;
                requiredDocumentVM.ModifiedBy = requiredDocData.ModifiedBy != null ? Convert.ToInt32(requiredDocData.ModifiedBy) : 0;

                return requiredDocumentVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsRequiredDocumentAvailable(int? recDocID, string recDocName)
        {
            try
            {
                if (recDocID != null && unitOfWork.TblRequiredDocumentRepository.Get().Any(x => x.DocumentName.ToLower() == recDocName.ToLower() && x.ReqDocID != recDocID))
                {
                    return true;
                }
                else if (recDocID == null && unitOfWork.TblRequiredDocumentRepository.Get().Any(x => x.DocumentName.ToLower() == recDocName.ToLower()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
