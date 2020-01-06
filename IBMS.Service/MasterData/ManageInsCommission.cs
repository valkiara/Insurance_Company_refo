using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
   
    public class ManageInsCommission
    {
        private UnitOfWork unitOfWork;
        public ManageInsCommission()
        {
            unitOfWork = new UnitOfWork();
        }


        #region Commision Structure Header
        public bool SaveInsCommisionStructureHeader(InsCommissionStructureHeaderVM commisionStructureHeaderVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblInsComCommisionStructureHeader commisionStructureHeader = new tblInsComCommisionStructureHeader();
                    commisionStructureHeader.ComStructName = commisionStructureHeaderVM.CommisionStructureName;
                    commisionStructureHeader.ExtraFloat1 = commisionStructureHeaderVM.BusinessUnitID;
                    commisionStructureHeader.InsCompanyID = commisionStructureHeaderVM.InsuranceCompanyID;
                    commisionStructureHeader.InsClassID = commisionStructureHeaderVM.InsuranceClassID;
                    commisionStructureHeader.InsSubClassID = commisionStructureHeaderVM.InsuranceSubClassID;
                    commisionStructureHeader.CreatedDate = DateTime.Now;
                    commisionStructureHeader.CreatedBy = commisionStructureHeaderVM.CreatedBy;
                    unitOfWork.TblInsCommisionStructureHeaderRepository.Insert(commisionStructureHeader);
                    unitOfWork.Save();

                    int savedIndex = commisionStructureHeader.ComHeaderID;

                    foreach(var chargeType in commisionStructureHeaderVM.ChargeTypeList)
                    {
                        tblComCommisionStructureLine inscommissionLine = new tblComCommisionStructureLine();
                        inscommissionLine.ComStructID = savedIndex;
                        inscommissionLine.ChargeTypeID = chargeType.ChargeTypeID;
                        inscommissionLine.Percentage = chargeType.Amount;
                        inscommissionLine.CreatedDate = DateTime.Now;
                        inscommissionLine.CreatedBy = commisionStructureHeaderVM.CreatedBy;
                        unitOfWork.TblInsCommisionStructureLineRepository.Insert(inscommissionLine);
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

        public bool UpdateInsCommisionStructureHeader(InsCommissionStructureHeaderVM commisionStructureHeaderVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblInsComCommisionStructureHeader commisionStructureHeader = new tblInsComCommisionStructureHeader();
                    commisionStructureHeader.ComStructName = commisionStructureHeaderVM.CommisionStructureName;
                    commisionStructureHeader.ExtraFloat1 = commisionStructureHeaderVM.BusinessUnitID;
                    commisionStructureHeader.InsCompanyID = commisionStructureHeaderVM.InsuranceCompanyID;
                    commisionStructureHeader.InsClassID = commisionStructureHeaderVM.InsuranceClassID;
                    commisionStructureHeader.InsSubClassID = commisionStructureHeaderVM.InsuranceSubClassID;
                    commisionStructureHeader.CreatedDate = DateTime.Now;
                    commisionStructureHeader.CreatedBy = commisionStructureHeaderVM.CreatedBy;
                    unitOfWork.TblInsCommisionStructureHeaderRepository.Insert(commisionStructureHeader);
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

        public bool DeleteInsCommisionStructureHeader(int commisionStructureID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblInsComCommisionStructureHeader commisionStructureHeader = unitOfWork.TblInsCommisionStructureHeaderRepository.GetByID(commisionStructureID);
                    unitOfWork.TblInsCommisionStructureHeaderRepository.Delete(commisionStructureHeader);
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

        public List<InsCommissionStructureHeaderVM> GetAllInsCommisionStructureHeaders(int businessUnitID)
        {
            try
            {
                var commisionStructureHeaderData = unitOfWork.TblInsCommisionStructureHeaderRepository.Get(x=>x.ExtraFloat1==(double)businessUnitID).ToList();
                List<InsCommissionStructureHeaderVM> commisionStructureHeaderList = new List<InsCommissionStructureHeaderVM>();
                List<ChargeTypeVM> chargeTpeListVm = new List<ChargeTypeVM>();

                foreach (var commisionStructureHeader in commisionStructureHeaderList)
                {
                    InsCommissionStructureHeaderVM commisionStructureHeaderVM = new InsCommissionStructureHeaderVM();
                    commisionStructureHeaderVM.CommisionStructureID = commisionStructureHeader.CommisionStructureLineID;
                    commisionStructureHeaderVM.CommisionStructureName = commisionStructureHeader.CommisionStructureName;
                    commisionStructureHeaderVM.BusinessUnitID = commisionStructureHeader.BusinessUnitID >0 ? Convert.ToInt32(commisionStructureHeader.BusinessUnitID) : 0;
                    commisionStructureHeaderVM.InsuranceCompanyID = commisionStructureHeader.InsuranceCompanyID > 0? commisionStructureHeader.InsuranceCompanyID:0;

                    tblInsCompany insuranceCompany = unitOfWork.TblInsCompanyRepository.GetByID(commisionStructureHeaderVM.InsuranceCompanyID);
                    commisionStructureHeaderVM.InsuranceCompanyName =string.IsNullOrEmpty(insuranceCompany.InsCompany)? "": insuranceCompany.InsCompany;


                    commisionStructureHeaderVM.InsuranceClassID = commisionStructureHeader.InsuranceClassID > 0 ? commisionStructureHeader.InsuranceClassID : 0;
                    tblInsClass insuranceClass = unitOfWork.TblInsClassRepository.GetByID(commisionStructureHeaderVM.InsuranceClassID);
                    commisionStructureHeaderVM.InsuranceClassName= string.IsNullOrEmpty(insuranceClass.Code) ? "" : insuranceClass.Code;

                    commisionStructureHeaderVM.InsuranceSubClassID = commisionStructureHeader.InsuranceSubClassID > 0 ? commisionStructureHeader.InsuranceSubClassID : 0;
                    tblInsSubClass insuranceSubClass = unitOfWork.TblInsSubClassRepository.GetByID(commisionStructureHeaderVM.InsuranceSubClassID);
                    commisionStructureHeaderVM.InsuranceSubClassName= string.IsNullOrEmpty(insuranceSubClass.Description) ? "" : insuranceSubClass.Description;


                    commisionStructureHeaderVM.CreatedDate = commisionStructureHeader.CreatedDate != null ? commisionStructureHeader.CreatedDate.ToString() : string.Empty;
                    commisionStructureHeaderVM.ModifiedDate = commisionStructureHeader.ModifiedDate != null ? commisionStructureHeader.ModifiedDate.ToString() : string.Empty;
                    commisionStructureHeaderVM.CreatedBy = commisionStructureHeader.CreatedBy >0 ? commisionStructureHeader.CreatedBy : 0;
                    commisionStructureHeaderVM.ModifiedBy = commisionStructureHeader.ModifiedBy >0 ? commisionStructureHeader.ModifiedBy : 0;

                    var chargeTypeList = unitOfWork.TblInsCommisionStructureLineRepository.Get(x => x.ComStructID == commisionStructureHeader.CommisionStructureLineID).ToList();
                    ChargeTypeVM chargeType = new ChargeTypeVM();

                    foreach (var chargeTpeListLines in chargeTypeList)
                    {
                        chargeType.ChargeTypeID = (int)chargeTpeListLines.ChargeTypeID >0 ? (int)chargeTpeListLines.ChargeTypeID:0;
                        chargeType.Percentage = Convert.ToDouble(chargeTpeListLines.Percentage) > 0 ? Convert.ToDouble(chargeTpeListLines.Percentage) : 0.0;
                        chargeTpeListVm.Add(chargeType);
                    }
                    commisionStructureHeaderVM.ChargeTypeList = chargeTpeListVm;
                    commisionStructureHeaderList.Add(commisionStructureHeaderVM);
                }

                return commisionStructureHeaderList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CommisionStructureHeaderVM> GetAllCommisionStructureHeadersByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var commisionStructureHeaderData = unitOfWork.TblCommisionStructureHeaderRepository.Get(x => x.BUID == businessUnitID).ToList();

                List<CommisionStructureHeaderVM> commisionStructureHeaderList = new List<CommisionStructureHeaderVM>();

                foreach (var commisionStructureHeader in commisionStructureHeaderData)
                {
                    CommisionStructureHeaderVM commisionStructureHeaderVM = new CommisionStructureHeaderVM();
                    commisionStructureHeaderVM.CommisionStructureID = commisionStructureHeader.ComStructID;
                    commisionStructureHeaderVM.CommisionStructureName = commisionStructureHeader.ComStructName;
                    commisionStructureHeaderVM.BusinessUnitID = commisionStructureHeader.BUID != null ? Convert.ToInt32(commisionStructureHeader.BUID) : 0;

                    if (commisionStructureHeaderVM.BusinessUnitID > 0)
                    {
                        commisionStructureHeaderVM.BusinessUnitName = commisionStructureHeader.tblBussinessUnit.BussinessUnit;
                    }

                    commisionStructureHeaderVM.PartnerID = commisionStructureHeader.PartnerID != null ? Convert.ToInt32(commisionStructureHeader.PartnerID) : 0;

                    if (commisionStructureHeaderVM.PartnerID > 0)
                    {
                        commisionStructureHeaderVM.PartnerName = commisionStructureHeader.tblPartner.PartnerName;
                    }

                    commisionStructureHeaderVM.InsuranceCompanyID = commisionStructureHeader.InsCompanyID != null ? Convert.ToInt32(commisionStructureHeader.InsCompanyID) : 0;

                    if (commisionStructureHeaderVM.InsuranceCompanyID > 0)
                    {
                        commisionStructureHeaderVM.InsuranceCompanyName = commisionStructureHeader.tblInsCompany.InsCompany;
                    }

                    commisionStructureHeaderVM.InsuranceClassID = commisionStructureHeader.InsClassID != null ? Convert.ToInt32(commisionStructureHeader.InsClassID) : 0;

                    if (commisionStructureHeaderVM.InsuranceClassID > 0)
                    {
                        commisionStructureHeaderVM.InsuranceClassName = commisionStructureHeader.tblInsClass.Code;
                    }

                    commisionStructureHeaderVM.InsuranceSubClassID = commisionStructureHeader.InsSubClassID != null ? Convert.ToInt32(commisionStructureHeader.InsSubClassID) : 0;

                    if (commisionStructureHeaderVM.InsuranceSubClassID > 0)
                    {
                        commisionStructureHeaderVM.InsuranceSubClassName = commisionStructureHeader.tblInsSubClass.Description;
                    }

                    commisionStructureHeaderVM.CreatedDate = commisionStructureHeader.CreatedDate != null ? commisionStructureHeader.CreatedDate.ToString() : string.Empty;
                    commisionStructureHeaderVM.ModifiedDate = commisionStructureHeader.ModifiedDate != null ? commisionStructureHeader.ModifiedDate.ToString() : string.Empty;
                    commisionStructureHeaderVM.CreatedBy = commisionStructureHeader.CreatedBy != null ? Convert.ToInt32(commisionStructureHeader.CreatedBy) : 0;
                    commisionStructureHeaderVM.ModifiedBy = commisionStructureHeader.ModifiedBy != null ? Convert.ToInt32(commisionStructureHeader.ModifiedBy) : 0;

                    commisionStructureHeaderList.Add(commisionStructureHeaderVM);
                }

                return commisionStructureHeaderList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public CommisionStructureHeaderVM GetCommisionStructureHeaderByID(int commisionStructureID)
        {
            try
            {
                tblCommisionStructureHeader commisionStructureHeaderData = unitOfWork.TblCommisionStructureHeaderRepository.GetByID(commisionStructureID);

                CommisionStructureHeaderVM commisionStructureHeaderVM = new CommisionStructureHeaderVM();
                commisionStructureHeaderVM.CommisionStructureID = commisionStructureHeaderData.ComStructID;
                commisionStructureHeaderVM.CommisionStructureName = commisionStructureHeaderData.ComStructName;
                commisionStructureHeaderVM.BusinessUnitID = commisionStructureHeaderData.BUID != null ? Convert.ToInt32(commisionStructureHeaderData.BUID) : 0;

                if (commisionStructureHeaderVM.BusinessUnitID > 0)
                {
                    commisionStructureHeaderVM.BusinessUnitName = commisionStructureHeaderData.tblBussinessUnit.BussinessUnit;
                }

                commisionStructureHeaderVM.PartnerID = commisionStructureHeaderData.PartnerID != null ? Convert.ToInt32(commisionStructureHeaderData.PartnerID) : 0;

                if (commisionStructureHeaderVM.PartnerID > 0)
                {
                    commisionStructureHeaderVM.PartnerName = commisionStructureHeaderData.tblPartner.PartnerName;
                }

                commisionStructureHeaderVM.InsuranceCompanyID = commisionStructureHeaderData.InsCompanyID != null ? Convert.ToInt32(commisionStructureHeaderData.InsCompanyID) : 0;

                if (commisionStructureHeaderVM.InsuranceCompanyID > 0)
                {
                    commisionStructureHeaderVM.InsuranceCompanyName = commisionStructureHeaderData.tblInsCompany.InsCompany;
                }

                commisionStructureHeaderVM.InsuranceClassID = commisionStructureHeaderData.InsClassID != null ? Convert.ToInt32(commisionStructureHeaderData.InsClassID) : 0;

                if (commisionStructureHeaderVM.InsuranceClassID > 0)
                {
                    commisionStructureHeaderVM.InsuranceClassName = commisionStructureHeaderData.tblInsClass.Code;
                }

                commisionStructureHeaderVM.InsuranceSubClassID = commisionStructureHeaderData.InsSubClassID != null ? Convert.ToInt32(commisionStructureHeaderData.InsSubClassID) : 0;

                if (commisionStructureHeaderVM.InsuranceSubClassID > 0)
                {
                    commisionStructureHeaderVM.InsuranceSubClassName = commisionStructureHeaderData.tblInsSubClass.Description;
                }

                commisionStructureHeaderVM.CreatedDate = commisionStructureHeaderData.CreatedDate != null ? commisionStructureHeaderData.CreatedDate.ToString() : string.Empty;
                commisionStructureHeaderVM.ModifiedDate = commisionStructureHeaderData.ModifiedDate != null ? commisionStructureHeaderData.ModifiedDate.ToString() : string.Empty;
                commisionStructureHeaderVM.CreatedBy = commisionStructureHeaderData.CreatedBy != null ? Convert.ToInt32(commisionStructureHeaderData.CreatedBy) : 0;
                commisionStructureHeaderVM.ModifiedBy = commisionStructureHeaderData.ModifiedBy != null ? Convert.ToInt32(commisionStructureHeaderData.ModifiedBy) : 0;

                return commisionStructureHeaderVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsInsCommisionStructureHeaderAvailable(int? commisionStructureID, string commisionStructureName)
        {
            try
            {
                if (commisionStructureID != null && unitOfWork.TblInsCommisionStructureHeaderRepository.Get().Any(x => x.ComStructName.ToLower() == commisionStructureName.ToLower() && x.ComHeaderID != commisionStructureID))
                {
                    return true;
                }
                else if (commisionStructureID == null && unitOfWork.TblInsCommisionStructureHeaderRepository.Get().Any(x => x.ComStructName.ToLower() == commisionStructureName.ToLower()))
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

        #region Commision Structure Line
        public bool SaveInsCommisionStructureLine(InsCommissionStructureLineVM commisionStructureLineVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblComCommisionStructureLine commisionStructureLine = new tblComCommisionStructureLine();
                    commisionStructureLine.ComStructID = commisionStructureLineVM.CommisionStructureID;
                    commisionStructureLine.ChargeTypeID = commisionStructureLineVM.RateCategoryID;
                    commisionStructureLine.Percentage = commisionStructureLineVM.RateValue;

                   
                    commisionStructureLine.CreatedDate = DateTime.Now;
                    //commisionStructureLine.CreatedBy = commisionStructureLineVM.CreatedBy;
                    //unitOfWork.TblCommisionStructureLineRepository.Insert(commisionStructureLine);
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

        public bool UpdateCommisionStructureLine(CommisionStructureLineVM commisionStructureLineVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCommisionStructureLine commisionStructureLine = unitOfWork.TblCommisionStructureLineRepository.GetByID(commisionStructureLineVM.CommisionStructureLineID);
                    commisionStructureLine.ComStructID = commisionStructureLineVM.CommisionStructureID;
                    commisionStructureLine.RateCategoryID = commisionStructureLineVM.RateCategoryID;
                    commisionStructureLine.IsAgeConsider = commisionStructureLineVM.IsAgeConsider;

                    if (commisionStructureLine.IsAgeConsider)
                    {
                        commisionStructureLine.AgeFrom = commisionStructureLineVM.AgeFrom;
                        commisionStructureLine.AgeTo = commisionStructureLineVM.AgeTo;
                    }
                    else
                    {
                        commisionStructureLine.AgeFrom = null;
                        commisionStructureLine.AgeTo = null;
                    }

                    commisionStructureLine.isFixed = commisionStructureLineVM.IsFixed;
                    commisionStructureLine.RateValue = commisionStructureLineVM.RateValue;
                    commisionStructureLine.ModifiedDate = DateTime.Now;
                    commisionStructureLine.ModifiedBy = commisionStructureLineVM.ModifiedBy;
                    unitOfWork.TblCommisionStructureLineRepository.Update(commisionStructureLine);
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

        public bool DeleteCommisionStructureLine(int commisionStructureLineID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCommisionStructureLine commisionStructureLine = unitOfWork.TblCommisionStructureLineRepository.GetByID(commisionStructureLineID);
                    unitOfWork.TblCommisionStructureLineRepository.Delete(commisionStructureLine);
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

        public List<CommisionStructureLineVM> GetAllCommisionStructureLines()
        {
            try
            {
                var commisionStructureLineData = unitOfWork.TblCommisionStructureLineRepository.Get().ToList();

                List<CommisionStructureLineVM> commisionStructureLineList = new List<CommisionStructureLineVM>();

                foreach (var commisionStructureLine in commisionStructureLineData)
                {
                    CommisionStructureLineVM commisionStructureLineVM = new CommisionStructureLineVM();
                    commisionStructureLineVM.CommisionStructureLineID = commisionStructureLine.ComStructLineID;
                    commisionStructureLineVM.CommisionStructureID = commisionStructureLine.ComStructID != null ? Convert.ToInt32(commisionStructureLine.ComStructID) : 0;

                    if (commisionStructureLineVM.CommisionStructureID > 0)
                    {
                        commisionStructureLineVM.CommisionStructureName = commisionStructureLine.tblCommisionStructureHeader.ComStructName;
                    }

                    commisionStructureLineVM.RateCategoryID = commisionStructureLine.RateCategoryID != null ? Convert.ToInt32(commisionStructureLine.RateCategoryID) : 0;

                    if (commisionStructureLineVM.RateCategoryID > 0)
                    {
                        commisionStructureLineVM.RateCategoryName = commisionStructureLine.tblRateCategory.RateCategory;
                    }

                    commisionStructureLineVM.IsAgeConsider = commisionStructureLine.IsAgeConsider;
                    commisionStructureLineVM.AgeFrom = commisionStructureLine.AgeFrom != null ? Convert.ToInt32(commisionStructureLine.AgeFrom) : 0;
                    commisionStructureLineVM.AgeTo = commisionStructureLine.AgeTo != null ? Convert.ToInt32(commisionStructureLine.AgeTo) : 0;
                    commisionStructureLineVM.IsFixed = commisionStructureLine.isFixed;
                    commisionStructureLineVM.RateValue = commisionStructureLine.RateValue != null ? Convert.ToDouble(commisionStructureLine.RateValue) : 0;
                    commisionStructureLineVM.CreatedDate = commisionStructureLine.CreatedDate != null ? commisionStructureLine.CreatedDate.ToString() : string.Empty;
                    commisionStructureLineVM.ModifiedDate = commisionStructureLine.ModifiedDate != null ? commisionStructureLine.ModifiedDate.ToString() : string.Empty;
                    commisionStructureLineVM.CreatedBy = commisionStructureLine.CreatedBy != null ? Convert.ToInt32(commisionStructureLine.CreatedBy) : 0;
                    commisionStructureLineVM.ModifiedBy = commisionStructureLine.ModifiedBy != null ? Convert.ToInt32(commisionStructureLine.ModifiedBy) : 0;

                    commisionStructureLineList.Add(commisionStructureLineVM);
                }

                return commisionStructureLineList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CommisionStructureLineVM> GetAllCommisionStructureLinesByComStructHeaderID(int comStructHeaderID)
        {
            try
            {
                var commisionStructureLineData = unitOfWork.TblCommisionStructureLineRepository.Get(x => x.ComStructID == comStructHeaderID).ToList();

                List<CommisionStructureLineVM> commisionStructureLineList = new List<CommisionStructureLineVM>();

                foreach (var commisionStructureLine in commisionStructureLineData)
                {
                    CommisionStructureLineVM commisionStructureLineVM = new CommisionStructureLineVM();
                    commisionStructureLineVM.CommisionStructureLineID = commisionStructureLine.ComStructLineID;
                    commisionStructureLineVM.CommisionStructureID = commisionStructureLine.ComStructID != null ? Convert.ToInt32(commisionStructureLine.ComStructID) : 0;

                    if (commisionStructureLineVM.CommisionStructureID > 0)
                    {
                        commisionStructureLineVM.CommisionStructureName = commisionStructureLine.tblCommisionStructureHeader.ComStructName;
                    }

                    commisionStructureLineVM.RateCategoryID = commisionStructureLine.RateCategoryID != null ? Convert.ToInt32(commisionStructureLine.RateCategoryID) : 0;

                    if (commisionStructureLineVM.RateCategoryID > 0)
                    {
                        commisionStructureLineVM.RateCategoryName = commisionStructureLine.tblRateCategory.RateCategory;
                    }

                    commisionStructureLineVM.IsAgeConsider = commisionStructureLine.IsAgeConsider;
                    commisionStructureLineVM.AgeFrom = commisionStructureLine.AgeFrom != null ? Convert.ToInt32(commisionStructureLine.AgeFrom) : 0;
                    commisionStructureLineVM.AgeTo = commisionStructureLine.AgeTo != null ? Convert.ToInt32(commisionStructureLine.AgeTo) : 0;
                    commisionStructureLineVM.IsFixed = commisionStructureLine.isFixed;
                    commisionStructureLineVM.RateValue = commisionStructureLine.RateValue != null ? Convert.ToDouble(commisionStructureLine.RateValue) : 0;
                    commisionStructureLineVM.CreatedDate = commisionStructureLine.CreatedDate != null ? commisionStructureLine.CreatedDate.ToString() : string.Empty;
                    commisionStructureLineVM.ModifiedDate = commisionStructureLine.ModifiedDate != null ? commisionStructureLine.ModifiedDate.ToString() : string.Empty;
                    commisionStructureLineVM.CreatedBy = commisionStructureLine.CreatedBy != null ? Convert.ToInt32(commisionStructureLine.CreatedBy) : 0;
                    commisionStructureLineVM.ModifiedBy = commisionStructureLine.ModifiedBy != null ? Convert.ToInt32(commisionStructureLine.ModifiedBy) : 0;

                    commisionStructureLineList.Add(commisionStructureLineVM);
                }

                return commisionStructureLineList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CommisionStructureLineVM> GetAllCommisionStructureLinesByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var commisionStructureLineData = unitOfWork.TblCommisionStructureLineRepository.Get(x => x.tblCommisionStructureHeader.BUID == businessUnitID).ToList();

                List<CommisionStructureLineVM> commisionStructureLineList = new List<CommisionStructureLineVM>();

                foreach (var commisionStructureLine in commisionStructureLineData)
                {
                    CommisionStructureLineVM commisionStructureLineVM = new CommisionStructureLineVM();
                    commisionStructureLineVM.CommisionStructureLineID = commisionStructureLine.ComStructLineID;
                    commisionStructureLineVM.CommisionStructureID = commisionStructureLine.ComStructID != null ? Convert.ToInt32(commisionStructureLine.ComStructID) : 0;

                    if (commisionStructureLineVM.CommisionStructureID > 0)
                    {
                        commisionStructureLineVM.CommisionStructureName = commisionStructureLine.tblCommisionStructureHeader.ComStructName;
                    }

                    commisionStructureLineVM.RateCategoryID = commisionStructureLine.RateCategoryID != null ? Convert.ToInt32(commisionStructureLine.RateCategoryID) : 0;

                    if (commisionStructureLineVM.RateCategoryID > 0)
                    {
                        commisionStructureLineVM.RateCategoryName = commisionStructureLine.tblRateCategory.RateCategory;
                    }

                    commisionStructureLineVM.IsAgeConsider = commisionStructureLine.IsAgeConsider;
                    commisionStructureLineVM.AgeFrom = commisionStructureLine.AgeFrom != null ? Convert.ToInt32(commisionStructureLine.AgeFrom) : 0;
                    commisionStructureLineVM.AgeTo = commisionStructureLine.AgeTo != null ? Convert.ToInt32(commisionStructureLine.AgeTo) : 0;
                    commisionStructureLineVM.IsFixed = commisionStructureLine.isFixed;
                    commisionStructureLineVM.RateValue = commisionStructureLine.RateValue != null ? Convert.ToDouble(commisionStructureLine.RateValue) : 0;
                    commisionStructureLineVM.CreatedDate = commisionStructureLine.CreatedDate != null ? commisionStructureLine.CreatedDate.ToString() : string.Empty;
                    commisionStructureLineVM.ModifiedDate = commisionStructureLine.ModifiedDate != null ? commisionStructureLine.ModifiedDate.ToString() : string.Empty;
                    commisionStructureLineVM.CreatedBy = commisionStructureLine.CreatedBy != null ? Convert.ToInt32(commisionStructureLine.CreatedBy) : 0;
                    commisionStructureLineVM.ModifiedBy = commisionStructureLine.ModifiedBy != null ? Convert.ToInt32(commisionStructureLine.ModifiedBy) : 0;

                    commisionStructureLineList.Add(commisionStructureLineVM);
                }

                return commisionStructureLineList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public CommisionStructureLineVM GetCommisionStructureLineByID(int commisionStructureLineID)
        {
            try
            {
                tblCommisionStructureLine commisionStructureLineData = unitOfWork.TblCommisionStructureLineRepository.GetByID(commisionStructureLineID);

                CommisionStructureLineVM commisionStructureLineVM = new CommisionStructureLineVM();
                commisionStructureLineVM.CommisionStructureLineID = commisionStructureLineData.ComStructLineID;
                commisionStructureLineVM.CommisionStructureID = commisionStructureLineData.ComStructID != null ? Convert.ToInt32(commisionStructureLineData.ComStructID) : 0;

                if (commisionStructureLineVM.CommisionStructureID > 0)
                {
                    commisionStructureLineVM.CommisionStructureName = commisionStructureLineData.tblCommisionStructureHeader.ComStructName;
                }

                commisionStructureLineVM.RateCategoryID = commisionStructureLineData.RateCategoryID != null ? Convert.ToInt32(commisionStructureLineData.RateCategoryID) : 0;

                if (commisionStructureLineVM.RateCategoryID > 0)
                {
                    commisionStructureLineVM.RateCategoryName = commisionStructureLineData.tblRateCategory.RateCategory;
                }

                commisionStructureLineVM.IsAgeConsider = commisionStructureLineData.IsAgeConsider;
                commisionStructureLineVM.AgeFrom = commisionStructureLineData.AgeFrom != null ? Convert.ToInt32(commisionStructureLineData.AgeFrom) : 0;
                commisionStructureLineVM.AgeTo = commisionStructureLineData.AgeTo != null ? Convert.ToInt32(commisionStructureLineData.AgeTo) : 0;
                commisionStructureLineVM.IsFixed = commisionStructureLineData.isFixed;
                commisionStructureLineVM.RateValue = commisionStructureLineData.RateValue != null ? Convert.ToDouble(commisionStructureLineData.RateValue) : 0;
                commisionStructureLineVM.CreatedDate = commisionStructureLineData.CreatedDate != null ? commisionStructureLineData.CreatedDate.ToString() : string.Empty;
                commisionStructureLineVM.ModifiedDate = commisionStructureLineData.ModifiedDate != null ? commisionStructureLineData.ModifiedDate.ToString() : string.Empty;
                commisionStructureLineVM.CreatedBy = commisionStructureLineData.CreatedBy != null ? Convert.ToInt32(commisionStructureLineData.CreatedBy) : 0;
                commisionStructureLineVM.ModifiedBy = commisionStructureLineData.ModifiedBy != null ? Convert.ToInt32(commisionStructureLineData.ModifiedBy) : 0;

                return commisionStructureLineVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }

}
