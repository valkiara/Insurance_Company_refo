using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBMS.Shared.ViewModel;
using IBMS.Repository;
using System.Globalization;

namespace IBMS.Service.TransactionData
{
    public class SingaporeAdmission2
    {
        #region Add Addmission
        private UnitOfWork unitOfWork;
        public SingaporeAdmission2()
        {
            unitOfWork = new UnitOfWork();
        }
        public bool SaveAdmissionRecording(AdmissionVM admissionVM)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    Admission_Singapore singaporeAdmission;
                    bool isModify = false;
                    using (var db = new PERFECTIBSEntities())
                    {
                        singaporeAdmission = db.Admission_Singapore.Where(p => p.ReferenceNo.Equals(admissionVM.ReferenceNo.Trim())).FirstOrDefault();
                        if (singaporeAdmission != null)
                            isModify = true;
                    }

                    if (singaporeAdmission == null) singaporeAdmission = new Admission_Singapore();

                    #region Assign Properties
                    singaporeAdmission.SingaporeAdmissionId = 0;
                    singaporeAdmission.ReferenceNo = admissionVM.ReferenceNo == null ? "":admissionVM.ReferenceNo.Trim();
                    singaporeAdmission.PatientName = admissionVM==null || admissionVM.DeductionID == null?"0": admissionVM.DeductionID.ToString();

                      singaporeAdmission.DateOfBirth = !string.IsNullOrEmpty(admissionVM.DateOfBirth) ? DateTime.ParseExact(admissionVM.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture) : DateTime.Parse("01/01/1900");
                   // singaporeAdmission.DateOfBirth = DateTime.Parse(admissionVM.DateOfBirth);
                    singaporeAdmission.PassportNumber = admissionVM.PassportNumber.Trim();
                    singaporeAdmission.Scheme = admissionVM==null?"": admissionVM.Scheme.Trim();


                  singaporeAdmission.InimatedDate = !string.IsNullOrEmpty(admissionVM.InimatedDate) ? DateTime.ParseExact(admissionVM.InimatedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : DateTime.Parse("01/01/1900");
                    singaporeAdmission.InceptionDate = !string.IsNullOrEmpty(admissionVM.InceptionDate) ? DateTime.ParseExact(admissionVM.InceptionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : DateTime.Parse("01/01/1900"); ;
                    singaporeAdmission.Deductible = admissionVM==null?0: admissionVM.Deductible;
                    singaporeAdmission.DeductibleForTheYear = admissionVM==null?0: admissionVM.DeductibleUsedForTheYear;
                    singaporeAdmission.Exclusions = admissionVM==null?0: admissionVM.Exclusions;
                    singaporeAdmission.Hospital = admissionVM==null || admissionVM.Hospital==null? "": admissionVM.Hospital;
                    singaporeAdmission.AdmissionDate = !string.IsNullOrEmpty(admissionVM.AdmissionDate) ? DateTime.ParseExact(admissionVM.AdmissionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : DateTime.Parse("01/01/1900");   
                 singaporeAdmission.DischargeDate = !string.IsNullOrEmpty(admissionVM.DischargedDate) ? DateTime.ParseExact(admissionVM.DischargedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : DateTime.Parse("01/01/1900");   ;
                  singaporeAdmission.CaseNumber = admissionVM == null || admissionVM.CaseNumber == null ? "" : admissionVM.CaseNumber; ;
                    singaporeAdmission.Illness = admissionVM==null || admissionVM.Illness==null? "": admissionVM.Illness.Trim();
                    singaporeAdmission.ConsultantName = admissionVM == null || admissionVM.ConsultantName==null ? "" : admissionVM.ConsultantName.Trim();
                    singaporeAdmission.InformedBy = admissionVM == null || admissionVM.InformedBy ==null? "" : admissionVM.InformedBy.Trim();
                    singaporeAdmission.GOPAmount = admissionVM == null ? 0 : admissionVM.GOPAmount;
                    singaporeAdmission.ExtendedGOP = admissionVM == null ? 0 : admissionVM.ExtendedGOP;
                    singaporeAdmission.FinalAmount = admissionVM == null ? 0 : admissionVM.FinalAmount;
                    singaporeAdmission.ConsultantFee = admissionVM == null ? 0 : admissionVM.ConsultantFee;
                   singaporeAdmission.FinalBillReceivedDate = !string.IsNullOrEmpty(admissionVM.FinalBillRecievedDate) ? DateTime.ParseExact(admissionVM.FinalBillRecievedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : DateTime.Parse("01/01/1900");
                    singaporeAdmission.PaymentGivenToAccount = admissionVM == null ? 0 : admissionVM.PaymentGivenToAccount;
                    singaporeAdmission.ReferalFee = admissionVM == null ? 0 : admissionVM.ReferalFee;
                     singaporeAdmission.FinalBill = admissionVM == null ? 0 : admissionVM.FinalBillAmount;
                     singaporeAdmission.ReferalFeeReceivedDate = !string.IsNullOrEmpty(admissionVM.ReferalFeeReceivedDate) ? DateTime.ParseExact(admissionVM.ReferalFeeReceivedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : DateTime.Parse("01/01/1900");
                    singaporeAdmission.PaymentGivenToAccount = admissionVM == null ? 0 : admissionVM.PaymentGivenToAccount;
                    singaporeAdmission.ReferalFeeReceivedTTNo = admissionVM == null || admissionVM.ReferalFeeReceivedTtTransfer ==null? "": admissionVM.ReferalFeeReceivedTtTransfer.ToString(); // WRONG DT
                    singaporeAdmission.Remark = admissionVM == null || admissionVM.Remark==null ? "" : admissionVM.Remark.Trim();
                    singaporeAdmission.ExtendedGOPDate = !string.IsNullOrEmpty(admissionVM.ExtendedGOPDate) ? DateTime.ParseExact(admissionVM.ExtendedGOPDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : DateTime.Parse("01/01/1900");
                    singaporeAdmission.CMAInvoiceNumber = admissionVM == null || admissionVM.CMAInvoiceNumber == null ? "" : admissionVM.CMAInvoiceNumber;
                    singaporeAdmission.MemberID = 0;
                    singaporeAdmission.DependantID = 0;
                    #endregion

                    if (!isModify)
                        unitOfWork.tblAdmission_Singapore.Insert(singaporeAdmission);
                    else
                        unitOfWork.tblAdmission_Singapore.Update(singaporeAdmission);

                    unitOfWork.Save();

                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }

        #endregion

        #region Load All Existing Local Admission

        public List<AdmissionViewVM> GetAllSingaporeAdmissions()
        {
            using (var db = new PERFECTIBSEntities())
            {
                var qry = (from pa in db.SingaporeAdmissions
                           select new AdmissionViewVM
                           {
                               PatientName = pa.PatientName.Trim(),
                               AdmissionDate = pa.AdmissionDate,
                               BHTNumber = pa.CaseNumber.Trim(),
                               Hospital = pa.Hospital.Trim(),
                               PassportNumber = pa.PassportNumber.Trim(),
                               ReferenceNo = pa.ReferenceNo.Trim()
                           }).ToList();
                return qry;
            }
        }

        #endregion

        #region Get Local Admission by reference no
        public AdmissionVM GetSingaporeAdmissionByReferenceNo(string refNo)
        {
            try
            {

                var singaporeAdmissions = unitOfWork.tblAdmission_Singapore.Get(x => x.ReferenceNo == refNo).ToList();
                var AdmissionVM = new AdmissionVM();
                if (singaporeAdmissions != null)
                {
                    AdmissionVM.ReferenceNo = singaporeAdmissions[0].ReferenceNo;
                    AdmissionVM.PatientName = singaporeAdmissions[0].PatientName;
                    AdmissionVM.DateOfBirth = singaporeAdmissions[0].DateOfBirth.ToString();
                    AdmissionVM.PassportNumber = singaporeAdmissions[0].PassportNumber;
                    AdmissionVM.Scheme = singaporeAdmissions[0].Scheme.Trim();
                    AdmissionVM.InimatedDate = singaporeAdmissions[0].InimatedDate.ToString();
                    AdmissionVM.InceptionDate = singaporeAdmissions[0].InceptionDate.ToString();
                    AdmissionVM.Deductible = singaporeAdmissions[0].Deductible==null?0:(decimal)singaporeAdmissions[0].Deductible;
                    AdmissionVM.DeductibleUsedForTheYear = singaporeAdmissions[0].DeductibleForTheYear == null ? 0 : singaporeAdmissions[0].DeductibleForTheYear == null ? 0 : (decimal)singaporeAdmissions[0].DeductibleForTheYear;
                                AdmissionVM.Exclusions = singaporeAdmissions[0].Exclusions == null ? 0 : (decimal)singaporeAdmissions[0].Exclusions;
                    AdmissionVM.Hospital = singaporeAdmissions[0].Hospital == null ? "" : singaporeAdmissions[0].Hospital.Trim();
                    AdmissionVM.AdmissionDate = singaporeAdmissions[0].AdmissionDate == null ? "" : singaporeAdmissions[0].AdmissionDate.ToString();
                    AdmissionVM.DischargedDate = singaporeAdmissions[0].DischargeDate == null ? "" : singaporeAdmissions[0].DischargeDate.ToString();
                    AdmissionVM.CaseNumber = singaporeAdmissions[0].CaseNumber == null ? "" : singaporeAdmissions[0].CaseNumber;
                    AdmissionVM.Illness = singaporeAdmissions[0].Illness == null ? "" : singaporeAdmissions[0].Illness.Trim();
                    AdmissionVM.ConsultantName = singaporeAdmissions[0].ConsultantName == null ? "" : singaporeAdmissions[0].ConsultantName.Trim();
                    AdmissionVM.InformedBy = singaporeAdmissions[0].InformedBy == null ? "" : singaporeAdmissions[0].InformedBy.Trim();
                    AdmissionVM.GOPAmount = singaporeAdmissions[0].GOPAmount == null ? 0 : (decimal)singaporeAdmissions[0].GOPAmount;
                    AdmissionVM.ExtendedGOP = singaporeAdmissions[0].ExtendedGOP == null ? 0 : (decimal)singaporeAdmissions[0].ExtendedGOP;
                    AdmissionVM.FinalAmount = singaporeAdmissions[0].FinalAmount == null ? 0 : (decimal)singaporeAdmissions[0].FinalAmount;
                    AdmissionVM.ConsultantFee = singaporeAdmissions[0].ConsultantFee == null ? 0 : (decimal)singaporeAdmissions[0].ConsultantFee;
                    AdmissionVM.FinalAmount = singaporeAdmissions[0].FinalAmount == null ? 0 : (decimal)singaporeAdmissions[0].FinalAmount;
                    AdmissionVM.FinalBillAmount = singaporeAdmissions[0].FinalAmount == null ? 0 : (decimal)singaporeAdmissions[0].FinalAmount;
                    AdmissionVM.ExtendedGOPDate = singaporeAdmissions[0].AdmissionDate == null ? "" : singaporeAdmissions[0].ExtendedGOPDate.ToString();
                    AdmissionVM.FinalBillRecievedDate = singaporeAdmissions[0].FinalBillReceivedDate == null ? "" : singaporeAdmissions[0].FinalBillReceivedDate.ToString();
                    AdmissionVM.PaymentGivenToAccount = singaporeAdmissions[0].PaymentGivenToAccount == null ? 0 : (decimal)singaporeAdmissions[0].PaymentGivenToAccount;
                    AdmissionVM.ReferalFee = singaporeAdmissions[0].ReferalFee == null ? 0 : (decimal)singaporeAdmissions[0].ReferalFee;
                    AdmissionVM.CMAInvoiceNumber = singaporeAdmissions[0].CMAInvoiceNumber.Trim();
                    AdmissionVM.ReferalFeeReceivedDate = singaporeAdmissions[0].ReferalFeeReceivedDate == null ? "" : singaporeAdmissions[0].ReferalFeeReceivedDate.ToString();
                    AdmissionVM.ReferalFeeReceivedTtTransfer = singaporeAdmissions[0].ReferalFeeReceivedTTNo == null ? "" : singaporeAdmissions[0].ReferalFeeReceivedTTNo; // Doubt
                    AdmissionVM.Remark = singaporeAdmissions[0].Remark == null ? "" : singaporeAdmissions[0].Remark.Trim();
                    AdmissionVM.DeductionID = singaporeAdmissions[0].PatientName == null ? 0: int.Parse(singaporeAdmissions[0].PatientName);




                }


                return AdmissionVM;




            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Get Singapore Admission Invoice

        public object GetSingaporeAdmissionInvoice(string referenceNo)
        {
            try
            {
                using (var db = new PERFECTIBSEntities())
                {
                    var qry = (from q in db.SingaporeAdmissions
                              where q.ReferenceNo == referenceNo
                              select new
                              {
                                  CaseNo = q.CaseNumber.Trim(),
                                  NameOnInsurance = q.PatientName.Trim(),
                                  ReferenceNo = q.ReferenceNo.Trim(),
                                  LocationOfIncident = q.Hospital.Trim(),
                                  DateOfAdmission = q.AdmissionDate,
                                  DateOfDischarge = q.DischargeDate,
                                  CMAInvoiceNumber = q.CMAInvoiceNumber.Trim(),
                                  MedicalExpenses = q.FinalAmount,
                                  CaseFee = 0,




                              }).FirstOrDefault();
                    return qry;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Save Invoice Service 

        public bool SaveInvoice(SingaporeAdmissionInvoiceVM admissionVM)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    SingaporeInvoice singaporeAdmission;
                    bool isModify = false;
                    using (var db = new PERFECTIBSEntities())
                    {
                        singaporeAdmission = db.SingaporeInvoices.Where(p => p.ReferenceNo.Equals(admissionVM.ReferenceNo.Trim())).FirstOrDefault();
                        if (singaporeAdmission != null)
                            isModify = true;
                    }

                    if (singaporeAdmission == null) singaporeAdmission = new SingaporeInvoice();

                    #region Assign Properties

                    singaporeAdmission.ReferenceNo = admissionVM.ReferenceNo.Trim();
                    singaporeAdmission.AccountNumber = admissionVM.AccountNo.Trim();
                    singaporeAdmission.BankAddress = admissionVM.BankAddress.Trim();
                    singaporeAdmission.BankBrach = admissionVM.BankBranch.Trim();
                    singaporeAdmission.BankCode = admissionVM.BankCode.Trim();
                    singaporeAdmission.BankName = admissionVM.BankName.Trim();
                    singaporeAdmission.BeneficiaryName = admissionVM.ClaimAssessor.Trim();
                    singaporeAdmission.ClaimsAssessor = admissionVM.ClaimAssessor.Trim();
                    singaporeAdmission.Customerid = admissionVM.CustomerId.Trim();
                    singaporeAdmission.MembershipNo = admissionVM.MembershipNo.Trim();
                    singaporeAdmission.SwiftCode = admissionVM.SwiftCode.Trim();
                    singaporeAdmission.Terms = admissionVM.Terms.Trim();

                    #endregion

                    if (!isModify)
                        unitOfWork.TblSingaporeInvoice.Insert(singaporeAdmission);
                    else
                        unitOfWork.TblSingaporeInvoice.Update(singaporeAdmission);

                    unitOfWork.Save();

                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }

        #endregion

    }
}
