using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.TransactionData
{
    public class ManageQuotation
    {
        private UnitOfWork unitOfWork;
        public ManageQuotation()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool SaveQuotationLine(List<QuotationLineView> lines)
        {
            List<QuotationLineView> QuotationLineViews = new List<QuotationLineView>();
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (QuotationLineView line in lines)
                    {
                        var quotLine = GetQuotationLine(line.CompID, line.InsSubClassID, line.QuotationHeaderID);
                        if (quotLine != null)
                        {
                            quotLine.FilePath = line.FilePath.Trim();
                            quotLine.FileName = line.FileName.Trim();
                            quotLine.IsRequested = line.IsRequested;
                            quotLine.ModifiedBy = line.ModifiedBy;
                            quotLine.ModifiedDate = DateTime.Now;
                            quotLine.RequestedDate = line.RequestedDate;

                            bool result = UpdateQuotationLine(quotLine);
                        }
                        else
                        {
                            tblQuotationLine quotationLine = new tblQuotationLine();
                            quotationLine.QuotationHeaderID = line.QuotationHeaderID;
                            quotationLine.InsClassID = line.InsClassID;
                            quotationLine.InsSubClassID = line.InsSubClassID;
                            quotationLine.CompID = line.CompID;
                            quotationLine.CreatedBy = line.CreatedBy;
                            quotationLine.CreatedDate = DateTime.Now;
                            quotationLine.FilePath = line.FilePath.Trim();
                            quotationLine.FileName = line.FileName.Trim();
                            quotationLine.IsRequested = line.IsRequested;
                            quotationLine.ModifiedBy = line.ModifiedBy;
                            quotationLine.ModifiedDate = DateTime.Now;
                            quotationLine.RequestedDate = line.RequestedDate;

                            unitOfWork.TblQuotationLineRepository.Insert(quotationLine);
                            unitOfWork.Save();
                        }
                    }
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool SaveQuotationHeader(QuotationHeaderVM quotationHeaderVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    //Save Quotation Header
                    tblQuotationHeader quotationHeader = new tblQuotationHeader();
                    quotationHeader.ClientRequestHeaderID = quotationHeaderVM.ClientRequestHeaderID;
                    quotationHeader.Status = quotationHeaderVM.Status;
                    quotationHeader.QuotationStatusCode = QuotationStatusCodeEnum.QP.ToString();
                    quotationHeader.CreatedBy = quotationHeaderVM.CreatedBy;
                    quotationHeader.CreatedDate = DateTime.Now;
                    quotationHeader.Other = quotationHeaderVM.Other;
                    quotationHeader.TransactionType = quotationHeaderVM.TransactionTypeID;
                    unitOfWork.TblQuotationHeaderRepository.Insert(quotationHeader);
                    unitOfWork.Save();

                    //Save Quotation Line
                    foreach (var quoteLine in quotationHeaderVM.QuotationLineDetails)
                    {
                        tblQuotationLine quotationLine = new tblQuotationLine();
                        quotationLine.QuotationHeaderID = quotationHeader.QuotationHeaderID;
                        quotationLine.InsClassID = quoteLine.InsuranceClassID;
                        quotationLine.InsSubClassID = quoteLine.InsuranceSubClassID;
                        quotationLine.CreatedBy = quotationHeaderVM.CreatedBy;
                        quotationLine.CreatedDate = DateTime.Now;
                        unitOfWork.TblQuotationLineRepository.Insert(quotationLine);
                        unitOfWork.Save();

                        //Save Quotation Requested Insurance Company
                        foreach (var reqInsComoany in quoteLine.RequestedInsuranceCompanyDetails)
                        {
                            tblQuotationRequestedInsCompany quotationRequestedInsCompany = new tblQuotationRequestedInsCompany();
                            quotationRequestedInsCompany.QuotationLineID = quotationLine.QuotationLineID;
                            quotationRequestedInsCompany.InsCompanyID = reqInsComoany.InsuranceCompanyID;
                            quotationRequestedInsCompany.Status = reqInsComoany.Status;
                            quotationRequestedInsCompany.CreatedBy = quotationHeaderVM.CreatedBy;
                            quotationRequestedInsCompany.CreatedDate = DateTime.Now;
                            unitOfWork.TblQuotationRequestedInsCompanyRepository.Insert(quotationRequestedInsCompany);
                            unitOfWork.Save();

                            //Save Quotation Details Insurance Company Header
                            foreach (var quoteInsCompanyHeader in reqInsComoany.QuotationDetailsInsCompanyHeaderDetails)
                            {
                                tblQuotationDetailsInsCompanyHeader quotationDetailsInsCompanyHeader = new tblQuotationDetailsInsCompanyHeader();
                                quotationDetailsInsCompanyHeader.QuotationRequestedInsCompanyID = quotationRequestedInsCompany.QuotationRequestedInsCompanyID;
                                quotationDetailsInsCompanyHeader.PremiumIncludingTax = quoteInsCompanyHeader.PremiumIncludingTax;
                                quotationDetailsInsCompanyHeader.ExcessDescription = quoteInsCompanyHeader.ExcessDescription;
                                quotationDetailsInsCompanyHeader.ExcessAmount = quoteInsCompanyHeader.ExcessAmount;
                                quotationDetailsInsCompanyHeader.CreatedBy = quotationHeaderVM.CreatedBy;
                                quotationDetailsInsCompanyHeader.CreatedDate = DateTime.Now;
                                unitOfWork.TblQuotationDetailsInsCompanyHeaderRepository.Insert(quotationDetailsInsCompanyHeader);
                                unitOfWork.Save();

                                //Save Quotation Details Insurance Company Line
                                foreach (var quoteInsCompanyLine in quoteInsCompanyHeader.QuotationDetailsInsCompanyLineDetails)
                                {
                                    tblQuotationDetailsInsCompanyLine quotationDetailsInsCompanyLine = new tblQuotationDetailsInsCompanyLine();
                                    quotationDetailsInsCompanyLine.QuotationDetailsInsCompanyHeaderID = quotationDetailsInsCompanyHeader.QuotationDetailsInsCompanyHeaderID;
                                    quotationDetailsInsCompanyLine.InsSubClassID = quoteInsCompanyLine.InsuranceSubClassID;
                                    quotationDetailsInsCompanyLine.SumInsured = quoteInsCompanyLine.SumInsured;
                                    quotationDetailsInsCompanyLine.CreatedBy = quotationHeaderVM.CreatedBy;
                                    quotationDetailsInsCompanyLine.CreatedDate = DateTime.Now;
                                    unitOfWork.TblQuotationDetailsInsCompanyLineRepository.Insert(quotationDetailsInsCompanyLine);
                                    unitOfWork.Save();

                                    //Save Quotation Details Insurance Company Scope
                                    foreach (var quoteInsCompanyScope in quoteInsCompanyLine.QuotationDetailsInsCompanyScopeDetails)
                                    {
                                        tblQuotationDetailsInsCompanyScope quotationDetailsInsCompanyScope = new tblQuotationDetailsInsCompanyScope();
                                        quotationDetailsInsCompanyScope.QuotationDetailsInsCompanyLineID = quotationDetailsInsCompanyLine.QuotationDetailsInsCompanyLineID;
                                        quotationDetailsInsCompanyScope.ScopeDescription = quoteInsCompanyScope.ScopeDescription;
                                        quotationDetailsInsCompanyScope.ExcessType = quoteInsCompanyScope.ExcessType;
                                        quotationDetailsInsCompanyScope.ExcessAmount = quoteInsCompanyScope.ExcessAmount;
                                        quotationDetailsInsCompanyScope.CreatedBy = quotationHeaderVM.CreatedBy;
                                        quotationDetailsInsCompanyScope.CreatedDate = DateTime.Now;
                                        unitOfWork.TblQuotationDetailsInsCompanyScopeRepository.Insert(quotationDetailsInsCompanyScope);
                                        unitOfWork.Save();
                                    }
                                }
                            }
                        }
                    }

                    //Update Client Request Header
                    tblClientRequestHeader clientRequestHeader = unitOfWork.TblClientRequestHeaderRepository.GetByID(quotationHeaderVM.ClientRequestHeaderID);

                    if (clientRequestHeader.IsQuotationCreated != true)
                    {
                        clientRequestHeader.IsQuotationCreated = true;
                        unitOfWork.TblClientRequestHeaderRepository.Update(clientRequestHeader);
                        unitOfWork.Save();
                    }

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

        public bool UpdateQuotationHeader(QuotHeaderVM quotationHeaderVM, List<QuotRequestedInsCompanyVM> insuranceCompanyVm, out string errorMessage)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (quotationHeaderVM.QuotationStatusCode != QuotationStatusCodeEnum.TCNI.ToString())
                    {
                        //Update Quotation Header
                        tblQuotationHeader quotationHeader = unitOfWork.TblQuotationHeaderRepository.GetByID(quotationHeaderVM.QuotationHeaderID);
                        quotationHeader.ClientRequestHeaderID = quotationHeaderVM.ClientRequestHeaderID;
                        quotationHeader.Status = quotationHeaderVM.Status;
                        quotationHeader.QuotationStatusCode = quotationHeaderVM.QuotationStatusCode;
                        quotationHeader.ModifiedBy = quotationHeaderVM.ModifiedBy;
                        quotationHeader.ModifiedDate = DateTime.Now;
                        quotationHeader.Other = quotationHeaderVM.Other;
                        quotationHeader.TransactionType = quotationHeaderVM.TransactionTypeID;
                        unitOfWork.TblQuotationHeaderRepository.Update(quotationHeader);
                        unitOfWork.Save();

                        //Delete Quotation Line Details, Requested Insurance Company Details, Insurance Company Header/Line Details and Insurance Company Scope Details
                        var quotationLineData = unitOfWork.TblQuotationLineRepository.Get(x => x.QuotationHeaderID == quotationHeader.QuotationHeaderID).ToList();

                        foreach (var quotationLine in quotationLineData)
                        {
                            var reqInsCompanyData = unitOfWork.TblQuotationRequestedInsCompanyRepository.Get(x => x.QuotationLineID == quotationLine.QuotationLineID).ToList();

                            foreach (var reqInsCompany in reqInsCompanyData)
                            {
                                var quotationInsCompanyHeaderData = unitOfWork.TblQuotationDetailsInsCompanyHeaderRepository.Get(x => x.QuotationRequestedInsCompanyID == reqInsCompany.QuotationRequestedInsCompanyID).ToList();

                                foreach (var quoteInsCompanyHeader in quotationInsCompanyHeaderData)
                                {
                                    var quotationInsCompanyLineData = unitOfWork.TblQuotationDetailsInsCompanyLineRepository.Get(x => x.QuotationDetailsInsCompanyHeaderID == quoteInsCompanyHeader.QuotationDetailsInsCompanyHeaderID).ToList();

                                    foreach (var quoteInsCompanyLine in quotationInsCompanyLineData)
                                    {
                                        var quotationInsCompanyScopeData = unitOfWork.TblQuotationDetailsInsCompanyScopeRepository.Get(x => x.QuotationDetailsInsCompanyLineID == quoteInsCompanyLine.QuotationDetailsInsCompanyLineID).ToList();

                                        foreach (var quotationInsCompanyScope in quotationInsCompanyScopeData)
                                        {
                                            unitOfWork.TblQuotationDetailsInsCompanyScopeRepository.Delete(quotationInsCompanyScope);
                                            unitOfWork.Save();
                                        }

                                        unitOfWork.TblQuotationDetailsInsCompanyLineRepository.Delete(quoteInsCompanyLine);
                                        unitOfWork.Save();
                                    }

                                    unitOfWork.TblQuotationDetailsInsCompanyHeaderRepository.Delete(quoteInsCompanyHeader);
                                    unitOfWork.Save();
                                }

                                unitOfWork.TblQuotationRequestedInsCompanyRepository.Delete(reqInsCompany);
                                unitOfWork.Save();
                            }

                            unitOfWork.TblQuotationLineRepository.Delete(quotationLine);
                            unitOfWork.Save();
                        }

                        if (quotationLineData.Count <= 0)
                        {

                            //foreach (var insLine in quotationHeaderVM.QuotLineDetails)
                            //{
                            foreach (var quoteLine in insuranceCompanyVm)
                            {
                                tblQuotationLine quotationLine = new tblQuotationLine();
                                //if (quotationLine.QuotationLineID > 0)
                                //    quotationLine.QuotationLineID = insLine.QuotationLineID;
                                //else
                                //    quotationLine.QuotationLineID = 0;
                                quotationLine.QuotationHeaderID = quotationHeader.QuotationHeaderID;
                                quotationLine.InsClassID = unitOfWork.TblInsSubClassRepository.GetByID(quoteLine.InsSubClassID).InsClassID; ;
                                quotationLine.InsSubClassID = quoteLine.InsSubClassID;
                                quotationLine.CreatedBy = quotationHeader.CreatedBy;
                                quotationLine.CreatedDate = quotationHeader.CreatedDate;
                                quotationLine.ModifiedBy = quotationHeaderVM.ModifiedBy;
                                quotationLine.ModifiedDate = DateTime.Now;
                                unitOfWork.TblQuotationLineRepository.Insert(quotationLine);
                                unitOfWork.Save();

                                //Save Quotation Requested Insurance Company
                                //foreach (var reqInsComoany in quoteLine.RequestedInsuranceCompanyDetails)
                                //{
                                tblQuotationRequestedInsCompany quotationRequestedInsCompany = new tblQuotationRequestedInsCompany();
                                quotationRequestedInsCompany.QuotationLineID = quotationLine.QuotationLineID;
                                quotationRequestedInsCompany.InsCompanyID = quoteLine.InsCompanyID;
                                quotationRequestedInsCompany.Status = quoteLine.Status;
                                quotationRequestedInsCompany.CreatedBy = quotationHeader.CreatedBy;
                                quotationRequestedInsCompany.CreatedDate = quotationHeader.CreatedDate;
                                quotationRequestedInsCompany.ModifiedBy = quotationHeaderVM.ModifiedBy;
                                quotationRequestedInsCompany.ModifiedDate = DateTime.Now;
                                unitOfWork.TblQuotationRequestedInsCompanyRepository.Insert(quotationRequestedInsCompany);
                                unitOfWork.Save();

                                #region Save Quotation Details Insurance Company Header/Line
                                //Save Quotation Details Insurance Company Header
                                //foreach (var quoteInsCompanyHeader in insuranceCompanyVm)
                                //{
                                tblQuotationDetailsInsCompanyHeader quotationDetailsInsCompanyHeader = new tblQuotationDetailsInsCompanyHeader();
                                quotationDetailsInsCompanyHeader.QuotationRequestedInsCompanyID = quotationRequestedInsCompany.QuotationRequestedInsCompanyID;
                                quotationDetailsInsCompanyHeader.PremiumIncludingTax = 0;
                                quotationDetailsInsCompanyHeader.ExcessDescription = "";
                                quotationDetailsInsCompanyHeader.ExcessAmount = 0;
                                quotationDetailsInsCompanyHeader.CreatedBy = quotationHeader.CreatedBy;
                                quotationDetailsInsCompanyHeader.CreatedDate = quotationHeader.CreatedDate;
                                quotationDetailsInsCompanyHeader.ModifiedBy = quotationHeaderVM.ModifiedBy;
                                quotationDetailsInsCompanyHeader.ModifiedDate = DateTime.Now;
                                unitOfWork.TblQuotationDetailsInsCompanyHeaderRepository.Insert(quotationDetailsInsCompanyHeader);
                                unitOfWork.Save();

                                if (quotationDetailsInsCompanyHeader != null)
                                {
                                    //Save Quotation Details Insurance Company Line
                                    //foreach (var quoteInsCompanyLine in quoteInsCompanyHeader.QuotationDetailsInsCompanyLineDetails)
                                    //{
                                    tblQuotationDetailsInsCompanyLine quotationDetailsInsCompanyLine = new tblQuotationDetailsInsCompanyLine();
                                    quotationDetailsInsCompanyLine.QuotationDetailsInsCompanyHeaderID = quotationDetailsInsCompanyHeader.QuotationDetailsInsCompanyHeaderID;
                                    quotationDetailsInsCompanyLine.InsSubClassID = quoteLine.InsSubClassID;
                                    quotationDetailsInsCompanyLine.SumInsured = 0;
                                    quotationDetailsInsCompanyLine.CreatedBy = quotationHeader.CreatedBy;
                                    quotationDetailsInsCompanyLine.CreatedDate = quotationHeader.CreatedDate;
                                    quotationDetailsInsCompanyLine.ModifiedBy = quotationHeaderVM.ModifiedBy;
                                    quotationDetailsInsCompanyLine.ModifiedDate = DateTime.Now;
                                    unitOfWork.TblQuotationDetailsInsCompanyLineRepository.Insert(quotationDetailsInsCompanyLine);
                                    unitOfWork.Save();

                                    //Save Quotation Details Insurance Company Scope
                                    //foreach (var quoteInsCompanyScope in quoteInsCompanyLine.QuotationDetailsInsCompanyScopeDetails)
                                    //{
                                    tblQuotationDetailsInsCompanyScope quotationDetailsInsCompanyScope = new tblQuotationDetailsInsCompanyScope();
                                    quotationDetailsInsCompanyScope.QuotationDetailsInsCompanyLineID = quotationDetailsInsCompanyLine.QuotationDetailsInsCompanyLineID;
                                    quotationDetailsInsCompanyScope.ScopeDescription = "";
                                    quotationDetailsInsCompanyScope.ExcessType = "";
                                    quotationDetailsInsCompanyScope.ExcessAmount = 0;
                                    quotationDetailsInsCompanyScope.CreatedBy = quotationHeader.CreatedBy;
                                    quotationDetailsInsCompanyScope.CreatedDate = quotationHeader.CreatedDate;
                                    quotationDetailsInsCompanyScope.ModifiedBy = quotationHeaderVM.ModifiedBy;
                                    quotationDetailsInsCompanyScope.ModifiedDate = DateTime.Now;
                                    unitOfWork.TblQuotationDetailsInsCompanyScopeRepository.Insert(quotationDetailsInsCompanyScope);
                                    unitOfWork.Save();
                                    //}

                                    //}
                                }
                                //}
                                #endregion
                                //}
                                //}
                                ////}
                            }
                        }
                        else
                        {
                            //Save Quotation Line
                            foreach (var quoteLine in quotationHeaderVM.QuotLineDetails)
                            {
                                tblQuotationLine quotationLine = new tblQuotationLine();
                                quotationLine.QuotationHeaderID = quotationHeader.QuotationHeaderID;
                                quotationLine.InsClassID = unitOfWork.TblInsSubClassRepository.GetByID(quoteLine.InsuranceSubClassID).InsClassID;
                                quotationLine.InsSubClassID = quoteLine.InsuranceSubClassID;
                                quotationLine.CreatedBy = quotationHeader.CreatedBy;
                                quotationLine.CreatedDate = quotationHeader.CreatedDate;
                                quotationLine.ModifiedBy = quotationHeaderVM.ModifiedBy;
                                quotationLine.ModifiedDate = DateTime.Now;
                                unitOfWork.TblQuotationLineRepository.Insert(quotationLine);
                                unitOfWork.Save();

                                //Save Quotation Requested Insurance Company
                                foreach (var reqInsComoany in quoteLine.RequestedQuotInsuranceCompanyDetails)
                                {
                                    tblQuotationRequestedInsCompany quotationRequestedInsCompany = new tblQuotationRequestedInsCompany();
                                    quotationRequestedInsCompany.QuotationLineID = quotationLine.QuotationLineID;
                                    quotationRequestedInsCompany.InsCompanyID = reqInsComoany.InsCompanyID;
                                    quotationRequestedInsCompany.Status = reqInsComoany.Status;
                                    quotationRequestedInsCompany.CreatedBy = quotationHeader.CreatedBy;
                                    quotationRequestedInsCompany.CreatedDate = quotationHeader.CreatedDate;
                                    quotationRequestedInsCompany.ModifiedBy = quotationHeaderVM.ModifiedBy;
                                    quotationRequestedInsCompany.ModifiedDate = DateTime.Now;
                                    unitOfWork.TblQuotationRequestedInsCompanyRepository.Insert(quotationRequestedInsCompany);
                                    unitOfWork.Save();

                                    #region Save Quotation Details Insurance Company Header/Line
                                    ////Save Quotation Details Insurance Company Header
                                    //foreach (var quoteInsCompanyHeader in reqInsComoany.QuotationDetailsInsCompanyHeaderDetails)
                                    //{
                                    //    tblQuotationDetailsInsCompanyHeader quotationDetailsInsCompanyHeader = new tblQuotationDetailsInsCompanyHeader();
                                    //    quotationDetailsInsCompanyHeader.QuotationRequestedInsCompanyID = quotationRequestedInsCompany.QuotationRequestedInsCompanyID;
                                    //    quotationDetailsInsCompanyHeader.PremiumIncludingTax = quoteInsCompanyHeader.PremiumIncludingTax;
                                    //    quotationDetailsInsCompanyHeader.ExcessDescription = quoteInsCompanyHeader.ExcessDescription;
                                    //    quotationDetailsInsCompanyHeader.ExcessAmount = quoteInsCompanyHeader.ExcessAmount;
                                    //    quotationDetailsInsCompanyHeader.CreatedBy = quotationHeader.CreatedBy;
                                    //    quotationDetailsInsCompanyHeader.CreatedDate = quotationHeader.CreatedDate;
                                    //    quotationDetailsInsCompanyHeader.ModifiedBy = quotationHeaderVM.ModifiedBy;
                                    //    quotationDetailsInsCompanyHeader.ModifiedDate = DateTime.Now;
                                    //    unitOfWork.TblQuotationDetailsInsCompanyHeaderRepository.Insert(quotationDetailsInsCompanyHeader);
                                    //    unitOfWork.Save();

                                    //    if (quoteInsCompanyHeader.QuotationDetailsInsCompanyLineDetails != null)
                                    //    {
                                    //        //Save Quotation Details Insurance Company Line
                                    //        foreach (var quoteInsCompanyLine in quoteInsCompanyHeader.QuotationDetailsInsCompanyLineDetails)
                                    //        {
                                    //            tblQuotationDetailsInsCompanyLine quotationDetailsInsCompanyLine = new tblQuotationDetailsInsCompanyLine();
                                    //            quotationDetailsInsCompanyLine.QuotationDetailsInsCompanyHeaderID = quotationDetailsInsCompanyHeader.QuotationDetailsInsCompanyHeaderID;
                                    //            quotationDetailsInsCompanyLine.InsSubClassID = quoteInsCompanyLine.InsuranceSubClassID;
                                    //            quotationDetailsInsCompanyLine.SumInsured = quoteInsCompanyLine.SumInsured;
                                    //            quotationDetailsInsCompanyLine.CreatedBy = quotationHeader.CreatedBy;
                                    //            quotationDetailsInsCompanyLine.CreatedDate = quotationHeader.CreatedDate;
                                    //            quotationDetailsInsCompanyLine.ModifiedBy = quotationHeaderVM.ModifiedBy;
                                    //            quotationDetailsInsCompanyLine.ModifiedDate = DateTime.Now;
                                    //            unitOfWork.TblQuotationDetailsInsCompanyLineRepository.Insert(quotationDetailsInsCompanyLine);
                                    //            unitOfWork.Save();

                                    //            //Save Quotation Details Insurance Company Scope
                                    //            foreach (var quoteInsCompanyScope in quoteInsCompanyLine.QuotationDetailsInsCompanyScopeDetails)
                                    //            {
                                    //                tblQuotationDetailsInsCompanyScope quotationDetailsInsCompanyScope = new tblQuotationDetailsInsCompanyScope();
                                    //                quotationDetailsInsCompanyScope.QuotationDetailsInsCompanyLineID = quotationDetailsInsCompanyLine.QuotationDetailsInsCompanyLineID;
                                    //                quotationDetailsInsCompanyScope.ScopeDescription = quoteInsCompanyScope.ScopeDescription;
                                    //                quotationDetailsInsCompanyScope.ExcessType = quoteInsCompanyScope.ExcessType;
                                    //                quotationDetailsInsCompanyScope.ExcessAmount = quoteInsCompanyScope.ExcessAmount;
                                    //                quotationDetailsInsCompanyScope.CreatedBy = quotationHeader.CreatedBy;
                                    //                quotationDetailsInsCompanyScope.CreatedDate = quotationHeader.CreatedDate;
                                    //                quotationDetailsInsCompanyScope.ModifiedBy = quotationHeaderVM.ModifiedBy;
                                    //                quotationDetailsInsCompanyScope.ModifiedDate = DateTime.Now;
                                    //                unitOfWork.TblQuotationDetailsInsCompanyScopeRepository.Insert(quotationDetailsInsCompanyScope);
                                    //                unitOfWork.Save();
                                    //            }
                                    //        }
                                    //    }
                                    //}
                                    #endregion
                                }
                            }


                        }
                        //Update Client Request Header
                        tblClientRequestHeader clientRequestHeader = unitOfWork.TblClientRequestHeaderRepository.GetByID(quotationHeaderVM.ClientRequestHeaderID);

                        if (clientRequestHeader.IsQuotationCreated != true)
                        {
                            clientRequestHeader.IsQuotationCreated = true;
                            unitOfWork.TblClientRequestHeaderRepository.Update(clientRequestHeader);
                            unitOfWork.Save();
                        }

                        //Complete the Transaction
                        dbTransaction.Commit();

                        errorMessage = "No Error";
                        return true;
                    }
                    else
                    {
                        errorMessage = "The quotation is in TCNI status. Therefore it cannot be modified";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();

                    errorMessage = "Update Failed";
                    return false;
                }
            }
        }

        public List<QuotationHeaderVM> GetAllQuotationHeaders()
        {
            try
            {
                var quotationHeaderData = unitOfWork.TblQuotationHeaderRepository.Get().ToList();

                List<QuotationHeaderVM> quotationHeaderList = new List<QuotationHeaderVM>();

                foreach (var quotationHeader in quotationHeaderData)
                {
                    QuotationHeaderVM quotationHeaderVM = new QuotationHeaderVM();
                    quotationHeaderVM.QuotationHeaderID = quotationHeader.QuotationHeaderID;
                    quotationHeaderVM.ClientRequestHeaderID = quotationHeader.ClientRequestHeaderID != null ? Convert.ToInt32(quotationHeader.ClientRequestHeaderID) : 0;

                    if (quotationHeaderVM.ClientRequestHeaderID > 0)
                    {
                        quotationHeaderVM.ClientID = quotationHeader.tblClientRequestHeader.ClientID != null ? Convert.ToInt32(quotationHeader.tblClientRequestHeader.ClientID) : 0;

                        if (quotationHeaderVM.ClientID > 0)
                        {
                            quotationHeaderVM.ClientName = quotationHeader.tblClientRequestHeader.tblClient.ClientName;
                        }

                        quotationHeaderVM.PartnerID = quotationHeader.tblClientRequestHeader.PartnerID != null ? Convert.ToInt32(quotationHeader.tblClientRequestHeader.PartnerID) : 0;

                        if (quotationHeaderVM.PartnerID > 0)
                        {
                            quotationHeaderVM.PartnerName = "Partner Name"; //quotationHeader.tblClientRequestHeader.tblPartner.PartnerName;
                        }

                        quotationHeaderVM.RequestedDate = quotationHeader.tblClientRequestHeader.RequestedDate != null ? Convert.ToDateTime(quotationHeader.tblClientRequestHeader.RequestedDate).ToString("dd/MM/yyyy") : string.Empty;
                    }

                    quotationHeaderVM.Status = quotationHeader.Status;
                    quotationHeaderVM.QuotationStatusCode = quotationHeader.QuotationStatusCode;

                    if (!string.IsNullOrEmpty(quotationHeaderVM.QuotationStatusCode))
                    {
                        quotationHeaderVM.QuotationStatusCodeDescription = unitOfWork.TblQuotationStatusRepository.Get(x => x.QuotationStatusCode == quotationHeaderVM.QuotationStatusCode).SingleOrDefault().Description;
                    }

                    quotationHeaderVM.CreatedBy = quotationHeader.CreatedBy != null ? Convert.ToInt32(quotationHeader.CreatedBy) : 0;
                    quotationHeaderVM.CreatedDate = quotationHeader.CreatedDate != null ? quotationHeader.CreatedDate.ToString() : string.Empty;
                    quotationHeaderVM.ModifiedBy = quotationHeader.ModifiedBy != null ? Convert.ToInt32(quotationHeader.ModifiedBy) : 0;
                    quotationHeaderVM.ModifiedDate = quotationHeader.ModifiedDate != null ? quotationHeader.ModifiedDate.ToString() : string.Empty;

                    //Quotation Line Details
                    var quotationLineData = unitOfWork.TblQuotationLineRepository.Get(x => x.QuotationHeaderID == quotationHeader.QuotationHeaderID).ToList();

                    List<QuotationLineVM> quotationLineList = new List<QuotationLineVM>();

                    foreach (var quotationLine in quotationLineData)
                    {
                        QuotationLineVM quotationLineVM = new QuotationLineVM();
                        quotationLineVM.QuotationLineID = quotationLine.QuotationLineID;
                        quotationLineVM.InsuranceClassID = quotationLine.InsClassID != null ? Convert.ToInt32(quotationLine.InsClassID) : 0;

                        if (quotationLineVM.InsuranceClassID > 0)
                        {
                            quotationLineVM.InsuranceCode = quotationLine.tblInsClass.Code;
                        }

                        quotationLineVM.InsuranceSubClassID = quotationLine.InsSubClassID != null ? Convert.ToInt32(quotationLine.InsSubClassID) : 0;

                        if (quotationLineVM.InsuranceSubClassID > 0)
                        {
                            quotationLineVM.InsuranceSubClassDescription = quotationLine.tblInsSubClass.Description;
                        }

                        quotationLineVM.CreatedBy = quotationLine.CreatedBy != null ? Convert.ToInt32(quotationLine.CreatedBy) : 0;
                        quotationLineVM.CreatedDate = quotationLine.CreatedDate != null ? quotationLine.CreatedDate.ToString() : string.Empty;
                        quotationLineVM.ModifiedBy = quotationLine.ModifiedBy != null ? Convert.ToInt32(quotationLine.ModifiedBy) : 0;
                        quotationLineVM.ModifiedDate = quotationLine.ModifiedDate != null ? quotationLine.ModifiedDate.ToString() : string.Empty;

                        //Requested Insurance Company Details
                        var reqInsCompanyData = unitOfWork.TblQuotationRequestedInsCompanyRepository.Get(x => x.QuotationLineID == quotationLine.QuotationLineID).ToList();

                        List<QuotationRequestedInsCompanyVM> requestedInsCompanyList = new List<QuotationRequestedInsCompanyVM>();

                        foreach (var reqInsCompany in reqInsCompanyData)
                        {
                            QuotationRequestedInsCompanyVM quotationRequestedInsCompanyVM = new QuotationRequestedInsCompanyVM();
                            quotationRequestedInsCompanyVM.QuotationRequestedInsCompanyID = reqInsCompany.QuotationRequestedInsCompanyID;
                            quotationRequestedInsCompanyVM.InsuranceCompanyID = reqInsCompany.InsCompanyID != null ? Convert.ToInt32(reqInsCompany.InsCompanyID) : 0;

                            if (quotationRequestedInsCompanyVM.InsuranceCompanyID > 0)
                            {
                                quotationRequestedInsCompanyVM.InsuranceCompanyName = reqInsCompany.tblInsCompany.InsCompany;
                            }

                            quotationRequestedInsCompanyVM.Status = reqInsCompany.Status;
                            quotationRequestedInsCompanyVM.CreatedBy = reqInsCompany.CreatedBy != null ? Convert.ToInt32(reqInsCompany.CreatedBy) : 0;
                            quotationRequestedInsCompanyVM.CreatedDate = reqInsCompany.CreatedDate != null ? reqInsCompany.CreatedDate.ToString() : string.Empty;
                            quotationRequestedInsCompanyVM.ModifiedBy = reqInsCompany.ModifiedBy != null ? Convert.ToInt32(reqInsCompany.ModifiedBy) : 0;
                            quotationRequestedInsCompanyVM.ModifiedDate = reqInsCompany.ModifiedDate != null ? reqInsCompany.ModifiedDate.ToString() : string.Empty;

                            //Quotation Details Insurance Company Header Details
                            var quotationInsCompanyHeaderData = unitOfWork.TblQuotationDetailsInsCompanyHeaderRepository.Get(x => x.QuotationRequestedInsCompanyID == reqInsCompany.QuotationRequestedInsCompanyID).ToList();

                            List<QuotationDetailsInsCompanyHeaderVM> quotationDetailsInsCompanyHeaderList = new List<QuotationDetailsInsCompanyHeaderVM>();

                            foreach (var quoteInsCompanyHeader in quotationInsCompanyHeaderData)
                            {
                                QuotationDetailsInsCompanyHeaderVM quotationDetailsInsCompanyHeaderVM = new QuotationDetailsInsCompanyHeaderVM();
                                quotationDetailsInsCompanyHeaderVM.QuotationDetailsInsCompanyHeaderID = quoteInsCompanyHeader.QuotationDetailsInsCompanyHeaderID;
                                quotationDetailsInsCompanyHeaderVM.PremiumIncludingTax = quoteInsCompanyHeader.PremiumIncludingTax != null ? Convert.ToDouble(quoteInsCompanyHeader.PremiumIncludingTax) : 0;
                                quotationDetailsInsCompanyHeaderVM.ExcessDescription = quoteInsCompanyHeader.ExcessDescription;
                                quotationDetailsInsCompanyHeaderVM.ExcessAmount = quoteInsCompanyHeader.ExcessAmount != null ? Convert.ToDecimal(quoteInsCompanyHeader.ExcessAmount) : 0;
                                quotationDetailsInsCompanyHeaderVM.CreatedBy = quoteInsCompanyHeader.CreatedBy != null ? Convert.ToInt32(quoteInsCompanyHeader.CreatedBy) : 0;
                                quotationDetailsInsCompanyHeaderVM.CreatedDate = quoteInsCompanyHeader.CreatedDate != null ? quoteInsCompanyHeader.CreatedDate.ToString() : string.Empty;
                                quotationDetailsInsCompanyHeaderVM.ModifiedBy = quoteInsCompanyHeader.ModifiedBy != null ? Convert.ToInt32(quoteInsCompanyHeader.ModifiedBy) : 0;
                                quotationDetailsInsCompanyHeaderVM.ModifiedDate = quoteInsCompanyHeader.ModifiedDate != null ? quoteInsCompanyHeader.ModifiedDate.ToString() : string.Empty;

                                //Quotation Details Insurance Company Line Details
                                var quotationInsCompanyLineData = unitOfWork.TblQuotationDetailsInsCompanyLineRepository.Get(x => x.QuotationDetailsInsCompanyHeaderID == quoteInsCompanyHeader.QuotationDetailsInsCompanyHeaderID).ToList();

                                List<QuotationDetailsInsCompanyLineVM> quotationDetailsInsCompanyLineList = new List<QuotationDetailsInsCompanyLineVM>();

                                foreach (var quoteInsCompanyLine in quotationInsCompanyLineData)
                                {
                                    QuotationDetailsInsCompanyLineVM quotationDetailsInsCompanyLineVM = new QuotationDetailsInsCompanyLineVM();
                                    quotationDetailsInsCompanyLineVM.QuotationDetailsInsCompanyLineID = quoteInsCompanyLine.QuotationDetailsInsCompanyLineID;
                                    quotationDetailsInsCompanyLineVM.InsuranceSubClassID = quoteInsCompanyLine.InsSubClassID != null ? Convert.ToInt32(quoteInsCompanyLine.InsSubClassID) : 0;

                                    if (quotationDetailsInsCompanyLineVM.InsuranceSubClassID > 0)
                                    {
                                        quotationDetailsInsCompanyLineVM.InsuranceSubClassDescription = quoteInsCompanyLine.tblInsSubClass.Description;
                                    }

                                    quotationDetailsInsCompanyLineVM.SumInsured = quoteInsCompanyLine.SumInsured != null ? Convert.ToDecimal(quoteInsCompanyLine.SumInsured) : 0;
                                    quotationDetailsInsCompanyLineVM.CreatedBy = quoteInsCompanyLine.CreatedBy != null ? Convert.ToInt32(quoteInsCompanyLine.CreatedBy) : 0;
                                    quotationDetailsInsCompanyLineVM.CreatedDate = quoteInsCompanyLine.CreatedDate != null ? quoteInsCompanyLine.CreatedDate.ToString() : string.Empty;
                                    quotationDetailsInsCompanyLineVM.ModifiedBy = quoteInsCompanyLine.ModifiedBy != null ? Convert.ToInt32(quoteInsCompanyLine.ModifiedBy) : 0;
                                    quotationDetailsInsCompanyLineVM.ModifiedDate = quoteInsCompanyLine.ModifiedDate != null ? quoteInsCompanyLine.ModifiedDate.ToString() : string.Empty;

                                    //Quotation Details Insurance Company Scope Details
                                    var quotationInsCompanyScopeData = unitOfWork.TblQuotationDetailsInsCompanyScopeRepository.Get(x => x.QuotationDetailsInsCompanyLineID == quoteInsCompanyLine.QuotationDetailsInsCompanyLineID).ToList();

                                    List<QuotationDetailsInsCompanyScopeVM> quotationDetailsInsCompanyScopeList = new List<QuotationDetailsInsCompanyScopeVM>();

                                    foreach (var quoteInsCompanyScope in quotationInsCompanyScopeData)
                                    {
                                        QuotationDetailsInsCompanyScopeVM quotationDetailsInsCompanyScopeVM = new QuotationDetailsInsCompanyScopeVM();
                                        quotationDetailsInsCompanyScopeVM.QuotationDetailsInsCompanyScopeID = quoteInsCompanyScope.QuotationDetailsInsCompanyScopeID;
                                        quotationDetailsInsCompanyScopeVM.ScopeDescription = quoteInsCompanyScope.ScopeDescription;
                                        quotationDetailsInsCompanyScopeVM.ExcessType = quoteInsCompanyScope.ExcessType;
                                        quotationDetailsInsCompanyScopeVM.ExcessAmount = quoteInsCompanyScope.ExcessAmount != null ? Convert.ToDecimal(quoteInsCompanyScope.ExcessAmount) : 0;
                                        quotationDetailsInsCompanyScopeVM.CreatedBy = quoteInsCompanyScope.CreatedBy != null ? Convert.ToInt32(quoteInsCompanyScope.CreatedBy) : 0;
                                        quotationDetailsInsCompanyScopeVM.CreatedDate = quoteInsCompanyScope.CreatedDate != null ? quoteInsCompanyScope.CreatedDate.ToString() : string.Empty;
                                        quotationDetailsInsCompanyScopeVM.ModifiedBy = quoteInsCompanyScope.ModifiedBy != null ? Convert.ToInt32(quoteInsCompanyScope.ModifiedBy) : 0;
                                        quotationDetailsInsCompanyScopeVM.ModifiedDate = quoteInsCompanyScope.ModifiedDate != null ? quoteInsCompanyScope.ModifiedDate.ToString() : string.Empty;

                                        quotationDetailsInsCompanyScopeList.Add(quotationDetailsInsCompanyScopeVM);
                                    }

                                    quotationDetailsInsCompanyLineVM.QuotationDetailsInsCompanyScopeDetails = quotationDetailsInsCompanyScopeList;

                                    quotationDetailsInsCompanyLineList.Add(quotationDetailsInsCompanyLineVM);
                                }

                                quotationDetailsInsCompanyHeaderVM.QuotationDetailsInsCompanyLineDetails = quotationDetailsInsCompanyLineList;

                                quotationDetailsInsCompanyHeaderList.Add(quotationDetailsInsCompanyHeaderVM);
                            }

                            quotationRequestedInsCompanyVM.QuotationDetailsInsCompanyHeaderDetails = quotationDetailsInsCompanyHeaderList;

                            requestedInsCompanyList.Add(quotationRequestedInsCompanyVM);
                        }

                        quotationLineVM.RequestedInsuranceCompanyDetails = requestedInsCompanyList;

                        quotationLineList.Add(quotationLineVM);
                    }

                    quotationHeaderVM.QuotationLineDetails = quotationLineList;

                    quotationHeaderList.Add(quotationHeaderVM);
                }

                return quotationHeaderList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<ChargeTypeVM> getInsChargeType(int insuranceCompanyID, int insclss, int inssubclass)
        {
            try
            {

                List<ChargeTypeVM> ChargeTypeList = new List<ChargeTypeVM>();
                var qryCommisionHeader = new List<tblComCommisionStructureLine>();
                ChargeTypeVM chargeTypeVM = new ChargeTypeVM();

                using (var db = new PERFECTIBSEntities())
                {

                    var qry = from ct in db.tblChargeTypes
                              join comst in db.tblComCommisionStructureLine on ct.ChargeTypeID equals comst.ChargeTypeID
                              join ins in db.tblInsComCommisionStructureHeader on comst.ComStructID equals ins.ComHeaderID
                              where ins.InsCompanyID == insuranceCompanyID && ins.InsSubClassID == inssubclass && ins.InsClassID == insclss
                              && ins.InsCompanyID == insuranceCompanyID
                              select new
                              {
                                  ct.ChargeType
                                     ,
                                  comst.ChargeTypeID
                                     ,

                                  comst.Percentage
                                     ,

                                  ins.ComHeaderID
                                  ,
                                  ins.ComStructName
                              };

                    List<ChargeTypeVM> lists = qry.AsEnumerable()
                   .Select(o => new ChargeTypeVM
                   {
                       ChargeTypeID = (int)o.ChargeTypeID,
                       ChargeTypeName = o.ChargeType,
                       Percentage = (double)o.Percentage,
                       comstructID = (int)o.ComHeaderID,
                       ComStructName = o.ComStructName
                   }).ToList();



                    foreach (var chargeType in lists)
                    {
                        chargeTypeVM = new ChargeTypeVM();

                        chargeTypeVM.ChargeTypeID = chargeType.ChargeTypeID;
                        chargeTypeVM.ChargeTypeName = chargeType.ChargeTypeName;
                        chargeTypeVM.Percentage = chargeType.Percentage != null ? Convert.ToDouble(chargeType.Percentage) : 0;
                        chargeTypeVM.comstructID = chargeType.comstructID;
                        chargeTypeVM.ComStructName = chargeType.ComStructName;
                        ChargeTypeList.Add(chargeTypeVM);

                    }



                }

                return ChargeTypeList;



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ChargeTypeVM> GetAllQuotationLine(int quotationHeaderID, int insuranceCompanyID)
        {
            try
            {
                var quotationHeaderData = unitOfWork.TblQuotationHeaderRepository.GetByID(quotationHeaderID);
                var quotationLineData = unitOfWork.TblQuotationLineRepository.Get(x => x.QuotationHeaderID == quotationHeaderID).ToList();

                List<QuotationLineVM> quotationLineList = new List<QuotationLineVM>();
                List<ChargeTypeVM> ChargeTypeList = new List<ChargeTypeVM>();

                var qryCommisionHeader = new List<tblComCommisionStructureLine>();

                foreach (var quotationLine in quotationLineData)
                {
                    QuotationLineVM quotationLineVM = new QuotationLineVM();
                    ChargeTypeVM chargeTypeVM = new ChargeTypeVM();
                    quotationLineVM.QuotationLineID = quotationLine.QuotationLineID;
                    quotationLineVM.InsuranceClassID = quotationLine.InsClassID != null ? Convert.ToInt32(quotationLine.InsClassID) : 0;

                    if (quotationLineVM.InsuranceClassID > 0)
                    {
                        quotationLineVM.InsuranceCode = quotationLine.tblInsClass.Code;
                    }

                    quotationLineVM.InsuranceSubClassID = quotationLine.InsSubClassID != null ? Convert.ToInt32(quotationLine.InsSubClassID) : 0;

                    if (quotationLineVM.InsuranceSubClassID > 0)
                    {
                        quotationLineVM.InsuranceSubClassDescription = quotationLine.tblInsSubClass.Description;
                    }


                    using (var db = new PERFECTIBSEntities())
                    {

                        var qry = from ct in db.tblChargeTypes
                                  join comst in db.tblComCommisionStructureLine on ct.ChargeTypeID equals comst.ChargeTypeID
                                  join ins in db.tblInsComCommisionStructureHeader on comst.ComStructID equals ins.ComHeaderID
                                  where ins.InsCompanyID == insuranceCompanyID && ins.InsSubClassID == quotationLineVM.InsuranceSubClassID && ins.InsClassID == quotationLineVM.InsuranceClassID
                                  select new
                                  {
                                      ct.ChargeType
                                         ,
                                      comst.ChargeTypeID
                                         ,

                                      comst.Percentage
                                         ,

                                      ins.ComHeaderID
                                      ,
                                      ins.ComStructName
                                  };

                        List<ChargeTypeVM> lists = qry.AsEnumerable()
                       .Select(o => new ChargeTypeVM
                       {
                           ChargeTypeID = (int)o.ChargeTypeID,
                           ChargeTypeName = o.ChargeType,
                           Percentage = (double)o.Percentage,
                           comstructID = (int)o.ComHeaderID,
                           ComStructName = o.ComStructName
                       }).ToList();



                        foreach (var chargeType in lists)
                        {
                            chargeTypeVM = new ChargeTypeVM();

                            chargeTypeVM.ChargeTypeID = chargeType.ChargeTypeID;
                            chargeTypeVM.ChargeTypeName = chargeType.ChargeTypeName;
                            chargeTypeVM.Percentage = chargeType.Percentage != null ? Convert.ToDouble(chargeType.Percentage) : 0;
                            chargeTypeVM.comstructID = chargeType.comstructID;
                            chargeTypeVM.ComStructName = chargeType.ComStructName;
                            ChargeTypeList.Add(chargeTypeVM);

                        }

                    }

                }

                return ChargeTypeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<QuotationHeaderVM> GetAllQuotationHeadersByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var quotationHeaderData = unitOfWork.TblQuotationHeaderRepository.Get(x => x.tblClientRequestHeader.tblClient.BUID == businessUnitID).ToList();

                List<QuotationHeaderVM> quotationHeaderList = new List<QuotationHeaderVM>();

                foreach (var quotationHeader in quotationHeaderData)
                {
                    QuotationHeaderVM quotationHeaderVM = new QuotationHeaderVM();
                    quotationHeaderVM.QuotationHeaderID = quotationHeader.QuotationHeaderID;
                    quotationHeaderVM.ClientRequestHeaderID = quotationHeader.ClientRequestHeaderID != null ? Convert.ToInt32(quotationHeader.ClientRequestHeaderID) : 0;

                    if (quotationHeaderVM.ClientRequestHeaderID > 0)
                    {
                        
                        
                        var dbbb = new PERFECTIBSEntities();

                        //var clientObj = (from cl in dbbb.tblClients
                        //              join clrequestheader in dbbb.tblClientRequestHeaders on cl.ClientID equals clrequestheader.ClientID
                        //              //join partner in dbbb.tblPartners on clrequestheader.PartnerID equals partner.PartnerID
                        //              where clrequestheader.ClientRequestHeaderID == quotationHeaderVM.ClientRequestHeaderID
                        //              select new
                        //              {
                        //                  cl,
                        //                  clrequestheader,
                        //              //    partner

                        //              }).FirstOrDefault();

                        var clientRequestHeaderObj = dbbb.tblClientRequestHeaders.AsNoTracking().Where(c => c.ClientRequestHeaderID == quotationHeader.ClientRequestHeaderID).FirstOrDefault();
                        var clientObj = dbbb.tblClients.Where(c => c.ClientID == clientRequestHeaderObj.ClientID).FirstOrDefault();
                        var partnerObj = dbbb.tblPartners.Where(c => c.PartnerID == clientRequestHeaderObj.PartnerID).FirstOrDefault();

                        quotationHeaderVM.ClientID = quotationHeader.tblClientRequestHeader.ClientID != null ? Convert.ToInt32(quotationHeader.tblClientRequestHeader.ClientID) : 0;


                        if (quotationHeaderVM.ClientID > 0)
                        {
                            quotationHeaderVM.ClientName = quotationHeader.tblClientRequestHeader.tblClient.ClientName;
                        }

                        quotationHeaderVM.PartnerID = quotationHeader.tblClientRequestHeader.PartnerID != null ? Convert.ToInt32(quotationHeader.tblClientRequestHeader.PartnerID) : 0;

                        if (quotationHeaderVM.PartnerID > 0)
                        {
                            quotationHeaderVM.PartnerName = partnerObj.PartnerName;//quotationHeader.tblClientRequestHeader.tblPartner.PartnerName;
                        }

                        quotationHeaderVM.RequestedDate = quotationHeader.tblClientRequestHeader.RequestedDate != null ? Convert.ToDateTime(quotationHeader.tblClientRequestHeader.RequestedDate).ToString("dd/MM/yyyy") : string.Empty;
                    }

                    quotationHeaderVM.Status = quotationHeader.Status;
                    quotationHeaderVM.QuotationStatusCode = quotationHeader.QuotationStatusCode;

                    if (!string.IsNullOrEmpty(quotationHeaderVM.QuotationStatusCode))
                    {
                        quotationHeaderVM.QuotationStatusCodeDescription = unitOfWork.TblQuotationStatusRepository.Get(x => x.QuotationStatusCode == quotationHeaderVM.QuotationStatusCode).SingleOrDefault().Description;
                    }

                    quotationHeaderVM.CreatedBy = quotationHeader.CreatedBy != null ? Convert.ToInt32(quotationHeader.CreatedBy) : 0;
                    quotationHeaderVM.CreatedDate = quotationHeader.CreatedDate != null ? quotationHeader.CreatedDate.ToString() : string.Empty;
                    quotationHeaderVM.ModifiedBy = quotationHeader.ModifiedBy != null ? Convert.ToInt32(quotationHeader.ModifiedBy) : 0;
                    quotationHeaderVM.ModifiedDate = quotationHeader.ModifiedDate != null ? quotationHeader.ModifiedDate.ToString() : string.Empty;

                    //Quotation Line Details
                    var quotationLineData = unitOfWork.TblQuotationLineRepository.Get(x => x.QuotationHeaderID == quotationHeader.QuotationHeaderID).ToList();

                    List<QuotationLineVM> quotationLineList = new List<QuotationLineVM>();

                    foreach (var quotationLine in quotationLineData)
                    {
                        QuotationLineVM quotationLineVM = new QuotationLineVM();
                        quotationLineVM.QuotationLineID = quotationLine.QuotationLineID;
                        quotationLineVM.InsuranceClassID = quotationLine.InsClassID != null ? Convert.ToInt32(quotationLine.InsClassID) : 0;

                        if (quotationLineVM.InsuranceClassID > 0)
                        {
                            quotationLineVM.InsuranceCode = quotationLine.tblInsClass.Code;
                        }

                        quotationLineVM.InsuranceSubClassID = quotationLine.InsSubClassID != null ? Convert.ToInt32(quotationLine.InsSubClassID) : 0;

                        if (quotationLineVM.InsuranceSubClassID > 0)
                        {
                            quotationLineVM.InsuranceSubClassDescription = quotationLine.tblInsSubClass.Description;
                        }

                        quotationLineVM.CreatedBy = quotationLine.CreatedBy != null ? Convert.ToInt32(quotationLine.CreatedBy) : 0;
                        quotationLineVM.CreatedDate = quotationLine.CreatedDate != null ? quotationLine.CreatedDate.ToString() : string.Empty;
                        quotationLineVM.ModifiedBy = quotationLine.ModifiedBy != null ? Convert.ToInt32(quotationLine.ModifiedBy) : 0;
                        quotationLineVM.ModifiedDate = quotationLine.ModifiedDate != null ? quotationLine.ModifiedDate.ToString() : string.Empty;

                        //Requested Insurance Company Details
                        var reqInsCompanyData = unitOfWork.TblQuotationRequestedInsCompanyRepository.Get(x => x.QuotationLineID == quotationLine.QuotationLineID).ToList();

                        List<QuotationRequestedInsCompanyVM> requestedInsCompanyList = new List<QuotationRequestedInsCompanyVM>();

                        foreach (var reqInsCompany in reqInsCompanyData)
                        {
                            QuotationRequestedInsCompanyVM quotationRequestedInsCompanyVM = new QuotationRequestedInsCompanyVM();
                            quotationRequestedInsCompanyVM.QuotationRequestedInsCompanyID = reqInsCompany.QuotationRequestedInsCompanyID;
                            quotationRequestedInsCompanyVM.InsuranceCompanyID = reqInsCompany.InsCompanyID != null ? Convert.ToInt32(reqInsCompany.InsCompanyID) : 0;

                            if (quotationRequestedInsCompanyVM.InsuranceCompanyID > 0)
                            {
                                quotationRequestedInsCompanyVM.InsuranceCompanyName = reqInsCompany.tblInsCompany.InsCompany;
                            }

                            quotationRequestedInsCompanyVM.Status = reqInsCompany.Status;
                            quotationRequestedInsCompanyVM.CreatedBy = reqInsCompany.CreatedBy != null ? Convert.ToInt32(reqInsCompany.CreatedBy) : 0;
                            quotationRequestedInsCompanyVM.CreatedDate = reqInsCompany.CreatedDate != null ? reqInsCompany.CreatedDate.ToString() : string.Empty;
                            quotationRequestedInsCompanyVM.ModifiedBy = reqInsCompany.ModifiedBy != null ? Convert.ToInt32(reqInsCompany.ModifiedBy) : 0;
                            quotationRequestedInsCompanyVM.ModifiedDate = reqInsCompany.ModifiedDate != null ? reqInsCompany.ModifiedDate.ToString() : string.Empty;

                            #region Quotation Details Insurance Company Header Details
                            //Quotation Details Insurance Company Header Details
                            //var quotationInsCompanyHeaderData = unitOfWork.TblQuotationDetailsInsCompanyHeaderRepository.Get(x => x.QuotationRequestedInsCompanyID == reqInsCompany.QuotationRequestedInsCompanyID).ToList();

                            //List<QuotationDetailsInsCompanyHeaderVM> quotationDetailsInsCompanyHeaderList = new List<QuotationDetailsInsCompanyHeaderVM>();

                            //foreach (var quoteInsCompanyHeader in quotationInsCompanyHeaderData)
                            //{
                            //    QuotationDetailsInsCompanyHeaderVM quotationDetailsInsCompanyHeaderVM = new QuotationDetailsInsCompanyHeaderVM();
                            //    quotationDetailsInsCompanyHeaderVM.QuotationDetailsInsCompanyHeaderID = quoteInsCompanyHeader.QuotationDetailsInsCompanyHeaderID;
                            //    quotationDetailsInsCompanyHeaderVM.PremiumIncludingTax = quoteInsCompanyHeader.PremiumIncludingTax != null ? Convert.ToDouble(quoteInsCompanyHeader.PremiumIncludingTax) : 0;
                            //    quotationDetailsInsCompanyHeaderVM.ExcessDescription = quoteInsCompanyHeader.ExcessDescription;
                            //    quotationDetailsInsCompanyHeaderVM.ExcessAmount = quoteInsCompanyHeader.ExcessAmount != null ? Convert.ToDecimal(quoteInsCompanyHeader.ExcessAmount) : 0;
                            //    quotationDetailsInsCompanyHeaderVM.CreatedBy = quoteInsCompanyHeader.CreatedBy != null ? Convert.ToInt32(quoteInsCompanyHeader.CreatedBy) : 0;
                            //    quotationDetailsInsCompanyHeaderVM.CreatedDate = quoteInsCompanyHeader.CreatedDate != null ? quoteInsCompanyHeader.CreatedDate.ToString() : string.Empty;
                            //    quotationDetailsInsCompanyHeaderVM.ModifiedBy = quoteInsCompanyHeader.ModifiedBy != null ? Convert.ToInt32(quoteInsCompanyHeader.ModifiedBy) : 0;
                            //    quotationDetailsInsCompanyHeaderVM.ModifiedDate = quoteInsCompanyHeader.ModifiedDate != null ? quoteInsCompanyHeader.ModifiedDate.ToString() : string.Empty;

                            //    //Quotation Details Insurance Company Line Details
                            //    var quotationInsCompanyLineData = unitOfWork.TblQuotationDetailsInsCompanyLineRepository.Get(x => x.QuotationDetailsInsCompanyHeaderID == quoteInsCompanyHeader.QuotationDetailsInsCompanyHeaderID).ToList();

                            //    List<QuotationDetailsInsCompanyLineVM> quotationDetailsInsCompanyLineList = new List<QuotationDetailsInsCompanyLineVM>();

                            //    foreach (var quoteInsCompanyLine in quotationInsCompanyLineData)
                            //    {
                            //        QuotationDetailsInsCompanyLineVM quotationDetailsInsCompanyLineVM = new QuotationDetailsInsCompanyLineVM();
                            //        quotationDetailsInsCompanyLineVM.QuotationDetailsInsCompanyLineID = quoteInsCompanyLine.QuotationDetailsInsCompanyLineID;
                            //        quotationDetailsInsCompanyLineVM.InsuranceSubClassID = quoteInsCompanyLine.InsSubClassID != null ? Convert.ToInt32(quoteInsCompanyLine.InsSubClassID) : 0;

                            //        if (quotationDetailsInsCompanyLineVM.InsuranceSubClassID > 0)
                            //        {
                            //            quotationDetailsInsCompanyLineVM.InsuranceSubClassDescription = quoteInsCompanyLine.tblInsSubClass.Description;
                            //        }

                            //        quotationDetailsInsCompanyLineVM.SumInsured = quoteInsCompanyLine.SumInsured != null ? Convert.ToDecimal(quoteInsCompanyLine.SumInsured) : 0;
                            //        quotationDetailsInsCompanyLineVM.CreatedBy = quoteInsCompanyLine.CreatedBy != null ? Convert.ToInt32(quoteInsCompanyLine.CreatedBy) : 0;
                            //        quotationDetailsInsCompanyLineVM.CreatedDate = quoteInsCompanyLine.CreatedDate != null ? quoteInsCompanyLine.CreatedDate.ToString() : string.Empty;
                            //        quotationDetailsInsCompanyLineVM.ModifiedBy = quoteInsCompanyLine.ModifiedBy != null ? Convert.ToInt32(quoteInsCompanyLine.ModifiedBy) : 0;
                            //        quotationDetailsInsCompanyLineVM.ModifiedDate = quoteInsCompanyLine.ModifiedDate != null ? quoteInsCompanyLine.ModifiedDate.ToString() : string.Empty;

                            //        //Quotation Details Insurance Company Scope Details
                            //        //var quotationInsCompanyScopeData = unitOfWork.TblQuotationDetailsInsCompanyScopeRepository.Get(x => x.QuotationDetailsInsCompanyLineID == quoteInsCompanyLine.QuotationDetailsInsCompanyLineID).ToList();

                            //        //List<QuotationDetailsInsCompanyScopeVM> quotationDetailsInsCompanyScopeList = new List<QuotationDetailsInsCompanyScopeVM>();

                            //        //foreach (var quoteInsCompanyScope in quotationInsCompanyScopeData)
                            //        //{
                            //        //    QuotationDetailsInsCompanyScopeVM quotationDetailsInsCompanyScopeVM = new QuotationDetailsInsCompanyScopeVM();
                            //        //    quotationDetailsInsCompanyScopeVM.QuotationDetailsInsCompanyScopeID = quoteInsCompanyScope.QuotationDetailsInsCompanyScopeID;
                            //        //    quotationDetailsInsCompanyScopeVM.ScopeDescription = quoteInsCompanyScope.ScopeDescription;
                            //        //    quotationDetailsInsCompanyScopeVM.ExcessType = quoteInsCompanyScope.ExcessType;
                            //        //    quotationDetailsInsCompanyScopeVM.ExcessAmount = quoteInsCompanyScope.ExcessAmount != null ? Convert.ToDecimal(quoteInsCompanyScope.ExcessAmount) : 0;
                            //        //    quotationDetailsInsCompanyScopeVM.CreatedBy = quoteInsCompanyScope.CreatedBy != null ? Convert.ToInt32(quoteInsCompanyScope.CreatedBy) : 0;
                            //        //    quotationDetailsInsCompanyScopeVM.CreatedDate = quoteInsCompanyScope.CreatedDate != null ? quoteInsCompanyScope.CreatedDate.ToString() : string.Empty;
                            //        //    quotationDetailsInsCompanyScopeVM.ModifiedBy = quoteInsCompanyScope.ModifiedBy != null ? Convert.ToInt32(quoteInsCompanyScope.ModifiedBy) : 0;
                            //        //    quotationDetailsInsCompanyScopeVM.ModifiedDate = quoteInsCompanyScope.ModifiedDate != null ? quoteInsCompanyScope.ModifiedDate.ToString() : string.Empty;

                            //        //    quotationDetailsInsCompanyScopeList.Add(quotationDetailsInsCompanyScopeVM);
                            //        //}

                            //        quotationDetailsInsCompanyLineVM.QuotationDetailsInsCompanyScopeDetails = quotationDetailsInsCompanyScopeList;

                            //        quotationDetailsInsCompanyLineList.Add(quotationDetailsInsCompanyLineVM);
                            //    }

                            //    quotationDetailsInsCompanyHeaderVM.QuotationDetailsInsCompanyLineDetails = quotationDetailsInsCompanyLineList;

                            //    quotationDetailsInsCompanyHeaderList.Add(quotationDetailsInsCompanyHeaderVM);
                            //}
                            #endregion
                            //quotationRequestedInsCompanyVM.QuotationDetailsInsCompanyHeaderDetails = quotationDetailsInsCompanyHeaderList;

                            requestedInsCompanyList.Add(quotationRequestedInsCompanyVM);
                        }

                        quotationLineVM.RequestedInsuranceCompanyDetails = requestedInsCompanyList;

                        quotationLineList.Add(quotationLineVM);
                    }

                    quotationHeaderVM.QuotationLineDetails = quotationLineList;

                    quotationHeaderList.Add(quotationHeaderVM);
                }

                return quotationHeaderList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<QuotationRequestedInsCompanyVM> GetQuotationHeaderInsuranceCompanyByID(int quotationHeaderID)
        {
            try
            {
                using (var db = new PERFECTIBSEntities())
                {
                    var qry = (from ct in db.tblQuotationRequestedInsCompanies
                               join w in db.tblQuotationDetailsInsCompanyHeaders on ct.QuotationRequestedInsCompanyID equals w.QuotationRequestedInsCompanyID
                               join k in db.tblQuotationDetailsInsCompanyLines on w.QuotationDetailsInsCompanyHeaderID equals k.QuotationDetailsInsCompanyHeaderID
                               where k.QuotationDetailsInsCompanyLineID == quotationHeaderID
                               select ct).ToList();

                    var quotationHeaderData = qry;
                    List<QuotationRequestedInsCompanyVM> quotationRequestedInsList = new List<QuotationRequestedInsCompanyVM>();
                    
                    foreach (var reqInsCompany in quotationHeaderData)
                    {
                        QuotationRequestedInsCompanyVM quotationRequestedInsCompanyVM = new QuotationRequestedInsCompanyVM();
                        quotationRequestedInsCompanyVM.QuotationRequestedInsCompanyID = reqInsCompany.QuotationRequestedInsCompanyID;
                        quotationRequestedInsCompanyVM.InsuranceCompanyID = reqInsCompany.InsCompanyID != null ? Convert.ToInt32(reqInsCompany.InsCompanyID) : 0;

                        if (quotationRequestedInsCompanyVM.InsuranceCompanyID > 0)
                        {
                            quotationRequestedInsCompanyVM.InsuranceCompanyName = reqInsCompany.tblInsCompany.InsCompany;
                            quotationRequestedInsCompanyVM.InsuranceCompanyEmail = reqInsCompany.tblInsCompany.Email;
                        }

                        quotationRequestedInsCompanyVM.Status = reqInsCompany.Status;
                        quotationRequestedInsCompanyVM.CreatedBy = reqInsCompany.CreatedBy != null ? Convert.ToInt32(reqInsCompany.CreatedBy) : 0;
                        quotationRequestedInsCompanyVM.CreatedDate = reqInsCompany.CreatedDate != null ? reqInsCompany.CreatedDate.ToString() : string.Empty;
                        quotationRequestedInsCompanyVM.ModifiedBy = reqInsCompany.ModifiedBy != null ? Convert.ToInt32(reqInsCompany.ModifiedBy) : 0;
                        quotationRequestedInsCompanyVM.ModifiedDate = reqInsCompany.ModifiedDate != null ? reqInsCompany.ModifiedDate.ToString() : string.Empty;


                        quotationRequestedInsList.Add(quotationRequestedInsCompanyVM);
                    }

                    return quotationRequestedInsList;

                }



            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public QuotationHeaderVM GetQuotationHeaderByID(int quotationHeaderID)
        {
            try
            {
                var quotationHeaderData = unitOfWork.TblQuotationHeaderRepository.GetByID(quotationHeaderID);

                QuotationHeaderVM quotationHeaderVM = new QuotationHeaderVM();
                quotationHeaderVM.QuotationHeaderID = quotationHeaderData.QuotationHeaderID;
                quotationHeaderVM.ClientRequestHeaderID = quotationHeaderData.ClientRequestHeaderID != null ? Convert.ToInt32(quotationHeaderData.ClientRequestHeaderID) : 0;

                if (quotationHeaderVM.ClientRequestHeaderID > 0)
                {
                    quotationHeaderVM.ClientID = quotationHeaderData.tblClientRequestHeader.ClientID != null ? Convert.ToInt32(quotationHeaderData.tblClientRequestHeader.ClientID) : 0;

                    if (quotationHeaderVM.ClientID > 0)
                    {
                        quotationHeaderVM.ClientName = quotationHeaderData.tblClientRequestHeader.tblClient.ClientName;
                    }

                    quotationHeaderVM.PartnerID = quotationHeaderData.tblClientRequestHeader.PartnerID != null ? Convert.ToInt32(quotationHeaderData.tblClientRequestHeader.PartnerID) : 0;

                    if (quotationHeaderVM.PartnerID > 0)
                    {
                        quotationHeaderVM.PartnerName = "Partner Name";//quotationHeaderData.tblClientRequestHeader.tblPartner.PartnerName;
                    }

                    quotationHeaderVM.RequestedDate = quotationHeaderData.tblClientRequestHeader.RequestedDate != null ? Convert.ToDateTime(quotationHeaderData.tblClientRequestHeader.RequestedDate).ToString("dd/MM/yyyy") : string.Empty;
                }

                quotationHeaderVM.Status = quotationHeaderData.Status;
                quotationHeaderVM.QuotationStatusCode = quotationHeaderData.QuotationStatusCode;

                if (!string.IsNullOrEmpty(quotationHeaderVM.QuotationStatusCode))
                {
                    quotationHeaderVM.QuotationStatusCodeDescription = unitOfWork.TblQuotationStatusRepository.Get(x => x.QuotationStatusCode == quotationHeaderVM.QuotationStatusCode).SingleOrDefault().Description;
                }

                quotationHeaderVM.CreatedBy = quotationHeaderData.CreatedBy != null ? Convert.ToInt32(quotationHeaderData.CreatedBy) : 0;
                quotationHeaderVM.CreatedDate = quotationHeaderData.CreatedDate != null ? quotationHeaderData.CreatedDate.ToString() : string.Empty;
                quotationHeaderVM.ModifiedBy = quotationHeaderData.ModifiedBy != null ? Convert.ToInt32(quotationHeaderData.ModifiedBy) : 0;
                quotationHeaderVM.ModifiedDate = quotationHeaderData.ModifiedDate != null ? quotationHeaderData.ModifiedDate.ToString() : string.Empty;

                //Quotation Line Details
                var quotationLineData = unitOfWork.TblQuotationLineRepository.Get(x => x.QuotationHeaderID == quotationHeaderData.QuotationHeaderID).ToList();

                List<QuotationLineVM> quotationLineList = new List<QuotationLineVM>();

                foreach (var quotationLine in quotationLineData)
                {
                    QuotationLineVM quotationLineVM = new QuotationLineVM();
                    quotationLineVM.QuotationLineID = quotationLine.QuotationLineID;
                    quotationLineVM.InsuranceClassID = quotationLine.InsClassID != null ? Convert.ToInt32(quotationLine.InsClassID) : 0;

                    if (quotationLineVM.InsuranceClassID > 0)
                    {
                        quotationLineVM.InsuranceCode = quotationLine.tblInsClass.Code;
                    }

                    quotationLineVM.InsuranceSubClassID = quotationLine.InsSubClassID != null ? Convert.ToInt32(quotationLine.InsSubClassID) : 0;

                    if (quotationLineVM.InsuranceSubClassID > 0)
                    {
                        quotationLineVM.InsuranceSubClassDescription = quotationLine.tblInsSubClass.Description;
                    }

                    quotationLineVM.CreatedBy = quotationLine.CreatedBy != null ? Convert.ToInt32(quotationLine.CreatedBy) : 0;
                    quotationLineVM.CreatedDate = quotationLine.CreatedDate != null ? quotationLine.CreatedDate.ToString() : string.Empty;
                    quotationLineVM.ModifiedBy = quotationLine.ModifiedBy != null ? Convert.ToInt32(quotationLine.ModifiedBy) : 0;
                    quotationLineVM.ModifiedDate = quotationLine.ModifiedDate != null ? quotationLine.ModifiedDate.ToString() : string.Empty;

                    //Requested Insurance Company Details
                    var reqInsCompanyData = unitOfWork.TblQuotationRequestedInsCompanyRepository.Get(x => x.QuotationLineID == quotationLine.QuotationLineID).ToList();

                    List<QuotationRequestedInsCompanyVM> requestedInsCompanyList = new List<QuotationRequestedInsCompanyVM>();

                    foreach (var reqInsCompany in reqInsCompanyData)
                    {
                        QuotationRequestedInsCompanyVM quotationRequestedInsCompanyVM = new QuotationRequestedInsCompanyVM();
                        quotationRequestedInsCompanyVM.QuotationRequestedInsCompanyID = reqInsCompany.QuotationRequestedInsCompanyID;
                        quotationRequestedInsCompanyVM.InsuranceCompanyID = reqInsCompany.InsCompanyID != null ? Convert.ToInt32(reqInsCompany.InsCompanyID) : 0;

                        if (quotationRequestedInsCompanyVM.InsuranceCompanyID > 0)
                        {
                            quotationRequestedInsCompanyVM.InsuranceCompanyName = reqInsCompany.tblInsCompany.InsCompany;
                            quotationRequestedInsCompanyVM.InsuranceCompanyEmail = reqInsCompany.tblInsCompany.Email;
                        }

                        quotationRequestedInsCompanyVM.Status = reqInsCompany.Status;
                        quotationRequestedInsCompanyVM.CreatedBy = reqInsCompany.CreatedBy != null ? Convert.ToInt32(reqInsCompany.CreatedBy) : 0;
                        quotationRequestedInsCompanyVM.CreatedDate = reqInsCompany.CreatedDate != null ? reqInsCompany.CreatedDate.ToString() : string.Empty;
                        quotationRequestedInsCompanyVM.ModifiedBy = reqInsCompany.ModifiedBy != null ? Convert.ToInt32(reqInsCompany.ModifiedBy) : 0;
                        quotationRequestedInsCompanyVM.ModifiedDate = reqInsCompany.ModifiedDate != null ? reqInsCompany.ModifiedDate.ToString() : string.Empty;

                        //Quotation Details Insurance Company Header Details
                        var quotationInsCompanyHeaderData = unitOfWork.TblQuotationDetailsInsCompanyHeaderRepository.Get(x => x.QuotationRequestedInsCompanyID == reqInsCompany.QuotationRequestedInsCompanyID).ToList();

                        List<QuotationDetailsInsCompanyHeaderVM> quotationDetailsInsCompanyHeaderList = new List<QuotationDetailsInsCompanyHeaderVM>();

                        foreach (var quoteInsCompanyHeader in quotationInsCompanyHeaderData)
                        {
                            QuotationDetailsInsCompanyHeaderVM quotationDetailsInsCompanyHeaderVM = new QuotationDetailsInsCompanyHeaderVM();
                            quotationDetailsInsCompanyHeaderVM.QuotationDetailsInsCompanyHeaderID = quoteInsCompanyHeader.QuotationDetailsInsCompanyHeaderID;
                            quotationDetailsInsCompanyHeaderVM.PremiumIncludingTax = quoteInsCompanyHeader.PremiumIncludingTax != null ? Convert.ToDouble(quoteInsCompanyHeader.PremiumIncludingTax) : 0;
                            quotationDetailsInsCompanyHeaderVM.ExcessDescription = quoteInsCompanyHeader.ExcessDescription;
                            quotationDetailsInsCompanyHeaderVM.ExcessAmount = quoteInsCompanyHeader.ExcessAmount != null ? Convert.ToDecimal(quoteInsCompanyHeader.ExcessAmount) : 0;
                            quotationDetailsInsCompanyHeaderVM.CreatedBy = quoteInsCompanyHeader.CreatedBy != null ? Convert.ToInt32(quoteInsCompanyHeader.CreatedBy) : 0;
                            quotationDetailsInsCompanyHeaderVM.CreatedDate = quoteInsCompanyHeader.CreatedDate != null ? quoteInsCompanyHeader.CreatedDate.ToString() : string.Empty;
                            quotationDetailsInsCompanyHeaderVM.ModifiedBy = quoteInsCompanyHeader.ModifiedBy != null ? Convert.ToInt32(quoteInsCompanyHeader.ModifiedBy) : 0;
                            quotationDetailsInsCompanyHeaderVM.ModifiedDate = quoteInsCompanyHeader.ModifiedDate != null ? quoteInsCompanyHeader.ModifiedDate.ToString() : string.Empty;

                            //Quotation Details Insurance Company Line Details
                            var quotationInsCompanyLineData = unitOfWork.TblQuotationDetailsInsCompanyLineRepository.Get(x => x.QuotationDetailsInsCompanyHeaderID == quoteInsCompanyHeader.QuotationDetailsInsCompanyHeaderID).ToList();


                            List<QuotationDetailsInsCompanyLineVM> quotationDetailsInsCompanyLineList = new List<QuotationDetailsInsCompanyLineVM>();

                            foreach (var quoteInsCompanyLine in quotationInsCompanyLineData)
                            {
                                QuotationDetailsInsCompanyLineVM quotationDetailsInsCompanyLineVM = new QuotationDetailsInsCompanyLineVM();
                                quotationDetailsInsCompanyLineVM.QuotationDetailsInsCompanyLineID = quoteInsCompanyLine.QuotationDetailsInsCompanyLineID;
                                quotationDetailsInsCompanyLineVM.InsuranceSubClassID = quoteInsCompanyLine.InsSubClassID != null ? Convert.ToInt32(quoteInsCompanyLine.InsSubClassID) : 0;

                                if (quotationDetailsInsCompanyLineVM.InsuranceSubClassID > 0)
                                {
                                    quotationDetailsInsCompanyLineVM.InsuranceSubClassDescription = quoteInsCompanyLine.tblInsSubClass.Description;
                                }

                                quotationDetailsInsCompanyLineVM.SumInsured = quoteInsCompanyLine.SumInsured != null ? Convert.ToDecimal(quoteInsCompanyLine.SumInsured) : 0;
                                quotationDetailsInsCompanyLineVM.CreatedBy = quoteInsCompanyLine.CreatedBy != null ? Convert.ToInt32(quoteInsCompanyLine.CreatedBy) : 0;
                                quotationDetailsInsCompanyLineVM.CreatedDate = quoteInsCompanyLine.CreatedDate != null ? quoteInsCompanyLine.CreatedDate.ToString() : string.Empty;
                                quotationDetailsInsCompanyLineVM.ModifiedBy = quoteInsCompanyLine.ModifiedBy != null ? Convert.ToInt32(quoteInsCompanyLine.ModifiedBy) : 0;
                                quotationDetailsInsCompanyLineVM.ModifiedDate = quoteInsCompanyLine.ModifiedDate != null ? quoteInsCompanyLine.ModifiedDate.ToString() : string.Empty;

                                //Quotation Details Insurance Company Scope Details
                                var quotationInsCompanyScopeData = unitOfWork.TblQuotationDetailsInsCompanyScopeRepository.Get(x => x.QuotationDetailsInsCompanyLineID == quoteInsCompanyLine.QuotationDetailsInsCompanyLineID).ToList();

                                List<QuotationDetailsInsCompanyScopeVM> quotationDetailsInsCompanyScopeList = new List<QuotationDetailsInsCompanyScopeVM>();

                                foreach (var quoteInsCompanyScope in quotationInsCompanyScopeData)
                                {
                                    QuotationDetailsInsCompanyScopeVM quotationDetailsInsCompanyScopeVM = new QuotationDetailsInsCompanyScopeVM();
                                    quotationDetailsInsCompanyScopeVM.QuotationDetailsInsCompanyScopeID = quoteInsCompanyScope.QuotationDetailsInsCompanyScopeID;
                                    quotationDetailsInsCompanyScopeVM.ScopeDescription = quoteInsCompanyScope.ScopeDescription;
                                    quotationDetailsInsCompanyScopeVM.ExcessType = quoteInsCompanyScope.ExcessType;
                                    quotationDetailsInsCompanyScopeVM.ExcessAmount = quoteInsCompanyScope.ExcessAmount != null ? Convert.ToDecimal(quoteInsCompanyScope.ExcessAmount) : 0;
                                    quotationDetailsInsCompanyScopeVM.CreatedBy = quoteInsCompanyScope.CreatedBy != null ? Convert.ToInt32(quoteInsCompanyScope.CreatedBy) : 0;
                                    quotationDetailsInsCompanyScopeVM.CreatedDate = quoteInsCompanyScope.CreatedDate != null ? quoteInsCompanyScope.CreatedDate.ToString() : string.Empty;
                                    quotationDetailsInsCompanyScopeVM.ModifiedBy = quoteInsCompanyScope.ModifiedBy != null ? Convert.ToInt32(quoteInsCompanyScope.ModifiedBy) : 0;
                                    quotationDetailsInsCompanyScopeVM.ModifiedDate = quoteInsCompanyScope.ModifiedDate != null ? quoteInsCompanyScope.ModifiedDate.ToString() : string.Empty;

                                    quotationDetailsInsCompanyScopeList.Add(quotationDetailsInsCompanyScopeVM);
                                }

                                quotationDetailsInsCompanyLineVM.QuotationDetailsInsCompanyScopeDetails = quotationDetailsInsCompanyScopeList;

                                quotationDetailsInsCompanyLineList.Add(quotationDetailsInsCompanyLineVM);
                            }

                            quotationDetailsInsCompanyHeaderVM.QuotationDetailsInsCompanyLineDetails = quotationDetailsInsCompanyLineList;

                            quotationDetailsInsCompanyHeaderList.Add(quotationDetailsInsCompanyHeaderVM);


                        }

                        quotationRequestedInsCompanyVM.QuotationDetailsInsCompanyHeaderDetails = quotationDetailsInsCompanyHeaderList;

                        requestedInsCompanyList.Add(quotationRequestedInsCompanyVM);
                    }

                    quotationLineVM.RequestedInsuranceCompanyDetails = requestedInsCompanyList;

                    quotationLineList.Add(quotationLineVM);
                }

                quotationHeaderVM.QuotationLineDetails = quotationLineList;

                return quotationHeaderVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string UpdateQuotationStatus(int quotationHeaderID, string quotationStatusCode, int userID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    string statusCode = string.Empty;

                    if (QuotationStatusCodeEnum.CA.ToString().ToLower() == quotationStatusCode.ToLower())
                    {
                        statusCode = QuotationStatusCodeEnum.CA.ToString();
                    }
                    else if (QuotationStatusCodeEnum.NA.ToString().ToLower() == quotationStatusCode.ToLower())
                    {
                        statusCode = QuotationStatusCodeEnum.NA.ToString();
                    }
                    else if (QuotationStatusCodeEnum.QNC.ToString().ToLower() == quotationStatusCode.ToLower())
                    {
                        statusCode = QuotationStatusCodeEnum.QNC.ToString();
                    }
                    else if (QuotationStatusCodeEnum.QP.ToString().ToLower() == quotationStatusCode.ToLower())
                    {
                        statusCode = QuotationStatusCodeEnum.QP.ToString();
                    }
                    else if (QuotationStatusCodeEnum.QR.ToString().ToLower() == quotationStatusCode.ToLower())
                    {
                        statusCode = QuotationStatusCodeEnum.QR.ToString();
                    }
                    else if (QuotationStatusCodeEnum.TCNI.ToString().ToLower() == quotationStatusCode.ToLower())
                    {
                        statusCode = QuotationStatusCodeEnum.TCNI.ToString();
                    }
                    else
                    {
                        return "IQSC";
                    }

                    var quotationHeaderData = unitOfWork.TblQuotationHeaderRepository.GetByID(quotationHeaderID);
                    quotationHeaderData.QuotationStatusCode = statusCode;
                    quotationHeaderData.ModifiedBy = userID;
                    quotationHeaderData.ModifiedDate = DateTime.Now;
                    unitOfWork.TblQuotationHeaderRepository.Update(quotationHeaderData);
                    unitOfWork.Save();

                    //Complete the Transaction
                    dbTransaction.Commit();
                    return "SUCCESS";
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                    return "UF";
                }
            }
        }

        public List<QuoteInfoInsCompanyLineVM> GetQuoteInfoInsCompanyLineDetailsByQuotation(int quotationHeaderID)
        {
            try
            {
                var quoteDetailsInsCompanyLineData = unitOfWork.TblQuotationDetailsInsCompanyLineRepository.Get(x => x.tblQuotationDetailsInsCompanyHeader.tblQuotationRequestedInsCompany.tblQuotationLine.QuotationHeaderID == quotationHeaderID);

                List<QuoteInfoInsCompanyLineVM> qauoteInfoInsCompanyLineList = new List<QuoteInfoInsCompanyLineVM>();

                foreach (var insCompanyLineData in quoteDetailsInsCompanyLineData)
                {
                    QuoteInfoInsCompanyLineVM quoteInfoInsCompanyLineVM = new QuoteInfoInsCompanyLineVM();
                    quoteInfoInsCompanyLineVM.QuoteInfoInsCompanyLineID = insCompanyLineData.QuotationDetailsInsCompanyLineID;
                    quoteInfoInsCompanyLineVM.InsSubClassName = insCompanyLineData.tblInsSubClass.Description;
                    quoteInfoInsCompanyLineVM.InsuranceCompanyName = insCompanyLineData.tblQuotationDetailsInsCompanyHeader.tblQuotationRequestedInsCompany.tblInsCompany.InsCompany;

                    qauoteInfoInsCompanyLineList.Add(quoteInfoInsCompanyLineVM);
                }

                return qauoteInfoInsCompanyLineList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<PremiumVM> GetPremium()
        {
            try
            {
                var priumData = unitOfWork.TblPremiumRepository.Get().ToList();

                List<PremiumVM> PremiumList = new List<PremiumVM>();

                foreach (var premium in priumData)
                {
                    PremiumVM loadingTypeVM = new PremiumVM();
                    loadingTypeVM.PremiumID = premium.PremiumID;
                    loadingTypeVM.PremiumName = premium.PremiumName;
                    loadingTypeVM.BUID =(int) premium.BUID;
                    //loadingTypeVM.CreatedBy = premium.CreatedBy != null ? Convert.ToInt32(premium.CreatedBy) : 0;
                    //loadingTypeVM.CreatedDate = premium.CreatedDate != null ? premium.CreatedDate.ToString() : string.Empty;
                    //loadingTypeVM.ModifiedBy = premium.ModifiedBy != null ? Convert.ToInt32(premium.ModifiedBy) : 0;
                    //loadingTypeVM.ModifiedDate = premium.ModifiedDate != null ? premium.ModifiedDate.ToString() : string.Empty;

                    PremiumList.Add(loadingTypeVM);
                }

                return PremiumList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PremiumVM> GetPremiumByBUID(string  BUID)
        {
            try
            {

                var buid = int.Parse(BUID);
                var priumData = unitOfWork.TblPremiumRepository.Get(x=>x.BUID== buid).ToList();

                List<PremiumVM> PremiumList = new List<PremiumVM>();

                foreach (var premium in priumData)
                {
                    PremiumVM loadingTypeVM = new PremiumVM();
                    loadingTypeVM.PremiumID = premium.PremiumID;
                    loadingTypeVM.PremiumName = premium.PremiumName;
                    loadingTypeVM.BUID = (int)premium.BUID;
                    //loadingTypeVM.CreatedBy = premium.CreatedBy != null ? Convert.ToInt32(premium.CreatedBy) : 0;
                    //loadingTypeVM.CreatedDate = premium.CreatedDate != null ? premium.CreatedDate.ToString() : string.Empty;
                    //loadingTypeVM.ModifiedBy = premium.ModifiedBy != null ? Convert.ToInt32(premium.ModifiedBy) : 0;
                    //loadingTypeVM.ModifiedDate = premium.ModifiedDate != null ? premium.ModifiedDate.ToString() : string.Empty;

                    PremiumList.Add(loadingTypeVM);
                }

                return PremiumList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<FrequncyVM> GetFrequncy()
        {
            try
            {
                //  var priumData = unitOfWork.TblPremiumRepository.Get().ToList();
                var priumData = unitOfWork.TblFrequncy.Get().ToList();
                List<FrequncyVM> FrequncyVMList = new List<FrequncyVM>();

                foreach (var premium in priumData)
                {
                    FrequncyVM loadingTypeVM = new FrequncyVM();
                    loadingTypeVM.FrequncyID= premium.ID;
                    loadingTypeVM.Frequncy = premium.Name;

                    //loadingTypeVM.CreatedBy = premium.CreatedBy != null ? Convert.ToInt32(premium.CreatedBy) : 0;
                    //loadingTypeVM.CreatedDate = premium.CreatedDate != null ? premium.CreatedDate.ToString() : string.Empty;
                    //loadingTypeVM.ModifiedBy = premium.ModifiedBy != null ? Convert.ToInt32(premium.ModifiedBy) : 0;
                    //loadingTypeVM.ModifiedDate = premium.ModifiedDate != null ? premium.ModifiedDate.ToString() : string.Empty;

                    FrequncyVMList.Add(loadingTypeVM);
                }

                return FrequncyVMList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public QuotationDetail GetQuotationDetail(int compId, int insSubClassId)
        {
            try
            {
                using (var db = new PERFECTIBSEntities())
                {
                    var qry = (from qd in db.tblQuotationLines
                               join com in db.tblInsCompanies on qd.CompID equals com.InsCompanyID
                               where qd.CompID == compId && qd.InsSubClassID == insSubClassId
                               select new QuotationDetail
                               {
                                   FileName = qd.FileName,
                                   Email = com.Email,
                               }).FirstOrDefault();
                    return qry;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tblQuotationLine GetQuotationLine(int compId, int insSubClassId, int quotationHeaderId)
        {
            try
            {
                using (var db = new PERFECTIBSEntities())
                {
                    var qry = (from qd in db.tblQuotationLines
                                   //join com in db.tblInsCompanies on qd.CompID equals com.InsCompanyID
                               where qd.CompID == compId && qd.InsSubClassID == insSubClassId && qd.QuotationHeaderID == quotationHeaderId
                               select qd).FirstOrDefault();
                    return qry;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateQuotationLine(tblQuotationLine quotationLine)
        {
            try
            {
                List<QuotationDetail> qd = new List<QuotationDetail>();
                using (var db = new PERFECTIBSEntities())
                {
                    db.Entry(quotationLine).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<QuatationDetail> GetQuotationLineDetailByHeaderId(int quotationHeaderId)
        {
            try
            {
                using (var db = new PERFECTIBSEntities())
                {
                    var qry = (
                        from qd in db.tblQuotationLines
                        where qd.QuotationHeaderID == quotationHeaderId
                        select new QuatationDetail
                        {
                            InsClassID = qd.InsClassID,
                            CompID = qd.CompID,
                            FileName = qd.FileName,
                            FilePath = qd.FilePath,
                            IsRequested = qd.IsRequested,
                            RequestedDate = qd.RequestedDate,
                            InsSubClassID = qd.InsSubClassID,
                            IsReceived = qd.IsReceived,
                            ReceivedDate = qd.ReceivedDate,
                        }).ToList();

                    return qry;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ReceiveQuotation(ReceiveQuotationVM quotation)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                tblReceivedQuotation quot = new tblReceivedQuotation();

                quot.ClassId = quotation.ClassId;
                quot.CompanyId = quotation.CompanyId;
                quot.FileName = quotation.FileName.Trim();
                quot.FilePath = quotation.FilePath.Trim();
                quot.QuotationRequestDetailId = 1;
                quot.QuotationRequestHeaderId = quotation.QuotationHeaderId;
                quot.ReceivedDate = quotation.ReceivedDate;
                quot.ReceivedUser = quotation.ReceivedUser;
                quot.SubClassId = quotation.SubClassId;

                unitOfWork.TblReceivedQuotationRepository.Insert(quot);
                unitOfWork.Save();

                ReceiveQuotationHeader(quot.QuotationRequestHeaderId);


                dbTransaction.Commit();
                return true;
            }
        }

        public bool ReceiveQuotationHeader(int quotationHeaderId)
        {
            try
            {
                using (var db = new PERFECTIBSEntities())
                {
                    var quotationHeader = db.tblQuotationHeaders.Where(q => q.QuotationHeaderID == quotationHeaderId).FirstOrDefault();
                    quotationHeader.IsReceived = true;
                    db.Entry(quotationHeader).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string GetReceivedDocument(int quotationHeaderId)
        {
            using (var db = new PERFECTIBSEntities())
            {
                var qry = db.tblReceivedQuotations.Where(q => q.QuotationRequestHeaderId == quotationHeaderId).FirstOrDefault();

                return qry == null ? "" : qry.FileName;
            }
        }
    }
}
