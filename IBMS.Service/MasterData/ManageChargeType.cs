using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManageChargeType
    {
        private UnitOfWork unitOfWork;
        public ManageChargeType()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool SaveChargeType(ChargeTypeVM chargeTypeVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblChargeType chargeType = new tblChargeType();
                    chargeType.ChargeType = chargeTypeVM.ChargeTypeName;
                    chargeType.Percentage = chargeTypeVM.Percentage;
                    chargeType.CreatedDate = DateTime.Now;
                    chargeType.CreatedBy = chargeTypeVM.CreatedBy;
                    unitOfWork.TblChargeTypeRepository.Insert(chargeType);
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

        public bool UpdateChargeType(ChargeTypeVM chargeTypeVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblChargeType chargeType = unitOfWork.TblChargeTypeRepository.GetByID(chargeTypeVM.ChargeTypeID);
                    chargeType.ChargeType = chargeTypeVM.ChargeTypeName;
                    chargeType.Percentage = chargeTypeVM.Percentage;
                    chargeType.ModifiedDate = DateTime.Now;
                    chargeType.ModifiedBy = chargeTypeVM.ModifiedBy;
                    unitOfWork.TblChargeTypeRepository.Update(chargeType);
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

        public bool DeleteChargeType(int chargeTypeID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblChargeType chargeType = unitOfWork.TblChargeTypeRepository.GetByID(chargeTypeID);
                    unitOfWork.TblChargeTypeRepository.Delete(chargeType);
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

        public List<ChargeTypeVM> GetAllChargeTypes()
        {
            try
            {
                var chargeTypeData = unitOfWork.TblChargeTypeRepository.Get().ToList();

                List<ChargeTypeVM> chargeTypeList = new List<ChargeTypeVM>();

                foreach (var chargeType in chargeTypeData)
                {
                    ChargeTypeVM chargeTypeVM = new ChargeTypeVM();
                    chargeTypeVM.ChargeTypeID = chargeType.ChargeTypeID;
                    chargeTypeVM.ChargeTypeName = chargeType.ChargeType;
                    chargeTypeVM.Percentage = chargeType.Percentage != null ? Convert.ToDouble(chargeType.Percentage) : 0;
                    chargeTypeVM.CreatedBy = chargeType.CreatedBy != null ? Convert.ToInt32(chargeType.CreatedBy) : 0;
                    chargeTypeVM.CreatedDate = chargeType.CreatedDate != null ? chargeType.CreatedDate.ToString() : string.Empty;
                    chargeTypeVM.ModifiedBy = chargeType.ModifiedBy != null ? Convert.ToInt32(chargeType.ModifiedBy) : 0;
                    chargeTypeVM.ModifiedDate = chargeType.ModifiedDate != null ? chargeType.ModifiedDate.ToString() : string.Empty;

                    chargeTypeList.Add(chargeTypeVM);
                }

                return chargeTypeList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ChargeTypeVM GetChargeTypeByID(int chargeTypeID)
        {
            try
            {
                var chargeTypeData = unitOfWork.TblChargeTypeRepository.GetByID(chargeTypeID);

                ChargeTypeVM chargeTypeVM = new ChargeTypeVM();
                chargeTypeVM.ChargeTypeID = chargeTypeData.ChargeTypeID;
                chargeTypeVM.ChargeTypeName = chargeTypeData.ChargeType;
                chargeTypeVM.Percentage = chargeTypeData.Percentage != null ? Convert.ToDouble(chargeTypeData.Percentage) : 0;
                chargeTypeVM.CreatedBy = chargeTypeData.CreatedBy != null ? Convert.ToInt32(chargeTypeData.CreatedBy) : 0;
                chargeTypeVM.CreatedDate = chargeTypeData.CreatedDate != null ? chargeTypeData.CreatedDate.ToString() : string.Empty;
                chargeTypeVM.ModifiedBy = chargeTypeData.ModifiedBy != null ? Convert.ToInt32(chargeTypeData.ModifiedBy) : 0;
                chargeTypeVM.ModifiedDate = chargeTypeData.ModifiedDate != null ? chargeTypeData.ModifiedDate.ToString() : string.Empty;

                return chargeTypeVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsChargeTypeAvailable(int? chargeTypeID, string chargeTypeName)
        {
            try
            {
                if (chargeTypeID != null && unitOfWork.TblChargeTypeRepository.Get().Any(x => x.ChargeType.ToLower() == chargeTypeName.ToLower() && x.ChargeTypeID != chargeTypeID))
                {
                    return true;
                }
                else if (chargeTypeID == null && unitOfWork.TblChargeTypeRepository.Get().Any(x => x.ChargeType.ToLower() == chargeTypeName.ToLower()))
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
        public List<ChargeTypeVM> GetApplicableChargeTypes()
        {
            try
            {
                var chargeTypeData = unitOfWork.TblChargeTypeRepository.Get(x => x.PolicyApplicable == true).ToList().OrderBy(o => o.PolicyPriority);

                List<ChargeTypeVM> chargeTypeList = new List<ChargeTypeVM>();

                foreach (var chargeType in chargeTypeData)
                {
                    ChargeTypeVM chargeTypeVM = new ChargeTypeVM();
                    chargeTypeVM.ChargeTypeID = chargeType.ChargeTypeID;
                    chargeTypeVM.ChargeTypeName = chargeType.ChargeType;
                    chargeTypeVM.Percentage = chargeType.Percentage != null ? Convert.ToDouble(chargeType.Percentage) : 0;
                    chargeTypeVM.CreatedBy = chargeType.CreatedBy != null ? Convert.ToInt32(chargeType.CreatedBy) : 0;
                    chargeTypeVM.CreatedDate = chargeType.CreatedDate != null ? chargeType.CreatedDate.ToString() : string.Empty;
                    chargeTypeVM.ModifiedBy = chargeType.ModifiedBy != null ? Convert.ToInt32(chargeType.ModifiedBy) : 0;
                    chargeTypeVM.ModifiedDate = chargeType.ModifiedDate != null ? chargeType.ModifiedDate.ToString() : string.Empty;

                    chargeTypeList.Add(chargeTypeVM);
                }

                return chargeTypeList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
