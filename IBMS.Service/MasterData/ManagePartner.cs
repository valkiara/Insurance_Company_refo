using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManagePartner
    {
        private UnitOfWork unitOfWork;
        public ManagePartner()
        {
            unitOfWork = new UnitOfWork();
        }

        #region Partner
        public bool SavePartner(PartnerVM partnerVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblPartner partner = new tblPartner();
                    partner.PartnerName = partnerVM.PartnerName;
                    partner.CreatedDate = DateTime.Now;
                    partner.CreatedBy = partnerVM.CreatedBy;
                    unitOfWork.TblPartnerRepository.Insert(partner);
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

        public bool UpdatePartner(PartnerVM partnerVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblPartner partner = unitOfWork.TblPartnerRepository.GetByID(partnerVM.PartnerID);
                    partner.PartnerName = partnerVM.PartnerName;
                    partner.ModifiedDate = DateTime.Now;
                    partner.ModifiedBy = partnerVM.ModifiedBy;
                    unitOfWork.TblPartnerRepository.Update(partner);
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

        public bool DeletePartner(int partnerID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblPartner partner = unitOfWork.TblPartnerRepository.GetByID(partnerID);
                    unitOfWork.TblPartnerRepository.Delete(partner);
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

        public List<PartnerVM> GetAllPartners()
        {
            try
            {
                var partnerData = unitOfWork.TblPartnerRepository.Get().ToList();

                List<PartnerVM> partnerList = new List<PartnerVM>();

                foreach (var partner in partnerData)
                {
                    PartnerVM partnerVM = new PartnerVM();
                    partnerVM.PartnerID = partner.PartnerID;
                    partnerVM.PartnerName = partner.PartnerName;
                    partnerVM.CreatedDate = partner.CreatedDate != null ? partner.CreatedDate.ToString() : string.Empty;
                    partnerVM.ModifiedDate = partner.ModifiedDate != null ? partner.ModifiedDate.ToString() : string.Empty;
                    partnerVM.CreatedBy = partner.CreatedBy != null ? Convert.ToInt32(partner.CreatedBy) : 0;
                    partnerVM.ModifiedBy = partner.ModifiedBy != null ? Convert.ToInt32(partner.ModifiedBy) : 0;

                    partnerList.Add(partnerVM);
                }

                return partnerList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PartnerVM GetPartnerByID(int partnerID)
        {
            try
            {
                var partnerData = unitOfWork.TblPartnerRepository.GetByID(partnerID);

                PartnerVM partnerVM = new PartnerVM();
                partnerVM.PartnerID = partnerData.PartnerID;
                partnerVM.PartnerName = partnerData.PartnerName;
                partnerVM.CreatedDate = partnerData.CreatedDate != null ? partnerData.CreatedDate.ToString() : string.Empty;
                partnerVM.ModifiedDate = partnerData.ModifiedDate != null ? partnerData.ModifiedDate.ToString() : string.Empty;
                partnerVM.CreatedBy = partnerData.CreatedBy != null ? Convert.ToInt32(partnerData.CreatedBy) : 0;
                partnerVM.ModifiedBy = partnerData.ModifiedBy != null ? Convert.ToInt32(partnerData.ModifiedBy) : 0;

                return partnerVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsPartnerAvailable(int? partnerID, string partnerName)
        {
            try
            {
                if (partnerID != null && unitOfWork.TblPartnerRepository.Get().Any(x => x.PartnerName.ToLower() == partnerName.ToLower() && x.PartnerID != partnerID))
                {
                    return true;
                }
                else if (partnerID == null && unitOfWork.TblPartnerRepository.Get().Any(x => x.PartnerName.ToLower() == partnerName.ToLower()))
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

        //Partner Mapping need to be modified
        #region Partner Mapping
        public bool SavePartnerMapping(PartnerMappingVM partnerMappingVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblPartnerMapping partnerMapping = new tblPartnerMapping();
                    partnerMapping.PartnerName = partnerMappingVM.PartnerName;
                    partnerMapping.CreatedDate = DateTime.Now;
                    partnerMapping.CreatedBy = partnerMappingVM.CreatedBy;
                    unitOfWork.TblPartnerMappingRepository.Insert(partnerMapping);
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

        public bool UpdatePartnerMapping(PartnerMappingVM partnerMappingVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblPartnerMapping partnerMapping = unitOfWork.TblPartnerMappingRepository.GetByID(partnerMappingVM.PartnerID);
                    partnerMapping.PartnerName = partnerMappingVM.PartnerName;
                    partnerMapping.ModifiedDate = DateTime.Now;
                    partnerMapping.ModifiedBy = partnerMappingVM.ModifiedBy;
                    unitOfWork.TblPartnerMappingRepository.Update(partnerMapping);
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

        public bool DeletePartnerMapping(int partnerID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblPartnerMapping partner = unitOfWork.TblPartnerMappingRepository.GetByID(partnerID);
                    unitOfWork.TblPartnerMappingRepository.Delete(partner);
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

        public List<PartnerMappingVM> GetAllPartnerMappings()
        {
            try
            {
                var partnerData = unitOfWork.TblPartnerMappingRepository.Get().ToList();

                List<PartnerMappingVM> partnerList = new List<PartnerMappingVM>();

                foreach (var partner in partnerData)
                {
                    PartnerMappingVM partnerMappingVM = new PartnerMappingVM();
                    partnerMappingVM.PartnerID = partner.PartnerID;
                    partnerMappingVM.PartnerName = partner.PartnerName;
                    partnerMappingVM.CreatedDate = partner.CreatedDate != null ? partner.CreatedDate.ToString() : string.Empty;
                    partnerMappingVM.ModifiedDate = partner.ModifiedDate != null ? partner.ModifiedDate.ToString() : string.Empty;
                    partnerMappingVM.CreatedBy = partner.CreatedBy != null ? Convert.ToInt32(partner.CreatedBy) : 0;
                    partnerMappingVM.ModifiedBy = partner.ModifiedBy != null ? Convert.ToInt32(partner.ModifiedBy) : 0;

                    partnerList.Add(partnerMappingVM);
                }

                return partnerList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PartnerMappingVM GetPartnerMappingByID(int partnerID)
        {
            try
            {
                var partnerData = unitOfWork.TblPartnerMappingRepository.GetByID(partnerID);

                PartnerMappingVM partnerMappingVM = new PartnerMappingVM();
                partnerMappingVM.PartnerID = partnerData.PartnerID;
                partnerMappingVM.PartnerName = partnerData.PartnerName;
                partnerMappingVM.CreatedDate = partnerData.CreatedDate != null ? partnerData.CreatedDate.ToString() : string.Empty;
                partnerMappingVM.ModifiedDate = partnerData.ModifiedDate != null ? partnerData.ModifiedDate.ToString() : string.Empty;
                partnerMappingVM.CreatedBy = partnerData.CreatedBy != null ? Convert.ToInt32(partnerData.CreatedBy) : 0;
                partnerMappingVM.ModifiedBy = partnerData.ModifiedBy != null ? Convert.ToInt32(partnerData.ModifiedBy) : 0;

                return partnerMappingVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsPartnerMappingAvailable(int? partnerID, string partnerName)
        {
            try
            {
                if (partnerID != null && unitOfWork.TblPartnerMappingRepository.Get().Any(x => x.PartnerName.ToLower() == partnerName.ToLower() && x.PartnerID != partnerID))
                {
                    return true;
                }
                else if (partnerID == null && unitOfWork.TblPartnerMappingRepository.Get().Any(x => x.PartnerName.ToLower() == partnerName.ToLower()))
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
