using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManageDesignation
    {
        private UnitOfWork unitOfWork;
        public ManageDesignation()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool SaveDesignation(DesignationVM designationVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblDesignation designation = new tblDesignation();
                    designation.BUID = designationVM.BusinessUnitID;
                    designation.Designation = designationVM.DesignationName;
                    designation.CreatedDate = DateTime.Now;
                    designation.CreatedBy = designationVM.CreatedBy;
                    unitOfWork.TblDesignationRepository.Insert(designation);
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

        public bool UpdateDesignation(DesignationVM designationVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblDesignation designation = unitOfWork.TblDesignationRepository.GetByID(designationVM.DesignationID);
                    designation.BUID = designationVM.BusinessUnitID;
                    designation.Designation = designationVM.DesignationName;
                    designation.ModifiedDate = DateTime.Now;
                    designation.ModifiedBy = designationVM.ModifiedBy;
                    unitOfWork.TblDesignationRepository.Update(designation);
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

        public bool DeleteDesignation(int designationID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblDesignation designation = unitOfWork.TblDesignationRepository.GetByID(designationID);
                    unitOfWork.TblDesignationRepository.Delete(designation);
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

        public List<DesignationVM> GetAllDesignations()
        {
            try
            {
                var designationData = unitOfWork.TblDesignationRepository.Get().ToList();

                List<DesignationVM> designationList = new List<DesignationVM>();

                foreach (var designation in designationData)
                {
                    DesignationVM designationVM = new DesignationVM();
                    designationVM.DesignationID = designation.DesignationID;
                    designationVM.BusinessUnitID = designation.BUID != null ? Convert.ToInt32(designation.BUID) : 0;

                    if (designationVM.BusinessUnitID > 0)
                    {
                        designationVM.BusinessUnitName = designation.tblBussinessUnit.BussinessUnit;
                    }

                    designationVM.DesignationName = designation.Designation;
                    designationVM.CreatedDate = designation.CreatedDate != null ? designation.CreatedDate.ToString() : string.Empty;
                    designationVM.ModifiedDate = designation.ModifiedDate != null ? designation.ModifiedDate.ToString() : string.Empty;
                    designationVM.CreatedBy = designation.CreatedBy != null ? Convert.ToInt32(designation.CreatedBy) : 0;
                    designationVM.ModifiedBy = designation.ModifiedBy != null ? Convert.ToInt32(designation.ModifiedBy) : 0;

                    designationList.Add(designationVM);
                }

                return designationList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<DesignationVM> GetAllDesignationsByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var designationData = unitOfWork.TblDesignationRepository.Get(x => x.BUID == businessUnitID).ToList();

                List<DesignationVM> designationList = new List<DesignationVM>();

                foreach (var designation in designationData)
                {
                    DesignationVM designationVM = new DesignationVM();
                    designationVM.DesignationID = designation.DesignationID;
                    designationVM.BusinessUnitID = designation.BUID != null ? Convert.ToInt32(designation.BUID) : 0;

                    if (designationVM.BusinessUnitID > 0)
                    {
                        designationVM.BusinessUnitName = designation.tblBussinessUnit.BussinessUnit;
                    }

                    designationVM.DesignationName = designation.Designation;
                    designationVM.CreatedDate = designation.CreatedDate != null ? designation.CreatedDate.ToString() : string.Empty;
                    designationVM.ModifiedDate = designation.ModifiedDate != null ? designation.ModifiedDate.ToString() : string.Empty;
                    designationVM.CreatedBy = designation.CreatedBy != null ? Convert.ToInt32(designation.CreatedBy) : 0;
                    designationVM.ModifiedBy = designation.ModifiedBy != null ? Convert.ToInt32(designation.ModifiedBy) : 0;

                    designationList.Add(designationVM);
                }

                return designationList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DesignationVM GetDesignationByID(int designationID)
        {
            try
            {
                var designationData = unitOfWork.TblDesignationRepository.GetByID(designationID);

                DesignationVM designationVM = new DesignationVM();
                designationVM.DesignationID = designationData.DesignationID;
                designationVM.BusinessUnitID = designationData.BUID != null ? Convert.ToInt32(designationData.BUID) : 0;

                if (designationVM.BusinessUnitID > 0)
                {
                    designationVM.BusinessUnitName = designationData.tblBussinessUnit.BussinessUnit;
                }

                designationVM.DesignationName = designationData.Designation;
                designationVM.CreatedDate = designationData.CreatedDate != null ? designationData.CreatedDate.ToString() : string.Empty;
                designationVM.ModifiedDate = designationData.ModifiedDate != null ? designationData.ModifiedDate.ToString() : string.Empty;
                designationVM.CreatedBy = designationData.CreatedBy != null ? Convert.ToInt32(designationData.CreatedBy) : 0;
                designationVM.ModifiedBy = designationData.ModifiedBy != null ? Convert.ToInt32(designationData.ModifiedBy) : 0;

                return designationVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsDesignationAvailable(int? designationID, string designationName)
        {
            try
            {
                if (designationID != null && unitOfWork.TblDesignationRepository.Get().Any(x => x.Designation.ToLower() == designationName.ToLower() && x.DesignationID != designationID))
                {
                    return true;
                }
                else if (designationID == null && unitOfWork.TblDesignationRepository.Get().Any(x => x.Designation.ToLower() == designationName.ToLower()))
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
    }
}
