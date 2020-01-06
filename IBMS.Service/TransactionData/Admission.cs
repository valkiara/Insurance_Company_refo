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
    public class Admission
    {

        private UnitOfWork unitOfWork;
        public Admission()
        {
            unitOfWork = new UnitOfWork();
        }


        #region save/edit Admmission for Aviva, BUPA, Pilot,Nestle, CMA Local
        public bool SaveAdmissionRecording(AdmissionVM admissionVM)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    PatientAdmission patientAdmission;
                    bool isModify = false;
                    using (var db = new PERFECTIBSEntities())
                    {
                        patientAdmission = db.PatientAdmissions.Where(p => p.ReferenceNo.Equals(admissionVM.ReferenceNo.Trim())).FirstOrDefault();

                    }

                    if (patientAdmission == null) patientAdmission = new PatientAdmission();
                    else isModify = true;
                    

                    patientAdmission.AdmissionDate = string.IsNullOrEmpty(admissionVM.AdmissionDate) ? (DateTime ?)null : DateTime.ParseExact(admissionVM.AdmissionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //patientAdmission.AdmissionDate = string.IsNullOrEmpty(admissionVM.AdmissionDate) ? (DateTime)null : DateTime.ParseExact(admissionVM.AdmissionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.AVIVADCSClaims = admissionVM.ClaimsDcsSentToAviva < 0?0: admissionVM.ClaimsDcsSentToAviva;
                    patientAdmission.BHTNumber = string.IsNullOrEmpty(admissionVM.BHTNumber) ? "":admissionVM.BHTNumber.Trim();
                    patientAdmission.ClaimSettledDate = string.IsNullOrEmpty(admissionVM.ClaimSettledDate)? (DateTime?) null : DateTime.ParseExact(admissionVM.ClaimSettledDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.ConsultantFee = admissionVM.ConsultantFee <0 ?0: admissionVM.ConsultantFee;
                    patientAdmission.ConsultantName = string.IsNullOrEmpty(admissionVM.ConsultantName) ?"":admissionVM.ConsultantName.Trim();
                    patientAdmission.DateOfBirth = string.IsNullOrEmpty(admissionVM.DateOfBirth) ? (DateTime?)null : DateTime.ParseExact(admissionVM.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.Deductible = admissionVM.Deductible < 0?0: admissionVM.Deductible;
                    patientAdmission.DeductibleForTheYear = admissionVM.DeductibleUsedForTheYear <0 ?0: admissionVM.DeductibleUsedForTheYear;
                    patientAdmission.DischargeDate =string.IsNullOrEmpty(admissionVM.DischargedDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.DischargedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.Exclusions = admissionVM.Exclusions < 0 ? 0 : admissionVM.Exclusions;
                    patientAdmission.ExtendedGOP = admissionVM.ExtendedGOP < 0 ? 0 : admissionVM.ExtendedGOP;
                    patientAdmission.FinalAmount = admissionVM.FinalAmount <0 ? 0:admissionVM.FinalAmount;
                    patientAdmission.FinalBillGivenToSGS = admissionVM.FinalBillGivenToSgs < 0? 0: admissionVM.FinalBillGivenToSgs;
                    patientAdmission.FinalBillReceivedDate = string.IsNullOrEmpty(admissionVM.FinalBillRecievedDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.FinalBillRecievedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.GOPAmount = admissionVM.GOPAmount <0 ?0: admissionVM.GOPAmount;
                    patientAdmission.GOPConfirmedBy = string.IsNullOrEmpty(admissionVM.GOPConfirmBy) ? "": admissionVM.GOPConfirmBy;
                    patientAdmission.GOPIssuedDate =  string.IsNullOrEmpty(admissionVM.GOPIssueDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.GOPIssueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                  //  patientAdmission.DischargeDate = string.IsNullOrEmpty(admissionVM.DischargedDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.DischargedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.HandledBy = string.IsNullOrEmpty(admissionVM.HandledBy) ? "" : admissionVM.HandledBy;
                    patientAdmission.Hospital = string.IsNullOrEmpty(admissionVM.Hospital)? "": admissionVM.Hospital;
                    patientAdmission.Illness = string.IsNullOrEmpty(admissionVM.Illness)?"": admissionVM.Illness;
                    patientAdmission.InceptionDate =  string.IsNullOrEmpty(admissionVM.InceptionDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.InceptionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.InformedBy = string.IsNullOrEmpty(admissionVM.InformedBy)?"": admissionVM.InformedBy;
                    patientAdmission.PassportNumber = string.IsNullOrEmpty(admissionVM.PassportNumber)?"": admissionVM.PassportNumber;
                    patientAdmission.PatientName = string.IsNullOrEmpty(admissionVM.PatientName)?"": admissionVM.PatientName;
                    patientAdmission.PaymentGivenToAccount = admissionVM.PaymentGivenToAccount<0 ?0: admissionVM.PaymentGivenToAccount;
                    patientAdmission.ReferalFee = admissionVM.ReferalFee <0? 0: admissionVM.ReferalFee;
                    patientAdmission.ReferalFeeReceivedBank = string.IsNullOrEmpty(admissionVM.ReferalFeeReceivedBank) ? "": admissionVM.ReferalFeeReceivedBank; // WRONG DT
                    patientAdmission.ReferalFeeReceivedChequeNo = string.IsNullOrEmpty(admissionVM.ReferalFeeReceivedChequeNumber)? "" : admissionVM.ReferalFeeReceivedChequeNumber;
                    patientAdmission.ReferalFeeReceivedDate = string.IsNullOrEmpty(admissionVM.ReferalFeeReceivedDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.ReferalFeeReceivedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.ReferalFeeReceivedTTNo = string.IsNullOrEmpty(admissionVM.ReferalFeeReceivedTtTransfer) ?"": admissionVM.ReferalFeeReceivedTtTransfer; // WRONG DT
                    patientAdmission.ReferenceNo = string.IsNullOrEmpty(admissionVM.ReferenceNo)?"": admissionVM.ReferenceNo;
                    patientAdmission.Remark = string.IsNullOrEmpty(admissionVM.Remark)?"" : admissionVM.Remark;
                    patientAdmission.Scheme = string.IsNullOrEmpty(admissionVM.Scheme) ? "": admissionVM.Scheme;
                    patientAdmission.PatientID = admissionVM.PatientID < 0 ? 0 : admissionVM.PatientID;
                    patientAdmission.PremiumHolderType = admissionVM.PremiumHolderType < 0 ? 0 : patientAdmission.PremiumHolderType;
                    patientAdmission.IncurredAmount = admissionVM.IncurredAmount < 0 ? 0 : admissionVM.IncurredAmount;
                    patientAdmission.ClaimDocumentReceivedDate = string.IsNullOrEmpty(admissionVM.ClaimDocumentReceivedDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.ClaimDocumentReceivedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.ClaimDocumentsEmailedDate = string.IsNullOrEmpty(admissionVM.ClaimDocumentsEmailedDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.ClaimDocumentsEmailedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.PaymentAdviceReceviedDate = string.IsNullOrEmpty(admissionVM.PaymentAdviceReceviedDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.PaymentAdviceReceviedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.PaymentAdviceEmailedDate = string.IsNullOrEmpty(admissionVM.PaymentAdviceEmailedDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.PaymentAdviceEmailedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //patientAdmission.ClaimDocumentReceivedDate = string.IsNullOrEmpty(admissionVM.ClaimDocumentReceivedDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.ClaimDocumentReceivedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    patientAdmission.OriginalDocumentscourieredDate= string.IsNullOrEmpty(admissionVM.OriginalDocumentscourieredDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.OriginalDocumentscourieredDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.TotalAmountPaid = admissionVM.TotalAmountPaid < 0 ? 0 : admissionVM.TotalAmountPaid;
                    patientAdmission.CurrencyType = admissionVM.CurrencyType < 0 ? 0 : admissionVM.CurrencyType;
                    patientAdmission.AirwayBillNo = string.IsNullOrEmpty(admissionVM.AirwayBillNo)? "" : admissionVM.AirwayBillNo;
                    patientAdmission.BUID = admissionVM.BUID;
                    patientAdmission.CurrancyID = admissionVM.CurrancyID;
                    patientAdmission.CurrancyCode = admissionVM.CurrancyCode;
                    patientAdmission.CreateBy = admissionVM.CreateBy;
                    patientAdmission.CreatedDate = DateTime.Now;
                patientAdmission.MembershipID= string.IsNullOrEmpty(admissionVM.MembershipID) ? "" : admissionVM.MembershipID;



                    if (!isModify)
                    {
                        unitOfWork.TblPatientAdmission.Insert(patientAdmission);
                    }
                    else
                    {
                        patientAdmission.ModifiedBy = admissionVM.CreateBy;
                        patientAdmission.ModifiedDate = DateTime.Now;

                        unitOfWork.TblPatientAdmission.Update(patientAdmission);
                    }
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

        public bool SavePilotClaimRecording(AdmissionVM admissionVM)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    PatientAdmission patientAdmission;
                    bool isModify = false;
                    using (var db = new PERFECTIBSEntities())
                    {
                        patientAdmission = db.PatientAdmissions.Where(p => p.PatientAdmissionId.Equals(admissionVM.PatientAdmissionId)).FirstOrDefault();

                    }

                    if (patientAdmission == null) patientAdmission = new PatientAdmission();
                    else isModify = true;


                    patientAdmission.AdmissionDate = string.IsNullOrEmpty(admissionVM.AdmissionDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.AdmissionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //patientAdmission.AdmissionDate = string.IsNullOrEmpty(admissionVM.AdmissionDate) ? (DateTime)null : DateTime.ParseExact(admissionVM.AdmissionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.AVIVADCSClaims = admissionVM.ClaimsDcsSentToAviva < 0 ? 0 : admissionVM.ClaimsDcsSentToAviva;
                    patientAdmission.BHTNumber = string.IsNullOrEmpty(admissionVM.BHTNumber) ? "" : admissionVM.BHTNumber.Trim();
                    patientAdmission.ClaimSettledDate = string.IsNullOrEmpty(admissionVM.ClaimSettledDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.ClaimSettledDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.ConsultantFee = admissionVM.ConsultantFee < 0 ? 0 : admissionVM.ConsultantFee;
                    patientAdmission.ConsultantName = string.IsNullOrEmpty(admissionVM.ConsultantName) ? "" : admissionVM.ConsultantName.Trim();
                    patientAdmission.DateOfBirth = string.IsNullOrEmpty(admissionVM.DateOfBirth) ? (DateTime?)null : DateTime.ParseExact(admissionVM.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.Deductible = admissionVM.Deductible < 0 ? 0 : admissionVM.Deductible;
                    patientAdmission.DeductibleForTheYear = admissionVM.DeductibleUsedForTheYear < 0 ? 0 : admissionVM.DeductibleUsedForTheYear;
                    patientAdmission.DischargeDate = string.IsNullOrEmpty(admissionVM.DischargedDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.DischargedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.Exclusions = admissionVM.Exclusions < 0 ? 0 : admissionVM.Exclusions;
                    patientAdmission.ExtendedGOP = admissionVM.ExtendedGOP < 0 ? 0 : admissionVM.ExtendedGOP;
                    patientAdmission.FinalAmount = admissionVM.FinalAmount < 0 ? 0 : admissionVM.FinalAmount;
                    patientAdmission.FinalBillGivenToSGS = admissionVM.FinalBillGivenToSgs < 0 ? 0 : admissionVM.FinalBillGivenToSgs;
                    patientAdmission.FinalBillReceivedDate = string.IsNullOrEmpty(admissionVM.FinalBillRecievedDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.FinalBillRecievedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.GOPAmount = admissionVM.GOPAmount < 0 ? 0 : admissionVM.GOPAmount;
                    patientAdmission.GOPConfirmedBy = string.IsNullOrEmpty(admissionVM.GOPConfirmBy) ? "" : admissionVM.GOPConfirmBy;
                    patientAdmission.GOPIssuedDate = string.IsNullOrEmpty(admissionVM.GOPIssueDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.GOPIssueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //  patientAdmission.DischargeDate = string.IsNullOrEmpty(admissionVM.DischargedDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.DischargedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.HandledBy = string.IsNullOrEmpty(admissionVM.HandledBy) ? "" : admissionVM.HandledBy;
                    patientAdmission.Hospital = string.IsNullOrEmpty(admissionVM.Hospital) ? "" : admissionVM.Hospital;
                    patientAdmission.Illness = string.IsNullOrEmpty(admissionVM.Illness) ? "" : admissionVM.Illness;
                    patientAdmission.InceptionDate = string.IsNullOrEmpty(admissionVM.InceptionDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.InceptionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.InformedBy = string.IsNullOrEmpty(admissionVM.InformedBy) ? "" : admissionVM.InformedBy;
                    patientAdmission.PassportNumber = string.IsNullOrEmpty(admissionVM.PassportNumber) ? "" : admissionVM.PassportNumber;
                    patientAdmission.PatientName = string.IsNullOrEmpty(admissionVM.PatientName) ? "" : admissionVM.PatientName;
                    patientAdmission.PaymentGivenToAccount = admissionVM.PaymentGivenToAccount < 0 ? 0 : admissionVM.PaymentGivenToAccount;
                    patientAdmission.ReferalFee = admissionVM.ReferalFee < 0 ? 0 : admissionVM.ReferalFee;
                    patientAdmission.ReferalFeeReceivedBank = string.IsNullOrEmpty(admissionVM.ReferalFeeReceivedBank) ? "" : admissionVM.ReferalFeeReceivedBank; // WRONG DT
                    patientAdmission.ReferalFeeReceivedChequeNo = string.IsNullOrEmpty(admissionVM.ReferalFeeReceivedChequeNumber) ? "" : admissionVM.ReferalFeeReceivedChequeNumber;
                    patientAdmission.ReferalFeeReceivedDate = string.IsNullOrEmpty(admissionVM.ReferalFeeReceivedDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.ReferalFeeReceivedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.ReferalFeeReceivedTTNo = string.IsNullOrEmpty(admissionVM.ReferalFeeReceivedTtTransfer) ? "" : admissionVM.ReferalFeeReceivedTtTransfer; // WRONG DT
                    patientAdmission.ReferenceNo = string.IsNullOrEmpty(admissionVM.ReferenceNo) ? "" : admissionVM.ReferenceNo;
                    patientAdmission.Remark = string.IsNullOrEmpty(admissionVM.Remark) ? "" : admissionVM.Remark;
                    patientAdmission.Scheme = string.IsNullOrEmpty(admissionVM.Scheme) ? "" : admissionVM.Scheme;
                    patientAdmission.PatientID = admissionVM.PatientID < 0 ? 0 : admissionVM.PatientID;
                    patientAdmission.PremiumHolderType = admissionVM.PremiumHolderType < 0 ? 0 : patientAdmission.PremiumHolderType;
                    patientAdmission.IncurredAmount = admissionVM.IncurredAmount < 0 ? 0 : admissionVM.IncurredAmount;
                    patientAdmission.ClaimDocumentReceivedDate = string.IsNullOrEmpty(admissionVM.ClaimDocumentReceivedDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.ClaimDocumentReceivedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.ClaimDocumentsEmailedDate = string.IsNullOrEmpty(admissionVM.ClaimDocumentsEmailedDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.ClaimDocumentsEmailedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.PaymentAdviceReceviedDate = string.IsNullOrEmpty(admissionVM.PaymentAdviceReceviedDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.PaymentAdviceReceviedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.PaymentAdviceEmailedDate = string.IsNullOrEmpty(admissionVM.PaymentAdviceEmailedDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.PaymentAdviceEmailedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //patientAdmission.ClaimDocumentReceivedDate = string.IsNullOrEmpty(admissionVM.ClaimDocumentReceivedDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.ClaimDocumentReceivedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    patientAdmission.OriginalDocumentscourieredDate = string.IsNullOrEmpty(admissionVM.OriginalDocumentscourieredDate) ? (DateTime?)null : DateTime.ParseExact(admissionVM.OriginalDocumentscourieredDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    patientAdmission.TotalAmountPaid = admissionVM.TotalAmountPaid < 0 ? 0 : admissionVM.TotalAmountPaid;
                    patientAdmission.CurrencyType = admissionVM.CurrencyType < 0 ? 0 : admissionVM.CurrencyType;
                    patientAdmission.AirwayBillNo = string.IsNullOrEmpty(admissionVM.AirwayBillNo) ? "" : admissionVM.AirwayBillNo;
                    patientAdmission.BUID = admissionVM.BUID;
                    patientAdmission.CurrancyID = admissionVM.CurrancyID;
                    patientAdmission.CurrancyCode = admissionVM.CurrancyCode;
                    patientAdmission.CreateBy = admissionVM.CreateBy;
                    patientAdmission.CreatedDate = DateTime.Now;
                    patientAdmission.MembershipID = string.IsNullOrEmpty(admissionVM.MembershipID) ? "" : admissionVM.MembershipID;



                    if (!isModify)
                    {
                        unitOfWork.TblPatientAdmission.Insert(patientAdmission);
                    }
                    else
                    {
                        patientAdmission.ModifiedBy = admissionVM.CreateBy;
                        patientAdmission.ModifiedDate = DateTime.Now;

                        unitOfWork.TblPatientAdmission.Update(patientAdmission);
                    }
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

        #region Load All Existing Local Admission

        public List<AdmissionViewVM> GetAllLocalAdmissions()
        {
            using (var db = new PERFECTIBSEntities())
            {
                var qry = (from pa in db.PatientAdmissions
                           select new AdmissionViewVM
                           {

                              
                               BHTNumber = pa.BHTNumber,
                               Hospital = pa.Hospital,
                               PassportNumber = pa.PassportNumber,
                              
                            ReferenceNo = string.IsNullOrEmpty(pa.ReferenceNo) ? "" : pa.ReferenceNo.Trim(),
                            PatientName= string.IsNullOrEmpty(pa.PatientName) ? "" : pa.PatientName.Trim(),
                            

                               //                       PatientName = string.IsNullOrEmpty(pa.ReferenceNo) ? "" : pa.ReferenceNo.Trim(),
                               //                       DateOfBirth= !string.IsNullOrEmpty(clientRequestVM.ClientDetails.DOB) ? DateTime.ParseExact(clientRequestVM.ClientDetails.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null
                               // PassportNumber
                               // Scheme
                               // InceptionDate
                               //Deductible
                               //DeductibleUsedForTheYear
                               //Exclusions
                               // Hospital
                               // AdmissionDate
                               // DischargedDate
                               // BHTNumber
                               // Illness
                               // ConsultantName
                               // InformedBy
                               //GOPAmount
                               // GOPConfirmBy
                               // GOPIssueDate
                               //ExtendedGOP
                               // HandledBy
                               //FinalAmount
                               //ConsultantFee
                               // FinalBillRecievedDate
                               //FinalBillGivenToSgs
                               //ClaimsDcsSentToAviva
                               // ClaimSettledDate
                               //ReferalFee
                               // ReferalFeeReceivedDate
                               // ReferalFeeReceivedBank
                               // ReferalFeeReceivedChequeNumber
                               // ReferalFeeReceivedTtTransfer
                               //PaymentGivenToAccount
                               // Remark

                               //PatientID

                               //IncurredAmount

                               // ClaimDocumentReceivedDate

                               // ClaimDocumentsEmailedDate

                               // PaymentAdviceReceviedDate

                               // PaymentAdviceEmailedDate

                               //TotalAmountPaid

                               //CurrencyType

                               // OriginalDocumentscourieredDate

                               // AirwayBillNo

                               //BUID

                               //CreateBy

                               // CreatedDate

                               //ModifiedBy

                               // ModifiedDate







                           }).ToList();
                return qry;
            }
        }

        public List<AdmissionVM> GetAllAdmissions(int businessUnitID)
        {
            var clientData = unitOfWork.TblPatientAdmission.Get(x => x.BUID == businessUnitID).ToList();
            List<AdmissionVM> clientList = new List<AdmissionVM>();
            //clientList = new List<AdmissionVM>();

            //clientList = new List<AdmissionVM>();
            foreach (var pa in clientData)

            {
                AdmissionVM admissionVM = new AdmissionVM();
                admissionVM.PatientAdmissionId = pa.PatientAdmissionId < 0 ? 0 : pa.PatientAdmissionId;
                admissionVM.ReferenceNo = string.IsNullOrEmpty(pa.ReferenceNo) ? "" : pa.ReferenceNo.Trim();
                admissionVM.PatientName = string.IsNullOrEmpty(pa.PatientName) ? "" : pa.PatientName.Trim();
                admissionVM.DateOfBirth = pa.DateOfBirth != null ? Convert.ToDateTime(pa.DateOfBirth).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.PassportNumber = string.IsNullOrEmpty(pa.PassportNumber) ? "" : pa.PassportNumber;
                admissionVM.Scheme = string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme;
                admissionVM.InceptionDate = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.Deductible = pa.Deductible < 0 ? 0 : (decimal)pa.Deductible;
                //DeductibleUsedForTheYear= pa.DeductibleUsedForTheYear < 0 ? 0 : (decimal)pa.DeductibleUsedForTheYear;
                admissionVM.Exclusions = pa.Exclusions < 0 ? 0 : (decimal)pa.Exclusions;
                admissionVM.Hospital = string.IsNullOrEmpty(pa.Hospital) ? "" : pa.Hospital;
                admissionVM.AdmissionDate = pa.AdmissionDate != null ? Convert.ToDateTime(pa.AdmissionDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.DischargedDate = pa.DischargeDate != null ? Convert.ToDateTime(pa.DischargeDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.BHTNumber = string.IsNullOrEmpty(pa.BHTNumber) ? "" : pa.BHTNumber;
                admissionVM.Illness = string.IsNullOrEmpty(pa.Illness) ? "" : pa.Illness;
                admissionVM.ConsultantName = string.IsNullOrEmpty(pa.ConsultantName) ? "" : pa.ConsultantName;
                admissionVM.InformedBy = string.IsNullOrEmpty(pa.InformedBy) ? "" : pa.InformedBy;
                admissionVM.GOPAmount = pa.GOPAmount < 0 ? 0 : (decimal)pa.GOPAmount;
                //GOPConfirmBy = string.IsNullOrEmpty(pa.GOPConfirmBy) ? "" : pa.GOPConfirmBy;
                //GOPIssueDate = pa.GOPIssueDate != null ? Convert.ToDateTime(pa.GOPIssueDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.ExtendedGOP = pa.ExtendedGOP < 0 ? 0 : (decimal)pa.ExtendedGOP;
                admissionVM.HandledBy = string.IsNullOrEmpty(pa.HandledBy) ? "" : pa.HandledBy;
                admissionVM.FinalAmount = pa.FinalAmount < 0 ? 0 : (decimal)pa.FinalAmount;
                admissionVM.ConsultantFee = pa.ConsultantFee < 0 ? 0 : (decimal)pa.ConsultantFee;
                admissionVM.FinalBillRecievedDate = pa.FinalBillReceivedDate != null ? Convert.ToDateTime(pa.FinalBillReceivedDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.FinalBillGivenToSgs = pa.Deductible < 0 ? 0 : (decimal)pa.Deductible;
                admissionVM.ClaimsDcsSentToAviva = pa.AVIVADCSClaims < 0 ? 0 : (decimal)pa.AVIVADCSClaims;
                admissionVM.ClaimSettledDate = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.ReferalFee = pa.Deductible < 0 ? 0 : (decimal)pa.Deductible;
                admissionVM.ReferalFeeReceivedDate = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.ReferalFeeReceivedBank = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.ReferalFeeReceivedChequeNumber = string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme;
                admissionVM.ReferalFeeReceivedTtTransfer = string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme;
                admissionVM.PaymentGivenToAccount = pa.PaymentGivenToAccount < 0 ? 0 : (decimal)pa.PaymentGivenToAccount;
                admissionVM.Remark = string.IsNullOrEmpty(pa.Remark) ? "" : pa.Remark;
                admissionVM.PatientID = pa.PatientID < 0 ? 0 : (int)pa.PatientID;
                admissionVM.IncurredAmount = pa.IncurredAmount < 0 ? 0 : (decimal)pa.IncurredAmount;
                admissionVM.ClaimDocumentReceivedDate = pa.ClaimDocumentReceivedDate != null ? Convert.ToDateTime(pa.ClaimDocumentReceivedDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.ClaimDocumentsEmailedDate = pa.ClaimDocumentsEmailedDate != null ? Convert.ToDateTime(pa.ClaimDocumentsEmailedDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.PaymentAdviceReceviedDate = pa.PaymentAdviceReceviedDate != null ? Convert.ToDateTime(pa.PaymentAdviceReceviedDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.PaymentAdviceEmailedDate = pa.PaymentAdviceEmailedDate != null ? Convert.ToDateTime(pa.PaymentAdviceEmailedDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.TotalAmountPaid = pa.TotalAmountPaid < 0 ? 0 : (decimal)pa.TotalAmountPaid;
                admissionVM.CurrencyType = pa.CurrencyType < 0 ? 0 : (int)pa.CurrencyType;
                admissionVM.OriginalDocumentscourieredDate = pa.OriginalDocumentscourieredDate != null ? Convert.ToDateTime(pa.OriginalDocumentscourieredDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.AirwayBillNo = string.IsNullOrEmpty(pa.AirwayBillNo) ? "" : pa.AirwayBillNo;
                admissionVM.BUID = pa.BUID < 0 ? 0 : (int)pa.BUID;
                admissionVM.CreateBy = pa.CreateBy < 0 ? 0 : (int)pa.CreateBy;
                admissionVM.CreatedDate = pa.CreatedDate != null ? Convert.ToDateTime(pa.CreatedDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.ModifiedBy = 0;
                admissionVM.ModifiedDate = pa.ModifiedDate != null ? Convert.ToDateTime(pa.ModifiedDate).ToString("dd/MM/yyyy") : string.Empty;
                clientList.Add(admissionVM);

            }
            return clientList;

            //using (var db = new PERFECTIBSEntities())
            //{
            //    var qry = (from pa in db.PatientAdmissions
            //               where pa.BUID == businessUnitID
            //               select new AdmissionVM
            //               {

            //                   ReferenceNo = string.IsNullOrEmpty(pa.ReferenceNo) ? "" : pa.ReferenceNo.Trim(),
            //                   PatientName = string.IsNullOrEmpty(pa.PatientName) ? "" : pa.PatientName.Trim(),
            //                   DateOfBirth = pa.DateOfBirth != null ? Convert.ToDateTime(pa.DateOfBirth).ToString("dd/MM/yyyy") : string.Empty,
            //                   PassportNumber = string.IsNullOrEmpty(pa.PassportNumber) ? "" : pa.PassportNumber,
            //                   Scheme= string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme,
            //                   InceptionDate= pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   Deductible= pa.Deductible <0 ? 0 : (decimal)pa.Deductible,
            //                   //DeductibleUsedForTheYear= pa.DeductibleUsedForTheYear < 0 ? 0 : (decimal)pa.DeductibleUsedForTheYear,
            //                   Exclusions= pa.Exclusions < 0 ? 0 : (decimal)pa.Exclusions,
            //                   Hospital = string.IsNullOrEmpty(pa.Hospital) ? "" : pa.Hospital,
            //                   AdmissionDate = pa.AdmissionDate != null ? Convert.ToDateTime(pa.AdmissionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   DischargedDate= pa.DischargeDate != null ? Convert.ToDateTime(pa.DischargeDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   BHTNumber = string.IsNullOrEmpty(pa.BHTNumber) ? "" : pa.BHTNumber,
            //                   Illness = string.IsNullOrEmpty(pa.Illness) ? "" : pa.Illness,
            //                   ConsultantName = string.IsNullOrEmpty(pa.ConsultantName) ? "" : pa.ConsultantName,
            //                   InformedBy = string.IsNullOrEmpty(pa.InformedBy) ? "" : pa.InformedBy,
            //                   GOPAmount = pa.GOPAmount < 0 ? 0 : (decimal)pa.GOPAmount,
            //                   //GOPConfirmBy = string.IsNullOrEmpty(pa.GOPConfirmBy) ? "" : pa.GOPConfirmBy,
            //                   //GOPIssueDate = pa.GOPIssueDate != null ? Convert.ToDateTime(pa.GOPIssueDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   ExtendedGOP = pa.ExtendedGOP < 0 ? 0 : (decimal)pa.ExtendedGOP,
            //                   HandledBy = string.IsNullOrEmpty(pa.HandledBy) ? "" : pa.HandledBy,
            //                   FinalAmount = pa.FinalAmount < 0 ? 0 : (decimal)pa.FinalAmount,
            //                   ConsultantFee = pa.ConsultantFee < 0 ? 0 : (decimal)pa.ConsultantFee,
            //                   //FinalBillRecievedDate = pa.FinalBillRecievedDate != null ? Convert.ToDateTime(pa.FinalBillRecievedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   //FinalBillGivenToSgs = pa.Deductible < 0 ? 0 : (decimal)pa.Deductible,
            //                   //ClaimsDcsSentToAviva = pa.ClaimsDcsSentToAviva < 0 ? 0 : (decimal)pa.ClaimsDcsSentToAviva,
            //                   //ClaimSettledDate = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   //ReferalFee = pa.Deductible < 0 ? 0 : (decimal)pa.Deductible,
            //                   //ReferalFeeReceivedDate = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   //ReferalFeeReceivedBank = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   //ReferalFeeReceivedChequeNumber = string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme,
            //                   //ReferalFeeReceivedTtTransfer = string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme,
            //                   PaymentGivenToAccount = pa.PaymentGivenToAccount < 0 ? 0 : (decimal)pa.PaymentGivenToAccount,
            //                   Remark = string.IsNullOrEmpty(pa.Remark) ? "" : pa.Remark,
            //                   PatientID = pa.PatientID < 0 ? 0 : (int)pa.PatientID,
            //                   IncurredAmount = pa.IncurredAmount < 0 ? 0 : (decimal)pa.IncurredAmount,
            //                   ClaimDocumentReceivedDate = pa.ClaimDocumentReceivedDate != null ? Convert.ToDateTime(pa.ClaimDocumentReceivedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   ClaimDocumentsEmailedDate = pa.ClaimDocumentsEmailedDate != null ? Convert.ToDateTime(pa.ClaimDocumentsEmailedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   PaymentAdviceReceviedDate = pa.PaymentAdviceReceviedDate != null ? Convert.ToDateTime(pa.PaymentAdviceReceviedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   PaymentAdviceEmailedDate = pa.PaymentAdviceEmailedDate != null ? Convert.ToDateTime(pa.PaymentAdviceEmailedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   TotalAmountPaid = pa.TotalAmountPaid < 0 ? 0 : (decimal)pa.TotalAmountPaid,
            //                   CurrencyType = pa.CurrencyType < 0 ? 0 : (int)pa.CurrencyType,
            //                   OriginalDocumentscourieredDate = pa.OriginalDocumentscourieredDate != null ? Convert.ToDateTime(pa.OriginalDocumentscourieredDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   AirwayBillNo = string.IsNullOrEmpty(pa.AirwayBillNo) ? "" : pa.AirwayBillNo,
            //                   BUID = pa.BUID < 0 ? 0 : (int)pa.BUID,
            //                   CreateBy = pa.CreateBy < 0 ? 0 : (int)pa.CreateBy,
            //                   CreatedDate = pa.CreatedDate != null ? Convert.ToDateTime(pa.CreatedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   ModifiedBy = pa.ModifiedBy < 0 ? 0 : (int)pa.ModifiedBy,
            //                   ModifiedDate = pa.ModifiedDate != null ? Convert.ToDateTime(pa.ModifiedDate).ToString("dd/MM/yyyy") : string.Empty,
                               

            //               }).ToList();
            //    return qry;
            

            
        }

        public List<AdmissionVM> GetAllPilotAdmissions(int businessUnitID)
        {
            var clientData = unitOfWork.TblPatientAdmission.Get(x => x.BUID == businessUnitID).ToList();
            List<AdmissionVM> clientList = new List<AdmissionVM>();
            //clientList = new List<AdmissionVM>();

            //clientList = new List<AdmissionVM>();
            foreach (var pa in clientData)

            {
                AdmissionVM admissionVM = new AdmissionVM();
                admissionVM.PatientAdmissionId = pa.PatientAdmissionId < 0 ? 0 : pa.PatientAdmissionId;
                admissionVM.ReferenceNo = string.IsNullOrEmpty(pa.ReferenceNo) ? "" : pa.ReferenceNo.Trim();
                admissionVM.PatientName = string.IsNullOrEmpty(pa.PatientName) ? "" : pa.PatientName.Trim();
                admissionVM.DateOfBirth = pa.DateOfBirth != null ? Convert.ToDateTime(pa.DateOfBirth).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.PassportNumber = string.IsNullOrEmpty(pa.PassportNumber) ? "" : pa.PassportNumber;
                admissionVM.Scheme = string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme;
                admissionVM.InceptionDate = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.Deductible = pa.Deductible < 0 ? 0 : (decimal)pa.Deductible;
                //DeductibleUsedForTheYear= pa.DeductibleUsedForTheYear < 0 ? 0 : (decimal)pa.DeductibleUsedForTheYear;
                admissionVM.Exclusions = pa.Exclusions < 0 ? 0 : (decimal)pa.Exclusions;
                admissionVM.Hospital = string.IsNullOrEmpty(pa.Hospital) ? "" : pa.Hospital;
                admissionVM.AdmissionDate = pa.AdmissionDate != null ? Convert.ToDateTime(pa.AdmissionDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.DischargedDate = pa.DischargeDate != null ? Convert.ToDateTime(pa.DischargeDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.BHTNumber = string.IsNullOrEmpty(pa.BHTNumber) ? "" : pa.BHTNumber;
                admissionVM.Illness = string.IsNullOrEmpty(pa.Illness) ? "" : pa.Illness;
                admissionVM.ConsultantName = string.IsNullOrEmpty(pa.ConsultantName) ? "" : pa.ConsultantName;
                admissionVM.InformedBy = string.IsNullOrEmpty(pa.InformedBy) ? "" : pa.InformedBy;
                admissionVM.GOPAmount = pa.GOPAmount < 0 ? 0 : (decimal)pa.GOPAmount;
                //GOPConfirmBy = string.IsNullOrEmpty(pa.GOPConfirmBy) ? "" : pa.GOPConfirmBy;
                //GOPIssueDate = pa.GOPIssueDate != null ? Convert.ToDateTime(pa.GOPIssueDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.ExtendedGOP = pa.ExtendedGOP < 0 ? 0 : (decimal)pa.ExtendedGOP;
                admissionVM.HandledBy = string.IsNullOrEmpty(pa.HandledBy) ? "" : pa.HandledBy;
                admissionVM.FinalAmount = pa.FinalAmount < 0 ? 0 : (decimal)pa.FinalAmount;
                admissionVM.ConsultantFee = pa.ConsultantFee < 0 ? 0 : (decimal)pa.ConsultantFee;
                admissionVM.FinalBillRecievedDate = pa.FinalBillReceivedDate != null ? Convert.ToDateTime(pa.FinalBillReceivedDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.FinalBillGivenToSgs = pa.Deductible < 0 ? 0 : (decimal)pa.Deductible;
                admissionVM.ClaimsDcsSentToAviva = pa.AVIVADCSClaims < 0 ? 0 : (decimal)pa.AVIVADCSClaims;
                admissionVM.ClaimSettledDate = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.ReferalFee = pa.Deductible < 0 ? 0 : (decimal)pa.Deductible;
                admissionVM.ReferalFeeReceivedDate = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.ReferalFeeReceivedBank = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.ReferalFeeReceivedChequeNumber = string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme;
                admissionVM.ReferalFeeReceivedTtTransfer = string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme;
                admissionVM.PaymentGivenToAccount = pa.PaymentGivenToAccount < 0 ? 0 : (decimal)pa.PaymentGivenToAccount;
                admissionVM.Remark = string.IsNullOrEmpty(pa.Remark) ? "" : pa.Remark;
                admissionVM.PatientID = pa.PatientID < 0 ? 0 : (int)pa.PatientID;
                admissionVM.IncurredAmount = pa.IncurredAmount < 0 ? 0 : (decimal)pa.IncurredAmount;
                admissionVM.ClaimDocumentReceivedDate = pa.ClaimDocumentReceivedDate != null ? Convert.ToDateTime(pa.ClaimDocumentReceivedDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.ClaimDocumentsEmailedDate = pa.ClaimDocumentsEmailedDate != null ? Convert.ToDateTime(pa.ClaimDocumentsEmailedDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.PaymentAdviceReceviedDate = pa.PaymentAdviceReceviedDate != null ? Convert.ToDateTime(pa.PaymentAdviceReceviedDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.PaymentAdviceEmailedDate = pa.PaymentAdviceEmailedDate != null ? Convert.ToDateTime(pa.PaymentAdviceEmailedDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.TotalAmountPaid = pa.TotalAmountPaid < 0 ? 0 : (decimal)pa.TotalAmountPaid;
                admissionVM.CurrencyType = pa.CurrencyType < 0 ? 0 : (int)pa.CurrencyType;
                admissionVM.OriginalDocumentscourieredDate = pa.OriginalDocumentscourieredDate != null ? Convert.ToDateTime(pa.OriginalDocumentscourieredDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.AirwayBillNo = string.IsNullOrEmpty(pa.AirwayBillNo) ? "" : pa.AirwayBillNo;
                admissionVM.BUID = pa.BUID < 0 ? 0 : (int)pa.BUID;
                admissionVM.CreateBy = pa.CreateBy < 0 ? 0 : (int)pa.CreateBy;
                admissionVM.CreatedDate = pa.CreatedDate != null ? Convert.ToDateTime(pa.CreatedDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.ModifiedBy = 0;
                admissionVM.ModifiedDate = pa.ModifiedDate != null ? Convert.ToDateTime(pa.ModifiedDate).ToString("dd/MM/yyyy") : string.Empty;
                clientList.Add(admissionVM);

            }
            return clientList;

            //using (var db = new PERFECTIBSEntities())
            //{
            //    var qry = (from pa in db.PatientAdmissions
            //               where pa.BUID == businessUnitID
            //               select new AdmissionVM
            //               {

            //                   ReferenceNo = string.IsNullOrEmpty(pa.ReferenceNo) ? "" : pa.ReferenceNo.Trim(),
            //                   PatientName = string.IsNullOrEmpty(pa.PatientName) ? "" : pa.PatientName.Trim(),
            //                   DateOfBirth = pa.DateOfBirth != null ? Convert.ToDateTime(pa.DateOfBirth).ToString("dd/MM/yyyy") : string.Empty,
            //                   PassportNumber = string.IsNullOrEmpty(pa.PassportNumber) ? "" : pa.PassportNumber,
            //                   Scheme= string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme,
            //                   InceptionDate= pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   Deductible= pa.Deductible <0 ? 0 : (decimal)pa.Deductible,
            //                   //DeductibleUsedForTheYear= pa.DeductibleUsedForTheYear < 0 ? 0 : (decimal)pa.DeductibleUsedForTheYear,
            //                   Exclusions= pa.Exclusions < 0 ? 0 : (decimal)pa.Exclusions,
            //                   Hospital = string.IsNullOrEmpty(pa.Hospital) ? "" : pa.Hospital,
            //                   AdmissionDate = pa.AdmissionDate != null ? Convert.ToDateTime(pa.AdmissionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   DischargedDate= pa.DischargeDate != null ? Convert.ToDateTime(pa.DischargeDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   BHTNumber = string.IsNullOrEmpty(pa.BHTNumber) ? "" : pa.BHTNumber,
            //                   Illness = string.IsNullOrEmpty(pa.Illness) ? "" : pa.Illness,
            //                   ConsultantName = string.IsNullOrEmpty(pa.ConsultantName) ? "" : pa.ConsultantName,
            //                   InformedBy = string.IsNullOrEmpty(pa.InformedBy) ? "" : pa.InformedBy,
            //                   GOPAmount = pa.GOPAmount < 0 ? 0 : (decimal)pa.GOPAmount,
            //                   //GOPConfirmBy = string.IsNullOrEmpty(pa.GOPConfirmBy) ? "" : pa.GOPConfirmBy,
            //                   //GOPIssueDate = pa.GOPIssueDate != null ? Convert.ToDateTime(pa.GOPIssueDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   ExtendedGOP = pa.ExtendedGOP < 0 ? 0 : (decimal)pa.ExtendedGOP,
            //                   HandledBy = string.IsNullOrEmpty(pa.HandledBy) ? "" : pa.HandledBy,
            //                   FinalAmount = pa.FinalAmount < 0 ? 0 : (decimal)pa.FinalAmount,
            //                   ConsultantFee = pa.ConsultantFee < 0 ? 0 : (decimal)pa.ConsultantFee,
            //                   //FinalBillRecievedDate = pa.FinalBillRecievedDate != null ? Convert.ToDateTime(pa.FinalBillRecievedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   //FinalBillGivenToSgs = pa.Deductible < 0 ? 0 : (decimal)pa.Deductible,
            //                   //ClaimsDcsSentToAviva = pa.ClaimsDcsSentToAviva < 0 ? 0 : (decimal)pa.ClaimsDcsSentToAviva,
            //                   //ClaimSettledDate = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   //ReferalFee = pa.Deductible < 0 ? 0 : (decimal)pa.Deductible,
            //                   //ReferalFeeReceivedDate = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   //ReferalFeeReceivedBank = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   //ReferalFeeReceivedChequeNumber = string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme,
            //                   //ReferalFeeReceivedTtTransfer = string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme,
            //                   PaymentGivenToAccount = pa.PaymentGivenToAccount < 0 ? 0 : (decimal)pa.PaymentGivenToAccount,
            //                   Remark = string.IsNullOrEmpty(pa.Remark) ? "" : pa.Remark,
            //                   PatientID = pa.PatientID < 0 ? 0 : (int)pa.PatientID,
            //                   IncurredAmount = pa.IncurredAmount < 0 ? 0 : (decimal)pa.IncurredAmount,
            //                   ClaimDocumentReceivedDate = pa.ClaimDocumentReceivedDate != null ? Convert.ToDateTime(pa.ClaimDocumentReceivedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   ClaimDocumentsEmailedDate = pa.ClaimDocumentsEmailedDate != null ? Convert.ToDateTime(pa.ClaimDocumentsEmailedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   PaymentAdviceReceviedDate = pa.PaymentAdviceReceviedDate != null ? Convert.ToDateTime(pa.PaymentAdviceReceviedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   PaymentAdviceEmailedDate = pa.PaymentAdviceEmailedDate != null ? Convert.ToDateTime(pa.PaymentAdviceEmailedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   TotalAmountPaid = pa.TotalAmountPaid < 0 ? 0 : (decimal)pa.TotalAmountPaid,
            //                   CurrencyType = pa.CurrencyType < 0 ? 0 : (int)pa.CurrencyType,
            //                   OriginalDocumentscourieredDate = pa.OriginalDocumentscourieredDate != null ? Convert.ToDateTime(pa.OriginalDocumentscourieredDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   AirwayBillNo = string.IsNullOrEmpty(pa.AirwayBillNo) ? "" : pa.AirwayBillNo,
            //                   BUID = pa.BUID < 0 ? 0 : (int)pa.BUID,
            //                   CreateBy = pa.CreateBy < 0 ? 0 : (int)pa.CreateBy,
            //                   CreatedDate = pa.CreatedDate != null ? Convert.ToDateTime(pa.CreatedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   ModifiedBy = pa.ModifiedBy < 0 ? 0 : (int)pa.ModifiedBy,
            //                   ModifiedDate = pa.ModifiedDate != null ? Convert.ToDateTime(pa.ModifiedDate).ToString("dd/MM/yyyy") : string.Empty,


            //               }).ToList();
            //    return qry;



        }



        public List<AdmissionVM> GetAllSingporeAdmissions(int businessUnitID)
        {
            var clientData = unitOfWork.tblAdmission_Singapore.Get().ToList();
            List<AdmissionVM> clientList = new List<AdmissionVM>();
            //clientList = new List<AdmissionVM>();

            //clientList = new List<AdmissionVM>();
            foreach (var pa in clientData)

            {
                AdmissionVM admissionVM = new AdmissionVM();
                admissionVM.PatientAdmissionId = pa.SingaporeAdmissionId < 0 ? 0 : pa.SingaporeAdmissionId;
                admissionVM.ReferenceNo = string.IsNullOrEmpty(pa.ReferenceNo) ? "" : pa.ReferenceNo.Trim();
                admissionVM.PatientName = string.IsNullOrEmpty(pa.PatientName) ? "" : pa.PatientName.Trim();
                admissionVM.DateOfBirth = pa.DateOfBirth != null ? Convert.ToDateTime(pa.DateOfBirth).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.PassportNumber = string.IsNullOrEmpty(pa.PassportNumber) ? "" : pa.PassportNumber;
                admissionVM.Scheme = string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme;
                admissionVM.InceptionDate = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.Deductible = pa.Deductible < 0 ? 0 : (decimal)pa.Deductible;
                admissionVM.DeductibleUsedForTheYear = pa.DeductibleForTheYear < 0 ? 0 : (decimal)pa.DeductibleForTheYear;
                admissionVM.Exclusions = pa.Exclusions < 0 ? 0 : (decimal)pa.Exclusions;
                admissionVM.Hospital = string.IsNullOrEmpty(pa.Hospital) ? "" : pa.Hospital;
                admissionVM.AdmissionDate = pa.AdmissionDate != null ? Convert.ToDateTime(pa.AdmissionDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.DischargedDate = pa.DischargeDate != null ? Convert.ToDateTime(pa.DischargeDate).ToString("dd/MM/yyyy") : string.Empty;
               // admissionVM.cu = string.IsNullOrEmpty(pa.BHTNumber) ? "" : pa.BHTNumber;
                admissionVM.Illness = string.IsNullOrEmpty(pa.Illness) ? "" : pa.Illness;
                admissionVM.ConsultantName = string.IsNullOrEmpty(pa.ConsultantName) ? "" : pa.ConsultantName;
                admissionVM.InformedBy = string.IsNullOrEmpty(pa.InformedBy) ? "" : pa.InformedBy;
                admissionVM.GOPAmount = pa.GOPAmount < 0 ? 0 : (decimal)pa.GOPAmount;
                //GOPConfirmBy = string.IsNullOrEmpty(pa.GOPConfirmBy) ? "" : pa.GOPConfirmBy;
                //GOPIssueDate = pa.GOPIssueDate != null ? Convert.ToDateTime(pa.GOPIssueDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.ExtendedGOP = pa.ExtendedGOP < 0 ? 0 : (decimal)pa.ExtendedGOP;
             //   admissionVM.HandledBy = string.IsNullOrEmpty(pa.HandledBy) ? "" : pa.HandledBy;
                admissionVM.FinalAmount = pa.FinalAmount < 0 ? 0 : (decimal)pa.FinalAmount;
                admissionVM.ConsultantFee = pa.ConsultantFee < 0 ? 0 : (decimal)pa.ConsultantFee;
                admissionVM.FinalBillRecievedDate = pa.FinalBillReceivedDate != null ? Convert.ToDateTime(pa.FinalBillReceivedDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.FinalBillGivenToSgs = pa.Deductible < 0 ? 0 : (decimal)pa.Deductible;
             //   admissionVM.ClaimsDcsSentToAviva = pa.AVIVADCSClaims < 0 ? 0 : (decimal)pa.AVIVADCSClaims;
                admissionVM.ClaimSettledDate = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.ReferalFee = pa.Deductible < 0 ? 0 : (decimal)pa.Deductible;
                admissionVM.ReferalFeeReceivedDate = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.ReferalFeeReceivedBank = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty;
                admissionVM.ReferalFeeReceivedChequeNumber = string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme;
                admissionVM.ReferalFeeReceivedTtTransfer = string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme;
                admissionVM.PaymentGivenToAccount = pa.PaymentGivenToAccount < 0 ? 0 : (decimal)pa.PaymentGivenToAccount;
                admissionVM.Remark = string.IsNullOrEmpty(pa.Remark) ? "" : pa.Remark;
              //  admissionVM.PatientID = pa.PatientID < 0 ? 0 : (int)pa.PatientID;
               // admissionVM.IncurredAmount = pa.IncurredAmount < 0 ? 0 : (decimal)pa.IncurredAmount;
               // admissionVM.ClaimDocumentReceivedDate = pa.ClaimDocumentReceivedDate != null ? Convert.ToDateTime(pa.ClaimDocumentReceivedDate).ToString("dd/MM/yyyy") : string.Empty;
               // admissionVM.ClaimDocumentsEmailedDate = pa.ClaimDocumentsEmailedDate != null ? Convert.ToDateTime(pa.ClaimDocumentsEmailedDate).ToString("dd/MM/yyyy") : string.Empty;
               // admissionVM.PaymentAdviceReceviedDate = pa.PaymentAdviceReceviedDate != null ? Convert.ToDateTime(pa.PaymentAdviceReceviedDate).ToString("dd/MM/yyyy") : string.Empty;
              //  admissionVM.PaymentAdviceEmailedDate = pa.PaymentAdviceEmailedDate != null ? Convert.ToDateTime(pa.PaymentAdviceEmailedDate).ToString("dd/MM/yyyy") : string.Empty;
              //  admissionVM.TotalAmountPaid = pa.TotalAmountPaid < 0 ? 0 : (decimal)pa.TotalAmountPaid;
              //  admissionVM.CurrencyType = pa.CurrencyType < 0 ? 0 : (int)pa.CurrencyType;
              //  admissionVM.OriginalDocumentscourieredDate = pa.OriginalDocumentscourieredDate != null ? Convert.ToDateTime(pa.OriginalDocumentscourieredDate).ToString("dd/MM/yyyy") : string.Empty;
              //  admissionVM.AirwayBillNo = string.IsNullOrEmpty(pa.AirwayBillNo) ? "" : pa.AirwayBillNo;
             //   admissionVM.BUID = pa.BUID < 0 ? 0 : (int)pa.BUID;
              //  admissionVM.CreateBy = pa.CreateBy < 0 ? 0 : (int)pa.CreateBy;
              //  admissionVM.CreatedDate = pa.CreatedDate != null ? Convert.ToDateTime(pa.CreatedDate).ToString("dd/MM/yyyy") : string.Empty;
              //  admissionVM.ModifiedBy = 0;
              //  admissionVM.ModifiedDate = pa.ModifiedDate != null ? Convert.ToDateTime(pa.ModifiedDate).ToString("dd/MM/yyyy") : string.Empty;
                clientList.Add(admissionVM);

            }
            return clientList;

            //using (var db = new PERFECTIBSEntities())
            //{
            //    var qry = (from pa in db.PatientAdmissions
            //               where pa.BUID == businessUnitID
            //               select new AdmissionVM
            //               {

            //                   ReferenceNo = string.IsNullOrEmpty(pa.ReferenceNo) ? "" : pa.ReferenceNo.Trim(),
            //                   PatientName = string.IsNullOrEmpty(pa.PatientName) ? "" : pa.PatientName.Trim(),
            //                   DateOfBirth = pa.DateOfBirth != null ? Convert.ToDateTime(pa.DateOfBirth).ToString("dd/MM/yyyy") : string.Empty,
            //                   PassportNumber = string.IsNullOrEmpty(pa.PassportNumber) ? "" : pa.PassportNumber,
            //                   Scheme= string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme,
            //                   InceptionDate= pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   Deductible= pa.Deductible <0 ? 0 : (decimal)pa.Deductible,
            //                   //DeductibleUsedForTheYear= pa.DeductibleUsedForTheYear < 0 ? 0 : (decimal)pa.DeductibleUsedForTheYear,
            //                   Exclusions= pa.Exclusions < 0 ? 0 : (decimal)pa.Exclusions,
            //                   Hospital = string.IsNullOrEmpty(pa.Hospital) ? "" : pa.Hospital,
            //                   AdmissionDate = pa.AdmissionDate != null ? Convert.ToDateTime(pa.AdmissionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   DischargedDate= pa.DischargeDate != null ? Convert.ToDateTime(pa.DischargeDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   BHTNumber = string.IsNullOrEmpty(pa.BHTNumber) ? "" : pa.BHTNumber,
            //                   Illness = string.IsNullOrEmpty(pa.Illness) ? "" : pa.Illness,
            //                   ConsultantName = string.IsNullOrEmpty(pa.ConsultantName) ? "" : pa.ConsultantName,
            //                   InformedBy = string.IsNullOrEmpty(pa.InformedBy) ? "" : pa.InformedBy,
            //                   GOPAmount = pa.GOPAmount < 0 ? 0 : (decimal)pa.GOPAmount,
            //                   //GOPConfirmBy = string.IsNullOrEmpty(pa.GOPConfirmBy) ? "" : pa.GOPConfirmBy,
            //                   //GOPIssueDate = pa.GOPIssueDate != null ? Convert.ToDateTime(pa.GOPIssueDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   ExtendedGOP = pa.ExtendedGOP < 0 ? 0 : (decimal)pa.ExtendedGOP,
            //                   HandledBy = string.IsNullOrEmpty(pa.HandledBy) ? "" : pa.HandledBy,
            //                   FinalAmount = pa.FinalAmount < 0 ? 0 : (decimal)pa.FinalAmount,
            //                   ConsultantFee = pa.ConsultantFee < 0 ? 0 : (decimal)pa.ConsultantFee,
            //                   //FinalBillRecievedDate = pa.FinalBillRecievedDate != null ? Convert.ToDateTime(pa.FinalBillRecievedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   //FinalBillGivenToSgs = pa.Deductible < 0 ? 0 : (decimal)pa.Deductible,
            //                   //ClaimsDcsSentToAviva = pa.ClaimsDcsSentToAviva < 0 ? 0 : (decimal)pa.ClaimsDcsSentToAviva,
            //                   //ClaimSettledDate = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   //ReferalFee = pa.Deductible < 0 ? 0 : (decimal)pa.Deductible,
            //                   //ReferalFeeReceivedDate = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   //ReferalFeeReceivedBank = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   //ReferalFeeReceivedChequeNumber = string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme,
            //                   //ReferalFeeReceivedTtTransfer = string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme,
            //                   PaymentGivenToAccount = pa.PaymentGivenToAccount < 0 ? 0 : (decimal)pa.PaymentGivenToAccount,
            //                   Remark = string.IsNullOrEmpty(pa.Remark) ? "" : pa.Remark,
            //                   PatientID = pa.PatientID < 0 ? 0 : (int)pa.PatientID,
            //                   IncurredAmount = pa.IncurredAmount < 0 ? 0 : (decimal)pa.IncurredAmount,
            //                   ClaimDocumentReceivedDate = pa.ClaimDocumentReceivedDate != null ? Convert.ToDateTime(pa.ClaimDocumentReceivedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   ClaimDocumentsEmailedDate = pa.ClaimDocumentsEmailedDate != null ? Convert.ToDateTime(pa.ClaimDocumentsEmailedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   PaymentAdviceReceviedDate = pa.PaymentAdviceReceviedDate != null ? Convert.ToDateTime(pa.PaymentAdviceReceviedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   PaymentAdviceEmailedDate = pa.PaymentAdviceEmailedDate != null ? Convert.ToDateTime(pa.PaymentAdviceEmailedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   TotalAmountPaid = pa.TotalAmountPaid < 0 ? 0 : (decimal)pa.TotalAmountPaid,
            //                   CurrencyType = pa.CurrencyType < 0 ? 0 : (int)pa.CurrencyType,
            //                   OriginalDocumentscourieredDate = pa.OriginalDocumentscourieredDate != null ? Convert.ToDateTime(pa.OriginalDocumentscourieredDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   AirwayBillNo = string.IsNullOrEmpty(pa.AirwayBillNo) ? "" : pa.AirwayBillNo,
            //                   BUID = pa.BUID < 0 ? 0 : (int)pa.BUID,
            //                   CreateBy = pa.CreateBy < 0 ? 0 : (int)pa.CreateBy,
            //                   CreatedDate = pa.CreatedDate != null ? Convert.ToDateTime(pa.CreatedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   ModifiedBy = pa.ModifiedBy < 0 ? 0 : (int)pa.ModifiedBy,
            //                   ModifiedDate = pa.ModifiedDate != null ? Convert.ToDateTime(pa.ModifiedDate).ToString("dd/MM/yyyy") : string.Empty,


            //               }).ToList();
            //    return qry;



        }
        public List<MembershipVM> GetClientBybusinessUnitID(int businessUnitID)
        {
            var clientData = unitOfWork.TblClientRepository.Get(x => x.BUID == businessUnitID).ToList();


            List<MembershipVM> clientList = new List<MembershipVM>();
            //clientList = new List<AdmissionVM>();

            //clientList = new List<AdmissionVM>();

          



            foreach (var pa in clientData)

            {
                var clientFamilyData = unitOfWork.TblFamilyMemberRepository.Get(x => x.ClientID == pa.ClientID).ToList();
                var clientReqData = unitOfWork.TblClientRequestHeaderRepository.Get(x => x.ClientID == pa.ClientID).ToList();


                foreach (var pR in clientReqData)

                {
                    var admissionReq = new MembershipVM();

                    if (pR.MembershipID!=null)
                    admissionReq.MembershipID = pR.MembershipID;

                    clientList.Add(admissionReq);

                }





                foreach (var paF in clientFamilyData)

                {
                    var admissionFVM = new MembershipVM();
                    admissionFVM.MembershipID = paF.MembershipID;

                    clientList.Add(admissionFVM);

                }



                    var clientGroupFamilyData = unitOfWork.TblGrpFamilyDetailRepository.Get(x => x.ClientID == pa.ClientID).ToList();
                foreach (var paGF in clientGroupFamilyData)

                {
                    var admissionGVM = new MembershipVM();
                    admissionGVM.MembershipID = paGF.MembershipID;

                    clientList.Add(admissionGVM);

                }




               
               
               

            }
            return clientList;

            //using (var db = new PERFECTIBSEntities())
            //{
            //    var qry = (from pa in db.PatientAdmissions
            //               where pa.BUID == businessUnitID
            //               select new AdmissionVM
            //               {

            //                   ReferenceNo = string.IsNullOrEmpty(pa.ReferenceNo) ? "" : pa.ReferenceNo.Trim(),
            //                   PatientName = string.IsNullOrEmpty(pa.PatientName) ? "" : pa.PatientName.Trim(),
            //                   DateOfBirth = pa.DateOfBirth != null ? Convert.ToDateTime(pa.DateOfBirth).ToString("dd/MM/yyyy") : string.Empty,
            //                   PassportNumber = string.IsNullOrEmpty(pa.PassportNumber) ? "" : pa.PassportNumber,
            //                   Scheme= string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme,
            //                   InceptionDate= pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   Deductible= pa.Deductible <0 ? 0 : (decimal)pa.Deductible,
            //                   //DeductibleUsedForTheYear= pa.DeductibleUsedForTheYear < 0 ? 0 : (decimal)pa.DeductibleUsedForTheYear,
            //                   Exclusions= pa.Exclusions < 0 ? 0 : (decimal)pa.Exclusions,
            //                   Hospital = string.IsNullOrEmpty(pa.Hospital) ? "" : pa.Hospital,
            //                   AdmissionDate = pa.AdmissionDate != null ? Convert.ToDateTime(pa.AdmissionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   DischargedDate= pa.DischargeDate != null ? Convert.ToDateTime(pa.DischargeDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   BHTNumber = string.IsNullOrEmpty(pa.BHTNumber) ? "" : pa.BHTNumber,
            //                   Illness = string.IsNullOrEmpty(pa.Illness) ? "" : pa.Illness,
            //                   ConsultantName = string.IsNullOrEmpty(pa.ConsultantName) ? "" : pa.ConsultantName,
            //                   InformedBy = string.IsNullOrEmpty(pa.InformedBy) ? "" : pa.InformedBy,
            //                   GOPAmount = pa.GOPAmount < 0 ? 0 : (decimal)pa.GOPAmount,
            //                   //GOPConfirmBy = string.IsNullOrEmpty(pa.GOPConfirmBy) ? "" : pa.GOPConfirmBy,
            //                   //GOPIssueDate = pa.GOPIssueDate != null ? Convert.ToDateTime(pa.GOPIssueDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   ExtendedGOP = pa.ExtendedGOP < 0 ? 0 : (decimal)pa.ExtendedGOP,
            //                   HandledBy = string.IsNullOrEmpty(pa.HandledBy) ? "" : pa.HandledBy,
            //                   FinalAmount = pa.FinalAmount < 0 ? 0 : (decimal)pa.FinalAmount,
            //                   ConsultantFee = pa.ConsultantFee < 0 ? 0 : (decimal)pa.ConsultantFee,
            //                   //FinalBillRecievedDate = pa.FinalBillRecievedDate != null ? Convert.ToDateTime(pa.FinalBillRecievedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   //FinalBillGivenToSgs = pa.Deductible < 0 ? 0 : (decimal)pa.Deductible,
            //                   //ClaimsDcsSentToAviva = pa.ClaimsDcsSentToAviva < 0 ? 0 : (decimal)pa.ClaimsDcsSentToAviva,
            //                   //ClaimSettledDate = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   //ReferalFee = pa.Deductible < 0 ? 0 : (decimal)pa.Deductible,
            //                   //ReferalFeeReceivedDate = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   //ReferalFeeReceivedBank = pa.InceptionDate != null ? Convert.ToDateTime(pa.InceptionDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   //ReferalFeeReceivedChequeNumber = string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme,
            //                   //ReferalFeeReceivedTtTransfer = string.IsNullOrEmpty(pa.Scheme) ? "" : pa.Scheme,
            //                   PaymentGivenToAccount = pa.PaymentGivenToAccount < 0 ? 0 : (decimal)pa.PaymentGivenToAccount,
            //                   Remark = string.IsNullOrEmpty(pa.Remark) ? "" : pa.Remark,
            //                   PatientID = pa.PatientID < 0 ? 0 : (int)pa.PatientID,
            //                   IncurredAmount = pa.IncurredAmount < 0 ? 0 : (decimal)pa.IncurredAmount,
            //                   ClaimDocumentReceivedDate = pa.ClaimDocumentReceivedDate != null ? Convert.ToDateTime(pa.ClaimDocumentReceivedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   ClaimDocumentsEmailedDate = pa.ClaimDocumentsEmailedDate != null ? Convert.ToDateTime(pa.ClaimDocumentsEmailedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   PaymentAdviceReceviedDate = pa.PaymentAdviceReceviedDate != null ? Convert.ToDateTime(pa.PaymentAdviceReceviedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   PaymentAdviceEmailedDate = pa.PaymentAdviceEmailedDate != null ? Convert.ToDateTime(pa.PaymentAdviceEmailedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   TotalAmountPaid = pa.TotalAmountPaid < 0 ? 0 : (decimal)pa.TotalAmountPaid,
            //                   CurrencyType = pa.CurrencyType < 0 ? 0 : (int)pa.CurrencyType,
            //                   OriginalDocumentscourieredDate = pa.OriginalDocumentscourieredDate != null ? Convert.ToDateTime(pa.OriginalDocumentscourieredDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   AirwayBillNo = string.IsNullOrEmpty(pa.AirwayBillNo) ? "" : pa.AirwayBillNo,
            //                   BUID = pa.BUID < 0 ? 0 : (int)pa.BUID,
            //                   CreateBy = pa.CreateBy < 0 ? 0 : (int)pa.CreateBy,
            //                   CreatedDate = pa.CreatedDate != null ? Convert.ToDateTime(pa.CreatedDate).ToString("dd/MM/yyyy") : string.Empty,
            //                   ModifiedBy = pa.ModifiedBy < 0 ? 0 : (int)pa.ModifiedBy,
            //                   ModifiedDate = pa.ModifiedDate != null ? Convert.ToDateTime(pa.ModifiedDate).ToString("dd/MM/yyyy") : string.Empty,


            //               }).ToList();
            //    return qry;



        }






        #endregion

        #region Get Local Admission by reference no
        public AdmissionVM GetLocalAdmissionByReferenceNo(string refNo)
        {
            try
            {
                int id = 0;
               id = int.Parse(refNo);
                var TblPatientAdmission = unitOfWork.TblPatientAdmission.Get(x=>x.PatientAdmissionId== id).ToList();

                var AdmissionVM = new AdmissionVM();

                AdmissionVM.PatientAdmissionId = TblPatientAdmission[0].PatientAdmissionId < 0 ? 0 : TblPatientAdmission[0].PatientAdmissionId;
                AdmissionVM.ReferenceNo = string.IsNullOrEmpty(TblPatientAdmission[0].ReferenceNo) ? "" : TblPatientAdmission[0].ReferenceNo.Trim();
                AdmissionVM.PatientName = string.IsNullOrEmpty(TblPatientAdmission[0].PatientName) ? "" : TblPatientAdmission[0].PatientName.Trim();
                AdmissionVM.DateOfBirth = TblPatientAdmission[0].DateOfBirth != null ? Convert.ToDateTime(TblPatientAdmission[0].DateOfBirth).ToString("dd/MM/yyyy") : string.Empty;
                                 AdmissionVM.PassportNumber = string.IsNullOrEmpty(TblPatientAdmission[0].PassportNumber) ? "" : TblPatientAdmission[0].PassportNumber;
                AdmissionVM.Scheme = string.IsNullOrEmpty(TblPatientAdmission[0].Scheme) ? "" : TblPatientAdmission[0].Scheme;
                                AdmissionVM.InceptionDate = TblPatientAdmission[0].InceptionDate != null ?Convert.ToDateTime( TblPatientAdmission[0].InceptionDate).ToString("dd/MM/yyyy") : string.Empty;
                                AdmissionVM.Deductible = TblPatientAdmission[0].Deductible < 0 ? 0 : (decimal)TblPatientAdmission[0].Deductible;
                                   //DeductibleUsedForTheYear= pa.DeductibleUsedForTheYear < 0 ? 0 : (decimal)pa.DeductibleUsedForTheYear;
                                AdmissionVM.Exclusions = TblPatientAdmission[0].Exclusions < 0 ? 0 : (decimal)TblPatientAdmission[0].Exclusions;
                                AdmissionVM.Hospital = string.IsNullOrEmpty(TblPatientAdmission[0].Hospital) ? "" : TblPatientAdmission[0].Hospital;
                                AdmissionVM.AdmissionDate = TblPatientAdmission[0].AdmissionDate != null ? Convert.ToDateTime(TblPatientAdmission[0].AdmissionDate).ToString("dd/MM/yyyy") : string.Empty;
                                AdmissionVM.DischargedDate = TblPatientAdmission[0].DischargeDate != null ? Convert.ToDateTime(TblPatientAdmission[0].DischargeDate).ToString("dd/MM/yyyy") : string.Empty;
                                AdmissionVM.BHTNumber = string.IsNullOrEmpty(TblPatientAdmission[0].BHTNumber) ? "" : TblPatientAdmission[0].BHTNumber;
                                AdmissionVM.Illness = string.IsNullOrEmpty(TblPatientAdmission[0].Illness) ? "" : TblPatientAdmission[0].Illness;
                                AdmissionVM.ConsultantName = string.IsNullOrEmpty(TblPatientAdmission[0].ConsultantName) ? "" : TblPatientAdmission[0].ConsultantName;
                                AdmissionVM.InformedBy = string.IsNullOrEmpty(TblPatientAdmission[0].InformedBy) ? "" : TblPatientAdmission[0].InformedBy;
                                 AdmissionVM.GOPAmount = TblPatientAdmission[0].GOPAmount < 0 ? 0 : (decimal)TblPatientAdmission[0].GOPAmount;
                                AdmissionVM. GOPConfirmBy = "";
                AdmissionVM.GOPIssueDate = DateTime.Now.ToString("dd/MM/yyyy");
                                 AdmissionVM.ExtendedGOP = TblPatientAdmission[0].ExtendedGOP < 0 ? 0 : (decimal)TblPatientAdmission[0].ExtendedGOP;
                                AdmissionVM.HandledBy = string.IsNullOrEmpty(TblPatientAdmission[0].HandledBy) ? "" : TblPatientAdmission[0].HandledBy;
                                AdmissionVM.FinalAmount = TblPatientAdmission[0].FinalAmount < 0 ? 0 : (decimal)TblPatientAdmission[0].FinalAmount;
                                 AdmissionVM.ConsultantFee = TblPatientAdmission[0].ConsultantFee < 0 ? 0 : (decimal)TblPatientAdmission[0].ConsultantFee;

                AdmissionVM.FinalBillRecievedDate = TblPatientAdmission[0].FinalBillReceivedDate != null ?Convert.ToDateTime( TblPatientAdmission[0].FinalBillReceivedDate).ToString("dd/MM/yyyy") : string.Empty;
                AdmissionVM.FinalBillGivenToSgs = TblPatientAdmission[0].Deductible < 0 ? 0 : (decimal)TblPatientAdmission[0].Deductible;
                AdmissionVM.ClaimsDcsSentToAviva = TblPatientAdmission[0].AVIVADCSClaims < 0 ? 0 : (decimal)TblPatientAdmission[0].AVIVADCSClaims;
                AdmissionVM.ClaimSettledDate = TblPatientAdmission[0].ClaimSettledDate!= null ? Convert.ToDateTime(TblPatientAdmission[0].ClaimSettledDate).ToString("dd/MM/yyyy") : string.Empty;
                AdmissionVM.ReferalFee = TblPatientAdmission[0].ReferalFee < 0 ? 0 : (decimal)TblPatientAdmission[0].ReferalFee;
                AdmissionVM.ReferalFeeReceivedDate = TblPatientAdmission[0].ReferalFeeReceivedDate != null ? Convert.ToDateTime( TblPatientAdmission[0].ReferalFeeReceivedDate).ToString("dd/MM/yyyy") : string.Empty;
                AdmissionVM.ReferalFeeReceivedBank = TblPatientAdmission[0].ReferalFeeReceivedBank != null ? TblPatientAdmission[0].ReferalFeeReceivedBank : string.Empty;
                AdmissionVM.ReferalFeeReceivedChequeNumber = string.IsNullOrEmpty(TblPatientAdmission[0].ReferalFeeReceivedChequeNo) ? "" : TblPatientAdmission[0].ReferalFeeReceivedChequeNo;
                AdmissionVM.ReferalFeeReceivedTtTransfer = string.IsNullOrEmpty(TblPatientAdmission[0].ReferalFeeReceivedTTNo) ? "" : TblPatientAdmission[0].ReferalFeeReceivedTTNo;
                AdmissionVM.PaymentGivenToAccount = TblPatientAdmission[0].PaymentGivenToAccount < 0 ? 0 : (decimal)TblPatientAdmission[0].PaymentGivenToAccount;
                               AdmissionVM.Remark = string.IsNullOrEmpty(TblPatientAdmission[0].Remark) ? "" : TblPatientAdmission[0].Remark;
                               AdmissionVM.PatientID = TblPatientAdmission[0].PatientID < 0 ? 0 : (int)TblPatientAdmission[0].PatientID;
                                AdmissionVM.IncurredAmount = TblPatientAdmission[0].IncurredAmount < 0 ? 0 : (decimal)TblPatientAdmission[0].IncurredAmount;
                                AdmissionVM.ClaimDocumentReceivedDate = TblPatientAdmission[0].ClaimDocumentReceivedDate != null ?Convert.ToDateTime( TblPatientAdmission[0].ClaimDocumentReceivedDate).ToString("dd/MM/yyyy") : string.Empty;
                               AdmissionVM.ClaimDocumentsEmailedDate = TblPatientAdmission[0].ClaimDocumentsEmailedDate != null ?Convert.ToDateTime(TblPatientAdmission[0].ClaimDocumentsEmailedDate).ToString("dd/MM/yyyy") : string.Empty;
                               AdmissionVM.PaymentAdviceReceviedDate = TblPatientAdmission[0].PaymentAdviceReceviedDate != null ?Convert.ToDateTime( TblPatientAdmission[0].PaymentAdviceReceviedDate).ToString("dd/MM/yyyy") : string.Empty;
                                AdmissionVM.PaymentAdviceEmailedDate = TblPatientAdmission[0].PaymentAdviceEmailedDate != null ?Convert.ToDateTime( TblPatientAdmission[0].PaymentAdviceEmailedDate).ToString("dd/MM/yyyy") : string.Empty;
                                AdmissionVM.TotalAmountPaid = TblPatientAdmission[0].TotalAmountPaid < 0 ? 0 : (decimal)TblPatientAdmission[0].TotalAmountPaid;
                                AdmissionVM.CurrencyType = TblPatientAdmission[0].CurrencyType < 0 ? 0 : (int)TblPatientAdmission[0].CurrencyType;
                               AdmissionVM.OriginalDocumentscourieredDate = TblPatientAdmission[0].OriginalDocumentscourieredDate != null ?Convert.ToDateTime(TblPatientAdmission[0].OriginalDocumentscourieredDate).ToString("dd/MM/yyyy") : string.Empty;
                               AdmissionVM.AirwayBillNo = string.IsNullOrEmpty(TblPatientAdmission[0].AirwayBillNo) ? "" : TblPatientAdmission[0].AirwayBillNo;
                               AdmissionVM.BUID = TblPatientAdmission[0].BUID < 0  || TblPatientAdmission[0].BUID==null? 0 : (int)TblPatientAdmission[0].BUID;
                               AdmissionVM.CreateBy = TblPatientAdmission[0].CreateBy < 0 || TblPatientAdmission[0].CreateBy==null ? 0 : (int)TblPatientAdmission[0].CreateBy;
                               AdmissionVM.CreatedDate = string.Empty;
                               AdmissionVM.ModifiedBy = TblPatientAdmission[0].ModifiedBy < 0 || TblPatientAdmission[0].ModifiedBy==null ? 0 : (int)TblPatientAdmission[0].ModifiedBy;
                               AdmissionVM.ModifiedDate = string.Empty;
                                AdmissionVM.MembershipID= string.IsNullOrEmpty(TblPatientAdmission[0].MembershipID) ? "" : TblPatientAdmission[0].MembershipID;
                AdmissionVM.CurrancyCode= string.IsNullOrEmpty(TblPatientAdmission[0].CurrancyCode) ? "" : TblPatientAdmission[0].CurrancyCode;



                return AdmissionVM;
                
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public AdmissionVM GetPIlotAdmissionByReferenceNo(string refNo)
        {
            try
            {
                int id = 0;
                id = int.Parse(refNo);
                var TblPatientAdmission = unitOfWork.TblPatientAdmission.Get(x => x.PatientAdmissionId == id).ToList();

                var AdmissionVM = new AdmissionVM();

                AdmissionVM.PatientAdmissionId = TblPatientAdmission[0].PatientAdmissionId < 0 ? 0 : TblPatientAdmission[0].PatientAdmissionId;
                AdmissionVM.ReferenceNo = string.IsNullOrEmpty(TblPatientAdmission[0].ReferenceNo) ? "" : TblPatientAdmission[0].ReferenceNo.Trim();
                AdmissionVM.PatientName = string.IsNullOrEmpty(TblPatientAdmission[0].PatientName) ? "" : TblPatientAdmission[0].PatientName.Trim();
                AdmissionVM.DateOfBirth = TblPatientAdmission[0].DateOfBirth != null ? Convert.ToDateTime(TblPatientAdmission[0].DateOfBirth).ToString("dd/MM/yyyy") : string.Empty;
                AdmissionVM.PassportNumber = string.IsNullOrEmpty(TblPatientAdmission[0].PassportNumber) ? "" : TblPatientAdmission[0].PassportNumber;
                AdmissionVM.Scheme = string.IsNullOrEmpty(TblPatientAdmission[0].Scheme) ? "" : TblPatientAdmission[0].Scheme;
                AdmissionVM.InceptionDate = TblPatientAdmission[0].InceptionDate != null ? Convert.ToDateTime(TblPatientAdmission[0].InceptionDate).ToString("dd/MM/yyyy") : string.Empty;
                AdmissionVM.Deductible = TblPatientAdmission[0].Deductible < 0 ? 0 : (decimal)TblPatientAdmission[0].Deductible;
                //DeductibleUsedForTheYear= pa.DeductibleUsedForTheYear < 0 ? 0 : (decimal)pa.DeductibleUsedForTheYear;
                AdmissionVM.Exclusions = TblPatientAdmission[0].Exclusions < 0 ? 0 : (decimal)TblPatientAdmission[0].Exclusions;
                AdmissionVM.Hospital = string.IsNullOrEmpty(TblPatientAdmission[0].Hospital) ? "" : TblPatientAdmission[0].Hospital;
                AdmissionVM.AdmissionDate = TblPatientAdmission[0].AdmissionDate != null ? Convert.ToDateTime(TblPatientAdmission[0].AdmissionDate).ToString("dd/MM/yyyy") : string.Empty;
                AdmissionVM.DischargedDate = TblPatientAdmission[0].DischargeDate != null ? Convert.ToDateTime(TblPatientAdmission[0].DischargeDate).ToString("dd/MM/yyyy") : string.Empty;
                AdmissionVM.BHTNumber = string.IsNullOrEmpty(TblPatientAdmission[0].BHTNumber) ? "" : TblPatientAdmission[0].BHTNumber;
                AdmissionVM.Illness = string.IsNullOrEmpty(TblPatientAdmission[0].Illness) ? "" : TblPatientAdmission[0].Illness;
                AdmissionVM.ConsultantName = string.IsNullOrEmpty(TblPatientAdmission[0].ConsultantName) ? "" : TblPatientAdmission[0].ConsultantName;
                AdmissionVM.InformedBy = string.IsNullOrEmpty(TblPatientAdmission[0].InformedBy) ? "" : TblPatientAdmission[0].InformedBy;
                AdmissionVM.GOPAmount = TblPatientAdmission[0].GOPAmount < 0 ? 0 : (decimal)TblPatientAdmission[0].GOPAmount;
                AdmissionVM.GOPConfirmBy = "";
                AdmissionVM.GOPIssueDate = DateTime.Now.ToString("dd/MM/yyyy");
                AdmissionVM.ExtendedGOP = TblPatientAdmission[0].ExtendedGOP < 0 ? 0 : (decimal)TblPatientAdmission[0].ExtendedGOP;
                AdmissionVM.HandledBy = string.IsNullOrEmpty(TblPatientAdmission[0].HandledBy) ? "" : TblPatientAdmission[0].HandledBy;
                AdmissionVM.FinalAmount = TblPatientAdmission[0].FinalAmount < 0 ? 0 : (decimal)TblPatientAdmission[0].FinalAmount;
                AdmissionVM.ConsultantFee = TblPatientAdmission[0].ConsultantFee < 0 ? 0 : (decimal)TblPatientAdmission[0].ConsultantFee;

                AdmissionVM.FinalBillRecievedDate = TblPatientAdmission[0].FinalBillReceivedDate != null ? Convert.ToDateTime(TblPatientAdmission[0].FinalBillReceivedDate).ToString("dd/MM/yyyy") : string.Empty;
                AdmissionVM.FinalBillGivenToSgs = TblPatientAdmission[0].Deductible < 0 ? 0 : (decimal)TblPatientAdmission[0].Deductible;
                AdmissionVM.ClaimsDcsSentToAviva = TblPatientAdmission[0].AVIVADCSClaims < 0 ? 0 : (decimal)TblPatientAdmission[0].AVIVADCSClaims;
                AdmissionVM.ClaimSettledDate = TblPatientAdmission[0].ClaimSettledDate != null ? Convert.ToDateTime(TblPatientAdmission[0].ClaimSettledDate).ToString("dd/MM/yyyy") : string.Empty;
                AdmissionVM.ReferalFee = TblPatientAdmission[0].ReferalFee < 0 ? 0 : (decimal)TblPatientAdmission[0].ReferalFee;
                AdmissionVM.ReferalFeeReceivedDate = TblPatientAdmission[0].ReferalFeeReceivedDate != null ? Convert.ToDateTime(TblPatientAdmission[0].ReferalFeeReceivedDate).ToString("dd/MM/yyyy") : string.Empty;
                AdmissionVM.ReferalFeeReceivedBank = TblPatientAdmission[0].ReferalFeeReceivedBank != null ? TblPatientAdmission[0].ReferalFeeReceivedBank : string.Empty;
                AdmissionVM.ReferalFeeReceivedChequeNumber = string.IsNullOrEmpty(TblPatientAdmission[0].ReferalFeeReceivedChequeNo) ? "" : TblPatientAdmission[0].ReferalFeeReceivedChequeNo;
                AdmissionVM.ReferalFeeReceivedTtTransfer = string.IsNullOrEmpty(TblPatientAdmission[0].ReferalFeeReceivedTTNo) ? "" : TblPatientAdmission[0].ReferalFeeReceivedTTNo;
                AdmissionVM.PaymentGivenToAccount = TblPatientAdmission[0].PaymentGivenToAccount < 0 ? 0 : (decimal)TblPatientAdmission[0].PaymentGivenToAccount;
                AdmissionVM.Remark = string.IsNullOrEmpty(TblPatientAdmission[0].Remark) ? "" : TblPatientAdmission[0].Remark;
                AdmissionVM.PatientID = TblPatientAdmission[0].PatientID < 0 ? 0 : (int)TblPatientAdmission[0].PatientID;
                AdmissionVM.IncurredAmount = TblPatientAdmission[0].IncurredAmount < 0 ? 0 : (decimal)TblPatientAdmission[0].IncurredAmount;
                AdmissionVM.ClaimDocumentReceivedDate = TblPatientAdmission[0].ClaimDocumentReceivedDate != null ? Convert.ToDateTime(TblPatientAdmission[0].ClaimDocumentReceivedDate).ToString("dd/MM/yyyy") : string.Empty;
                AdmissionVM.ClaimDocumentsEmailedDate = TblPatientAdmission[0].ClaimDocumentsEmailedDate != null ? Convert.ToDateTime(TblPatientAdmission[0].ClaimDocumentsEmailedDate).ToString("dd/MM/yyyy") : string.Empty;
                AdmissionVM.PaymentAdviceReceviedDate = TblPatientAdmission[0].PaymentAdviceReceviedDate != null ? Convert.ToDateTime(TblPatientAdmission[0].PaymentAdviceReceviedDate).ToString("dd/MM/yyyy") : string.Empty;
                AdmissionVM.PaymentAdviceEmailedDate = TblPatientAdmission[0].PaymentAdviceEmailedDate != null ? Convert.ToDateTime(TblPatientAdmission[0].PaymentAdviceEmailedDate).ToString("dd/MM/yyyy") : string.Empty;
                AdmissionVM.TotalAmountPaid = TblPatientAdmission[0].TotalAmountPaid < 0 ? 0 : (decimal)TblPatientAdmission[0].TotalAmountPaid;
                AdmissionVM.CurrencyType = TblPatientAdmission[0].CurrencyType < 0 ? 0 : (int)TblPatientAdmission[0].CurrencyType;
                AdmissionVM.OriginalDocumentscourieredDate = TblPatientAdmission[0].OriginalDocumentscourieredDate != null ? Convert.ToDateTime(TblPatientAdmission[0].OriginalDocumentscourieredDate).ToString("dd/MM/yyyy") : string.Empty;
                AdmissionVM.AirwayBillNo = string.IsNullOrEmpty(TblPatientAdmission[0].AirwayBillNo) ? "" : TblPatientAdmission[0].AirwayBillNo;
                AdmissionVM.BUID = TblPatientAdmission[0].BUID < 0 || TblPatientAdmission[0].BUID == null ? 0 : (int)TblPatientAdmission[0].BUID;
                AdmissionVM.CreateBy = TblPatientAdmission[0].CreateBy < 0 || TblPatientAdmission[0].CreateBy == null ? 0 : (int)TblPatientAdmission[0].CreateBy;
                AdmissionVM.CreatedDate = string.Empty;
                AdmissionVM.ModifiedBy = TblPatientAdmission[0].ModifiedBy < 0 || TblPatientAdmission[0].ModifiedBy == null ? 0 : (int)TblPatientAdmission[0].ModifiedBy;
                AdmissionVM.ModifiedDate = string.Empty;
                AdmissionVM.MembershipID = string.IsNullOrEmpty(TblPatientAdmission[0].MembershipID) ? "" : TblPatientAdmission[0].MembershipID;
                AdmissionVM.CurrancyCode = string.IsNullOrEmpty(TblPatientAdmission[0].CurrancyCode) ? "" : TblPatientAdmission[0].CurrancyCode;



                return AdmissionVM;

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        #endregion



        public List<ClientVM> GetClientByMembershipID(string  MembershipID)
        {
          

          var clientList = new List<ClientVM>();
            var clientFamilyData = unitOfWork.TblFamilyMemberRepository.Get(x => x.MembershipID == MembershipID).ToList();

            foreach (var pa in clientFamilyData)

            {
                var admissionVM = new ClientVM();
                admissionVM.ClientName = pa.MemberName;
                clientList.Add(admissionVM);
                return clientList;
            }
            var clientGroupFamilyData = unitOfWork.TblGrpFamilyDetailRepository.Get(x => x.MembershipID == MembershipID).ToList();
            foreach (var paGF in clientGroupFamilyData)

            {
                var admissionGVM = new ClientVM();
                admissionGVM.ClientName = paGF.MemberName;

                clientList.Add(admissionGVM);
                return clientList;
            }

            var clientData = unitOfWork.TblClientRequestHeaderRepository.Get(x => x.MembershipID == MembershipID).ToList();

            foreach (var paC in clientData)

            {
                var clientDataNew = unitOfWork.TblClientRepository.GetByID(paC.ClientID);
                var admissionC = new ClientVM();
                admissionC.ClientName = clientDataNew.ClientName;

                clientList.Add(admissionC);
                return clientList;

            }




            return clientList;




        }



    }
}
