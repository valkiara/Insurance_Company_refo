using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.TransactionData
{
    public class ManagePayment
    {
        private UnitOfWork unitOfWork;
        public ManagePayment()
        {
            unitOfWork = new UnitOfWork();
        }

        #region Manage Payment
        public bool SavePayment(PaymentVM paymentVM, int userID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    //Save Payment
                    tblPayment payment = new tblPayment();
                    payment.ClientID = paymentVM.ClientID;
                    payment.PaymentAmount = paymentVM.PaymentAmount;
                    payment.CreatedBy = userID;
                    payment.CreatedDate = DateTime.Now;
                   
                    unitOfWork.TblPaymentRepository.Insert(payment);
                    unitOfWork.Save();

                    //Save Debit Note
                    foreach (var debitNoteVM in paymentVM.DebitNoteList)
                    {
                        tblDebitNote debitNote = new tblDebitNote();
                        debitNote.TotalNonCommissionPremium = debitNoteVM.TotalNonCommissionPremium;
                        debitNote.TotalGrossPremium = debitNoteVM.TotalGrossPremium;
                        debitNote.CreatedBy = userID;
                        debitNote.CreatedDate = DateTime.Now;
                        unitOfWork.TblDebitNoteRepository.Insert(debitNote);
                        unitOfWork.Save();

                       
                        //Save Policy Info Payments
                        foreach (var policyInfoPaymentVM in debitNoteVM.PolicyInfoPaymentLists)
                        {
                            tblBankTransactionDetail Bank = new tblBankTransactionDetail();
                            Bank.BankID = policyInfoPaymentVM.BankID;
                            Bank.DraftNo = policyInfoPaymentVM.DraftNo;
                            Bank.PaymentID = policyInfoPaymentVM.PaymentMethodID;
                            Bank.Amount = policyInfoPaymentVM.BankAmount;
                            Bank.AgentID = policyInfoPaymentVM.AgentID;
                            Bank.AgentAmount = policyInfoPaymentVM.AgentAmount;
                            Bank.ClientID = paymentVM.ClientID;
                            Bank.PolicyInfoRecID = debitNoteVM.PolicyInfoRecID;
                            Bank.currencyType = policyInfoPaymentVM.currencyType;
                            Bank.ExchangeRate = policyInfoPaymentVM.ExchangeRate;
                            Bank.PaymentDate = !string.IsNullOrEmpty(policyInfoPaymentVM.PaymentDate) ? DateTime.ParseExact(policyInfoPaymentVM.PaymentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            //   Bank.IBSAmount = policyInfoPaymentVM.SGSAmount;
                            Bank.RequestDate = !string.IsNullOrEmpty(policyInfoPaymentVM.RequestDate) ? DateTime.ParseExact(policyInfoPaymentVM.RequestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            unitOfWork.TblBankTransactionDetailRepository.Insert(Bank);
                            unitOfWork.Save();

                            tblPolicyInfoPayment policyInfoPayment = new tblPolicyInfoPayment();
                            policyInfoPayment.PolicyInfoRecID = debitNoteVM.PolicyInfoRecID;
                            policyInfoPayment.NonCommissionPremium = debitNoteVM.TotalNonCommissionPremium;
                            policyInfoPayment.GrossPremium = policyInfoPaymentVM.BankAmount;
                            policyInfoPayment.CreatedBy = userID;
                            policyInfoPayment.CreatedDate = DateTime.Now;
                            unitOfWork.TblPolicyInfoPaymentRepository.Insert(policyInfoPayment);
                            unitOfWork.Save();

                            //foreach (var policyInfoPaymentObj in debitNoteVM.PolicyInfoPaymentList)
                            //{
                                tblPolicyDebitNote policyDebitNote = new tblPolicyDebitNote();
                                policyDebitNote.PolicyInfoPaymentID = policyInfoPayment.PolicyInfoPaymentID;
                                policyDebitNote.DebitNoteID = debitNote.DebitNoteID;
                                policyDebitNote.PaymentID = payment.PaymentID;
                                unitOfWork.TblPolicyDebitNoteRepository.Insert(policyDebitNote);
                                unitOfWork.Save();
                          //  }

                            //   policyInfoPaymentVM.PolicyInfoPaymentID = policyInfoPayment.PolicyInfoPaymentID;

                            //Save Policy Info Charges
                            //foreach (var policyInfoChargeVM in policyInfoPaymentVM.PolicyInfoChargeList)
                            //{
                            //    tblPolicyInfoCharge policyInfoCharge = new tblPolicyInfoCharge();
                            //    policyInfoCharge.PolicyInfoPaymentID = policyInfoPaymentVM.PolicyInfoRecID;
                            //    policyInfoCharge.ChargeTypeID = policyInfoChargeVM.ChargeTypeID;
                            //    policyInfoCharge.Amount = policyInfoChargeVM.Amount;
                            //    policyInfoCharge.IsCR = policyInfoChargeVM.IsCR;
                            //    policyInfoCharge.CreatedBy = userID;
                            //    policyInfoCharge.CreatedDate = DateTime.Now;
                            //    unitOfWork.TblPolicyInfoChargeRepository.Insert(policyInfoCharge);
                            //    unitOfWork.Save();
                            //}
                            //}

                            //Save Policy Info Payment - Debit Note

                        } }

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

        public bool UpdatePayment(PaymentVM paymentVM, int userID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    //Update Payment
                    tblPayment payment = unitOfWork.TblPaymentRepository.GetByID(paymentVM.PaymentID);
                    payment.ClientID = paymentVM.ClientID;
                    payment.PaymentAmount = paymentVM.PaymentAmount;
                    payment.ModifiedBy = userID;
                    payment.ModifiedDate = DateTime.Now;
                    unitOfWork.TblPaymentRepository.Update(payment);
                    unitOfWork.Save();

                    List<tblPolicyDebitNote> policyDebitNoteList = unitOfWork.TblPolicyDebitNoteRepository.Get(x => x.PaymentID == payment.PaymentID).ToList();
                    List<int> debitNoteList = policyDebitNoteList.GroupBy(x => x.DebitNoteID).Select(x => x.FirstOrDefault()).Select(x => (int)x.DebitNoteID).ToList();
                    List<int> policyInfoPaymentList = policyDebitNoteList.GroupBy(x => x.PolicyInfoPaymentID).Select(x => x.FirstOrDefault()).Select(x => (int)x.PolicyInfoPaymentID).ToList();

                    //Delete Existing Policy Debit Note Details
                    foreach (var policyDebitNote in policyDebitNoteList)
                    {
                        unitOfWork.TblPolicyDebitNoteRepository.Delete(policyDebitNote.PolicyDebitNoteID);
                        unitOfWork.Save();
                    }

                    //Delete Existing Debit Note Details
                    foreach (var debitNote in debitNoteList)
                    {
                        unitOfWork.TblDebitNoteRepository.Delete(debitNote);
                        unitOfWork.Save();
                    }

                    //Delete Policy Info Payment and Policy Info Charge Details
                    foreach (var policyInfoPayment in policyInfoPaymentList)
                    {
                        List<tblPolicyInfoCharge> policyInfoChargeList = unitOfWork.TblPolicyInfoChargeRepository.Get(x => x.PolicyInfoPaymentID == policyInfoPayment).ToList();

                        foreach (var policyInfoCharge in policyInfoChargeList)
                        {
                            unitOfWork.TblPolicyInfoChargeRepository.Delete(policyInfoCharge);
                            unitOfWork.Save();
                        }

                        unitOfWork.TblPolicyInfoPaymentRepository.Delete(policyInfoPayment);
                        unitOfWork.Save();
                    }

                    //Save Debit Note
                    foreach (var debitNoteVM in paymentVM.DebitNoteList)
                    {
                        tblDebitNote debitNote = new tblDebitNote();
                        debitNote.TotalNonCommissionPremium = debitNoteVM.TotalNonCommissionPremium;
                        debitNote.TotalGrossPremium = debitNoteVM.TotalGrossPremium;
                        debitNote.CreatedBy = payment.CreatedBy;
                        debitNote.CreatedDate = payment.CreatedDate;
                        debitNote.ModifiedBy = userID;
                        debitNote.ModifiedDate = DateTime.Now;
                        unitOfWork.TblDebitNoteRepository.Insert(debitNote);
                        unitOfWork.Save();

                        //Save Policy Info Payments
                        //foreach (var policyInfoPaymentVM in debitNoteVM.PolicyInfoPaymentList)
                        //{
                        //    tblPolicyInfoPayment policyInfoPayment = new tblPolicyInfoPayment();
                        //    policyInfoPayment.PolicyInfoRecID = policyInfoPaymentVM.PolicyInfoRecID;
                        //    unitOfWork.TblBankTransactionDetailRepository.Delete(policyInfoPayment.PolicyInfoRecID);
                        //    unitOfWork.Save();
                        //    policyInfoPayment.NonCommissionPremium = policyInfoPaymentVM.NonCommissionPremium;
                        //    policyInfoPayment.GrossPremium = policyInfoPaymentVM.GrossPremium;
                        //    policyInfoPayment.CreatedBy = payment.CreatedBy;
                        //    policyInfoPayment.CreatedDate = payment.CreatedDate;
                        //    policyInfoPayment.ModifiedBy = userID;
                        //    policyInfoPayment.ModifiedDate = DateTime.Now;
                        //    unitOfWork.TblPolicyInfoPaymentRepository.Insert(policyInfoPayment);
                        //    unitOfWork.Save();

                        //    policyInfoPaymentVM.PolicyInfoPaymentID = policyInfoPayment.PolicyInfoPaymentID;
                        List<tblBankTransactionDetail> BankTransactionDetailList = unitOfWork.TblBankTransactionDetailRepository.Get(x => x.PolicyInfoRecID == debitNoteVM.PolicyInfoRecID).ToList();
                        foreach (var BankTransactionDetail in BankTransactionDetailList)
                        {
                            unitOfWork.TblBankTransactionDetailRepository.Delete(BankTransactionDetail);
                            unitOfWork.Save();
                        }


                        foreach (var policyInfoPaymentVMs in debitNoteVM.PolicyInfoPaymentLists)
                            {

                            
                            tblBankTransactionDetail Bank = new tblBankTransactionDetail();
                                Bank.BankID = policyInfoPaymentVMs.BankID;
                                Bank.DraftNo = policyInfoPaymentVMs.DraftNo;
                                Bank.PaymentID = policyInfoPaymentVMs.PaymentMethodID;
                                Bank.Amount = policyInfoPaymentVMs.BankAmount;
                                Bank.AgentID = policyInfoPaymentVMs.AgentID;
                                Bank.AgentAmount = policyInfoPaymentVMs.AgentAmount;
                                Bank.ClientID = paymentVM.ClientID;
                                Bank.PolicyInfoRecID = policyInfoPaymentVMs.PolicyInfoRecID;
                                Bank.PolicyInfoRecID = debitNoteVM.PolicyInfoRecID;
                            //   Bank.IBSAmount = policyInfoPaymentVM.SGSAmount;
                            Bank.RequestDate = !string.IsNullOrEmpty(policyInfoPaymentVMs.RequestDate) ? DateTime.ParseExact(policyInfoPaymentVMs.RequestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                unitOfWork.TblBankTransactionDetailRepository.Insert(Bank);
                                unitOfWork.Save();
                            tblPolicyInfoPayment policyInfoPayment = new tblPolicyInfoPayment();
                            policyInfoPayment.PolicyInfoRecID = policyInfoPaymentVMs.PolicyInfoRecID;
                            policyInfoPayment.NonCommissionPremium = debitNoteVM.TotalNonCommissionPremium;
                            policyInfoPayment.GrossPremium = policyInfoPaymentVMs.BankAmount;
                            policyInfoPayment.CreatedBy = userID;
                            policyInfoPayment.CreatedDate = DateTime.Now;
                            unitOfWork.TblPolicyInfoPaymentRepository.Insert(policyInfoPayment);
                            unitOfWork.Save();

                            //foreach (var policyInfoPaymentObj in debitNoteVM.PolicyInfoPaymentList)
                            //{
                                tblPolicyDebitNote policyDebitNote = new tblPolicyDebitNote();
                                policyDebitNote.PolicyInfoPaymentID = policyInfoPayment.PolicyInfoPaymentID;
                                policyDebitNote.DebitNoteID = debitNote.DebitNoteID;
                                policyDebitNote.PaymentID = payment.PaymentID;
                                unitOfWork.TblPolicyDebitNoteRepository.Insert(policyDebitNote);
                                unitOfWork.Save();
                            //}

                        }
                                //Save Policy Info Charges
                            //    foreach (var policyInfoChargeVM in policyInfoPaymentVM.PolicyInfoChargeList)
                            //{
                            //    tblPolicyInfoCharge policyInfoCharge = new tblPolicyInfoCharge();
                            //    policyInfoCharge.PolicyInfoPaymentID = policyInfoPaymentVM.PolicyInfoRecID;
                            //    policyInfoCharge.ChargeTypeID = policyInfoChargeVM.ChargeTypeID;
                            //    policyInfoCharge.Amount = policyInfoChargeVM.Amount;
                            //    policyInfoCharge.IsCR = policyInfoChargeVM.IsCR;
                            //    policyInfoCharge.CreatedBy = payment.CreatedBy;
                            //    policyInfoCharge.CreatedDate = payment.CreatedDate;
                            //    policyInfoCharge.ModifiedBy = userID;
                            //    policyInfoCharge.ModifiedDate = DateTime.Now;
                            //    unitOfWork.TblPolicyInfoChargeRepository.Insert(policyInfoCharge);
                            //    unitOfWork.Save();
                            //}
                       // }

                        //Save Policy Info Payment - Debit Note
                        
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

        public List<PaymentVM> GetAllPayments()
        {
            try
            {
                ManagePolicyInfoRecording managePolicyInfoRecording = new ManagePolicyInfoRecording();

                var paymentData = unitOfWork.TblPaymentRepository.Get().ToList();

                List<PaymentVM> paymentVMList = new List<PaymentVM>();

                foreach (var payment in paymentData)
                {
                    PaymentVM paymentVM = new PaymentVM();
                    paymentVM.PaymentID = payment.PaymentID;
                    paymentVM.ClientID = payment.ClientID != null ? Convert.ToInt32(payment.ClientID) : 0;

                    if (paymentVM.ClientID > 0)
                    {
                        paymentVM.ClientName = payment.tblClient.ClientName;
                    }

                    paymentVM.PaymentAmount = payment.PaymentAmount != null ? Convert.ToDecimal(payment.PaymentAmount) : 0;
                    paymentVM.CreatedBy = payment.CreatedBy != null ? Convert.ToInt32(payment.CreatedBy) : 0;
                    paymentVM.CreatedDate = payment.CreatedDate != null ? payment.CreatedDate.ToString() : string.Empty;
                    paymentVM.ModifiedBy = payment.ModifiedBy != null ? Convert.ToInt32(payment.ModifiedBy) : 0;
                    paymentVM.ModifiedDate = payment.ModifiedDate != null ? payment.ModifiedDate.ToString() : string.Empty;

                    List<tblPolicyDebitNote> policyDebitNoteList = unitOfWork.TblPolicyDebitNoteRepository.Get(x => x.PaymentID == payment.PaymentID).ToList();
                    List<tblPolicyDebitNote> debitNoteList = policyDebitNoteList.GroupBy(x => x.DebitNoteID).Select(x => x.FirstOrDefault()).ToList();

                    List<DebitNoteVM> debitNoteVMList = new List<DebitNoteVM>();

                    foreach (var debitNote in debitNoteList)
                    {
                        DebitNoteVM debitNoteVM = new DebitNoteVM();
                        debitNoteVM.DebitNoteID = debitNote.tblDebitNote.DebitNoteID;
                        debitNoteVM.TotalNonCommissionPremium = debitNote.tblDebitNote.TotalNonCommissionPremium != null ? Convert.ToDecimal(debitNote.tblDebitNote.TotalNonCommissionPremium) : 0;
                        debitNoteVM.TotalGrossPremium = debitNote.tblDebitNote.TotalGrossPremium != null ? Convert.ToDecimal(debitNote.tblDebitNote.TotalGrossPremium) : 0;
                        debitNoteVM.CreatedBy = debitNote.tblDebitNote.CreatedBy != null ? Convert.ToInt32(debitNote.tblDebitNote.CreatedBy) : 0;
                        debitNoteVM.CreatedDate = debitNote.tblDebitNote.CreatedDate != null ? debitNote.tblDebitNote.CreatedDate.ToString() : string.Empty;
                        debitNoteVM.ModifiedBy = debitNote.tblDebitNote.ModifiedBy != null ? Convert.ToInt32(debitNote.tblDebitNote.ModifiedBy) : 0;
                        debitNoteVM.ModifiedDate = debitNote.tblDebitNote.ModifiedDate != null ? debitNote.tblDebitNote.ModifiedDate.ToString() : string.Empty;

                        List<tblPolicyDebitNote> policyInfoPaymentList = policyDebitNoteList.Where(x => x.DebitNoteID == debitNote.DebitNoteID).ToList();

                        List<PolicyInfoPaymentVM> policyInfoPaymentVMList = new List<PolicyInfoPaymentVM>();

                        foreach (var policyInfoPayment in policyInfoPaymentList)
                        {
                            PolicyInfoPaymentVM policyInfoPaymentVM = new PolicyInfoPaymentVM();
                            policyInfoPaymentVM.PolicyInfoPaymentID = policyInfoPayment.tblPolicyInfoPayment.PolicyInfoPaymentID;
                            policyInfoPaymentVM.PolicyInfoRecID = policyInfoPayment.tblPolicyInfoPayment.PolicyInfoRecID != null ? Convert.ToInt32(policyInfoPayment.tblPolicyInfoPayment.PolicyInfoRecID) : 0;

                            if (policyInfoPaymentVM.PolicyInfoRecID > 0)
                            {
                                policyInfoPaymentVM.PolicyInfoRecObj = managePolicyInfoRecording.GetPolicyInfoRecordingByID(policyInfoPaymentVM.PolicyInfoRecID);
                            }

                            policyInfoPaymentVM.NonCommissionPremium = policyInfoPayment.tblPolicyInfoPayment.NonCommissionPremium != null ? Convert.ToDecimal(policyInfoPayment.tblPolicyInfoPayment.NonCommissionPremium) : 0;
                            policyInfoPaymentVM.GrossPremium = policyInfoPayment.tblPolicyInfoPayment.GrossPremium != null ? Convert.ToDecimal(policyInfoPayment.tblPolicyInfoPayment.GrossPremium) : 0;
                            policyInfoPaymentVM.CreatedBy = policyInfoPayment.tblPolicyInfoPayment.CreatedBy != null ? Convert.ToInt32(policyInfoPayment.tblPolicyInfoPayment.CreatedBy) : 0;
                            policyInfoPaymentVM.CreatedDate = policyInfoPayment.tblPolicyInfoPayment.CreatedDate != null ? policyInfoPayment.tblPolicyInfoPayment.CreatedDate.ToString() : string.Empty;
                            policyInfoPaymentVM.ModifiedBy = policyInfoPayment.tblPolicyInfoPayment.ModifiedBy != null ? Convert.ToInt32(policyInfoPayment.tblPolicyInfoPayment.ModifiedBy) : 0;
                            policyInfoPaymentVM.ModifiedDate = policyInfoPayment.tblPolicyInfoPayment.ModifiedDate != null ? policyInfoPayment.tblPolicyInfoPayment.ModifiedDate.ToString() : string.Empty;

                            List<tblPolicyInfoCharge> policyInfoChargeList = unitOfWork.TblPolicyInfoChargeRepository.Get(x => x.PolicyInfoPaymentID == policyInfoPayment.PolicyInfoPaymentID).ToList();

                            List<PolicyInfoChargeVM> policyInfoChargeVMList = new List<PolicyInfoChargeVM>();

                            foreach (var policyInfoCharge in policyInfoChargeList)
                            {
                                PolicyInfoChargeVM policyInfoChargeVM = new PolicyInfoChargeVM();
                                policyInfoChargeVM.PolicyInfoChargeID = policyInfoPaymentVM.PolicyInfoRecID;
                                policyInfoChargeVM.ChargeTypeID = policyInfoCharge.ChargeTypeID != null ? Convert.ToInt32(policyInfoCharge.ChargeTypeID) : 0;

                                if (policyInfoChargeVM.ChargeTypeID > 0)
                                {
                                    tblChargeType chargeTypeDetails = unitOfWork.TblChargeTypeRepository.GetByID(policyInfoChargeVM.ChargeTypeID);

                                    policyInfoChargeVM.ChargeTypeName = chargeTypeDetails.ChargeType;
                                }

                                policyInfoChargeVM.Amount = policyInfoCharge.Amount != null ? Convert.ToDecimal(policyInfoCharge.Amount) : 0;
                                policyInfoChargeVM.IsCR = (bool)policyInfoCharge.IsCR;
                                policyInfoChargeVM.CreatedBy = policyInfoCharge.CreatedBy != null ? Convert.ToInt32(policyInfoCharge.CreatedBy) : 0;
                                policyInfoChargeVM.CreatedDate = policyInfoCharge.CreatedDate != null ? policyInfoCharge.CreatedDate.ToString() : string.Empty;
                                policyInfoChargeVM.ModifiedBy = policyInfoCharge.ModifiedBy != null ? Convert.ToInt32(policyInfoCharge.ModifiedBy) : 0;
                                policyInfoChargeVM.ModifiedDate = policyInfoCharge.ModifiedDate != null ? policyInfoCharge.ModifiedDate.ToString() : string.Empty;

                                policyInfoChargeVMList.Add(policyInfoChargeVM);
                            }

                            policyInfoPaymentVM.PolicyInfoChargeList = policyInfoChargeVMList;

                            policyInfoPaymentVMList.Add(policyInfoPaymentVM);
                        }

                        debitNoteVM.PolicyInfoPaymentList = policyInfoPaymentVMList;

                        debitNoteVMList.Add(debitNoteVM);
                    }

                    paymentVM.DebitNoteList = debitNoteVMList;

                    paymentVMList.Add(paymentVM);
                }

                return paymentVMList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<PaymentVM> GetAllPaymentsByBusinessUnitID(int businessUnitID)
        {
            try
            {

                

                ManagePolicyInfoRecording managePolicyInfoRecording = new ManagePolicyInfoRecording();

                var paymentData = unitOfWork.TblPaymentRepository.Get(x => x.tblClient.BUID == businessUnitID).ToList();

                List<PaymentVM> paymentVMList = new List<PaymentVM>();
                int policyId = 0;
                foreach (var payment in paymentData)
                {
                    PaymentVM paymentVM = new PaymentVM();
                    paymentVM.PaymentID = payment.PaymentID;
                    paymentVM.ClientID = payment.ClientID != null ? Convert.ToInt32(payment.ClientID) : 0;

                    if (paymentVM.ClientID > 0)
                    {
                        paymentVM.ClientName = payment.tblClient.ClientName;
                    }

                    paymentVM.PaymentAmount = payment.PaymentAmount != null ? Convert.ToDecimal(payment.PaymentAmount) : 0;
                    paymentVM.CreatedBy = payment.CreatedBy != null ? Convert.ToInt32(payment.CreatedBy) : 0;
                    paymentVM.CreatedDate = payment.CreatedDate != null ? payment.CreatedDate.ToString() : string.Empty;
                    paymentVM.ModifiedBy = payment.ModifiedBy != null ? Convert.ToInt32(payment.ModifiedBy) : 0;
                    paymentVM.ModifiedDate = payment.ModifiedDate != null ? payment.ModifiedDate.ToString() : string.Empty;

                    List<tblPolicyDebitNote> policyDebitNoteList = unitOfWork.TblPolicyDebitNoteRepository.Get(x => x.PaymentID == payment.PaymentID).ToList();
                    List<tblPolicyDebitNote> debitNoteList = policyDebitNoteList.GroupBy(x => x.DebitNoteID).Select(x => x.FirstOrDefault()).ToList();

                    List<DebitNoteVM> debitNoteVMList = new List<DebitNoteVM>();

                    foreach (var debitNote in debitNoteList)
                    {
                        DebitNoteVM debitNoteVM = new DebitNoteVM();
                        debitNoteVM.DebitNoteID = debitNote.tblDebitNote.DebitNoteID;
                        debitNoteVM.TotalNonCommissionPremium = debitNote.tblDebitNote.TotalNonCommissionPremium != null ? Convert.ToDecimal(debitNote.tblDebitNote.TotalNonCommissionPremium) : 0;
                        debitNoteVM.TotalGrossPremium = debitNote.tblDebitNote.TotalGrossPremium != null ? Convert.ToDecimal(debitNote.tblDebitNote.TotalGrossPremium) : 0;
                        debitNoteVM.CreatedBy = debitNote.tblDebitNote.CreatedBy != null ? Convert.ToInt32(debitNote.tblDebitNote.CreatedBy) : 0;
                        debitNoteVM.CreatedDate = debitNote.tblDebitNote.CreatedDate != null ? debitNote.tblDebitNote.CreatedDate.ToString() : string.Empty;
                        debitNoteVM.ModifiedBy = debitNote.tblDebitNote.ModifiedBy != null ? Convert.ToInt32(debitNote.tblDebitNote.ModifiedBy) : 0;
                        debitNoteVM.ModifiedDate = debitNote.tblDebitNote.ModifiedDate != null ? debitNote.tblDebitNote.ModifiedDate.ToString() : string.Empty;

                        List<tblPolicyDebitNote> policyInfoPaymentList = policyDebitNoteList.Where(x => x.DebitNoteID == debitNote.DebitNoteID).ToList();

                        List<PolicyInfoPaymentVM> policyInfoPaymentVMList = new List<PolicyInfoPaymentVM>();

                        foreach (var policyInfoPayment in policyInfoPaymentList)
                        {
                            PolicyInfoPaymentVM policyInfoPaymentVM = new PolicyInfoPaymentVM();
                            policyInfoPaymentVM.PolicyInfoPaymentID = policyInfoPayment.tblPolicyInfoPayment.PolicyInfoPaymentID;
                            policyInfoPaymentVM.PolicyInfoRecID = policyInfoPayment.tblPolicyInfoPayment.PolicyInfoRecID != null ? Convert.ToInt32(policyInfoPayment.tblPolicyInfoPayment.PolicyInfoRecID) : 0;

                            if (policyInfoPaymentVM.PolicyInfoRecID > 0)
                            {
                                policyInfoPaymentVM.PolicyInfoRecObj = managePolicyInfoRecording.GetPolicyInfoRecordingByID(policyInfoPaymentVM.PolicyInfoRecID);
                            }

                            policyInfoPaymentVM.NonCommissionPremium = policyInfoPayment.tblPolicyInfoPayment.NonCommissionPremium != null ? Convert.ToDecimal(policyInfoPayment.tblPolicyInfoPayment.NonCommissionPremium) : 0;
                            policyInfoPaymentVM.GrossPremium = policyInfoPayment.tblPolicyInfoPayment.GrossPremium != null ? Convert.ToDecimal(policyInfoPayment.tblPolicyInfoPayment.GrossPremium) : 0;
                            policyInfoPaymentVM.CreatedBy = policyInfoPayment.tblPolicyInfoPayment.CreatedBy != null ? Convert.ToInt32(policyInfoPayment.tblPolicyInfoPayment.CreatedBy) : 0;
                            policyInfoPaymentVM.CreatedDate = policyInfoPayment.tblPolicyInfoPayment.CreatedDate != null ? policyInfoPayment.tblPolicyInfoPayment.CreatedDate.ToString() : string.Empty;
                            policyInfoPaymentVM.ModifiedBy = policyInfoPayment.tblPolicyInfoPayment.ModifiedBy != null ? Convert.ToInt32(policyInfoPayment.tblPolicyInfoPayment.ModifiedBy) : 0;
                            policyInfoPaymentVM.ModifiedDate = policyInfoPayment.tblPolicyInfoPayment.ModifiedDate != null ? policyInfoPayment.tblPolicyInfoPayment.ModifiedDate.ToString() : string.Empty;

                            List<tblPolicyInfoCharge> policyInfoChargeList = unitOfWork.TblPolicyInfoChargeRepository.Get(x => x.PolicyInfoPaymentID == policyInfoPayment.PolicyInfoPaymentID).ToList();

                            List<PolicyInfoChargeVM> policyInfoChargeVMList = new List<PolicyInfoChargeVM>();

                            foreach (var policyInfoCharge in policyInfoChargeList)
                            {
                                PolicyInfoChargeVM policyInfoChargeVM = new PolicyInfoChargeVM();
                                policyInfoChargeVM.PolicyInfoChargeID = policyInfoPaymentVM.PolicyInfoRecID;
                                policyInfoChargeVM.ChargeTypeID = policyInfoCharge.ChargeTypeID != null ? Convert.ToInt32(policyInfoCharge.ChargeTypeID) : 0;

                                if (policyInfoChargeVM.ChargeTypeID > 0)
                                {
                                    tblChargeType chargeTypeDetails = unitOfWork.TblChargeTypeRepository.GetByID(policyInfoChargeVM.ChargeTypeID);

                                    policyInfoChargeVM.ChargeTypeName = chargeTypeDetails.ChargeType;
                                }

                                policyInfoChargeVM.Amount = policyInfoCharge.Amount != null ? Convert.ToDecimal(policyInfoCharge.Amount) : 0;
                                policyInfoChargeVM.IsCR = (bool)policyInfoCharge.IsCR;
                                policyInfoChargeVM.CreatedBy = policyInfoCharge.CreatedBy != null ? Convert.ToInt32(policyInfoCharge.CreatedBy) : 0;
                                policyInfoChargeVM.CreatedDate = policyInfoCharge.CreatedDate != null ? policyInfoCharge.CreatedDate.ToString() : string.Empty;
                                policyInfoChargeVM.ModifiedBy = policyInfoCharge.ModifiedBy != null ? Convert.ToInt32(policyInfoCharge.ModifiedBy) : 0;
                                policyInfoChargeVM.ModifiedDate = policyInfoCharge.ModifiedDate != null ? policyInfoCharge.ModifiedDate.ToString() : string.Empty;

                                policyInfoChargeVMList.Add(policyInfoChargeVM);
                                
                            }

                            policyInfoPaymentVM.PolicyInfoChargeList = policyInfoChargeVMList;

                            policyInfoPaymentVMList.Add(policyInfoPaymentVM);

                            
                        }

                        debitNoteVM.PolicyInfoPaymentList = policyInfoPaymentVMList;

                        debitNoteVMList.Add(debitNoteVM);
                    }

                   
                    paymentVM.DebitNoteList = debitNoteVMList;

                    paymentVMList.Add(paymentVM);
                }

                return paymentVMList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PaymentVM GetPaymentByID(int paymentID)
        {
            try
            {
                ManagePolicyInfoRecording managePolicyInfoRecording = new ManagePolicyInfoRecording();

                var paymentData = unitOfWork.TblPaymentRepository.GetByID(paymentID);

                PaymentVM paymentVM = new PaymentVM();
                paymentVM.PaymentID = paymentData.PaymentID;
                paymentVM.ClientID = paymentData.ClientID != null ? Convert.ToInt32(paymentData.ClientID) : 0;

                if (paymentVM.ClientID > 0)
                {
                    paymentVM.ClientName = paymentData.tblClient.ClientName;
                }

                paymentVM.PaymentAmount = paymentData.PaymentAmount != null ? Convert.ToDecimal(paymentData.PaymentAmount) : 0;
                paymentVM.CreatedBy = paymentData.CreatedBy != null ? Convert.ToInt32(paymentData.CreatedBy) : 0;
                paymentVM.CreatedDate = paymentData.CreatedDate != null ? paymentData.CreatedDate.ToString() : string.Empty;
                paymentVM.ModifiedBy = paymentData.ModifiedBy != null ? Convert.ToInt32(paymentData.ModifiedBy) : 0;
                paymentVM.ModifiedDate = paymentData.ModifiedDate != null ? paymentData.ModifiedDate.ToString() : string.Empty;

                List<tblPolicyDebitNote> policyDebitNoteList = unitOfWork.TblPolicyDebitNoteRepository.Get(x => x.PaymentID == paymentData.PaymentID).ToList();
                List<tblPolicyDebitNote> debitNoteList = policyDebitNoteList.GroupBy(x => x.DebitNoteID).Select(x => x.FirstOrDefault()).ToList();

                List<DebitNoteVM> debitNoteVMList = new List<DebitNoteVM>();

                foreach (var debitNote in debitNoteList)
                {
                    DebitNoteVM debitNoteVM = new DebitNoteVM();
                    debitNoteVM.DebitNoteID = debitNote.tblDebitNote.DebitNoteID;
                    debitNoteVM.TotalNonCommissionPremium = debitNote.tblDebitNote.TotalNonCommissionPremium != null ? Convert.ToDecimal(debitNote.tblDebitNote.TotalNonCommissionPremium) : 0;
                    debitNoteVM.TotalGrossPremium = debitNote.tblDebitNote.TotalGrossPremium != null ? Convert.ToDecimal(debitNote.tblDebitNote.TotalGrossPremium) : 0;
                    debitNoteVM.CreatedBy = debitNote.tblDebitNote.CreatedBy != null ? Convert.ToInt32(debitNote.tblDebitNote.CreatedBy) : 0;
                    debitNoteVM.CreatedDate = debitNote.tblDebitNote.CreatedDate != null ? debitNote.tblDebitNote.CreatedDate.ToString() : string.Empty;
                    debitNoteVM.ModifiedBy = debitNote.tblDebitNote.ModifiedBy != null ? Convert.ToInt32(debitNote.tblDebitNote.ModifiedBy) : 0;
                    debitNoteVM.ModifiedDate = debitNote.tblDebitNote.ModifiedDate != null ? debitNote.tblDebitNote.ModifiedDate.ToString() : string.Empty;

                    List<tblPolicyDebitNote> policyInfoPaymentList = policyDebitNoteList.Where(x => x.DebitNoteID == debitNote.DebitNoteID).ToList();

                    List<PolicyInfoPaymentVM> policyInfoPaymentVMList = new List<PolicyInfoPaymentVM>();

                    foreach (var policyInfoPayment in policyInfoPaymentList)
                    {
                        PolicyInfoPaymentVM policyInfoPaymentVM = new PolicyInfoPaymentVM();
                        policyInfoPaymentVM.PolicyInfoPaymentID = policyInfoPayment.tblPolicyInfoPayment.PolicyInfoPaymentID;
                        policyInfoPaymentVM.PolicyInfoRecID = policyInfoPayment.tblPolicyInfoPayment.PolicyInfoRecID != null ? Convert.ToInt32(policyInfoPayment.tblPolicyInfoPayment.PolicyInfoRecID) : 0;

                        if (policyInfoPaymentVM.PolicyInfoRecID > 0)
                        {
                            policyInfoPaymentVM.PolicyInfoRecObj = managePolicyInfoRecording.GetPolicyInfoRecordingByID(policyInfoPaymentVM.PolicyInfoRecID);
                            debitNoteVM.PolicyInfoPaymentLists = policyInfoPaymentVM.PolicyInfoRecObj.BankTransactionList;
                        }

                        policyInfoPaymentVM.NonCommissionPremium = policyInfoPayment.tblPolicyInfoPayment.NonCommissionPremium != null ? Convert.ToDecimal(policyInfoPayment.tblPolicyInfoPayment.NonCommissionPremium) : 0;
                        policyInfoPaymentVM.GrossPremium = policyInfoPayment.tblPolicyInfoPayment.GrossPremium != null ? Convert.ToDecimal(policyInfoPayment.tblPolicyInfoPayment.GrossPremium) : 0;
                        policyInfoPaymentVM.CreatedBy = policyInfoPayment.tblPolicyInfoPayment.CreatedBy != null ? Convert.ToInt32(policyInfoPayment.tblPolicyInfoPayment.CreatedBy) : 0;
                        policyInfoPaymentVM.CreatedDate = policyInfoPayment.tblPolicyInfoPayment.CreatedDate != null ? policyInfoPayment.tblPolicyInfoPayment.CreatedDate.ToString() : string.Empty;
                        policyInfoPaymentVM.ModifiedBy = policyInfoPayment.tblPolicyInfoPayment.ModifiedBy != null ? Convert.ToInt32(policyInfoPayment.tblPolicyInfoPayment.ModifiedBy) : 0;
                        policyInfoPaymentVM.ModifiedDate = policyInfoPayment.tblPolicyInfoPayment.ModifiedDate != null ? policyInfoPayment.tblPolicyInfoPayment.ModifiedDate.ToString() : string.Empty;

                        List<tblPolicyInfoCharge> policyInfoChargeList = unitOfWork.TblPolicyInfoChargeRepository.Get(x => x.PolicyInfoPaymentID == policyInfoPayment.PolicyInfoPaymentID).ToList();

                        List<PolicyInfoChargeVM> policyInfoChargeVMList = new List<PolicyInfoChargeVM>();

                        foreach (var policyInfoCharge in policyInfoChargeList)
                        {
                            PolicyInfoChargeVM policyInfoChargeVM = new PolicyInfoChargeVM();
                            policyInfoChargeVM.PolicyInfoChargeID = policyInfoPaymentVM.PolicyInfoRecID;
                            policyInfoChargeVM.ChargeTypeID = policyInfoCharge.ChargeTypeID != null ? Convert.ToInt32(policyInfoCharge.ChargeTypeID) : 0;

                            if (policyInfoChargeVM.ChargeTypeID > 0)
                            {
                                tblChargeType chargeTypeDetails = unitOfWork.TblChargeTypeRepository.GetByID(policyInfoChargeVM.ChargeTypeID);

                                policyInfoChargeVM.ChargeTypeName = chargeTypeDetails.ChargeType;
                            }

                            policyInfoChargeVM.Amount = policyInfoCharge.Amount != null ? Convert.ToDecimal(policyInfoCharge.Amount) : 0;
                            policyInfoChargeVM.IsCR = (bool)policyInfoCharge.IsCR;
                            policyInfoChargeVM.CreatedBy = policyInfoCharge.CreatedBy != null ? Convert.ToInt32(policyInfoCharge.CreatedBy) : 0;
                            policyInfoChargeVM.CreatedDate = policyInfoCharge.CreatedDate != null ? policyInfoCharge.CreatedDate.ToString() : string.Empty;
                            policyInfoChargeVM.ModifiedBy = policyInfoCharge.ModifiedBy != null ? Convert.ToInt32(policyInfoCharge.ModifiedBy) : 0;
                            policyInfoChargeVM.ModifiedDate = policyInfoCharge.ModifiedDate != null ? policyInfoCharge.ModifiedDate.ToString() : string.Empty;

                            policyInfoChargeVMList.Add(policyInfoChargeVM);
                        }

                        policyInfoPaymentVM.PolicyInfoChargeList = policyInfoChargeVMList;

                        policyInfoPaymentVMList.Add(policyInfoPaymentVM);
                    }

                    //  debitNoteVM.PolicyInfoPaymentList = policyInfoPaymentVMList;
                    

                    debitNoteVMList.Add(debitNoteVM);
                }

                paymentVM.DebitNoteList = debitNoteVMList;

                return paymentVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
