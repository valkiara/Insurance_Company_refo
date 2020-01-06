using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.TransactionData
{
    public class ManagePolicyInfoRecording
    {
        private UnitOfWork unitOfWork;
        public ManagePolicyInfoRecording()
        {
            unitOfWork = new UnitOfWork();
        }

        #region Policy Info Recording
        //public bool SavePolicyInfoRecording(int quotationHeaderID, List<PolicyInfoRecNewVM> policyInfoRecVMList,List<PolicyNewCommissionPaymentVM> policyQuotationNewCommissionPaymentVM, List<PolicyInfoChargeVM> chargeTypeList, int userID)
        //{
        //    using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            foreach (var policyInfoRecVM in policyInfoRecVMList)
        //            {

        //                if (policyInfoRecVM.TransactionTypeID == 1039)
        //                {
        //                }
        //                else
        //                {

        //                    //Save Policy Information Recording Details
        //                    tblPolicyInformationRecording policyInfoRecording = new tblPolicyInformationRecording();
        //                    policyInfoRecording.PolicyNumber = policyInfoRecVM.PolicyNumber;
        //                    policyInfoRecording.QuotationHeaderID = quotationHeaderID;
        //                    policyInfoRecording.QuotationDetailsInsCompanyLineID = policyInfoRecVM.QuotationDetailsInsCompanyLineID;
        //                    policyInfoRecording.SumAssured = policyInfoRecVM.SumAssured;
        //                    policyInfoRecording.SumAssuredCurrencyTypeID = policyInfoRecVM.SumAssuredCurrencyTypeID;
        //                    policyInfoRecording.PremiumIncludingTax = policyInfoRecVM.PremiumIncludingTax;
        //                    policyInfoRecording.PremiumIncludingTaxCurrencyTypeID = policyInfoRecVM.PremiumIncludingTaxCurrencyTypeID;
        //                    policyInfoRecording.PeriodOfCoverFromDate = !string.IsNullOrEmpty(policyInfoRecVM.PeriodOfCoverFromDate) ? DateTime.ParseExact(policyInfoRecVM.PeriodOfCoverFromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
        //                    policyInfoRecording.PeriodOfCoverToDate = !string.IsNullOrEmpty(policyInfoRecVM.PeriodOfCoverToDate) ? DateTime.ParseExact(policyInfoRecVM.PeriodOfCoverToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
        //                    policyInfoRecording.OtherExcessDescription = policyInfoRecVM.OtherExcessDescription;
        //                    policyInfoRecording.NonCommissionPremium = policyInfoRecVM.NonCommissionPremium;
        //                    policyInfoRecording.GrossPremium = policyInfoRecVM.GrossPremium;
        //                    policyInfoRecording.OtherExcessAmount = policyInfoRecVM.OtherExcessAmount;
        //                    policyInfoRecording.VehicleNumber = policyInfoRecVM.VehicleNumber;
        //                    policyInfoRecording.TransactionTypeID = policyInfoRecVM.TransactionTypeID;
        //                    policyInfoRecording.CommissionStructureHeaderID = policyInfoRecVM.CommissionStructureHeaderID;
        //                    policyInfoRecording.IntroducerID = policyInfoRecVM.IntroducerID;
        //                    policyInfoRecording.AccountExecutiveID = policyInfoRecVM.AccountExecutiveID;
        //                    policyInfoRecording.TotalCommission = policyInfoRecVM.TotalCommission;
        //                    policyInfoRecording.OtherExcessAmount = policyInfoRecVM.OtherExcessAmount;
        //                    policyInfoRecording.TaxInvoiceNumber = policyInfoRecVM.TaxInvoiceNumber;
        //                    policyInfoRecording.FileNumber = policyInfoRecVM.FileNumber;
        //                    policyInfoRecording.IsActive = policyInfoRecVM.IsActive;
        //                    policyInfoRecording.CreatedDate = DateTime.Now;
        //                    policyInfoRecording.CreatedBy = userID;
        //                    policyInfoRecording.InuranceCompanyId = policyInfoRecVM.InsuranceCompanyID;
        //                    unitOfWork.TblPolicyInformationRecordingRepository.Insert(policyInfoRecording);
        //                    unitOfWork.Save();

        //                    //Save Policy Commission Payment Details
        //                    foreach (var policyCommissionPayment in policyQuotationNewCommissionPaymentVM)
        //                    {
        //                        ////Saving charge type and commission as commision details
        //                        //tblPolicyCommisionPayment policyCommisionPayment = new tblPolicyCommisionPayment();
        //                        //policyCommisionPayment.PolicyInfoRecID = policyInfoRecording.PolicyInfoRecID;
        //                        //policyCommisionPayment.CommisionTypeID = policyCommissionPayment.ComHeaderID;
        //                        //policyCommisionPayment.CommisionValue = policyCommissionPayment.ComHeaderID;
        //                        //policyCommisionPayment.ComStructLineID = policyCommissionPayment.ChargeTypeID;
        //                        //policyCommisionPayment.Amount = policyCommissionPayment.Amount;
        //                        //policyCommisionPayment.CreatedDate = DateTime.Now;
        //                        //policyCommisionPayment.CreatedBy = userID;
        //                        //unitOfWork.TblPolicyCommisionPaymentRepository.Insert(policyCommisionPayment);
        //                        //unitOfWork.Save();
                               


        //                        tblPolicyCommisionPayment policyCommisionPayment = new tblPolicyCommisionPayment();
        //                        policyCommisionPayment.PolicyInfoRecID = policyInfoRecording.PolicyInfoRecID;
        //                        policyCommisionPayment.CommisionTypeID = policyCommissionPayment.CommisionTypeID;
        //                        policyCommisionPayment.CommisionValue = policyCommissionPayment.Amount;
        //                        policyCommisionPayment.CreatedDate = DateTime.Now;
        //                        policyCommisionPayment.CreatedBy = userID;
        //                        unitOfWork.TblPolicyCommisionPaymentRepository.Insert(policyCommisionPayment);
        //                        unitOfWork.Save();




        //                    }

        //                    foreach (var policyInfoChargeVM in chargeTypeList)
        //                    {
        //                        tblPolicyInfoCharge policyInfoCharge = new tblPolicyInfoCharge();
        //                        policyInfoCharge.PolicyInfoPaymentID = policyInfoRecording.PolicyInfoRecID;
        //                        policyInfoCharge.ChargeTypeID = policyInfoChargeVM.ChargeTypeID;
        //                        policyInfoCharge.Amount = policyInfoChargeVM.Amount;
        //                        policyInfoCharge.ComHeaderID = policyInfoChargeVM.ComHeaderID;
        //                        policyInfoCharge.IsCR = policyInfoChargeVM.IsCR;
        //                        policyInfoCharge.CreatedBy = userID;
        //                        policyInfoCharge.CreatedDate = DateTime.Now;
        //                        unitOfWork.TblPolicyInfoChargeRepository.Insert(policyInfoCharge);
        //                        unitOfWork.Save();
        //                    }
        //                }
        //            }

        //            //Update Quotation Status Code
        //            tblQuotationHeader quotationHeader = unitOfWork.TblQuotationHeaderRepository.GetByID(quotationHeaderID);

        //            if (quotationHeader.QuotationStatusCode != QuotationStatusCodeEnum.TCNI.ToString())
        //            {
        //                quotationHeader.QuotationStatusCode = QuotationStatusCodeEnum.TCNI.ToString();
        //                unitOfWork.TblQuotationHeaderRepository.Update(quotationHeader);
        //                unitOfWork.Save();
        //            }

        //            //Complete the Transaction
        //            dbTransaction.Commit();
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            //Roll back the Transaction
        //            dbTransaction.Rollback();
        //            return false;
        //        }
        //    }
        //}

         //31-12-2018 Commited 
        
        public bool SavePolicyInfoRecording(int quotationHeaderID, List<PolicyInfoRecNewVM> policyInfoRecList, int userID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    EndorsementVM endorsement = new EndorsementVM();
                    foreach (var policyInfoRecVM in policyInfoRecList)
                    {

                        if (policyInfoRecVM.TransactionTypeID == 1039)
                        {
                            
                            //Save Policy Information Recording Details
                            tblPolicyInformationRecording policyInfoRecording = new tblPolicyInformationRecording();
                            policyInfoRecording.PolicyNumber = policyInfoRecVM.PolicyNumber;
                            policyInfoRecording.QuotationHeaderID = quotationHeaderID;
                            policyInfoRecording.QuotationDetailsInsCompanyLineID = policyInfoRecVM.QuotationDetailsInsCompanyLineID;
                            policyInfoRecording.SumAssured = policyInfoRecVM.SumAssured < 0 ? 0 : policyInfoRecVM.SumAssured;
                            policyInfoRecording.SumAssuredCurrencyTypeID = policyInfoRecVM.SumAssuredCurrencyTypeID < 0 ? 0 : policyInfoRecVM.SumAssuredCurrencyTypeID;
                            policyInfoRecording.PremiumIncludingTax = policyInfoRecVM.PremiumIncludingTax < 0 ? 0 : policyInfoRecVM.PremiumIncludingTax;
                            policyInfoRecording.PremiumIncludingTaxCurrencyTypeID = policyInfoRecVM.PremiumIncludingTaxCurrencyTypeID;
                            policyInfoRecording.PeriodOfCoverFromDate = !string.IsNullOrEmpty(policyInfoRecVM.PeriodOfCoverFromDate) ? DateTime.ParseExact(policyInfoRecVM.PeriodOfCoverFromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            policyInfoRecording.PeriodOfCoverToDate = !string.IsNullOrEmpty(policyInfoRecVM.PeriodOfCoverToDate) ? DateTime.ParseExact(policyInfoRecVM.PeriodOfCoverToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            policyInfoRecording.PolicyRequestedDate = !string.IsNullOrEmpty(policyInfoRecVM.PolicyRequestedDate) ? DateTime.ParseExact(policyInfoRecVM.PeriodOfCoverToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            policyInfoRecording.OtherExcessDescription = policyInfoRecVM.OtherExcessDescription == null ? "" : policyInfoRecVM.OtherExcessDescription;
                            policyInfoRecording.NonCommissionPremium = policyInfoRecVM.NonCommissionPremium > 0 ? policyInfoRecVM.NonCommissionPremium : 0;
                            policyInfoRecording.GrossPremium = policyInfoRecVM.GrossPremium > 0 ? policyInfoRecVM.GrossPremium : 0;
                            policyInfoRecording.OtherExcessAmount = policyInfoRecVM.OtherExcessAmount > 0 ? policyInfoRecVM.OtherExcessAmount : 0;
                            policyInfoRecording.VehicleNumber = !string.IsNullOrEmpty(policyInfoRecVM.VehicleNumber) ? policyInfoRecVM.VehicleNumber : "";
                            policyInfoRecording.TransactionTypeID = policyInfoRecVM.TransactionTypeID > 0 ? policyInfoRecVM.TransactionTypeID : 0;
                            policyInfoRecording.CommissionStructureHeaderID = policyInfoRecVM.CommissionStructureHeaderID > 0 ? policyInfoRecVM.CommissionStructureHeaderID : 0;
                            policyInfoRecording.IntroducerID = policyInfoRecVM.IntroducerID > 0 ? policyInfoRecVM.IntroducerID : 0;
                            policyInfoRecording.AccountExecutiveID = policyInfoRecVM.AccountExecutiveID > 0 ? policyInfoRecVM.AccountExecutiveID : 0;
                            policyInfoRecording.TotalCommission = policyInfoRecVM.TotalCommission > 0 ? policyInfoRecVM.TotalCommission : 0;
                            policyInfoRecording.OtherExcessAmount = policyInfoRecVM.OtherExcessAmount > 0 ? policyInfoRecVM.OtherExcessAmount : 0;
                            policyInfoRecording.TaxInvoiceNumber = !string.IsNullOrEmpty(policyInfoRecVM.TaxInvoiceNumber) ? policyInfoRecVM.TaxInvoiceNumber : "";
                            policyInfoRecording.FileNumber = !string.IsNullOrEmpty(policyInfoRecVM.FileNumber) ? policyInfoRecVM.FileNumber : "";
                            policyInfoRecording.IsActive = policyInfoRecVM.IsActive;
                            policyInfoRecording.CreatedDate = DateTime.Now;
                            policyInfoRecording.CreatedBy = userID;
                            policyInfoRecording.ExchangeRate = policyInfoRecVM.CurrencyRate > 0 ? policyInfoRecVM.CurrencyRate : 0;
                            policyInfoRecording.InuranceCompanyId = policyInfoRecVM.InsuranceCompanyID > 0 ? policyInfoRecVM.InsuranceCompanyID : 0;
                            unitOfWork.TblPolicyInformationRecordingRepository.Insert(policyInfoRecording);
                            unitOfWork.Save();

                            int index = 0;
                            index = policyInfoRecording.PolicyInfoRecID;

                            #region Save Policy Commission Payment Details
                            foreach (var policyCommissionPayment in policyInfoRecVM.PolicyNewInfoCharge)
                            {
                                ////Saving charge type and commission as commision details
                                //tblPolicyCommisionPayment policyCommisionPayment = new tblPolicyCommisionPayment();
                                //policyCommisionPayment.PolicyInfoRecID = policyInfoRecording.PolicyInfoRecID;
                                //policyCommisionPayment.CommisionTypeID = policyCommissionPayment.ComHeaderID;
                                //policyCommisionPayment.CommisionValue = policyCommissionPayment.ComHeaderID;
                                //policyCommisionPayment.ComStructLineID = policyCommissionPayment.ChargeTypeID;
                                //policyCommisionPayment.Amount = policyCommissionPayment.Amount;
                                //policyCommisionPayment.CreatedDate = DateTime.Now;
                                //policyCommisionPayment.CreatedBy = userID;
                                //unitOfWork.TblPolicyCommisionPaymentRepository.Insert(policyCommisionPayment);
                                //unitOfWork.Save();



                                tblPolicyCommisionPayment_New policyCommisionPayment = new tblPolicyCommisionPayment_New();
                                policyCommisionPayment.PolicyInfoRecID = policyInfoRecording.PolicyInfoRecID;
                                policyCommisionPayment.ChargeTypeID = policyCommissionPayment.ChargeTypeID;
                                policyCommisionPayment.Amount = policyCommissionPayment.Amount > 0 ? policyCommissionPayment.Amount : 0;
                                policyCommisionPayment.CreatedDate = DateTime.Now;
                                policyCommisionPayment.CreatedBy = userID;
                                unitOfWork.TblPolicyCommisionPayment_NewRepository.Insert(policyCommisionPayment);
                                unitOfWork.Save();

                                tblEndorsementInfo EndorsementInfo = new tblEndorsementInfo();
                                EndorsementInfo.PolicyInfoRecID = index;
                                EndorsementInfo.PolicyNumber = policyInfoRecVM.PolicyNumber;
                                EndorsementInfo.NewSumInsured = policyInfoRecVM.SumAssured;
                                EndorsementInfo.CreatedBy = policyInfoRecVM.CreatedBy;
                                EndorsementInfo.CreatedDate = DateTime.Now;
                                EndorsementInfo.ModifiedBy = userID;
                                EndorsementInfo.ModifiedDate = !string.IsNullOrEmpty(policyInfoRecVM.ModifiedDate) ? DateTime.ParseExact(policyInfoRecVM.ModifiedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                unitOfWork.TblEndorsementInfo.Insert(EndorsementInfo);
                                unitOfWork.Save();


                            }
                            #endregion


                                foreach (var policyInfoChargeVM in policyInfoRecVM.policyInfoChargeList)
                                {
                                    tblPolicyInforCharge_New policyInfoCharge = new tblPolicyInforCharge_New();
                                    policyInfoCharge.PolicyInfoPaymentID = policyInfoRecording.PolicyInfoRecID;
                                    policyInfoCharge.ChargeTypeID = policyInfoChargeVM.ChargeTypeID;
                                    policyInfoCharge.Amount = policyInfoChargeVM.Amount;
                                    policyInfoCharge.ComHeaderID = policyInfoChargeVM.ComHeaderID;
                                    //policyInfoCharge.IsCR = policyInfoChargeVM.IsCR;
                                    policyInfoCharge.CreatedBy = userID;
                                    policyInfoCharge.CreatedDate = DateTime.Now;
                                    unitOfWork.TblPolicyInforCharge_NewRepository.Insert(policyInfoCharge);
                                    unitOfWork.Save();
                                }





                        }

                    


                    PolicyRenewalHistoryVM policyRenewalHistoryVM = new PolicyRenewalHistoryVM();
                    

                        if (policyInfoRecVM.TransactionTypeID == 1037)
                        {

                            //Save Policy Information Recording Details
                            tblPolicyInformationRecording policyInfoRecording = new tblPolicyInformationRecording();
                            policyInfoRecording.PolicyNumber = policyInfoRecVM.PolicyNumber;
                            policyInfoRecording.QuotationHeaderID = quotationHeaderID;
                            policyInfoRecording.QuotationDetailsInsCompanyLineID = policyInfoRecVM.QuotationDetailsInsCompanyLineID;
                            policyInfoRecording.SumAssured = policyInfoRecVM.SumAssured < 0 ? 0 : policyInfoRecVM.SumAssured;
                            policyInfoRecording.SumAssuredCurrencyTypeID = policyInfoRecVM.SumAssuredCurrencyTypeID < 0 ? 0 : policyInfoRecVM.SumAssuredCurrencyTypeID;
                            policyInfoRecording.PremiumIncludingTax = policyInfoRecVM.PremiumIncludingTax < 0 ? 0 : policyInfoRecVM.PremiumIncludingTax;
                            policyInfoRecording.PremiumIncludingTaxCurrencyTypeID = policyInfoRecVM.PremiumIncludingTaxCurrencyTypeID;
                            policyInfoRecording.PeriodOfCoverFromDate = !string.IsNullOrEmpty(policyInfoRecVM.PeriodOfCoverFromDate) ? DateTime.ParseExact(policyInfoRecVM.PeriodOfCoverFromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            policyInfoRecording.PeriodOfCoverToDate = !string.IsNullOrEmpty(policyInfoRecVM.PeriodOfCoverToDate) ? DateTime.ParseExact(policyInfoRecVM.PeriodOfCoverToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            policyInfoRecording.PolicyRequestedDate = !string.IsNullOrEmpty(policyInfoRecVM.PolicyRequestedDate) ? DateTime.ParseExact(policyInfoRecVM.PeriodOfCoverToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            policyInfoRecording.OtherExcessDescription = policyInfoRecVM.OtherExcessDescription == null ? "" : policyInfoRecVM.OtherExcessDescription;
                            policyInfoRecording.NonCommissionPremium = policyInfoRecVM.NonCommissionPremium > 0 ? policyInfoRecVM.NonCommissionPremium : 0;
                            policyInfoRecording.GrossPremium = policyInfoRecVM.GrossPremium > 0 ? policyInfoRecVM.GrossPremium : 0;
                            policyInfoRecording.OtherExcessAmount = policyInfoRecVM.OtherExcessAmount > 0 ? policyInfoRecVM.OtherExcessAmount : 0;
                            policyInfoRecording.VehicleNumber = !string.IsNullOrEmpty(policyInfoRecVM.VehicleNumber) ? policyInfoRecVM.VehicleNumber : "";
                            policyInfoRecording.TransactionTypeID = policyInfoRecVM.TransactionTypeID > 0 ? policyInfoRecVM.TransactionTypeID : 0;
                            policyInfoRecording.CommissionStructureHeaderID = policyInfoRecVM.CommissionStructureHeaderID > 0 ? policyInfoRecVM.CommissionStructureHeaderID : 0;
                            policyInfoRecording.IntroducerID = policyInfoRecVM.IntroducerID > 0 ? policyInfoRecVM.IntroducerID : 0;
                            policyInfoRecording.AccountExecutiveID = policyInfoRecVM.AccountExecutiveID > 0 ? policyInfoRecVM.AccountExecutiveID : 0;
                            policyInfoRecording.TotalCommission = policyInfoRecVM.TotalCommission > 0 ? policyInfoRecVM.TotalCommission : 0;
                            policyInfoRecording.OtherExcessAmount = policyInfoRecVM.OtherExcessAmount > 0 ? policyInfoRecVM.OtherExcessAmount : 0;
                            policyInfoRecording.TaxInvoiceNumber = !string.IsNullOrEmpty(policyInfoRecVM.TaxInvoiceNumber) ? policyInfoRecVM.TaxInvoiceNumber : "";
                            policyInfoRecording.FileNumber = !string.IsNullOrEmpty(policyInfoRecVM.FileNumber) ? policyInfoRecVM.FileNumber : "";
                            policyInfoRecording.IsActive = policyInfoRecVM.IsActive;
                            policyInfoRecording.CreatedDate = DateTime.Now;
                            policyInfoRecording.CreatedBy = userID;
                            policyInfoRecording.ExchangeRate = policyInfoRecVM.CurrencyRate > 0 ? policyInfoRecVM.CurrencyRate : 0;
                            policyInfoRecording.InuranceCompanyId = policyInfoRecVM.InsuranceCompanyID > 0 ? policyInfoRecVM.InsuranceCompanyID : 0;
                            unitOfWork.TblPolicyInformationRecordingRepository.Insert(policyInfoRecording);
                            unitOfWork.Save();

                            int index = 0;
                            index = policyInfoRecording.PolicyInfoRecID;

                            #region Save Policy Commission Payment Details
                            foreach (var policyCommissionPayment in policyInfoRecVM.PolicyNewInfoCharge)
                            {
                                ////Saving charge type and commission as commision details
                                //tblPolicyCommisionPayment policyCommisionPayment = new tblPolicyCommisionPayment();
                                //policyCommisionPayment.PolicyInfoRecID = policyInfoRecording.PolicyInfoRecID;
                                //policyCommisionPayment.CommisionTypeID = policyCommissionPayment.ComHeaderID;
                                //policyCommisionPayment.CommisionValue = policyCommissionPayment.ComHeaderID;
                                //policyCommisionPayment.ComStructLineID = policyCommissionPayment.ChargeTypeID;
                                //policyCommisionPayment.Amount = policyCommissionPayment.Amount;
                                //policyCommisionPayment.CreatedDate = DateTime.Now;
                                //policyCommisionPayment.CreatedBy = userID;
                                //unitOfWork.TblPolicyCommisionPaymentRepository.Insert(policyCommisionPayment);
                                //unitOfWork.Save();



                                tblPolicyCommisionPayment_New policyCommisionPayment = new tblPolicyCommisionPayment_New();
                                policyCommisionPayment.PolicyInfoRecID = policyInfoRecording.PolicyInfoRecID;
                                policyCommisionPayment.ChargeTypeID = policyCommissionPayment.ChargeTypeID;
                                policyCommisionPayment.Amount = policyCommissionPayment.Amount > 0 ? policyCommissionPayment.Amount : 0;
                                policyCommisionPayment.CreatedDate = DateTime.Now;
                                policyCommisionPayment.CreatedBy = userID;
                                unitOfWork.TblPolicyCommisionPayment_NewRepository.Insert(policyCommisionPayment);
                                unitOfWork.Save();
                                

                                tblPolicyRenewalHistory policyRenewalHistory = unitOfWork.TblPolicyRenewalHistoryRepository.GetByID(policyRenewalHistoryVM.PolicyRenewalHistoryID);
                                policyRenewalHistory.PolicyInfoRecID = index;
                                policyRenewalHistory.RenewalDate = !string.IsNullOrEmpty(policyRenewalHistoryVM.RenewalDate) ? DateTime.ParseExact(policyRenewalHistoryVM.RenewalDate, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                policyRenewalHistory.NotificationDate = !string.IsNullOrEmpty(policyRenewalHistoryVM.NotificationDate) ? DateTime.ParseExact(policyRenewalHistoryVM.NotificationDate, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                policyRenewalHistory.IsSent = policyRenewalHistoryVM.IsSent;
                                policyRenewalHistory.IsCancel = policyRenewalHistoryVM.IsCancel;
                                policyRenewalHistory.ModifiedDate = DateTime.Now;
                                policyRenewalHistory.ModifiedBy = policyRenewalHistoryVM.ModifiedBy;
                                unitOfWork.TblPolicyRenewalHistoryRepository.Update(policyRenewalHistory);
                                unitOfWork.Save();


                            }
                            #endregion


                            foreach (var policyInfoChargeVM in policyInfoRecVM.policyInfoChargeList)
                            {
                                tblPolicyInforCharge_New policyInfoCharge = new tblPolicyInforCharge_New();
                                policyInfoCharge.PolicyInfoPaymentID = policyInfoRecording.PolicyInfoRecID;
                                policyInfoCharge.ChargeTypeID = policyInfoChargeVM.ChargeTypeID;
                                policyInfoCharge.Amount = policyInfoChargeVM.Amount;
                                policyInfoCharge.ComHeaderID = policyInfoChargeVM.ComHeaderID;
                                //policyInfoCharge.IsCR = policyInfoChargeVM.IsCR;
                                policyInfoCharge.CreatedBy = userID;
                                policyInfoCharge.CreatedDate = DateTime.Now;
                                unitOfWork.TblPolicyInforCharge_NewRepository.Insert(policyInfoCharge);
                                unitOfWork.Save();
                            }

                        }


                       else
                        {

                            //Save Policy Information Recording Details
                            tblPolicyInformationRecording policyInfoRecording = new tblPolicyInformationRecording();
                            policyInfoRecording.PolicyNumber = policyInfoRecVM.PolicyNumber;
                            policyInfoRecording.QuotationHeaderID = quotationHeaderID;
                            policyInfoRecording.QuotationDetailsInsCompanyLineID = policyInfoRecVM.QuotationDetailsInsCompanyLineID;
                            policyInfoRecording.SumAssured = policyInfoRecVM.SumAssured < 0 ? 0 : policyInfoRecVM.SumAssured;
                            policyInfoRecording.SumAssuredCurrencyTypeID = policyInfoRecVM.SumAssuredCurrencyTypeID < 0 ? 0 : policyInfoRecVM.SumAssuredCurrencyTypeID;
                            policyInfoRecording.PremiumIncludingTax = policyInfoRecVM.PremiumIncludingTax < 0 ? 0 : policyInfoRecVM.PremiumIncludingTax;
                            policyInfoRecording.PremiumIncludingTaxCurrencyTypeID = policyInfoRecVM.PremiumIncludingTaxCurrencyTypeID;
                            policyInfoRecording.PeriodOfCoverFromDate = !string.IsNullOrEmpty(policyInfoRecVM.PeriodOfCoverFromDate) ? DateTime.ParseExact(policyInfoRecVM.PeriodOfCoverFromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            policyInfoRecording.PeriodOfCoverToDate = !string.IsNullOrEmpty(policyInfoRecVM.PeriodOfCoverToDate) ? DateTime.ParseExact(policyInfoRecVM.PeriodOfCoverToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            policyInfoRecording.PolicyRequestedDate = !string.IsNullOrEmpty(policyInfoRecVM.PolicyRequestedDate) ? DateTime.ParseExact(policyInfoRecVM.PeriodOfCoverToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            policyInfoRecording.OtherExcessDescription = policyInfoRecVM.OtherExcessDescription == null ? "" : policyInfoRecVM.OtherExcessDescription;
                            policyInfoRecording.NonCommissionPremium = policyInfoRecVM.NonCommissionPremium > 0 ? policyInfoRecVM.NonCommissionPremium : 0;
                            policyInfoRecording.GrossPremium = policyInfoRecVM.GrossPremium > 0 ? policyInfoRecVM.GrossPremium : 0;
                            policyInfoRecording.OtherExcessAmount = policyInfoRecVM.OtherExcessAmount > 0 ? policyInfoRecVM.OtherExcessAmount : 0;
                            policyInfoRecording.VehicleNumber = !string.IsNullOrEmpty(policyInfoRecVM.VehicleNumber) ? policyInfoRecVM.VehicleNumber : "";
                            policyInfoRecording.TransactionTypeID = policyInfoRecVM.TransactionTypeID > 0 ? policyInfoRecVM.TransactionTypeID : 0;
                            policyInfoRecording.CommissionStructureHeaderID = policyInfoRecVM.CommissionStructureHeaderID > 0 ? policyInfoRecVM.CommissionStructureHeaderID : 0;
                            policyInfoRecording.IntroducerID = policyInfoRecVM.IntroducerID > 0 ? policyInfoRecVM.IntroducerID : 0;
                            policyInfoRecording.AccountExecutiveID = policyInfoRecVM.AccountExecutiveID > 0 ? policyInfoRecVM.AccountExecutiveID : 0;
                            policyInfoRecording.TotalCommission = policyInfoRecVM.TotalCommission > 0 ? policyInfoRecVM.TotalCommission : 0;
                            policyInfoRecording.OtherExcessAmount = policyInfoRecVM.OtherExcessAmount > 0 ? policyInfoRecVM.OtherExcessAmount : 0;
                            policyInfoRecording.TaxInvoiceNumber = !string.IsNullOrEmpty(policyInfoRecVM.TaxInvoiceNumber) ? policyInfoRecVM.TaxInvoiceNumber : "";
                            policyInfoRecording.FileNumber = !string.IsNullOrEmpty(policyInfoRecVM.FileNumber) ? policyInfoRecVM.FileNumber : "";
                            policyInfoRecording.IsActive = policyInfoRecVM.IsActive;
                            policyInfoRecording.CreatedDate = DateTime.Now;
                            policyInfoRecording.CreatedBy = userID;
                            policyInfoRecording.ExchangeRate = policyInfoRecVM.CurrencyRate > 0 ? policyInfoRecVM.CurrencyRate : 0;
                            policyInfoRecording.InuranceCompanyId = policyInfoRecVM.InsuranceCompanyID > 0 ? policyInfoRecVM.InsuranceCompanyID : 0;
                            unitOfWork.TblPolicyInformationRecordingRepository.Insert(policyInfoRecording);
                            unitOfWork.Save();

                            int index = 0;
                            index = policyInfoRecording.PolicyInfoRecID;

                            #region Save Policy Commission Payment Details
                            foreach (var policyCommissionPayment in policyInfoRecVM.PolicyNewInfoCharge)
                            {
                                //Saving charge type and commission as commision details
                                //tblPolicyCommisionPayment policyCommisionPayment = new tblPolicyCommisionPayment();
                                //policyCommisionPayment.PolicyInfoRecID = policyInfoRecording.PolicyInfoRecID;
                                //policyCommisionPayment.com = policyCommissionPayment.ChargeTypeID;
                                //policyCommisionPayment.CommisionValue = policyCommissionPayment.ComHeaderID;
                                //policyCommisionPayment.ComStructLineID = policyCommissionPayment.ChargeTypeID;
                                //policyCommisionPayment.Amount = policyCommissionPayment.Amount > 0 ? policyCommissionPayment.Amount : 0;
                                //policyCommisionPayment.CreatedDate = DateTime.Now;
                                //policyCommisionPayment.CreatedBy = userID;
                                //unitOfWork.TblPolicyCommisionPaymentRepository.Insert(policyCommisionPayment);
                                //unitOfWork.Save();



                                tblPolicyCommisionPayment_New policyCommisionPayment = new tblPolicyCommisionPayment_New();
                                policyCommisionPayment.PolicyInfoRecID = policyInfoRecording.PolicyInfoRecID;
                                policyCommisionPayment.ChargeTypeID = policyCommissionPayment.ChargeTypeID;
                                policyCommisionPayment.Amount = policyCommissionPayment.Amount > 0 ? policyCommissionPayment.Amount : 0;
                                policyCommisionPayment.CreatedDate = DateTime.Now;
                                policyCommisionPayment.CreatedBy = userID;
                                unitOfWork.TblPolicyCommisionPayment_NewRepository.Insert(policyCommisionPayment);
                                unitOfWork.Save(); 




                            }
                            #endregion


                            foreach (var policyInfoChargeVM in policyInfoRecVM.policyInfoChargeList)
                            {
                                tblPolicyInfoCharge policyInfoCharge = new tblPolicyInfoCharge();
                                policyInfoCharge.PolicyInfoChargeID = policyInfoRecording.PolicyInfoRecID;
                                policyInfoCharge.ChargeTypeID =(int) policyInfoChargeVM.ChargeTypeID;
                                policyInfoCharge.Amount = policyInfoChargeVM.Amount;
                                policyInfoCharge.ComHeaderID = policyInfoChargeVM.ComHeaderID;
                                policyInfoCharge.Percentage = (double)policyInfoChargeVM.Percentage;
                                //policyInfoCharge.IsCR = policyInfoChargeVM.IsCR;
                                policyInfoCharge.CreatedBy = userID;
                                policyInfoCharge.CreatedDate = DateTime.Now;
                                policyInfoCharge.ModifiedBy = userID;
                                policyInfoCharge.ModifiedDate = DateTime.Now;
                                unitOfWork.TblPolicyInfoChargeRepository.Insert(policyInfoCharge);
                                unitOfWork.Save();
                            }





                        }
                        

                        //Update Quotation Status Code
                        tblQuotationHeader quotationHeader = unitOfWork.TblQuotationHeaderRepository.GetByID(quotationHeaderID);

                        if (quotationHeader.QuotationStatusCode != QuotationStatusCodeEnum.TCNI.ToString())
                        {
                            quotationHeader.QuotationStatusCode = QuotationStatusCodeEnum.TCNI.ToString();
                            unitOfWork.TblQuotationHeaderRepository.Update(quotationHeader);
                            unitOfWork.Save();
                        }
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


        public bool UpdatePolicyInfoRecording(int quotationHeaderID, List<PolicyInfoRecNewVM> policyInfoRecVMList, List<PolicyNewCommissionPaymentVM> policyQuotationNewCommissionPaymentVM, int userID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    List<tblPolicyInformationRecording> existingPolicyInfoRecList = unitOfWork.TblPolicyInformationRecordingRepository.Get(x => x.QuotationHeaderID == quotationHeaderID).ToList();

                    foreach (var policyInfoRecVM in policyInfoRecVMList)
                    {
                        tblPolicyInformationRecording policyInfoRecordingUpdateObj = existingPolicyInfoRecList.Where(x => x.PolicyInfoRecID == policyInfoRecVM.PolicyInfoRecID).FirstOrDefault();

                        if (policyInfoRecordingUpdateObj != null)
                        {
                            //Update Policy Information Recording Details
                            ////policyInfoRecordingUpdateObj.PolicyNumber = policyInfoRecVM.PolicyNumber;
                            ////policyInfoRecordingUpdateObj.QuotationHeaderID = quotationHeaderID;
                            ////policyInfoRecordingUpdateObj.QuotationDetailsInsCompanyLineID = policyInfoRecVM.QuotationDetailsInsCompanyLineID;
                            ////policyInfoRecordingUpdateObj.SumAssured = policyInfoRecVM.SumAssured;
                            ////policyInfoRecordingUpdateObj.SumAssuredCurrencyTypeID = policyInfoRecVM.SumAssuredCurrencyTypeID;
                            ////policyInfoRecordingUpdateObj.PremiumIncludingTax = policyInfoRecVM.PremiumIncludingTax;
                            ////policyInfoRecordingUpdateObj.PremiumIncludingTaxCurrencyTypeID = policyInfoRecVM.PremiumIncludingTaxCurrencyTypeID;
                            ////policyInfoRecordingUpdateObj.PeriodOfCoverFromDate = !string.IsNullOrEmpty(policyInfoRecVM.PeriodOfCoverFromDate) ? DateTime.ParseExact(policyInfoRecVM.PeriodOfCoverFromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            ////policyInfoRecordingUpdateObj.PeriodOfCoverToDate = !string.IsNullOrEmpty(policyInfoRecVM.PeriodOfCoverToDate) ? DateTime.ParseExact(policyInfoRecVM.PeriodOfCoverToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            ////policyInfoRecordingUpdateObj.OtherExcessDescription = policyInfoRecVM.OtherExcessDescription;
                            ////policyInfoRecordingUpdateObj.NonCommissionPremium = policyInfoRecVM.NonCommissionPremium;
                            ////policyInfoRecordingUpdateObj.GrossPremium = policyInfoRecVM.GrossPremium;
                            ////policyInfoRecordingUpdateObj.VehicleNumber = policyInfoRecVM.VehicleNumber;
                            ////policyInfoRecordingUpdateObj.TransactionTypeID = policyInfoRecVM.TransactionTypeID;
                            ////policyInfoRecordingUpdateObj.CommissionStructureHeaderID = policyInfoRecVM.CommissionStructureHeaderID;
                            ////policyInfoRecordingUpdateObj.IntroducerID = policyInfoRecVM.IntroducerID;
                            ////policyInfoRecordingUpdateObj.AccountExecutiveID = policyInfoRecVM.AccountExecutiveID;
                            ////policyInfoRecordingUpdateObj.TotalCommission = policyInfoRecVM.TotalCommission;
                            
                            //////exchange value rate stored in  otherExcessAmount
                            ////policyInfoRecordingUpdateObj.OtherExcessAmount = policyInfoRecVM.OtherExcessAmount;
                            ////policyInfoRecordingUpdateObj.TaxInvoiceNumber = policyInfoRecVM.TaxInvoiceNumber;
                            ////policyInfoRecordingUpdateObj.FileNumber = policyInfoRecVM.FileNumber;
                            ////policyInfoRecordingUpdateObj.IsActive = policyInfoRecVM.IsActive;
                            ////policyInfoRecordingUpdateObj.ModifiedDate = DateTime.Now;
                            ////policyInfoRecordingUpdateObj.ModifiedBy = userID;
                            ////policyInfoRecordingUpdateObjUpdateObj.InuranceCompanyId = policyInfoRecVM.InsuranceCompanyID;
                            policyInfoRecordingUpdateObj.PolicyNumber = policyInfoRecVM.PolicyNumber;
                            policyInfoRecordingUpdateObj.QuotationHeaderID = quotationHeaderID;
                            policyInfoRecordingUpdateObj.QuotationDetailsInsCompanyLineID = policyInfoRecVM.QuotationDetailsInsCompanyLineID;
                            policyInfoRecordingUpdateObj.SumAssured = policyInfoRecVM.SumAssured;
                            policyInfoRecordingUpdateObj.SumAssuredCurrencyTypeID = policyInfoRecVM.SumAssuredCurrencyTypeID;
                            policyInfoRecordingUpdateObj.PremiumIncludingTax = policyInfoRecVM.PremiumIncludingTax;
                            policyInfoRecordingUpdateObj.PremiumIncludingTaxCurrencyTypeID = policyInfoRecVM.PremiumIncludingTaxCurrencyTypeID;
                            policyInfoRecordingUpdateObj.PeriodOfCoverFromDate = !string.IsNullOrEmpty(policyInfoRecVM.PeriodOfCoverFromDate) ? DateTime.ParseExact(policyInfoRecVM.PeriodOfCoverFromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            policyInfoRecordingUpdateObj.PeriodOfCoverToDate = !string.IsNullOrEmpty(policyInfoRecVM.PeriodOfCoverToDate) ? DateTime.ParseExact(policyInfoRecVM.PeriodOfCoverToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            policyInfoRecordingUpdateObj.PolicyRequestedDate = !string.IsNullOrEmpty(policyInfoRecVM.PolicyRequestedDate) ? DateTime.ParseExact(policyInfoRecVM.PeriodOfCoverToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            policyInfoRecordingUpdateObj.OtherExcessDescription = policyInfoRecVM.OtherExcessDescription;
                            policyInfoRecordingUpdateObj.NonCommissionPremium = policyInfoRecVM.NonCommissionPremium;
                            policyInfoRecordingUpdateObj.GrossPremium = policyInfoRecVM.GrossPremium;
                            policyInfoRecordingUpdateObj.OtherExcessAmount = policyInfoRecVM.OtherExcessAmount;
                            policyInfoRecordingUpdateObj.VehicleNumber = policyInfoRecVM.VehicleNumber;
                            policyInfoRecordingUpdateObj.TransactionTypeID = policyInfoRecVM.TransactionTypeID;
                            policyInfoRecordingUpdateObj.CommissionStructureHeaderID = policyInfoRecVM.CommissionStructureHeaderID;
                            policyInfoRecordingUpdateObj.IntroducerID = policyInfoRecVM.IntroducerID;
                            policyInfoRecordingUpdateObj.AccountExecutiveID = policyInfoRecVM.AccountExecutiveID;
                            policyInfoRecordingUpdateObj.TotalCommission = policyInfoRecVM.TotalCommission;
                            policyInfoRecordingUpdateObj.OtherExcessAmount = policyInfoRecVM.OtherExcessAmount;
                            policyInfoRecordingUpdateObj.TaxInvoiceNumber = policyInfoRecVM.TaxInvoiceNumber;
                            policyInfoRecordingUpdateObj.FileNumber = policyInfoRecVM.FileNumber;
                            policyInfoRecordingUpdateObj.IsActive = policyInfoRecVM.IsActive;
                            policyInfoRecordingUpdateObj.CreatedDate = DateTime.Now;
                            policyInfoRecordingUpdateObj.CreatedBy = userID;
                            policyInfoRecordingUpdateObj.ExchangeRate = policyInfoRecVM.CurrencyRate;
                            policyInfoRecordingUpdateObj.InuranceCompanyId = policyInfoRecVM.InsuranceCompanyID;


                            unitOfWork.TblPolicyInformationRecordingRepository.Update(policyInfoRecordingUpdateObj);
                            unitOfWork.Save();


                            int index = 0;
                            index = policyInfoRecordingUpdateObj.PolicyInfoRecID;


                            if (policyInfoRecVM.TransactionTypeID == 1039)
                            {
                                tblEndorsementInfo EndorsementInfo = new tblEndorsementInfo();
                                EndorsementInfo.PolicyInfoRecID = index;
                                EndorsementInfo.PolicyNumber = policyInfoRecVM.PolicyNumber;
                                EndorsementInfo.NewSumInsured = policyInfoRecVM.SumAssured;
                                EndorsementInfo.CreatedBy = policyInfoRecVM.CreatedBy;
                                EndorsementInfo.CreatedDate = !string.IsNullOrEmpty(policyInfoRecVM.CreatedDate) ? DateTime.ParseExact(policyInfoRecVM.CreatedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                EndorsementInfo.ModifiedBy = policyInfoRecVM.ModifiedBy;
                                EndorsementInfo.ModifiedDate = !string.IsNullOrEmpty(policyInfoRecVM.ModifiedDate) ? DateTime.ParseExact(policyInfoRecVM.ModifiedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                unitOfWork.TblEndorsementInfo.Insert(EndorsementInfo);
                                unitOfWork.Save();
                            }

                            //Delete Existing Policy Commission Payment Details
                            List<tblPolicyCommisionPayment> existingPolicyCommissionPaymentList = unitOfWork.TblPolicyCommisionPaymentRepository.Get(x => x.PolicyInfoRecID == policyInfoRecordingUpdateObj.PolicyInfoRecID).ToList();

                            foreach (var policyCommissionPayment in existingPolicyCommissionPaymentList)
                            {
                                unitOfWork.TblPolicyCommisionPaymentRepository.Delete(policyCommissionPayment);
                                unitOfWork.Save();
                            }

                           
                            #region Save Policy Commission Payment Details
                            foreach (var policyCommissionPayment in policyQuotationNewCommissionPaymentVM)
                            {
                                
                                tblPolicyCommisionPayment policyCommisionPayment = new tblPolicyCommisionPayment();
                                policyCommisionPayment.PolicyInfoRecID = policyInfoRecordingUpdateObj.PolicyInfoRecID;
                                policyCommisionPayment.CommisionTypeID = policyCommissionPayment.CommisionTypeID;
                                policyCommisionPayment.CommisionValue = policyInfoRecVM.TotalCommission - policyCommissionPayment.Amount;
                                policyCommisionPayment.CreatedDate = DateTime.Now;
                                policyCommisionPayment.CreatedBy = userID;
                                unitOfWork.TblPolicyCommisionPaymentRepository.Insert(policyCommisionPayment);
                                unitOfWork.Save();
                                
                            }
                            #endregion



                            
                            //foreach (var policyInfoChargeVM in policyInfoRecVM.policyInfoChargeList)
                            //{
                            //    tblPolicyInfoCharge policyInfoCharge = new tblPolicyInfoCharge();
                            //    policyInfoCharge.PolicyInfoPaymentID = policyInfoRecVM.PolicyInfoRecID;
                            //    policyInfoCharge.ChargeTypeID = policyInfoChargeVM.ChargeTypeID;
                            //    policyInfoCharge.Amount = policyInfoChargeVM.Amount;
                            //    policyInfoCharge.IsCR = policyInfoChargeVM.IsCR;
                            //    policyInfoCharge.CreatedBy = userID;
                            //    policyInfoCharge.CreatedDate = DateTime.Now;
                            //    unitOfWork.TblPolicyInfoChargeRepository.Insert(policyInfoCharge);
                            //    unitOfWork.Save();
                            //}
                        }
                      
                    }

                    //Delete Policy Information Recording Details
                    foreach (var existingPolicyInfoRec in existingPolicyInfoRecList)
                    {
                        if (!policyInfoRecVMList.Any(x => x.PolicyInfoRecID == existingPolicyInfoRec.PolicyInfoRecID))
                        {
                            List<tblPolicyCommisionPayment> policyCommissionPaymentList = unitOfWork.TblPolicyCommisionPaymentRepository.Get(x => x.PolicyInfoRecID == existingPolicyInfoRec.PolicyInfoRecID).ToList();

                            foreach (var policyCommissionPayment in policyCommissionPaymentList)
                            {
                                unitOfWork.TblPolicyCommisionPaymentRepository.Delete(policyCommissionPayment);
                                unitOfWork.Save();
                            }

                            unitOfWork.TblPolicyInformationRecordingRepository.Delete(existingPolicyInfoRec);
                            unitOfWork.Save();
                        }
                    }

                    //Update Quotation Status Code
                    tblQuotationHeader quotationHeader = unitOfWork.TblQuotationHeaderRepository.GetByID(quotationHeaderID);

                    if (quotationHeader.QuotationStatusCode != QuotationStatusCodeEnum.TCNI.ToString())
                    {
                        quotationHeader.QuotationStatusCode = QuotationStatusCodeEnum.TCNI.ToString();
                        unitOfWork.TblQuotationHeaderRepository.Update(quotationHeader);
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

        public List<PolicyInfoRecVM> GetAllPolicyInfoRecordings()
        {
            try
            {
                var policyInfoRecData = unitOfWork.TblPolicyInformationRecordingRepository.Get().ToList();

                List<PolicyInfoRecVM> policyInfoRecVMList = new List<PolicyInfoRecVM>();

                foreach (var policyInfoRec in policyInfoRecData)
                {
                    PolicyInfoRecVM policyInfoRecVM = new PolicyInfoRecVM();
                    policyInfoRecVM.PolicyInfoRecID = policyInfoRec.PolicyInfoRecID;
                    policyInfoRecVM.PolicyNumber = policyInfoRec.PolicyNumber;
                    policyInfoRecVM.QuotationHeaderID = policyInfoRec.QuotationHeaderID != null ? Convert.ToInt32(policyInfoRec.QuotationHeaderID) : 0;
                    policyInfoRecVM.QuotationDetailsInsCompanyLineID = policyInfoRec.QuotationDetailsInsCompanyLineID != null ? Convert.ToInt32(policyInfoRec.QuotationDetailsInsCompanyLineID) : 0;
                    policyInfoRecVM.SumAssured = policyInfoRec.SumAssured != null ? Convert.ToDecimal(policyInfoRec.SumAssured) : 0;
                    policyInfoRecVM.SumAssuredCurrencyTypeID = policyInfoRec.SumAssuredCurrencyTypeID != null ? Convert.ToInt32(policyInfoRec.SumAssuredCurrencyTypeID) : 0;

                    if (policyInfoRecVM.SumAssuredCurrencyTypeID > 0)
                    {
                        policyInfoRecVM.SumAssuredCurrencyCode = policyInfoRec.tblCurrency.CurrencyCode;
                    }

                    policyInfoRecVM.PremiumIncludingTax = policyInfoRec.PremiumIncludingTax != null ? Convert.ToDecimal(policyInfoRec.PremiumIncludingTax) : 0;
                    policyInfoRecVM.PremiumIncludingTaxCurrencyTypeID = policyInfoRec.PremiumIncludingTaxCurrencyTypeID != null ? Convert.ToInt32(policyInfoRec.PremiumIncludingTaxCurrencyTypeID) : 0;

                    if (policyInfoRecVM.PremiumIncludingTaxCurrencyTypeID > 0)
                    {
                        policyInfoRecVM.PremiumIncludingTaxCurrencyCode = policyInfoRec.tblCurrency1.CurrencyCode;
                    }
                    policyInfoRecVM.NonCommissionPremium = policyInfoRec.NonCommissionPremium;
                    policyInfoRecVM.GrossPremium = policyInfoRec.GrossPremium;
                    policyInfoRecVM.VehicleNumber = policyInfoRec.VehicleNumber;
                    policyInfoRecVM.TransactionTypeID = policyInfoRec.TransactionTypeID;
                    policyInfoRecVM.CommissionStructureHeaderID = policyInfoRec.CommissionStructureHeaderID;
                    policyInfoRecVM.IntroducerID = policyInfoRec.IntroducerID;
                    policyInfoRecVM.AccountExecutiveID = policyInfoRec.AccountExecutiveID;
                    policyInfoRecVM.PeriodOfCoverFromDate = policyInfoRec.PeriodOfCoverFromDate != null ? Convert.ToDateTime(policyInfoRec.PeriodOfCoverFromDate).ToString("MM/dd/yyyy") : string.Empty;
                    policyInfoRecVM.PeriodOfCoverToDate = policyInfoRec.PeriodOfCoverToDate != null ? Convert.ToDateTime(policyInfoRec.PeriodOfCoverToDate).ToString("MM/dd/yyyy") : string.Empty;
                    policyInfoRecVM.OtherExcessDescription = policyInfoRec.OtherExcessDescription;
                    policyInfoRecVM.OtherExcessAmount = policyInfoRec.OtherExcessAmount != null ? Convert.ToDecimal(policyInfoRec.OtherExcessAmount) : 0;
                    policyInfoRecVM.TaxInvoiceNumber = policyInfoRec.TaxInvoiceNumber;
                    policyInfoRecVM.FileNumber = policyInfoRec.FileNumber;
                    policyInfoRecVM.IsActive = policyInfoRec.IsActive;
                    policyInfoRecVM.CreatedBy = policyInfoRec.CreatedBy != null ? Convert.ToInt32(policyInfoRec.CreatedBy) : 0;
                    policyInfoRecVM.CreatedDate = policyInfoRec.CreatedDate != null ? policyInfoRec.CreatedDate.ToString() : string.Empty;
                    policyInfoRecVM.ModifiedBy = policyInfoRec.ModifiedBy != null ? Convert.ToInt32(policyInfoRec.ModifiedBy) : 0;
                    policyInfoRecVM.ModifiedDate = policyInfoRec.ModifiedDate != null ? policyInfoRec.ModifiedDate.ToString() : string.Empty;

                    List<tblPolicyCommisionPayment> policyCommissionPaymentData = unitOfWork.TblPolicyCommisionPaymentRepository.Get(x => x.PolicyInfoRecID == policyInfoRec.PolicyInfoRecID).ToList();

                    List<PolicyCommissionPaymentVM> policyCommissionPaymentList = new List<PolicyCommissionPaymentVM>();

                    foreach (var policyCommissionPayment in policyCommissionPaymentData)
                    {
                        PolicyCommissionPaymentVM policyCommissionPaymentVM = new PolicyCommissionPaymentVM();
                        policyCommissionPaymentVM.PolicyCommisionPaymentID = policyCommissionPayment.PolicyCommisionPaymentID;
                        policyCommissionPaymentVM.PolicyInfoRecID = policyCommissionPayment.PolicyInfoRecID != null ? Convert.ToInt32(policyCommissionPayment.PolicyInfoRecID) : 0;
                        policyCommissionPaymentVM.CommisionTypeID = policyCommissionPayment.CommisionTypeID != null ? Convert.ToInt32(policyCommissionPayment.CommisionTypeID) : 0;

                        if (policyCommissionPaymentVM.CommisionTypeID > 0)
                        {
                            policyCommissionPaymentVM.CommissionTypeName = policyCommissionPayment.tblCommisionType.CommisionType;
                        }

                        policyCommissionPaymentVM.CommisionValue = policyCommissionPayment.CommisionValue != null ? Convert.ToDecimal(policyCommissionPayment.CommisionValue) : 0;
                        policyCommissionPaymentVM.ComStructLineID = policyCommissionPayment.ComStructLineID != null ? Convert.ToInt32(policyCommissionPayment.ComStructLineID) : 0;

                        if (policyCommissionPaymentVM.ComStructLineID > 0)
                        {
                            policyCommissionPaymentVM.RateValue = policyCommissionPayment.tblCommisionStructureLine.RateValue != null ? Convert.ToDouble(policyCommissionPayment.tblCommisionStructureLine.RateValue) : 0;

                            if (policyCommissionPayment.tblCommisionStructureLine.ComStructID != null)
                            {
                                policyCommissionPaymentVM.PartnerID = policyCommissionPayment.tblCommisionStructureLine.tblCommisionStructureHeader.PartnerID != null ? Convert.ToInt32(policyCommissionPayment.tblCommisionStructureLine.tblCommisionStructureHeader.PartnerID) : 0;

                                if (policyCommissionPaymentVM.PartnerID > 0)
                                {
                                    policyCommissionPaymentVM.PartnerName = policyCommissionPayment.tblCommisionStructureLine.tblCommisionStructureHeader.tblPartner.PartnerName;
                                }
                            }
                        }

                        policyCommissionPaymentVM.CreatedBy = policyCommissionPayment.CreatedBy != null ? Convert.ToInt32(policyCommissionPayment.CreatedBy) : 0;
                        policyCommissionPaymentVM.CreatedDate = policyCommissionPayment.CreatedDate != null ? policyCommissionPayment.CreatedDate.ToString() : string.Empty;
                        policyCommissionPaymentVM.ModifiedBy = policyCommissionPayment.ModifiedBy != null ? Convert.ToInt32(policyCommissionPayment.ModifiedBy) : 0;
                        policyCommissionPaymentVM.ModifiedDate = policyCommissionPayment.ModifiedDate != null ? policyCommissionPayment.ModifiedDate.ToString() : string.Empty;

                        policyCommissionPaymentList.Add(policyCommissionPaymentVM);
                    }

                    policyInfoRecVM.PolicyCommissionPaymentDetails = policyCommissionPaymentList;

                    policyInfoRecVMList.Add(policyInfoRecVM);
                }

                return policyInfoRecVMList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<PolicyInfoRecVM> GetAllPolicyInfoRecordingsByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var policyInfoRecData = unitOfWork.TblPolicyInformationRecordingRepository.Get(x => x.tblQuotationHeader.tblClientRequestHeader.tblClient.BUID == businessUnitID).ToList();

                List<PolicyInfoRecVM> policyInfoRecVMList = new List<PolicyInfoRecVM>();

                foreach (var policyInfoRec in policyInfoRecData)
                {
                    PolicyInfoRecVM policyInfoRecVM = new PolicyInfoRecVM();
                    policyInfoRecVM.PolicyInfoRecID = policyInfoRec.PolicyInfoRecID;
                    policyInfoRecVM.PolicyNumber = policyInfoRec.PolicyNumber;
                    policyInfoRecVM.QuotationHeaderID = policyInfoRec.QuotationHeaderID != null ? Convert.ToInt32(policyInfoRec.QuotationHeaderID) : 0;
                    policyInfoRecVM.QuotationDetailsInsCompanyLineID = policyInfoRec.QuotationDetailsInsCompanyLineID != null ? Convert.ToInt32(policyInfoRec.QuotationDetailsInsCompanyLineID) : 0;
                    policyInfoRecVM.SumAssured = policyInfoRec.SumAssured != null ? Convert.ToDecimal(policyInfoRec.SumAssured) : 0;
                    policyInfoRecVM.SumAssuredCurrencyTypeID = policyInfoRec.SumAssuredCurrencyTypeID != null ? Convert.ToInt32(policyInfoRec.SumAssuredCurrencyTypeID) : 0;

                    if (policyInfoRecVM.SumAssuredCurrencyTypeID > 0)
                    {
                        policyInfoRecVM.SumAssuredCurrencyCode = policyInfoRec.tblCurrency.CurrencyCode;
                    }

                    policyInfoRecVM.PremiumIncludingTax = policyInfoRec.PremiumIncludingTax != null ? Convert.ToDecimal(policyInfoRec.PremiumIncludingTax) : 0;
                    policyInfoRecVM.PremiumIncludingTaxCurrencyTypeID = policyInfoRec.PremiumIncludingTaxCurrencyTypeID != null ? Convert.ToInt32(policyInfoRec.PremiumIncludingTaxCurrencyTypeID) : 0;

                    if (policyInfoRecVM.PremiumIncludingTaxCurrencyTypeID > 0)
                    {
                        policyInfoRecVM.PremiumIncludingTaxCurrencyCode = policyInfoRec.tblCurrency1.CurrencyCode;
                    }

                    policyInfoRecVM.PeriodOfCoverFromDate = policyInfoRec.PeriodOfCoverFromDate != null ? Convert.ToDateTime(policyInfoRec.PeriodOfCoverFromDate).ToString("MM/dd/yyyy") : string.Empty;
                    policyInfoRecVM.PeriodOfCoverToDate = policyInfoRec.PeriodOfCoverToDate != null ? Convert.ToDateTime(policyInfoRec.PeriodOfCoverToDate).ToString("MM/dd/yyyy") : string.Empty;
                    policyInfoRecVM.OtherExcessDescription = policyInfoRec.OtherExcessDescription;
                    policyInfoRecVM.OtherExcessAmount = policyInfoRec.OtherExcessAmount != null ? Convert.ToDecimal(policyInfoRec.OtherExcessAmount) : 0;
                    policyInfoRecVM.TaxInvoiceNumber = policyInfoRec.TaxInvoiceNumber;
                    policyInfoRecVM.FileNumber = policyInfoRec.FileNumber;
                    policyInfoRecVM.IsActive = policyInfoRec.IsActive;
                    policyInfoRecVM.CreatedBy = policyInfoRec.CreatedBy != null ? Convert.ToInt32(policyInfoRec.CreatedBy) : 0;
                    policyInfoRecVM.CreatedDate = policyInfoRec.CreatedDate != null ? policyInfoRec.CreatedDate.ToString() : string.Empty;
                    policyInfoRecVM.ModifiedBy = policyInfoRec.ModifiedBy != null ? Convert.ToInt32(policyInfoRec.ModifiedBy) : 0;
                    policyInfoRecVM.ModifiedDate = policyInfoRec.ModifiedDate != null ? policyInfoRec.ModifiedDate.ToString() : string.Empty;


                    List<tblPolicyCommisionPayment> policyCommissionPaymentData = unitOfWork.TblPolicyCommisionPaymentRepository.Get(x => x.PolicyInfoRecID == policyInfoRec.PolicyInfoRecID).ToList();

                    List<PolicyCommissionPaymentVM> policyCommissionPaymentList = new List<PolicyCommissionPaymentVM>();

                    foreach (var policyCommissionPayment in policyCommissionPaymentData)
                    {
                        PolicyCommissionPaymentVM policyCommissionPaymentVM = new PolicyCommissionPaymentVM();
                        policyCommissionPaymentVM.PolicyCommisionPaymentID = policyCommissionPayment.PolicyCommisionPaymentID;
                        policyCommissionPaymentVM.PolicyInfoRecID = policyCommissionPayment.PolicyInfoRecID != null ? Convert.ToInt32(policyCommissionPayment.PolicyInfoRecID) : 0;
                        policyCommissionPaymentVM.CommisionTypeID = policyCommissionPayment.CommisionTypeID != null ? Convert.ToInt32(policyCommissionPayment.CommisionTypeID) : 0;

                        if (policyCommissionPaymentVM.CommisionTypeID > 0)
                        {
                            policyCommissionPaymentVM.CommissionTypeName = policyCommissionPayment.tblCommisionType.CommisionType;
                        }

                        policyCommissionPaymentVM.CommisionValue = policyCommissionPayment.CommisionValue != null ? Convert.ToDecimal(policyCommissionPayment.CommisionValue) : 0;
                        policyCommissionPaymentVM.ComStructLineID = policyCommissionPayment.ComStructLineID != null ? Convert.ToInt32(policyCommissionPayment.ComStructLineID) : 0;

                        if (policyCommissionPaymentVM.ComStructLineID > 0)
                        {
                            policyCommissionPaymentVM.RateValue = policyCommissionPayment.tblCommisionStructureLine.RateValue != null ? Convert.ToDouble(policyCommissionPayment.tblCommisionStructureLine.RateValue) : 0;

                            if (policyCommissionPayment.tblCommisionStructureLine.ComStructID != null)
                            {
                                policyCommissionPaymentVM.PartnerID = policyCommissionPayment.tblCommisionStructureLine.tblCommisionStructureHeader.PartnerID != null ? Convert.ToInt32(policyCommissionPayment.tblCommisionStructureLine.tblCommisionStructureHeader.PartnerID) : 0;

                                if (policyCommissionPaymentVM.PartnerID > 0)
                                {
                                    policyCommissionPaymentVM.PartnerName = policyCommissionPayment.tblCommisionStructureLine.tblCommisionStructureHeader.tblPartner.PartnerName;
                                }
                            }
                        }

                        policyCommissionPaymentVM.CreatedBy = policyCommissionPayment.CreatedBy != null ? Convert.ToInt32(policyCommissionPayment.CreatedBy) : 0;
                        policyCommissionPaymentVM.CreatedDate = policyCommissionPayment.CreatedDate != null ? policyCommissionPayment.CreatedDate.ToString() : string.Empty;
                        policyCommissionPaymentVM.ModifiedBy = policyCommissionPayment.ModifiedBy != null ? Convert.ToInt32(policyCommissionPayment.ModifiedBy) : 0;
                        policyCommissionPaymentVM.ModifiedDate = policyCommissionPayment.ModifiedDate != null ? policyCommissionPayment.ModifiedDate.ToString() : string.Empty;

                        policyCommissionPaymentList.Add(policyCommissionPaymentVM);
                    }

                    policyInfoRecVM.PolicyCommissionPaymentDetails = policyCommissionPaymentList;




                    //List<tblPolicyCommisionPayment> policyCommissionPaymentData = unitOfWork.TblPolicyCommisionPaymentRepository.Get(x => x.PolicyInfoRecID == policyInfoRec.PolicyInfoRecID).ToList();



                    List<PolicyCommissionPaymentVM> policyCommissionPaymentListOLD = new List<PolicyCommissionPaymentVM>();

                    //foreach (var policyCommissionPayment in policyCommissionPaymentData)
                    //{
                    //    PolicyCommissionPaymentVM policyCommissionPaymentVM = new PolicyCommissionPaymentVM();
                    //    policyCommissionPaymentVM.PolicyCommisionPaymentID = policyCommissionPayment.PolicyCommisionPaymentID;
                    //    policyCommissionPaymentVM.PolicyInfoRecID = policyCommissionPayment.PolicyInfoRecID != null ? Convert.ToInt32(policyCommissionPayment.PolicyInfoRecID) : 0;
                    //    policyCommissionPaymentVM.CommisionTypeID = policyCommissionPayment.ChargeTypeID != null ? Convert.ToInt32(policyCommissionPayment.ChargeTypeID) : 0;

                    //    if (policyCommissionPaymentVM.CommisionTypeID > 0)
                    //    {

                    //        //policyCommissionPaymentVM.CommissionTypeName = policyCommissionPayment.tblCommisionType.CommisionType;
                    //        policyCommissionPaymentVM.CommissionTypeName = "";
                    //    }

                    //    policyCommissionPaymentVM.CommisionValue = policyCommissionPayment.CommisionValue != null ? Convert.ToDecimal(policyCommissionPayment.CommisionValue) : 0;
                    //    policyCommissionPaymentVM.ComStructLineID = policyCommissionPayment.ComStructLineID != null ? Convert.ToInt32(policyCommissionPayment.ComStructLineID) : 0;

                    //    if (policyCommissionPaymentVM.ComStructLineID > 0)
                    //    {
                    //        policyCommissionPaymentVM.RateValue = policyCommissionPayment.tblCommisionStructureLine.RateValue != null ? Convert.ToDouble(policyCommissionPayment.tblCommisionStructureLine.RateValue) : 0;

                    //        if (policyCommissionPayment.tblCommisionStructureLine.ComStructID != null)
                    //        {
                    //            policyCommissionPaymentVM.PartnerID = policyCommissionPayment.tblCommisionStructureLine.tblCommisionStructureHeader.PartnerID != null ? Convert.ToInt32(policyCommissionPayment.tblCommisionStructureLine.tblCommisionStructureHeader.PartnerID) : 0;

                    //            if (policyCommissionPaymentVM.PartnerID > 0)
                    //            {
                    //                policyCommissionPaymentVM.PartnerName = policyCommissionPayment.tblCommisionStructureLine.tblCommisionStructureHeader.tblPartner.PartnerName;
                    //            }
                    //        }
                    //    }

                    //    policyCommissionPaymentVM.CreatedBy = policyCommissionPayment.CreatedBy != null ? Convert.ToInt32(policyCommissionPayment.CreatedBy) : 0;
                    //    policyCommissionPaymentVM.CreatedDate = policyCommissionPayment.CreatedDate != null ? policyCommissionPayment.CreatedDate.ToString() : string.Empty;
                    //    policyCommissionPaymentVM.ModifiedBy = policyCommissionPayment.ModifiedBy != null ? Convert.ToInt32(policyCommissionPayment.ModifiedBy) : 0;
                    //    policyCommissionPaymentVM.ModifiedDate = policyCommissionPayment.ModifiedDate != null ? policyCommissionPayment.ModifiedDate.ToString() : string.Empty;

                    //    policyCommissionPaymentList.Add(policyCommissionPaymentVM);
                    //}

                    policyInfoRecVM.PolicyCommissionPaymentDetails = policyCommissionPaymentListOLD;

                    policyInfoRecVMList.Add(policyInfoRecVM);
                }

                return policyInfoRecVMList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<PolicyInfoRecVM> GetPolicyInfoRecordingsByQuotation(int quotationHeaderID)
        {
            try
            {
                var policyInfoRecData = unitOfWork.TblPolicyInformationRecordingRepository.Get(x => x.QuotationHeaderID == quotationHeaderID).ToList();

                List<PolicyInfoRecVM> policyInfoRecVMList = new List<PolicyInfoRecVM>();

                foreach (var policyInfoRec in policyInfoRecData)
                {
                    PolicyInfoRecVM policyInfoRecVM = new PolicyInfoRecVM();
                    policyInfoRecVM.PolicyInfoRecID = policyInfoRec.PolicyInfoRecID;
                    policyInfoRecVM.PolicyNumber = policyInfoRec.PolicyNumber;
                    policyInfoRecVM.QuotationHeaderID = policyInfoRec.QuotationHeaderID != null ? Convert.ToInt32(policyInfoRec.QuotationHeaderID) : 0;
                    policyInfoRecVM.QuotationDetailsInsCompanyLineID = policyInfoRec.QuotationDetailsInsCompanyLineID != null ? Convert.ToInt32(policyInfoRec.QuotationDetailsInsCompanyLineID) : 0;
                    policyInfoRecVM.SumAssured = policyInfoRec.SumAssured != null ? Convert.ToDecimal(policyInfoRec.SumAssured) : 0;
                    policyInfoRecVM.SumAssuredCurrencyTypeID = policyInfoRec.SumAssuredCurrencyTypeID != null ? Convert.ToInt32(policyInfoRec.SumAssuredCurrencyTypeID) : 0;

                    if (policyInfoRecVM.SumAssuredCurrencyTypeID > 0)
                    {
                        policyInfoRecVM.SumAssuredCurrencyCode = policyInfoRec.tblCurrency.CurrencyCode;
                    }

                    policyInfoRecVM.PremiumIncludingTax = policyInfoRec.PremiumIncludingTax != null ? Convert.ToDecimal(policyInfoRec.PremiumIncludingTax) : 0;
                    policyInfoRecVM.PremiumIncludingTaxCurrencyTypeID = policyInfoRec.PremiumIncludingTaxCurrencyTypeID != null ? Convert.ToInt32(policyInfoRec.PremiumIncludingTaxCurrencyTypeID) : 0;

                    if (policyInfoRecVM.PremiumIncludingTaxCurrencyTypeID > 0)
                    {
                        policyInfoRecVM.PremiumIncludingTaxCurrencyCode = policyInfoRec.tblCurrency1.CurrencyCode;
                    }

                    policyInfoRecVM.PeriodOfCoverFromDate = policyInfoRec.PeriodOfCoverFromDate != null ? Convert.ToDateTime(policyInfoRec.PeriodOfCoverFromDate).ToString("MM/dd/yyyy") : string.Empty;
                    policyInfoRecVM.PeriodOfCoverToDate = policyInfoRec.PeriodOfCoverToDate != null ? Convert.ToDateTime(policyInfoRec.PeriodOfCoverToDate).ToString("MM/dd/yyyy") : string.Empty;
                    policyInfoRecVM.OtherExcessDescription = policyInfoRec.OtherExcessDescription;
                    policyInfoRecVM.OtherExcessAmount = policyInfoRec.OtherExcessAmount != null ? Convert.ToDecimal(policyInfoRec.OtherExcessAmount) : 0;
                    policyInfoRecVM.TaxInvoiceNumber = policyInfoRec.TaxInvoiceNumber;
                    policyInfoRecVM.FileNumber = policyInfoRec.FileNumber;
                    policyInfoRecVM.IsActive = policyInfoRec.IsActive;
                    policyInfoRecVM.CreatedBy = policyInfoRec.CreatedBy != null ? Convert.ToInt32(policyInfoRec.CreatedBy) : 0;
                    policyInfoRecVM.CreatedDate = policyInfoRec.CreatedDate != null ? policyInfoRec.CreatedDate.ToString() : string.Empty;
                    policyInfoRecVM.ModifiedBy = policyInfoRec.ModifiedBy != null ? Convert.ToInt32(policyInfoRec.ModifiedBy) : 0;
                    policyInfoRecVM.ModifiedDate = policyInfoRec.ModifiedDate != null ? policyInfoRec.ModifiedDate.ToString() : string.Empty;

                    List<tblPolicyCommisionPayment> policyCommissionPaymentData = unitOfWork.TblPolicyCommisionPaymentRepository.Get(x => x.PolicyInfoRecID == policyInfoRec.PolicyInfoRecID).ToList();

                    List<PolicyCommissionPaymentVM> policyCommissionPaymentList = new List<PolicyCommissionPaymentVM>();

                    foreach (var policyCommissionPayment in policyCommissionPaymentData)
                    {
                        PolicyCommissionPaymentVM policyCommissionPaymentVM = new PolicyCommissionPaymentVM();
                        policyCommissionPaymentVM.PolicyCommisionPaymentID = policyCommissionPayment.PolicyCommisionPaymentID;
                        policyCommissionPaymentVM.PolicyInfoRecID = policyCommissionPayment.PolicyInfoRecID != null ? Convert.ToInt32(policyCommissionPayment.PolicyInfoRecID) : 0;
                        policyCommissionPaymentVM.CommisionTypeID = policyCommissionPayment.CommisionTypeID != null ? Convert.ToInt32(policyCommissionPayment.CommisionTypeID) : 0;

                        if (policyCommissionPaymentVM.CommisionTypeID > 0)
                        {
                            policyCommissionPaymentVM.CommissionTypeName = policyCommissionPayment.tblCommisionType.CommisionType;
                        }

                        policyCommissionPaymentVM.CommisionValue = policyCommissionPayment.CommisionValue != null ? Convert.ToDecimal(policyCommissionPayment.CommisionValue) : 0;
                        policyCommissionPaymentVM.ComStructLineID = policyCommissionPayment.ComStructLineID != null ? Convert.ToInt32(policyCommissionPayment.ComStructLineID) : 0;

                        if (policyCommissionPaymentVM.ComStructLineID > 0)
                        {
                            policyCommissionPaymentVM.RateValue = policyCommissionPayment.tblCommisionStructureLine.RateValue != null ? Convert.ToDouble(policyCommissionPayment.tblCommisionStructureLine.RateValue) : 0;

                            if (policyCommissionPayment.tblCommisionStructureLine.ComStructID != null)
                            {
                                policyCommissionPaymentVM.PartnerID = policyCommissionPayment.tblCommisionStructureLine.tblCommisionStructureHeader.PartnerID != null ? Convert.ToInt32(policyCommissionPayment.tblCommisionStructureLine.tblCommisionStructureHeader.PartnerID) : 0;

                                if (policyCommissionPaymentVM.PartnerID > 0)
                                {
                                    policyCommissionPaymentVM.PartnerName = policyCommissionPayment.tblCommisionStructureLine.tblCommisionStructureHeader.tblPartner.PartnerName;
                                }
                            }
                        }

                        policyCommissionPaymentVM.CreatedBy = policyCommissionPayment.CreatedBy != null ? Convert.ToInt32(policyCommissionPayment.CreatedBy) : 0;
                        policyCommissionPaymentVM.CreatedDate = policyCommissionPayment.CreatedDate != null ? policyCommissionPayment.CreatedDate.ToString() : string.Empty;
                        policyCommissionPaymentVM.ModifiedBy = policyCommissionPayment.ModifiedBy != null ? Convert.ToInt32(policyCommissionPayment.ModifiedBy) : 0;
                        policyCommissionPaymentVM.ModifiedDate = policyCommissionPayment.ModifiedDate != null ? policyCommissionPayment.ModifiedDate.ToString() : string.Empty;

                        policyCommissionPaymentList.Add(policyCommissionPaymentVM);
                    }

                    policyInfoRecVM.PolicyCommissionPaymentDetails = policyCommissionPaymentList;

                    policyInfoRecVMList.Add(policyInfoRecVM);
                }

                return policyInfoRecVMList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<PolicyInfoRecVM> GetPolicyInfoRecordingsByClient(int clientID)
        {
            try
            {
                var policyInfoRecData = unitOfWork.TblPolicyInformationRecordingRepository.Get(x => x.tblQuotationHeader.tblClientRequestHeader.ClientID == clientID).ToList();

                List<PolicyInfoRecVM> policyInfoRecVMList = new List<PolicyInfoRecVM>();

                foreach (var policyInfoRec in policyInfoRecData)
                {
                    PolicyInfoRecVM policyInfoRecVM = new PolicyInfoRecVM();
                    policyInfoRecVM.PolicyInfoRecID = policyInfoRec.PolicyInfoRecID;
                    policyInfoRecVM.PolicyNumber = policyInfoRec.PolicyNumber;
                    policyInfoRecVM.QuotationHeaderID = policyInfoRec.QuotationHeaderID != null ? Convert.ToInt32(policyInfoRec.QuotationHeaderID) : 0;
                    policyInfoRecVM.QuotationDetailsInsCompanyLineID = policyInfoRec.QuotationDetailsInsCompanyLineID != null ? Convert.ToInt32(policyInfoRec.QuotationDetailsInsCompanyLineID) : 0;
                    policyInfoRecVM.SumAssured = policyInfoRec.SumAssured != null ? Convert.ToDecimal(policyInfoRec.SumAssured) : 0;
                    policyInfoRecVM.SumAssuredCurrencyTypeID = policyInfoRec.SumAssuredCurrencyTypeID != null ? Convert.ToInt32(policyInfoRec.SumAssuredCurrencyTypeID) : 0;

                    if (policyInfoRecVM.SumAssuredCurrencyTypeID > 0)
                    {
                        policyInfoRecVM.SumAssuredCurrencyCode = policyInfoRec.tblCurrency.CurrencyCode;
                    }

                    policyInfoRecVM.PremiumIncludingTax = policyInfoRec.PremiumIncludingTax != null ? Convert.ToDecimal(policyInfoRec.PremiumIncludingTax) : 0;
                    policyInfoRecVM.PremiumIncludingTaxCurrencyTypeID = policyInfoRec.PremiumIncludingTaxCurrencyTypeID != null ? Convert.ToInt32(policyInfoRec.PremiumIncludingTaxCurrencyTypeID) : 0;

                    if (policyInfoRecVM.PremiumIncludingTaxCurrencyTypeID > 0)
                    {
                        policyInfoRecVM.PremiumIncludingTaxCurrencyCode = policyInfoRec.tblCurrency1.CurrencyCode;
                    }

                    policyInfoRecVM.PeriodOfCoverFromDate = policyInfoRec.PeriodOfCoverFromDate != null ? Convert.ToDateTime(policyInfoRec.PeriodOfCoverFromDate).ToString("MM/dd/yyyy") : string.Empty;
                    policyInfoRecVM.PeriodOfCoverToDate = policyInfoRec.PeriodOfCoverToDate != null ? Convert.ToDateTime(policyInfoRec.PeriodOfCoverToDate).ToString("MM/dd/yyyy") : string.Empty;
                    policyInfoRecVM.OtherExcessDescription = policyInfoRec.OtherExcessDescription;
                    policyInfoRecVM.OtherExcessAmount = policyInfoRec.OtherExcessAmount != null ? Convert.ToDecimal(policyInfoRec.OtherExcessAmount) : 0;
                    policyInfoRecVM.TaxInvoiceNumber = policyInfoRec.TaxInvoiceNumber;
                    policyInfoRecVM.FileNumber = policyInfoRec.FileNumber;
                    policyInfoRecVM.IsActive = policyInfoRec.IsActive;
                    policyInfoRecVM.CreatedBy = policyInfoRec.CreatedBy != null ? Convert.ToInt32(policyInfoRec.CreatedBy) : 0;
                    policyInfoRecVM.CreatedDate = policyInfoRec.CreatedDate != null ? policyInfoRec.CreatedDate.ToString() : string.Empty;
                    policyInfoRecVM.ModifiedBy = policyInfoRec.ModifiedBy != null ? Convert.ToInt32(policyInfoRec.ModifiedBy) : 0;
                    policyInfoRecVM.ModifiedDate = policyInfoRec.ModifiedDate != null ? policyInfoRec.ModifiedDate.ToString() : string.Empty;

                    List<tblPolicyCommisionPayment> policyCommissionPaymentData = unitOfWork.TblPolicyCommisionPaymentRepository.Get(x => x.PolicyInfoRecID == policyInfoRec.PolicyInfoRecID).ToList();

                    List<PolicyCommissionPaymentVM> policyCommissionPaymentList = new List<PolicyCommissionPaymentVM>();

                    foreach (var policyCommissionPayment in policyCommissionPaymentData)
                    {
                        PolicyCommissionPaymentVM policyCommissionPaymentVM = new PolicyCommissionPaymentVM();
                        policyCommissionPaymentVM.PolicyCommisionPaymentID = policyCommissionPayment.PolicyCommisionPaymentID;
                        policyCommissionPaymentVM.PolicyInfoRecID = policyCommissionPayment.PolicyInfoRecID != null ? Convert.ToInt32(policyCommissionPayment.PolicyInfoRecID) : 0;
                        policyCommissionPaymentVM.CommisionTypeID = policyCommissionPayment.CommisionTypeID != null ? Convert.ToInt32(policyCommissionPayment.CommisionTypeID) : 0;

                        if (policyCommissionPaymentVM.CommisionTypeID > 0)
                        {
                            policyCommissionPaymentVM.CommissionTypeName = policyCommissionPayment.tblCommisionType.CommisionType;
                        }

                        policyCommissionPaymentVM.CommisionValue = policyCommissionPayment.CommisionValue != null ? Convert.ToDecimal(policyCommissionPayment.CommisionValue) : 0;
                        policyCommissionPaymentVM.ComStructLineID = policyCommissionPayment.ComStructLineID != null ? Convert.ToInt32(policyCommissionPayment.ComStructLineID) : 0;

                        if (policyCommissionPaymentVM.ComStructLineID > 0)
                        {
                            policyCommissionPaymentVM.RateValue = policyCommissionPayment.tblCommisionStructureLine.RateValue != null ? Convert.ToDouble(policyCommissionPayment.tblCommisionStructureLine.RateValue) : 0;

                            if (policyCommissionPayment.tblCommisionStructureLine.ComStructID != null)
                            {
                                policyCommissionPaymentVM.PartnerID = policyCommissionPayment.tblCommisionStructureLine.tblCommisionStructureHeader.PartnerID != null ? Convert.ToInt32(policyCommissionPayment.tblCommisionStructureLine.tblCommisionStructureHeader.PartnerID) : 0;

                                if (policyCommissionPaymentVM.PartnerID > 0)
                                {
                                    policyCommissionPaymentVM.PartnerName = policyCommissionPayment.tblCommisionStructureLine.tblCommisionStructureHeader.tblPartner.PartnerName;
                                }
                            }
                        }

                        policyCommissionPaymentVM.CreatedBy = policyCommissionPayment.CreatedBy != null ? Convert.ToInt32(policyCommissionPayment.CreatedBy) : 0;
                        policyCommissionPaymentVM.CreatedDate = policyCommissionPayment.CreatedDate != null ? policyCommissionPayment.CreatedDate.ToString() : string.Empty;
                        policyCommissionPaymentVM.ModifiedBy = policyCommissionPayment.ModifiedBy != null ? Convert.ToInt32(policyCommissionPayment.ModifiedBy) : 0;
                        policyCommissionPaymentVM.ModifiedDate = policyCommissionPayment.ModifiedDate != null ? policyCommissionPayment.ModifiedDate.ToString() : string.Empty;

                        policyCommissionPaymentList.Add(policyCommissionPaymentVM);
                    }

                    policyInfoRecVM.PolicyCommissionPaymentDetails = policyCommissionPaymentList;

                    policyInfoRecVMList.Add(policyInfoRecVM);
                }

                return policyInfoRecVMList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PolicyInfoRecVM GetPolicyInfoRecordingByID(int policyInfoRecID)
        {
            try
            {


                var policyInfoRecData = unitOfWork.TblPolicyInformationRecordingRepository.GetByID(policyInfoRecID);

                PolicyInfoRecVM policyInfoRecVM = new PolicyInfoRecVM();
                policyInfoRecVM.PolicyInfoRecID = policyInfoRecData.PolicyInfoRecID;
                policyInfoRecVM.PolicyNumber = policyInfoRecData.PolicyNumber;
                policyInfoRecVM.QuotationHeaderID = policyInfoRecData.QuotationHeaderID != null ? Convert.ToInt32(policyInfoRecData.QuotationHeaderID) : 0;
                policyInfoRecVM.QuotationDetailsInsCompanyLineID = policyInfoRecData.QuotationDetailsInsCompanyLineID != null ? Convert.ToInt32(policyInfoRecData.QuotationDetailsInsCompanyLineID) : 0;
                policyInfoRecVM.SumAssured = policyInfoRecData.SumAssured != null ? Convert.ToDecimal(policyInfoRecData.SumAssured) : 0;
                policyInfoRecVM.SumAssuredCurrencyTypeID = policyInfoRecData.SumAssuredCurrencyTypeID != null ? Convert.ToInt32(policyInfoRecData.SumAssuredCurrencyTypeID) : 0;
                policyInfoRecVM.PolicyRequestedDate = policyInfoRecData.PolicyRequestedDate != null ? Convert.ToDateTime(policyInfoRecData.PolicyRequestedDate).ToString("MM/dd/yyyy") : string.Empty;
                if (policyInfoRecVM.SumAssuredCurrencyTypeID > 0)
                {
                    policyInfoRecVM.SumAssuredCurrencyCode = policyInfoRecData.tblCurrency.CurrencyCode;
                }

                policyInfoRecVM.PremiumIncludingTax = policyInfoRecData.PremiumIncludingTax != null ? Convert.ToDecimal(policyInfoRecData.PremiumIncludingTax) : 0;
                policyInfoRecVM.PremiumIncludingTaxCurrencyTypeID = policyInfoRecData.PremiumIncludingTaxCurrencyTypeID != null ? Convert.ToInt32(policyInfoRecData.PremiumIncludingTaxCurrencyTypeID) : 0;

                if (policyInfoRecVM.PremiumIncludingTaxCurrencyTypeID > 0)
                {
                    policyInfoRecVM.PremiumIncludingTaxCurrencyCode = policyInfoRecData.tblCurrency1.CurrencyCode;
                }

                ///
                policyInfoRecVM.InsuranceCompanyID = (int)policyInfoRecData.InuranceCompanyId;
                policyInfoRecVM.InsuranceClassID = policyInfoRecData.InsClassId;
                policyInfoRecVM.InsuranceSubClassID = policyInfoRecData.InsSubClassId;
                policyInfoRecVM.PeriodOfCoverFromDate = policyInfoRecData.PeriodOfCoverFromDate != null ? Convert.ToDateTime(policyInfoRecData.PeriodOfCoverFromDate).ToString("MM/dd/yyyy") : string.Empty;
                policyInfoRecVM.PeriodOfCoverToDate = policyInfoRecData.PeriodOfCoverToDate != null ? Convert.ToDateTime(policyInfoRecData.PeriodOfCoverToDate).ToString("MM/dd/yyyy") : string.Empty;
                policyInfoRecVM.OtherExcessDescription = policyInfoRecData.OtherExcessDescription;
                policyInfoRecVM.NonCommissionPremium = policyInfoRecData.NonCommissionPremium != null ? Convert.ToDecimal(policyInfoRecData.NonCommissionPremium) : 0;
                policyInfoRecVM.GrossPremium = policyInfoRecData.GrossPremium != null ? Convert.ToDecimal(policyInfoRecData.GrossPremium) : 0;
                //  policyInfoRecVM.OtherExcessAmount = policyInfoRecData.OtherExcessAmount;
                policyInfoRecVM.VehicleNumber = policyInfoRecData.VehicleNumber;
                policyInfoRecVM.TransactionTypeID = policyInfoRecData.TransactionTypeID;
                policyInfoRecVM.CommissionStructureHeaderID = policyInfoRecData.CommissionStructureHeaderID;
                policyInfoRecVM.IntroducerID = policyInfoRecData.IntroducerID;
                policyInfoRecVM.AccountExecutiveID = policyInfoRecData.AccountExecutiveID;
                policyInfoRecVM.OtherExcessAmount = policyInfoRecData.OtherExcessAmount != null ? Convert.ToDecimal(policyInfoRecData.OtherExcessAmount) : 0;
                policyInfoRecVM.TaxInvoiceNumber = policyInfoRecData.TaxInvoiceNumber;
                policyInfoRecVM.NonCommissionPremium = policyInfoRecData.NonCommissionPremium != null ? Convert.ToDecimal(policyInfoRecData.NonCommissionPremium) : 0;
                policyInfoRecVM.GrossPremium = policyInfoRecData.GrossPremium != null ? Convert.ToDecimal(policyInfoRecData.GrossPremium) : 0;
                policyInfoRecVM.OtherExcessAmount = policyInfoRecData.OtherExcessAmount != null ? Convert.ToDecimal(policyInfoRecData.OtherExcessAmount) : 0;
                policyInfoRecVM.VehicleNumber = policyInfoRecData.VehicleNumber;
                policyInfoRecVM.TransactionTypeID = policyInfoRecData.TransactionTypeID;
                policyInfoRecVM.CommissionStructureHeaderID = policyInfoRecData.CommissionStructureHeaderID;


                policyInfoRecVM.TotalCommission = policyInfoRecData.TotalCommission != null ? Convert.ToDecimal(policyInfoRecData.TotalCommission) : 0;
                policyInfoRecVM.FileNumber = policyInfoRecData.FileNumber;
                policyInfoRecVM.IsActive = policyInfoRecData.IsActive;
                policyInfoRecVM.CreatedBy = policyInfoRecData.CreatedBy != null ? Convert.ToInt32(policyInfoRecData.CreatedBy) : 0;
                policyInfoRecVM.CreatedDate = policyInfoRecData.CreatedDate != null ? policyInfoRecData.CreatedDate.ToString() : string.Empty;
                policyInfoRecVM.ModifiedBy = policyInfoRecData.ModifiedBy != null ? Convert.ToInt32(policyInfoRecData.ModifiedBy) : 0;
                policyInfoRecVM.ModifiedDate = policyInfoRecData.ModifiedDate != null ? policyInfoRecData.ModifiedDate.ToString() : string.Empty;

                //var policyRenewalData = unitOfWork.TblPolicyRenewalHistoryRepository.Get(x => x.tblPolicyInformationRecording.PolicyInfoRecID == policyInfoRecID).FirstOrDefault();
                //PolicyRenewalHistoryVM policyRenewalVM = new PolicyRenewalHistoryVM();
                //policyRenewalVM.Agent = policyRenewalData.Agent != null ? Convert.ToInt32( policyRenewalData.Agent) : 0;



                var bankTrasData = unitOfWork.TblBankTransactionDetailRepository.Get(x => x.PolicyInfoRecID == policyInfoRecID).ToList();

                List<BankTransactionVM> bankDetailsVM = new List<BankTransactionVM>();

                foreach (var bankData in bankTrasData)
                {
                    BankTransactionVM bankVM = new BankTransactionVM();
                    bankVM.BankID = bankData.BankID;
                    bankVM.BankAmount = bankData.Amount != null ? Convert.ToDecimal(bankData.Amount) : 0;
                    bankVM.DraftNo = bankData.DraftNo;
                    bankVM.PaymentMethodID = bankData.PaymentID;
                    bankVM.AgentID = bankData.AgentID;
                    bankVM.AgentAmount = bankData.AgentAmount != null ? Convert.ToDecimal(bankData.AgentAmount) : 0;
                    bankVM.ClientID = bankData.ClientID;
                    bankVM.PolicyInfoRecID = bankData.PolicyInfoRecID;
                    bankVM.BankDetailID = bankData.BankDetailID;
                    //   Bank.IBSAmount = policyInfoPaymentVM.SGSAmount;
                    bankVM.RequestDate = bankData.RequestDate != null ? Convert.ToDateTime(bankData.RequestDate).ToString("dd/MM/yyyy") : string.Empty;
                    bankDetailsVM.Add(bankVM);
                }
                policyInfoRecVM.BankTransactionList = bankDetailsVM;


                List<tblPolicyCommisionPayment> policyCommissionPaymentData = unitOfWork.TblPolicyCommisionPaymentRepository.Get(x => x.PolicyInfoRecID == policyInfoRecData.PolicyInfoRecID).ToList();

                List<PolicyCommissionPaymentVM> policyCommissionPaymentList = new List<PolicyCommissionPaymentVM>();



                foreach (var policyCommissionPayment in policyCommissionPaymentData)
                {
                    PolicyCommissionPaymentVM policyCommissionPaymentVM = new PolicyCommissionPaymentVM();
                    policyCommissionPaymentVM.PolicyCommisionPaymentID = policyCommissionPayment.PolicyCommisionPaymentID;
                    policyCommissionPaymentVM.PolicyInfoRecID = policyCommissionPayment.PolicyInfoRecID != null ? Convert.ToInt32(policyCommissionPayment.PolicyInfoRecID) : 0;
                    policyCommissionPaymentVM.CommisionTypeID = policyCommissionPayment.CommisionTypeID != null ? Convert.ToInt32(policyCommissionPayment.CommisionTypeID) : 0;

                    if (policyCommissionPaymentVM.CommisionTypeID > 0)
                    {
                        policyCommissionPaymentVM.CommissionTypeName = policyCommissionPayment.tblCommisionType.CommisionType;
                    }
                    policyCommissionPaymentVM.Amount = policyCommissionPayment.Amount != null ? Convert.ToDecimal(policyCommissionPayment.Amount) : 0;
                    policyCommissionPaymentVM.CommisionValue = policyCommissionPayment.CommisionValue != null ? Convert.ToDecimal(policyCommissionPayment.CommisionValue) : 0;
                    policyCommissionPaymentVM.ComStructLineID = policyCommissionPayment.ComStructLineID != null ? Convert.ToInt32(policyCommissionPayment.ComStructLineID) : 0;

                    if (policyCommissionPaymentVM.ComStructLineID > 0)
                    {
                        policyCommissionPaymentVM.RateValue = policyCommissionPayment.tblCommisionStructureLine.RateValue != null ? Convert.ToDouble(policyCommissionPayment.tblCommisionStructureLine.RateValue) : 0;

                        if (policyCommissionPayment.tblCommisionStructureLine.ComStructID != null)
                        {
                            policyCommissionPaymentVM.PartnerID = policyCommissionPayment.tblCommisionStructureLine.tblCommisionStructureHeader.PartnerID != null ? Convert.ToInt32(policyCommissionPayment.tblCommisionStructureLine.tblCommisionStructureHeader.PartnerID) : 0;

                            if (policyCommissionPaymentVM.PartnerID > 0)
                            {
                                policyCommissionPaymentVM.PartnerName = policyCommissionPayment.tblCommisionStructureLine.tblCommisionStructureHeader.tblPartner.PartnerName;
                            }
                        }
                    }

                    policyCommissionPaymentVM.CreatedBy = policyCommissionPayment.CreatedBy != null ? Convert.ToInt32(policyCommissionPayment.CreatedBy) : 0;
                    policyCommissionPaymentVM.CreatedDate = policyCommissionPayment.CreatedDate != null ? policyCommissionPayment.CreatedDate.ToString() : string.Empty;
                    policyCommissionPaymentVM.ModifiedBy = policyCommissionPayment.ModifiedBy != null ? Convert.ToInt32(policyCommissionPayment.ModifiedBy) : 0;
                    policyCommissionPaymentVM.ModifiedDate = policyCommissionPayment.ModifiedDate != null ? policyCommissionPayment.ModifiedDate.ToString() : string.Empty;

                    policyCommissionPaymentList.Add(policyCommissionPaymentVM);
                }

                policyInfoRecVM.PolicyCommissionPaymentDetails = policyCommissionPaymentList;



                //List<tblPolicyInfoCharge> policyInfoChargeData = unitOfWork.TblPolicyInfoChargeRepository.Get(x => x.PolicyInfoPaymentID == policyInfoRecData.PolicyInfoRecID).ToList();
                //List<PolicyInfoChargeVM> policyInfoChargeList = new List<PolicyInfoChargeVM>();

                List<tblPolicyInfoCharge> policyInfoChargeData = unitOfWork.TblPolicyInfoChargeRepository.Get(x => x.PolicyInfoPaymentID == policyInfoRecData.PolicyInfoRecID).ToList();
                List<PolicyInfoChargeVM> policyInfoChargeList = new List<PolicyInfoChargeVM>();

                foreach (var policyInfoCharge in policyInfoChargeData)
                {
                    PolicyInfoChargeVM policyInfoChargeVM = new PolicyInfoChargeVM();
                    policyInfoChargeVM.PolicyInfoChargeID = policyInfoCharge.PolicyInfoChargeID;
                    policyInfoChargeVM.ChargeTypeID = policyInfoCharge.ChargeTypeID;
                    var chargeTypeDet = unitOfWork.TblChargeTypeRepository.GetByID(policyInfoCharge.ChargeTypeID);
                    policyInfoChargeVM.ChargeTypeName = chargeTypeDet.ChargeType;
                    policyInfoChargeVM.ComHeaderID = policyInfoCharge.ComHeaderID > 0 ? Convert.ToInt32(policyInfoCharge.ComHeaderID) : 0; ;
                    policyInfoChargeVM.Percentage = policyInfoCharge.Percentage != null ? Convert.ToDecimal(policyInfoCharge.Percentage) : 0; ;
                    policyInfoChargeVM.Amount = policyInfoCharge.Amount != null ? Convert.ToDecimal(policyInfoCharge.Amount) : 0;
                    //policyInfoChargeVM.IsCR = (bool)policyInfoCharge.IsCR;
                    policyInfoChargeVM.CreatedBy = policyInfoCharge.CreatedBy != null ? Convert.ToInt32(policyInfoCharge.CreatedBy) : 0; ;
                    policyInfoChargeVM.CreatedDate = policyInfoCharge.CreatedDate != null ? policyInfoCharge.CreatedDate.ToString() : string.Empty; ;
                    policyInfoChargeVM.ModifiedBy = policyInfoCharge.ModifiedBy != null ? Convert.ToInt32(policyInfoCharge.ModifiedBy) : 0; ;
                    policyInfoChargeVM.ModifiedDate = policyInfoCharge.ModifiedDate != null ? policyInfoCharge.ModifiedDate.ToString() : string.Empty; ;
                    policyInfoChargeList.Add(policyInfoChargeVM);

                }

                policyInfoRecVM.policyInfoChargeList = policyInfoChargeList;

                List<tblPolicyCommisionPayment_New> policyInfoChargeComData = unitOfWork.TblPolicyCommisionPayment_NewRepository.Get(x => x.PolicyInfoRecID == policyInfoRecData.PolicyInfoRecID).ToList();
                List<PolicyNewInfoChargeVM> PolicyNewInfoCharge = new List<PolicyNewInfoChargeVM>();

                foreach (var policyInfoCharge in policyInfoChargeComData)
                {
                    PolicyNewInfoChargeVM policyInfoChargeVM = new PolicyNewInfoChargeVM();
                    policyInfoChargeVM.PolicyInfoChargeID = policyInfoCharge.PolicyCommisionPaymentID;
                    policyInfoChargeVM.ChargeTypeID = (int)policyInfoCharge.ChargeTypeID;
                    var chargeTypeDet = unitOfWork.TblChargeTypeRepository.GetByID(policyInfoCharge.ChargeTypeID);
                    policyInfoChargeVM.ChargeTypeName = chargeTypeDet.ChargeType;
                    policyInfoChargeVM.Amount = policyInfoCharge.Amount != null ? Convert.ToDecimal(policyInfoCharge.Amount) : 0;

                    policyInfoChargeVM.CreatedBy = policyInfoCharge.CreatedBy != null ? Convert.ToInt32(policyInfoCharge.CreatedBy) : 0; ;
                    policyInfoChargeVM.CreatedDate = policyInfoCharge.CreatedDate != null ? policyInfoCharge.CreatedDate.ToString() : string.Empty; ;
                    policyInfoChargeVM.ModifiedBy = policyInfoCharge.ModifiedBy != null ? Convert.ToInt32(policyInfoCharge.ModifiedBy) : 0; ;
                    policyInfoChargeVM.ModifiedDate = policyInfoCharge.ModifiedDate != null ? policyInfoCharge.ModifiedDate.ToString() : string.Empty; ;
                    PolicyNewInfoCharge.Add(policyInfoChargeVM);

                }
                policyInfoRecVM.PolicyNewInfoCharge = PolicyNewInfoCharge;

                return policyInfoRecVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Policy Renewal History
        public bool SavePolicyRenewalHistory(PolicyRenewalHistoryVM policyRenewalHistoryVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblPolicyRenewalHistory policyRenewalHistory = new tblPolicyRenewalHistory();
                    policyRenewalHistory.PolicyInfoRecID = policyRenewalHistoryVM.PolicyInfoRecID;
                    policyRenewalHistory.RenewalDate = !string.IsNullOrEmpty(policyRenewalHistoryVM.RenewalDate) ? DateTime.ParseExact(policyRenewalHistoryVM.RenewalDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    policyRenewalHistory.NotificationDate = !string.IsNullOrEmpty(policyRenewalHistoryVM.NotificationDate) ? DateTime.ParseExact(policyRenewalHistoryVM.NotificationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    policyRenewalHistory.IsSent = policyRenewalHistoryVM.IsSent;
                    policyRenewalHistory.IsCancel = policyRenewalHistoryVM.IsCancel;
                    policyRenewalHistory.IsRenewal= policyRenewalHistoryVM.IsCancel;
                    policyRenewalHistory.CreatedDate = DateTime.Now;
                    policyRenewalHistory.CreatedBy = policyRenewalHistoryVM.CreatedBy;
                    policyRenewalHistory.Agent = policyRenewalHistoryVM.Agent > 0 ? policyRenewalHistoryVM.Agent : 0;
                    policyRenewalHistory.Executive= policyRenewalHistoryVM.Executive > 0 ? policyRenewalHistoryVM.Executive : 0;
                    policyRenewalHistory.RenewalStartDate= !string.IsNullOrEmpty(policyRenewalHistoryVM.RenewalStartDate) ? DateTime.ParseExact(policyRenewalHistoryVM.RenewalStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    policyRenewalHistory.RenewalEndDate= !string.IsNullOrEmpty(policyRenewalHistoryVM.RenewalEndDate) ? DateTime.ParseExact(policyRenewalHistoryVM.RenewalEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    unitOfWork.TblPolicyRenewalHistoryRepository.Insert(policyRenewalHistory);
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

        public bool UpdatePolicyRenewalHistory(PolicyRenewalHistoryVM policyRenewalHistoryVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    //tblPolicyRenewalHistory policyRenewalHistory = unitOfWork.TblPolicyRenewalHistoryRepository.GetByID(policyRenewalHistoryVM.PolicyRenewalHistoryID);
                    //policyRenewalHistory.PolicyInfoRecID = policyRenewalHistoryVM.PolicyInfoRecID;
                    //policyRenewalHistory.RenewalDate = !string.IsNullOrEmpty(policyRenewalHistoryVM.RenewalDate) ? DateTime.ParseExact(policyRenewalHistoryVM.RenewalDate, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    //policyRenewalHistory.NotificationDate = !string.IsNullOrEmpty(policyRenewalHistoryVM.NotificationDate) ? DateTime.ParseExact(policyRenewalHistoryVM.NotificationDate, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    //policyRenewalHistory.IsSent = policyRenewalHistoryVM.IsSent;
                    //policyRenewalHistory.IsCancel = policyRenewalHistoryVM.IsCancel;
                    //policyRenewalHistory.IsRenewal = policyRenewalHistoryVM.IsCancel;
                    //policyRenewalHistory.ModifiedDate = DateTime.Now;
                    //policyRenewalHistory.ModifiedBy = policyRenewalHistoryVM.ModifiedBy;
                    //policyRenewalHistory.Agent = policyRenewalHistoryVM.Agent > 0 ? policyRenewalHistoryVM.Agent : 0;
                    //policyRenewalHistory.Executive = policyRenewalHistoryVM.Executive > 0 ? policyRenewalHistoryVM.Executive : 0;
                    //policyRenewalHistory.RenewalStartDate = !string.IsNullOrEmpty(policyRenewalHistoryVM.RenewalStartDate) ? DateTime.ParseExact(policyRenewalHistoryVM.RenewalStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    //policyRenewalHistory.RenewalEndDate = !string.IsNullOrEmpty(policyRenewalHistoryVM.RenewalEndDate) ? DateTime.ParseExact(policyRenewalHistoryVM.RenewalEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    //unitOfWork.TblPolicyRenewalHistoryRepository.Update(policyRenewalHistory);
                    //unitOfWork.Save();

                    tblPolicyInformationRecording policyRecording = unitOfWork.TblPolicyInformationRecordingRepository.GetByID(policyRenewalHistoryVM.PolicyInfoRecID);
                    policyRecording.PeriodOfCoverFromDate = !string.IsNullOrEmpty(policyRenewalHistoryVM.RenewalStartDate) ? DateTime.ParseExact(policyRenewalHistoryVM.RenewalStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    policyRecording.PeriodOfCoverToDate = !string.IsNullOrEmpty(policyRenewalHistoryVM.RenewalEndDate) ? DateTime.ParseExact(policyRenewalHistoryVM.RenewalEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    policyRecording.PolicyRequestedDate = !string.IsNullOrEmpty(policyRenewalHistoryVM.RenewalDate) ? DateTime.ParseExact(policyRenewalHistoryVM.RenewalDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    unitOfWork.TblPolicyInformationRecordingRepository.Update(policyRecording);
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

        public bool DeletePolicyRenewalHistory(int policyRenewalHistoryID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblPolicyRenewalHistory policyRenewalHistory = unitOfWork.TblPolicyRenewalHistoryRepository.GetByID(policyRenewalHistoryID);
                    unitOfWork.TblPolicyRenewalHistoryRepository.Delete(policyRenewalHistory);
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

        public List<PolicyRenewalHistoryVM> GetAllPolicyRenewalHistories()
        {
            try
            {
                var policyRenewalHistoryData = unitOfWork.TblPolicyRenewalHistoryRepository.Get().ToList();

                List<PolicyRenewalHistoryVM> policyRenewalHistoryList = new List<PolicyRenewalHistoryVM>();

                foreach (var policyRenewalHistory in policyRenewalHistoryData)
                {
                    PolicyRenewalHistoryVM policyRenewalHistoryVM = new PolicyRenewalHistoryVM();
                    policyRenewalHistoryVM.PolicyRenewalHistoryID = policyRenewalHistory.PolicyRenewalHistoryID;
                    policyRenewalHistoryVM.PolicyInfoRecID = policyRenewalHistory.PolicyInfoRecID != null ? Convert.ToInt32(policyRenewalHistory.PolicyInfoRecID) : 0;
                    policyRenewalHistoryVM.RenewalDate = policyRenewalHistory.RenewalDate != null ? policyRenewalHistory.RenewalDate.ToString() : string.Empty;
                    policyRenewalHistoryVM.NotificationDate = policyRenewalHistory.NotificationDate != null ? policyRenewalHistory.NotificationDate.ToString() : string.Empty;
                    policyRenewalHistoryVM.CreatedBy = policyRenewalHistory.CreatedBy != null ? Convert.ToInt32(policyRenewalHistory.CreatedBy) : 0;
                    policyRenewalHistoryVM.CreatedDate = policyRenewalHistory.CreatedDate != null ? policyRenewalHistory.CreatedDate.ToString() : string.Empty;
                    policyRenewalHistoryVM.ModifiedBy = policyRenewalHistory.ModifiedBy != null ? Convert.ToInt32(policyRenewalHistory.ModifiedBy) : 0;
                    policyRenewalHistoryVM.ModifiedDate = policyRenewalHistory.ModifiedDate != null ? policyRenewalHistory.ModifiedDate.ToString() : string.Empty;
                    policyRenewalHistoryVM.Agent = policyRenewalHistoryVM.Agent > 0 ? policyRenewalHistoryVM.Agent : 0;
                    policyRenewalHistoryVM.Executive = policyRenewalHistoryVM.Executive > 0 ? policyRenewalHistoryVM.Executive : 0;
                    policyRenewalHistoryVM.RenewalStartDate = policyRenewalHistoryVM.RenewalStartDate!=null ? policyRenewalHistory.RenewalStartDate.ToString() : string.Empty;
                    policyRenewalHistoryVM.RenewalEndDate = policyRenewalHistoryVM.RenewalEndDate!=null ? policyRenewalHistory.RenewalEndDate.ToString() : string.Empty;
                    policyRenewalHistoryList.Add(policyRenewalHistoryVM);
                }

                return policyRenewalHistoryList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<PolicyRenewalHistoryVM> GetAllPolicyRenewalHistoriesByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var policyRenewalHistoryData = unitOfWork.TblPolicyRenewalHistoryRepository.Get(x => x.tblPolicyInformationRecording.tblQuotationHeader.tblClientRequestHeader.tblClient.BUID == businessUnitID).ToList();

                List<PolicyRenewalHistoryVM> policyRenewalHistoryList = new List<PolicyRenewalHistoryVM>();

                foreach (var policyRenewalHistory in policyRenewalHistoryData)
                {
                    PolicyRenewalHistoryVM policyRenewalHistoryVM = new PolicyRenewalHistoryVM();
                    policyRenewalHistoryVM.PolicyRenewalHistoryID = policyRenewalHistory.PolicyRenewalHistoryID;
                    policyRenewalHistoryVM.PolicyInfoRecID = policyRenewalHistory.PolicyInfoRecID != null ? Convert.ToInt32(policyRenewalHistory.PolicyInfoRecID) : 0;
                    policyRenewalHistoryVM.RenewalDate = policyRenewalHistory.RenewalDate != null ? policyRenewalHistory.RenewalDate.ToString() : string.Empty;
                    policyRenewalHistoryVM.NotificationDate = policyRenewalHistory.NotificationDate != null ? policyRenewalHistory.NotificationDate.ToString() : string.Empty;
                    policyRenewalHistoryVM.CreatedBy = policyRenewalHistory.CreatedBy != null ? Convert.ToInt32(policyRenewalHistory.CreatedBy) : 0;
                    policyRenewalHistoryVM.CreatedDate = policyRenewalHistory.CreatedDate != null ? policyRenewalHistory.CreatedDate.ToString() : string.Empty;
                    policyRenewalHistoryVM.ModifiedBy = policyRenewalHistory.ModifiedBy != null ? Convert.ToInt32(policyRenewalHistory.ModifiedBy) : 0;
                    policyRenewalHistoryVM.ModifiedDate = policyRenewalHistory.ModifiedDate != null ? policyRenewalHistory.ModifiedDate.ToString() : string.Empty;
                    policyRenewalHistoryVM.Agent = policyRenewalHistoryVM.Agent > 0 ? policyRenewalHistoryVM.Agent : 0;
                    policyRenewalHistoryVM.Executive = policyRenewalHistoryVM.Executive > 0 ? policyRenewalHistoryVM.Executive : 0;
                    policyRenewalHistoryVM.RenewalStartDate = policyRenewalHistoryVM.RenewalStartDate != null ? policyRenewalHistory.RenewalStartDate.ToString() : string.Empty;
                    policyRenewalHistoryVM.RenewalEndDate = policyRenewalHistoryVM.RenewalEndDate != null ? policyRenewalHistory.RenewalEndDate.ToString() : string.Empty;
                    policyRenewalHistoryList.Add(policyRenewalHistoryVM);
                }

                return policyRenewalHistoryList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<PolicyRenewalHistoryVM> GetPolicyRenewalHistoriesByPolicyInfoRecID(int policyInfoRecID)
        {
            try
            {
                var policyRenewalHistoryData = unitOfWork.TblPolicyRenewalHistoryRepository.Get(x => x.PolicyInfoRecID == policyInfoRecID).ToList();

                List<PolicyRenewalHistoryVM> policyRenewalHistoryList = new List<PolicyRenewalHistoryVM>();

                foreach (var policyRenewalHistory in policyRenewalHistoryData)
                {
                    PolicyRenewalHistoryVM policyRenewalHistoryVM = new PolicyRenewalHistoryVM();
                    policyRenewalHistoryVM.PolicyRenewalHistoryID = policyRenewalHistory.PolicyRenewalHistoryID;
                    policyRenewalHistoryVM.PolicyInfoRecID = policyRenewalHistory.PolicyInfoRecID != null ? Convert.ToInt32(policyRenewalHistory.PolicyInfoRecID) : 0;
                    policyRenewalHistoryVM.RenewalDate = policyRenewalHistory.RenewalDate != null ? policyRenewalHistory.RenewalDate.ToString() : string.Empty;
                    policyRenewalHistoryVM.NotificationDate = policyRenewalHistory.NotificationDate != null ? policyRenewalHistory.NotificationDate.ToString() : string.Empty;
                    policyRenewalHistoryVM.CreatedBy = policyRenewalHistory.CreatedBy != null ? Convert.ToInt32(policyRenewalHistory.CreatedBy) : 0;
                    policyRenewalHistoryVM.CreatedDate = policyRenewalHistory.CreatedDate != null ? policyRenewalHistory.CreatedDate.ToString() : string.Empty;
                    policyRenewalHistoryVM.ModifiedBy = policyRenewalHistory.ModifiedBy != null ? Convert.ToInt32(policyRenewalHistory.ModifiedBy) : 0;
                    policyRenewalHistoryVM.ModifiedDate = policyRenewalHistory.ModifiedDate != null ? policyRenewalHistory.ModifiedDate.ToString() : string.Empty;
                    policyRenewalHistoryVM.Agent = policyRenewalHistoryVM.Agent > 0 ? policyRenewalHistoryVM.Agent : 0;
                    policyRenewalHistoryVM.Executive = policyRenewalHistoryVM.Executive > 0 ? policyRenewalHistoryVM.Executive : 0;
                    policyRenewalHistoryVM.RenewalStartDate = policyRenewalHistoryVM.RenewalStartDate != null ? policyRenewalHistory.RenewalStartDate.ToString() : string.Empty;
                    policyRenewalHistoryVM.RenewalEndDate = policyRenewalHistoryVM.RenewalEndDate != null ? policyRenewalHistory.RenewalEndDate.ToString() : string.Empty;
                    policyRenewalHistoryList.Add(policyRenewalHistoryVM);
                }

                return policyRenewalHistoryList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PolicyRenewalHistoryVM GetPolicyRenewalHistoryByID(int policyRenewalHistoryID)
        {
            try
            {
                var policyRenewalHistoryData = unitOfWork.TblPolicyRenewalHistoryRepository.GetByID(policyRenewalHistoryID);

                PolicyRenewalHistoryVM policyRenewalHistoryVM = new PolicyRenewalHistoryVM();
                policyRenewalHistoryVM.PolicyRenewalHistoryID = policyRenewalHistoryData.PolicyRenewalHistoryID;
                policyRenewalHistoryVM.PolicyInfoRecID = policyRenewalHistoryData.PolicyInfoRecID != null ? Convert.ToInt32(policyRenewalHistoryData.PolicyInfoRecID) : 0;
                policyRenewalHistoryVM.RenewalDate = policyRenewalHistoryData.RenewalDate != null ? policyRenewalHistoryData.RenewalDate.ToString() : string.Empty;
                policyRenewalHistoryVM.NotificationDate = policyRenewalHistoryData.NotificationDate != null ? policyRenewalHistoryData.NotificationDate.ToString() : string.Empty;
                policyRenewalHistoryVM.RenewalDate = policyRenewalHistoryData.RenewalDate != null ? Convert.ToDateTime(policyRenewalHistoryData.RenewalDate).ToString("MM/dd/yyyy") : string.Empty;
                policyRenewalHistoryVM.NotificationDate = policyRenewalHistoryData.NotificationDate != null ? Convert.ToDateTime(policyRenewalHistoryData.NotificationDate).ToString("MM/dd/yyyy") : string.Empty;
                policyRenewalHistoryVM.IsSent = policyRenewalHistoryData.IsSent;
                policyRenewalHistoryVM.IsCancel = policyRenewalHistoryData.IsCancel;
                policyRenewalHistoryVM.IsRenewal = (bool)policyRenewalHistoryData.IsRenewal;
                policyRenewalHistoryVM.CreatedBy = policyRenewalHistoryData.CreatedBy != null ? Convert.ToInt32(policyRenewalHistoryData.CreatedBy) : 0;
                policyRenewalHistoryVM.CreatedDate = policyRenewalHistoryData.CreatedDate != null ? policyRenewalHistoryData.CreatedDate.ToString() : string.Empty;
                policyRenewalHistoryVM.ModifiedBy = policyRenewalHistoryData.ModifiedBy != null ? Convert.ToInt32(policyRenewalHistoryData.ModifiedBy) : 0;
                policyRenewalHistoryVM.ModifiedDate = policyRenewalHistoryData.ModifiedDate != null ? policyRenewalHistoryData.ModifiedDate.ToString() : string.Empty;

                return policyRenewalHistoryVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



    
        #endregion


        
    }
}
