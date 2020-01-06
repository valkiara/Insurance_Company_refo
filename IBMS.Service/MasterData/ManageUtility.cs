using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.MasterData
{
    public class ManageUtility
    {
        private UnitOfWork unitOfWork;
        public ManageUtility()
        {
            unitOfWork = new UnitOfWork();
        }

        public List<DeductionTypeVM> getAllDeductionType(int businessUnitID)
        {
            try
            {
                var DeductionTypedata = unitOfWork.TblDeductionTypeRepository.Get(x=>x.BusinessUnit==businessUnitID).ToList();

                List<DeductionTypeVM> DeductionTypedataList = new List<DeductionTypeVM>();

                foreach (var DeductionType in DeductionTypedata)
                {
                    DeductionTypeVM deductionTypeVM = new DeductionTypeVM();
                    deductionTypeVM.DeductionID = DeductionType.DeductionID;
                    deductionTypeVM.DeductionRate = DeductionType.DeductionRate < 0 ? 0 : DeductionType.DeductionRate;
                    deductionTypeVM.DeductionAmount = DeductionType.DeductionAmount < 0 ? 0 : DeductionType.DeductionAmount;
                    deductionTypeVM.DeductionName = string.IsNullOrEmpty(DeductionType.DeductionName) ? "" : DeductionType.DeductionName;
                    deductionTypeVM.BusinessUnit = DeductionType.BusinessUnit < 0 ? 0 : DeductionType.BusinessUnit;
                    DeductionTypedataList.Add(deductionTypeVM);
                }

                return DeductionTypedataList;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<PremiumHolderTypeVM> getAllPremiumHolderType()
        {
            try
            {
                var PremiumHolderTypeTypedata = unitOfWork.TblPremiumHolderTypeRepository.Get().ToList();

                List<PremiumHolderTypeVM> PremiumHolderTypedataList = new List<PremiumHolderTypeVM>();

                foreach (var PremiumHolderType in PremiumHolderTypeTypedata)
                {
                    PremiumHolderTypeVM premiumHolderTypeVM = new PremiumHolderTypeVM();
                    premiumHolderTypeVM.PremiumHolderTypeID = PremiumHolderType.PremiumHolderTypeID;
                    premiumHolderTypeVM.PremiumHolderType = string.IsNullOrEmpty(PremiumHolderType.PremiumHolderType) ? "" : PremiumHolderType.PremiumHolderType;
                    PremiumHolderTypedataList.Add(premiumHolderTypeVM);
                }

                return PremiumHolderTypedataList;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<AgeWisePremiumVM> getAllAgeWisePremium()
        {
            try
            {
                var AgeWisePremiumdata = unitOfWork.TblAgeWisePremiumRepository.Get().ToList();

                List<AgeWisePremiumVM> AgeWisePremiumList = new List<AgeWisePremiumVM>();

                foreach (var AgeWisePremium in AgeWisePremiumdata)
                {
                    AgeWisePremiumVM ageWisePremiumVM = new AgeWisePremiumVM();
                    ageWisePremiumVM.AgeID = AgeWisePremium.AgeID;
                    ageWisePremiumVM.FromDate = AgeWisePremium.FromDate > 0 ? AgeWisePremium.FromDate : 0;
                    ageWisePremiumVM.ToDate = AgeWisePremium.ToDate > 0 ? AgeWisePremium.ToDate : 0;
                    ageWisePremiumVM.PremiumValue = AgeWisePremium.PremiumValue > 0 ? AgeWisePremium.PremiumValue : 0;
                    ageWisePremiumVM.PremiumID = AgeWisePremium.PremiumID > 0 ? AgeWisePremium.PremiumID : 0;

                    AgeWisePremiumList.Add(ageWisePremiumVM);
                }

                return AgeWisePremiumList;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<PilotPremiumVM> getAllPilotPremium()
        {
            try
            {
                var PilotPremiumdata = unitOfWork.TblPilotPremiumRepository.Get().ToList();

                List<PilotPremiumVM> PilotPremiumList = new List<PilotPremiumVM>();

                foreach (var pilotPremium in PilotPremiumdata)
                {
                    PilotPremiumVM pilotPremiumVM = new PilotPremiumVM();
                    pilotPremiumVM.PremiumID = pilotPremiumVM.PremiumID;
                    pilotPremiumVM.DedctibleType = pilotPremium.DedctibleType > 0 ? pilotPremium.DedctibleType : 0;
                    pilotPremiumVM.PremiumType = pilotPremium.PremiumType > 0 ? pilotPremium.PremiumType : 0;
                    pilotPremiumVM.Premium = pilotPremium.Premium > 0 ? pilotPremium.Premium : 0;
                    pilotPremiumVM.PremiumName = string.IsNullOrEmpty(pilotPremium.PremiumName) ? pilotPremium.PremiumName : "";

                    PilotPremiumList.Add(pilotPremiumVM);
                }

                return PilotPremiumList;

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public List<RelatioshipVM> getAllRelatioship()
        {
            try
            {
                var relationshipdata = unitOfWork.TblrelationshipRepository.Get().ToList();

                List<RelatioshipVM> relationshipdataList = new List<RelatioshipVM>();

                foreach (var rela in relationshipdata)
                {
                    RelatioshipVM relatioshipVM = new RelatioshipVM();
                    relatioshipVM.RelationShipID = rela.RelationShipID;
                    relatioshipVM.Relationship = rela.Relationship;

                    relationshipdataList.Add(relatioshipVM);
                }

                return relationshipdataList;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<GenderVM> getAllGender()
        {
            try
            {
                var genData = unitOfWork.TblGenderRepository.Get().ToList();

                List<GenderVM> TitleList = new List<GenderVM>();

                foreach (var title in genData)
                {
                    GenderVM genderVM = new GenderVM();
                    genderVM.GenderID = title.GenderID;
                    genderVM.Gender = title.Gender;

                    TitleList.Add(genderVM);
                }

                return TitleList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<PremiumVM> GetPremium(int BussinusID)
        {
            try
            {
                var Data = unitOfWork.TblPremiumRepository.Get(x=>x.BUID== BussinusID).ToList();

               var  PremiumVMList = new List<PremiumVM>();

                foreach (var PrM in Data)
                {
                    var PData = new PremiumVM();
                    PData.PremiumID = PrM.PremiumID;
                    PData.PremiumCode = PrM.PremiumCode;
                    PData.PremiumName = PrM.PremiumName;


                    PremiumVMList.Add(PData);
                }

                return PremiumVMList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PremiumVM GetPremiumByID(int id)
        {
            try
            {


                var Data = unitOfWork.TblPremiumRepository.GetByID(id);

                var PremiumVMList = new PremiumVM();


                PremiumVMList.PremiumID = Data.PremiumID;
                PremiumVMList.PremiumCode = Data.PremiumCode;
                PremiumVMList.PremiumName = Data.PremiumName;


               
               

                return PremiumVMList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool SavePremium(PremiumVM clientRequestVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {

                    var Data = unitOfWork.TblPremiumRepository.GetByID(clientRequestVM.PremiumID);

                    int clientID = 0;

                    if (Data == null)
                    {

                        //Save Client
                        var client = new tblPremium();
                        client.PremiumName = clientRequestVM.PremiumName;
                        client.PremiumCode = clientRequestVM.PremiumCode;
                        client.BUID = clientRequestVM.BUID;
                        unitOfWork.TblPremiumRepository.Insert(client);
                        unitOfWork.Save();

                    }
                    else
                    {

                        
                        Data.PremiumName = clientRequestVM.PremiumName;
                        Data.PremiumCode = clientRequestVM.PremiumCode;
                        Data.BUID = clientRequestVM.BUID;
                        unitOfWork.TblPremiumRepository.Update(Data);
                        unitOfWork.Save();

                    }


                       
                    



                   
                    

                    
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




        public List<PilotPRMVM> getAllPilotPremum()
        {
            try
            {
                var genData = unitOfWork.TblPilotPRMRepository.Get().ToList();

                List<PilotPRMVM> TitleList = new List<PilotPRMVM>();

                foreach (var title in genData)
                {
                    PilotPRMVM genderVM = new PilotPRMVM();
                    genderVM.PID = title.PID;
                    genderVM.Description = title.Description;

                    TitleList.Add(genderVM);
                }

                return TitleList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<MemberStatusVM> getStatus()
        {
            try
            {
                var genData = unitOfWork.TblMemberStatu.Get().ToList();

                List<MemberStatusVM> SList = new List<MemberStatusVM>();

                foreach (var title in genData)
                {
                    MemberStatusVM genderVM = new MemberStatusVM();
                    genderVM.StatusID = title.StatusID;
                    genderVM.Status = title.Status;

                    SList.Add(genderVM);
                }

                return SList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<HospitalVM> getAllHospitals()
        {
            try
            {
                var HospitalData = unitOfWork.TblHospitalsRepository.Get().ToList();

                List<HospitalVM> HospitalList = new List<HospitalVM>();

                foreach (var hospital in HospitalData)
                {
                    HospitalVM HospitalVM = new HospitalVM();
                    HospitalVM.HospitalID = hospital.HospitalID;
                    HospitalVM.HospitalName = hospital.HospitalName;
                    HospitalVM.CountryID =(int)hospital.CountryID;
                    HospitalList.Add(HospitalVM);
                }

                return HospitalList;
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<TitleVM> getAllTitles()
        {
            try
            {
                var TitleData = unitOfWork.TblTitleRepository.Get().ToList();

                List<TitleVM> TitleList = new List<TitleVM>();

                foreach (var title in TitleData)
                {
                    TitleVM titleVM = new TitleVM();
                    titleVM.TitleID = title.TitleID;
                    titleVM.TitleName = title.TitleName;

                    TitleList.Add(titleVM);
                }

                return TitleList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<DistrictVM> getAllDistrict()
        {
            try
            {
                var DistrictData = unitOfWork.TblDistrictInfoRepository.Get().ToList();

                List<DistrictVM> DistrictList = new List<DistrictVM>();

                foreach (var district in DistrictData)
                {
                    DistrictVM districtVM = new DistrictVM();
                    districtVM.DistrictId = district.DistrictId;
                    districtVM.Description = district.Description;

                    DistrictList.Add(districtVM);
                }

                return DistrictList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CountryVM> GetAllCountries()
        {
            try
            {
                var countryData = unitOfWork.TblCountryRepository.Get().ToList();

                List<CountryVM> countryList = new List<CountryVM>();

                foreach (var country in countryData)
                {
                    CountryVM countryVM = new CountryVM();
                    countryVM.CountryID = country.CountryID;
                    countryVM.CountryName = country.CountryName;

                    countryList.Add(countryVM);
                }

                return countryList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public CountryVM GetCountryByID(int countryID)
        {
            try
            {
                var countryData = unitOfWork.TblCountryRepository.GetByID(countryID);

                CountryVM countryVM = new CountryVM();
                countryVM.CountryID = countryData.CountryID;
                countryVM.CountryName = countryData.CountryName;

                return countryVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CurrencyVM> GetAllCurrencies()
        {
            try
            {
                var currencyData = unitOfWork.TblCurrencyRepository.Get().ToList();

                List<CurrencyVM> currencyList = new List<CurrencyVM>();

                foreach (var currency in currencyData)
                {
                    CurrencyVM currencyVM = new CurrencyVM();
                    currencyVM.CurrencyID = currency.CurrencyID;
                    currencyVM.CountryID = currency.CountryID;
                    currencyVM.CurrencyName = currency.CurrencyName;
                    currencyVM.CurrencyCode = currency.CurrencyCode;
                    currencyVM.BUID = (int)currency.BUID;
                    currencyList.Add(currencyVM);
                }

                return currencyList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public CurrencyVM GetCurrencyByID(int currencyID)
        {
            try
            {
                var currencyData = unitOfWork.TblCurrencyRepository.GetByID(currencyID);

                CurrencyVM currencyVM = new CurrencyVM();
                currencyVM.CurrencyID = currencyData.CurrencyID;
                currencyVM.CountryID = currencyData.CountryID;
                currencyVM.CurrencyName = currencyData.CurrencyName;
                currencyVM.CurrencyCode = currencyData.CurrencyCode;

                return currencyVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public CurrencyVM GetCurrencyByCountryID(int countryID)
        {
            try
            {
                var currencyData = unitOfWork.TblCurrencyRepository.Get(x => x.CountryID == countryID).FirstOrDefault();

                CurrencyVM currencyVM = new CurrencyVM();

                if (currencyData != null)
                {
                    currencyVM.CurrencyID = currencyData.CurrencyID;
                    currencyVM.CountryID = currencyData.CountryID;
                    currencyVM.CurrencyName = currencyData.CurrencyName;
                    currencyVM.CurrencyCode = currencyData.CurrencyCode;
                }

                return currencyVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
