using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManageIntroducer
    {
        private UnitOfWork unitOfWork;
        public ManageIntroducer()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool SaveIntroducer(IntroducerVM introducerVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    //Save Introducer
                    tblIntroducer introducer = new tblIntroducer();
                    introducer.IntroducerName = introducerVM.IntroducerName;
                    introducer.Description = introducerVM.Description;
                    introducer.Address1 = introducerVM.Address1;
                    introducer.Address2 = introducerVM.Address2;
                    introducer.Address3 = introducerVM.Address3;
                    introducer.CreatedDate = DateTime.Now;
                    introducer.CreatedBy = introducerVM.CreatedBy;
                    unitOfWork.TblIntroducerRepository.Insert(introducer);
                    unitOfWork.Save();

                    //Save Business Unit ID List
                    foreach (int businessUnitID in introducerVM.BusinessUnitIDList)
                    {
                        tblIntroducerBusinessUnit introducerBusinessUnit = new tblIntroducerBusinessUnit();
                        introducerBusinessUnit.IntroducerID = introducer.IntroducerID;
                        introducerBusinessUnit.BUID = businessUnitID;
                        unitOfWork.TblIntroducerBusinessUnitRepository.Insert(introducerBusinessUnit);
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

        public bool UpdateIntroducer(IntroducerVM introducerVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    //Update Introducer
                    tblIntroducer introducer = unitOfWork.TblIntroducerRepository.GetByID(introducerVM.IntroducerID);
                    introducer.IntroducerName = introducerVM.IntroducerName;
                    introducer.Description = introducerVM.Description;
                    introducer.Address1 = introducerVM.Address1;
                    introducer.Address2 = introducerVM.Address2;
                    introducer.Address3 = introducerVM.Address3;
                    introducer.ModifiedDate = DateTime.Now;
                    introducer.ModifiedBy = introducerVM.ModifiedBy;
                    unitOfWork.TblIntroducerRepository.Update(introducer);
                    unitOfWork.Save();


                    //Delete Existing Business Unit ID List
                    List<tblIntroducerBusinessUnit> existingBusinessUnitList = unitOfWork.TblIntroducerBusinessUnitRepository.Get(x => x.IntroducerID == introducerVM.IntroducerID).ToList();

                    foreach (tblIntroducerBusinessUnit existingBusinessUnit in existingBusinessUnitList)
                    {
                        unitOfWork.TblIntroducerBusinessUnitRepository.Delete(existingBusinessUnit);
                        unitOfWork.Save();
                    }

                    //Save New Business Unit ID List
                    foreach (int businessUnitID in introducerVM.BusinessUnitIDList)
                    {
                        tblIntroducerBusinessUnit introducerBusinessUnit = new tblIntroducerBusinessUnit();
                        introducerBusinessUnit.IntroducerID = introducer.IntroducerID;
                        introducerBusinessUnit.BUID = businessUnitID;
                        unitOfWork.TblIntroducerBusinessUnitRepository.Insert(introducerBusinessUnit);
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

        public bool DeleteIntroducer(int introducerID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    //Delete Business Unit List
                    List<tblIntroducerBusinessUnit> existingBusinessUnitList = unitOfWork.TblIntroducerBusinessUnitRepository.Get(x => x.IntroducerID == introducerID).ToList();

                    foreach (tblIntroducerBusinessUnit existingBusinessUnit in existingBusinessUnitList)
                    {
                        unitOfWork.TblIntroducerBusinessUnitRepository.Delete(existingBusinessUnit);
                        unitOfWork.Save();
                    }

                    //Delete Introducer
                    tblIntroducer introducer = unitOfWork.TblIntroducerRepository.GetByID(introducerID);
                    unitOfWork.TblIntroducerRepository.Delete(introducer);
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

        public List<IntroducerVM> GetAllIntroducers()
        {
            try
            {
                var introducerData = unitOfWork.TblIntroducerRepository.Get().ToList();

                List<IntroducerVM> introducerList = new List<IntroducerVM>();

                foreach (var introducer in introducerData)
                {
                    IntroducerVM introducerVM = new IntroducerVM();
                    introducerVM.IntroducerID = introducer.IntroducerID;
                    introducerVM.IntroducerName = introducer.IntroducerName;
                    introducerVM.Description = introducer.Description;
                    introducerVM.Address1 = introducer.Address1;
                    introducerVM.Address2 = introducer.Address2;
                    introducerVM.Address3 = introducer.Address3;
                    introducerVM.CreatedDate = introducer.CreatedDate != null ? introducer.CreatedDate.ToString() : string.Empty;
                    introducerVM.ModifiedDate = introducer.ModifiedDate != null ? introducer.ModifiedDate.ToString() : string.Empty;
                    introducerVM.CreatedBy = introducer.CreatedBy != null ? Convert.ToInt32(introducer.CreatedBy) : 0;
                    introducerVM.ModifiedBy = introducer.ModifiedBy != null ? Convert.ToInt32(introducer.ModifiedBy) : 0;

                    List<int> businessUnitIDList = new List<int>();
                    List<tblIntroducerBusinessUnit> introducerBusinessUnitList = unitOfWork.TblIntroducerBusinessUnitRepository.Get(x => x.IntroducerID == introducer.IntroducerID).ToList();

                    foreach (tblIntroducerBusinessUnit introducerBusinessUnit in introducerBusinessUnitList)
                    {
                        businessUnitIDList.Add(introducerBusinessUnit.BUID);
                    }

                    introducerVM.BusinessUnitIDList = businessUnitIDList;

                    introducerList.Add(introducerVM);
                }

                return introducerList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<IntroducerVM> GetAllIntroducersByBusinessUnitID(int businessUnitID)
        {
            try
            {
                //List<tblIntroducerBusinessUnit> introducerData = unitOfWork.TblIntroducerBusinessUnitRepository.Get(x => x.BUID == businessUnitID).ToList().GroupBy(x => x.IntroducerID).Select(x => x.FirstOrDefault()).ToList();
                List<tblIntroducerBusinessUnit> introducerData = unitOfWork.TblIntroducerBusinessUnitRepository.Get().ToList();;
                List<IntroducerVM> introducerList = new List<IntroducerVM>();

                foreach (var introducer in introducerData)
                {
                    IntroducerVM introducerVM = new IntroducerVM();
                    introducerVM.IntroducerID = introducer.tblIntroducer.IntroducerID;
                    introducerVM.IntroducerName = introducer.tblIntroducer.IntroducerName;
                    introducerVM.Description = introducer.tblIntroducer.Description;
                    introducerVM.Address1 = introducer.tblIntroducer.Address1;
                    introducerVM.Address2 = introducer.tblIntroducer.Address2;
                    introducerVM.Address3 = introducer.tblIntroducer.Address3;
                    introducerVM.CreatedDate = introducer.tblIntroducer.CreatedDate != null ? introducer.tblIntroducer.CreatedDate.ToString() : string.Empty;
                    introducerVM.ModifiedDate = introducer.tblIntroducer.ModifiedDate != null ? introducer.tblIntroducer.ModifiedDate.ToString() : string.Empty;
                    introducerVM.CreatedBy = introducer.tblIntroducer.CreatedBy != null ? Convert.ToInt32(introducer.tblIntroducer.CreatedBy) : 0;
                    introducerVM.ModifiedBy = introducer.tblIntroducer.ModifiedBy != null ? Convert.ToInt32(introducer.tblIntroducer.ModifiedBy) : 0;

                    List<int> businessUnitIDList = new List<int>();
                    List<tblIntroducerBusinessUnit> introducerBusinessUnitList = unitOfWork.TblIntroducerBusinessUnitRepository.Get(x => x.IntroducerID == introducer.tblIntroducer.IntroducerID).ToList();

                    foreach (tblIntroducerBusinessUnit introducerBusinessUnit in introducerBusinessUnitList)
                    {
                        businessUnitIDList.Add(introducerBusinessUnit.BUID);
                    }

                    introducerVM.BusinessUnitIDList = businessUnitIDList;

                    introducerList.Add(introducerVM);
                }

                return introducerList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IntroducerVM GetIntroducerByID(int introducerID)
        {
            try
            {
                var introducerData = unitOfWork.TblIntroducerRepository.GetByID(introducerID);

                IntroducerVM introducerVM = new IntroducerVM();
                introducerVM.IntroducerID = introducerData.IntroducerID;
                introducerVM.IntroducerName = introducerData.IntroducerName;
                introducerVM.Description = introducerData.Description;
                introducerVM.Address1 = introducerData.Address1;
                introducerVM.Address2 = introducerData.Address2;
                introducerVM.Address3 = introducerData.Address3;
                introducerVM.CreatedDate = introducerData.CreatedDate != null ? introducerData.CreatedDate.ToString() : string.Empty;
                introducerVM.ModifiedDate = introducerData.ModifiedDate != null ? introducerData.ModifiedDate.ToString() : string.Empty;
                introducerVM.CreatedBy = introducerData.CreatedBy != null ? Convert.ToInt32(introducerData.CreatedBy) : 0;
                introducerVM.ModifiedBy = introducerData.ModifiedBy != null ? Convert.ToInt32(introducerData.ModifiedBy) : 0;

                List<int> businessUnitIDList = new List<int>();
                List<tblIntroducerBusinessUnit> introducerBusinessUnitList = unitOfWork.TblIntroducerBusinessUnitRepository.Get(x => x.IntroducerID == introducerID).ToList();

                foreach (tblIntroducerBusinessUnit introducerBusinessUnit in introducerBusinessUnitList)
                {
                    businessUnitIDList.Add(introducerBusinessUnit.BUID);
                }

                introducerVM.BusinessUnitIDList = businessUnitIDList;

                return introducerVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsIntroducerAvailable(int? introducerID, string introducerName)
        {
            try
            {
                if (introducerID != null && unitOfWork.TblIntroducerRepository.Get().Any(x => x.IntroducerName.ToLower() == introducerName.ToLower() && x.IntroducerID != introducerID))
                {
                    return true;
                }
                else if (introducerID == null && unitOfWork.TblIntroducerRepository.Get().Any(x => x.IntroducerName.ToLower() == introducerName.ToLower()))
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
