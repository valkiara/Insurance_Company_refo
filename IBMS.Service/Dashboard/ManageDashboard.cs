using IBMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBMS.Shared.ViewModel;
using System.Data;

namespace IBMS.Service.Dashboard
{
    public class ManageDashboard
    {
        private UnitOfWork unitOfWork;
        public ManageDashboard()
        {
            unitOfWork = new UnitOfWork();
        }

        public DashboardVM GetQuotationCount()
        {
            try
            {
                DashboardVM objDashboardVM = new DashboardVM();
                PERFECTIBSEntities db = new PERFECTIBSEntities();

                objDashboardVM.PendingQuotationCount = unitOfWork.TblQuotationHeaderRepository.Get(o => o.QuotationStatusCode.Equals("QP")).Count();
                objDashboardVM.ApprovedQuotationCount = unitOfWork.TblQuotationHeaderRepository.Get(o => o.QuotationStatusCode.Equals("CA")).Count();
                objDashboardVM.NotCreatingQuotationCount = unitOfWork.TblQuotationHeaderRepository.Get(o => o.QuotationStatusCode.Equals("QNC")).Count();
                objDashboardVM.TCNIQuotationCount = unitOfWork.TblQuotationHeaderRepository.Get(o => o.QuotationStatusCode.Equals("TCNI")).Count();
                objDashboardVM.QuotationTotalCount = unitOfWork.TblQuotationHeaderRepository.Get().Count();
                objDashboardVM.ClientRequestCount = unitOfWork.TblClientRequestHeaderRepository.Get().Count();


                List<sp_GetClientVSQuotation_Count_Result> ListItems = db.sp_GetClientVSQuotation_Count().ToList<sp_GetClientVSQuotation_Count_Result>();
                // objDashboardVM.ClientRequestWithQuotation = ListItems;

                return objDashboardVM;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ClientWithQuotationCount> GetClienVSQuotation()
        {
            try
            {
                PERFECTIBSEntities db = new PERFECTIBSEntities();

                var QuotationData = db.sp_GetClientVSQuotation_Count().ToList();

                //  List<sp_GetClientVSQuotation_Count_Result> ListItems = db.sp_GetClientVSQuotation_Count().ToList<sp_GetClientVSQuotation_Count_Result>();
                List<ClientWithQuotationCount> List = new List<ClientWithQuotationCount>();


                foreach (var Quotation in QuotationData)
                {
                    ClientWithQuotationCount ClientQuoVM = new ClientWithQuotationCount();
                    ClientQuoVM.count = Quotation.COUNTS;
                    ClientQuoVM.January = Quotation.January.ToString();
                    ClientQuoVM.Febrary = Quotation.Febrary.ToString();
                    ClientQuoVM.March = Quotation.March.ToString();
                    ClientQuoVM.April = Quotation.April.ToString();
                    ClientQuoVM.May = Quotation.May.ToString();
                    ClientQuoVM.June = Quotation.June.ToString();
                    ClientQuoVM.July = Quotation.July.ToString();
                    ClientQuoVM.August = Quotation.August.ToString();
                    ClientQuoVM.September = Quotation.September.ToString();
                    ClientQuoVM.October = Quotation.October.ToString();
                    ClientQuoVM.November = Quotation.November.ToString();
                    ClientQuoVM.December = Quotation.December.ToString();

                    List.Add(ClientQuoVM);
                }


                return List;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public BusinessUnitVM GetBusinessUnitPer()
        //{
        //    try
        //    {
        //        BusinessUnitVM _objBusinessUnitVM = new BusinessUnitVM();
        //        _objBusinessUnitVM.BusinessUnit = unitOfWork.TblBussinessUnitRepository.Get().Count();


        //        return _objBusinessUnitVM;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        public List<BusinessUnitVM> GetBusinessUnitPer()
        {
            try
            {
                var businessUnitData = unitOfWork.TblBussinessUnitRepository.Get().ToList();

                List<BusinessUnitVM> businessUnitList = new List<BusinessUnitVM>();

                foreach (var businessUnit in businessUnitData)
                {
                    BusinessUnitVM businessUnitVM = new BusinessUnitVM();
                    businessUnitVM.BusinessUnitID = businessUnit.BUID;
                    businessUnitVM.BusinessUnit = businessUnit.BussinessUnit;

                    businessUnitVM.count = unitOfWork.TblClientRepository.Get(o => o.BUID == businessUnitVM.BusinessUnitID).Count();

                    businessUnitList.Add(businessUnitVM);
                }

                return businessUnitList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<ClientPaymentsVM> GetClientPaymentforDashboard()
        {
            try
            {
                var clientData = unitOfWork.TblClientRepository.Get().ToList();

                List<ClientPaymentsVM> clientVMList = new List<ClientPaymentsVM>();

                foreach (var Client in clientData)
                {
                    ClientPaymentsVM clientVM = new ClientPaymentsVM();
                    clientVM.ClientID = Client.ClientID;
                    clientVM.ClientName = Client.ClientName;


                    List<tblPayment> paymentList = unitOfWork.TblPaymentRepository.Get(a => a.ClientID == clientVM.ClientID).ToList();
                    List<tblPayment> clientPaymentList = paymentList.GroupBy(x => x.ClientID).Select(x => x.FirstOrDefault()).ToList();

                    //clientVM.PaymentList = clientPaymentList;
                    clientVM.PaymentAmount = paymentList.Sum(a => a.PaymentAmount).Value;
                    clientVMList.Add(clientVM);
                }

                return clientVMList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ClientPaymentsVM> GetPaymentAll()
        {
            List<ClientPaymentsVM> clientPaymentList = new List<ClientPaymentsVM>();
            var clientData = unitOfWork.TblClientRepository.Get().ToList();

            foreach (var client in clientData)
            {
                ClientPaymentsVM clientVM = new ClientPaymentsVM();
                clientVM.ClientID = client.ClientID;
                clientVM.ClientName = client.ClientName;

                List<tblPayment> paymentList = unitOfWork.TblPaymentRepository.Get(a => a.ClientID == clientVM.ClientID).ToList();
                List<tblPayment> tblclientPaymentList = paymentList.GroupBy(x => x.ClientID).Select(x => x.FirstOrDefault()).ToList();

                clientVM.PaymentAmount = paymentList.Sum(a => a.PaymentAmount).Value;

                clientVM.NoOfClientRequest = unitOfWork.TblClientRequestHeaderRepository.Get(x => x.ClientID == clientVM.ClientID).Count();
                List<tblQuotationHeader> quotatioHeaderList = unitOfWork.TblQuotationHeaderRepository.Get().ToList();

                foreach (var qoutationList in quotatioHeaderList)
                {
                    var clientRequestID = qoutationList.ClientRequestHeaderID;

                    clientVM.NoOfQuitation = unitOfWork.TblClientRequestHeaderRepository.Get(x => x.ClientID == clientVM.ClientID).GroupBy
                        (y => y.ClientRequestHeaderID == qoutationList.ClientRequestHeaderID).Select(y => y.FirstOrDefault()).ToList().Count();

                }

                clientPaymentList.Add(clientVM);
            }

            return clientPaymentList;
        }

    }
}



