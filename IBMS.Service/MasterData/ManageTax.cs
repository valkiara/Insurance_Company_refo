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
    public class ManageTax
    {
        private UnitOfWork unitOfWork;
        public ManageTax()
        {
            unitOfWork = new UnitOfWork();
        }

        #region Tax Type
        public bool SaveTaxType(TaxTypeVM taxTypeVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblTaxType taxType = new tblTaxType();
                    taxType.TaxTypeCode = taxTypeVM.TaxTypeCode;
                    taxType.Description = taxTypeVM.Description;
                    taxType.Percentage = taxTypeVM.Percentage;
                    taxType.ExpiryDate = !string.IsNullOrEmpty(taxTypeVM.ExpiryDate) ? DateTime.ParseExact(taxTypeVM.ExpiryDate, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    taxType.IsActive = taxTypeVM.IsActive;
                    taxType.CreatedDate = DateTime.Now;
                    taxType.CreatedBy = taxTypeVM.CreatedBy;
                    unitOfWork.TblTaxTypeRepository.Insert(taxType);
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

        public bool UpdateTaxType(TaxTypeVM taxTypeVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblTaxType taxType = unitOfWork.TblTaxTypeRepository.GetByID(taxTypeVM.TaxTypeID);
                    taxType.TaxTypeCode = taxTypeVM.TaxTypeCode;
                    taxType.Description = taxTypeVM.Description;
                    taxType.Percentage = taxTypeVM.Percentage;
                    taxType.ExpiryDate = !string.IsNullOrEmpty(taxTypeVM.ExpiryDate) ? DateTime.ParseExact(taxTypeVM.ExpiryDate, "MM/dd/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    taxType.IsActive = taxTypeVM.IsActive;
                    taxType.ModifiedDate = DateTime.Now;
                    taxType.ModifiedBy = taxTypeVM.ModifiedBy;
                    unitOfWork.TblTaxTypeRepository.Update(taxType);
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

        public bool DeleteTaxType(int taxTypeID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblTaxType taxType = unitOfWork.TblTaxTypeRepository.GetByID(taxTypeID);
                    unitOfWork.TblTaxTypeRepository.Delete(taxType);
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

        public List<TaxTypeVM> GetAllTaxTypes()
        {
            try
            {
                var taxTypeData = unitOfWork.TblTaxTypeRepository.Get().ToList();

                List<TaxTypeVM> taxTypeList = new List<TaxTypeVM>();

                foreach (var taxType in taxTypeData)
                {
                    TaxTypeVM taxTypeVM = new TaxTypeVM();
                    taxTypeVM.TaxTypeID = taxType.TaxTypeID;
                    taxTypeVM.TaxTypeCode = taxType.TaxTypeCode;
                    taxTypeVM.Description = taxType.Description;
                    taxTypeVM.Percentage = taxType.Percentage != null ? Convert.ToDouble(taxType.Percentage) : 0;
                    taxTypeVM.ExpiryDate = taxType.ExpiryDate != null ? taxType.ExpiryDate.ToString() : string.Empty;
                    taxTypeVM.IsActive = taxType.IsActive;
                    taxTypeVM.CreatedBy = taxType.CreatedBy != null ? Convert.ToInt32(taxType.CreatedBy) : 0;
                    taxTypeVM.CreatedDate = taxType.CreatedDate != null ? taxType.CreatedDate.ToString() : string.Empty;
                    taxTypeVM.ModifiedBy = taxType.ModifiedBy != null ? Convert.ToInt32(taxType.ModifiedBy) : 0;
                    taxTypeVM.ModifiedDate = taxType.ModifiedDate != null ? taxType.ModifiedDate.ToString() : string.Empty;

                    taxTypeList.Add(taxTypeVM);
                }

                return taxTypeList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public TaxTypeVM GetTaxTypeByID(int taxTypeID)
        {
            try
            {
                var taxTypeData = unitOfWork.TblTaxTypeRepository.GetByID(taxTypeID);

                TaxTypeVM taxTypeVM = new TaxTypeVM();
                taxTypeVM.TaxTypeID = taxTypeData.TaxTypeID;
                taxTypeVM.TaxTypeCode = taxTypeData.TaxTypeCode;
                taxTypeVM.Description = taxTypeData.Description;
                taxTypeVM.Percentage = taxTypeData.Percentage != null ? Convert.ToDouble(taxTypeData.Percentage) : 0;
                taxTypeVM.ExpiryDate = taxTypeData.ExpiryDate != null ? taxTypeData.ExpiryDate.ToString() : string.Empty;
                taxTypeVM.IsActive = taxTypeData.IsActive;
                taxTypeVM.CreatedBy = taxTypeData.CreatedBy != null ? Convert.ToInt32(taxTypeData.CreatedBy) : 0;
                taxTypeVM.CreatedDate = taxTypeData.CreatedDate != null ? taxTypeData.CreatedDate.ToString() : string.Empty;
                taxTypeVM.ModifiedBy = taxTypeData.ModifiedBy != null ? Convert.ToInt32(taxTypeData.ModifiedBy) : 0;
                taxTypeVM.ModifiedDate = taxTypeData.ModifiedDate != null ? taxTypeData.ModifiedDate.ToString() : string.Empty;

                return taxTypeVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsTaxTypeAvailable(int? taxTypeID, string taxTypeCode)
        {
            try
            {
                if (taxTypeID != null && unitOfWork.TblTaxTypeRepository.Get().Any(x => x.TaxTypeCode.ToLower() == taxTypeCode.ToLower() && x.TaxTypeID != taxTypeID))
                {
                    return true;
                }
                else if (taxTypeID == null && unitOfWork.TblTaxTypeRepository.Get().Any(x => x.TaxTypeCode.ToLower() == taxTypeCode.ToLower()))
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
