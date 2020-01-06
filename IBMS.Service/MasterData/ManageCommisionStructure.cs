using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManageCommisionStructure
    {
        private UnitOfWork unitOfWork;
        public ManageCommisionStructure()
        {
            unitOfWork = new UnitOfWork();
        }

        #region Commision Type
        public bool SaveCommisionType(CommisionTypeVM commisionTypeVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCommisionType commisionType = new tblCommisionType();
                    commisionType.CommisionType = commisionTypeVM.CommisionTypeName;
                    commisionType.CreatedDate = DateTime.Now;
                    commisionType.CreatedBy = commisionTypeVM.CreatedBy;
                    unitOfWork.TblCommisionTypeRepository.Insert(commisionType);
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

        public bool UpdateCommisionType(CommisionTypeVM commisionTypeVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCommisionType commisionType = unitOfWork.TblCommisionTypeRepository.GetByID(commisionTypeVM.CommisionTypeID);
                    commisionType.CommisionType = commisionTypeVM.CommisionTypeName;
                    commisionType.ModifiedDate = DateTime.Now;
                    commisionType.ModifiedBy = commisionTypeVM.ModifiedBy;
                    unitOfWork.TblCommisionTypeRepository.Update(commisionType);
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

        public bool DeleteCommisionType(int commisionTypeID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCommisionType commisionType = unitOfWork.TblCommisionTypeRepository.GetByID(commisionTypeID);
                    unitOfWork.TblCommisionTypeRepository.Delete(commisionType);
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

        public List<CommisionTypeVM> GetAllCommisionTypes()
        {
            try
            {
                var commisionTypeData = unitOfWork.TblCommisionTypeRepository.Get().ToList();

                List<CommisionTypeVM> commisionTypeList = new List<CommisionTypeVM>();

                foreach (var commisionType in commisionTypeData)
                {
                    CommisionTypeVM commisionTypeVM = new CommisionTypeVM();
                    commisionTypeVM.CommisionTypeID = commisionType.CommisionTypeID;
                    commisionTypeVM.CommisionTypeName = commisionType.CommisionType;
                    commisionTypeVM.CreatedDate = commisionType.CreatedDate != null ? commisionType.CreatedDate.ToString() : string.Empty;
                    commisionTypeVM.ModifiedDate = commisionType.ModifiedDate != null ? commisionType.ModifiedDate.ToString() : string.Empty;
                    commisionTypeVM.CreatedBy = commisionType.CreatedBy != null ? Convert.ToInt32(commisionType.CreatedBy) : 0;
                    commisionTypeVM.ModifiedBy = commisionType.ModifiedBy != null ? Convert.ToInt32(commisionType.ModifiedBy) : 0;

                    commisionTypeList.Add(commisionTypeVM);
                }

                return commisionTypeList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public CommisionTypeVM GetCommisionTypeByID(int commisionTypeID)
        {
            try
            {
                tblCommisionType commisionTypeData = unitOfWork.TblCommisionTypeRepository.GetByID(commisionTypeID);

                CommisionTypeVM commisionTypeVM = new CommisionTypeVM();
                commisionTypeVM.CommisionTypeID = commisionTypeData.CommisionTypeID;
                commisionTypeVM.CommisionTypeName = commisionTypeData.CommisionType;
                commisionTypeVM.CreatedDate = commisionTypeData.CreatedDate != null ? commisionTypeData.CreatedDate.ToString() : string.Empty;
                commisionTypeVM.ModifiedDate = commisionTypeData.ModifiedDate != null ? commisionTypeData.ModifiedDate.ToString() : string.Empty;
                commisionTypeVM.CreatedBy = commisionTypeData.CreatedBy != null ? Convert.ToInt32(commisionTypeData.CreatedBy) : 0;
                commisionTypeVM.ModifiedBy = commisionTypeData.ModifiedBy != null ? Convert.ToInt32(commisionTypeData.ModifiedBy) : 0;

                return commisionTypeVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsCommisionTypeAvailable(int? commisionTypeID, string commisionTypeName)
        {
            try
            {
                if (commisionTypeID != null && unitOfWork.TblCommisionTypeRepository.Get().Any(x => x.CommisionType.ToLower() == commisionTypeName.ToLower() && x.CommisionTypeID != commisionTypeID))
                {
                    return true;
                }
                else if (commisionTypeID == null && unitOfWork.TblCommisionTypeRepository.Get().Any(x => x.CommisionType.ToLower() == commisionTypeName.ToLower()))
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

        #region Commision Structure Header
        public bool SaveCommisionStructureHeader(CommisionStructureHeaderVM commisionStructureHeaderVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCommisionStructureHeader commisionStructureHeader = new tblCommisionStructureHeader();
                    commisionStructureHeader.ComStructName = commisionStructureHeaderVM.CommisionStructureName;
                    commisionStructureHeader.BUID = commisionStructureHeaderVM.BusinessUnitID;
                    commisionStructureHeader.PartnerID = commisionStructureHeaderVM.PartnerID;
                    commisionStructureHeader.InsCompanyID = commisionStructureHeaderVM.InsuranceCompanyID;
                    commisionStructureHeader.InsClassID = commisionStructureHeaderVM.InsuranceClassID;
                    commisionStructureHeader.InsSubClassID = commisionStructureHeaderVM.InsuranceSubClassID;
                    commisionStructureHeader.CreatedDate = DateTime.Now;
                    commisionStructureHeader.CreatedBy = commisionStructureHeaderVM.CreatedBy;
                    unitOfWork.TblCommisionStructureHeaderRepository.Insert(commisionStructureHeader);
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

        public bool UpdateCommisionStructureHeader(CommisionStructureHeaderVM commisionStructureHeaderVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCommisionStructureHeader commisionStructureHeader = unitOfWork.TblCommisionStructureHeaderRepository.GetByID(commisionStructureHeaderVM.CommisionStructureID);
                    commisionStructureHeader.ComStructName = commisionStructureHeaderVM.CommisionStructureName;
                    commisionStructureHeader.BUID = commisionStructureHeaderVM.BusinessUnitID;
                    commisionStructureHeader.PartnerID = commisionStructureHeaderVM.PartnerID;
                    commisionStructureHeader.InsCompanyID = commisionStructureHeaderVM.InsuranceCompanyID;
                    commisionStructureHeader.InsClassID = commisionStructureHeaderVM.InsuranceClassID;
                    commisionStructureHeader.InsSubClassID = commisionStructureHeaderVM.InsuranceSubClassID;
                    commisionStructureHeader.ModifiedDate = DateTime.Now;
                    commisionStructureHeader.ModifiedBy = commisionStructureHeaderVM.ModifiedBy;
                    unitOfWork.TblCommisionStructureHeaderRepository.Update(commisionStructureHeader);
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

        public bool DeleteCommisionStructureHeader(int commisionStructureID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCommisionStructureHeader commisionStructureHeader = unitOfWork.TblCommisionStructureHeaderRepository.GetByID(commisionStructureID);
                    unitOfWork.TblCommisionStructureHeaderRepository.Delete(commisionStructureHeader);
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

        public List<CommisionStructureHeaderVM> GetAllCommisionStructureHeaders()
        {
            try
            {
                var commisionStructureHeaderData = unitOfWork.TblCommisionStructureHeaderRepository.Get().ToList();
                

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

        public bool IsCommisionStructureHeaderAvailable(int? commisionStructureID, string commisionStructureName)
        {
            try
            {
                if (commisionStructureID != null && unitOfWork.TblCommisionStructureHeaderRepository.Get().Any(x => x.ComStructName.ToLower() == commisionStructureName.ToLower() && x.ComStructID != commisionStructureID))
                {
                    return true;
                }
                else if (commisionStructureID == null && unitOfWork.TblCommisionStructureHeaderRepository.Get().Any(x => x.ComStructName.ToLower() == commisionStructureName.ToLower()))
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
        public bool SaveCommisionStructureLine(CommisionStructureLineVM commisionStructureLineVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblCommisionStructureLine commisionStructureLine = new tblCommisionStructureLine();
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
                    commisionStructureLine.CreatedDate = DateTime.Now;
                    commisionStructureLine.CreatedBy = commisionStructureLineVM.CreatedBy;
                    unitOfWork.TblCommisionStructureLineRepository.Insert(commisionStructureLine);
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
