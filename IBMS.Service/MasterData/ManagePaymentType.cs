using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManagePaymentType
    {
        private UnitOfWork unitOfWork;
        public ManagePaymentType()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool SavePaymentType(PaymentTypeVM paymentTypeVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblPaymentType paymentType = new tblPaymentType();
                    paymentType.PaymentType = paymentTypeVM.PaymentTypeName;
                    paymentType.Description = paymentTypeVM.Description;
                    paymentType.CreatedDate = DateTime.Now;
                    paymentType.CreatedBy = paymentTypeVM.CreatedBy;
                    unitOfWork.TblPaymentTypeRepository.Insert(paymentType);
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

        public bool UpdatePaymentType(PaymentTypeVM paymentTypeVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblPaymentType paymentType = unitOfWork.TblPaymentTypeRepository.GetByID(paymentTypeVM.PaymentTypeID);
                    paymentType.PaymentType = paymentTypeVM.PaymentTypeName;
                    paymentType.Description = paymentTypeVM.Description;
                    paymentType.ModifiedDate = DateTime.Now;
                    paymentType.ModifiedBy = paymentTypeVM.ModifiedBy;
                    unitOfWork.TblPaymentTypeRepository.Update(paymentType);
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

        public bool DeletePaymentType(int paymentTypeID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblPaymentType paymentType = unitOfWork.TblPaymentTypeRepository.GetByID(paymentTypeID);
                    unitOfWork.TblPaymentTypeRepository.Delete(paymentType);
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

        public List<PaymentTypeVM> GetAllPaymentTypes()
        {
            try
            {
                var paymentTypeData = unitOfWork.TblPaymentTypeRepository.Get().ToList();

                List<PaymentTypeVM> paymentTypeList = new List<PaymentTypeVM>();

                foreach (var paymentType in paymentTypeData)
                {
                    PaymentTypeVM paymentTypeVM = new PaymentTypeVM();
                    paymentTypeVM.PaymentTypeID = paymentType.PaymentTypeID;
                    paymentTypeVM.PaymentTypeName = paymentType.PaymentType;
                    paymentTypeVM.Description = paymentType.Description;
                    paymentTypeVM.CreatedBy = paymentType.CreatedBy != null ? Convert.ToInt32(paymentType.CreatedBy) : 0;
                    paymentTypeVM.CreatedDate = paymentType.CreatedDate != null ? paymentType.CreatedDate.ToString() : string.Empty;
                    paymentTypeVM.ModifiedBy = paymentType.ModifiedBy != null ? Convert.ToInt32(paymentType.ModifiedBy) : 0;
                    paymentTypeVM.ModifiedDate = paymentType.ModifiedDate != null ? paymentType.ModifiedDate.ToString() : string.Empty;

                    paymentTypeList.Add(paymentTypeVM);
                }

                return paymentTypeList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PaymentTypeVM GetPaymentTypeByID(int paymentTypeID)
        {
            try
            {
                var paymentTypeData = unitOfWork.TblPaymentTypeRepository.GetByID(paymentTypeID);

                PaymentTypeVM paymentTypeVM = new PaymentTypeVM();
                paymentTypeVM.PaymentTypeID = paymentTypeData.PaymentTypeID;
                paymentTypeVM.PaymentTypeName = paymentTypeData.PaymentType;
                paymentTypeVM.Description = paymentTypeData.Description;
                paymentTypeVM.CreatedBy = paymentTypeData.CreatedBy != null ? Convert.ToInt32(paymentTypeData.CreatedBy) : 0;
                paymentTypeVM.CreatedDate = paymentTypeData.CreatedDate != null ? paymentTypeData.CreatedDate.ToString() : string.Empty;
                paymentTypeVM.ModifiedBy = paymentTypeData.ModifiedBy != null ? Convert.ToInt32(paymentTypeData.ModifiedBy) : 0;
                paymentTypeVM.ModifiedDate = paymentTypeData.ModifiedDate != null ? paymentTypeData.ModifiedDate.ToString() : string.Empty;

                return paymentTypeVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsPaymentTypeAvailable(int? paymentTypeID, string paymentTypeName)
        {
            try
            {
                if (paymentTypeID != null && unitOfWork.TblPaymentTypeRepository.Get().Any(x => x.PaymentType.ToLower() == paymentTypeName.ToLower() && x.PaymentTypeID != paymentTypeID))
                {
                    return true;
                }
                else if (paymentTypeID == null && unitOfWork.TblPaymentTypeRepository.Get().Any(x => x.PaymentType.ToLower() == paymentTypeName.ToLower()))
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
