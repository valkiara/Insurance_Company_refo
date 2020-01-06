using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Repository
{
    public class UnitOfWork : IDisposable
    {

        //31-12-2018
        private PERFECTIBSEntities context = new PERFECTIBSEntities();

        #region Admin Data
        //Master Data Needed
        private GenericRepository<tblAccessLevelType> tblAccessLevelTypeRepository;
        //Master Data Needed
        private GenericRepository<tblFunctionType> tblFunctionTypeRepository;
        //Master Data Needed
        private GenericRepository<tblFunction> tblFunctionRepository;
        //Development Needed
        private GenericRepository<tblFunctionAccess> tblFunctionAccessRepository;

        #endregion

        #region Master Data
        //Development Needed
        private GenericRepository<tblUser> tblUserRepository;
        //Master Data Needed
        private GenericRepository<tblCompany> tblCompanyRepository;
        private GenericRepository<tblBussinessUnit> tblBussinessUnitRepository;
        private GenericRepository<tblInsCompany> tblInsCompanyRepository;
        private GenericRepository<tblAgent> tblAgentRepository;
        private GenericRepository<tblAccountHandler> tblACHandlerRepository;
        private GenericRepository<tblCommisionStructureHeader> tblCommisionStructureHeaderRepository;
        private GenericRepository<tblCommisionStructureLine> tblCommisionStructureLineRepository;
        private GenericRepository<tblComCommisionStructureLine> tblComCommisionStructureLineRepository;
        private GenericRepository<tblInsComCommisionStructureHeader> tblInsComCommisionStructureHeaderRepository;
        //Master Data Needed
        private GenericRepository<tblCountry> tblCountryRepository;
        //Master Data Needed
        private GenericRepository<tblCurrency> tblCurrencyRepository;
        private GenericRepository<tblDesignation> tblDesignationRepository;
        private GenericRepository<tblInsClass> tblInsClassRepository;
        private GenericRepository<tblInsSubClass> tblInsSubClassRepository;
        private GenericRepository<tblDocument> tblDocumentRepository;
        private GenericRepository<tblEmployee> tblEmployeeRepository;
        private GenericRepository<tblDocCategory> tblDocCategoryRepository;
        private GenericRepository<tblRequiredDocument> tblRequiredDocumentRepository;
        //Master Data Needed
        private GenericRepository<tblCommisionType> tblCommisionTypeRepository;
        private GenericRepository<tblCommonInsScope> tblCommonInsScopeRepository;
        private GenericRepository<tblIntroducer> tblIntroducerRepository;
        private GenericRepository<tblIntroducerBusinessUnit> tblIntroducerBusinessUnitRepository;
        private GenericRepository<tblSetting> tblSettingRepository;
        private GenericRepository<tblPartner> tblPartnerRepository;
        private GenericRepository<tblPartnerMapping> tblPartnerMappingRepository;
        private GenericRepository<tblRateCategory> tblRateCategoryRepository;
        private GenericRepository<tblPolicyCategory> tblPolicyCategoryRepository;
        private GenericRepository<tblPolicy> tblPolicyRepository;
        private GenericRepository<tblTransactionType> tblTransactionTypeRepository;
        private GenericRepository<tblLoadingType> tblLoadingTypeRepository;
        private GenericRepository<tblInternalPolicyNumSetup> tblInternalPolicyNumSetupRepository;
        private GenericRepository<tblTaxType> tblTaxTypeRepository;
        private GenericRepository<tblClaimRejectReason> tblClaimRejectReasonRepository;
        private GenericRepository<tblClaimReOpenReason> tblClaimReOpenReasonRepository;
        private GenericRepository<tblCauseOfLoss> tblCauseOfLossRepository;
        private GenericRepository<tblDistrict> tblDistrictRepository;
        private GenericRepository<tblClaimStatu> tbltblClaimStatuRepository;
        private GenericRepository<tblYear> tblYearRepository;
        private GenericRepository<tblInsClassType> tblInsClassTypeRepository;
        //private GenericRepository<tblInsClassType> tblClassTypeRepository;
        private GenericRepository<Status> StatusRepository;
        private GenericRepository<UploadedFile> UploadFileRepository;
        private GenericRepository<tblTitle> tblTitleRepository;
        private GenericRepository<tblHospitals> tblHospitalsRepository;
        private GenericRepository<tblRelationship> tblrelationshipRepository;
        private GenericRepository<tblGender> tblGenderRepository;
        public GenericRepository<tblDeductionType> tblDeductionTypeRepository;
        public GenericRepository<tblPremiumHolderType> tblPremiumHolderTypeRepository;
        public GenericRepository<tblAgeWisePremium> tblAgeWisePremiumRepository;
        public GenericRepository<tblPilotPremium> tblPilotPremiumRepository;
        public GenericRepository<tblWBN> tblBUFAFrequncyRepository;
        public GenericRepository<MemberStatu> tblStatus;
        public GenericRepository<tblPilotPRM> tblPilotPRMRepository;
        public GenericRepository<tblFrequncyDetail> tblFrequncyDetailRepository;
        #endregion

        #region Transaction Data
        private GenericRepository<tblClient> tblClientRepository;
        private GenericRepository<tblGrpFamilyDetail> tblGrpFamilyDetailRepository;
        private GenericRepository<tblClientRequestHeader> tblClientRequestHeaderRepository;
        private GenericRepository<tblClientRequestLine> tblClientRequestLineRepository;
        private GenericRepository<tblClientProperty> tblClientPropertyRepository;
        private GenericRepository<tblClientRequestInsSubClassScope> tblClientRequestInsSubClassScopeRepository;
        private GenericRepository<tblQuotationHeader> tblQuotationHeaderRepository;
        private GenericRepository<tblQuotationLine> tblQuotationLineRepository;
        private GenericRepository<tblQuotationRequestedInsCompany> tblQuotationRequestedInsCompanyRepository;
        private GenericRepository<tblQuotationDetailsInsCompanyHeader> tblQuotationDetailsInsCompanyHeaderRepository;
        private GenericRepository<tblQuotationDetailsInsCompanyLine> tblQuotationDetailsInsCompanyLineRepository;
        private GenericRepository<tblQuotationDetailsInsCompanyScope> tblQuotationDetailsInsCompanyScopeRepository;
        private GenericRepository<tblCommissionFormat> tblCommissionFormatRepository;
        // private GenericRepository<tblQuotationInsClassScope> tblQuotationInsClassScopeRepository;
        //Master Data Needed
        private GenericRepository<tblQuotationStatu> tblQuotationStatusRepository;
        private GenericRepository<tblCoverNote> tblCoverNoteRepository;
        private GenericRepository<tblPolicyInformationRecording> tblPolicyInformationRecordingRepository;
        private GenericRepository<tblPolicyCommisionPayment> tblPolicyCommisionPaymentRepository;
        private GenericRepository<tblPolicyRenewalHistory> tblPolicyRenewalHistoryRepository;
        private GenericRepository<tblClaimRecording> tblClaimRecordingRepository;
        private GenericRepository<tblClaimRecHistory> tblClaimRecHistoryRepository;
        private GenericRepository<tblClaimRecPendingDoc> tblClaimRecPendingDocRepository;
        private GenericRepository<tblClaimPayment> tblClaimPaymentRepository;
        private GenericRepository<tblPaymentType> tblPaymentTypeRepository;
        private GenericRepository<tblClaimPaymentMethod> tblClaimPaymentMethodRepository;
        private GenericRepository<tblChargeType> tblChargeTypeRepository;
        private GenericRepository<tblDebitNote> tblDebitNoteRepository;
        private GenericRepository<tblPolicyInfoPayment> tblPolicyInfoPaymentRepository;
        private GenericRepository<tblPolicyCommisionPayment_New> tblPolicyCommisionPayment_NewRepository;
        private GenericRepository<tblPolicyInforCharge_New> tblPolicyInforCharge_NewRepository;
        private GenericRepository<tblPolicyInfoCharge> tblPolicyInfoChargeRepository;
        private GenericRepository<tblPolicyDebitNote> tblPolicyDebitNoteRepository;
        private GenericRepository<tblPayment> tblPaymentRepository;        private GenericRepository<tblPremium> tblPremiumRepository;

        private GenericRepository<tblFamilyMember> tblFamilyMemberRepository;
        private GenericRepository<tblFamilyDiscount> tblFamilyDiscountRepository;
        private GenericRepository<tblBankDetail> tblBankDetailRepository;
        private GenericRepository<tblBankTransactionDetail> tblBankTransactionDetailRepository;
        private GenericRepository<tblDeduction> tblDeductionRepository;
        private GenericRepository<tblPolicyInformationBUPA> tblPolicyInformationBUPARepository;
        private GenericRepository<tblPolicyMemberDetail> tblPolicyMemberDetailRepository;
        private GenericRepository<PatientAdmission> tblPatientAdmission;
        private GenericRepository<SingaporeAdmission> tblSingaporeAdmission;
        private GenericRepository<tblEndorsementInfo> tblEndorsementInfo;
        public GenericRepository<tblDeductionsLine> tblDeductionsLine;
        public GenericRepository<SingaporeInvoice> tblSingaporeInvoice;
        public GenericRepository<tblReceivedQuotation> tblReceivedQuotationRepository;
        private GenericRepository<Admission_Singapore> tblAdmissionSingapore;
        private GenericRepository<tblClientRewenelHistory> tblClientRHistoryRepository;
        private GenericRepository<tblClientRnlRequestHeader> tblClientRenewelRequestHeaderRepository;
        #endregion

        #region Admin Data

        public GenericRepository<tblTitle> TblTitleRepository
        {
            get
            {
                if(this.tblTitleRepository==null)
                {
                    this.tblTitleRepository = new GenericRepository<tblTitle>(context);
                }
                return tblTitleRepository;
            }
        }
        public GenericRepository<tblClientRnlRequestHeader> TblClientRenewelRequestHeaderRepository
        {
            get
            {
                if (this.tblClientRenewelRequestHeaderRepository == null)
                {
                    this.tblClientRenewelRequestHeaderRepository = new GenericRepository<tblClientRnlRequestHeader>(context);
                }
                return tblClientRenewelRequestHeaderRepository;
            }
        }


        


   public GenericRepository<tblFrequncyDetail> TblFrequncyDetailRepository
        {
            get
            {
                if (this.tblFrequncyDetailRepository == null)
                {
                    this.tblFrequncyDetailRepository = new GenericRepository<tblFrequncyDetail>(context);
                }
                return tblFrequncyDetailRepository;
            }
        }



        public GenericRepository<tblClientRewenelHistory> TblClientRHistoryRepository
        {
            get
            {
                if (this.tblClientRHistoryRepository == null)
                {
                    this.tblClientRHistoryRepository = new GenericRepository<tblClientRewenelHistory>(context);
                }
                return tblClientRHistoryRepository;
            }
        }


        public GenericRepository<tblPilotPRM> TblPilotPRMRepository
        {
            get
            {
                if (this.tblPilotPRMRepository == null)
                {
                    this.tblPilotPRMRepository = new GenericRepository<tblPilotPRM>(context);
                }
                return tblPilotPRMRepository;
            }
        }
        public GenericRepository<tblInsClassType> TblInsClassTypeUnitRepository
        {
            get
            {
                if (this.tblInsClassTypeRepository == null)
                {
                    this.tblInsClassTypeRepository = new GenericRepository<tblInsClassType>(context);
                }
                return tblInsClassTypeRepository;

            }
        }

        

        public GenericRepository<Status> StatusDetailsRepository
        {
            get
            {
                if (this.StatusRepository == null)
                {
                    this.StatusRepository = new GenericRepository<Status>(context);
                }
                return StatusRepository;
            }
        }

        public GenericRepository<UploadedFile> UploadedFileRepository
        {
             get
            {
                if (this.UploadFileRepository == null)
                {
                    this.UploadFileRepository = new GenericRepository<UploadedFile>(context);
                }
                return UploadFileRepository;
            }
        }




        public  GenericRepository<tblInsClassType> TbtblInsClassTypelRepository
        {
            get
            {
                if (this.tblInsClassTypeRepository == null)
                {
                    this.tblInsClassTypeRepository = new GenericRepository<tblInsClassType>(context);
                }
                return tblInsClassTypeRepository;
            }
        }

        public GenericRepository<tblClaimStatu> TblClaimStatuRepository
        {
            get
            {
                if (this.tbltblClaimStatuRepository == null)
                {
                    this.tbltblClaimStatuRepository = new GenericRepository<tblClaimStatu>(context);
                }
                return tbltblClaimStatuRepository;
            }
        }

        public GenericRepository<tblYear> TblYearRepository
        {
            get
            {
                if (this.tblYearRepository == null)
                {
                    this.tblYearRepository = new GenericRepository<tblYear>(context);
                }
                return tblYearRepository;
            }
        }

        public GenericRepository<tblDistrict> TblDistrictInfoRepository
        {
            get
            {
                if(this.tblDistrictRepository==null)
                {
                    this.tblDistrictRepository = new GenericRepository<tblDistrict>(context);
                }
                return tblDistrictRepository;
            }
        }

        public GenericRepository<tblAccessLevelType> TblAccessLevelTypeRepository
        {
            get
            {
                if (this.tblAccessLevelTypeRepository == null)
                {
                    this.tblAccessLevelTypeRepository = new GenericRepository<tblAccessLevelType>(context);
                }
                return tblAccessLevelTypeRepository;
            }
        }

        public GenericRepository<tblFunctionType> TblFunctionTypeRepository
        {
            get
            {
                if (this.tblFunctionTypeRepository == null)
                {
                    this.tblFunctionTypeRepository = new GenericRepository<tblFunctionType>(context);
                }
                return tblFunctionTypeRepository;
            }
        }

        public GenericRepository<tblFunction> TblFunctionRepository
        {
            get
            {
                if (this.tblFunctionRepository == null)
                {
                    this.tblFunctionRepository = new GenericRepository<tblFunction>(context);
                }
                return tblFunctionRepository;
            }
        }

        public GenericRepository<tblFunctionAccess> TblFunctionAccessRepository
        {
            get
            {
                if (this.tblFunctionAccessRepository == null)
                {
                    this.tblFunctionAccessRepository = new GenericRepository<tblFunctionAccess>(context);
                }
                return tblFunctionAccessRepository;
            }
        }
        #endregion

        #region Master Data


        public GenericRepository<tblPilotPremium> TblPilotPremiumRepository
        {
            get
            {
                if (this.tblPilotPremiumRepository == null)
                {
                    this.tblPilotPremiumRepository = new GenericRepository<tblPilotPremium>(context);
                }
                return tblPilotPremiumRepository;
            }
        }
        public GenericRepository<tblDeductionType> TblDeductionTypeRepository
        {
            get
            {
                if (this.tblDeductionTypeRepository == null)
                {
                    this.tblDeductionTypeRepository = new GenericRepository<tblDeductionType>(context);
                }
                return tblDeductionTypeRepository;
            }
        }

        public GenericRepository<tblPremiumHolderType> TblPremiumHolderTypeRepository
        {
            get
            {
                if (this.tblPremiumHolderTypeRepository == null)
                {
                    this.tblPremiumHolderTypeRepository = new GenericRepository<tblPremiumHolderType>(context);
                }
                return tblPremiumHolderTypeRepository;
            }
        }

     

        public GenericRepository<tblAgeWisePremium> TblAgeWisePremiumRepository
        {
            get
            {
                if (this.tblAgeWisePremiumRepository == null)
                {
                    this.tblAgeWisePremiumRepository = new GenericRepository<tblAgeWisePremium>(context);
                }
                return tblAgeWisePremiumRepository;
            }
        }


        public GenericRepository<tblRelationship> TblrelationshipRepository
        {
            get
            {
                if (this.tblrelationshipRepository == null)
                {
                    this.tblrelationshipRepository = new GenericRepository<tblRelationship>(context);
                }
                return tblrelationshipRepository;
            }
        }

        public GenericRepository<tblGender> TblGenderRepository
        {
            get
            {
                if (this.tblGenderRepository == null)
                {
                    this.tblGenderRepository = new GenericRepository<tblGender>(context);
                }
                return tblGenderRepository;
            }
        }
        public GenericRepository<MemberStatu> TblMemberStatu
        {
            get
            {
                if (this.tblStatus == null)
                {
                    this.tblStatus = new GenericRepository<MemberStatu>(context);
                }
                return tblStatus;
            }
        }

        public GenericRepository<tblHospitals> TblHospitalsRepository
        {
            get
            {
                if (this.tblHospitalsRepository == null)
                {
                    this.tblHospitalsRepository = new GenericRepository<tblHospitals>(context);
                }
                return tblHospitalsRepository;
            }
        }
        public GenericRepository<tblUser> TblUserRepository
        {
            get
            {
                if (this.tblUserRepository == null)
                {
                    this.tblUserRepository = new GenericRepository<tblUser>(context);
                }
                return tblUserRepository;
            }
        }

        public GenericRepository<tblCompany> TblCompanyRepository
        {
            get
            {
                if (this.tblCompanyRepository == null)
                {
                    this.tblCompanyRepository = new GenericRepository<tblCompany>(context);
                }
                return tblCompanyRepository;
            }
        }

        public GenericRepository<tblBussinessUnit> TblBussinessUnitRepository
        {
            get
            {
                if (this.tblBussinessUnitRepository == null)
                {
                    this.tblBussinessUnitRepository = new GenericRepository<tblBussinessUnit>(context);
                }
                return tblBussinessUnitRepository;
            }
        }

        public GenericRepository<tblInsCompany> TblInsCompanyRepository
        {
            get
            {
                if (this.tblInsCompanyRepository == null)
                {
                    this.tblInsCompanyRepository = new GenericRepository<tblInsCompany>(context);
                }
                return tblInsCompanyRepository;
            }
        }

        public GenericRepository<tblAgent> TblAgentRepository
        {
            get
            {
                if (this.tblAgentRepository == null)
                {
                    this.tblAgentRepository = new GenericRepository<tblAgent>(context);
                }
                return tblAgentRepository;
            }
        }

        public GenericRepository<tblInsComCommisionStructureHeader> TblInsCommisionStructureHeaderRepository
        {
            get
            {
                if (this.tblInsComCommisionStructureHeaderRepository == null)
                {
                    this.tblInsComCommisionStructureHeaderRepository = new GenericRepository<tblInsComCommisionStructureHeader>(context);
                }
                return tblInsComCommisionStructureHeaderRepository;
            }
        }

        public GenericRepository<tblComCommisionStructureLine> TblInsCommisionStructureLineRepository
        {
            get
            {
                if (this.tblComCommisionStructureLineRepository == null)
                {
                    this.tblComCommisionStructureLineRepository = new GenericRepository<tblComCommisionStructureLine>(context);
                }
                return tblComCommisionStructureLineRepository;
            }
        }

        public GenericRepository<tblAccountHandler> TblAccountHandlerRepository
        {
            get
            {
                if (this.tblACHandlerRepository == null)
                {
                    this.tblACHandlerRepository = new GenericRepository<tblAccountHandler>(context);
                }
                return tblACHandlerRepository;
            }
        }



        public GenericRepository<tblCommisionStructureHeader> TblCommisionStructureHeaderRepository
        {
            get
            {
                if (this.tblCommisionStructureHeaderRepository == null)
                {
                    this.tblCommisionStructureHeaderRepository = new GenericRepository<tblCommisionStructureHeader>(context);
                }
                return tblCommisionStructureHeaderRepository;
            }
        }

        public GenericRepository<tblCommisionStructureLine> TblCommisionStructureLineRepository
        {
            get
            {
                if (this.tblCommisionStructureLineRepository == null)
                {
                    this.tblCommisionStructureLineRepository = new GenericRepository<tblCommisionStructureLine>(context);
                }
                return tblCommisionStructureLineRepository;
            }
        }

        public GenericRepository<tblCountry> TblCountryRepository
        {
            get
            {
                if (this.tblCountryRepository == null)
                {
                    this.tblCountryRepository = new GenericRepository<tblCountry>(context);
                }
                return tblCountryRepository;
            }
        }
        public GenericRepository<tblReceivedQuotation> TblReceivedQuotationRepository
        {
            get
            {
                if (this.tblReceivedQuotationRepository == null)
                {
                    this.tblReceivedQuotationRepository = new GenericRepository<tblReceivedQuotation>(context);
                }
                return tblReceivedQuotationRepository;
            }
        }
        public GenericRepository<tblFamilyMember> TblFamilyMemberRepository
        {
            get
            {
                if (this.tblFamilyMemberRepository == null)
                {
                    this.tblFamilyMemberRepository = new GenericRepository<tblFamilyMember>(context);
                }
                return tblFamilyMemberRepository;
            }
        }
        public GenericRepository<tblDeduction> TblDeductionRepository
        {
            get
            {
                if (this.tblDeductionRepository == null)
                {
                    this.tblDeductionRepository = new GenericRepository<tblDeduction>(context);
                }
                return tblDeductionRepository;
            }
        }
        public GenericRepository<tblPolicyInformationBUPA> TblPolicyInformationBUPARepository
        {
            get
            {
                if (this.tblPolicyInformationBUPARepository == null)
                {
                    this.tblPolicyInformationBUPARepository = new GenericRepository<tblPolicyInformationBUPA>(context);
                }
                return tblPolicyInformationBUPARepository;
            }
        }
        public GenericRepository<tblPolicyMemberDetail> TblPolicyMemberDetailRepository
        {
            get
            {
                if (this.tblPolicyMemberDetailRepository == null)
                {
                    this.tblPolicyMemberDetailRepository = new GenericRepository<tblPolicyMemberDetail>(context);
                }
                return tblPolicyMemberDetailRepository;
            }
        }

        public GenericRepository<tblBankTransactionDetail> TblBankTransactionDetailRepository
        {
            get
            {
                if (this.tblBankTransactionDetailRepository == null)
                {
                    this.tblBankTransactionDetailRepository = new GenericRepository<tblBankTransactionDetail>(context);
                }
                return tblBankTransactionDetailRepository;
            }
        }


        public GenericRepository<tblCurrency> TblCurrencyRepository
        {
            get
            {
                if (this.tblCurrencyRepository == null)
                {
                    this.tblCurrencyRepository = new GenericRepository<tblCurrency>(context);
                }
                return tblCurrencyRepository;
            }
        }

        public GenericRepository<tblDesignation> TblDesignationRepository
        {
            get
            {
                if (this.tblDesignationRepository == null)
                {
                    this.tblDesignationRepository = new GenericRepository<tblDesignation>(context);
                }
                return tblDesignationRepository;
            }
        }

        public GenericRepository<tblBankDetail> TblBankDetailRepository
        {
            get
            {
                if (this.tblBankDetailRepository == null)
                {
                    this.tblBankDetailRepository = new GenericRepository<tblBankDetail>(context);
                }
                return tblBankDetailRepository;
            }
        }

        public GenericRepository<tblInsClass> TblInsClassRepository
        {
            get
            {
                if (this.tblInsClassRepository == null)
                {
                    this.tblInsClassRepository = new GenericRepository<tblInsClass>(context);
                }
                return tblInsClassRepository;
            }
        }

        public GenericRepository<tblInsSubClass> TblInsSubClassRepository
        {
            get
            {
                if (this.tblInsSubClassRepository == null)
                {
                    this.tblInsSubClassRepository = new GenericRepository<tblInsSubClass>(context);
                }
                return tblInsSubClassRepository;
            }
        }

        public GenericRepository<tblDocument> TblDocumentRepository
        {
            get
            {
                if (this.tblDocumentRepository == null)
                {
                    this.tblDocumentRepository = new GenericRepository<tblDocument>(context);
                }
                return tblDocumentRepository;
            }
        }

        public GenericRepository<tblEmployee> TblEmployeeRepository
        {
            get
            {
                if (this.tblEmployeeRepository == null)
                {
                    this.tblEmployeeRepository = new GenericRepository<tblEmployee>(context);
                }
                return tblEmployeeRepository;
            }
        }

        public GenericRepository<tblDocCategory> TblDocCategoryRepository
        {
            get
            {
                if (this.tblDocCategoryRepository == null)
                {
                    this.tblDocCategoryRepository = new GenericRepository<tblDocCategory>(context);
                }
                return tblDocCategoryRepository;
            }
        }

        public GenericRepository<tblRequiredDocument> TblRequiredDocumentRepository
        {
            get
            {
                if (this.tblRequiredDocumentRepository == null)
                {
                    this.tblRequiredDocumentRepository = new GenericRepository<tblRequiredDocument>(context);
                }
                return tblRequiredDocumentRepository;
            }
        }

        public GenericRepository<tblCommisionType> TblCommisionTypeRepository
        {
            get
            {
                if (this.tblCommisionTypeRepository == null)
                {
                    this.tblCommisionTypeRepository = new GenericRepository<tblCommisionType>(context);
                }
                return tblCommisionTypeRepository;
            }
        }

        public GenericRepository<tblCommonInsScope> TblCommonInsScopeRepository
        {
            get
            {
                if (this.tblCommonInsScopeRepository == null)
                {
                    this.tblCommonInsScopeRepository = new GenericRepository<tblCommonInsScope>(context);
                }
                return tblCommonInsScopeRepository;
            }
        }

        public GenericRepository<tblIntroducer> TblIntroducerRepository
        {
            get
            {
                if (this.tblIntroducerRepository == null)
                {
                    this.tblIntroducerRepository = new GenericRepository<tblIntroducer>(context);
                }
                return tblIntroducerRepository;
            }
        }

        public GenericRepository<tblIntroducerBusinessUnit> TblIntroducerBusinessUnitRepository
        {
            get
            {
                if (this.tblIntroducerBusinessUnitRepository == null)
                {
                    this.tblIntroducerBusinessUnitRepository = new GenericRepository<tblIntroducerBusinessUnit>(context);
                }
                return tblIntroducerBusinessUnitRepository;
            }
        }

        public GenericRepository<tblSetting> TblSettingRepository
        {
            get
            {
                if (this.tblSettingRepository == null)
                {
                    this.tblSettingRepository = new GenericRepository<tblSetting>(context);
                }
                return tblSettingRepository;
            }
        }

        public GenericRepository<tblPartner> TblPartnerRepository
        {
            get
            {
                if (this.tblPartnerRepository == null)
                {
                    this.tblPartnerRepository = new GenericRepository<tblPartner>(context);
                }
                return tblPartnerRepository;
            }
        }

        public GenericRepository<tblPartnerMapping> TblPartnerMappingRepository
        {
            get
            {
                if (this.tblPartnerMappingRepository == null)
                {
                    this.tblPartnerMappingRepository = new GenericRepository<tblPartnerMapping>(context);
                }
                return tblPartnerMappingRepository;
            }
        }

        public GenericRepository<tblRateCategory> TblRateCategoryRepository
        {
            get
            {
                if (this.tblRateCategoryRepository == null)
                {
                    this.tblRateCategoryRepository = new GenericRepository<tblRateCategory>(context);
                }
                return tblRateCategoryRepository;
            }
        }
   
        public GenericRepository<tblPolicyCategory> TblPolicyCategoryRepository
        {
            get
            {
                if (this.tblPolicyCategoryRepository == null)
                {
                    this.tblPolicyCategoryRepository = new GenericRepository<tblPolicyCategory>(context);
                }
                return tblPolicyCategoryRepository;
            }
        }

        public GenericRepository<tblPolicy> TblPolicyRepository
        {
            get
            {
                if (this.tblPolicyRepository == null)
                {
                    this.tblPolicyRepository = new GenericRepository<tblPolicy>(context);
                }
                return tblPolicyRepository;
            }
        }

        public GenericRepository<tblTransactionType> TblTransactionTypeRepository
        {
            get
            {
                if (this.tblTransactionTypeRepository == null)
                {
                    this.tblTransactionTypeRepository = new GenericRepository<tblTransactionType>(context);
                }
                return tblTransactionTypeRepository;
            }
        }

        public GenericRepository<tblLoadingType> TblLoadingTypeRepository
        {
            get
            {
                if (this.tblLoadingTypeRepository == null)
                {
                    this.tblLoadingTypeRepository = new GenericRepository<tblLoadingType>(context);
                }
                return tblLoadingTypeRepository;
            }
        }

        public GenericRepository<tblInternalPolicyNumSetup> TblInternalPolicyNumSetupRepository
        {
            get
            {
                if (this.tblInternalPolicyNumSetupRepository == null)
                {
                    this.tblInternalPolicyNumSetupRepository = new GenericRepository<tblInternalPolicyNumSetup>(context);
                }
                return tblInternalPolicyNumSetupRepository;
            }
        }

        public GenericRepository<tblTaxType> TblTaxTypeRepository
        {
            get
            {
                if (this.tblTaxTypeRepository == null)
                {
                    this.tblTaxTypeRepository = new GenericRepository<tblTaxType>(context);
                }
                return tblTaxTypeRepository;
            }
        }

        public GenericRepository<tblClaimRejectReason> TblClaimRejectReasonRepository
        {
            get
            {
                if (this.tblClaimRejectReasonRepository == null)
                {
                    this.tblClaimRejectReasonRepository = new GenericRepository<tblClaimRejectReason>(context);
                }
                return tblClaimRejectReasonRepository;
            }
        }

        public GenericRepository<tblClaimReOpenReason> TblClaimReOpenReasonRepository
        {
            get
            {
                if (this.tblClaimReOpenReasonRepository == null)
                {
                    this.tblClaimReOpenReasonRepository = new GenericRepository<tblClaimReOpenReason>(context);
                }
                return tblClaimReOpenReasonRepository;
            }
        }

        public GenericRepository<tblCauseOfLoss> TblCauseOfLossRepository
        {
            get
            {
                if (this.tblCauseOfLossRepository == null)
                {
                    this.tblCauseOfLossRepository = new GenericRepository<tblCauseOfLoss>(context);
                }
                return tblCauseOfLossRepository;
            }
        }

        #endregion

        #region Transaction Data
        

        public GenericRepository<tblCommissionFormat> TblCommissionFormatRepository
        {
            get
            {
                if (this.tblCommissionFormatRepository == null)
                {
                    this.tblCommissionFormatRepository = new GenericRepository<tblCommissionFormat>(context);
                }
                return tblCommissionFormatRepository;
            }
        }

        public GenericRepository<tblPolicyInforCharge_New> TblPolicyInforCharge_NewRepository
        {
            get
            {
                if (this.tblPolicyInforCharge_NewRepository == null)
                {
                    this.tblPolicyInforCharge_NewRepository = new GenericRepository<tblPolicyInforCharge_New>(context);
                }
                return tblPolicyInforCharge_NewRepository;
            }
        }
        public GenericRepository<tblPolicyCommisionPayment_New> TblPolicyCommisionPayment_NewRepository
        {
            get
            {
                if (this.tblPolicyCommisionPayment_NewRepository == null)
                {
                    this.tblPolicyCommisionPayment_NewRepository = new GenericRepository<tblPolicyCommisionPayment_New>(context);
                }
                return tblPolicyCommisionPayment_NewRepository;
            }
        }
        public GenericRepository<SingaporeInvoice> TblSingaporeInvoice
        {
            get
            {
                if (this.tblSingaporeInvoice == null)
                {
                    this.tblSingaporeInvoice = new GenericRepository<SingaporeInvoice>(context);
                }
                return tblSingaporeInvoice;
            }
        }
        public GenericRepository<tblEndorsementInfo> TblEndorsementInfo
        {
            get
            {
                if (this.tblEndorsementInfo == null)
                {
                    this.tblEndorsementInfo = new GenericRepository<tblEndorsementInfo>(context);
                }
                return tblEndorsementInfo;
            }
        }
        public GenericRepository<tblClient> TblClientRepository
        {
            get
            {
                if (this.tblClientRepository == null)
                {
                    this.tblClientRepository = new GenericRepository<tblClient>(context);
                }
                return tblClientRepository;
            }
        }

       

        public GenericRepository<tblGrpFamilyDetail> TblGrpFamilyDetailRepository
        {
            get
            {
                if (this.tblGrpFamilyDetailRepository == null)
                {
                    this.tblGrpFamilyDetailRepository = new GenericRepository<tblGrpFamilyDetail>(context);
                }
                return tblGrpFamilyDetailRepository;
            }
        }

        public GenericRepository<tblClientRequestHeader> TblClientRequestHeaderRepository
        {
            get
            {
                if (this.tblClientRequestHeaderRepository == null)
                {
                    this.tblClientRequestHeaderRepository = new GenericRepository<tblClientRequestHeader>(context);
                }
                return tblClientRequestHeaderRepository;
            }
        }

        public GenericRepository<tblClientRequestLine> TblClientRequestLineRepository
        {
            get
            {
                if (this.tblClientRequestLineRepository == null)
                {
                    this.tblClientRequestLineRepository = new GenericRepository<tblClientRequestLine>(context);
                }
                return tblClientRequestLineRepository;
            }
        }

        public GenericRepository<tblClientProperty> TblClientPropertyRepository
        {
            get
            {
                if (this.tblClientPropertyRepository == null)
                {
                    this.tblClientPropertyRepository = new GenericRepository<tblClientProperty>(context);
                }
                return tblClientPropertyRepository;
            }
        }

        public GenericRepository<tblClientRequestInsSubClassScope> TblClientRequestInsSubClassScopeRepository
        {
            get
            {
                if (this.tblClientRequestInsSubClassScopeRepository == null)
                {
                    this.tblClientRequestInsSubClassScopeRepository = new GenericRepository<tblClientRequestInsSubClassScope>(context);
                }
                return tblClientRequestInsSubClassScopeRepository;
            }
        }

        public GenericRepository<tblQuotationHeader> TblQuotationHeaderRepository
        {
            get
            {
                if (this.tblQuotationHeaderRepository == null)
                {
                    this.tblQuotationHeaderRepository = new GenericRepository<tblQuotationHeader>(context);
                }
                return tblQuotationHeaderRepository;
            }
        }

        public GenericRepository<tblQuotationLine> TblQuotationLineRepository
        {
            get
            {
                if (this.tblQuotationLineRepository == null)
                {
                    this.tblQuotationLineRepository = new GenericRepository<tblQuotationLine>(context);
                }
                return tblQuotationLineRepository;
            }
        }

        public GenericRepository<tblQuotationRequestedInsCompany> TblQuotationRequestedInsCompanyRepository
        {
            get
            {
                if (this.tblQuotationRequestedInsCompanyRepository == null)
                {
                    this.tblQuotationRequestedInsCompanyRepository = new GenericRepository<tblQuotationRequestedInsCompany>(context);
                }
                return tblQuotationRequestedInsCompanyRepository;
            }
        }

        public GenericRepository<tblQuotationDetailsInsCompanyHeader> TblQuotationDetailsInsCompanyHeaderRepository
        {
            get
            {
                if (this.tblQuotationDetailsInsCompanyHeaderRepository == null)
                {
                    this.tblQuotationDetailsInsCompanyHeaderRepository = new GenericRepository<tblQuotationDetailsInsCompanyHeader>(context);
                }
                return tblQuotationDetailsInsCompanyHeaderRepository;
            }
        }

        public GenericRepository<tblQuotationDetailsInsCompanyLine> TblQuotationDetailsInsCompanyLineRepository
        {
            get
            {
                if (this.tblQuotationDetailsInsCompanyLineRepository == null)
                {
                    this.tblQuotationDetailsInsCompanyLineRepository = new GenericRepository<tblQuotationDetailsInsCompanyLine>(context);
                }
                return tblQuotationDetailsInsCompanyLineRepository;
            }
        }

        public GenericRepository<tblQuotationDetailsInsCompanyScope> TblQuotationDetailsInsCompanyScopeRepository
        {
            get
            {
                if (this.tblQuotationDetailsInsCompanyScopeRepository == null)
                {
                    this.tblQuotationDetailsInsCompanyScopeRepository = new GenericRepository<tblQuotationDetailsInsCompanyScope>(context);
                }
                return tblQuotationDetailsInsCompanyScopeRepository;
            }
        }

        //public GenericRepository<tblQuotationInsClassScope> TblQuotationInsClassScopeRepository
        //{
        //    get
        //    {
        //        if (this.tblQuotationInsClassScopeRepository == null)
        //        {
        //            this.tblQuotationInsClassScopeRepository = new GenericRepository<tblQuotationInsClassScope>(context);
        //        }
        //        return tblQuotationInsClassScopeRepository;
        //    }
        //}
        public GenericRepository<tblQuotationStatu> TblQuotationStatusRepository
        {
            get
            {
                if (this.tblQuotationStatusRepository == null)
                {
                    this.tblQuotationStatusRepository = new GenericRepository<tblQuotationStatu>(context);
                }
                return tblQuotationStatusRepository;
            }
        }
        public GenericRepository<tblPremium> TblPremiumRepository
        {
            get
            {
                if (this.tblPremiumRepository == null)
                {
                    this.tblPremiumRepository = new GenericRepository<tblPremium>(context);
                }
                return tblPremiumRepository;
            }
        }

        public GenericRepository<tblFamilyDiscount> TblFamilyDiscountRepository
        {
            get
            {
                if (this.tblFamilyDiscountRepository == null)
                {
                    this.tblFamilyDiscountRepository = new GenericRepository<tblFamilyDiscount>(context);
                }
                return tblFamilyDiscountRepository;
            }
        }

        public GenericRepository<tblCoverNote> TblCoverNoteRepository
        {
            get
            {
                if (this.tblCoverNoteRepository == null)
                {
                    this.tblCoverNoteRepository = new GenericRepository<tblCoverNote>(context);
                }
                return tblCoverNoteRepository;
            }
        }

        public GenericRepository<tblPolicyInformationRecording> TblPolicyInformationRecordingRepository
        {
            get
            {
                if (this.tblPolicyInformationRecordingRepository == null)
                {
                    this.tblPolicyInformationRecordingRepository = new GenericRepository<tblPolicyInformationRecording>(context);
                }
                return tblPolicyInformationRecordingRepository;
            }
        }

        public GenericRepository<tblPolicyCommisionPayment> TblPolicyCommisionPaymentRepository
        {
            get
            {
                if (this.tblPolicyCommisionPaymentRepository == null)
                {
                    this.tblPolicyCommisionPaymentRepository = new GenericRepository<tblPolicyCommisionPayment>(context);
                }
                return tblPolicyCommisionPaymentRepository;
            }
        }

        public GenericRepository<tblPolicyRenewalHistory> TblPolicyRenewalHistoryRepository
        {
            get
            {
                if (this.tblPolicyRenewalHistoryRepository == null)
                {
                    this.tblPolicyRenewalHistoryRepository = new GenericRepository<tblPolicyRenewalHistory>(context);
                }
                return tblPolicyRenewalHistoryRepository;
            }
        }

        public GenericRepository<tblClaimRecording> TblClaimRecordingRepository
        {
            get
            {
                if (this.tblClaimRecordingRepository == null)
                {
                    this.tblClaimRecordingRepository = new GenericRepository<tblClaimRecording>(context);
                }
                return tblClaimRecordingRepository;
            }
        }

        public GenericRepository<tblClaimRecHistory> TblClaimRecHistoryRepository
        {
            get
            {
                if (this.tblClaimRecHistoryRepository == null)
                {
                    this.tblClaimRecHistoryRepository = new GenericRepository<tblClaimRecHistory>(context);
                }
                return tblClaimRecHistoryRepository;
            }
        }

        public GenericRepository<tblClaimRecPendingDoc> TblClaimRecPendingDocRepository
        {
            get
            {
                if (this.tblClaimRecPendingDocRepository == null)
                {
                    this.tblClaimRecPendingDocRepository = new GenericRepository<tblClaimRecPendingDoc>(context);
                }
                return tblClaimRecPendingDocRepository;
            }
        }

        public GenericRepository<tblClaimPayment> TblClaimPaymentRepository
        {
            get
            {
                if (this.tblClaimPaymentRepository == null)
                {
                    this.tblClaimPaymentRepository = new GenericRepository<tblClaimPayment>(context);
                }
                return tblClaimPaymentRepository;
            }
        }

        public GenericRepository<tblPaymentType> TblPaymentTypeRepository
        {
            get
            {
                if (this.tblPaymentTypeRepository == null)
                {
                    this.tblPaymentTypeRepository = new GenericRepository<tblPaymentType>(context);
                }
                return tblPaymentTypeRepository;
            }
        }

        public GenericRepository<tblClaimPaymentMethod> TblClaimPaymentMethodRepository
        {
            get
            {
                if (this.tblClaimPaymentMethodRepository == null)
                {
                    this.tblClaimPaymentMethodRepository = new GenericRepository<tblClaimPaymentMethod>(context);
                }
                return tblClaimPaymentMethodRepository;
            }
        }

        public GenericRepository<tblChargeType> TblChargeTypeRepository
        {
            get
            {
                if (this.tblChargeTypeRepository == null)
                {
                    this.tblChargeTypeRepository = new GenericRepository<tblChargeType>(context);
                }
                return tblChargeTypeRepository;
            }
        }

        public GenericRepository<tblDebitNote> TblDebitNoteRepository
        {
            get
            {
                if (this.tblDebitNoteRepository == null)
                {
                    this.tblDebitNoteRepository = new GenericRepository<tblDebitNote>(context);
                }
                return tblDebitNoteRepository;
            }
        }

        public GenericRepository<tblPolicyInfoPayment> TblPolicyInfoPaymentRepository
        {
            get
            {
                if (this.tblPolicyInfoPaymentRepository == null)
                {
                    this.tblPolicyInfoPaymentRepository = new GenericRepository<tblPolicyInfoPayment>(context);
                }
                return tblPolicyInfoPaymentRepository;
            }
        }

        public GenericRepository<tblPolicyInfoCharge> TblPolicyInfoChargeRepository
        {
            get
            {
                if (this.tblPolicyInfoChargeRepository == null)
                {
                    this.tblPolicyInfoChargeRepository = new GenericRepository<tblPolicyInfoCharge>(context);
                }
                return tblPolicyInfoChargeRepository;
            }
        }

        public GenericRepository<tblPolicyDebitNote> TblPolicyDebitNoteRepository
        {
            get
            {
                if (this.tblPolicyDebitNoteRepository == null)
                {
                    this.tblPolicyDebitNoteRepository = new GenericRepository<tblPolicyDebitNote>(context);
                }
                return tblPolicyDebitNoteRepository;
            }
        }

        public GenericRepository<tblPayment> TblPaymentRepository
        {
            get
            {
                if (this.tblPaymentRepository == null)
                {
                    this.tblPaymentRepository = new GenericRepository<tblPayment>(context);
                }
                return tblPaymentRepository;
            }
        }
        public GenericRepository<PatientAdmission> TblPatientAdmission
        {
            get
            {
                if (this.tblPatientAdmission == null)
                {
                    this.tblPatientAdmission = new GenericRepository<PatientAdmission>(context);
                }
                return tblPatientAdmission;
            }
        }
        public GenericRepository<SingaporeAdmission> TblSingaporeAdmission
        {
            get
            {
                if (this.tblSingaporeAdmission == null)
                {
                    this.tblSingaporeAdmission = new GenericRepository<SingaporeAdmission>(context);
                }
                return tblSingaporeAdmission;
            }
        }
        public GenericRepository<tblWBN> TblFrequncy
        {
            get
            {
                if (this.tblBUFAFrequncyRepository == null)
                {
                    this.tblBUFAFrequncyRepository = new GenericRepository<tblWBN>(context);
                }
                return tblBUFAFrequncyRepository;
            }
        }
        public GenericRepository<Admission_Singapore> tblAdmission_Singapore
        {
            get
            {
                if (this.tblAdmissionSingapore == null)
                {
                    this.tblAdmissionSingapore = new GenericRepository<Admission_Singapore>(context);
                }
                return tblAdmissionSingapore;
            }
        }
        #endregion

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public PERFECTIBSEntities dbContext { get
            { return context; } }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
