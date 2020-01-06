using IBMS.Repository;
using IBMS.Shared.ViewModel;
using IBMS.Service.Errorlog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBMS.Shared.ViewModel.Mapper;

namespace IBMS.Service.TransactionData
{
    public class ManageClientRequest
    {
        private UnitOfWork unitOfWork;
        public ManageClientRequest()
        {
            unitOfWork = new UnitOfWork();
        }

        #region client save/update/Load

        public bool SaveClient(ClientVM clientVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            { 
                try
                {
                    int clientID = 0;
                    if (clientVM.IsClientUpdated)
                    {
                        clientID = clientVM.ClientID;

                        tblClient client = unitOfWork.TblClientRepository.GetByID(clientID);
                        client.ClientName = clientVM.ClientName;
                        client.ClientAddress = clientVM.ClientAddress;
                        client.NIC = clientVM.NIC;
                        client.ContactNo = clientVM.ContactNo;
                        client.FixedLine = clientVM.FixedLine;
                        client.Email = clientVM.Email;
                        client.DOB = !string.IsNullOrEmpty(clientVM.DOB) ? DateTime.ParseExact(clientVM.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        client.PPID = clientVM.PPID;
                        client.FamilyDiscount = clientVM.FamilyDiscount;
                        client.AdditionalNote = clientVM.AdditionalNote;
                        client.HomeCountryID = clientVM.HomeCountryID;
                        client.ResidentCountryID = clientVM.ResidentCountryID;
                        client.BUID = clientVM.BusinessUnitID;
                        client.ModifiedBy = clientVM.UserID;
                        client.ModifiedDate = DateTime.Now;
                        client.District = clientVM.DistrictId;
                        client.BRNo = clientVM.BRNo;
                        client.Other = clientVM.Other;
                        client.TitleID = clientVM.TitleID;
                        client.ExtraText = clientVM.ClientOtherName;
                        unitOfWork.TblClientRepository.Update(client);
                        unitOfWork.Save();
                    }
                    else if (clientVM.IsClientAdded)
                    {
                        tblClient client = new tblClient();
                        client.ClientName = clientVM.ClientName;
                        client.ClientAddress = clientVM.ClientAddress;
                        client.NIC = clientVM.NIC;
                        client.ContactNo = clientVM.ContactNo;
                        client.FixedLine = clientVM.FixedLine;
                        client.Email = clientVM.Email;
                        client.DOB = !string.IsNullOrEmpty(clientVM.DOB) ? DateTime.ParseExact(clientVM.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        client.PPID = clientVM.PPID;
                        client.FamilyDiscount = clientVM.FamilyDiscount;
                        client.AdditionalNote = clientVM.AdditionalNote;
                        client.HomeCountryID = clientVM.HomeCountryID;
                        client.ResidentCountryID = clientVM.ResidentCountryID;
                        client.BUID = clientVM.BusinessUnitID;
                        client.CreatedBy = clientVM.UserID;
                        client.CreatedDate = DateTime.Now;
                        client.District = clientVM.DistrictId;
                        client.BRNo = clientVM.BRNo;
                        client.Other = clientVM.Other;
                        client.TitleID = clientVM.TitleID;
                        client.ExtraText = clientVM.ClientOtherName;
                        unitOfWork.TblClientRepository.Insert(client);
                        unitOfWork.Save();
                        clientID = client.ClientID;

                    }
                    dbTransaction.Commit();
                    return true;
                }
                catch(Exception ex)
                {
                    dbTransaction.Rollback();
                    return false;

                }
            }
        }
        #endregion

        #region client Request SIB
        public bool SaveClientRequest(ClientRequestVM clientRequestVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    int clientID = 0;

                        if (clientRequestVM.IsClientUpdated)
                    {
                        clientID = clientRequestVM.ClientDetails.ClientID;

                        tblClient client = unitOfWork.TblClientRepository.GetByID(clientID);
                        client.ClientName = clientRequestVM.ClientDetails.ClientName;
                        client.ClientAddress = clientRequestVM.ClientDetails.ClientAddress;
                        client.NIC = clientRequestVM.ClientDetails.NIC;
                        client.ContactNo = clientRequestVM.ClientDetails.ContactNo;
                        client.FixedLine = clientRequestVM.ClientDetails.FixedLine;
                        client.Email = clientRequestVM.ClientDetails.Email;
                        client.DOB = !string.IsNullOrEmpty(clientRequestVM.ClientDetails.DOB) ? DateTime.ParseExact(clientRequestVM.ClientDetails.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        client.PPID = clientRequestVM.ClientDetails.PPID;
                        client.FamilyDiscount = clientRequestVM.ClientDetails.FamilyDiscount;
                        client.AdditionalNote = clientRequestVM.ClientDetails.AdditionalNote;
                        client.HomeCountryID = clientRequestVM.ClientDetails.HomeCountryID;
                        client.ResidentCountryID = clientRequestVM.ClientDetails.ResidentCountryID;
                        client.BUID = clientRequestVM.ClientDetails.BusinessUnitID;
                        client.ModifiedBy = clientRequestVM.UserID;
                        client.ModifiedDate = DateTime.Now;
                        client.District = clientRequestVM.ClientDetails.DistrictId;
                        client.Other = clientRequestVM.ClientDetails.Other;
                        unitOfWork.TblClientRepository.Update(client);
                        unitOfWork.Save();
                    }
                    else if (clientRequestVM.IsClientAdded)
                    {
                        //Save Client
                        tblClient client = new tblClient();
                        client.ClientName = clientRequestVM.ClientDetails.ClientName;
                        client.ClientAddress = clientRequestVM.ClientDetails.ClientAddress;
                        client.NIC = clientRequestVM.ClientDetails.NIC;
                        client.ContactNo = clientRequestVM.ClientDetails.ContactNo;
                        client.FixedLine = clientRequestVM.ClientDetails.FixedLine;
                        client.Email = clientRequestVM.ClientDetails.Email;
                        client.DOB = !string.IsNullOrEmpty(clientRequestVM.ClientDetails.DOB) ? DateTime.ParseExact(clientRequestVM.ClientDetails.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        client.PPID = clientRequestVM.ClientDetails.PPID;
                        client.FamilyDiscount = clientRequestVM.ClientDetails.FamilyDiscount;
                        client.AdditionalNote = clientRequestVM.ClientDetails.AdditionalNote;
                        client.HomeCountryID = clientRequestVM.ClientDetails.HomeCountryID;
                        client.ResidentCountryID = clientRequestVM.ClientDetails.ResidentCountryID;
                        client.BUID = clientRequestVM.ClientDetails.BusinessUnitID;
                        client.CreatedBy = clientRequestVM.UserID;
                        client.CreatedDate = DateTime.Now;
                        client.District = clientRequestVM.ClientDetails.DistrictId;
                        unitOfWork.TblClientRepository.Insert(client);
                        unitOfWork.Save();

                        clientID = client.ClientID;
                    }
                    else
                    {
                        clientID = clientRequestVM.ClientDetails.ClientID;
                    }


                   
                   // Save Client Request Header
                    tblClientRequestHeader clientRequestHeader = new tblClientRequestHeader();
                    clientRequestHeader.ClientID = clientID;
                    clientRequestHeader.PartnerID = clientRequestVM.ClientRequestHeaderDetails.PartnerID;
                    clientRequestHeader.AccountHandlerID = clientRequestVM.ClientRequestHeaderDetails.AccountHandlerID < 0 ? 0 : clientRequestVM.ClientRequestHeaderDetails.AccountHandlerID;
                    clientRequestHeader.AgentID = clientRequestVM.ClientRequestHeaderDetails.AgentID < 0 ? 0 : clientRequestVM.ClientRequestHeaderDetails.AgentID;
                    clientRequestHeader.EmployeeID = clientRequestVM.ClientRequestHeaderDetails.EmployeeID < 0 ? 0 : clientRequestVM.ClientRequestHeaderDetails.EmployeeID;
                    clientRequestHeader.IntroducerID = clientRequestVM.ClientRequestHeaderDetails.IntroducerID < 0 ? 0 : clientRequestVM.ClientRequestHeaderDetails.IntroducerID;
                    //   clientRequestHeader.PartnerID = clientRequestVM.ClientRequestHeaderDetails.AgentID;
                    clientRequestHeader.RequestedDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.RequestedDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.RequestedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    clientRequestHeader.CreatedBy = clientRequestVM.UserID;
                    clientRequestHeader.CreatedDate = DateTime.Now;
                    unitOfWork.TblClientRequestHeaderRepository.Insert(clientRequestHeader);
                    unitOfWork.Save();

                    // Commented on 12-02-2019
                    ////Save Client Request Line
                    //foreach (var requestLine in clientRequestVM.ClientRequestHeaderDetails.ClientRequestLineDetails)
                    //{
                    //    tblClientRequestLine clientRequestLine = new tblClientRequestLine();
                    //    clientRequestLine.ClientRequestHeaderID = clientRequestHeader.ClientRequestHeaderID;
                    //    clientRequestLine.InsSubClassID = requestLine.InsSubClassID;
                    //    clientRequestLine.CreatedBy = clientRequestVM.UserID;
                    //    clientRequestLine.CreatedDate = DateTime.Now;
                    //    unitOfWork.TblClientRequestLineRepository.Insert(clientRequestLine);
                    //    unitOfWork.Save();

                    //    //Save Client Property
                    //    foreach (var property in requestLine.ClientPropertyDetails)
                    //    {
                    //        tblClientProperty clientProperty = new tblClientProperty();
                    //        clientProperty.ClientPropertyName = property.ClientPropertyName;
                    //        clientProperty.BRNo = property.BRNo;
                    //        clientProperty.VATNo = property.VATNo;
                    //        clientProperty.ClientRequestLineID = clientRequestLine.ClientRequestLineID;
                    //        clientProperty.CreatedBy = clientRequestVM.UserID;
                    //        clientProperty.CreatedDate = DateTime.Now;
                    //        unitOfWork.TblClientPropertyRepository.Insert(clientProperty);
                    //        unitOfWork.Save();
                    //    }

                    //    //Save Client Request Insurance Sub Class Scope
                    //    foreach (var insSubClassScope in requestLine.ClientRequestInsSubClassScopeDetails)
                    //    {
                    //        tblClientRequestInsSubClassScope clientRequestInsSubClassScope = new tblClientRequestInsSubClassScope();
                    //        clientRequestInsSubClassScope.ClientRequestLineID = clientRequestLine.ClientRequestLineID;
                    //        clientRequestInsSubClassScope.CommonInsScopeID = insSubClassScope.CommonInsScopeID;
                    //        clientRequestInsSubClassScope.CreatedBy = clientRequestVM.UserID;
                    //        clientRequestInsSubClassScope.CreatedDate = DateTime.Now;
                    //        unitOfWork.TblClientRequestInsSubClassScopeRepository.Insert(clientRequestInsSubClassScope);
                    //        unitOfWork.Save();
                    //    }
                    //}

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

        public bool UpdateClientRequest(ClientRequestHeaderVM clientRequestHeaderVM, bool isClientUpdated, bool isClientAdded, ClientVM clientObj, out string errorMessage)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    int clientID = 0;

                    if (clientRequestHeaderVM.IsQuotationCreated != true)
                    {
                        if (isClientUpdated)
                        {
                            clientID = clientObj.ClientID;

                            //Update Client
                            tblClient client = unitOfWork.TblClientRepository.GetByID(clientObj.ClientID);
                            client.ClientName = clientObj.ClientName;
                            client.ClientAddress = clientObj.ClientAddress;
                            client.NIC = clientObj.NIC;
                            client.ContactNo = clientObj.ContactNo;
                            client.FixedLine = clientObj.FixedLine;
                            client.Email = clientObj.Email;
                            client.DOB = !string.IsNullOrEmpty(clientObj.DOB) ? DateTime.ParseExact(clientObj.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            client.PPID = clientObj.PPID;
                            client.FamilyDiscount = clientObj.FamilyDiscount;
                            client.AdditionalNote = clientObj.AdditionalNote;
                            client.HomeCountryID = clientObj.HomeCountryID;
                            client.ResidentCountryID = clientObj.ResidentCountryID;
                            client.BUID = clientObj.BusinessUnitID;
                            client.ModifiedBy = clientRequestHeaderVM.ModifiedBy;
                            client.ModifiedDate = DateTime.Now;
                            unitOfWork.TblClientRepository.Update(client);
                            unitOfWork.Save();

                        }
                        else if (isClientAdded)
                        {
                            //Save Client
                            tblClient client = new tblClient();
                            client.ClientName = clientObj.ClientName;
                            client.ClientAddress = clientObj.ClientAddress;
                            client.NIC = clientObj.NIC;
                            client.ContactNo = clientObj.ContactNo;
                            client.FixedLine = clientObj.FixedLine;
                            client.Email = clientObj.Email;
                            client.DOB = !string.IsNullOrEmpty(clientObj.DOB) ? DateTime.ParseExact(clientObj.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            client.PPID = clientObj.PPID;
                            client.FamilyDiscount = clientObj.FamilyDiscount;
                            client.AdditionalNote = clientObj.AdditionalNote;
                            client.HomeCountryID = clientObj.HomeCountryID;
                            client.ResidentCountryID = clientObj.ResidentCountryID;
                            client.BUID = clientObj.BusinessUnitID;
                            client.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                            client.CreatedDate = DateTime.Now;
                            unitOfWork.TblClientRepository.Insert(client);
                            unitOfWork.Save();

                            clientID = client.ClientID;
                        }
                        else
                        {
                            clientID = clientObj.ClientID;
                        }

                        //Update Client Request Header
                        tblClientRequestHeader clientRequestHeader = unitOfWork.TblClientRequestHeaderRepository.GetByID(clientRequestHeaderVM.ClientRequestHeaderID);
                        clientRequestHeader.ClientID = clientRequestHeaderVM.ClientID;
                        clientRequestHeader.PartnerID = clientRequestHeaderVM.PartnerID;
                        clientRequestHeader.RequestedDate = !string.IsNullOrEmpty(clientRequestHeaderVM.RequestedDate) ? DateTime.ParseExact(clientRequestHeaderVM.RequestedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        clientRequestHeader.ModifiedBy = clientRequestHeaderVM.ModifiedBy;
                        clientRequestHeader.ModifiedDate = DateTime.Now;
                        clientRequestHeader.AccountHandlerID = clientRequestHeaderVM.AccountHandlerID < 0 ? 0 : clientRequestHeaderVM.AccountHandlerID;
                        clientRequestHeader.AgentID = clientRequestHeaderVM.AgentID < 0 ? 0 : clientRequestHeaderVM.AgentID;
                        clientRequestHeader.EmployeeID = clientRequestHeaderVM.EmployeeID < 0 ? 0 : clientRequestHeaderVM.EmployeeID;
                        clientRequestHeader.IntroducerID = clientRequestHeaderVM.IntroducerID < 0 ? 0 : clientRequestHeaderVM.IntroducerID;
                        unitOfWork.TblClientRequestHeaderRepository.Update(clientRequestHeader);
                        unitOfWork.Save();

                        //Delete Client Request Line, Client Property and Client Request Insurance Sub Class Scope Details
                        var clientRequestLineData = unitOfWork.TblClientRequestLineRepository.Get(x => x.ClientRequestHeaderID == clientRequestHeaderVM.ClientRequestHeaderID).ToList();

                        foreach (var clientRequestLine in clientRequestLineData)
                        {
                            var clientPropertyData = unitOfWork.TblClientPropertyRepository.Get(x => x.ClientRequestLineID == clientRequestLine.ClientRequestLineID).ToList();

                            foreach (var clientProperty in clientPropertyData)
                            {
                                unitOfWork.TblClientPropertyRepository.Delete(clientProperty);
                                unitOfWork.Save();
                            }

                            var clientReqInsSubClassScopeData = unitOfWork.TblClientRequestInsSubClassScopeRepository.Get(x => x.ClientRequestLineID == clientRequestLine.ClientRequestLineID).ToList();

                            foreach (var clientReqInsSubClassScope in clientReqInsSubClassScopeData)
                            {
                                unitOfWork.TblClientRequestInsSubClassScopeRepository.Delete(clientReqInsSubClassScope);
                                unitOfWork.Save();
                            }

                            unitOfWork.TblClientRequestLineRepository.Delete(clientRequestLine);
                            unitOfWork.Save();
                        }

                        //Save Client Request Line
                        foreach (var requestLine in clientRequestHeaderVM.ClientRequestLineDetails)
                        {
                            tblClientRequestLine clientRequestLine = new tblClientRequestLine();
                            clientRequestLine.ClientRequestHeaderID = clientRequestHeader.ClientRequestHeaderID;
                            clientRequestLine.InsSubClassID = requestLine.InsSubClassID;
                            clientRequestLine.CreatedBy = clientRequestHeader.CreatedBy;
                            clientRequestLine.CreatedDate = clientRequestHeader.CreatedDate;
                            clientRequestLine.ModifiedBy = clientRequestHeaderVM.ModifiedBy;
                            clientRequestLine.ModifiedDate = DateTime.Now;
                            unitOfWork.TblClientRequestLineRepository.Insert(clientRequestLine);
                            unitOfWork.Save();

                            //Save Client Property
                            foreach (var property in requestLine.ClientPropertyDetails)
                            {
                                tblClientProperty clientProperty = new tblClientProperty();
                                clientProperty.ClientPropertyName = property.ClientPropertyName;
                                clientProperty.BRNo = property.BRNo;
                                clientProperty.VATNo = property.VATNo;
                                clientProperty.ClientRequestLineID = clientRequestLine.ClientRequestLineID;
                                clientProperty.CreatedBy = clientRequestHeader.CreatedBy;
                                clientProperty.CreatedDate = clientRequestHeader.CreatedDate;
                                clientProperty.ModifiedBy = clientRequestHeaderVM.ModifiedBy;
                                clientProperty.ModifiedDate = DateTime.Now;
                                unitOfWork.TblClientPropertyRepository.Insert(clientProperty);
                                unitOfWork.Save();
                            }

                            //Save Client Request Insurance Sub Class Scope
                            foreach (var insSubClassScope in requestLine.ClientRequestInsSubClassScopeDetails)
                            {
                                tblClientRequestInsSubClassScope clientRequestInsSubClassScope = new tblClientRequestInsSubClassScope();
                                clientRequestInsSubClassScope.ClientRequestLineID = clientRequestLine.ClientRequestLineID;
                                clientRequestInsSubClassScope.CommonInsScopeID = insSubClassScope.CommonInsScopeID;
                                clientRequestInsSubClassScope.CreatedBy = clientRequestHeader.CreatedBy;
                                clientRequestInsSubClassScope.CreatedDate = clientRequestHeader.CreatedDate;
                                clientRequestInsSubClassScope.ModifiedBy = clientRequestHeaderVM.ModifiedBy;
                                clientRequestInsSubClassScope.ModifiedDate = DateTime.Now;
                                unitOfWork.TblClientRequestInsSubClassScopeRepository.Insert(clientRequestInsSubClassScope);
                                unitOfWork.Save();
                            }
                        }

                        //Complete the Transaction
                        dbTransaction.Commit();

                        errorMessage = "No Error";
                        return true;
                    }
                    else
                    {
                        errorMessage = "Quotations are created based on this request. Therefore it cannot be modified";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();

                    errorMessage = "Update Failed";
                    return false;
                }
            }
        }

        
        public List<ClientVM> GetAllClients()
        {
            try
            {
                var clientData = unitOfWork.TblClientRepository.Get().ToList();

                List<ClientVM> clientList = new List<ClientVM>();

                foreach (var client in clientData)
                {
                    ClientVM clientVM = new ClientVM();
                    clientVM.ClientID = client.ClientID;
                    clientVM.ClientName = client.ClientName;
                    clientVM.ClientAddress = client.ClientAddress;
                    clientVM.NIC = client.NIC;
                    clientVM.ContactNo = client.ContactNo;
                    clientVM.FixedLine = client.FixedLine;
                    clientVM.Email = client.Email;
                    clientVM.DOB = client.DOB != null ? Convert.ToDateTime(client.DOB).ToString("dd/MM/yyyy") : string.Empty;
                    clientVM.PPID = client.PPID;
                    clientVM.FamilyDiscount = client.FamilyDiscount != null ? Convert.ToDecimal(client.FamilyDiscount) : 0;
                    clientVM.AdditionalNote = client.AdditionalNote;
                    clientVM.HomeCountryID = client.HomeCountryID != null ? Convert.ToInt32(client.HomeCountryID) : 0;

                    if (clientVM.HomeCountryID > 0)
                    {
                        clientVM.HomeCountryName = client.tblCountry.CountryName;
                    }

                    clientVM.ResidentCountryID = client.ResidentCountryID != null ? Convert.ToInt32(client.ResidentCountryID) : 0;

                    if (clientVM.ResidentCountryID > 0)
                    {
                        clientVM.ResidentCountryName = client.tblCountry1.CountryName;
                    }

                    clientVM.BusinessUnitID = client.BUID != null ? Convert.ToInt32(client.BUID) : 0;

                    if (clientVM.BusinessUnitID > 0)
                    {
                        clientVM.BusinessUnitName = client.tblBussinessUnit.BussinessUnit;
                    }

                    clientVM.CreatedBy = client.CreatedBy != null ? Convert.ToInt32(client.CreatedBy) : 0;
                    clientVM.CreatedDate = client.CreatedDate != null ? client.CreatedDate.ToString() : string.Empty;
                    clientVM.ModifiedBy = client.ModifiedBy != null ? Convert.ToInt32(client.ModifiedBy) : 0;
                    clientVM.ModifiedDate = client.ModifiedDate != null ? client.ModifiedDate.ToString() : string.Empty;

                    clientList.Add(clientVM);
                }

                return clientList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ClientVM> SearchClients(int? businessUnitID, int? homeCountryID, int? residentCountryID)
        {
            try
            {
                PERFECTIBSEntities context = new PERFECTIBSEntities();
                var clientData = context.spSearchClient(businessUnitID, homeCountryID, residentCountryID);

                List<ClientVM> clientList = new List<ClientVM>();

                foreach (var client in clientData)
                {
                    ClientVM clientVM = new ClientVM();
                    clientVM.ClientID = client.ClientID;
                    clientVM.ClientName = client.ClientName;
                    clientVM.ClientAddress = client.ClientAddress;
                    clientVM.NIC = client.NIC;
                    clientVM.ContactNo = client.ContactNo;
                    clientVM.FixedLine = client.FixedLine;
                    clientVM.Email = client.Email;
                    clientVM.DOB = client.DOB != null ? Convert.ToDateTime(client.DOB).ToString("dd/MM/yyyy") : string.Empty;
                    clientVM.PPID = client.PPID;
                    clientVM.FamilyDiscount = client.FamilyDiscount != null ? Convert.ToDecimal(client.FamilyDiscount) : 0;
                    clientVM.AdditionalNote = client.AdditionalNote;
                    clientVM.HomeCountryID = client.HomeCountryID != null ? Convert.ToInt32(client.HomeCountryID) : 0;
                    clientVM.HomeCountryName = client.HomeCountryName;
                    clientVM.ResidentCountryID = client.ResidentCountryID != null ? Convert.ToInt32(client.ResidentCountryID) : 0;
                    clientVM.ResidentCountryName = client.ResidentCountryName;
                    clientVM.BusinessUnitID = client.BUID != null ? Convert.ToInt32(client.BUID) : 0;
                    clientVM.BusinessUnitName = client.BussinessUnit;
                    clientVM.CreatedBy = client.CreatedBy != null ? Convert.ToInt32(client.CreatedBy) : 0;
                    clientVM.CreatedDate = client.CreatedDate != null ? client.CreatedDate.ToString() : string.Empty;
                    clientVM.ModifiedBy = client.ModifiedBy != null ? Convert.ToInt32(client.ModifiedBy) : 0;
                    clientVM.ModifiedDate = client.ModifiedDate != null ? client.ModifiedDate.ToString() : string.Empty;

                    clientList.Add(clientVM);
                }

                return clientList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ClientVM> GetAllClientsByBusinessUnitID(int businessUnitID)
        {
            try
            {
                PERFECTIBSEntities context = new PERFECTIBSEntities();
                

                //if (businessUnitID == 129)
                //{
                //    var clientIndoData = unitOfWork.TblClientRepository.Get(x => x.BUID == businessUnitID).ToList();

                //    List<ClientVM> clientList = new List<ClientVM>();

                //    foreach (var client in clientIndoData)
                //    {
                //        ClientVM clientVM = new ClientVM();
                //        clientVM.ClientID = client.ClientID;
                //        clientVM.ClientName = client.ClientName;
                //        clientVM.ClientAddress = client.ClientAddress;
                //        clientVM.NIC = client.NIC;
                //        clientVM.ContactNo = client.ContactNo;
                //        clientVM.FixedLine = client.FixedLine;
                //        clientVM.Email = client.Email;
                //        clientVM.DOB = client.DOB != null ? Convert.ToDateTime(client.DOB).ToString("dd/MM/yyyy") : string.Empty;
                //        clientVM.PPID = client.PPID;
                //        clientVM.FamilyDiscount = client.FamilyDiscount != null ? Convert.ToDecimal(client.FamilyDiscount) : 0;
                //        clientVM.AdditionalNote = client.AdditionalNote;
                //        clientVM.HomeCountryID = client.HomeCountryID != null ? Convert.ToInt32(client.HomeCountryID) : 0;
                //        var countryName = unitOfWork.TblCountryRepository.Get(x => x.CountryID == clientVM.HomeCountryID);

                //        foreach (var country in countryName)
                //        {
                //            CountryVM homeCountry = new CountryVM();
                //            clientVM.HomeCountryName = country.CountryName;

                //        }

               
                        
                //        clientVM.ResidentCountryID = client.ResidentCountryID != null ? Convert.ToInt32(client.ResidentCountryID) : 0;
                //        clientVM.ResidentCountryName = client.ResidentCountryName;
                //        clientVM.BusinessUnitID = client.BUID != null ? Convert.ToInt32(client.BUID) : 0;
                //        clientVM.BusinessUnitName = client.b;
                //        clientVM.CreatedBy = client.CreatedBy != null ? Convert.ToInt32(client.CreatedBy) : 0;
                //        clientVM.CreatedDate = client.CreatedDate != null ? client.CreatedDate.ToString() : string.Empty;
                //        clientVM.ModifiedBy = client.ModifiedBy != null ? Convert.ToInt32(client.ModifiedBy) : 0;
                //        clientVM.ModifiedDate = client.ModifiedDate != null ? client.ModifiedDate.ToString() : string.Empty;

                //        clientList.Add(clientVM);
                //    }
                //}
                //else
                //{
                    var clientData = context.spSearchClient(businessUnitID, null, null);
                    List<ClientVM> clientList = new List<ClientVM>();

                    clientList = new List<ClientVM>();
                    foreach (var client in clientData)
                    {
                        ClientVM clientVM = new ClientVM();
                        clientVM.ClientID = client.ClientID;
                        clientVM.ClientName = client.ClientName;
                        clientVM.ClientAddress = client.ClientAddress;
                        clientVM.NIC = client.NIC;
                        clientVM.ContactNo = client.ContactNo;
                        clientVM.FixedLine = client.FixedLine;
                        clientVM.Email = client.Email;
                        clientVM.DOB = client.DOB != null ? Convert.ToDateTime(client.DOB).ToString("dd/MM/yyyy") : string.Empty;
                        clientVM.PPID = client.PPID;
                        clientVM.FamilyDiscount = client.FamilyDiscount != null ? Convert.ToDecimal(client.FamilyDiscount) : 0;
                        clientVM.AdditionalNote = client.AdditionalNote;
                        clientVM.HomeCountryID = client.HomeCountryID != null ? Convert.ToInt32(client.HomeCountryID) : 0;
                        clientVM.HomeCountryName = client.HomeCountryName;
                        clientVM.ResidentCountryID = client.ResidentCountryID != null ? Convert.ToInt32(client.ResidentCountryID) : 0;
                        clientVM.ResidentCountryName = client.ResidentCountryName;
                        clientVM.BusinessUnitID = client.BUID != null ? Convert.ToInt32(client.BUID) : 0;
                        clientVM.BusinessUnitName = client.BussinessUnit;
                        clientVM.CreatedBy = client.CreatedBy != null ? Convert.ToInt32(client.CreatedBy) : 0;
                        clientVM.CreatedDate = client.CreatedDate != null ? client.CreatedDate.ToString() : string.Empty;
                        clientVM.ModifiedBy = client.ModifiedBy != null ? Convert.ToInt32(client.ModifiedBy) : 0;
                        clientVM.ModifiedDate = client.ModifiedDate != null ? client.ModifiedDate.ToString() : string.Empty;

                        clientList.Add(clientVM);
                    }
                //}

                return clientList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ClientVM GetClientByID(int clientID)
        {
            try
            {
                var clientData = unitOfWork.TblClientRepository.GetByID(clientID);

                ClientVM clientVM = new ClientVM();
                clientVM.ClientID = clientData.ClientID;
                clientVM.ClientName = clientData.ClientName;
                clientVM.ClientAddress = clientData.ClientAddress;
                clientVM.NIC = clientData.NIC;
                clientVM.ContactNo = clientData.ContactNo;
                clientVM.FixedLine = clientData.FixedLine;
                clientVM.Email = clientData.Email;
                clientVM.DOB = clientData.DOB != null ? Convert.ToDateTime(clientData.DOB).ToString("dd/MM/yyyy") : string.Empty;
                clientVM.PPID = clientData.PPID;
                clientVM.FamilyDiscount = clientData.FamilyDiscount != null ? Convert.ToDecimal(clientData.FamilyDiscount) : 0;
                clientVM.AdditionalNote = clientData.AdditionalNote;
                clientVM.HomeCountryID = clientData.HomeCountryID != null ? Convert.ToInt32(clientData.HomeCountryID) : 0;
                
                if (clientVM.HomeCountryID > 0)
                {
                    clientVM.HomeCountryName = clientData.tblCountry.CountryName;
                }

                clientVM.ResidentCountryID = clientData.ResidentCountryID != null ? Convert.ToInt32(clientData.ResidentCountryID) : 0;

                if (clientVM.ResidentCountryID > 0)
                {
                    clientVM.ResidentCountryName = clientData.tblCountry1.CountryName;
                }

                clientVM.BusinessUnitID = clientData.BUID != null ? Convert.ToInt32(clientData.BUID) : 0;

                if (clientVM.BusinessUnitID > 0)
                {
                    clientVM.BusinessUnitName = clientData.tblBussinessUnit.BussinessUnit;
                }

                clientVM.CreatedBy = clientData.CreatedBy != null ? Convert.ToInt32(clientData.CreatedBy) : 0;
                clientVM.CreatedDate = clientData.CreatedDate != null ? clientData.CreatedDate.ToString() : string.Empty;
                clientVM.ModifiedBy = clientData.ModifiedBy != null ? Convert.ToInt32(clientData.ModifiedBy) : 0;
                clientVM.ModifiedDate = clientData.ModifiedDate != null ? clientData.ModifiedDate.ToString() : string.Empty;

                return clientVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ClientRequestHeaderVM> GetAllClientRequests()
        {
            try
            {
                var clientRequestHeaderData = unitOfWork.TblClientRequestHeaderRepository.Get().ToList();

                List<ClientRequestHeaderVM> clientRequestHeaderList = new List<ClientRequestHeaderVM>();

                foreach (var clientReqHeader in clientRequestHeaderData)
                {
                    ClientRequestHeaderVM clientRequestHeaderVM = new ClientRequestHeaderVM();
                    clientRequestHeaderVM.ClientRequestHeaderID = clientReqHeader.ClientRequestHeaderID;
                    clientRequestHeaderVM.ClientID = clientReqHeader.ClientID != null ? Convert.ToInt32(clientReqHeader.ClientID) : 0;

                    if (clientRequestHeaderVM.ClientID > 0)
                    {
                        clientRequestHeaderVM.ClientName = clientReqHeader.tblClient.ClientName;
                        clientRequestHeaderVM.BusinessUnitID = clientReqHeader.tblClient.BUID != null ? Convert.ToInt32(clientReqHeader.tblClient.BUID) : 0;

                        if (clientRequestHeaderVM.BusinessUnitID > 0)
                        {
                            clientRequestHeaderVM.BusinessUnitName = clientReqHeader.tblClient.tblBussinessUnit.BussinessUnit;
                            clientRequestHeaderVM.CompanyID = clientReqHeader.tblClient.tblBussinessUnit.CompID != null ? Convert.ToInt32(clientReqHeader.tblClient.tblBussinessUnit.CompID) : 0;

                            if (clientRequestHeaderVM.CompanyID > 0)
                            {
                                clientRequestHeaderVM.CompanyName = clientReqHeader.tblClient.tblBussinessUnit.tblCompany.CompanyName;
                            }
                        }
                    }

                    clientRequestHeaderVM.PartnerID = clientReqHeader.PartnerID != null ? Convert.ToInt32(clientReqHeader.PartnerID) : 0;

                    if (clientRequestHeaderVM.PartnerID > 0)
                    {
                        clientRequestHeaderVM.PartnerName = "Partner Name";//clientReqHeader.tblPartner.PartnerName;
                    }

                    clientRequestHeaderVM.RequestedDate = clientReqHeader.RequestedDate != null ? Convert.ToDateTime(clientReqHeader.RequestedDate).ToString("dd/MM/yyyy") : string.Empty;
                    clientRequestHeaderVM.IsQuotationCreated = clientReqHeader.IsQuotationCreated != null ? Convert.ToBoolean(clientReqHeader.IsQuotationCreated) : false;
                    clientRequestHeaderVM.CreatedBy = clientReqHeader.CreatedBy != null ? Convert.ToInt32(clientReqHeader.CreatedBy) : 0;
                    clientRequestHeaderVM.CreatedDate = clientReqHeader.CreatedDate != null ? clientReqHeader.CreatedDate.ToString() : string.Empty;
                    clientRequestHeaderVM.ModifiedBy = clientReqHeader.ModifiedBy != null ? Convert.ToInt32(clientReqHeader.ModifiedBy) : 0;
                    clientRequestHeaderVM.ModifiedDate = clientReqHeader.ModifiedDate != null ? clientReqHeader.ModifiedDate.ToString() : string.Empty;

                    //Client Request Line Details
                    var clientRequestLineData = unitOfWork.TblClientRequestLineRepository.Get(x => x.ClientRequestHeaderID == clientReqHeader.ClientRequestHeaderID).ToList();

                    List<ClientRequestLineVM> clientRequesLinetList = new List<ClientRequestLineVM>();

                    foreach (var clientReqLine in clientRequestLineData)
                    {
                        ClientRequestLineVM clientRequestLineVM = new ClientRequestLineVM();
                        clientRequestLineVM.ClientRequestLineID = clientReqLine.ClientRequestLineID;
                        clientRequestLineVM.InsSubClassID = clientReqLine.InsSubClassID != null ? Convert.ToInt32(clientReqLine.InsSubClassID) : 0;

                        if (clientRequestLineVM.InsSubClassID > 0)
                        {
                            clientRequestLineVM.InsSubClassName = clientReqLine.tblInsSubClass.Description;
                            clientRequestLineVM.InsClassID = clientReqLine.tblInsSubClass.InsClassID != null ? Convert.ToInt32(clientReqLine.tblInsSubClass.InsClassID) : 0;

                            if (clientRequestLineVM.InsClassID > 0)
                            {
                                clientRequestLineVM.InsClassName = clientReqLine.tblInsSubClass.tblInsClass.Code;
                            }
                        }

                        clientRequestLineVM.CreatedBy = clientReqLine.CreatedBy != null ? Convert.ToInt32(clientReqLine.CreatedBy) : 0;
                        clientRequestLineVM.CreatedDate = clientReqLine.CreatedDate != null ? clientReqLine.CreatedDate.ToString() : string.Empty;
                        clientRequestLineVM.ModifiedBy = clientReqLine.ModifiedBy != null ? Convert.ToInt32(clientReqLine.ModifiedBy) : 0;
                        clientRequestLineVM.ModifiedDate = clientReqLine.ModifiedDate != null ? clientReqLine.ModifiedDate.ToString() : string.Empty;

                        //Client Property Details
                        var clientPropertyData = unitOfWork.TblClientPropertyRepository.Get(x => x.ClientRequestLineID == clientReqLine.ClientRequestLineID).ToList();

                        List<ClientPropertyVM> clientPropertyList = new List<ClientPropertyVM>();

                        foreach (var clientProperty in clientPropertyData)
                        {
                            ClientPropertyVM clientPropertyVM = new ClientPropertyVM();
                            clientPropertyVM.ClientPropertyID = clientProperty.ClientPropertyID;
                            clientPropertyVM.ClientPropertyName = clientProperty.ClientPropertyName;
                            clientPropertyVM.BRNo = clientProperty.BRNo;
                            clientPropertyVM.VATNo = clientProperty.VATNo;
                            clientPropertyVM.CreatedBy = clientProperty.CreatedBy != null ? Convert.ToInt32(clientProperty.CreatedBy) : 0;
                            clientPropertyVM.CreatedDate = clientProperty.CreatedDate != null ? clientProperty.CreatedDate.ToString() : string.Empty;
                            clientPropertyVM.ModifiedBy = clientProperty.ModifiedBy != null ? Convert.ToInt32(clientProperty.ModifiedBy) : 0;
                            clientPropertyVM.ModifiedDate = clientProperty.ModifiedDate != null ? clientProperty.ModifiedDate.ToString() : string.Empty;

                            clientPropertyList.Add(clientPropertyVM);
                        }

                        //Client Request Insurance Sub Class Scope Details
                        var clientReqInsSubClassScopeData = unitOfWork.TblClientRequestInsSubClassScopeRepository.Get(x => x.ClientRequestLineID == clientReqLine.ClientRequestLineID).ToList();

                        List<ClientRequestInsSubClassScopeVM> clientReqInsSubClassScopeList = new List<ClientRequestInsSubClassScopeVM>();

                        foreach (var clientReqInsSubClassScope in clientReqInsSubClassScopeData)
                        {
                            ClientRequestInsSubClassScopeVM clientRequestInsSubClassScopeVM = new ClientRequestInsSubClassScopeVM();
                            clientRequestInsSubClassScopeVM.ClientRequestInsSubClassScopeID = clientReqInsSubClassScope.ClientRequestInsSubClassScopeID;
                            clientRequestInsSubClassScopeVM.CommonInsScopeID = clientReqInsSubClassScope.CommonInsScopeID != null ? Convert.ToInt32(clientReqInsSubClassScope.CommonInsScopeID) : 0;

                            if (clientRequestInsSubClassScopeVM.CommonInsScopeID > 0)
                            {
                                clientRequestInsSubClassScopeVM.CommonInsScopeName = clientReqInsSubClassScope.tblCommonInsScope.Description;
                            }

                            clientRequestInsSubClassScopeVM.CreatedBy = clientReqInsSubClassScope.CreatedBy != null ? Convert.ToInt32(clientReqInsSubClassScope.CreatedBy) : 0;
                            clientRequestInsSubClassScopeVM.CreatedDate = clientReqInsSubClassScope.CreatedDate != null ? clientReqInsSubClassScope.CreatedDate.ToString() : string.Empty;
                            clientRequestInsSubClassScopeVM.ModifiedBy = clientReqInsSubClassScope.ModifiedBy != null ? Convert.ToInt32(clientReqInsSubClassScope.ModifiedBy) : 0;
                            clientRequestInsSubClassScopeVM.ModifiedDate = clientReqInsSubClassScope.ModifiedDate != null ? clientReqInsSubClassScope.ModifiedDate.ToString() : string.Empty;

                            clientReqInsSubClassScopeList.Add(clientRequestInsSubClassScopeVM);
                        }

                        clientRequestLineVM.ClientPropertyDetails = clientPropertyList;
                        clientRequestLineVM.ClientRequestInsSubClassScopeDetails = clientReqInsSubClassScopeList;

                        clientRequesLinetList.Add(clientRequestLineVM);
                    }

                    clientRequestHeaderVM.ClientRequestLineDetails = clientRequesLinetList;

                    clientRequestHeaderList.Add(clientRequestHeaderVM);
                }

                return clientRequestHeaderList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ClientRequestHeaderVM> GetAllClientRequestsByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var clientRequestHeaderData = unitOfWork.TblClientRequestHeaderRepository.Get(x => x.tblClient.BUID == businessUnitID).ToList();


             


                List<ClientRequestHeaderVM> clientRequestHeaderList = new List<ClientRequestHeaderVM>();

                foreach (var clientReqHeader in clientRequestHeaderData)
                {
                    ClientRequestHeaderVM clientRequestHeaderVM = new ClientRequestHeaderVM();
                    clientRequestHeaderVM.ClientRequestHeaderID = clientReqHeader.ClientRequestHeaderID;
                    clientRequestHeaderVM.ClientID = clientReqHeader.ClientID != null ? Convert.ToInt32(clientReqHeader.ClientID) : 0;

                    if (clientRequestHeaderVM.ClientID > 0)
                    {
                        clientRequestHeaderVM.ClientName = clientReqHeader.tblClient.ClientName;
                        clientRequestHeaderVM.BusinessUnitID = clientReqHeader.tblClient.BUID != null ? Convert.ToInt32(clientReqHeader.tblClient.BUID) : 0;

                        if (clientRequestHeaderVM.BusinessUnitID > 0)
                        {
                            clientRequestHeaderVM.BusinessUnitName = clientReqHeader.tblClient.tblBussinessUnit.BussinessUnit;
                            clientRequestHeaderVM.CompanyID = clientReqHeader.tblClient.tblBussinessUnit.CompID != null ? Convert.ToInt32(clientReqHeader.tblClient.tblBussinessUnit.CompID) : 0;

                            if (clientRequestHeaderVM.CompanyID > 0)
                            {
                                clientRequestHeaderVM.CompanyName = clientReqHeader.tblClient.tblBussinessUnit.tblCompany.CompanyName;
                            }
                        }
                    }

                    clientRequestHeaderVM.PartnerID = clientReqHeader.PartnerID != null ? Convert.ToInt32(clientReqHeader.PartnerID) : 0;

                    if (clientRequestHeaderVM.PartnerID > 0)
                    {
                        clientRequestHeaderVM.PartnerName = "Partner Name";//clientReqHeader.tblPartner.PartnerName;
                    }

                    clientRequestHeaderVM.RequestedDate = clientReqHeader.RequestedDate != null ? Convert.ToDateTime(clientReqHeader.RequestedDate).ToString("dd/MM/yyyy") : string.Empty;
                    clientRequestHeaderVM.IsQuotationCreated = clientReqHeader.IsQuotationCreated != null ? Convert.ToBoolean(clientReqHeader.IsQuotationCreated) : false;
                    clientRequestHeaderVM.CreatedBy = clientReqHeader.CreatedBy != null ? Convert.ToInt32(clientReqHeader.CreatedBy) : 0;
                    clientRequestHeaderVM.CreatedDate = clientReqHeader.CreatedDate != null ? clientReqHeader.CreatedDate.ToString() : string.Empty;
                    clientRequestHeaderVM.ModifiedBy = clientReqHeader.ModifiedBy != null ? Convert.ToInt32(clientReqHeader.ModifiedBy) : 0;
                    clientRequestHeaderVM.ModifiedDate = clientReqHeader.ModifiedDate != null ? clientReqHeader.ModifiedDate.ToString() : string.Empty;
                    clientRequestHeaderVM.Other = clientReqHeader.Other != null ? clientReqHeader.Other.ToString() : string.Empty;
                    clientRequestHeaderVM.transactionType = clientReqHeader.TransactionID != null ? Convert.ToInt32(clientReqHeader.TransactionID) : 0;
                    clientRequestHeaderVM.FileNo = clientReqHeader.FileNo;
                    clientRequestHeaderVM.InspectionDate = clientReqHeader.InspectionDate != null ? Convert.ToDateTime(clientReqHeader.InspectionDate).ToString("dd/MM/yyyy") : string.Empty;
                    clientRequestHeaderVM.AdditionalNote = clientReqHeader.AdditionalNote;

                    List<ClientRequestLineVM> clientRequesLinetList = new List<ClientRequestLineVM>();


                    //12022019 commented
                    //Client Request Line Details
                    var clientRequestLineData = unitOfWork.TblClientRequestLineRepository.Get(x => x.ClientRequestHeaderID == clientReqHeader.ClientRequestHeaderID).ToList();

                    foreach (var clientReqLine in clientRequestLineData)
                    {
                        ClientRequestLineVM clientRequestLineVM = new ClientRequestLineVM();
                        //clientRequestLineVM.ClientRequestLineID = clientReqLine.ClientRequestLineID;
                        //clientRequestLineVM.InsSubClassID = clientReqLine.InsSubClassID != null ? Convert.ToInt32(clientReqLine.InsSubClassID) : 0;

                        //if (clientRequestLineVM.InsSubClassID > 0)
                        //{
                        //    clientRequestLineVM.InsSubClassName = clientReqLine.tblInsSubClass.Description;
                        //    clientRequestLineVM.InsClassID = clientReqLine.tblInsSubClass.InsClassID != null ? Convert.ToInt32(clientReqLine.tblInsSubClass.InsClassID) : 0;

                        //    if (clientRequestLineVM.InsClassID > 0)
                        //    {
                        //        clientRequestLineVM.InsClassName = clientReqLine.tblInsSubClass.tblInsClass.Code;
                        //    }
                        //}

                        //clientRequestLineVM.CreatedBy = clientReqLine.CreatedBy != null ? Convert.ToInt32(clientReqLine.CreatedBy) : 0;
                        //clientRequestLineVM.CreatedDate = clientReqLine.CreatedDate != null ? clientReqLine.CreatedDate.ToString() : string.Empty;
                        //clientRequestLineVM.ModifiedBy = clientReqLine.ModifiedBy != null ? Convert.ToInt32(clientReqLine.ModifiedBy) : 0;
                        //clientRequestLineVM.ModifiedDate = clientReqLine.ModifiedDate != null ? clientReqLine.ModifiedDate.ToString() : string.Empty;

                        ////Client Property Details
                        //var clientPropertyData = unitOfWork.TblClientPropertyRepository.Get(x => x.ClientRequestLineID == clientReqLine.ClientRequestLineID).ToList();

                        List<ClientPropertyVM> clientPropertyList = new List<ClientPropertyVM>();

                        //foreach (var clientProperty in clientPropertyData)
                        //{
                        //    ClientPropertyVM clientPropertyVM = new ClientPropertyVM();
                        //    clientPropertyVM.ClientPropertyID = clientProperty.ClientPropertyID;
                        //    clientPropertyVM.ClientPropertyName = clientProperty.ClientPropertyName;
                        //    clientPropertyVM.BRNo = clientProperty.BRNo;
                        //    clientPropertyVM.VATNo = clientProperty.VATNo;
                        //    clientPropertyVM.CreatedBy = clientProperty.CreatedBy != null ? Convert.ToInt32(clientProperty.CreatedBy) : 0;
                        //    clientPropertyVM.CreatedDate = clientProperty.CreatedDate != null ? clientProperty.CreatedDate.ToString() : string.Empty;
                        //    clientPropertyVM.ModifiedBy = clientProperty.ModifiedBy != null ? Convert.ToInt32(clientProperty.ModifiedBy) : 0;
                        //    clientPropertyVM.ModifiedDate = clientProperty.ModifiedDate != null ? clientProperty.ModifiedDate.ToString() : string.Empty;

                        //    clientPropertyList.Add(clientPropertyVM);
                        //}

                        //Client Request Insurance Sub Class Scope Details
                        //var clientReqInsSubClassScopeData = unitOfWork.TblClientRequestInsSubClassScopeRepository.Get(x => x.ClientRequestLineID == clientReqLine.ClientRequestLineID).ToList();

                        List<ClientRequestInsSubClassScopeVM> clientReqInsSubClassScopeList = new List<ClientRequestInsSubClassScopeVM>();

                        //foreach (var clientReqInsSubClassScope in clientReqInsSubClassScopeData)
                        //{
                        //    ClientRequestInsSubClassScopeVM clientRequestInsSubClassScopeVM = new ClientRequestInsSubClassScopeVM();
                        //    clientRequestInsSubClassScopeVM.ClientRequestInsSubClassScopeID = clientReqInsSubClassScope.ClientRequestInsSubClassScopeID;
                        //    clientRequestInsSubClassScopeVM.CommonInsScopeID = clientReqInsSubClassScope.CommonInsScopeID != null ? Convert.ToInt32(clientReqInsSubClassScope.CommonInsScopeID) : 0;

                        //    if (clientRequestInsSubClassScopeVM.CommonInsScopeID > 0)
                        //    {
                        //        clientRequestInsSubClassScopeVM.CommonInsScopeName = clientReqInsSubClassScope.tblCommonInsScope.Description;
                        //    }

                        //    clientRequestInsSubClassScopeVM.CreatedBy = clientReqInsSubClassScope.CreatedBy != null ? Convert.ToInt32(clientReqInsSubClassScope.CreatedBy) : 0;
                        //    clientRequestInsSubClassScopeVM.CreatedDate = clientReqInsSubClassScope.CreatedDate != null ? clientReqInsSubClassScope.CreatedDate.ToString() : string.Empty;
                        //    clientRequestInsSubClassScopeVM.ModifiedBy = clientReqInsSubClassScope.ModifiedBy != null ? Convert.ToInt32(clientReqInsSubClassScope.ModifiedBy) : 0;
                        //    clientRequestInsSubClassScopeVM.ModifiedDate = clientReqInsSubClassScope.ModifiedDate != null ? clientReqInsSubClassScope.ModifiedDate.ToString() : string.Empty;

                        //    clientReqInsSubClassScopeList.Add(clientRequestInsSubClassScopeVM);
                        //}

                        //clientRequestLineVM.ClientPropertyDetails = clientPropertyList;
                        //clientRequestLineVM.ClientRequestInsSubClassScopeDetails = clientReqInsSubClassScopeList;

                        //clientRequesLinetList.Add(clientRequestLineVM);
                    }

                    clientRequestHeaderVM.ClientRequestLineDetails = clientRequesLinetList;

                    clientRequestHeaderList.Add(clientRequestHeaderVM);
                }

                return clientRequestHeaderList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ClientRequestHeaderVM GetClientRequestByID(int clientRequestHeaderID)
        {
            try
            {
                var clientRequestHeaderData = unitOfWork.TblClientRequestHeaderRepository.GetByID(clientRequestHeaderID);

                ClientRequestHeaderVM clientRequestHeaderVM = new ClientRequestHeaderVM();
                clientRequestHeaderVM.ClientRequestHeaderID = clientRequestHeaderData.ClientRequestHeaderID;
                clientRequestHeaderVM.ClientID = clientRequestHeaderData.ClientID != null ? Convert.ToInt32(clientRequestHeaderData.ClientID) : 0;
                clientRequestHeaderVM.CurrancyID = clientRequestHeaderData.CurrancyID==null?0:(int)clientRequestHeaderData.CurrancyID;
                if (clientRequestHeaderVM.ClientID > 0)
                {
                    clientRequestHeaderVM.ClientName = clientRequestHeaderData.tblClient.ClientName;
                    clientRequestHeaderVM.BusinessUnitID = clientRequestHeaderData.tblClient.BUID != null ? Convert.ToInt32(clientRequestHeaderData.tblClient.BUID) : 0;

                        if (clientRequestHeaderVM.BusinessUnitID > 0)
                    {
                        clientRequestHeaderVM.BusinessUnitName = clientRequestHeaderData.tblClient.tblBussinessUnit.BussinessUnit;
                        clientRequestHeaderVM.CompanyID = clientRequestHeaderData.tblClient.tblBussinessUnit.CompID != null ? Convert.ToInt32(clientRequestHeaderData.tblClient.tblBussinessUnit.CompID) : 0;

                        if (clientRequestHeaderVM.CompanyID > 0)
                        {
                            clientRequestHeaderVM.CompanyName = clientRequestHeaderData.tblClient.tblBussinessUnit.tblCompany.CompanyName;
                        }
                    }
                }

                clientRequestHeaderVM.PartnerID = clientRequestHeaderData.PartnerID != null ? Convert.ToInt32(clientRequestHeaderData.PartnerID) : 0;

                if (clientRequestHeaderVM.PartnerID > 0)
                {
                    clientRequestHeaderVM.PartnerName = "Partner Name"; //clientRequestHeaderData.tblPartner.PartnerName;

                    if (clientRequestHeaderVM.PartnerName == "Agent")
                    {
                        var AgentData = unitOfWork.TblAgentRepository.GetByID(clientRequestHeaderVM.AgentID);
                        clientRequestHeaderVM.PremiumName = AgentData.AgentName;
                    }
                    if (clientRequestHeaderVM.PartnerName == "Introducer")
                    {
                        var IntroducerData = unitOfWork.TblIntroducerRepository.GetByID(clientRequestHeaderVM.AgentID);
                        clientRequestHeaderVM.PremiumName = IntroducerData.IntroducerName;
                    }


                    if (clientRequestHeaderVM.PartnerID == 1)
                    {
                        var AgentData = unitOfWork.TblAgentRepository.GetByID(clientRequestHeaderData.AgentID);
                        clientRequestHeaderVM.AgentID = AgentData.AgentID < 0 ? 0 : AgentData.AgentID;
                        clientRequestHeaderVM.PremiumName = AgentData.AgentName;
                    }
                    if (clientRequestHeaderVM.PartnerID == 2)
                    {
                        var EmployeeData = unitOfWork.TblEmployeeRepository.GetByID(clientRequestHeaderData.EmployeeID);
                        clientRequestHeaderVM.EmployeeID = EmployeeData.EmpID <0 ? 0 : EmployeeData.EmpID;
                        clientRequestHeaderVM.EmployeeName = EmployeeData.EmpName;
                    }
                    if (clientRequestHeaderVM.PartnerID == 3)
                    {
                        var AgentData = unitOfWork.TblAgentRepository.GetByID(clientRequestHeaderData.AgentID);
                        clientRequestHeaderVM.AgentID = AgentData.AgentID < 0 ? 0 : AgentData.AgentID;
                        clientRequestHeaderVM.AgentName = AgentData.AgentName;
                    }
                    if (clientRequestHeaderVM.PartnerID == 4)
                    {
                        var IntroducerData = unitOfWork.TblIntroducerRepository.GetByID(clientRequestHeaderData.IntroducerID);
                        clientRequestHeaderVM.IntroducerID = IntroducerData.IntroducerID < 0 ? 0 : IntroducerData.IntroducerID;
                        clientRequestHeaderVM.IntroducerName = IntroducerData.IntroducerName;
                    }

                 

                }

                clientRequestHeaderVM.RequestedDate = clientRequestHeaderData.RequestedDate != null ? Convert.ToDateTime(clientRequestHeaderData.RequestedDate).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.IsQuotationCreated = clientRequestHeaderData.IsQuotationCreated != null ? Convert.ToBoolean(clientRequestHeaderData.IsQuotationCreated) : false;
                clientRequestHeaderVM.CreatedBy = clientRequestHeaderData.CreatedBy != null ? Convert.ToInt32(clientRequestHeaderData.CreatedBy) : 0;
                clientRequestHeaderVM.CreatedDate = clientRequestHeaderData.CreatedDate != null ? clientRequestHeaderData.CreatedDate.ToString() : string.Empty;
                clientRequestHeaderVM.ModifiedBy = clientRequestHeaderData.ModifiedBy != null ? Convert.ToInt32(clientRequestHeaderData.ModifiedBy) : 0;
                clientRequestHeaderVM.ModifiedDate = clientRequestHeaderData.ModifiedDate != null ? clientRequestHeaderData.ModifiedDate.ToString() : string.Empty;
                clientRequestHeaderVM.FileNo = clientRequestHeaderData.AdditionalNote != null ? clientRequestHeaderData.AdditionalNote.ToString() : string.Empty;
                clientRequestHeaderVM.InspectionDate= clientRequestHeaderData.InspectionDate != null ? clientRequestHeaderData.InspectionDate.ToString() : string.Empty;

                //Client Request Line Details
                var clientRequestLineData = unitOfWork.TblClientRequestLineRepository.Get(x => x.ClientRequestHeaderID == clientRequestHeaderData.ClientRequestHeaderID).ToList();

                List<ClientRequestLineVM> clientRequesLinetList = new List<ClientRequestLineVM>();

                foreach (var clientReqLine in clientRequestLineData)
                {
                    ClientRequestLineVM clientRequestLineVM = new ClientRequestLineVM();
                    clientRequestLineVM.ClientRequestLineID = clientReqLine.ClientRequestLineID;
                    clientRequestLineVM.InsSubClassID = clientReqLine.InsSubClassID != null ? Convert.ToInt32(clientReqLine.InsSubClassID) : 0;

                    if (clientRequestLineVM.InsSubClassID > 0)
                    {
                        clientRequestLineVM.InsSubClassName = clientReqLine.tblInsSubClass.Description;
                        clientRequestLineVM.InsClassID = clientReqLine.tblInsSubClass.InsClassID != null ? Convert.ToInt32(clientReqLine.tblInsSubClass.InsClassID) : 0;

                        if (clientRequestLineVM.InsClassID > 0)
                        {
                            clientRequestLineVM.InsClassName = clientReqLine.tblInsSubClass.tblInsClass.Code;
                        }
                    }

                    clientRequestLineVM.CreatedBy = clientReqLine.CreatedBy != null ? Convert.ToInt32(clientReqLine.CreatedBy) : 0;
                    clientRequestLineVM.CreatedDate = clientReqLine.CreatedDate != null ? clientReqLine.CreatedDate.ToString() : string.Empty;
                    clientRequestLineVM.ModifiedBy = clientReqLine.ModifiedBy != null ? Convert.ToInt32(clientReqLine.ModifiedBy) : 0;
                    clientRequestLineVM.ModifiedDate = clientReqLine.ModifiedDate != null ? clientReqLine.ModifiedDate.ToString() : string.Empty;

                    //Client Property Details
                    var clientPropertyData = unitOfWork.TblClientPropertyRepository.Get(x => x.ClientRequestLineID == clientReqLine.ClientRequestLineID).ToList();

                    List<ClientPropertyVM> clientPropertyList = new List<ClientPropertyVM>();

                    foreach (var clientProperty in clientPropertyData)
                    {
                        ClientPropertyVM clientPropertyVM = new ClientPropertyVM();
                        clientPropertyVM.ClientPropertyID = clientProperty.ClientPropertyID;
                        clientPropertyVM.ClientPropertyName = clientProperty.ClientPropertyName;
                        clientPropertyVM.BRNo = clientProperty.BRNo;
                        clientPropertyVM.VATNo = clientProperty.VATNo;
                        clientPropertyVM.CreatedBy = clientProperty.CreatedBy != null ? Convert.ToInt32(clientProperty.CreatedBy) : 0;
                        clientPropertyVM.CreatedDate = clientProperty.CreatedDate != null ? clientProperty.CreatedDate.ToString() : string.Empty;
                        clientPropertyVM.ModifiedBy = clientProperty.ModifiedBy != null ? Convert.ToInt32(clientProperty.ModifiedBy) : 0;
                        clientPropertyVM.ModifiedDate = clientProperty.ModifiedDate != null ? clientProperty.ModifiedDate.ToString() : string.Empty;

                        clientPropertyList.Add(clientPropertyVM);
                    }

                    //Client Request Insurance Sub Class Scope Details
                    var clientReqInsSubClassScopeData = unitOfWork.TblClientRequestInsSubClassScopeRepository.Get(x => x.ClientRequestLineID == clientReqLine.ClientRequestLineID).ToList();

                    List<ClientRequestInsSubClassScopeVM> clientReqInsSubClassScopeList = new List<ClientRequestInsSubClassScopeVM>();

                    foreach (var clientReqInsSubClassScope in clientReqInsSubClassScopeData)
                    {
                        ClientRequestInsSubClassScopeVM clientRequestInsSubClassScopeVM = new ClientRequestInsSubClassScopeVM();
                        clientRequestInsSubClassScopeVM.ClientRequestInsSubClassScopeID = clientReqInsSubClassScope.ClientRequestInsSubClassScopeID;
                        clientRequestInsSubClassScopeVM.CommonInsScopeID = clientReqInsSubClassScope.CommonInsScopeID != null ? Convert.ToInt32(clientReqInsSubClassScope.CommonInsScopeID) : 0;

                        if (clientRequestInsSubClassScopeVM.CommonInsScopeID > 0)
                        {
                            clientRequestInsSubClassScopeVM.CommonInsScopeName = clientReqInsSubClassScope.tblCommonInsScope.Description;
                        }

                        clientRequestInsSubClassScopeVM.CreatedBy = clientReqInsSubClassScope.CreatedBy != null ? Convert.ToInt32(clientReqInsSubClassScope.CreatedBy) : 0;
                        clientRequestInsSubClassScopeVM.CreatedDate = clientReqInsSubClassScope.CreatedDate != null ? clientReqInsSubClassScope.CreatedDate.ToString() : string.Empty;
                        clientRequestInsSubClassScopeVM.ModifiedBy = clientReqInsSubClassScope.ModifiedBy != null ? Convert.ToInt32(clientReqInsSubClassScope.ModifiedBy) : 0;
                        clientRequestInsSubClassScopeVM.ModifiedDate = clientReqInsSubClassScope.ModifiedDate != null ? clientReqInsSubClassScope.ModifiedDate.ToString() : string.Empty;

                        clientReqInsSubClassScopeList.Add(clientRequestInsSubClassScopeVM);
                    }

                    clientRequestLineVM.ClientPropertyDetails = clientPropertyList;
                    clientRequestLineVM.ClientRequestInsSubClassScopeDetails = clientReqInsSubClassScopeList;

                    clientRequesLinetList.Add(clientRequestLineVM);
                }

                clientRequestHeaderVM.ClientRequestLineDetails = clientRequesLinetList;

                return clientRequestHeaderVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion


        #region Client Request BUPA

        public bool SaveAvivaRequest(ClientRequestVM clientRequestVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {



                    int clientID = 0;
                    clientRequestVM.ClientRequestHeaderDetails.transactionType = 1;

                    if (clientRequestVM.IsClientAdded == false)
                    {
                        clientRequestVM.IsClientUpdated = true;
                    }

                    if (clientRequestVM.IsClientUpdated)
                    {
                        clientID = clientRequestVM.ClientDetails.ClientID;

                        tblClient client = unitOfWork.TblClientRepository.GetByID(clientID);
                        //client.ClientName = clientRequestVM.ClientDetails.ClientName;
                        //client.ClientAddress = clientRequestVM.ClientDetails.ClientAddress;
                        //client.NIC = clientRequestVM.ClientDetails.NIC;
                        //client.ContactNo = clientRequestVM.ClientDetails.ContactNo;
                        //client.FixedLine = clientRequestVM.ClientDetails.FixedLine;
                        //client.Email = clientRequestVM.ClientDetails.Email;
                        //client.DOB = !string.IsNullOrEmpty(clientRequestVM.ClientDetails.DOB) ? DateTime.ParseExact(clientRequestVM.ClientDetails.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        //client.PPID = clientRequestVM.ClientDetails.PPID;
                        //client.FamilyDiscount = clientRequestVM.ClientDetails.FamilyDiscount;
                        //client.AdditionalNote = clientRequestVM.ClientDetails.AdditionalNote;
                        //client.InspectionDate = !string.IsNullOrEmpty(clientRequestVM.ClientDetails.InspectionDate) ? DateTime.ParseExact(clientRequestVM.ClientDetails.InspectionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        //client.HomeCountryID = clientRequestVM.ClientDetails.HomeCountryID;
                        //client.ResidentCountryID = clientRequestVM.ClientDetails.ResidentCountryID;
                        //client.BUID = clientRequestVM.ClientDetails.BusinessUnitID;
                        //client.ModifiedBy = clientRequestVM.UserID;
                        //client.ModifiedDate = DateTime.Now;
                        //client.TitleID = clientRequestVM.TitleID;

                        client.TitleID = clientRequestVM.ClientDetails.TitleID < 0 ? 0 : clientRequestVM.ClientDetails.TitleID;
                        client.ClientName = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientName) ? "" : clientRequestVM.ClientDetails.ClientName;
                        client.ExtraText = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientOtherName) ? "" : clientRequestVM.ClientDetails.ClientOtherName;
                        client.ClientAddress = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientAddress) ? "" : clientRequestVM.ClientDetails.ClientAddress;
                        client.NIC = string.IsNullOrEmpty(clientRequestVM.ClientDetails.NIC) ? "" : clientRequestVM.ClientDetails.NIC;
                        client.ContactNo = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ContactNo) ? "" : clientRequestVM.ClientDetails.ContactNo;
                        client.FixedLine = string.IsNullOrEmpty(clientRequestVM.ClientDetails.FixedLine) ? "" : clientRequestVM.ClientDetails.FixedLine;
                        client.Email = string.IsNullOrEmpty(clientRequestVM.ClientDetails.Email) ? "" : clientRequestVM.ClientDetails.Email;
                        client.DOB = !string.IsNullOrEmpty(clientRequestVM.ClientDetails.DOB) ? DateTime.ParseExact(clientRequestVM.ClientDetails.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        client.PPID = string.IsNullOrEmpty(clientRequestVM.ClientDetails.PPID) ? "" : clientRequestVM.ClientDetails.PPID;
                        client.FamilyDiscount = clientRequestVM.ClientDetails.FamilyDiscount < 0 ? 0 : clientRequestVM.ClientDetails.FamilyDiscount;
                        client.AdditionalNote = string.IsNullOrEmpty(clientRequestVM.ClientDetails.AdditionalNote) ? "" : clientRequestVM.ClientDetails.AdditionalNote;
                        client.HomeCountryID = clientRequestVM.ClientDetails.HomeCountryID < 0 ? 0 : clientRequestVM.ClientDetails.HomeCountryID;
                        client.ResidentCountryID = clientRequestVM.ClientDetails.ResidentCountryID < 0 ? 0 : clientRequestVM.ClientDetails.ResidentCountryID;
                        client.BUID = clientRequestVM.ClientDetails.BusinessUnitID < 0 ? 0 : clientRequestVM.ClientDetails.BusinessUnitID;
                        client.JoinDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.JoinDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        client.ModifiedBy = clientRequestVM.UserID;
                        client.ModifiedDate = DateTime.Now;
                        //client.TitleID = clientRequestVM.TitleID;

                        unitOfWork.TblClientRepository.Update(client);
                        unitOfWork.Save();
                        //if (clientRequestVM.ClientDetails.FamilyDetails != null)
                        //{
                        //    foreach (var requestLine in clientRequestVM.ClientDetails.FamilyDetails)
                        //    {
                        //        tblFamilyMember tblFamilyDetail = new tblFamilyMember();
                        //        tblFamilyDetail.ClientID = clientRequestVM.ClientDetails.ClientID;
                        //        tblFamilyDetail.MemberName = requestLine.MemberName;
                        //        tblFamilyDetail.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        //        tblFamilyDetail.NICNo = requestLine.NIC;
                        //        tblFamilyDetail.ContactNo = requestLine.ContactNo;
                        //        //clientRequestLine.CreatedDate = DateTime.Now;
                        //        unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyDetail);
                        //        unitOfWork.Save();
                        //    } 
                        //        }

                        clientID = client.ClientID;
                        if (clientRequestVM.ClientDetails.FamilyDetails != null)
                        {
                            foreach (var requestLine in clientRequestVM.ClientDetails.FamilyDetails)
                            {
                                tblFamilyMember tblFamilyMember = new tblFamilyMember();
                                tblFamilyMember.ClientID = clientID;
                                tblFamilyMember.Title = requestLine.TitleID < 0 ? 0 : requestLine.TitleID;
                                tblFamilyMember.MemberName = string.IsNullOrEmpty(requestLine.MemberName) ? "" : requestLine.MemberName;
                                tblFamilyMember.MemberOtherName = string.IsNullOrEmpty(requestLine.MemberOtherName) ? "" : requestLine.MemberOtherName;
                                tblFamilyMember.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyMember.JoinDate = !string.IsNullOrEmpty(requestLine.JoinDate) ? DateTime.ParseExact(requestLine.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyMember.NICNo = !string.IsNullOrEmpty(requestLine.NIC) ? requestLine.NIC : "";
                                tblFamilyMember.ContactNo = !string.IsNullOrEmpty(requestLine.ContactNo) ? requestLine.ContactNo : "";
                                tblFamilyMember.HomeCountry = requestLine.MemberCountryID < 0 ? 0 : requestLine.MemberCountryID;
                                tblFamilyMember.CountryOfResident = requestLine.MemberResCountryID < 0 ? 0 : requestLine.MemberResCountryID;
                                tblFamilyMember.RelationShipID = requestLine.RelationShipID < 0 ? 0 : requestLine.RelationShipID;
                                tblFamilyMember.GenderID = requestLine.GenderID < 0 ? 0 : requestLine.GenderID;
                                unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyMember);
                                unitOfWork.Save();
                            }
                        }
                    }
                    else if (clientRequestVM.IsClientAdded)
                    {
                        //Save Client
                        tblClient client = new tblClient();

                        client.TitleID = clientRequestVM.ClientDetails.TitleID < 0 ? 0 : clientRequestVM.ClientDetails.TitleID;
                        client.ClientName = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientName) ? "" : clientRequestVM.ClientDetails.ClientName;
                        client.ExtraText = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientOtherName) ? "" : clientRequestVM.ClientDetails.ClientOtherName;
                        client.ClientAddress = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientAddress) ? "" : clientRequestVM.ClientDetails.ClientAddress;
                        client.NIC = string.IsNullOrEmpty(clientRequestVM.ClientDetails.NIC) ? "" : clientRequestVM.ClientDetails.NIC;
                        client.ContactNo = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ContactNo) ? "" : clientRequestVM.ClientDetails.ContactNo;
                        client.FixedLine = string.IsNullOrEmpty(clientRequestVM.ClientDetails.FixedLine) ? "" : clientRequestVM.ClientDetails.FixedLine;
                        client.Email = string.IsNullOrEmpty(clientRequestVM.ClientDetails.Email) ? "" : clientRequestVM.ClientDetails.Email;
                        client.DOB = !string.IsNullOrEmpty(clientRequestVM.ClientDetails.DOB) ? DateTime.ParseExact(clientRequestVM.ClientDetails.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        client.PPID = string.IsNullOrEmpty(clientRequestVM.ClientDetails.PPID) ? "" : clientRequestVM.ClientDetails.PPID;
                        client.FamilyDiscount = clientRequestVM.ClientDetails.FamilyDiscount < 0 ? 0 : clientRequestVM.ClientDetails.FamilyDiscount;
                        client.AdditionalNote = string.IsNullOrEmpty(clientRequestVM.ClientDetails.AdditionalNote) ? "" : clientRequestVM.ClientDetails.AdditionalNote;
                        client.HomeCountryID = clientRequestVM.ClientDetails.HomeCountryID < 0 ? 0 : clientRequestVM.ClientDetails.HomeCountryID;
                        client.ResidentCountryID = clientRequestVM.ClientDetails.ResidentCountryID < 0 ? 0 : clientRequestVM.ClientDetails.ResidentCountryID;
                        client.BUID = clientRequestVM.ClientDetails.BusinessUnitID < 0 ? 0 : clientRequestVM.ClientDetails.BusinessUnitID;
                        client.JoinDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.JoinDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        client.CreatedBy = clientRequestVM.UserID;
                        client.CreatedDate = DateTime.Now;
                        //client.TitleID = clientRequestVM.TitleID;
                        unitOfWork.TblClientRepository.Insert(client);
                        unitOfWork.Save();




                        clientID = client.ClientID;
                        if (clientRequestVM.ClientDetails.FamilyDetails != null)
                        {
                            foreach (var requestLine in clientRequestVM.ClientDetails.FamilyDetails)
                            {
                                tblFamilyMember tblFamilyDetail = new tblFamilyMember();
                                tblFamilyDetail.ClientID = clientID;
                                tblFamilyDetail.Title = requestLine.TitleID < 0 ? 0 : requestLine.TitleID;
                                tblFamilyDetail.MemberName = string.IsNullOrEmpty(requestLine.MemberName) ? "" : requestLine.MemberName;
                                tblFamilyDetail.MemberOtherName = string.IsNullOrEmpty(requestLine.MemberOtherName) ? "" : requestLine.MemberOtherName;
                                tblFamilyDetail.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyDetail.JoinDate = !string.IsNullOrEmpty(requestLine.JoinDate) ? DateTime.ParseExact(requestLine.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyDetail.NICNo = !string.IsNullOrEmpty(requestLine.NIC) ? requestLine.NIC : "";
                                tblFamilyDetail.ContactNo = !string.IsNullOrEmpty(requestLine.ContactNo) ? requestLine.ContactNo : "";
                                tblFamilyDetail.HomeCountry = requestLine.MemberCountryID < 0 ? 0 : requestLine.MemberCountryID;
                                tblFamilyDetail.CountryOfResident = requestLine.MemberResCountryID < 0 ? 0 : requestLine.MemberResCountryID;
                                tblFamilyDetail.RelationShipID = requestLine.RelationShipID < 0 ? 0 : requestLine.RelationShipID;
                                tblFamilyDetail.GenderID = requestLine.GenderID < 0 ? 0 : requestLine.GenderID;
                                unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyDetail);
                                unitOfWork.Save();
                                //clientRequestLine.CreatedDate = DateTime.Now;


                                //int famId = tblFamilyDetail.FamilyMemberID;


                                //tblDeduction deduction = new tblDeduction();
                                //deduction.ClientID = clientID;
                                //deduction.LodingRate = clientRequestVM.ClientDetails.LoadnigRate;
                                //deduction.DeductionRate = 0;
                                //deduction.FamilyMemberID = famId;
                                //unitOfWork.TblDeductionRepository.Insert(deduction);
                                //unitOfWork.Save();
                            }

                        }



                    }
                    else
                    {
                        clientID = clientRequestVM.ClientDetails.ClientID;
                        clientRequestVM.ClientRequestHeaderDetails.transactionType = 1;
                    }

                    //Save Client Request Header
                    tblClientRequestHeader clientRequestHeader = new tblClientRequestHeader();
                    clientRequestHeader.ClientID = clientID;
                    clientRequestHeader.PartnerID = clientRequestVM.ClientRequestHeaderDetails.PartnerID;
                    clientRequestHeader.RequestedDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.RequestedDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.RequestedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    clientRequestHeader.CreatedBy = clientRequestVM.UserID;
                    clientRequestHeader.CreatedDate = DateTime.Now;
                    clientRequestHeader.ModifiedBy = clientRequestVM.ClientRequestHeaderDetails.AgentID;
                    clientRequestHeader.InspectionDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.InspectionDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.InspectionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    clientRequestHeader.AdditionalNote = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.AdditionalNote) ? clientRequestVM.ClientRequestHeaderDetails.AdditionalNote : "";
                    clientRequestHeader.FileNo = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.FileNo) ? clientRequestVM.ClientRequestHeaderDetails.FileNo : "";
                    //clientRequestHeader.PolicyStart = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    //clientRequestHeader.PolicyEnd = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    //clientRequestHeader.PolicyStart = DateTime.Parse(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate);
                    clientRequestHeader.PolicyStart = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    clientRequestHeader.PolicyEnd = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    clientRequestHeader.TransactionID = clientRequestVM.ClientRequestHeaderDetails.transactionType = 1;
                    clientRequestHeader.JoinDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.JoinDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    unitOfWork.TblClientRequestHeaderRepository.Insert(clientRequestHeader);
                    unitOfWork.Save();

                    //tblDeduction deduction = new tblDeduction();
                    //deduction.ClientID = clientID;
                    //deduction.LodingRate = clientRequestVM.ClientDetails.LoadnigRate;
                    //deduction.DeductionRate = clientRequestVM.ClientDetails.DeductionRate;
                    //unitOfWork.TblDeductionRepository.Insert(deduction);
                    //unitOfWork.Save();

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
        public bool SaveRequest(ClientRequestVM clientRequestVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {



                    int clientID = 0;
                    clientRequestVM.ClientRequestHeaderDetails.transactionType = 1;

                    if (clientRequestVM.IsClientAdded == false)
                    {
                        clientRequestVM.IsClientUpdated = true;
                    }

                    if (clientRequestVM.IsClientUpdated)
                    {
                        clientID = clientRequestVM.ClientDetails.ClientID;

                        tblClient client = unitOfWork.TblClientRepository.GetByID(clientID);
                        //client.ClientName = clientRequestVM.ClientDetails.ClientName;
                        //client.ClientAddress = clientRequestVM.ClientDetails.ClientAddress;
                        //client.NIC = clientRequestVM.ClientDetails.NIC;
                        //client.ContactNo = clientRequestVM.ClientDetails.ContactNo;
                        //client.FixedLine = clientRequestVM.ClientDetails.FixedLine;
                        //client.Email = clientRequestVM.ClientDetails.Email;
                        //client.DOB = !string.IsNullOrEmpty(clientRequestVM.ClientDetails.DOB) ? DateTime.ParseExact(clientRequestVM.ClientDetails.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        //client.PPID = clientRequestVM.ClientDetails.PPID;
                        //client.FamilyDiscount = clientRequestVM.ClientDetails.FamilyDiscount;
                        //client.AdditionalNote = clientRequestVM.ClientDetails.AdditionalNote;
                        //client.InspectionDate = !string.IsNullOrEmpty(clientRequestVM.ClientDetails.InspectionDate) ? DateTime.ParseExact(clientRequestVM.ClientDetails.InspectionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        //client.HomeCountryID = clientRequestVM.ClientDetails.HomeCountryID;
                        //client.ResidentCountryID = clientRequestVM.ClientDetails.ResidentCountryID;
                        //client.BUID = clientRequestVM.ClientDetails.BusinessUnitID;
                        //client.ModifiedBy = clientRequestVM.UserID;
                        //client.ModifiedDate = DateTime.Now;
                        //client.TitleID = clientRequestVM.TitleID;

                        client.TitleID = clientRequestVM.ClientDetails.TitleID < 0 ? 0 : clientRequestVM.ClientDetails.TitleID;
                        client.ClientName = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientName) ? "" : clientRequestVM.ClientDetails.ClientName;
                        client.ExtraText = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientOtherName) ? "" : clientRequestVM.ClientDetails.ClientOtherName;
                        client.ClientAddress = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientAddress) ? "" : clientRequestVM.ClientDetails.ClientAddress;
                        client.NIC = string.IsNullOrEmpty(clientRequestVM.ClientDetails.NIC) ? "" : clientRequestVM.ClientDetails.NIC;
                        client.ContactNo = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ContactNo) ? "" : clientRequestVM.ClientDetails.ContactNo;
                        client.FixedLine = string.IsNullOrEmpty(clientRequestVM.ClientDetails.FixedLine) ? "" : clientRequestVM.ClientDetails.FixedLine;
                        client.Email = string.IsNullOrEmpty(clientRequestVM.ClientDetails.Email) ? "" : clientRequestVM.ClientDetails.Email;
                        client.DOB = !string.IsNullOrEmpty(clientRequestVM.ClientDetails.DOB) ? DateTime.ParseExact(clientRequestVM.ClientDetails.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        client.PPID = string.IsNullOrEmpty(clientRequestVM.ClientDetails.PPID) ? "" : clientRequestVM.ClientDetails.PPID;
                        client.FamilyDiscount = clientRequestVM.ClientDetails.FamilyDiscount < 0 ? 0 : clientRequestVM.ClientDetails.FamilyDiscount;
                        client.AdditionalNote = string.IsNullOrEmpty(clientRequestVM.ClientDetails.AdditionalNote) ? "" : clientRequestVM.ClientDetails.AdditionalNote;
                        client.HomeCountryID = clientRequestVM.ClientDetails.HomeCountryID < 0 ? 0 : clientRequestVM.ClientDetails.HomeCountryID;
                        client.ResidentCountryID = clientRequestVM.ClientDetails.ResidentCountryID < 0 ? 0 : clientRequestVM.ClientDetails.ResidentCountryID;
                        client.BUID = clientRequestVM.ClientDetails.BusinessUnitID < 0 ? 0 : clientRequestVM.ClientDetails.BusinessUnitID;
                        client.JoinDate= !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.JoinDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        client.ModifiedBy = clientRequestVM.UserID;
                        client.ModifiedDate = DateTime.Now;
                        //client.TitleID = clientRequestVM.TitleID;

                        unitOfWork.TblClientRepository.Update(client);
                        unitOfWork.Save();
                        //if (clientRequestVM.ClientDetails.FamilyDetails != null)
                        //{
                        //    foreach (var requestLine in clientRequestVM.ClientDetails.FamilyDetails)
                        //    {
                        //        tblFamilyMember tblFamilyDetail = new tblFamilyMember();
                        //        tblFamilyDetail.ClientID = clientRequestVM.ClientDetails.ClientID;
                        //        tblFamilyDetail.MemberName = requestLine.MemberName;
                        //        tblFamilyDetail.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        //        tblFamilyDetail.NICNo = requestLine.NIC;
                        //        tblFamilyDetail.ContactNo = requestLine.ContactNo;
                        //        //clientRequestLine.CreatedDate = DateTime.Now;
                        //        unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyDetail);
                        //        unitOfWork.Save();
                        //    } 
                        //        }

                        clientID = client.ClientID;
                        if (clientRequestVM.ClientDetails.FamilyDetails != null)
                        {
                            foreach (var requestLine in clientRequestVM.ClientDetails.FamilyDetails)
                            {
                                tblFamilyMember tblFamilyMember = new tblFamilyMember();
                                tblFamilyMember.ClientID = clientID;
                                tblFamilyMember.Title = requestLine.TitleID < 0 ? 0 : requestLine.TitleID;
                                tblFamilyMember.MemberName = string.IsNullOrEmpty(requestLine.MemberName) ? "" : requestLine.MemberName;
                                tblFamilyMember.MemberOtherName = string.IsNullOrEmpty(requestLine.MemberOtherName) ? "" : requestLine.MemberOtherName;
                                tblFamilyMember.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyMember.JoinDate = !string.IsNullOrEmpty(requestLine.JoinDate) ? DateTime.ParseExact(requestLine.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyMember.NICNo = !string.IsNullOrEmpty(requestLine.NIC) ? requestLine.NIC : "";
                                tblFamilyMember.ContactNo = !string.IsNullOrEmpty(requestLine.ContactNo) ? requestLine.ContactNo : "";
                                tblFamilyMember.HomeCountry = requestLine.MemberCountryID < 0 ? 0 : requestLine.MemberCountryID;
                                tblFamilyMember.CountryOfResident = requestLine.MemberResCountryID < 0 ? 0 : requestLine.MemberResCountryID;
                                tblFamilyMember.RelationShipID = requestLine.RelationShipID < 0 ? 0 : requestLine.RelationShipID;
                                tblFamilyMember.GenderID = requestLine.GenderID < 0 ? 0 : requestLine.GenderID;
                                unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyMember);
                                unitOfWork.Save();
                            }
                        }
                    }
                    else if (clientRequestVM.IsClientAdded)
                    {
                        //Save Client
                        tblClient client = new tblClient();

                        client.TitleID = clientRequestVM.ClientDetails.TitleID < 0 ? 0 : clientRequestVM.ClientDetails.TitleID;
                        client.ClientName = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientName) ? "" : clientRequestVM.ClientDetails.ClientName;
                        client.ExtraText = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientOtherName) ? "" : clientRequestVM.ClientDetails.ClientOtherName;
                        client.ClientAddress = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientAddress) ? "" : clientRequestVM.ClientDetails.ClientAddress;
                        client.NIC = string.IsNullOrEmpty(clientRequestVM.ClientDetails.NIC) ? "" : clientRequestVM.ClientDetails.NIC;
                        client.ContactNo = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ContactNo) ? "" : clientRequestVM.ClientDetails.ContactNo;
                        client.FixedLine = string.IsNullOrEmpty(clientRequestVM.ClientDetails.FixedLine) ? "" : clientRequestVM.ClientDetails.FixedLine;
                        client.Email = string.IsNullOrEmpty(clientRequestVM.ClientDetails.Email) ? "" : clientRequestVM.ClientDetails.Email;
                        client.DOB = !string.IsNullOrEmpty(clientRequestVM.ClientDetails.DOB) ? DateTime.ParseExact(clientRequestVM.ClientDetails.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        client.PPID = string.IsNullOrEmpty(clientRequestVM.ClientDetails.PPID) ? "" : clientRequestVM.ClientDetails.PPID;
                        client.FamilyDiscount = clientRequestVM.ClientDetails.FamilyDiscount < 0 ? 0 : clientRequestVM.ClientDetails.FamilyDiscount;
                        client.AdditionalNote = string.IsNullOrEmpty(clientRequestVM.ClientDetails.AdditionalNote) ? "" : clientRequestVM.ClientDetails.AdditionalNote;
                        client.HomeCountryID = clientRequestVM.ClientDetails.HomeCountryID < 0 ? 0 : clientRequestVM.ClientDetails.HomeCountryID;
                        client.ResidentCountryID = clientRequestVM.ClientDetails.ResidentCountryID < 0 ? 0 : clientRequestVM.ClientDetails.ResidentCountryID;
                        client.BUID = clientRequestVM.ClientDetails.BusinessUnitID < 0 ? 0 : clientRequestVM.ClientDetails.BusinessUnitID;
                        client.JoinDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.JoinDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        client.CreatedBy = clientRequestVM.UserID;
                        client.CreatedDate = DateTime.Now;
                        //client.TitleID = clientRequestVM.TitleID;
                        unitOfWork.TblClientRepository.Insert(client);
                        unitOfWork.Save();


                        

                        clientID = client.ClientID;
                        if (clientRequestVM.ClientRequestHeaderDetails.FamilyDetails != null)
                        {
                            foreach (var requestLine in clientRequestVM.ClientRequestHeaderDetails.FamilyDetails)
                            {
                                tblFamilyMember tblFamilyDetail = new tblFamilyMember();
                                tblFamilyDetail.ClientID = clientID;
                                tblFamilyDetail.Title = requestLine.TitleID < 0 ? 0 : requestLine.TitleID;
                                tblFamilyDetail.MemberName = string.IsNullOrEmpty(requestLine.MemberName) ? "" : requestLine.MemberName;
                                tblFamilyDetail.MemberOtherName = string.IsNullOrEmpty(requestLine.MemberOtherName) ? "" : requestLine.MemberOtherName;
                                tblFamilyDetail.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyDetail.JoinDate = !string.IsNullOrEmpty(requestLine.JoinDate) ? DateTime.ParseExact(requestLine.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyDetail.NICNo = !string.IsNullOrEmpty(requestLine.NIC) ? requestLine.NIC : "";
                                tblFamilyDetail.ContactNo = !string.IsNullOrEmpty(requestLine.ContactNo) ? requestLine.ContactNo : "";
                                tblFamilyDetail.HomeCountry = requestLine.MemberCountryID < 0 ? 0 : requestLine.MemberCountryID;
                                tblFamilyDetail.CountryOfResident = requestLine.MemberResCountryID < 0 ? 0 : requestLine.MemberResCountryID;
                                tblFamilyDetail.RelationShipID = requestLine.RelationShipID < 0 ? 0 : requestLine.RelationShipID;
                                tblFamilyDetail.GenderID = requestLine.GenderID < 0 ? 0 : requestLine.GenderID;
                                unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyDetail);
                                unitOfWork.Save();
                                //clientRequestLine.CreatedDate = DateTime.Now;
                               

                                //int famId = tblFamilyDetail.FamilyMemberID;


                                //tblDeduction deduction = new tblDeduction();
                                //deduction.ClientID = clientID;
                                //deduction.LodingRate = clientRequestVM.ClientDetails.LoadnigRate;
                                //deduction.DeductionRate = 0;
                                //deduction.FamilyMemberID = famId;
                                //unitOfWork.TblDeductionRepository.Insert(deduction);
                                //unitOfWork.Save();
                            }

                        }
                        
                        

                    }
                    else
                    {
                        clientID = clientRequestVM.ClientDetails.ClientID;
                        clientRequestVM.ClientRequestHeaderDetails.transactionType = 1;
                    }

                    //Save Client Request Header
                    tblClientRequestHeader clientRequestHeader = new tblClientRequestHeader();
                    clientRequestHeader.ClientID = clientID;
                    clientRequestHeader.PartnerID = clientRequestVM.ClientRequestHeaderDetails.PartnerID;
                    clientRequestHeader.RequestedDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.RequestedDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.RequestedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    clientRequestHeader.CreatedBy = clientRequestVM.UserID;
                    clientRequestHeader.CreatedDate = DateTime.Now;
                    clientRequestHeader.ModifiedBy = clientRequestVM.ClientRequestHeaderDetails.AgentID;
                    clientRequestHeader.InspectionDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.InspectionDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.InspectionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    clientRequestHeader.AdditionalNote = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.AdditionalNote) ? clientRequestVM.ClientRequestHeaderDetails.AdditionalNote : "";
                    clientRequestHeader.FileNo = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.FileNo) ? clientRequestVM.ClientRequestHeaderDetails.FileNo : "";
                    //clientRequestHeader.PolicyStart = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    //clientRequestHeader.PolicyEnd = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    //clientRequestHeader.PolicyStart = DateTime.Parse(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate);
                    clientRequestHeader.PolicyStart = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    clientRequestHeader.PolicyEnd = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    clientRequestHeader.TransactionID= clientRequestVM.ClientRequestHeaderDetails.transactionType = 1;
                    clientRequestHeader.JoinDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.JoinDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    unitOfWork.TblClientRequestHeaderRepository.Insert(clientRequestHeader);
                    unitOfWork.Save();

                    //tblDeduction deduction = new tblDeduction();
                    //deduction.ClientID = clientID;
                    //deduction.LodingRate = clientRequestVM.ClientDetails.LoadnigRate;
                    //deduction.DeductionRate = clientRequestVM.ClientDetails.DeductionRate;
                    //unitOfWork.TblDeductionRepository.Insert(deduction);
                    //unitOfWork.Save();

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
        public bool SaveBUPARequest(ClientRequestVM clientRequestVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    int clientID = 0;

                    if (clientRequestVM.IsClientAdded == false)
                    {
                        clientRequestVM.IsClientUpdated = true;
                    }

                    if (clientRequestVM.IsClientUpdated)
                    {
                        clientID = clientRequestVM.ClientDetails.ClientID;

                        tblClient client = unitOfWork.TblClientRepository.GetByID(clientID);
                        client.TitleID = clientRequestVM.ClientDetails.TitleID < 0 ?0: clientRequestVM.ClientDetails.TitleID;
                        client.ClientName = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientName) ? "": clientRequestVM.ClientDetails.ClientName;

                        client.ExtraText = clientRequestVM.ClientDetails.ClientOtherName; 



                        client.ClientAddress = clientRequestVM.ClientDetails.ClientAddress;
                        client.ExtraText = clientRequestVM.ClientDetails.MemberOtherName;
                        client.NIC = clientRequestVM.ClientDetails.NIC;
                        client.ContactNo = clientRequestVM.ClientDetails.ContactNo;
                        client.FixedLine = clientRequestVM.ClientDetails.FixedLine;
                        client.Email = clientRequestVM.ClientDetails.Email;
                        client.PremiumAccept = clientRequestVM.ClientDetails.PremiumAccept;
                        client.ExtraInt1 =string.IsNullOrEmpty( clientRequestVM.ClientRequestHeaderDetails.Year)?0:int.Parse(clientRequestVM.ClientRequestHeaderDetails.Year);



                        //if (clientRequestVM.ClientDetails.DOB !=null & clientRequestVM.ClientDetails.DOB.Length > 10)
                        //    clientRequestVM.ClientDetails.DOB = clientRequestVM.ClientDetails.DOB.Substring(clientRequestVM.ClientDetails.DOB.Length -10).ToString();
                        //else
                        client.DOB = !string.IsNullOrEmpty(clientRequestVM.ClientDetails.DOB) ? DateTime.ParseExact(clientRequestVM.ClientDetails.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        client.PPID = clientRequestVM.ClientDetails.PPID;
                        client.FamilyDiscount = clientRequestVM.ClientDetails.FamilyDiscount;
                        client.AdditionalNote = clientRequestVM.ClientDetails.AdditionalNote;
                        client.HomeCountryID = clientRequestVM.ClientDetails.HomeCountryID;
                        client.ResidentCountryID = clientRequestVM.ClientDetails.ResidentCountryID;
                        client.BUID = clientRequestVM.ClientDetails.BusinessUnitID;
                        
                        client.ModifiedBy = clientRequestVM.UserID;
                        client.ModifiedDate = DateTime.Now;
                        client.CustomerType = clientRequestVM.ClientDetails.CustomerType;

                        client.IsActive = clientRequestVM.ClientDetails.ClientStatus;
                        client.FrequncyID = clientRequestVM.ClientDetails.frequncyID;
                        client.FrequncyDID = clientRequestVM.ClientDetails.FrequncyDID;


                        unitOfWork.TblClientRepository.Update(client);
                        unitOfWork.Save();

                        clientID = client.ClientID;
                        if (clientRequestVM.ClientDetails.FamilyDetails != null )
                        {
                            foreach (var requestLine in clientRequestVM.ClientDetails.FamilyDetails)
                            {

                               
                                    tblFamilyMember tblFamilyDetail = new tblFamilyMember();
                                    //tblFamilyDetail.ClientID = clientID;
                                    //tblFamilyDetail.MemberName = requestLine.MemberName;
                                    //tblFamilyDetail.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                    //tblFamilyDetail.NICNo = requestLine.NIC;
                                    //tblFamilyDetail.ContactNo = requestLine.ContactNo;
                                    //tblFamilyDetail.JoinDate = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                                    // tblFamilyDetail.FamilyMemberID = requestLine.FamilyMemberID;
                                    //clientRequestLine.CreatedDate = DateTime.Now;

                                    tblFamilyDetail.ClientID = clientID;
                                    tblFamilyDetail.Title = requestLine.TitleID < 0 ? 0 : requestLine.TitleID;
                                    tblFamilyDetail.MemberName = string.IsNullOrEmpty(requestLine.MemberName) ? "" : requestLine.MemberName;
                                    tblFamilyDetail.MemberOtherName = string.IsNullOrEmpty(requestLine.MemberOtherName) ? "" : requestLine.MemberOtherName;

                                    //if (requestLine.MemberDOB.Length > 10)
                                    //    requestLine.MemberDOB = requestLine.MemberDOB.Substring(requestLine.MemberDOB.Length - 10).ToString();





                                    tblFamilyDetail.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                                    //if (requestLine.InceptionDate.Length > 10)
                                    //    requestLine.InceptionDate = requestLine.InceptionDate.Substring(requestLine.InceptionDate.Length - 10).ToString();


                                    tblFamilyDetail.JoinDate = !string.IsNullOrEmpty(requestLine.JoinDate) ? DateTime.ParseExact(requestLine.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                    tblFamilyDetail.NICNo = !string.IsNullOrEmpty(requestLine.NIC) ? requestLine.NIC : "";
                                    tblFamilyDetail.ContactNo = !string.IsNullOrEmpty(requestLine.ContactNo) ? requestLine.ContactNo : "";
                                    tblFamilyDetail.HomeCountry = requestLine.HomeCountryID < 0 ? 0 : requestLine.HomeCountryID;
                                    tblFamilyDetail.CountryOfResident = requestLine.
ResidentCountryID < 0 ? 0 : requestLine.
ResidentCountryID;
                                tblFamilyDetail.premiumID =! string.IsNullOrEmpty(requestLine.SchemeID.ToString()) ? requestLine.SchemeID.ToString() : "";
                                tblFamilyDetail.RelationShipID = requestLine.RelationShipID < 0 ? 0 : requestLine.RelationShipID;
                                    tblFamilyDetail.GenderID = requestLine.GenderID < 0 ? 0 : requestLine.GenderID;
                                    //  tblFamilyDetail.ExtraText2= !string.IsNullOrEmpty(requestLine.Exclusion) ? requestLine.Exclusion : "";
                                    tblFamilyDetail.MembershipID = !string.IsNullOrEmpty(requestLine.MembershipID) ? requestLine.MembershipID : "";
                                tblFamilyDetail.Exclusions = 0;
                                    tblFamilyDetail.OptionalCover = requestLine.OptionalCover;
                                    tblFamilyDetail.IsActive = requestLine.IsActive;
                               tblFamilyDetail.Exclu = requestLine.Exclu;
                                tblFamilyDetail.ExtraText1=  string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.Year) ? "0" : clientRequestVM.ClientRequestHeaderDetails.Year;
                                tblFamilyDetail.FrequncyID = clientRequestVM.ClientRequestHeaderDetails.FrequncyID.ToString();
                                tblFamilyDetail.FrequncyDID = clientRequestVM.ClientRequestHeaderDetails.FrequncyDID.ToString();

                                unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyDetail);
                                    unitOfWork.Save();
                                
                                if (clientRequestVM.ClientDetails.FamilyDetails != null)
                                {
                                        foreach (var grpMember in requestLine.GroupMemberDetails)
                                        {
                                            tblGrpFamilyDetail tblGrpFamily = new tblGrpFamilyDetail();
                                            tblGrpFamily.ClientID = clientRequestVM.ClientDetails.ClientID;
                                            tblGrpFamily.MemberName = grpMember.MemberName;
                                            //if (grpMember.MemberDOB.Length > 10)
                                            //    grpMember.MemberDOB = grpMember.MemberDOB.Substring(grpMember.MemberDOB.Length - 10).ToString();




                                            tblGrpFamily.MemberDOB = !string.IsNullOrEmpty(grpMember.MemberDOB) ? DateTime.ParseExact(grpMember.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                            tblGrpFamily.MemberNIC = grpMember.NIC;
                                            tblGrpFamily.MemberContact = grpMember.ContactNo;
                                            tblGrpFamily.FamilyMemberID = requestLine.FamilyMemberID;
                                            tblGrpFamily.MemberCountryID = requestLine.MemberCountryID;
                                        tblGrpFamily.Exclusions = 0;
                                           // tblFamilyDetail.OptionalCover = requestLine.OptionalCover;
                                        tblGrpFamily.OptionalCovers= requestLine.OptionalCover;
                                        tblGrpFamily.Exclu = grpMember.Exclu;
                                        tblGrpFamily.IsActive = grpMember.IsActive;
                                        tblGrpFamily.ExtraText2 = string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.Year) ? "0" : clientRequestVM.ClientRequestHeaderDetails.Year;

                                        tblGrpFamily.FrequncyID = clientRequestVM.ClientRequestHeaderDetails.FrequncyID.ToString();
                                        tblGrpFamily.FrequncyDID = clientRequestVM.ClientRequestHeaderDetails.FrequncyDID.ToString();
                                        //clientRequestLine.CreatedDate = DateTime.Now;
                                        unitOfWork.TblGrpFamilyDetailRepository.Insert(tblGrpFamily);
                                            unitOfWork.Save();
                                        }
                                    
                                }
                                }
                        }
                    }
                    else if (clientRequestVM.IsClientAdded)
                    {
                        //Save Client
                        tblClient client = new tblClient();
                        client.ClientName = clientRequestVM.ClientDetails.ClientName;
                        client.ExtraText = clientRequestVM.ClientDetails.ClientOtherName;
                        client.CustomerType = clientRequestVM.ClientDetails.CustomerType;
                        client.ClientAddress = clientRequestVM.ClientDetails.ClientAddress;
                        client.NIC = clientRequestVM.ClientDetails.NIC;
                        client.ContactNo = clientRequestVM.ClientDetails.ContactNo;
                        client.FixedLine = clientRequestVM.ClientDetails.FixedLine;
                        client.Email = clientRequestVM.ClientDetails.Email;
                        client.PremiumAccept = clientRequestVM.ClientDetails.PremiumAccept;
                        //if (clientRequestVM.ClientDetails.DOB != null)

                        //{
                        //    if (clientRequestVM.ClientDetails.DOB.Length > 10)
                        //        clientRequestVM.ClientDetails.DOB = clientRequestVM.ClientDetails.DOB.Substring(clientRequestVM.ClientDetails.DOB.Length - 10).ToString();
                        //    else
                                client.DOB = !string.IsNullOrEmpty(clientRequestVM.ClientDetails.DOB) ? DateTime.ParseExact(clientRequestVM.ClientDetails.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        //}


                            client.PPID = clientRequestVM.ClientDetails.PPID;
                        client.FamilyDiscount = clientRequestVM.ClientDetails.FamilyDiscount;
                        client.AdditionalNote = clientRequestVM.ClientDetails.AdditionalNote;
                        client.HomeCountryID = clientRequestVM.ClientDetails.HomeCountryID;
                        client.ResidentCountryID = clientRequestVM.ClientDetails.ResidentCountryID;
                        client.BUID = clientRequestVM.ClientDetails.BusinessUnitID;
                        client.JoinDate= !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.JoinDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        client.CreatedBy = clientRequestVM.UserID;
                        client.CreatedDate = DateTime.Now;
                        client.TitleID = clientRequestVM.ClientDetails.TitleID;

                        client.ExtraInt1 = string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.Year) ? 0 : int.Parse(clientRequestVM.ClientRequestHeaderDetails.Year);
                        client.IsActive = clientRequestVM.ClientDetails.ClientStatus;
                        client.FrequncyID = clientRequestVM. ClientRequestHeaderDetails.FrequncyID;
                        client.FrequncyDID = clientRequestVM.ClientRequestHeaderDetails.FrequncyDID;
                        client.SeqNo = clientRequestVM.ClientDetails.PremiumAccept == "2" ? "" : "1";
                        unitOfWork.TblClientRepository.Insert(client);
                        unitOfWork.Save();

                        clientID = client.ClientID;
                        if (clientRequestVM.ClientDetails.FamilyDetails != null)
                        {

                            var fseqNo = 1;
                            var fSubseqNo = 1;
                            var fMseqNo = 1;
                            foreach (var requestLine in clientRequestVM.ClientDetails.FamilyDetails)
                            {
                                if (requestLine.MemberName.Trim() != null || requestLine.MemberName.Trim() != "")
                                {
                                    tblFamilyMember tblFamilyDetail = new tblFamilyMember();
                                    tblFamilyDetail.ClientID = clientID;
                                    tblFamilyDetail.Title = requestLine.TitleID < 0 ? 0 : requestLine.TitleID;
                                    tblFamilyDetail.MemberName = string.IsNullOrEmpty(requestLine.MemberName) ? "" : requestLine.MemberName;
                                    tblFamilyDetail.MemberOtherName = string.IsNullOrEmpty(requestLine.MemberOtherName) ? "" : requestLine.MemberOtherName;
                                    //if (requestLine.MemberDOB.Length > 10)
                                    //    requestLine.MemberDOB = requestLine.MemberDOB.Substring(requestLine.MemberDOB.Length - 10).ToString();


                                    //else
                                        tblFamilyDetail.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;


                                    //if (requestLine.JoinDate.Length > 10)
                                    //    requestLine.JoinDate = requestLine.JoinDate.Substring(requestLine.JoinDate.Length - 10).ToString();

                                    //else
                                        tblFamilyDetail.JoinDate = !string.IsNullOrEmpty(requestLine.JoinDate) ? DateTime.ParseExact(requestLine.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;




                                    tblFamilyDetail.NICNo = !string.IsNullOrEmpty(requestLine.NIC) ? requestLine.NIC : "";
                                    tblFamilyDetail.ContactNo = !string.IsNullOrEmpty(requestLine.ContactNo) ? requestLine.ContactNo : "";
                                    //tblFamilyDetail.HomeCountry = requestLine.MemberCountryID < 0 ? 0 : requestLine.MemberCountryID;
                                    //tblFamilyDetail.CountryOfResident = requestLine.MemberResCountryID < 0 ? 0 : requestLine.MemberResCountryID;
                                    tblFamilyDetail.RelationShipID = requestLine.RelationShipID < 0 ? 0 : requestLine.RelationShipID;


                                    tblFamilyDetail.HomeCountry = requestLine.HomeCountryID < 0 ? 0 : requestLine.HomeCountryID;
                                    tblFamilyDetail.CountryOfResident = requestLine.
ResidentCountryID < 0 ? 0 : requestLine.
ResidentCountryID;
                                    tblFamilyDetail.premiumID = !string.IsNullOrEmpty(requestLine.SchemeID.ToString()) ? requestLine.SchemeID.ToString() : "";





                                    tblFamilyDetail.GenderID = requestLine.GenderID < 0 ? 0 : requestLine.GenderID;
                                    // tblFamilyDetail.ExtraText2 = !string.IsNullOrEmpty(requestLine.Exclusion) ? requestLine.Exclusion : "";
                                    tblFamilyDetail.MembershipID = !string.IsNullOrEmpty(requestLine.MembershipID) ? requestLine.MembershipID : "";

                                    if (requestLine == null)
                                        tblFamilyDetail.Exclusions = 0;
                                    else if (requestLine.Exclusion == null)
                                        tblFamilyDetail.Exclusions = 0;
                                    else
                                        tblFamilyDetail.Exclusions = float.Parse(requestLine.Exclusion.ToString());
                                    tblFamilyDetail.Exclu = requestLine.Exclu;
                                    // tblFamilyDetail.Exclusions = string.IsNullOrEmpty(requestLine.Exclusion.ToString()) || requestLine == null ? 0 : float.Parse(requestLine.Exclusion);
                                    tblFamilyDetail.OptionalCover = requestLine.OptionalCover;
                                    tblFamilyDetail.IsActive = requestLine.IsActive;
                                    tblFamilyDetail.ExtraText1 = string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.Year) ? "0" : clientRequestVM.ClientRequestHeaderDetails.Year;

                                    tblFamilyDetail.FrequncyID = clientRequestVM.ClientRequestHeaderDetails.FrequncyID.ToString();
                                    tblFamilyDetail.FrequncyDID = clientRequestVM.ClientRequestHeaderDetails.FrequncyDID.ToString();

                                    if (clientRequestVM.ClientDetails.PremiumAccept == "2")
                                    {
                                        tblFamilyDetail.SeqNo = fseqNo.ToString();
                                        tblFamilyDetail.SeqSubNo = "";
                                    }
                                    else
                                    {
                                        tblFamilyDetail.SeqNo = "1";
                                        tblFamilyDetail.SeqSubNo = fSubseqNo.ToString();
                                    }

                                    //clientRequestLine.CreatedDate = DateTime.Now;
                                    unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyDetail);
                                    unitOfWork.Save();


                                    if (requestLine.GroupMemberDetails != null)
                                    {
                                        fMseqNo = 1;
                                        foreach (var grpMember in requestLine.GroupMemberDetails)
                                        {
                                            tblGrpFamilyDetail tblGrpFamily = new tblGrpFamilyDetail();
                                            tblGrpFamily.ClientID = clientID;
                                            tblGrpFamily.TitleID = grpMember.TitleID < 0 ? 0 : grpMember.TitleID;
                                            tblGrpFamily.MemberName = grpMember.MemberName;
                                            tblGrpFamily.MembershipID = grpMember.MembershipID; 
                                            tblGrpFamily.ExtraText1 = string.IsNullOrEmpty(grpMember.MemberOtherName) ? "" : grpMember.MemberOtherName;
                                       //     tblGrpFamily.MemberOtherName = string.IsNullOrEmpty(grpMember.MemberOtherName) ? "" : grpMember.MemberOtherName;

                                            //if (grpMember.MemberDOB.Length > 10)
                                            //    grpMember.MemberDOB = grpMember.MemberDOB.Substring(grpMember.MemberDOB.Length - 10).ToString();

                                            tblGrpFamily.MemberDOB = !string.IsNullOrEmpty(grpMember.MemberDOB) ? DateTime.ParseExact(grpMember.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                                            //if (grpMember.JoinDate.Length > 10)
                                            //   grpMember.JoinDate = grpMember.JoinDate.Substring(grpMember.JoinDate.Length - 10).ToString();
                                            tblGrpFamily.RelationShipID = grpMember.RelationShipID < 0 ? 0 : grpMember.RelationShipID;

                                            tblGrpFamily.JoinDate = !string.IsNullOrEmpty(grpMember.JoinDate) ? DateTime.ParseExact(grpMember.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                            tblGrpFamily.MemberNIC = grpMember.NIC;
                                            tblGrpFamily.MemberContact = grpMember.ContactNo;
                                            tblGrpFamily.FamilyMemberID = tblFamilyDetail.FamilyMemberID;
                                            //tblGrpFamily.MemberCountryID = grpMember.countryID < 0 ? 0 : (int)grpMember.MemberCountryID;
                                            tblGrpFamily.MemberCountryID = grpMember.MemberCountryID < 0 ? 0 : grpMember.MemberCountryID;

                                            tblGrpFamily.MemberResCountryID= grpMember.ResidentCountryID < 0 ? 0 : grpMember.ResidentCountryID;
                                            tblGrpFamily.Exclusions = 0;
                                            tblGrpFamily.Exclu = grpMember.Exclu;
                                            tblGrpFamily.OptionalCovers = requestLine.OptionalCover;
                                            tblGrpFamily.IsActive= grpMember.IsActive < 0 ? 0 : grpMember.IsActive;

                                            tblGrpFamily.ExtraText2 = string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.Year) ? "0" : clientRequestVM.ClientRequestHeaderDetails.Year;
                                            //clientRequestLine.CreatedDate = DateTime.Now;


                                            tblGrpFamily.FrequncyID = clientRequestVM.ClientRequestHeaderDetails.FrequncyID.ToString();
                                            tblGrpFamily.FrequncyDID = clientRequestVM.ClientRequestHeaderDetails.FrequncyDID.ToString();
                                            tblGrpFamily.SeqNo = fseqNo.ToString();
                                            tblGrpFamily.SeqSubNo = fMseqNo.ToString();

                                            unitOfWork.TblGrpFamilyDetailRepository.Insert(tblGrpFamily);
                                            unitOfWork.Save();
                                            fMseqNo++;
                                        }
                                    }

                                    fseqNo++;
                                    fSubseqNo++;
                                }
                                
                            }
                        }


                    }
                    else
                    {
                        clientID = clientRequestVM.ClientDetails.ClientID;
                    }

                    //Save Client Request Header
                    tblClientRequestHeader clientRequestHeader = new tblClientRequestHeader();
                    clientRequestHeader.ClientID = clientID;
                    clientRequestHeader.PartnerID = 2;
                    clientRequestHeader.Exclusions = 0;
                    clientRequestHeader.OptionalCovers = clientRequestVM.ClientRequestHeaderDetails.OptionalCovers;
                    clientRequestHeader.Occupation= clientRequestVM.ClientRequestHeaderDetails.Occupation;
                    clientRequestHeader.CurrancyID = clientRequestVM.ClientRequestHeaderDetails.CurrancyID;
                    clientRequestHeader.FrequncyDID= clientRequestVM.ClientRequestHeaderDetails.FrequncyDID;
                    clientRequestHeader.FrequncyID= clientRequestVM.ClientRequestHeaderDetails.FrequncyID;
                    //   clientRequestVM.ClientRequestHeaderDetails.RequestedDate = clientRequestVM.ClientDetails.RequestedDate;
                    clientRequestHeader.SchemeID= clientRequestVM.ClientRequestHeaderDetails.SchemeID;
                    clientRequestHeader.MembershipID = clientRequestVM.ClientRequestHeaderDetails.MembershipID;
                    clientRequestHeader.Exclu = clientRequestVM.ClientRequestHeaderDetails.Exclu;
                    clientRequestHeader.GroupID = clientRequestVM.ClientRequestHeaderDetails.GroupID;

                    //if (clientRequestVM.ClientRequestHeaderDetails.RequestedDate.Length > 10)
                    //    clientRequestVM.ClientRequestHeaderDetails.RequestedDate = clientRequestVM.ClientRequestHeaderDetails.RequestedDate.Substring(clientRequestVM.ClientRequestHeaderDetails.RequestedDate.Length - 10).ToString();


                    clientRequestHeader.RequestedDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.RequestedDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.RequestedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    clientRequestHeader.CreatedBy = clientRequestVM.UserID;
                    clientRequestHeader.CreatedDate = DateTime.Now;
                    var PolicyStart = "";
                    var PolicyEnd = "";
                    //if (clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate != null & clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate.Length > 10)
                    //{
                    //    PolicyStart = clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate.Substring(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate.Length - 10).ToString();
                    //    clientRequestHeader.PolicyStart = !string.IsNullOrEmpty(PolicyStart) ? DateTime.ParseExact(PolicyStart, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                    //}
                    //else



                        clientRequestHeader.PolicyStart = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;


                    //if (clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate != null & clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate.Length > 10)
                    //{
                    //    PolicyEnd = clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate.Substring(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate.Length - 10).ToString();
                    //    clientRequestHeader.PolicyEnd = !string.IsNullOrEmpty(PolicyEnd) ? DateTime.ParseExact(PolicyEnd, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                    //}
                    //else



                        clientRequestHeader.PolicyEnd = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;









                    // clientRequestHeader.PolicyEnd = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                    clientRequestHeader.AgentID = string.IsNullOrEmpty( clientRequestVM.ClientRequestHeaderDetails.AgentID.ToString())?0: clientRequestVM.ClientRequestHeaderDetails.AgentID;
                    clientRequestHeader.ModifiedBy = clientRequestVM.ClientRequestHeaderDetails.AgentID;
                    clientRequestHeader.IsActive = true;
                    unitOfWork.TblClientRequestHeaderRepository.Insert(clientRequestHeader);
                    unitOfWork.Save();

                    //tblDeduction deduction = new tblDeduction();
                    //deduction.ClientID = clientID;
                    //deduction.LodingRate = clientRequestVM.ClientDetails.LoadnigRate;
                    //deduction.DeductionRate = clientRequestVM.ClientDetails.DeductionRate;
                    //unitOfWork.TblDeductionRepository.Insert(deduction);
                    //unitOfWork.Save();

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

        public bool SaveBUPARenewelRequest(ClientRequestVM clientRequestVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    int clientID = 0;

                    if (clientRequestVM.IsClientAdded == false)
                    {
                        clientRequestVM.IsClientUpdated = true;
                    }

                    if (clientRequestVM.IsClientUpdated)
                    {
                        clientID = clientRequestVM.ClientDetails.ClientID;

                        tblClient client = unitOfWork.TblClientRepository.GetByID(clientID);
                        client.TitleID = clientRequestVM.ClientDetails.TitleID < 0 ? 0 : clientRequestVM.ClientDetails.TitleID;
                        client.ClientName = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientName) ? "" : clientRequestVM.ClientDetails.ClientName;

                        client.ExtraText = clientRequestVM.ClientDetails.ClientOtherName;



                        client.ClientAddress = clientRequestVM.ClientDetails.ClientAddress;
                        client.ExtraText = clientRequestVM.ClientDetails.MemberOtherName;
                        client.NIC = clientRequestVM.ClientDetails.NIC;
                        client.ContactNo = clientRequestVM.ClientDetails.ContactNo;
                        client.FixedLine = clientRequestVM.ClientDetails.FixedLine;
                        client.Email = clientRequestVM.ClientDetails.Email;
                        client.PremiumAccept = clientRequestVM.ClientDetails.PremiumAccept;




                        //if (clientRequestVM.ClientDetails.DOB !=null & clientRequestVM.ClientDetails.DOB.Length > 10)
                        //    clientRequestVM.ClientDetails.DOB = clientRequestVM.ClientDetails.DOB.Substring(clientRequestVM.ClientDetails.DOB.Length -10).ToString();
                        //else
                        client.DOB = !string.IsNullOrEmpty(clientRequestVM.ClientDetails.DOB) ? DateTime.ParseExact(clientRequestVM.ClientDetails.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        client.PPID = clientRequestVM.ClientDetails.PPID;
                        client.FamilyDiscount = clientRequestVM.ClientDetails.FamilyDiscount;
                        client.AdditionalNote = clientRequestVM.ClientDetails.AdditionalNote;
                        client.HomeCountryID = clientRequestVM.ClientDetails.HomeCountryID;
                        client.ResidentCountryID = clientRequestVM.ClientDetails.ResidentCountryID;
                        client.BUID = clientRequestVM.ClientDetails.BusinessUnitID;

                        client.ModifiedBy = clientRequestVM.UserID;
                        client.ModifiedDate = DateTime.Now;
                        client.CustomerType = clientRequestVM.ClientDetails.CustomerType;

                        client.IsActive = clientRequestVM.ClientDetails.ClientStatus;
                        unitOfWork.TblClientRepository.Update(client);
                        unitOfWork.Save();

                        clientID = client.ClientID;
                        if (clientRequestVM.ClientDetails.FamilyDetails != null)
                        {
                            foreach (var requestLine in clientRequestVM.ClientDetails.FamilyDetails)
                            {


                                tblFamilyMember tblFamilyDetail = new tblFamilyMember();
                                //tblFamilyDetail.ClientID = clientID;
                                //tblFamilyDetail.MemberName = requestLine.MemberName;
                                //tblFamilyDetail.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                //tblFamilyDetail.NICNo = requestLine.NIC;
                                //tblFamilyDetail.ContactNo = requestLine.ContactNo;
                                //tblFamilyDetail.JoinDate = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                                // tblFamilyDetail.FamilyMemberID = requestLine.FamilyMemberID;
                                //clientRequestLine.CreatedDate = DateTime.Now;

                                tblFamilyDetail.ClientID = clientID;
                                tblFamilyDetail.Title = requestLine.TitleID < 0 ? 0 : requestLine.TitleID;
                                tblFamilyDetail.MemberName = string.IsNullOrEmpty(requestLine.MemberName) ? "" : requestLine.MemberName;
                                tblFamilyDetail.MemberOtherName = string.IsNullOrEmpty(requestLine.MemberOtherName) ? "" : requestLine.MemberOtherName;

                                //if (requestLine.MemberDOB.Length > 10)
                                //    requestLine.MemberDOB = requestLine.MemberDOB.Substring(requestLine.MemberDOB.Length - 10).ToString();





                                tblFamilyDetail.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                                //if (requestLine.InceptionDate.Length > 10)
                                //    requestLine.InceptionDate = requestLine.InceptionDate.Substring(requestLine.InceptionDate.Length - 10).ToString();


                                tblFamilyDetail.JoinDate = !string.IsNullOrEmpty(requestLine.JoinDate) ? DateTime.ParseExact(requestLine.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyDetail.NICNo = !string.IsNullOrEmpty(requestLine.NIC) ? requestLine.NIC : "";
                                tblFamilyDetail.ContactNo = !string.IsNullOrEmpty(requestLine.ContactNo) ? requestLine.ContactNo : "";
                                tblFamilyDetail.HomeCountry = requestLine.HomeCountryID < 0 ? 0 : requestLine.HomeCountryID;
                                tblFamilyDetail.CountryOfResident = requestLine.
ResidentCountryID < 0 ? 0 : requestLine.
ResidentCountryID;
                                tblFamilyDetail.premiumID = !string.IsNullOrEmpty(requestLine.SchemeID.ToString()) ? requestLine.SchemeID.ToString() : "";
                                tblFamilyDetail.RelationShipID = requestLine.RelationShipID < 0 ? 0 : requestLine.RelationShipID;
                                tblFamilyDetail.GenderID = requestLine.GenderID < 0 ? 0 : requestLine.GenderID;
                                //  tblFamilyDetail.ExtraText2= !string.IsNullOrEmpty(requestLine.Exclusion) ? requestLine.Exclusion : "";
                                tblFamilyDetail.MembershipID = !string.IsNullOrEmpty(requestLine.MembershipID) ? requestLine.MembershipID : "";
                                tblFamilyDetail.Exclusions = 0;
                                tblFamilyDetail.OptionalCover = requestLine.OptionalCover;
                                tblFamilyDetail.IsActive = requestLine.IsActive;
                                tblFamilyDetail.Exclu = requestLine.Exclu;
                                tblFamilyDetail.FrequncyID = clientRequestVM.ClientRequestHeaderDetails.FrequncyID.ToString();
                                tblFamilyDetail.FrequncyDID = clientRequestVM.ClientRequestHeaderDetails.FrequncyDID.ToString();


                                unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyDetail);
                                unitOfWork.Save();

                                if (clientRequestVM.ClientDetails.FamilyDetails != null)
                                {
                                    foreach (var grpMember in requestLine.GroupMemberDetails)
                                    {
                                        tblGrpFamilyDetail tblGrpFamily = new tblGrpFamilyDetail();
                                        tblGrpFamily.ClientID = clientRequestVM.ClientDetails.ClientID;
                                        tblGrpFamily.MemberName = grpMember.MemberName;
                                        //if (grpMember.MemberDOB.Length > 10)
                                        //    grpMember.MemberDOB = grpMember.MemberDOB.Substring(grpMember.MemberDOB.Length - 10).ToString();




                                        tblGrpFamily.MemberDOB = !string.IsNullOrEmpty(grpMember.MemberDOB) ? DateTime.ParseExact(grpMember.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                        tblGrpFamily.MemberNIC = grpMember.NIC;
                                        tblGrpFamily.MemberContact = grpMember.ContactNo;
                                        tblGrpFamily.FamilyMemberID = requestLine.FamilyMemberID;
                                        tblGrpFamily.MemberCountryID = requestLine.MemberCountryID;
                                        tblGrpFamily.Exclusions = 0;
                                        tblFamilyDetail.OptionalCover = requestLine.OptionalCover;

                                        tblGrpFamily.Exclu = grpMember.Exclu;
                                        tblGrpFamily.IsActive = grpMember.IsActive;

                                        tblGrpFamily.FrequncyID = clientRequestVM.ClientRequestHeaderDetails.FrequncyID.ToString();
                                        tblGrpFamily.FrequncyDID = clientRequestVM.ClientRequestHeaderDetails.FrequncyDID.ToString();
                                        //clientRequestLine.CreatedDate = DateTime.Now;
                                        unitOfWork.TblGrpFamilyDetailRepository.Insert(tblGrpFamily);
                                        unitOfWork.Save();
                                    }

                                }
                            }
                        }
                    }
                    else if (clientRequestVM.IsClientAdded)
                    {
                        //Save Client
                        tblClient client = new tblClient();
                        client.ClientName = clientRequestVM.ClientDetails.ClientName;
                        client.ExtraText = clientRequestVM.ClientDetails.ClientOtherName;
                        client.CustomerType = clientRequestVM.ClientDetails.CustomerType;
                        client.ClientAddress = clientRequestVM.ClientDetails.ClientAddress;
                        client.NIC = clientRequestVM.ClientDetails.NIC;
                        client.ContactNo = clientRequestVM.ClientDetails.ContactNo;
                        client.FixedLine = clientRequestVM.ClientDetails.FixedLine;
                        client.Email = clientRequestVM.ClientDetails.Email;
                        client.PremiumAccept = clientRequestVM.ClientDetails.PremiumAccept;
                        //if (clientRequestVM.ClientDetails.DOB != null)

                        //{
                        //    if (clientRequestVM.ClientDetails.DOB.Length > 10)
                        //        clientRequestVM.ClientDetails.DOB = clientRequestVM.ClientDetails.DOB.Substring(clientRequestVM.ClientDetails.DOB.Length - 10).ToString();
                        //    else
                        client.DOB = !string.IsNullOrEmpty(clientRequestVM.ClientDetails.DOB) ? DateTime.ParseExact(clientRequestVM.ClientDetails.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        //}


                        client.PPID = clientRequestVM.ClientDetails.PPID;
                        client.FamilyDiscount = clientRequestVM.ClientDetails.FamilyDiscount;
                        client.AdditionalNote = clientRequestVM.ClientDetails.AdditionalNote;
                        client.HomeCountryID = clientRequestVM.ClientDetails.HomeCountryID;
                        client.ResidentCountryID = clientRequestVM.ClientDetails.ResidentCountryID;
                        client.BUID = clientRequestVM.ClientDetails.BusinessUnitID;
                        client.JoinDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.JoinDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        client.CreatedBy = clientRequestVM.UserID;
                        client.CreatedDate = DateTime.Now;
                        client.TitleID = clientRequestVM.ClientDetails.TitleID;
                        client.IsActive = clientRequestVM.ClientDetails.ClientStatus;
                        unitOfWork.TblClientRepository.Insert(client);
                        unitOfWork.Save();

                        clientID = client.ClientID;
                        if (clientRequestVM.ClientDetails.FamilyDetails != null)
                        {
                            foreach (var requestLine in clientRequestVM.ClientDetails.FamilyDetails)
                            {
                                if (requestLine.MemberName.Trim() != null || requestLine.MemberName.Trim() != "")
                                {
                                    tblFamilyMember tblFamilyDetail = new tblFamilyMember();
                                    tblFamilyDetail.ClientID = clientID;
                                    tblFamilyDetail.Title = requestLine.TitleID < 0 ? 0 : requestLine.TitleID;
                                    tblFamilyDetail.MemberName = string.IsNullOrEmpty(requestLine.MemberName) ? "" : requestLine.MemberName;
                                    tblFamilyDetail.MemberOtherName = string.IsNullOrEmpty(requestLine.MemberOtherName) ? "" : requestLine.MemberOtherName;
                                    //if (requestLine.MemberDOB.Length > 10)
                                    //    requestLine.MemberDOB = requestLine.MemberDOB.Substring(requestLine.MemberDOB.Length - 10).ToString();


                                    //else
                                    tblFamilyDetail.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;


                                    //if (requestLine.JoinDate.Length > 10)
                                    //    requestLine.JoinDate = requestLine.JoinDate.Substring(requestLine.JoinDate.Length - 10).ToString();

                                    //else
                                    tblFamilyDetail.JoinDate = !string.IsNullOrEmpty(requestLine.JoinDate) ? DateTime.ParseExact(requestLine.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;




                                    tblFamilyDetail.NICNo = !string.IsNullOrEmpty(requestLine.NIC) ? requestLine.NIC : "";
                                    tblFamilyDetail.ContactNo = !string.IsNullOrEmpty(requestLine.ContactNo) ? requestLine.ContactNo : "";
                                    //tblFamilyDetail.HomeCountry = requestLine.MemberCountryID < 0 ? 0 : requestLine.MemberCountryID;
                                    //tblFamilyDetail.CountryOfResident = requestLine.MemberResCountryID < 0 ? 0 : requestLine.MemberResCountryID;
                                    tblFamilyDetail.RelationShipID = requestLine.RelationShipID < 0 ? 0 : requestLine.RelationShipID;


                                    tblFamilyDetail.HomeCountry = requestLine.HomeCountryID < 0 ? 0 : requestLine.HomeCountryID;
                                    tblFamilyDetail.CountryOfResident = requestLine.
ResidentCountryID < 0 ? 0 : requestLine.
ResidentCountryID;
                                    tblFamilyDetail.premiumID = !string.IsNullOrEmpty(requestLine.SchemeID.ToString()) ? requestLine.SchemeID.ToString() : "";





                                    tblFamilyDetail.GenderID = requestLine.GenderID < 0 ? 0 : requestLine.GenderID;
                                    // tblFamilyDetail.ExtraText2 = !string.IsNullOrEmpty(requestLine.Exclusion) ? requestLine.Exclusion : "";
                                    tblFamilyDetail.MembershipID = !string.IsNullOrEmpty(requestLine.MembershipID) ? requestLine.MembershipID : "";

                                    if (requestLine == null)
                                        tblFamilyDetail.Exclusions = 0;
                                    else if (requestLine.Exclusion == null)
                                        tblFamilyDetail.Exclusions = 0;
                                    else
                                        tblFamilyDetail.Exclusions = float.Parse(requestLine.Exclusion.ToString());
                                    tblFamilyDetail.Exclu = requestLine.Exclu;
                                    // tblFamilyDetail.Exclusions = string.IsNullOrEmpty(requestLine.Exclusion.ToString()) || requestLine == null ? 0 : float.Parse(requestLine.Exclusion);
                                    tblFamilyDetail.OptionalCover = requestLine.OptionalCover;
                                    tblFamilyDetail.IsActive = requestLine.IsActive;


                                    tblFamilyDetail.FrequncyID = clientRequestVM.ClientRequestHeaderDetails.FrequncyID.ToString();
                                    tblFamilyDetail.FrequncyDID = clientRequestVM.ClientRequestHeaderDetails.FrequncyDID.ToString();







                                    //clientRequestLine.CreatedDate = DateTime.Now;
                                    unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyDetail);
                                    unitOfWork.Save();


                                    if (requestLine.GroupMemberDetails != null)
                                    {

                                        foreach (var grpMember in requestLine.GroupMemberDetails)
                                        {
                                            tblGrpFamilyDetail tblGrpFamily = new tblGrpFamilyDetail();
                                            tblGrpFamily.ClientID = clientID;
                                            tblGrpFamily.TitleID = grpMember.TitleID < 0 ? 0 : grpMember.TitleID;
                                            tblGrpFamily.MemberName = grpMember.MemberName;
                                            tblGrpFamily.MembershipID = grpMember.MembershipID;
                                            tblGrpFamily.ExtraText1 = string.IsNullOrEmpty(grpMember.MemberOtherName) ? "" : grpMember.MemberOtherName;
                                            //     tblGrpFamily.MemberOtherName = string.IsNullOrEmpty(grpMember.MemberOtherName) ? "" : grpMember.MemberOtherName;

                                            //if (grpMember.MemberDOB.Length > 10)
                                            //    grpMember.MemberDOB = grpMember.MemberDOB.Substring(grpMember.MemberDOB.Length - 10).ToString();

                                            tblGrpFamily.MemberDOB = !string.IsNullOrEmpty(grpMember.MemberDOB) ? DateTime.ParseExact(grpMember.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                                            //if (grpMember.JoinDate.Length > 10)
                                            //   grpMember.JoinDate = grpMember.JoinDate.Substring(grpMember.JoinDate.Length - 10).ToString();
                                            tblGrpFamily.RelationShipID = grpMember.RelationShipID < 0 ? 0 : grpMember.RelationShipID;

                                            tblGrpFamily.JoinDate = !string.IsNullOrEmpty(grpMember.JoinDate) ? DateTime.ParseExact(grpMember.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                            tblGrpFamily.MemberNIC = grpMember.NIC;
                                            tblGrpFamily.MemberContact = grpMember.ContactNo;
                                            tblGrpFamily.FamilyMemberID = tblFamilyDetail.FamilyMemberID;
                                            //tblGrpFamily.MemberCountryID = grpMember.countryID < 0 ? 0 : (int)grpMember.MemberCountryID;
                                            tblGrpFamily.MemberCountryID = grpMember.MemberCountryID < 0 ? 0 : grpMember.MemberCountryID;

                                            tblGrpFamily.MemberResCountryID = grpMember.ResidentCountryID < 0 ? 0 : grpMember.ResidentCountryID;
                                            tblGrpFamily.Exclusions = 0;
                                            tblGrpFamily.Exclu = grpMember.Exclu;
                                            tblGrpFamily.OptionalCovers = requestLine.OptionalCover;
                                            tblGrpFamily.IsActive = grpMember.IsActive < 0 ? 0 : grpMember.IsActive;
                                            tblGrpFamily.FrequncyID = clientRequestVM.ClientRequestHeaderDetails.FrequncyID.ToString();
                                            tblGrpFamily.FrequncyDID = clientRequestVM.ClientRequestHeaderDetails.FrequncyDID.ToString();



                                            //clientRequestLine.CreatedDate = DateTime.Now;
                                            unitOfWork.TblGrpFamilyDetailRepository.Insert(tblGrpFamily);
                                            unitOfWork.Save();
                                        }
                                    }
                                }

                            }
                        }


                    }
                    else
                    {
                        clientID = clientRequestVM.ClientDetails.ClientID;
                    }

                    //Save Client Request Header
                    tblClientRequestHeader clientRequestHeader = new tblClientRequestHeader();
                    clientRequestHeader.ClientID = clientID;
                    clientRequestHeader.PartnerID = 2;
                    clientRequestHeader.Exclusions = 0;
                    clientRequestHeader.OptionalCovers = clientRequestVM.ClientRequestHeaderDetails.OptionalCovers;
                    clientRequestHeader.Occupation = clientRequestVM.ClientRequestHeaderDetails.Occupation;
                    clientRequestHeader.CurrancyID = clientRequestVM.ClientRequestHeaderDetails.CurrancyID;
                    clientRequestHeader.FrequncyDID = clientRequestVM.ClientRequestHeaderDetails.FrequncyDID;
                    clientRequestHeader.FrequncyID = clientRequestVM.ClientRequestHeaderDetails.FrequncyID;
                    //   clientRequestVM.ClientRequestHeaderDetails.RequestedDate = clientRequestVM.ClientDetails.RequestedDate;
                    clientRequestHeader.SchemeID = clientRequestVM.ClientRequestHeaderDetails.SchemeID;
                    clientRequestHeader.MembershipID = clientRequestVM.ClientRequestHeaderDetails.MembershipID;
                    clientRequestHeader.Exclu = clientRequestVM.ClientRequestHeaderDetails.Exclu;
                    clientRequestHeader.GroupID = clientRequestVM.ClientRequestHeaderDetails.GroupID;

                    //if (clientRequestVM.ClientRequestHeaderDetails.RequestedDate.Length > 10)
                    //    clientRequestVM.ClientRequestHeaderDetails.RequestedDate = clientRequestVM.ClientRequestHeaderDetails.RequestedDate.Substring(clientRequestVM.ClientRequestHeaderDetails.RequestedDate.Length - 10).ToString();


                    clientRequestHeader.RequestedDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.RequestedDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.RequestedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    clientRequestHeader.CreatedBy = clientRequestVM.UserID;
                    clientRequestHeader.CreatedDate = DateTime.Now;
                    var PolicyStart = "";
                    var PolicyEnd = "";
                    //if (clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate != null & clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate.Length > 10)
                    //{
                    //    PolicyStart = clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate.Substring(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate.Length - 10).ToString();
                    //    clientRequestHeader.PolicyStart = !string.IsNullOrEmpty(PolicyStart) ? DateTime.ParseExact(PolicyStart, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                    //}
                    //else



                    clientRequestHeader.PolicyStart = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;


                    //if (clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate != null & clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate.Length > 10)
                    //{
                    //    PolicyEnd = clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate.Substring(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate.Length - 10).ToString();
                    //    clientRequestHeader.PolicyEnd = !string.IsNullOrEmpty(PolicyEnd) ? DateTime.ParseExact(PolicyEnd, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                    //}
                    //else



                    clientRequestHeader.PolicyEnd = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;









                    // clientRequestHeader.PolicyEnd = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                    clientRequestHeader.AgentID = string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.AgentID.ToString()) ? 0 : clientRequestVM.ClientRequestHeaderDetails.AgentID;
                    clientRequestHeader.ModifiedBy = clientRequestVM.ClientRequestHeaderDetails.AgentID;
                    unitOfWork.TblClientRequestHeaderRepository.Insert(clientRequestHeader);
                    unitOfWork.Save();



                    var plicyHistorytable = new tblPolicyRenewalHistory();

                    plicyHistorytable.PolicyInfoRecID = clientID;
                    plicyHistorytable.RenewalDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.RequestedDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.RequestedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    plicyHistorytable.NotificationDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.RequestedDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.RequestedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    plicyHistorytable.IsSent = false;
                    plicyHistorytable.IsCancel = false;
                    plicyHistorytable.Agent= string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.AgentID.ToString()) ? 0 : clientRequestVM.ClientRequestHeaderDetails.AgentID;
                    plicyHistorytable.IsRenewal = true;
                    plicyHistorytable.RenewalStartDate= !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                    plicyHistorytable.RenewalEndDate= !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    //tblDeduction deduction = new tblDeduction();
                    //deduction.ClientID = clientID;
                    //deduction.LodingRate = clientRequestVM.ClientDetails.LoadnigRate;
                    //deduction.DeductionRate = clientRequestVM.ClientDetails.DeductionRate;
                    //unitOfWork.TblDeductionRepository.Insert(deduction);
                    //unitOfWork.Save();

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




        public bool UpdateRequest(ClientRequestHeaderVM clientRequestHeaderVM, bool isClientUpdated, bool isClientAdded, ClientVM clientObj, out string errorMessage)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    int clientID = 0;

                    if (clientRequestHeaderVM.IsQuotationCreated != true)
                    {
                        if (isClientUpdated)
                        {
                            clientID = clientObj.ClientID;

                            //Update Client
                            tblClient client = unitOfWork.TblClientRepository.GetByID(clientObj.ClientID);
                            client.ClientName = clientObj.ClientName;
                            client.ClientAddress = clientObj.ClientAddress;
                            client.NIC = clientObj.NIC;
                            client.ContactNo = clientObj.ContactNo;
                            client.FixedLine = clientObj.FixedLine;
                            client.Email = clientObj.Email;
                            client.DOB = !string.IsNullOrEmpty(clientObj.DOB) ? DateTime.ParseExact(clientObj.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            client.PPID = clientObj.PPID;
                            client.FamilyDiscount = clientObj.FamilyDiscount;
                            client.AdditionalNote = clientObj.AdditionalNote;
                            client.HomeCountryID = clientObj.HomeCountryID;
                            client.ResidentCountryID = clientObj.ResidentCountryID;
                            client.BUID = clientObj.BusinessUnitID;
                            client.JoinDate = !string.IsNullOrEmpty(clientRequestHeaderVM.JoinDate) ? DateTime.ParseExact(clientRequestHeaderVM.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            client.ModifiedBy = clientRequestHeaderVM.ModifiedBy;
                            client.ModifiedDate = DateTime.Now;
                            unitOfWork.TblClientRepository.Update(client);
                            unitOfWork.Save();


                            //var familyData = unitOfWork.TblFamilyMemberRepository.Get(x => x.ClientID == clientObj.ClientID).ToList();

                            //foreach (var family in familyData)
                            //{
                            //    unitOfWork.TblFamilyMemberRepository.Delete(family);
                            //    unitOfWork.Save();
                            //}

                            foreach (var requestLine in clientRequestHeaderVM.FamilyDetails)
                            {
                                
                                tblFamilyMember tblFamilyMember = new tblFamilyMember();
                                tblFamilyMember.ClientID = clientRequestHeaderVM.ClientID;
                                //tblFamilyMember.MemberName = requestLine.MemberName;
                                //tblFamilyMember.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null; ;
                                //tblFamilyMember.NICNo = requestLine.NIC;
                                //tblFamilyMember.ContactNo = requestLine.ContactNo;
                                tblFamilyMember.Title = requestLine.TitleID < 0 ? 0 : requestLine.TitleID;
                                tblFamilyMember.MemberName = string.IsNullOrEmpty(requestLine.MemberName) ? "" : requestLine.MemberName;
                                tblFamilyMember.MemberOtherName = string.IsNullOrEmpty(requestLine.MemberOtherName) ? "" : requestLine.MemberOtherName;
                                tblFamilyMember.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyMember.JoinDate = !string.IsNullOrEmpty(requestLine.JoinDate) ? DateTime.ParseExact(requestLine.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyMember.NICNo = !string.IsNullOrEmpty(requestLine.NIC) ? requestLine.NIC : "";
                                tblFamilyMember.ContactNo = !string.IsNullOrEmpty(requestLine.ContactNo) ? requestLine.ContactNo : "";
                                tblFamilyMember.HomeCountry = requestLine.MemberCountryID < 0 ? 0 : requestLine.MemberCountryID;
                                tblFamilyMember.CountryOfResident = requestLine.MemberResCountryID < 0 ? 0 : requestLine.MemberResCountryID;
                                tblFamilyMember.RelationShipID = requestLine.RelationShipID < 0 ? 0 : requestLine.RelationShipID;
                                tblFamilyMember.GenderID = requestLine.GenderID < 0 ? 0 : requestLine.GenderID;


                                if (requestLine.FamilyMemberID < 0)
                                {

                                    unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyMember);
                                    unitOfWork.Save();
                                }
                                else
                                {
                                    unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyMember);
                                    unitOfWork.Save();
                                }

                                //if (requestLine.FamilyMemberID > 0)
                                //{
                                //    tblFamilyMember family = unitOfWork.TblFamilyMemberRepository.GetByID(requestLine.FamilyMemberID);
                                //    family.ClientID = clientRequestHeaderVM.ClientID;
                                //    family.MemberName = requestLine.MemberName;
                                //    family.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null; ;
                                //    //clientRequestLine.CreatedDate = DateTime.Now;
                                //    unitOfWork.TblFamilyMemberRepository.Update(family);
                                //    unitOfWork.Save();
                                //}
                                //else
                                //{

                                // }

                            }
                        }
                        else if (isClientAdded)
                        {
                            //Save Client
                            tblClient client = new tblClient();
                            client.ClientName = clientObj.ClientName;
                            client.ClientAddress = clientObj.ClientAddress;
                            client.NIC = clientObj.NIC;
                            client.ContactNo = clientObj.ContactNo;
                            client.FixedLine = clientObj.FixedLine;
                            client.Email = clientObj.Email;
                            client.DOB = !string.IsNullOrEmpty(clientObj.DOB) ? DateTime.ParseExact(clientObj.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            client.PPID = clientObj.PPID;
                            client.FamilyDiscount = clientObj.FamilyDiscount;
                            client.AdditionalNote = clientObj.AdditionalNote;
                            client.HomeCountryID = clientObj.HomeCountryID;
                            client.ResidentCountryID = clientObj.ResidentCountryID;
                            client.BUID = clientObj.BusinessUnitID;
                            client.JoinDate = !string.IsNullOrEmpty(clientRequestHeaderVM.JoinDate) ? DateTime.ParseExact(clientRequestHeaderVM.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            client.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                            client.CreatedDate = DateTime.Now;
                            unitOfWork.TblClientRepository.Insert(client);
                            unitOfWork.Save();
                            foreach (var requestLine in clientRequestHeaderVM.FamilyDetails)
                            {
                                tblFamilyMember tblFamilyMember = new tblFamilyMember();
                                tblFamilyMember.ClientID = clientRequestHeaderVM.ClientID;
                                tblFamilyMember.MemberName = requestLine.MemberName;
                                tblFamilyMember.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null; ;
                                tblFamilyMember.NICNo = requestLine.NIC;
                                tblFamilyMember.ContactNo = requestLine.ContactNo;
                                //clientRequestLine.CreatedDate = DateTime.Now;
                                unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyMember);
                                unitOfWork.Save();
                            }
                            
                            clientID = client.ClientID;
                        }
                        else
                        {
                            clientID = clientObj.ClientID;
                        }

                        //Update Client Request Header
                        tblClientRequestHeader clientRequestHeader = unitOfWork.TblClientRequestHeaderRepository.GetByID(clientRequestHeaderVM.ClientRequestHeaderID);
                        clientRequestHeader.ClientID = clientRequestHeaderVM.ClientID;
                        clientRequestHeader.PartnerID = clientRequestHeaderVM.PartnerID;
                        clientRequestHeader.RequestedDate = !string.IsNullOrEmpty(clientRequestHeaderVM.RequestedDate) ? DateTime.ParseExact(clientRequestHeaderVM.RequestedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        clientRequestHeader.ModifiedBy = clientRequestHeaderVM.AgentID;
                        clientRequestHeader.ModifiedDate = DateTime.Now;
                        clientRequestHeader.JoinDate = !string.IsNullOrEmpty(clientRequestHeaderVM.JoinDate) ? DateTime.ParseExact(clientRequestHeaderVM.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        clientRequestHeader.InspectionDate = !string.IsNullOrEmpty(clientRequestHeaderVM.InspectionDate) ? DateTime.ParseExact(clientRequestHeaderVM.InspectionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        clientRequestHeader.AdditionalNote = !string.IsNullOrEmpty(clientRequestHeaderVM.FileNo) ? clientRequestHeaderVM.FileNo : "";
                        clientRequestHeader.PolicyStart = !string.IsNullOrEmpty(clientRequestHeaderVM.PolicyStartDate) ? DateTime.ParseExact(clientRequestHeaderVM.PolicyStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        clientRequestHeader.PolicyEnd = !string.IsNullOrEmpty(clientRequestHeaderVM.PolicyEndDate) ? DateTime.ParseExact(clientRequestHeaderVM.PolicyEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;


                        unitOfWork.TblClientRequestHeaderRepository.Update(clientRequestHeader);
                        unitOfWork.Save();


                       

                        if (clientObj.DeductionDetails.Count > 0)
                        {
                            foreach (var paymentLine in clientObj.DeductionDetails)
                            {

                                tblDeduction deduction = new tblDeduction();
                                deduction.ClientID = clientRequestHeader.ClientID;
                                deduction.PremiumHolderType = paymentLine.PremiumHolderType < 0 ? 0 : paymentLine.PremiumHolderType;
                                if (paymentLine.PremiumHolderType ==2)
                                    deduction.JoinDate = !string.IsNullOrEmpty(paymentLine.JoinDate) ? DateTime.ParseExact(paymentLine.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                if (paymentLine.PremiumHolderType == 1)
                                {
                                    deduction.JoinDate = !string.IsNullOrEmpty(clientRequestHeaderVM.JoinDate) ? DateTime.ParseExact(clientRequestHeaderVM.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                    //deduction.InspectionDate = !string.IsNullOrEmpty(clientRequestHeaderVM.InspectionDate) ? DateTime.ParseExact(clientRequestHeaderVM.InspectionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                }


                                deduction.FamilyMemberID = paymentLine.FamilyMemberID < 0 ? 0 : paymentLine.FamilyMemberID;
                                
                                deduction.PremiumHolder = string.IsNullOrEmpty(paymentLine.PremiumHolder) ? "" : paymentLine.PremiumHolder;
                                deduction.LodingRate = paymentLine.LoadingRate < 0 ? 0 : paymentLine.LoadingRate;
                                deduction.DeductionRate = paymentLine.DeductionRate < 0 ? 0 : paymentLine.DeductionRate;
                                deduction.Premium = paymentLine.PremiumAmount < 0 ? 0 : paymentLine.PremiumAmount;
                                deduction.NetPremium = paymentLine.NetPremium < 0 ? 0 : paymentLine.NetPremium;
                                deduction.Deductibles = string.IsNullOrEmpty(paymentLine.Deductible) ? "" : paymentLine.Deductible;
                                deduction.GroupFamilyMemberID = paymentLine.GroupFamilyMemberID < 0 ? 0 : paymentLine.GroupFamilyMemberID;
                                deduction.PremiumID = paymentLine.PremiumID < 0 ? 0 : paymentLine.PremiumID;
                                deduction.DeductionAmount = 0;
                                deduction.Exclusion = "";
                                deduction.BusinessUnit = clientObj.BusinessUnitID;
                                deduction.DeductionRemarks = "";
                                deduction.OptionalAmount = "0";
                                deduction.LoadingAmount = 0;
                                
                                deduction.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                                deduction.CreatedDate = DateTime.Now;

                                
                                unitOfWork.TblDeductionRepository.Insert(deduction);
                                unitOfWork.Save();
                                

                            }
                        }

                        //Update Deduction
                        //else
                        //{
                        //    tblDeduction deductions = unitOfWork.TblDeductionRepository.GetByID(clientObj.DeductionID);
                        //    deductions.ClientID = clientID;
                        //    deductions.LodingRate = clientObj.LoadnigRate;
                        //    deductions.DeductionRate = clientObj.DeductionRate;
                        //    unitOfWork.TblDeductionRepository.Update(deductions);
                        //    unitOfWork.Save();
                        //}
                        

                        //Save Payment
                        if (clientRequestHeaderVM.PaymentID > 0)
                        {
                            tblPayment payments = unitOfWork.TblPaymentRepository.GetByID(clientRequestHeaderVM.PaymentID);
                            payments.ClientID = clientRequestHeaderVM.ClientID;
                            payments.PaymentAmount = clientRequestHeaderVM.PaymentAmount;
                            payments.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                            payments.CreatedDate = DateTime.Now;
                            unitOfWork.TblPaymentRepository.Update(payments);
                            unitOfWork.Save();
                        }
                        else {
                            tblPayment payment = new tblPayment();
                            payment.ClientID = clientRequestHeaderVM.ClientID;
                            payment.PaymentAmount = clientRequestHeaderVM.PaymentAmount;
                            payment.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                            payment.CreatedDate = DateTime.Now;
                            unitOfWork.TblPaymentRepository.Insert(payment);
                            unitOfWork.Save();
                        }
                        

                        //Complete the Transaction
                        dbTransaction.Commit();

                        errorMessage = "No Error";
                        return true;
                    }
                    else
                    {
                        errorMessage = "Quotations are created based on this request. Therefore it cannot be modified";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();

                    errorMessage = "Update Failed";
                    return false;
                }
            }
        }


        public bool UpdatePilotRequest(ClientRequestHeaderVM clientRequestHeaderVM, bool isClientUpdated, bool isClientAdded, ClientVM clientObj, out string errorMessage)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    int clientID = 0;

                    if (clientRequestHeaderVM.IsQuotationCreated != true)
                    {
                        if (isClientUpdated)
                        {
                            clientID = clientObj.ClientID;

                            //Update Client
                            tblClient client = unitOfWork.TblClientRepository.GetByID(clientObj.ClientID);
                            client.ClientName = clientObj.ClientName;
                            client.ClientAddress = clientObj.ClientAddress;
                            client.NIC = clientObj.NIC;
                            client.ContactNo = clientObj.ContactNo;
                            client.FixedLine = clientObj.FixedLine;
                            client.Email = clientObj.Email;
                            client.DOB = !string.IsNullOrEmpty(clientObj.DOB) ? DateTime.ParseExact(clientObj.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            client.PPID = clientObj.PPID;
                            client.FamilyDiscount = clientObj.FamilyDiscount;
                            client.AdditionalNote = clientObj.AdditionalNote;
                            client.HomeCountryID = clientObj.HomeCountryID;
                            client.ResidentCountryID = clientObj.ResidentCountryID;
                            client.BUID = clientObj.BusinessUnitID;
                            client.JoinDate = !string.IsNullOrEmpty(clientRequestHeaderVM.JoinDate) ? DateTime.ParseExact(clientRequestHeaderVM.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            client.ModifiedBy = clientRequestHeaderVM.ModifiedBy;
                            client.ModifiedDate = DateTime.Now;
                            unitOfWork.TblClientRepository.Update(client);
                            unitOfWork.Save();


                            var familyData = unitOfWork.TblFamilyMemberRepository.Get(x => x.ClientID == clientObj.ClientID).ToList();

                            foreach (var family in familyData)
                            {
                                unitOfWork.TblFamilyMemberRepository.Delete(family);
                                unitOfWork.Save();
                            }

                            foreach (var requestLine in clientRequestHeaderVM.FamilyDetails)
                            {

                                tblFamilyMember tblFamilyMember = new tblFamilyMember();
                                tblFamilyMember.ClientID = clientRequestHeaderVM.ClientID;
                                //tblFamilyMember.MemberName = requestLine.MemberName;
                                //tblFamilyMember.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null; ;
                                //tblFamilyMember.NICNo = requestLine.NIC;
                                //tblFamilyMember.ContactNo = requestLine.ContactNo;
                                tblFamilyMember.Title = requestLine.TitleID < 0 ? 0 : requestLine.TitleID;
                                tblFamilyMember.MemberName = string.IsNullOrEmpty(requestLine.MemberName) ? "" : requestLine.MemberName;
                                tblFamilyMember.MemberOtherName = string.IsNullOrEmpty(requestLine.MemberOtherName) ? "" : requestLine.MemberOtherName;
                                tblFamilyMember.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyMember.JoinDate = !string.IsNullOrEmpty(requestLine.JoinDate) ? DateTime.ParseExact(requestLine.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyMember.NICNo = !string.IsNullOrEmpty(requestLine.NIC) ? requestLine.NIC : "";
                                tblFamilyMember.ContactNo = !string.IsNullOrEmpty(requestLine.ContactNo) ? requestLine.ContactNo : "";
                                tblFamilyMember.HomeCountry = requestLine.MemberCountryID < 0 ? 0 : requestLine.MemberCountryID;
                                tblFamilyMember.CountryOfResident = requestLine.MemberResCountryID < 0 ? 0 : requestLine.MemberResCountryID;
                                tblFamilyMember.RelationShipID = requestLine.RelationShipID < 0 ? 0 : requestLine.RelationShipID;
                                tblFamilyMember.GenderID = requestLine.GenderID < 0 ? 0 : requestLine.GenderID;


                                if (requestLine.FamilyMemberID < 0)
                                {

                                    unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyMember);
                                    unitOfWork.Save();
                                }
                                else
                                {
                                    unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyMember);
                                    unitOfWork.Save();
                                }

                                //if (requestLine.FamilyMemberID > 0)
                                //{
                                //    tblFamilyMember family = unitOfWork.TblFamilyMemberRepository.GetByID(requestLine.FamilyMemberID);
                                //    family.ClientID = clientRequestHeaderVM.ClientID;
                                //    family.MemberName = requestLine.MemberName;
                                //    family.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null; ;
                                //    //clientRequestLine.CreatedDate = DateTime.Now;
                                //    unitOfWork.TblFamilyMemberRepository.Update(family);
                                //    unitOfWork.Save();
                                //}
                                //else
                                //{

                                // }

                            }
                        }
                        else if (isClientAdded)
                        {
                            //Save Client
                            tblClient client = new tblClient();
                            client.ClientName = clientObj.ClientName;
                            client.ClientAddress = clientObj.ClientAddress;
                            client.NIC = clientObj.NIC;
                            client.ContactNo = clientObj.ContactNo;
                            client.FixedLine = clientObj.FixedLine;
                            client.Email = clientObj.Email;
                            client.DOB = !string.IsNullOrEmpty(clientObj.DOB) ? DateTime.ParseExact(clientObj.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            client.PPID = clientObj.PPID;
                            client.FamilyDiscount = clientObj.FamilyDiscount;
                            client.AdditionalNote = clientObj.AdditionalNote;
                            client.HomeCountryID = clientObj.HomeCountryID;
                            client.ResidentCountryID = clientObj.ResidentCountryID;
                            client.BUID = clientObj.BusinessUnitID;
                            client.JoinDate = !string.IsNullOrEmpty(clientRequestHeaderVM.JoinDate) ? DateTime.ParseExact(clientRequestHeaderVM.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            client.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                            client.CreatedDate = DateTime.Now;
                            unitOfWork.TblClientRepository.Insert(client);
                            unitOfWork.Save();
                            foreach (var requestLine in clientRequestHeaderVM.FamilyDetails)
                            {
                                tblFamilyMember tblFamilyMember = new tblFamilyMember();
                                tblFamilyMember.ClientID = clientRequestHeaderVM.ClientID;
                                tblFamilyMember.MemberName = requestLine.MemberName;
                                tblFamilyMember.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null; ;
                                tblFamilyMember.NICNo = requestLine.NIC;
                                tblFamilyMember.ContactNo = requestLine.ContactNo;
                                //clientRequestLine.CreatedDate = DateTime.Now;
                                unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyMember);
                                unitOfWork.Save();
                            }

                            clientID = client.ClientID;
                        }
                        else
                        {
                            clientID = clientObj.ClientID;
                        }

                        //Update Client Request Header
                        tblClientRequestHeader clientRequestHeader = unitOfWork.TblClientRequestHeaderRepository.GetByID(clientRequestHeaderVM.ClientRequestHeaderID);
                        clientRequestHeader.ClientID = clientRequestHeaderVM.ClientID;
                        clientRequestHeader.PartnerID = clientRequestHeaderVM.PartnerID;
                        clientRequestHeader.RequestedDate = !string.IsNullOrEmpty(clientRequestHeaderVM.RequestedDate) ? DateTime.ParseExact(clientRequestHeaderVM.RequestedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        clientRequestHeader.ModifiedBy = clientRequestHeaderVM.AgentID;
                        clientRequestHeader.ModifiedDate = DateTime.Now;
                        clientRequestHeader.JoinDate = !string.IsNullOrEmpty(clientRequestHeaderVM.JoinDate) ? DateTime.ParseExact(clientRequestHeaderVM.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        clientRequestHeader.InspectionDate = !string.IsNullOrEmpty(clientRequestHeaderVM.InspectionDate) ? DateTime.ParseExact(clientRequestHeaderVM.InspectionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        clientRequestHeader.AdditionalNote = !string.IsNullOrEmpty(clientRequestHeaderVM.FileNo) ? clientRequestHeaderVM.FileNo : "";
                        clientRequestHeader.PolicyStart = !string.IsNullOrEmpty(clientRequestHeaderVM.PolicyStartDate) ? DateTime.ParseExact(clientRequestHeaderVM.PolicyStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        clientRequestHeader.PolicyEnd = !string.IsNullOrEmpty(clientRequestHeaderVM.PolicyEndDate) ? DateTime.ParseExact(clientRequestHeaderVM.PolicyEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;


                        unitOfWork.TblClientRequestHeaderRepository.Update(clientRequestHeader);
                        unitOfWork.Save();


                        var deductdata = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == clientObj.ClientID ).ToList();

                        foreach (var family in deductdata)
                        {
                            unitOfWork.TblDeductionRepository.Delete(family);
                            unitOfWork.Save();
                        }


                        if (clientObj.DeductionDetails.Count > 0)
                        {
                            foreach (var paymentLine in clientObj.DeductionDetails)
                            {

                                tblDeduction deduction = new tblDeduction();
                                deduction.ClientID = clientRequestHeader.ClientID;
                                deduction.PremiumHolderType = paymentLine.PremiumHolderType < 0 ? 0 : paymentLine.PremiumHolderType;
                                if (paymentLine.PremiumHolderType == 2)
                                    deduction.JoinDate = !string.IsNullOrEmpty(paymentLine.JoinDate) ? DateTime.ParseExact(paymentLine.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                if (paymentLine.PremiumHolderType == 1)
                                {
                                    deduction.JoinDate = !string.IsNullOrEmpty(clientRequestHeaderVM.JoinDate) ? DateTime.ParseExact(clientRequestHeaderVM.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                    //deduction.InspectionDate = !string.IsNullOrEmpty(clientRequestHeaderVM.InspectionDate) ? DateTime.ParseExact(clientRequestHeaderVM.InspectionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                }


                                deduction.FamilyMemberID = paymentLine.FamilyMemberID < 0 ? 0 : paymentLine.FamilyMemberID;

                                deduction.PremiumHolder = string.IsNullOrEmpty(paymentLine.PremiumHolder) ? "" : paymentLine.PremiumHolder;
                                deduction.LodingRate = paymentLine.LoadingRate < 0 ? 0 : paymentLine.LoadingRate;
                                deduction.DeductionRate = paymentLine.DeductionRate < 0 ? 0 : paymentLine.DeductionRate;
                                deduction.Premium = paymentLine.PremiumAmount < 0 ? 0 : paymentLine.PremiumAmount;
                                deduction.NetPremium = paymentLine.NetPremium < 0 ? 0 : paymentLine.NetPremium;
                                deduction.Deductibles = string.IsNullOrEmpty(paymentLine.Deductible) ? "" : paymentLine.Deductible;
                                deduction.GroupFamilyMemberID = paymentLine.GroupFamilyMemberID < 0 ? 0 : paymentLine.GroupFamilyMemberID;
                                deduction.PremiumID = paymentLine.PremiumID < 0 ? 0 : paymentLine.PremiumID;
                                deduction.DeductionAmount = 0;
                                deduction.Exclusion = "";
                                deduction.BusinessUnit = clientObj.BusinessUnitID;
                                deduction.DeductionRemarks = "";
                                deduction.OptionalAmount = "0";
                                deduction.LoadingAmount = 0;

                                deduction.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                                deduction.CreatedDate = DateTime.Now;


                                unitOfWork.TblDeductionRepository.Insert(deduction);
                                unitOfWork.Save();


                            }
                        }

                        //Update Deduction
                        //else
                        //{
                        //    tblDeduction deductions = unitOfWork.TblDeductionRepository.GetByID(clientObj.DeductionID);
                        //    deductions.ClientID = clientID;
                        //    deductions.LodingRate = clientObj.LoadnigRate;
                        //    deductions.DeductionRate = clientObj.DeductionRate;
                        //    unitOfWork.TblDeductionRepository.Update(deductions);
                        //    unitOfWork.Save();
                        //}


                        //Save Payment
                        if (clientRequestHeaderVM.PaymentID > 0)
                        {
                            tblPayment payments = unitOfWork.TblPaymentRepository.GetByID(clientRequestHeaderVM.PaymentID);
                            payments.ClientID = clientRequestHeaderVM.ClientID;
                            payments.PaymentAmount = clientRequestHeaderVM.PaymentAmount;
                            payments.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                            payments.CreatedDate = DateTime.Now;
                            unitOfWork.TblPaymentRepository.Update(payments);
                            unitOfWork.Save();
                        }
                        else
                        {
                            tblPayment payment = new tblPayment();
                            payment.ClientID = clientRequestHeaderVM.ClientID;
                            payment.PaymentAmount = clientRequestHeaderVM.PaymentAmount;
                            payment.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                            payment.CreatedDate = DateTime.Now;
                            unitOfWork.TblPaymentRepository.Insert(payment);
                            unitOfWork.Save();
                        }


                        //Complete the Transaction
                        dbTransaction.Commit();

                        errorMessage = "No Error";
                        return true;
                    }
                    else
                    {
                        errorMessage = "Quotations are created based on this request. Therefore it cannot be modified";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();

                    errorMessage = "Update Failed";
                    return false;
                }
            }
        }


        public bool UpdateAvivaRequest(ClientRequestHeaderVM clientRequestHeaderVM, bool isClientUpdated, bool isClientAdded, ClientVM clientObj, out string errorMessage)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    int clientID = 0;

                    if (clientRequestHeaderVM.IsQuotationCreated != true)
                    {
                        if (isClientUpdated)
                        {
                            clientID = clientObj.ClientID;

                            //Update Client
                            tblClient client = unitOfWork.TblClientRepository.GetByID(clientObj.ClientID);
                            client.ClientName = clientObj.ClientName;
                            client.ClientAddress = clientObj.ClientAddress;
                            client.NIC = clientObj.NIC;
                            client.ContactNo = clientObj.ContactNo;
                            client.FixedLine = clientObj.FixedLine;
                            client.Email = clientObj.Email;
                            client.DOB = !string.IsNullOrEmpty(clientObj.DOB) ? DateTime.ParseExact(clientObj.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            client.PPID = clientObj.PPID;
                            client.FamilyDiscount = clientObj.FamilyDiscount;
                            client.AdditionalNote = clientObj.AdditionalNote;
                            client.HomeCountryID = clientObj.HomeCountryID;
                            client.ResidentCountryID = clientObj.ResidentCountryID;
                            client.BUID = clientObj.BusinessUnitID;
                            client.JoinDate = !string.IsNullOrEmpty(clientRequestHeaderVM.JoinDate) ? DateTime.ParseExact(clientRequestHeaderVM.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            client.ModifiedBy = clientRequestHeaderVM.ModifiedBy;
                            client.ModifiedDate = DateTime.Now;
                            unitOfWork.TblClientRepository.Update(client);
                            unitOfWork.Save();


                            var familyData = unitOfWork.TblFamilyMemberRepository.Get(x => x.ClientID == clientObj.ClientID).ToList();

                            foreach (var family in familyData)
                            {
                                unitOfWork.TblFamilyMemberRepository.Delete(family);
                                unitOfWork.Save();
                            }

                            foreach (var requestLine in clientRequestHeaderVM.FamilyDetails)
                            {

                                tblFamilyMember tblFamilyMember = new tblFamilyMember();
                                tblFamilyMember.ClientID = clientRequestHeaderVM.ClientID;
                                //tblFamilyMember.MemberName = requestLine.MemberName;
                                //tblFamilyMember.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null; ;
                                //tblFamilyMember.NICNo = requestLine.NIC;
                                //tblFamilyMember.ContactNo = requestLine.ContactNo;
                                tblFamilyMember.Title = requestLine.TitleID < 0 ? 0 : requestLine.TitleID;
                                tblFamilyMember.MemberName = string.IsNullOrEmpty(requestLine.MemberName) ? "" : requestLine.MemberName;
                                tblFamilyMember.MemberOtherName = string.IsNullOrEmpty(requestLine.MemberOtherName) ? "" : requestLine.MemberOtherName;
                                tblFamilyMember.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyMember.JoinDate = !string.IsNullOrEmpty(requestLine.JoinDate) ? DateTime.ParseExact(requestLine.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyMember.NICNo = !string.IsNullOrEmpty(requestLine.NIC) ? requestLine.NIC : "";
                                tblFamilyMember.ContactNo = !string.IsNullOrEmpty(requestLine.ContactNo) ? requestLine.ContactNo : "";
                                tblFamilyMember.HomeCountry = requestLine.MemberCountryID < 0 ? 0 : requestLine.MemberCountryID;
                                tblFamilyMember.CountryOfResident = requestLine.MemberResCountryID < 0 ? 0 : requestLine.MemberResCountryID;
                                tblFamilyMember.RelationShipID = requestLine.RelationShipID < 0 ? 0 : requestLine.RelationShipID;
                                tblFamilyMember.GenderID = requestLine.GenderID < 0 ? 0 : requestLine.GenderID;


                                if (requestLine.FamilyMemberID < 0)
                                {

                                    unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyMember);
                                    unitOfWork.Save();
                                }
                                else
                                {
                                    unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyMember);
                                    unitOfWork.Save();
                                }

                                //if (requestLine.FamilyMemberID > 0)
                                //{
                                //    tblFamilyMember family = unitOfWork.TblFamilyMemberRepository.GetByID(requestLine.FamilyMemberID);
                                //    family.ClientID = clientRequestHeaderVM.ClientID;
                                //    family.MemberName = requestLine.MemberName;
                                //    family.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null; ;
                                //    //clientRequestLine.CreatedDate = DateTime.Now;
                                //    unitOfWork.TblFamilyMemberRepository.Update(family);
                                //    unitOfWork.Save();
                                //}
                                //else
                                //{

                                // }

                            }
                        }
                        else if (isClientAdded)
                        {
                            //Save Client
                            tblClient client = new tblClient();
                            client.ClientName = clientObj.ClientName;
                            client.ClientAddress = clientObj.ClientAddress;
                            client.NIC = clientObj.NIC;
                            client.ContactNo = clientObj.ContactNo;
                            client.FixedLine = clientObj.FixedLine;
                            client.Email = clientObj.Email;
                            client.DOB = !string.IsNullOrEmpty(clientObj.DOB) ? DateTime.ParseExact(clientObj.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            client.PPID = clientObj.PPID;
                            client.FamilyDiscount = clientObj.FamilyDiscount;
                            client.AdditionalNote = clientObj.AdditionalNote;
                            client.HomeCountryID = clientObj.HomeCountryID;
                            client.ResidentCountryID = clientObj.ResidentCountryID;
                            client.BUID = clientObj.BusinessUnitID;
                            client.JoinDate = !string.IsNullOrEmpty(clientRequestHeaderVM.JoinDate) ? DateTime.ParseExact(clientRequestHeaderVM.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            client.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                            client.CreatedDate = DateTime.Now;
                            unitOfWork.TblClientRepository.Insert(client);
                            unitOfWork.Save();
                            foreach (var requestLine in clientRequestHeaderVM.FamilyDetails)
                            {
                                tblFamilyMember tblFamilyMember = new tblFamilyMember();
                                tblFamilyMember.ClientID = clientRequestHeaderVM.ClientID;
                                tblFamilyMember.MemberName = requestLine.MemberName;
                                tblFamilyMember.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null; ;
                                tblFamilyMember.NICNo = requestLine.NIC;
                                tblFamilyMember.ContactNo = requestLine.ContactNo;
                                //clientRequestLine.CreatedDate = DateTime.Now;
                                unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyMember);
                                unitOfWork.Save();
                            }

                            clientID = client.ClientID;
                        }
                        else
                        {
                            clientID = clientObj.ClientID;
                        }

                        //Update Client Request Header
                        tblClientRequestHeader clientRequestHeader = unitOfWork.TblClientRequestHeaderRepository.GetByID(clientRequestHeaderVM.ClientRequestHeaderID);
                        clientRequestHeader.ClientID = clientRequestHeaderVM.ClientID;
                        clientRequestHeader.PartnerID = clientRequestHeaderVM.PartnerID;
                        clientRequestHeader.RequestedDate = !string.IsNullOrEmpty(clientRequestHeaderVM.RequestedDate) ? DateTime.ParseExact(clientRequestHeaderVM.RequestedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        clientRequestHeader.ModifiedBy = clientRequestHeaderVM.AgentID;
                        clientRequestHeader.ModifiedDate = DateTime.Now;
                        clientRequestHeader.JoinDate = !string.IsNullOrEmpty(clientRequestHeaderVM.JoinDate) ? DateTime.ParseExact(clientRequestHeaderVM.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        clientRequestHeader.InspectionDate = !string.IsNullOrEmpty(clientRequestHeaderVM.InspectionDate) ? DateTime.ParseExact(clientRequestHeaderVM.InspectionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        clientRequestHeader.AdditionalNote = !string.IsNullOrEmpty(clientRequestHeaderVM.FileNo) ? clientRequestHeaderVM.FileNo : "";
                        clientRequestHeader.PolicyStart = !string.IsNullOrEmpty(clientRequestHeaderVM.PolicyStartDate) ? DateTime.ParseExact(clientRequestHeaderVM.PolicyStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        clientRequestHeader.PolicyEnd = !string.IsNullOrEmpty(clientRequestHeaderVM.PolicyEndDate) ? DateTime.ParseExact(clientRequestHeaderVM.PolicyEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;


                        unitOfWork.TblClientRequestHeaderRepository.Update(clientRequestHeader);
                        unitOfWork.Save();


                        var deductdata = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == clientObj.ClientID).ToList();

                        foreach (var family in deductdata)
                        {
                            unitOfWork.TblDeductionRepository.Delete(family);
                            unitOfWork.Save();
                        }


                        if (clientObj.DeductionDetails.Count > 0)
                        {
                            foreach (var paymentLine in clientObj.DeductionDetails)
                            {

                                tblDeduction deduction = new tblDeduction();
                                deduction.ClientID = clientRequestHeader.ClientID;
                                deduction.PremiumHolderType = paymentLine.PremiumHolderType < 0 ? 0 : paymentLine.PremiumHolderType;
                                if (paymentLine.PremiumHolderType == 2)
                                    deduction.JoinDate = !string.IsNullOrEmpty(paymentLine.JoinDate) ? DateTime.ParseExact(paymentLine.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                if (paymentLine.PremiumHolderType == 1)
                                {
                                    deduction.JoinDate = !string.IsNullOrEmpty(clientRequestHeaderVM.JoinDate) ? DateTime.ParseExact(clientRequestHeaderVM.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                    //deduction.InspectionDate = !string.IsNullOrEmpty(clientRequestHeaderVM.InspectionDate) ? DateTime.ParseExact(clientRequestHeaderVM.InspectionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                }


                                deduction.FamilyMemberID = paymentLine.FamilyMemberID < 0 ? 0 : paymentLine.FamilyMemberID;

                                deduction.PremiumHolder = string.IsNullOrEmpty(paymentLine.PremiumHolder) ? "" : paymentLine.PremiumHolder;
                                deduction.LodingRate = paymentLine.LoadingRate < 0 ? 0 : paymentLine.LoadingRate;
                                deduction.DeductionRate = paymentLine.DeductionRate < 0 ? 0 : paymentLine.DeductionRate;
                                deduction.Premium = paymentLine.PremiumAmount < 0 ? 0 : paymentLine.PremiumAmount;
                                deduction.NetPremium = paymentLine.NetPremium < 0 ? 0 : paymentLine.NetPremium;
                                deduction.Deductibles = string.IsNullOrEmpty(paymentLine.Deductible) ? "" : paymentLine.Deductible;
                                deduction.GroupFamilyMemberID = paymentLine.GroupFamilyMemberID < 0 ? 0 : paymentLine.GroupFamilyMemberID;
                                deduction.PremiumID = paymentLine.PremiumID < 0 ? 0 : paymentLine.PremiumID;
                                deduction.DeductionAmount = 0;
                                deduction.Exclusion = "";
                                deduction.BusinessUnit = clientObj.BusinessUnitID;
                                deduction.DeductionRemarks = "";
                                deduction.OptionalAmount = "0";
                                deduction.LoadingAmount = 0;

                                deduction.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                                deduction.CreatedDate = DateTime.Now;


                                unitOfWork.TblDeductionRepository.Insert(deduction);
                                unitOfWork.Save();


                            }
                        }

                        //Update Deduction
                        //else
                        //{
                        //    tblDeduction deductions = unitOfWork.TblDeductionRepository.GetByID(clientObj.DeductionID);
                        //    deductions.ClientID = clientID;
                        //    deductions.LodingRate = clientObj.LoadnigRate;
                        //    deductions.DeductionRate = clientObj.DeductionRate;
                        //    unitOfWork.TblDeductionRepository.Update(deductions);
                        //    unitOfWork.Save();
                        //}


                        //Save Payment
                        if (clientRequestHeaderVM.PaymentID > 0)
                        {
                            tblPayment payments = unitOfWork.TblPaymentRepository.GetByID(clientRequestHeaderVM.PaymentID);
                            payments.ClientID = clientRequestHeaderVM.ClientID;
                            payments.PaymentAmount = clientRequestHeaderVM.PaymentAmount;
                            payments.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                            payments.CreatedDate = DateTime.Now;
                            unitOfWork.TblPaymentRepository.Update(payments);
                            unitOfWork.Save();
                        }
                        else
                        {
                            tblPayment payment = new tblPayment();
                            payment.ClientID = clientRequestHeaderVM.ClientID;
                            payment.PaymentAmount = clientRequestHeaderVM.PaymentAmount;
                            payment.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                            payment.CreatedDate = DateTime.Now;
                            unitOfWork.TblPaymentRepository.Insert(payment);
                            unitOfWork.Save();
                        }


                        //Complete the Transaction
                        dbTransaction.Commit();

                        errorMessage = "No Error";
                        return true;
                    }
                    else
                    {
                        errorMessage = "Quotations are created based on this request. Therefore it cannot be modified";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();

                    errorMessage = "Update Failed";
                    return false;
                }
            }
        }
        public bool UpdateBUPARequest(ClientRequestHeaderVM clientRequestHeaderVM, bool isClientUpdated, bool isClientAdded, ClientVM clientObj, out string errorMessage)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    int clientID = 0;
                    clientRequestHeaderVM.transactionType = 1;
                    var Year= string.IsNullOrEmpty(clientRequestHeaderVM.Year) ? 0 : int.Parse(clientRequestHeaderVM.Year);
                    var fid= string.IsNullOrEmpty(clientRequestHeaderVM.FrequncyID.ToString()) ? "0" : clientRequestHeaderVM.FrequncyID.ToString();
                    var fFid = string.IsNullOrEmpty(clientRequestHeaderVM.FrequncyDID.ToString()) ? "0" : clientRequestHeaderVM.FrequncyDID.ToString();


                    if (clientRequestHeaderVM.IsQuotationCreated != true)
                    {


                        if (isClientUpdated)
                        {
                            clientID = clientObj.ClientID;

                            //Update Client
                            tblClient client = unitOfWork.TblClientRepository.GetByID(clientObj.ClientID);
                            //client.ClientName = clientObj.ClientName;
                            //client.ClientAddress = clientObj.ClientAddress;
                            //client.NIC = clientObj.NIC;
                            //client.ContactNo = clientObj.ContactNo;
                            //client.FixedLine = clientObj.FixedLine;
                            //client.Email = clientObj.Email;
                            //client.DOB = !string.IsNullOrEmpty(clientObj.DOB) ? DateTime.ParseExact(clientObj.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            //client.PPID = clientObj.PPID;
                            //client.FamilyDiscount = clientObj.FamilyDiscount;
                            //client.AdditionalNote = clientObj.AdditionalNote;
                            //client.HomeCountryID = clientObj.HomeCountryID;
                            //client.ResidentCountryID = clientObj.ResidentCountryID;
                            //client.BUID = clientObj.BusinessUnitID;

                            client.TitleID =clientObj.TitleID < 0 ? 0 :clientObj.TitleID;
                            client.ClientName = string.IsNullOrEmpty(clientObj.ClientName) ? "" :clientObj.ClientName;
                            client.ClientAddress =clientObj.ClientAddress;
                            client.ExtraText =clientObj.ClientOtherName;
                            client.NIC =clientObj.NIC;
                            client.ContactNo =clientObj.ContactNo;
                            client.FixedLine =clientObj.FixedLine;
                            client.Email =clientObj.Email;
                            client.DOB = !string.IsNullOrEmpty(clientObj.DOB) ? DateTime.ParseExact(clientObj.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            client.PPID =clientObj.PPID;
                            client.FamilyDiscount =clientObj.FamilyDiscount;
                            client.AdditionalNote =clientObj.AdditionalNote;
                            client.HomeCountryID =clientObj.HomeCountryID;
                            client.ResidentCountryID =clientObj.ResidentCountryID;
                            client.BUID =clientObj.BusinessUnitID;

                            client.ExtraInt1 = string.IsNullOrEmpty(clientRequestHeaderVM.Year) ? 0 : int.Parse(clientRequestHeaderVM.Year);



                            client.ModifiedBy = clientRequestHeaderVM.ModifiedBy;
                            client.ModifiedDate = DateTime.Now;
                            client.IsActive = clientObj.ClientStatus;
                          //  client.SeqNo = clientRequestHeaderVM.ClientDetails.PremiumAccept == "2" ? "" : "1";
                            unitOfWork.TblClientRepository.Update(client);
                            unitOfWork.Save();



                            //if (Year == client.ExtraInt1)
                            //{

                                var familyData = unitOfWork.TblFamilyMemberRepository.Get(x => x.ClientID == clientObj.ClientID & x.ExtraText1 == client.ExtraInt1.ToString() & x.FrequncyID== client.FrequncyID.ToString() & x.FrequncyDID== client.FrequncyDID).ToList();
                                foreach (var family in familyData)
                                {
                                    unitOfWork.TblFamilyMemberRepository.Delete(family);
                                    unitOfWork.Save();
                                }

                                var familyGrpData = unitOfWork.TblGrpFamilyDetailRepository.Get(x => x.ClientID == clientObj.ClientID & x.ExtraText2 == client.ExtraInt1.ToString() & x.FrequncyID == client.FrequncyID.ToString() & x.FrequncyDID == client.FrequncyDID).ToList();
                                foreach (var family in familyGrpData)
                                {
                                    unitOfWork.TblGrpFamilyDetailRepository.Delete(family);
                                    unitOfWork.Save();
                                }


                            var fseqNo = 1;
                            var fSubseqNo = 1;
                            var fMseqNo = 1;

                            foreach (var requestLine in clientRequestHeaderVM.FamilyDetails)
                                {
                                    //tblFamilyMember tblFamilyMember = new tblFamilyMember();
                                    //tblFamilyMember.ClientID = clientRequestHeaderVM.ClientID;
                                    //tblFamilyMember.MemberName = requestLine.MemberName;
                                    //tblFamilyMember.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null; ;
                                    //tblFamilyMember.NICNo = requestLine.NIC;
                                    //tblFamilyMember.ContactNo = requestLine.ContactNo;
                                    //tblFamilyMember.IsActive = requestLine.IsActive;
                                    //clientRequestLine.CreatedDate = DateTime.Now;


                                    tblFamilyMember tblFamilyDetail = new tblFamilyMember();
                                    //tblFamilyDetail.ClientID = clientID;
                                    //tblFamilyDetail.MemberName = requestLine.MemberName;
                                    //tblFamilyDetail.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                    //tblFamilyDetail.NICNo = requestLine.NIC;
                                    //tblFamilyDetail.ContactNo = requestLine.ContactNo;
                                    //tblFamilyDetail.JoinDate = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                                    // tblFamilyDetail.FamilyMemberID = requestLine.FamilyMemberID;
                                    //clientRequestLine.CreatedDate = DateTime.Now;

                                    tblFamilyDetail.ClientID = clientID;
                                    tblFamilyDetail.Title = requestLine.TitleID < 0 ? 0 : requestLine.TitleID;
                                    tblFamilyDetail.MemberName = string.IsNullOrEmpty(requestLine.MemberName) ? "" : requestLine.MemberName;
                                    tblFamilyDetail.MemberOtherName = string.IsNullOrEmpty(requestLine.MemberOtherName) ? "" : requestLine.MemberOtherName;

                                    //if (requestLine.MemberDOB.Length > 10)
                                    //    requestLine.MemberDOB = requestLine.MemberDOB.Substring(requestLine.MemberDOB.Length - 10).ToString();





                                    tblFamilyDetail.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                                    //if (requestLine.InceptionDate.Length > 10)
                                    //    requestLine.InceptionDate = requestLine.InceptionDate.Substring(requestLine.InceptionDate.Length - 10).ToString();


                                    tblFamilyDetail.JoinDate = !string.IsNullOrEmpty(requestLine.JoinDate) ? DateTime.ParseExact(requestLine.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                    tblFamilyDetail.NICNo = !string.IsNullOrEmpty(requestLine.NIC) ? requestLine.NIC : "";
                                    tblFamilyDetail.ContactNo = !string.IsNullOrEmpty(requestLine.ContactNo) ? requestLine.ContactNo : "";
                                    tblFamilyDetail.HomeCountry = requestLine.HomeCountryID < 0 ? 0 : requestLine.HomeCountryID;
                                    tblFamilyDetail.CountryOfResident = requestLine.
    ResidentCountryID < 0 ? 0 : requestLine.
    ResidentCountryID;
                                    tblFamilyDetail.premiumID = !string.IsNullOrEmpty(requestLine.SchemeID.ToString()) ? requestLine.SchemeID.ToString() : "";
                                    tblFamilyDetail.RelationShipID = requestLine.RelationShipID < 0 ? 0 : requestLine.RelationShipID;
                                    tblFamilyDetail.GenderID = requestLine.GenderID < 0 ? 0 : requestLine.GenderID;
                                    //  tblFamilyDetail.ExtraText2= !string.IsNullOrEmpty(requestLine.Exclusion) ? requestLine.Exclusion : "";
                                    tblFamilyDetail.MembershipID = !string.IsNullOrEmpty(requestLine.MembershipID) ? requestLine.MembershipID : "";
                                    tblFamilyDetail.Exclusions = 0;
                                    tblFamilyDetail.OptionalCover = requestLine.OptionalCover;
                                    tblFamilyDetail.IsActive = requestLine.IsActive;
                                    tblFamilyDetail.Exclu = requestLine.Exclu;

                                    tblFamilyDetail.ExtraText1 = Year.ToString();
                                   tblFamilyDetail.FrequncyID = clientRequestHeaderVM.FrequncyID.ToString();
                                   tblFamilyDetail.FrequncyDID = clientRequestHeaderVM.FrequncyDID.ToString();
                                if (clientObj.PremiumAccept == "2")
                                {
                                    tblFamilyDetail.SeqNo = fseqNo.ToString();
                                    tblFamilyDetail.SeqSubNo = "";
                                }
                                else
                                {
                                    tblFamilyDetail.SeqNo = "1";
                                    tblFamilyDetail.SeqSubNo = fSubseqNo.ToString();
                                }

                                unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyDetail);
                                    unitOfWork.Save();

                                    if (requestLine.GroupMemberDetails != null)
                                    {
                                    fMseqNo = 1;
                                    foreach (var grpMember in requestLine.GroupMemberDetails)
                                        {
                                            //tblGrpFamilyDetail tblGrpFamily = new tblGrpFamilyDetail();
                                            //tblGrpFamily.ClientID = clientID;
                                            //tblGrpFamily.MemberName = grpMember.MemberName;
                                            //tblGrpFamily.MemberDOB = !string.IsNullOrEmpty(grpMember.MemberDOB) ? DateTime.ParseExact(grpMember.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                            //tblGrpFamily.MemberNIC = grpMember.NIC;
                                            //tblGrpFamily.MemberContact = grpMember.ContactNo;
                                            //tblGrpFamily.FamilyMemberID = tblFamilyDetail.FamilyMemberID;


                                            tblGrpFamilyDetail tblGrpFamily = new tblGrpFamilyDetail();
                                            tblGrpFamily.ClientID = clientID;
                                            tblGrpFamily.TitleID = grpMember.TitleID < 0 ? 0 : grpMember.TitleID;
                                            tblGrpFamily.MemberName = grpMember.MemberName;
                                            tblGrpFamily.MembershipID = grpMember.MembershipID;
                                            tblGrpFamily.ExtraText1 = string.IsNullOrEmpty(grpMember.MemberOtherName) ? "" : grpMember.MemberOtherName;
                                            //     tblGrpFamily.MemberOtherName = string.IsNullOrEmpty(grpMember.MemberOtherName) ? "" : grpMember.MemberOtherName;

                                            //if (grpMember.MemberDOB.Length > 10)
                                            //    grpMember.MemberDOB = grpMember.MemberDOB.Substring(grpMember.MemberDOB.Length - 10).ToString();

                                            tblGrpFamily.MemberDOB = !string.IsNullOrEmpty(grpMember.MemberDOB) ? DateTime.ParseExact(grpMember.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                                            //if (grpMember.JoinDate.Length > 10)
                                            //   grpMember.JoinDate = grpMember.JoinDate.Substring(grpMember.JoinDate.Length - 10).ToString();
                                            tblGrpFamily.RelationShipID = grpMember.RelationShipID < 0 ? 0 : grpMember.RelationShipID;

                                            tblGrpFamily.JoinDate = !string.IsNullOrEmpty(grpMember.JoinDate) ? DateTime.ParseExact(grpMember.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                            tblGrpFamily.MemberNIC = grpMember.NIC;
                                            tblGrpFamily.MemberContact = grpMember.ContactNo;
                                            tblGrpFamily.FamilyMemberID = tblFamilyDetail.FamilyMemberID;
                                            //tblGrpFamily.MemberCountryID = grpMember.countryID < 0 ? 0 : (int)grpMember.MemberCountryID;
                                            tblGrpFamily.MemberCountryID = grpMember.MemberCountryID < 0 ? 0 : grpMember.MemberCountryID;

                                            tblGrpFamily.MemberResCountryID = grpMember.ResidentCountryID < 0 ? 0 : grpMember.ResidentCountryID;
                                            tblGrpFamily.Exclusions = 0;
                                            tblGrpFamily.Exclu = grpMember.Exclu;
                                            tblGrpFamily.OptionalCovers = requestLine.OptionalCover;
                                            tblGrpFamily.IsActive = grpMember.IsActive < 0 ? 0 : grpMember.IsActive;







                                            tblGrpFamily.ExtraText2 = Year.ToString();

                                           tblGrpFamily.FrequncyID = clientRequestHeaderVM.FrequncyID.ToString();
                                         tblGrpFamily.FrequncyDID = clientRequestHeaderVM.FrequncyDID.ToString();

                                        tblGrpFamily.SeqNo = fseqNo.ToString();
                                        tblGrpFamily.SeqSubNo = fMseqNo.ToString();
                                        //clientRequestLine.CreatedDate = DateTime.Now;
                                        unitOfWork.TblGrpFamilyDetailRepository.Insert(tblGrpFamily);
                                            unitOfWork.Save();



                                        fMseqNo++;
                                    }
                                    }

                                fseqNo++;
                                fSubseqNo++;

                            }


                            //}




                        }
                        else if (isClientAdded)
                        {
                            //Save Client
                            tblClient client = new tblClient();
                            
                            client.ClientName = clientObj.ClientName;
                            client.ClientAddress = clientObj.ClientAddress;
                            client.NIC = clientObj.NIC;
                            client.ContactNo = clientObj.ContactNo;
                            client.FixedLine = clientObj.FixedLine;
                            client.Email = clientObj.Email;
                            client.DOB = !string.IsNullOrEmpty(clientObj.DOB) ? DateTime.ParseExact(clientObj.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            client.PPID = clientObj.PPID;
                            client.FamilyDiscount = clientObj.FamilyDiscount;
                            client.AdditionalNote = clientObj.AdditionalNote;
                            client.HomeCountryID = clientObj.HomeCountryID;
                            client.ResidentCountryID = clientObj.ResidentCountryID;
                            client.BUID = clientObj.BusinessUnitID;
                            client.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                            client.CreatedDate = DateTime.Now;
                            client.ExtraInt1 = Year;
                            client.IsActive = clientObj.ClientStatus;
                            client.FrequncyID = clientObj.frequncyID;
                            client.FrequncyDID = clientObj.FrequncyDID;
                            unitOfWork.TblClientRepository.Insert(client);
                            unitOfWork.Save();
                            foreach (var requestLine in clientRequestHeaderVM.FamilyDetails)
                            {
                                tblFamilyMember tblFamilyMember = new tblFamilyMember();
                                tblFamilyMember.ClientID = clientRequestHeaderVM.ClientID;
                                tblFamilyMember.MemberName = requestLine.MemberName;
                                tblFamilyMember.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null; ;
                                tblFamilyMember.NICNo = requestLine.NIC;
                                tblFamilyMember.ContactNo = requestLine.ContactNo;
                                tblFamilyMember.IsActive = requestLine.IsActive;
                                tblFamilyMember.FrequncyID = clientRequestHeaderVM.FrequncyID.ToString();
                                tblFamilyMember.FrequncyDID = clientRequestHeaderVM.FrequncyDID.ToString();
                                tblFamilyMember.ExtraText1 = Year.ToString();
                              


                                unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyMember);
                                unitOfWork.Save();

                                foreach (var grpMember in requestLine.GroupMemberDetails)
                                {
                                    tblGrpFamilyDetail tblGrpFamily = new tblGrpFamilyDetail();
                                    tblGrpFamily.ClientID = clientID;
                                    tblGrpFamily.MemberName = grpMember.MemberName;
                                    tblGrpFamily.MemberDOB = !string.IsNullOrEmpty(grpMember.MemberDOB) ? DateTime.ParseExact(grpMember.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                    tblGrpFamily.MemberNIC = grpMember.NIC;
                                    tblGrpFamily.MemberContact = grpMember.ContactNo;
                                    tblGrpFamily.FamilyMemberID = tblFamilyMember.FamilyMemberID;
                                    tblGrpFamily.ExtraText2 = Year.ToString();
                                    tblGrpFamily.FrequncyID = clientRequestHeaderVM.FrequncyID.ToString();
                                    tblGrpFamily.FrequncyDID = clientRequestHeaderVM.FrequncyDID.ToString();
                                    //clientRequestLine.CreatedDate = DateTime.Now;
                                    unitOfWork.TblGrpFamilyDetailRepository.Insert(tblGrpFamily);
                                    unitOfWork.Save();
                                }
                            }

                            clientID = client.ClientID;
                        }
                        else
                        {
                            clientID = clientObj.ClientID;
                            clientRequestHeaderVM.transactionType = 2;
                        }

                        //Update Client Request Header
                        tblClientRequestHeader clientRequestHeader = unitOfWork.TblClientRequestHeaderRepository.GetByID(clientRequestHeaderVM.ClientRequestHeaderID);
                        clientRequestHeader.ClientID = clientRequestHeaderVM.ClientID;
                        clientRequestHeader.AgentID = clientObj.AgentID;
                        clientRequestHeader.PartnerID = clientRequestHeaderVM.PartnerID;
                        clientRequestHeader.RequestedDate = !string.IsNullOrEmpty(clientRequestHeaderVM.RequestedDate) ? DateTime.ParseExact(clientRequestHeaderVM.RequestedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        clientRequestHeader.ModifiedBy = clientRequestHeaderVM.AgentID;
                        clientRequestHeader.ModifiedDate = DateTime.Now;
                        clientRequestHeader.TransactionID = clientRequestHeaderVM.transactionType;
                        clientRequestHeader.FrequncyID = clientRequestHeaderVM==null?0: clientRequestHeaderVM.FrequncyID;
                        clientRequestHeader.FrequncyDID = clientRequestHeaderVM == null ? "0" : clientRequestHeaderVM.FrequncyDID;



                        clientRequestHeader.CurrancyID= clientRequestHeaderVM == null ? 0 : clientRequestHeaderVM.CurrancyID;


                        //var PolicyStart = "";
                        //var PolicyEnd = "";
                        //if (clientRequestHeaderVM.PolicyStartDate != null & clientRequestHeaderVM.PolicyStartDate.Length > 10)
                        //{
                        //    PolicyStart = clientRequestHeaderVM.PolicyStartDate.Substring(clientRequestHeaderVM.PolicyStartDate.Length - 10).ToString();
                        //    clientRequestHeader.PolicyStart = !string.IsNullOrEmpty(PolicyStart) ? DateTime.ParseExact(PolicyStart, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                        //}
                        //else



                           clientRequestHeader.PolicyStart = !string.IsNullOrEmpty(clientRequestHeaderVM.PolicyStartDate) ? DateTime.ParseExact(clientRequestHeaderVM.PolicyStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;


                        //if (clientRequestHeaderVM.PolicyEndDate != null & clientRequestHeaderVM.PolicyEndDate.Length > 10)
                        //{
                        //    PolicyEnd = clientRequestHeaderVM.PolicyEndDate.Substring(clientRequestHeaderVM.PolicyEndDate.Length - 10).ToString();
                        //    clientRequestHeader.PolicyEnd = !string.IsNullOrEmpty(PolicyEnd) ? DateTime.ParseExact(PolicyEnd, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                        //}
                        //else



                            clientRequestHeader.PolicyEnd = !string.IsNullOrEmpty(clientRequestHeaderVM.PolicyEndDate) ? DateTime.ParseExact(clientRequestHeaderVM.PolicyEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;






                        unitOfWork.TblClientRequestHeaderRepository.Update(clientRequestHeader);
                        unitOfWork.Save();
                        if (clientObj.DeductionDetails != null)
                        {
                            if (clientObj.DeductionDetails.Count > 0)
                            {
                                tblClient client = unitOfWork.TblClientRepository.GetByID(clientObj.ClientID);
                                var deductionData = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == clientObj.ClientID & x.ExtraText1== client.ExtraInt1.ToString() & x.FrequncyID== client.FrequncyID.ToString()  &  x.FrequncyDID== client.FrequncyDID).ToList();
                                foreach (var deduction in deductionData)
                                {
                                    unitOfWork.TblDeductionRepository.Delete(deduction);
                                    unitOfWork.Save();
                                }
                                foreach (var paymentLine in clientObj.DeductionDetails)
                                {
                                    tblDeduction deduction = new tblDeduction();
                                    deduction.ClientID = clientID;
                                    deduction.PremiumHolder = paymentLine.PremiumHolder;
                                    deduction.FamilyMemberID = paymentLine.FamilyMemberID;
                                    deduction.GroupFamilyMemberID = paymentLine.GroupFamilyMemberID;
                                    deduction.LodingRate = paymentLine.LoadingRate;
                                    deduction.DeductionRate = paymentLine.DeductionRate;
                                    deduction.Premium = paymentLine.PremiumAmount;
                                    deduction.NetPremium = paymentLine.NetPremium;
                                    deduction.Deductibles = paymentLine.Deductible;
                                    deduction.LodingRate = paymentLine.LoadingRate;
                                    deduction.ExtraText1 = Year.ToString();
                                    deduction.MI = paymentLine.MI;
                                    deduction.FrequncyID = clientRequestHeaderVM.FrequncyID.ToString();
                                    deduction.FrequncyDID= clientRequestHeaderVM.FrequncyDID.ToString();
                                    deduction.SNo = paymentLine.SNo;
                                    deduction.SeqSubNo = paymentLine.SeqSubNo;
                                    unitOfWork.TblDeductionRepository.Insert(deduction);
                                    unitOfWork.Save();
                                }
                            }
                        }
                        if (clientObj.PolicyInfoID == 0)
                        {
                            tblPolicyInformationBUPA bupa = new tblPolicyInformationBUPA();
                            bupa.ClientID = clientID;
                            bupa.MemberID = clientObj.MemberID;
                            bupa.PolicyMethod = clientObj.PolicyMethod;
                            bupa.Premium = clientObj.Premium;
                            unitOfWork.TblPolicyInformationBUPARepository.Insert(bupa);
                            unitOfWork.Save();
                        }

                        //Update Deduction
                        else
                        {
                            tblPolicyInformationBUPA bupa = unitOfWork.TblPolicyInformationBUPARepository.GetByID(clientObj.PolicyInfoID);
                            bupa.ClientID = clientID;
                            bupa.MemberID = clientObj.MemberID;
                            bupa.PolicyMethod = clientObj.PolicyMethod;
                            bupa.Premium = clientObj.Premium;
                            unitOfWork.TblPolicyInformationBUPARepository.Update(bupa);
                            unitOfWork.Save();
                        }


                    //    Save Payment
                        if (clientRequestHeaderVM.PaymentID > 0)
                        {
                            tblPayment payments = unitOfWork.TblPaymentRepository.GetByID(clientRequestHeaderVM.PaymentID);
                            payments.ClientID = clientRequestHeaderVM.ClientID;
                            payments.PaymentAmount = clientRequestHeaderVM.PaymentAmount;
                            payments.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                            payments.CreatedDate = DateTime.Now;
                            unitOfWork.TblPaymentRepository.Update(payments);
                            unitOfWork.Save();
                        }
                        else
                        {
                            tblPayment payment = new tblPayment();
                            payment.ClientID = clientRequestHeaderVM.ClientID;
                            payment.PaymentAmount = clientRequestHeaderVM.PaymentAmount;
                            payment.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                            payment.CreatedDate = DateTime.Now;
                            unitOfWork.TblPaymentRepository.Insert(payment);
                            unitOfWork.Save();
                           }


                            //Complete the Transaction
                            dbTransaction.Commit();

                        errorMessage = "No Error";
                        return true;
                    }
                    else
                    {
                        errorMessage = "Quotations are created based on this request. Therefore it cannot be modified";
                        return false;
                    }
                }
                catch (Exception ex)
                {

                    ErrHandler.WriteError(ex.Message);
                    //Roll back the Transaction
                    dbTransaction.Rollback();

                    errorMessage = "Update Failed";
                    return false;
                }
            }
        }
        public bool UpdateBUPARenewelRequest(ClientRequestHeaderVM clientRequestHeaderVM, bool isClientUpdated, bool isClientAdded, ClientVM clientObj, out string errorMessage)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    int clientID = 0;
                    clientRequestHeaderVM.transactionType = 1;
                    var year = clientRequestHeaderVM.Year;
                   // var tblclientheader = unitOfWork.TblClientRequestHeaderRepository.Get(x => x.ClientID == clientObj.ClientID).ToList();
                  //  var FrequncyID = string.IsNullOrEmpty(tblclientheader[0].FrequncyID.ToString()) ? "0" : tblclientheader[0].FrequncyID.ToString();
                 //   var FrequncyFID = string.IsNullOrEmpty(tblclientheader[0].FrequncyDID) ? "0" : tblclientheader[0].FrequncyDID;
                    if (clientRequestHeaderVM.IsQuotationCreated != true)
                    {
                        if (isClientUpdated)
                        {
                            clientID = clientObj.ClientID;

                            //Update Client
                            tblClient client = unitOfWork.TblClientRepository.GetByID(clientObj.ClientID);
                            var tblClientRHistory = unitOfWork.TblClientRHistoryRepository.Get(x => x.ClientID == clientObj.ClientID & x.ExtraInt1 == client.ExtraInt1 & x.FrequncyID == client.FrequncyID & x.FrequncyDID == client.FrequncyDID).ToList();


                            //if (client.ExtraInt1 <= int.Parse(year) & tblClientRHistory.Count == 0 & clientRequestHeaderVM.FrequncyID >= client.FrequncyID & int.Parse(clientRequestHeaderVM.FrequncyDID) > int.Parse(client.FrequncyDID))
                            //{
                                if ( tblClientRHistory.Count == 0 )
                                {

                                    var clientHistory = new tblClientRewenelHistory();
                                clientHistory.ClientID = client.ClientID;
                                clientHistory.ClientName = client.ClientName;
                                clientHistory.ClientAddress = client.ClientAddress;
                                clientHistory.NIC = client.NIC;
                                clientHistory.ContactNo = client.ContactNo;
                                clientHistory.FixedLine = client.FixedLine;
                                clientHistory.Email = client.Email;
                                clientHistory.DOB = client.DOB;
                                clientHistory.PPID = client.PPID;
                                clientHistory.FamilyDiscount = client.FamilyDiscount;
                                clientHistory.AdditionalNote = client.AdditionalNote;
                                clientHistory.HomeCountryID = client.HomeCountryID;
                                clientHistory.ResidentCountryID = client.ResidentCountryID;
                                clientHistory.JoinDate = client.JoinDate;
                                clientHistory.PremiumAccept = client.PremiumAccept;
                                clientHistory.TitleID = client.TitleID < 0 ? 0 : client.TitleID;
                                clientHistory.ClientName = string.IsNullOrEmpty(client.ClientName) ? "" : client.ClientName;
                                clientHistory.ClientAddress = client.ClientAddress;
                                clientHistory.ExtraText = client.ExtraText;
                                clientHistory.NIC = client.NIC;
                                clientHistory.ContactNo = client.ContactNo;
                                clientHistory.FixedLine = client.FixedLine;
                                clientHistory.Email = client.Email;
                                clientHistory.DOB = client.DOB;
                                clientHistory.PPID = client.PPID;
                                clientHistory.FamilyDiscount = client.FamilyDiscount;
                                clientHistory.AdditionalNote = client.AdditionalNote;
                                clientHistory.HomeCountryID = client.HomeCountryID;
                                clientHistory.ResidentCountryID = client.ResidentCountryID;
                                clientHistory.BUID = client.BUID;
                                clientHistory.ExtraInt1 = (int)client.ExtraInt1;
                                clientHistory.ModifiedBy = client.ModifiedBy;
                                clientHistory.ModifiedDate = client.ModifiedDate;
                                clientHistory.IsActive = client.IsActive;
                                clientHistory.FrequncyID = client.FrequncyID;
                                clientHistory.FrequncyDID = client.FrequncyDID;
                                unitOfWork.TblClientRHistoryRepository.Insert(clientHistory);
                                unitOfWork.Save();


                                var RequsetData = unitOfWork.TblClientRequestHeaderRepository.Get(x => x.ClientID == clientObj.ClientID & x.FrequncyID == client.FrequncyID & x.FrequncyDID == client.FrequncyDID).ToList();

                                foreach (var rdata in RequsetData)
                                {



                                    var clientRequestHeaderHistory = new tblClientRnlRequestHeader();

                                    clientRequestHeaderHistory.ClientRequestHeaderID = rdata.ClientRequestHeaderID;
                                    clientRequestHeaderHistory.ClientID = rdata.ClientID;
                                    clientRequestHeaderHistory.PartnerID = rdata.PartnerID;
                                    clientRequestHeaderHistory.Exclusions = rdata.Exclusions;
                                    clientRequestHeaderHistory.OptionalCovers = rdata.OptionalCovers;
                                    clientRequestHeaderHistory.Occupation = rdata.Occupation;
                                    clientRequestHeaderHistory.CurrancyID = rdata.CurrancyID;

                                    clientRequestHeaderHistory.FrequncyID = rdata.FrequncyID;
                                    //   clientRequestVM.ClientRequestHeaderDetails.RequestedDate = clientRequestVM.ClientDetails.RequestedDate;
                                    clientRequestHeaderHistory.SchemeID = rdata.SchemeID;
                                    clientRequestHeaderHistory.MembershipID = rdata.MembershipID;
                                    clientRequestHeaderHistory.Exclu = rdata.Exclu;
                                    clientRequestHeaderHistory.GroupID = rdata.GroupID;
                                    clientRequestHeaderHistory.Year = client.ExtraInt1.ToString();
                                    //if (clientRequestVM.ClientRequestHeaderDetails.RequestedDate.Length > 10)
                                    //    clientRequestVM.ClientRequestHeaderDetails.RequestedDate = clientRequestVM.ClientRequestHeaderDetails.RequestedDate.Substring(clientRequestVM.ClientRequestHeaderDetails.RequestedDate.Length - 10).ToString();


                                    clientRequestHeaderHistory.RequestedDate = rdata.RequestedDate;
                                    clientRequestHeaderHistory.CreatedBy = rdata.CreatedBy;
                                    clientRequestHeaderHistory.CreatedDate = rdata.CreatedDate;
                                    clientRequestHeaderHistory.PolicyStart = rdata.PolicyStart;
                                    clientRequestHeaderHistory.PolicyEnd = rdata.PolicyEnd;









                                    // clientRequestHeader.PolicyEnd = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                                    clientRequestHeaderHistory.AgentID = rdata.AgentID;
                                    clientRequestHeaderHistory.FrequncyID = rdata.FrequncyID;
                                    clientRequestHeaderHistory.FrequncyDID = rdata.FrequncyDID;

                                    clientRequestHeaderHistory.ModifiedBy = rdata.ModifiedBy;
                                    unitOfWork.TblClientRenewelRequestHeaderRepository.Insert(clientRequestHeaderHistory);
                                    unitOfWork.Save();




                                }






                            }







                            client.ExtraInt1 = string.IsNullOrEmpty(year) ? 0 : int.Parse(year);
                            client.FrequncyID = clientRequestHeaderVM.FrequncyID;
                            client.FrequncyDID= clientRequestHeaderVM.FrequncyDID;


                            var familyData = unitOfWork.TblFamilyMemberRepository.Get(x => x.ClientID == clientObj.ClientID & x.ExtraText1 == year & x.FrequncyID == clientRequestHeaderVM.FrequncyID.ToString() & x.FrequncyDID == clientRequestHeaderVM.FrequncyDID).ToList();
                            foreach (var family in familyData)
                            {
                                unitOfWork.TblFamilyMemberRepository.Delete(family);
                                unitOfWork.Save();
                            }

                            var familyGrpData = unitOfWork.TblGrpFamilyDetailRepository.Get(x => x.ClientID == clientObj.ClientID & x.ExtraText2 == year & x.FrequncyID == clientRequestHeaderVM.FrequncyID.ToString() & x.FrequncyDID == clientRequestHeaderVM.FrequncyDID).ToList();
                            foreach (var family in familyGrpData)
                            {
                                unitOfWork.TblGrpFamilyDetailRepository.Delete(family);
                                unitOfWork.Save();
                            }

                            var fseqNo = 1;
                            var fSubseqNo = 1;
                            var fMseqNo = 1;
                            if (clientRequestHeaderVM.FamilyDetails != null)
                            { 
                                foreach (var requestLine in clientRequestHeaderVM.FamilyDetails)
                                {
                                    //tblFamilyMember tblFamilyMember = new tblFamilyMember();
                                    //tblFamilyMember.ClientID = clientRequestHeaderVM.ClientID;
                                    //tblFamilyMember.MemberName = requestLine.MemberName;
                                    //tblFamilyMember.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null; ;
                                    //tblFamilyMember.NICNo = requestLine.NIC;
                                    //tblFamilyMember.ContactNo = requestLine.ContactNo;
                                    //tblFamilyMember.IsActive = requestLine.IsActive;
                                    //clientRequestLine.CreatedDate = DateTime.Now;


                                    tblFamilyMember tblFamilyDetail = new tblFamilyMember();
                                    //tblFamilyDetail.ClientID = clientID;
                                    //tblFamilyDetail.MemberName = requestLine.MemberName;
                                    //tblFamilyDetail.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                    //tblFamilyDetail.NICNo = requestLine.NIC;
                                    //tblFamilyDetail.ContactNo = requestLine.ContactNo;
                                    //tblFamilyDetail.JoinDate = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                                    // tblFamilyDetail.FamilyMemberID = requestLine.FamilyMemberID;
                                    //clientRequestLine.CreatedDate = DateTime.Now;

                                    tblFamilyDetail.ClientID = clientID;
                                    tblFamilyDetail.Title = requestLine.TitleID < 0 ? 0 : requestLine.TitleID;
                                    tblFamilyDetail.MemberName = string.IsNullOrEmpty(requestLine.MemberName) ? "" : requestLine.MemberName;
                                    tblFamilyDetail.MemberOtherName = string.IsNullOrEmpty(requestLine.MemberOtherName) ? "" : requestLine.MemberOtherName;

                                    //if (requestLine.MemberDOB.Length > 10)
                                    //    requestLine.MemberDOB = requestLine.MemberDOB.Substring(requestLine.MemberDOB.Length - 10).ToString();





                                    tblFamilyDetail.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                                    //if (requestLine.InceptionDate.Length > 10)
                                    //    requestLine.InceptionDate = requestLine.InceptionDate.Substring(requestLine.InceptionDate.Length - 10).ToString();


                                    tblFamilyDetail.JoinDate = !string.IsNullOrEmpty(requestLine.JoinDate) ? DateTime.ParseExact(requestLine.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                    tblFamilyDetail.NICNo = !string.IsNullOrEmpty(requestLine.NIC) ? requestLine.NIC : "";
                                    tblFamilyDetail.ContactNo = !string.IsNullOrEmpty(requestLine.ContactNo) ? requestLine.ContactNo : "";
                                    tblFamilyDetail.HomeCountry = requestLine.HomeCountryID < 0 ? 0 : requestLine.HomeCountryID;
                                    tblFamilyDetail.CountryOfResident = requestLine.
    ResidentCountryID < 0 ? 0 : requestLine.
    ResidentCountryID;
                                    tblFamilyDetail.premiumID = !string.IsNullOrEmpty(requestLine.SchemeID.ToString()) ? requestLine.SchemeID.ToString() : "";
                                    tblFamilyDetail.RelationShipID = requestLine.RelationShipID < 0 ? 0 : requestLine.RelationShipID;
                                    tblFamilyDetail.GenderID = requestLine.GenderID < 0 ? 0 : requestLine.GenderID;
                                    //  tblFamilyDetail.ExtraText2= !string.IsNullOrEmpty(requestLine.Exclusion) ? requestLine.Exclusion : "";
                                    tblFamilyDetail.MembershipID = !string.IsNullOrEmpty(requestLine.MembershipID) ? requestLine.MembershipID : "";
                                    tblFamilyDetail.Exclusions = 0;
                                    tblFamilyDetail.OptionalCover = requestLine.OptionalCover;
                                    tblFamilyDetail.IsActive = requestLine.IsActive;
                                    tblFamilyDetail.Exclu = requestLine.Exclu;
                                    tblFamilyDetail.ExtraText1 = year;
                                    tblFamilyDetail.FrequncyID = client.FrequncyID.ToString();
                                    tblFamilyDetail.FrequncyDID = client.FrequncyDID;


                                    tblFamilyDetail.FrequncyDID = clientRequestHeaderVM.FrequncyDID.ToString();
                                    if (clientObj.PremiumAccept == "2")
                                    {
                                        tblFamilyDetail.SeqNo = fseqNo.ToString();
                                        tblFamilyDetail.SeqSubNo = "";
                                    }
                                    else
                                    {
                                        tblFamilyDetail.SeqNo = "1";
                                        tblFamilyDetail.SeqSubNo = fSubseqNo.ToString();
                                    }
                                    unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyDetail);
                                    unitOfWork.Save();

                                    if (requestLine.GroupMemberDetails != null)
                                    {
                                        fMseqNo = 1;
                                        foreach (var grpMember in requestLine.GroupMemberDetails)
                                        {
                                            //tblGrpFamilyDetail tblGrpFamily = new tblGrpFamilyDetail();
                                            //tblGrpFamily.ClientID = clientID;
                                            //tblGrpFamily.MemberName = grpMember.MemberName;
                                            //tblGrpFamily.MemberDOB = !string.IsNullOrEmpty(grpMember.MemberDOB) ? DateTime.ParseExact(grpMember.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                            //tblGrpFamily.MemberNIC = grpMember.NIC;
                                            //tblGrpFamily.MemberContact = grpMember.ContactNo;
                                            //tblGrpFamily.FamilyMemberID = tblFamilyDetail.FamilyMemberID;


                                            tblGrpFamilyDetail tblGrpFamily = new tblGrpFamilyDetail();
                                            tblGrpFamily.ClientID = clientID;
                                            tblGrpFamily.TitleID = grpMember.TitleID < 0 ? 0 : grpMember.TitleID;
                                            tblGrpFamily.MemberName = grpMember.MemberName;
                                            tblGrpFamily.MembershipID = grpMember.MembershipID;
                                            tblGrpFamily.ExtraText1 = string.IsNullOrEmpty(grpMember.MemberOtherName) ? "" : grpMember.MemberOtherName;
                                            //     tblGrpFamily.MemberOtherName = string.IsNullOrEmpty(grpMember.MemberOtherName) ? "" : grpMember.MemberOtherName;

                                            //if (grpMember.MemberDOB.Length > 10)
                                            //    grpMember.MemberDOB = grpMember.MemberDOB.Substring(grpMember.MemberDOB.Length - 10).ToString();

                                            tblGrpFamily.MemberDOB = !string.IsNullOrEmpty(grpMember.MemberDOB) ? DateTime.ParseExact(grpMember.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                                            //if (grpMember.JoinDate.Length > 10)
                                            //   grpMember.JoinDate = grpMember.JoinDate.Substring(grpMember.JoinDate.Length - 10).ToString();
                                            tblGrpFamily.RelationShipID = grpMember.RelationShipID < 0 ? 0 : grpMember.RelationShipID;

                                            tblGrpFamily.JoinDate = !string.IsNullOrEmpty(grpMember.JoinDate) ? DateTime.ParseExact(grpMember.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                            tblGrpFamily.MemberNIC = grpMember.NIC;
                                            tblGrpFamily.MemberContact = grpMember.ContactNo;
                                            tblGrpFamily.FamilyMemberID = tblFamilyDetail.FamilyMemberID;
                                            //tblGrpFamily.MemberCountryID = grpMember.countryID < 0 ? 0 : (int)grpMember.MemberCountryID;
                                            tblGrpFamily.MemberCountryID = grpMember.MemberCountryID < 0 ? 0 : grpMember.MemberCountryID;

                                            tblGrpFamily.MemberResCountryID = grpMember.ResidentCountryID < 0 ? 0 : grpMember.ResidentCountryID;
                                            tblGrpFamily.Exclusions = 0;
                                            tblGrpFamily.Exclu = grpMember.Exclu;
                                            tblGrpFamily.OptionalCovers = requestLine.OptionalCover;
                                            tblGrpFamily.IsActive = grpMember.IsActive < 0 ? 0 : grpMember.IsActive;
                                            tblGrpFamily.ExtraText2 = year;
                                            tblGrpFamily.FrequncyID = client.FrequncyID.ToString();
                                            tblGrpFamily.FrequncyDID = client.FrequncyDID;


                                            tblGrpFamily.SeqNo = fseqNo.ToString();
                                            tblGrpFamily.SeqSubNo = fMseqNo.ToString();







                                            //clientRequestLine.CreatedDate = DateTime.Now;
                                            unitOfWork.TblGrpFamilyDetailRepository.Insert(tblGrpFamily);
                                            unitOfWork.Save();
                                            fMseqNo++;
                                        }
                                    }
                                    fseqNo++;
                                    fSubseqNo++;
                                }
                        }


                            //var polcyhistory = unitOfWork.TblPolicyRenewalHistoryRepository.Get(x => x.PolicyInfoRecID == clientObj.ClientID ).ToList();
                            //var plicyHistorytable = new tblPolicyRenewalHistory();

                            //plicyHistorytable.PolicyInfoRecID = clientID;
                            //plicyHistorytable.RenewalDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.RequestedDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.RequestedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            //plicyHistorytable.NotificationDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.RequestedDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.RequestedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            //plicyHistorytable.IsSent = false;
                            //plicyHistorytable.IsCancel = false;
                            //plicyHistorytable.Agent = string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.AgentID.ToString()) ? 0 : clientRequestVM.ClientRequestHeaderDetails.AgentID;
                            //plicyHistorytable.IsRenewal = true;
                            //plicyHistorytable.RenewalStartDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                            //plicyHistorytable.RenewalEndDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;







                        }
                        else if (isClientAdded)
                        {
                            //Save Client
                            tblClient client = new tblClient();
                            client.ClientName = clientObj.ClientName;
                            client.ClientAddress = clientObj.ClientAddress;
                            client.NIC = clientObj.NIC;
                            client.ContactNo = clientObj.ContactNo;
                            client.FixedLine = clientObj.FixedLine;
                            client.Email = clientObj.Email;
                            client.DOB = !string.IsNullOrEmpty(clientObj.DOB) ? DateTime.ParseExact(clientObj.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            client.PPID = clientObj.PPID;
                            client.FamilyDiscount = clientObj.FamilyDiscount;
                            client.AdditionalNote = clientObj.AdditionalNote;
                            client.HomeCountryID = clientObj.HomeCountryID;
                            client.ResidentCountryID = clientObj.ResidentCountryID;
                            client.BUID = clientObj.BusinessUnitID;
                            client.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                            client.CreatedDate = DateTime.Now;
                            unitOfWork.TblClientRepository.Insert(client);
                            unitOfWork.Save();
                            foreach (var requestLine in clientRequestHeaderVM.FamilyDetails)
                            {
                                tblFamilyMember tblFamilyMember = new tblFamilyMember();
                                tblFamilyMember.ClientID = clientRequestHeaderVM.ClientID;
                                tblFamilyMember.MemberName = requestLine.MemberName;
                                tblFamilyMember.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null; ;
                                tblFamilyMember.NICNo = requestLine.NIC;
                                tblFamilyMember.ContactNo = requestLine.ContactNo;
                                tblFamilyMember.IsActive = requestLine.IsActive;
                                tblFamilyMember.ExtraText1 = requestLine.Year;

                                //tblFamilyMember.FrequncyID = FrequncyID;
                               // tblFamilyMember.FrequncyDID = FrequncyFID;
                                unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyMember);
                                unitOfWork.Save();

                                foreach (var grpMember in requestLine.GroupMemberDetails)
                                {
                                    tblGrpFamilyDetail tblGrpFamily = new tblGrpFamilyDetail();
                                    tblGrpFamily.ClientID = clientID;
                                    tblGrpFamily.MemberName = grpMember.MemberName;
                                    tblGrpFamily.MemberDOB = !string.IsNullOrEmpty(grpMember.MemberDOB) ? DateTime.ParseExact(grpMember.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                    tblGrpFamily.MemberNIC = grpMember.NIC;
                                    tblGrpFamily.MemberContact = grpMember.ContactNo;
                                    tblGrpFamily.FamilyMemberID = tblFamilyMember.FamilyMemberID;
                                    tblGrpFamily.ExtraText2 = grpMember.Year;
                                   // tblGrpFamily.FrequncyID = FrequncyID;
                                 //   tblGrpFamily.FrequncyDID = FrequncyFID;
                                    //clientRequestLine.CreatedDate = DateTime.Now;
                                    unitOfWork.TblGrpFamilyDetailRepository.Insert(tblGrpFamily);
                                    unitOfWork.Save();
                                }
                            }

                            clientID = client.ClientID;
                        }
                        else
                        {
                            clientID = clientObj.ClientID;
                            clientRequestHeaderVM.transactionType = 2;
                        }

                        //Update Client Request Header
                        tblClientRequestHeader clientRequestHeader = unitOfWork.TblClientRequestHeaderRepository.GetByID(clientRequestHeaderVM.ClientRequestHeaderID);
                        clientRequestHeader.ClientID = clientRequestHeaderVM.ClientID;
                        clientRequestHeader.PartnerID = clientRequestHeaderVM.PartnerID;
                        clientRequestHeader.RequestedDate = !string.IsNullOrEmpty(clientRequestHeaderVM.RequestedDate) ? DateTime.ParseExact(clientRequestHeaderVM.RequestedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        clientRequestHeader.ModifiedBy = clientRequestHeaderVM.AgentID;
                        clientRequestHeader.ModifiedDate = DateTime.Now;
                        clientRequestHeader.TransactionID = clientRequestHeaderVM.transactionType;
                        clientRequestHeader.FrequncyID = clientRequestHeaderVM == null ? 0 : clientRequestHeaderVM.FrequncyID;
                        clientRequestHeader.FrequncyDID = clientRequestHeaderVM == null ? "0" : clientRequestHeaderVM.FrequncyDID;



                        clientRequestHeader.CurrancyID = clientRequestHeaderVM == null ? 0 : clientRequestHeaderVM.CurrancyID;


                        //var PolicyStart = "";
                        //var PolicyEnd = "";
                        //if (clientRequestHeaderVM.PolicyStartDate != null & clientRequestHeaderVM.PolicyStartDate.Length > 10)
                        //{
                        //    PolicyStart = clientRequestHeaderVM.PolicyStartDate.Substring(clientRequestHeaderVM.PolicyStartDate.Length - 10).ToString();
                        //    clientRequestHeader.PolicyStart = !string.IsNullOrEmpty(PolicyStart) ? DateTime.ParseExact(PolicyStart, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                        //}
                        //else



                        clientRequestHeader.PolicyStart = !string.IsNullOrEmpty(clientRequestHeaderVM.PolicyStartDate) ? DateTime.ParseExact(clientRequestHeaderVM.PolicyStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;


                        //if (clientRequestHeaderVM.PolicyEndDate != null & clientRequestHeaderVM.PolicyEndDate.Length > 10)
                        //{
                        //    PolicyEnd = clientRequestHeaderVM.PolicyEndDate.Substring(clientRequestHeaderVM.PolicyEndDate.Length - 10).ToString();
                        //    clientRequestHeader.PolicyEnd = !string.IsNullOrEmpty(PolicyEnd) ? DateTime.ParseExact(PolicyEnd, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                        //}
                        //else



                        clientRequestHeader.PolicyEnd = !string.IsNullOrEmpty(clientRequestHeaderVM.PolicyEndDate) ? DateTime.ParseExact(clientRequestHeaderVM.PolicyEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;






                        unitOfWork.TblClientRequestHeaderRepository.Update(clientRequestHeader);
                        unitOfWork.Save();
                        if (clientObj.DeductionDetails != null)
                        {
                            if (clientObj.DeductionDetails.Count > 0)
                            {

                                var deductionData = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == clientObj.ClientID &&  x.ExtraText1==year & x.FrequncyID== clientRequestHeaderVM.FrequncyID.ToString() & x.FrequncyDID== clientRequestHeaderVM.FrequncyDID).ToList();
                                foreach (var deduction in deductionData)
                                {
                                    unitOfWork.TblDeductionRepository.Delete(deduction);
                                    unitOfWork.Save();
                                }
                                foreach (var paymentLine in clientObj.DeductionDetails)
                                {
                                    tblDeduction deduction = new tblDeduction();
                                    deduction.ClientID = clientID;
                                    deduction.PremiumHolder = paymentLine.PremiumHolder;
                                    deduction.FamilyMemberID = paymentLine.FamilyMemberID;
                                    deduction.GroupFamilyMemberID = paymentLine.GroupFamilyMemberID;
                                    deduction.LodingRate = paymentLine.LoadingRate;
                                    deduction.DeductionRate = paymentLine.DeductionRate;
                                    deduction.Premium = paymentLine.PremiumAmount;
                                    deduction.NetPremium = paymentLine.NetPremium;
                                    deduction.Deductibles = paymentLine.Deductible;
                                    deduction.LodingRate = paymentLine.LoadingRate;
                                    deduction.ExtraText1 = year;
                                    deduction.MI = paymentLine.MI;
                                    deduction.FrequncyID = clientRequestHeaderVM.FrequncyID.ToString();
                                    deduction.FrequncyDID = clientRequestHeaderVM.FrequncyDID;
                                    deduction.SNo = paymentLine.SNo;
                                    deduction.SeqSubNo = paymentLine.SeqSubNo;
                                    unitOfWork.TblDeductionRepository.Insert(deduction);
                                    unitOfWork.Save();
                                }
                            }
                        }
                        if (clientObj.PolicyInfoID == 0)
                        {
                            tblPolicyInformationBUPA bupa = new tblPolicyInformationBUPA();
                            bupa.ClientID = clientID;
                            bupa.MemberID = clientObj.MemberID;
                            bupa.PolicyMethod = clientObj.PolicyMethod;
                            bupa.Premium = clientObj.Premium;
                            unitOfWork.TblPolicyInformationBUPARepository.Insert(bupa);
                            unitOfWork.Save();
                        }

                        //Update Deduction
                        else
                        {
                            tblPolicyInformationBUPA bupa = unitOfWork.TblPolicyInformationBUPARepository.GetByID(clientObj.PolicyInfoID);
                            bupa.ClientID = clientID;
                            bupa.MemberID = clientObj.MemberID;
                            bupa.PolicyMethod = clientObj.PolicyMethod;
                            bupa.Premium = clientObj.Premium;
                            unitOfWork.TblPolicyInformationBUPARepository.Update(bupa);
                            unitOfWork.Save();
                        }


                       /// Save Payment
                        if (clientRequestHeaderVM.PaymentID > 0)
                        {
                            tblPayment payments = unitOfWork.TblPaymentRepository.GetByID(clientRequestHeaderVM.PaymentID);
                            payments.ClientID = clientRequestHeaderVM.ClientID;
                            payments.PaymentAmount = clientRequestHeaderVM.PaymentAmount;
                            payments.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                            payments.CreatedDate = DateTime.Now;
                            unitOfWork.TblPaymentRepository.Update(payments);
                            unitOfWork.Save();
                        }
                        else
                        {
                            tblPayment payment = new tblPayment();
                            payment.ClientID = clientRequestHeaderVM.ClientID;
                            payment.PaymentAmount = clientRequestHeaderVM.PaymentAmount;
                            payment.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                            payment.CreatedDate = DateTime.Now;
                            unitOfWork.TblPaymentRepository.Insert(payment);
                            unitOfWork.Save();
                        }


                        //Complete the Transaction
                        dbTransaction.Commit();

                        errorMessage = "No Error";
                        return true;
                    }
                    else
                    {
                        errorMessage = "Quotations are created based on this request. Therefore it cannot be modified";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();

                    errorMessage = "Update Failed";
                    return false;
                }
            }
        }
        public ClientVM GetClientByClientID(int clientID)
        {
            try
            {
                var clientData = unitOfWork.TblClientRepository.GetByID(clientID);

                ClientVM clientVM = new ClientVM();
                clientVM.TitleID = (int)clientData.TitleID <0 ? 0: (int)clientData.TitleID;
                clientVM.ClientID = clientData.ClientID < 0 ? 0 : clientData.ClientID;
                clientVM.ClientName = string.IsNullOrEmpty(clientData.ClientName) ? "" : clientData.ClientName;
                clientVM.ClientOtherName = string.IsNullOrEmpty(clientData.ExtraText) ? "": clientData.ExtraText;
                clientVM.ClientAddress = string.IsNullOrEmpty(clientData.ClientAddress) ? "" : clientData.ClientAddress;
                clientVM.NIC = string.IsNullOrEmpty(clientData.NIC) ? "" : clientData.NIC;
                clientVM.ContactNo = string.IsNullOrEmpty(clientData.ContactNo) ? "" : clientData.ContactNo;
                clientVM.FixedLine = string.IsNullOrEmpty(clientData.FixedLine) ? "" : clientData.FixedLine;
                clientVM.Email = string.IsNullOrEmpty(clientData.Email) ? "" : clientData.Email;
                clientVM.DOB = clientData.DOB != null ? Convert.ToDateTime(clientData.DOB).ToString("dd/MM/yyyy") : string.Empty;
                
                clientVM.PPID = string.IsNullOrEmpty(clientData.PPID) ? "" : clientData.PPID;
                clientVM.FamilyDiscount = clientData.FamilyDiscount >0 ? Convert.ToDecimal(clientData.FamilyDiscount) : 0;
                clientVM.AdditionalNote = string.IsNullOrEmpty(clientData.AdditionalNote) ? "" : clientData.AdditionalNote;
                clientVM.HomeCountryID = clientData.HomeCountryID >0 ? Convert.ToInt32(clientData.HomeCountryID) : 0;
                clientVM.ResidentCountryID = clientData.ResidentCountryID >0 ? Convert.ToInt32(clientData.ResidentCountryID) : 0;
                clientVM.PremiumAccept = clientData.PremiumAccept;


                var FamilyDetailsData = unitOfWork.TblFamilyMemberRepository.Get(x => x.ClientID == clientID).ToList();
                List<FamilyMembersVM> clienttVM = new List<FamilyMembersVM>();
                foreach (var family in FamilyDetailsData)
                {
                    FamilyMembersVM familyDetailsVM = new FamilyMembersVM();
                    familyDetailsVM.FamilyMemberID = family.FamilyMemberID;
                    familyDetailsVM.TitleID = (int)family.Title < 0 ? 0 : (int)family.Title;
                    familyDetailsVM.MemberName = string.IsNullOrEmpty(family.MemberName) ?"": family.MemberName;
                    familyDetailsVM.MemberOtherName = string.IsNullOrEmpty(family.MemberOtherName) ? "" : family.MemberOtherName;
                    familyDetailsVM.MemberDOB = family.MemberDOB != null ? Convert.ToDateTime(family.MemberDOB).ToString("dd/MM/yyyy") : string.Empty;
                    familyDetailsVM.JoinDate= family.JoinDate != null ? Convert.ToDateTime(family.JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                    familyDetailsVM.NIC = string.IsNullOrEmpty(family.NICNo) ?"": family.NICNo;
                    familyDetailsVM.ContactNo = string.IsNullOrEmpty(family.ContactNo) ? "" : family.ContactNo;
                    familyDetailsVM.RelationShipID = family.RelationShipID < 0 ? 0 : (int)family.RelationShipID;
                    familyDetailsVM.GenderID = family.GenderID < 0 ? 0 : (int)family.GenderID;


                  
                    clienttVM.Add(familyDetailsVM);
                }
                clientVM.FamilyDetails = clienttVM;

                var PaymentData = unitOfWork.TblPaymentRepository.Get(x => x.ClientID == clientID).ToList();
                var DeductionData = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == clientID).ToList();
                List<PaymentVM> paymenttVM = new List<PaymentVM>();
                foreach (var payment in PaymentData)
                {
                    PaymentVM paymentVM = new PaymentVM();
                    paymentVM.PaymentID = payment.PaymentID;
                    paymentVM.PaymentAmount = payment.PaymentAmount;

                    paymenttVM.Add(paymentVM);
                }
                clientVM.PaymentDetails = paymenttVM;
                List<DeductionDetailsVM> deductionDetailsVM = new List<DeductionDetailsVM>();
                foreach (var deduction in DeductionData)
                {
                    DeductionDetailsVM deductionVM = new DeductionDetailsVM();
                    deductionVM.DeductionID = deduction.DeductionID;
                    deductionVM.DeductionRate = deduction.DeductionRate;
                    deductionVM.LoadingRate = deduction.LodingRate;
                    deductionVM.PremiumHolder = deduction.PremiumHolder;
                    deductionVM.PremiumAmount = deduction.Premium;
                    deductionVM.NetPremium = deduction.NetPremium;
                    deductionVM.ClientID = deduction.ClientID;
                    deductionVM.PremiumHolderType = deduction.PremiumHolderType <0 ?0: deduction.PremiumHolderType;
                    deductionVM.JoinDate = deduction.JoinDate != null ? Convert.ToDateTime(deduction.JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                    deductionVM.Deductible = string.IsNullOrEmpty(deduction.Deductibles) ? "" : deduction.Deductibles;
                    deductionVM.GroupFamilyMemberID = deduction.GroupFamilyMemberID < 0 ? 0 : deduction.GroupFamilyMemberID;
                    deductionVM.PremiumID = deduction.PremiumID < 0 ? 0 : deduction.PremiumID;
                    
                    //deductionVM.ex = "";
                    //deductionVM.BusinessUnit = clientObj.BusinessUnitID;
                    //deductionVM.DeductionRemarks = "";
                    //deductionVM.OptionalAmount = "0";
                    //deductionVM.LoadingAmount = 0;
                    //deductionVM.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                    //deduction.CreatedDate = DateTime.Now;

                    deductionDetailsVM.Add(deductionVM);
                }

                clientVM.DeductionDetails = deductionDetailsVM;

                var BankTransData = unitOfWork.TblBankTransactionDetailRepository.Get(x => x.ClientID == clientID).ToList();
                List<BankTransactionVM> bankTransactiontVM = new List<BankTransactionVM>();
                foreach (var bank in BankTransData)
                {
                    BankTransactionVM bankTransactionVM = new BankTransactionVM();
                    bankTransactionVM.BankID = bank.BankID;
                    bankTransactionVM.BankRate = bank.PaymentID;
                    bankTransactionVM.DraftNo = bank.DraftNo;
                    bankTransactionVM.BankAmount = bank.Amount;
                    bankTransactionVM.SGSAmount = bank.IBSAmount;
                    bankTransactionVM.BankDetailID = bank.BankDetailID;


                    bankTransactiontVM.Add(bankTransactionVM);
                }
                clientVM.BankTransactionDetails = bankTransactiontVM;

                var PolicyBUPAData = unitOfWork.TblPolicyInformationBUPARepository.Get(x => x.ClientID == clientID).ToList();
                List<PolicyInfoBUPAVM> policyInfoBUPAtVM = new List<PolicyInfoBUPAVM>();
                foreach (var policy in PolicyBUPAData)
                {
                    PolicyInfoBUPAVM policyInfoBUPAVM = new PolicyInfoBUPAVM();
                    policyInfoBUPAVM.PolicyInfoID = policy.PolicyInfoID;
                    policyInfoBUPAVM.Premium = policy.Premium;
                    policyInfoBUPAVM.MemberID = policy.MemberID;
                    policyInfoBUPAVM.PolicyMethod = policy.PolicyMethod;
                    policyInfoBUPAVM.ClientID = policy.ClientID;



                    policyInfoBUPAtVM.Add(policyInfoBUPAVM);
                }
                clientVM.PolicyInfoBUPADetails = policyInfoBUPAtVM;

                if (clientVM.HomeCountryID > 0)
                {
                    clientVM.HomeCountryName = clientData.tblCountry.CountryName;
                }

                clientVM.ResidentCountryID = clientData.ResidentCountryID != null ? Convert.ToInt32(clientData.ResidentCountryID) : 0;

                if (clientVM.ResidentCountryID > 0)
                {
                    clientVM.ResidentCountryName = clientData.tblCountry1.CountryName;
                }

                clientVM.BusinessUnitID = clientData.BUID != null ? Convert.ToInt32(clientData.BUID) : 0;

                if (clientVM.BusinessUnitID > 0)
                {
                    clientVM.BusinessUnitName = clientData.tblBussinessUnit.BussinessUnit;
                }
                
                clientVM.CreatedBy = clientData.CreatedBy != null ? Convert.ToInt32(clientData.CreatedBy) : 0;
                clientVM.CreatedDate = clientData.CreatedDate != null ? clientData.CreatedDate.ToString() : string.Empty;
                clientVM.ModifiedBy = clientData.ModifiedBy != null ? Convert.ToInt32(clientData.ModifiedBy) : 0;
                clientVM.ModifiedDate = clientData.ModifiedDate != null ? clientData.ModifiedDate.ToString() : string.Empty;
                

                return clientVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public ClientVM GetPilotClientByClientID(int clientID)
        {
            try
            {
                var clientData = unitOfWork.TblClientRepository.GetByID(clientID);

                ClientVM clientVM = new ClientVM();
                clientVM.TitleID = (int)clientData.TitleID < 0 ? 0 : (int)clientData.TitleID;
                clientVM.ClientID = clientData.ClientID < 0 ? 0 : clientData.ClientID;
                clientVM.ClientName = string.IsNullOrEmpty(clientData.ClientName) ? "" : clientData.ClientName;
                clientVM.ClientOtherName = string.IsNullOrEmpty(clientData.ExtraText) ? "" : clientData.ExtraText;
                clientVM.ClientAddress = string.IsNullOrEmpty(clientData.ClientAddress) ? "" : clientData.ClientAddress;
                clientVM.NIC = string.IsNullOrEmpty(clientData.NIC) ? "" : clientData.NIC;
                clientVM.ContactNo = string.IsNullOrEmpty(clientData.ContactNo) ? "" : clientData.ContactNo;
                clientVM.FixedLine = string.IsNullOrEmpty(clientData.FixedLine) ? "" : clientData.FixedLine;
                clientVM.Email = string.IsNullOrEmpty(clientData.Email) ? "" : clientData.Email;
                clientVM.DOB = clientData.DOB != null ? Convert.ToDateTime(clientData.DOB).ToString("dd/MM/yyyy") : string.Empty;

                clientVM.PPID = string.IsNullOrEmpty(clientData.PPID) ? "" : clientData.PPID;
                clientVM.FamilyDiscount = clientData.FamilyDiscount > 0 ? Convert.ToDecimal(clientData.FamilyDiscount) : 0;
                clientVM.AdditionalNote = string.IsNullOrEmpty(clientData.AdditionalNote) ? "" : clientData.AdditionalNote;
                clientVM.HomeCountryID = clientData.HomeCountryID > 0 ? Convert.ToInt32(clientData.HomeCountryID) : 0;
                clientVM.ResidentCountryID = clientData.ResidentCountryID > 0 ? Convert.ToInt32(clientData.ResidentCountryID) : 0;
                clientVM.PremiumAccept = clientData.PremiumAccept;


                var FamilyDetailsData = unitOfWork.TblFamilyMemberRepository.Get(x => x.ClientID == clientID).ToList();
                List<FamilyMembersVM> clienttVM = new List<FamilyMembersVM>();
                foreach (var family in FamilyDetailsData)
                {
                    FamilyMembersVM familyDetailsVM = new FamilyMembersVM();
                    familyDetailsVM.FamilyMemberID = family.FamilyMemberID;
                    familyDetailsVM.TitleID = (int)family.Title < 0 ? 0 : (int)family.Title;
                    familyDetailsVM.MemberName = string.IsNullOrEmpty(family.MemberName) ? "" : family.MemberName;
                    familyDetailsVM.MemberOtherName = string.IsNullOrEmpty(family.MemberOtherName) ? "" : family.MemberOtherName;
                    familyDetailsVM.MemberDOB = family.MemberDOB != null ? Convert.ToDateTime(family.MemberDOB).ToString("dd/MM/yyyy") : string.Empty;
                    familyDetailsVM.JoinDate = family.JoinDate != null ? Convert.ToDateTime(family.JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                    familyDetailsVM.NIC = string.IsNullOrEmpty(family.NICNo) ? "" : family.NICNo;
                    familyDetailsVM.ContactNo = string.IsNullOrEmpty(family.ContactNo) ? "" : family.ContactNo;
                    familyDetailsVM.RelationShipID = family.RelationShipID < 0 ? 0 : (int)family.RelationShipID;
                    familyDetailsVM.GenderID = family.GenderID < 0 ? 0 : (int)family.GenderID;
                    familyDetailsVM.MemberCountryID= family.HomeCountry == null ? 0 : (int)family.HomeCountry;
                    familyDetailsVM.MemberResCountryID = family.CountryOfResident == null ? 0 : (int)family.CountryOfResident;

                    clienttVM.Add(familyDetailsVM);
                }
                clientVM.FamilyDetails = clienttVM;

                var PaymentData = unitOfWork.TblPaymentRepository.Get(x => x.ClientID == clientID).ToList();
                var DeductionData = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == clientID).ToList();
                List<PaymentVM> paymenttVM = new List<PaymentVM>();
                foreach (var payment in PaymentData)
                {
                    PaymentVM paymentVM = new PaymentVM();
                    paymentVM.PaymentID = payment.PaymentID;
                    paymentVM.PaymentAmount = payment.PaymentAmount;

                    paymenttVM.Add(paymentVM);
                }
                clientVM.PaymentDetails = paymenttVM;
                List<DeductionDetailsVM> deductionDetailsVM = new List<DeductionDetailsVM>();
                foreach (var deduction in DeductionData)
                {
                    DeductionDetailsVM deductionVM = new DeductionDetailsVM();
                    deductionVM.DeductionID = deduction.DeductionID;
                    deductionVM.DeductionRate = deduction.DeductionRate;
                    deductionVM.LoadingRate = deduction.LodingRate;
                    deductionVM.PremiumHolder = deduction.PremiumHolder;
                    deductionVM.PremiumAmount = deduction.Premium;
                    deductionVM.NetPremium = deduction.NetPremium;
                    deductionVM.ClientID = deduction.ClientID;
                    deductionVM.PremiumHolderType = deduction.PremiumHolderType < 0 ? 0 : deduction.PremiumHolderType;
                    deductionVM.JoinDate = deduction.JoinDate != null ? Convert.ToDateTime(deduction.JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                    deductionVM.Deductible = string.IsNullOrEmpty(deduction.Deductibles) ? "" : deduction.Deductibles;
                    deductionVM.GroupFamilyMemberID = deduction.GroupFamilyMemberID < 0 ? 0 : deduction.GroupFamilyMemberID;
                    deductionVM.PremiumID = deduction.PremiumID < 0 ? 0 : deduction.PremiumID;

                    //deductionVM.ex = "";
                    //deductionVM.BusinessUnit = clientObj.BusinessUnitID;
                    //deductionVM.DeductionRemarks = "";
                    //deductionVM.OptionalAmount = "0";
                    //deductionVM.LoadingAmount = 0;
                    //deductionVM.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                    //deduction.CreatedDate = DateTime.Now;

                    deductionDetailsVM.Add(deductionVM);
                }

                clientVM.DeductionDetails = deductionDetailsVM;

                var BankTransData = unitOfWork.TblBankTransactionDetailRepository.Get(x => x.ClientID == clientID).ToList();
                List<BankTransactionVM> bankTransactiontVM = new List<BankTransactionVM>();
                foreach (var bank in BankTransData)
                {
                    BankTransactionVM bankTransactionVM = new BankTransactionVM();
                    bankTransactionVM.BankID = bank.BankID;
                    bankTransactionVM.BankRate = bank.PaymentID;
                    bankTransactionVM.DraftNo = bank.DraftNo;
                    bankTransactionVM.BankAmount = bank.Amount;
                    bankTransactionVM.SGSAmount = bank.IBSAmount;
                    bankTransactionVM.BankDetailID = bank.BankDetailID;


                    bankTransactiontVM.Add(bankTransactionVM);
                }
                clientVM.BankTransactionDetails = bankTransactiontVM;

                var PolicyBUPAData = unitOfWork.TblPolicyInformationBUPARepository.Get(x => x.ClientID == clientID).ToList();
                List<PolicyInfoBUPAVM> policyInfoBUPAtVM = new List<PolicyInfoBUPAVM>();
                foreach (var policy in PolicyBUPAData)
                {
                    PolicyInfoBUPAVM policyInfoBUPAVM = new PolicyInfoBUPAVM();
                    policyInfoBUPAVM.PolicyInfoID = policy.PolicyInfoID;
                    policyInfoBUPAVM.Premium = policy.Premium;
                    policyInfoBUPAVM.MemberID = policy.MemberID;
                    policyInfoBUPAVM.PolicyMethod = policy.PolicyMethod;
                    policyInfoBUPAVM.ClientID = policy.ClientID;



                    policyInfoBUPAtVM.Add(policyInfoBUPAVM);
                }
                clientVM.PolicyInfoBUPADetails = policyInfoBUPAtVM;

                if (clientVM.HomeCountryID > 0)
                {
                    clientVM.HomeCountryName = clientData.tblCountry.CountryName;
                }

                clientVM.ResidentCountryID = clientData.ResidentCountryID != null ? Convert.ToInt32(clientData.ResidentCountryID) : 0;

                if (clientVM.ResidentCountryID > 0)
                {
                    clientVM.ResidentCountryName = clientData.tblCountry1.CountryName;
                }

                clientVM.BusinessUnitID = clientData.BUID != null ? Convert.ToInt32(clientData.BUID) : 0;

                if (clientVM.BusinessUnitID > 0)
                {
                    clientVM.BusinessUnitName = clientData.tblBussinessUnit.BussinessUnit;
                }

                clientVM.CreatedBy = clientData.CreatedBy != null ? Convert.ToInt32(clientData.CreatedBy) : 0;
                clientVM.CreatedDate = clientData.CreatedDate != null ? clientData.CreatedDate.ToString() : string.Empty;
                clientVM.ModifiedBy = clientData.ModifiedBy != null ? Convert.ToInt32(clientData.ModifiedBy) : 0;
                clientVM.ModifiedDate = clientData.ModifiedDate != null ? clientData.ModifiedDate.ToString() : string.Empty;


                return clientVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }






        public ClientVM GetBUPAClientByClientID(int clientID)
        {
            try
            {
                var clientData = unitOfWork.TblClientRepository.GetByID(clientID);
                var year = clientData.ExtraInt1;
                var fID = clientData.FrequncyID;
                var ffID = clientData.FrequncyDID;


                ClientVM clientVM = new ClientVM();
                
                clientVM.CustomerType = clientData.CustomerType != null ? Convert.ToInt32(clientData.HomeCountryID) : 0;

                if (clientData.TitleID == null)
                    clientVM.TitleID = 0;
                else if ((int)clientData.TitleID < 0)
                    clientVM.TitleID = 0;
                else
                    clientVM.TitleID = (int)clientData.TitleID;

                   // clientVM.TitleID = (int)clientData.TitleID < 0  || string.IsNullOrEmpty(clientData.TitleID.ToString()) || clientData.TitleID.ToString ().Length<=0 ? 0 : (int)clientData.TitleID;
                clientVM.ClientID = clientData.ClientID < 0 ? 0 : clientData.ClientID;
                clientVM.ClientName = string.IsNullOrEmpty(clientData.ClientName) ? "" : clientData.ClientName;
                clientVM.ClientOtherName = string.IsNullOrEmpty(clientData.ExtraText) ? "" : clientData.ExtraText;
                clientVM.ClientAddress = string.IsNullOrEmpty(clientData.ClientAddress) ? "" : clientData.ClientAddress;
                clientVM.NIC = string.IsNullOrEmpty(clientData.NIC) ? "" : clientData.NIC;
                clientVM.ContactNo = string.IsNullOrEmpty(clientData.ContactNo) ? "" : clientData.ContactNo;
                clientVM.FixedLine = string.IsNullOrEmpty(clientData.FixedLine) ? "" : clientData.FixedLine;
                clientVM.Email = string.IsNullOrEmpty(clientData.Email) ? "" : clientData.Email;
                clientVM.DOB = clientData.DOB != null ? Convert.ToDateTime(clientData.DOB).ToString("dd/MM/yyyy") : string.Empty;
                clientVM.PPID = string.IsNullOrEmpty(clientData.PPID) ? "" : clientData.PPID;
                clientVM.FamilyDiscount = clientData.FamilyDiscount > 0 ? Convert.ToDecimal(clientData.FamilyDiscount) : 0;
                clientVM.AdditionalNote = string.IsNullOrEmpty(clientData.AdditionalNote) ? "" : clientData.AdditionalNote;
                clientVM.HomeCountryID = clientData.HomeCountryID > 0 ? Convert.ToInt32(clientData.HomeCountryID) : 0;
                clientVM.ResidentCountryID = clientData.ResidentCountryID > 0 ? Convert.ToInt32(clientData.ResidentCountryID) : 0;
                clientVM.PremiumAccept = clientData.PremiumAccept;
                clientVM.JoinDate = clientVM != null & clientData.JoinDate != null ? Convert.ToDateTime(clientData.JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                clientVM.ClientStatus= string.IsNullOrEmpty(clientData.IsActive.ToString()) ? 0 : clientData.IsActive;
                clientVM.frequncyID =(int) clientData.FrequncyID;
                clientVM.FrequncyDID = clientData.FrequncyDID;
                clientVM.SeqNo = clientData.SeqNo;
                var FamilyDetailsData = unitOfWork.TblFamilyMemberRepository.Get(x => x.ClientID == clientID &  x.ExtraText1== year.ToString() & x.FrequncyID==fID.ToString() & x.FrequncyDID==ffID).ToList();

                var FamilyDetailsDedudtion = unitOfWork.TblFamilyMemberRepository.Get(x => x.ClientID == clientID & x.IsActive == 1 & x.ExtraText1 == year.ToString() & x.FrequncyID == fID.ToString() & x.FrequncyDID == ffID).ToList();
                List<FamilyMembersVM> clienttVM = new List<FamilyMembersVM>();




                decimal paysum = 0;


                List<DeductionDetailsVM> deductionDetailsVM = new List<DeductionDetailsVM>();


                var ClientDeductionData = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == clientID & x.PremiumHolder == clientData.ExtraText & x.ExtraText1== year.ToString() & x.FrequncyID == fID.ToString() & x.FrequncyDID == ffID);
                foreach (var deduction in ClientDeductionData)
                {
                    DeductionDetailsVM deductionVM = new DeductionDetailsVM();
                    deductionVM.DeductionID = deduction.DeductionID;
                    deductionVM.DeductionRate = deduction.DeductionRate;
                    deductionVM.LoadingRate = deduction.LodingRate;
                    deductionVM.PremiumHolder = deduction.PremiumHolder;
                    deductionVM.PremiumAmount = deduction.Premium;
                    deductionVM.NetPremium = deduction.NetPremium;
                    deductionVM.ClientID = deduction.ClientID;
                    deductionVM.Deductible = string.IsNullOrEmpty(deduction.Deductibles) ? "" : deduction.Deductibles;
                    deductionVM.MI = deduction.MI;

                    deductionVM.SNo = deduction.SNo;
                    deductionVM.SeqSubNo = deduction.SeqSubNo;
                    deductionVM.SeqNo = deduction.SNo;
                    deductionDetailsVM.Add(deductionVM);

                    paysum = (decimal)paysum + (decimal)deduction.NetPremium;
                }








                // List<BUPAPremiumVM> BUPAPremiumVM = new List<BUPAPremiumVM>();


                if (FamilyDetailsData.Count > 0)
                {

                    foreach (var family in FamilyDetailsData)
                    {
                        FamilyMembersVM familyDetailsVM = new FamilyMembersVM();
                        familyDetailsVM.FamilyMemberID = family.FamilyMemberID;




                        familyDetailsVM.TitleID = family.Title == null ? 0 : (int)family.Title;
                        familyDetailsVM.MemberName = string.IsNullOrEmpty(family.MemberName) ? "" : family.MemberName;
                        familyDetailsVM.MemberOtherName = string.IsNullOrEmpty(family.MemberOtherName) ? "" : family.MemberOtherName;
                        familyDetailsVM.MemberDOB = family.MemberDOB != null ? Convert.ToDateTime(family.MemberDOB).ToString("dd/MM/yyyy") : string.Empty;
                        familyDetailsVM.JoinDate = family.JoinDate != null ? Convert.ToDateTime(family.JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                        familyDetailsVM.NIC = string.IsNullOrEmpty(family.NICNo) ? "" : family.NICNo;
                        familyDetailsVM.ContactNo = string.IsNullOrEmpty(family.ContactNo) ? "" : family.ContactNo;
                        familyDetailsVM.RelationShipID = family.RelationShipID == null ? 0 : (int)family.RelationShipID;
                     
                        familyDetailsVM.HomeCountryID= family.HomeCountry == null ? 0 : (int)family.HomeCountry;
                        familyDetailsVM.ResidentCountryID = family.CountryOfResident == null ? 0 : (int)family.CountryOfResident;


                        familyDetailsVM.GenderID = family.GenderID == null ? 0 : (int)family.GenderID;
                        familyDetailsVM.SchemeID = family.premiumID == null ? 0 :int.Parse( family.premiumID.ToString());
                        familyDetailsVM.IsActive = family.IsActive == null ? 0 : (int)family.IsActive;
                        familyDetailsVM.MembershipID = family.MembershipID == null ? "NA" : family.MembershipID;
                        familyDetailsVM.OptionalCover = family.OptionalCover == null ? "NA" : family.OptionalCover;
                        familyDetailsVM.Exclu = family.Exclu == null ? "NA" : family.Exclu;
                        familyDetailsVM.SeqNo = family.SeqNo + "." + family.SeqSubNo;
                        familyDetailsVM.SNo = family.SeqNo;
                        familyDetailsVM.SeqSubNo= family.SeqSubNo;
                        clienttVM.Add(familyDetailsVM);

                        // var ObjBUPAPremiumVM = new BUPAPremiumVM();
                        // ObjBUPAPremiumVM.Primium= string.IsNullOrEmpty(family.MemberName) ? "" : family.MemberName;
                        // BUPAPremiumVM.Add(ObjBUPAPremiumVM);

                        var GrpFamilyData = unitOfWork.TblGrpFamilyDetailRepository.Get(x => x.FamilyMemberID == family.FamilyMemberID & x.ExtraText2 == year.ToString() & x.FrequncyID==fID.ToString() & x.FrequncyDID==ffID).ToList();
                        List<GroupFamilyMembersVM> GroupVM = new List<GroupFamilyMembersVM>();

                        //if (GrpFamilyData.Count > 0)
                        //{

                        foreach (var group in GrpFamilyData)
                        {
                            GroupFamilyMembersVM groupFamilyVM = new GroupFamilyMembersVM();
                            groupFamilyVM.TitleID = group.TitleID == null || group.TitleID < 0 ? 0 : (int)group.TitleID;
                            groupFamilyVM.FamilyMemberID = group.MemberID < 0 ? 0 : group.MemberID;

                            groupFamilyVM.MemberName = group.MemberName;
                            groupFamilyVM.Exclu = group.Exclu == null ? "NA" : group.Exclu;
                            groupFamilyVM.MemberOtherName = group.ExtraText1 == null ? "NA" : group.ExtraText1;
                            groupFamilyVM.OptionalCover = group.OptionalCovers== null ? "NA" : group.OptionalCovers;
                            groupFamilyVM.MemberDOB = group.MemberDOB != null ? Convert.ToDateTime(group.MemberDOB).ToString("dd/MM/yyyy") : string.Empty;
                            groupFamilyVM.JoinDate = group.JoinDate != null ? Convert.ToDateTime(group.JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                            groupFamilyVM.NIC = group.MemberNIC;
                            groupFamilyVM.ContactNo = group.MemberContact;
                            groupFamilyVM.MemberCountryID = group.MemberCountryID == null ? 0 :(int) group.MemberCountryID;
                            groupFamilyVM.ResidentCountryID = group.MemberResCountryID == null ? 0 : (int)group.MemberResCountryID;
                            groupFamilyVM.IsActive = group.IsActive == null ? 0 : (int)group.IsActive;
                            groupFamilyVM.MembershipID = group.MembershipID == null ? "NA" : group.MembershipID;
                            groupFamilyVM.RelationShipID= group.RelationShipID == null ? 0 : (int)group.RelationShipID;
                            //  groupFamilyVM=

                            groupFamilyVM.SeqNo = group.SeqNo + "." + group.SeqSubNo;
                            groupFamilyVM.SNo = group.SeqNo;
                            groupFamilyVM.SeqSubNo = group.SeqSubNo;
                            //groupFamilyVM.RelationShipID = group.RelationShipID < 0 ? 0 : group.RelationShipID;
                            //groupFamilyVM.GenderID = group.GenderID < 0 ? 0 : group.GenderID;
                            //groupFamilyVM.MemberOtherName = string.IsNullOrEmpty(group.MemberOtherName) ? "" : group.MemberOtherName;

                            GroupVM.Add(groupFamilyVM);

                            //  var ObjBUPAPremiumVMGrp = new BUPAPremiumVM();
                            //  ObjBUPAPremiumVMGrp.Primium = group.MemberName;
                            //  BUPAPremiumVM.Add(ObjBUPAPremiumVMGrp);



                        }
                        familyDetailsVM.GroupMemberDetails = GroupVM;
                        //var DeductionData = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == clientID & x.PremiumHolder == family.MemberName).ToList();
                        //foreach (var deduction in DeductionData)
                        //{
                        //    DeductionDetailsVM deductionVM = new DeductionDetailsVM();
                        //    deductionVM.DeductionID = deduction.DeductionID;
                        //    deductionVM.DeductionRate = deduction.DeductionRate;
                        //    deductionVM.LoadingRate = deduction.LodingRate;
                        //    deductionVM.PremiumHolder = deduction.PremiumHolder;
                        //    deductionVM.PremiumAmount = deduction.Premium;
                        //    deductionVM.NetPremium = deduction.NetPremium;
                        //    deductionVM.ClientID = deduction.ClientID;
                        //    deductionVM.Deductible = string.IsNullOrEmpty(deduction.Deductibles) ? "" : deduction.Deductibles;

                        //    deductionDetailsVM.Add(deductionVM);

                        //    paysum = (decimal)paysum + (decimal)deduction.Premium;

                        //}











                        //}


                    }


                    clientVM.FamilyDetails = clienttVM;
                }







                if (FamilyDetailsDedudtion.Count > 0)
                {

                    foreach (var family in FamilyDetailsDedudtion)
                    {
                        

                           
                        var DeductionData = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == clientID & x.MI== family.MembershipID & x.ExtraText1== year.ToString() & x.FrequncyID == fID.ToString() & x.FrequncyDID == ffID).ToList();
                        foreach (var deduction in DeductionData)
                        {
                            DeductionDetailsVM deductionVM = new DeductionDetailsVM();
                            deductionVM.DeductionID = deduction.DeductionID;
                            deductionVM.DeductionRate = deduction.DeductionRate;
                            deductionVM.LoadingRate = deduction.LodingRate;
                            deductionVM.PremiumHolder = deduction.PremiumHolder;
                            deductionVM.PremiumAmount = deduction.Premium;
                            deductionVM.NetPremium = deduction.NetPremium;
                            deductionVM.ClientID = deduction.ClientID;
                            deductionVM.Deductible = string.IsNullOrEmpty(deduction.Deductibles) ? "" : deduction.Deductibles;
                            deductionVM.MI = deduction.MI;
                            deductionVM.SNo = deduction.SNo;
                            deductionVM.SeqSubNo = deduction.SeqSubNo;
                            deductionVM.SeqNo = deduction.SNo + "."+ deduction.SeqSubNo; 

                            deductionDetailsVM.Add(deductionVM);

                            paysum = (decimal)paysum +(decimal) deduction.NetPremium;

                        }

                        var GrpFamilly= unitOfWork.TblGrpFamilyDetailRepository.Get(x => x.ClientID == clientID & x.FamilyMemberID == family.FamilyMemberID & x.IsActive==1 & x.ExtraText2 == year.ToString() & x.FrequncyID == fID.ToString() & x.FrequncyDID == ffID).ToList();


                        foreach (var grpFamillynew in GrpFamilly)
                        {
                            var DeductionDataGrp = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == clientID & x.MI== grpFamillynew.MembershipID & x.ExtraText1 == year.ToString() & x.FrequncyID == fID.ToString() & x.FrequncyDID == ffID).ToList();
                            foreach (var deduction in DeductionDataGrp)
                            {
                                DeductionDetailsVM deductionVM = new DeductionDetailsVM();
                                deductionVM.DeductionID = deduction.DeductionID;
                                deductionVM.DeductionRate = deduction.DeductionRate;
                                deductionVM.LoadingRate = deduction.LodingRate;
                                deductionVM.PremiumHolder = deduction.PremiumHolder;
                                deductionVM.PremiumAmount =Math.Round((decimal) deduction.Premium,2);
                                deductionVM.NetPremium =Math.Round((decimal)deduction.NetPremium,2);
                                deductionVM.ClientID = deduction.ClientID;
                                deductionVM.Deductible = string.IsNullOrEmpty(deduction.Deductibles) ? "" : deduction.Deductibles;
                                deductionVM.MI = deduction.MI;
                                deductionVM.SNo = deduction.SNo;
                                deductionVM.SeqSubNo = deduction.SeqSubNo;
                                deductionVM.SeqNo = deduction.SNo + "." + deduction.SeqSubNo;
                                deductionDetailsVM.Add(deductionVM);

                                paysum = (decimal)paysum + (decimal)deduction.NetPremium;

                            }

                        }






                        //}


                    }
                  

                   // clientVM.FamilyDetails = clienttVM;
                }

                var PaymentData = unitOfWork.TblPaymentRepository.Get(x => x.ClientID == clientID).ToList();
             
                List<PaymentVM> paymenttVM = new List<PaymentVM>();
                foreach (var payment in PaymentData)
                {
                    PaymentVM paymentVM = new PaymentVM();
                    paymentVM.PaymentID = payment.PaymentID;
                    paymentVM.PaymentAmount = payment.PaymentAmount;

                    paymenttVM.Add(paymentVM);
                    clientVM.PaymentDetails = paymenttVM;
                }






                if (clientVM.PaymentDetails != null)

                    clientVM.PaymentDetails[0].PaymentAmount = Math.Round(paysum, 2);

                else
                {
                 

                    PaymentVM paymentVM = new PaymentVM();
                    paymenttVM.Add(paymentVM);
                    clientVM.PaymentDetails = paymenttVM;
                    clientVM.PaymentDetails[0].PaymentAmount = Math.Round(paysum, 2);
                }

                //List<DeductionDetailsVM> deductionDetailsVM = new List<DeductionDetailsVM>();




                clientVM.DeductionDetails = deductionDetailsVM;

                //if (clientData.PremiumAccept == "1")
                //{
                //    var ObjBUPAPremiumVMClient = new BUPAPremiumVM();
                //    ObjBUPAPremiumVMClient.Primium= string.IsNullOrEmpty(clientData.ClientName) ? "" : clientData.ClientName;


                //    BUPAPremiumVM.Add(ObjBUPAPremiumVMClient);
                //    clientVM.BUPAPremiumVM = BUPAPremiumVM;

                //}

                var BankTransData = unitOfWork.TblBankTransactionDetailRepository.Get(x => x.ClientID == clientID & x.Year == year & x.FrequncyID == fID.ToString() & x.FrequncyDID == ffID).ToList();
                List<BankTransactionVM> bankTransactiontVM = new List<BankTransactionVM>();

                decimal paiedAmount=0;
                decimal TotalAmount = 0;
                foreach (var bank in BankTransData)
                {
                    BankTransactionVM bankTransactionVM = new BankTransactionVM();
                    bankTransactionVM.BankID = bank.BankID;
                    bankTransactionVM.PaymentMethodID = bank.PaymentID;
                    bankTransactionVM.DraftNo = bank.DraftNo;
                    bankTransactionVM.SGSAmount = bank.Amount;
                    bankTransactionVM.IBSAmount = bank.IBSAmount;
                    bankTransactionVM.BankDetailID = bank.BankDetailID;
                    bankTransactionVM.PaymentDate = bank.PaymentDate.ToString();

                    bankTransactiontVM.Add(bankTransactionVM);

                    paiedAmount = paiedAmount + (decimal) bank.Amount;
                }
              

                var ClientSum = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == clientID ).ToList();
                var Csum = (from x in ClientSum select x.NetPremium).Sum();  
                var Banksum = unitOfWork.TblBankTransactionDetailRepository.Get(x => x.ClientID == clientID ).ToList();
                var Bsum = (from x in Banksum select x.Amount).Sum();  

                var  TotalPayment= (decimal)Csum - (decimal)Bsum;
                var currentOU = Math.Round(paysum - paiedAmount, 2);



                clientVM.BankTransactionDetails = bankTransactiontVM;
              //  clientVM.PaiedAmount =Math.Round( paysum-paiedAmount,2);
                clientVM.PaiedAmount = Math.Round(TotalPayment, 2);
                clientVM.COutstanding = Math.Round(paysum - paiedAmount, 2);
                clientVM.Outstanding = (decimal)TotalPayment - (decimal)currentOU;





                var PolicyBUPAData = unitOfWork.TblPolicyInformationBUPARepository.Get(x => x.ClientID == clientID).ToList();
                List<PolicyInfoBUPAVM> policyInfoBUPAtVM = new List<PolicyInfoBUPAVM>();
                foreach (var policy in PolicyBUPAData)
                {
                    PolicyInfoBUPAVM policyInfoBUPAVM = new PolicyInfoBUPAVM();
                    policyInfoBUPAVM.PolicyInfoID = policy.PolicyInfoID;
                    policyInfoBUPAVM.Premium = policy.Premium;
                    policyInfoBUPAVM.MemberID = policy.MemberID;
                    policyInfoBUPAVM.PolicyMethod = policy.PolicyMethod;
                    policyInfoBUPAVM.ClientID = policy.ClientID;



                    policyInfoBUPAtVM.Add(policyInfoBUPAVM);
                }
                clientVM.PolicyInfoBUPADetails = policyInfoBUPAtVM;

                if (clientVM.HomeCountryID > 0)
                {
                    clientVM.HomeCountryName = clientData.tblCountry.CountryName;
                }

                clientVM.ResidentCountryID = clientData.ResidentCountryID != null ? Convert.ToInt32(clientData.ResidentCountryID) : 0;

                if (clientVM.ResidentCountryID > 0)
                {
                    clientVM.ResidentCountryName = clientData.tblCountry1.CountryName;
                }

                clientVM.BusinessUnitID = clientData.BUID != null ? Convert.ToInt32(clientData.BUID) : 0;

                if (clientVM.BusinessUnitID > 0)
                {
                    clientVM.BusinessUnitName = clientData.tblBussinessUnit.BussinessUnit;
                }

               // var clientRequestHeaderData = unitOfWork.TblClientRequestHeaderRepository.GetByID(clientID);

                var clientRequestHeaderData = unitOfWork.TblClientRequestHeaderRepository.Get(x => x.ClientID == clientID & x.FrequncyID == fID & x.FrequncyDID == ffID).ToList();
               // List<FamilyMembersVM> clienttVM = new List<FamilyMembersVM>();





                if (clientRequestHeaderData != null)
                {

                    clientVM.PremiumID = clientRequestHeaderData == null || clientRequestHeaderData[0].PartnerID < 0 || string.IsNullOrEmpty(clientRequestHeaderData[0].PartnerID.ToString()) ? 0 : (int)clientRequestHeaderData[0].PartnerID;
                    clientVM.PolicyStartDate = clientRequestHeaderData == null || clientRequestHeaderData[0].PolicyStart == null ? string.Empty : Convert.ToDateTime(clientRequestHeaderData[0].PolicyStart).ToString("dd/MM/yyyy");
                    clientVM.Exclusions = string.IsNullOrEmpty(clientRequestHeaderData[0].Exclusions.ToString()) || clientRequestHeaderData == null ? 0 : float.Parse(clientRequestHeaderData[0].Exclusions.ToString());
                    clientVM.OptionalCovers = clientRequestHeaderData[0].OptionalCovers;
                    clientVM.Occupation = clientRequestHeaderData[0].Occupation;
                    clientVM.RequestedDate = clientRequestHeaderData == null || clientRequestHeaderData[0].RequestedDate == null ? string.Empty : Convert.ToDateTime(clientRequestHeaderData[0].RequestedDate).ToString("dd/MM/yyyy");
                    clientVM.MembershipID = string.IsNullOrEmpty(clientRequestHeaderData[0].MembershipID.ToString()) || clientRequestHeaderData == null ? "": clientRequestHeaderData[0].MembershipID.ToString();
                    clientVM.PolicyEndDate = clientRequestHeaderData == null || clientRequestHeaderData[0].PolicyEnd == null ? string.Empty : Convert.ToDateTime(clientRequestHeaderData[0].PolicyEnd).ToString("dd/MM/yyyy");
                    clientVM.SchemeID= string.IsNullOrEmpty(clientRequestHeaderData[0].SchemeID.ToString()) || clientRequestHeaderData == null ? 0 : int.Parse(clientRequestHeaderData[0].SchemeID.ToString());
                    clientVM.Exclu = string.IsNullOrEmpty(clientRequestHeaderData[0].Exclu.ToString()) || clientRequestHeaderData == null ? "" : clientRequestHeaderData[0].Exclu.ToString();
                    clientVM.GroupID = string.IsNullOrEmpty(clientRequestHeaderData[0].GroupID.ToString()) || clientRequestHeaderData == null ? "" : clientRequestHeaderData[0].GroupID.ToString();
                    clientVM.FrequncyDID = string.IsNullOrEmpty(clientRequestHeaderData[0].FrequncyDID.ToString()) || clientRequestHeaderData == null ? "" : clientRequestHeaderData[0].FrequncyDID.ToString();
                }
                else
                {
                    clientVM.PremiumID = 0;
                    clientVM.PolicyStartDate = string.Empty ;
                    clientVM.Exclusions = 0;
                    clientVM.OptionalCovers = string.Empty;
                    clientVM.Occupation = string.Empty;
                    clientVM.MembershipID = "";
                    clientVM.SchemeID = 0;
                    clientVM.Exclu = "";
                    clientVM.GroupID = "";
                    // clientVM.PolicyStartDate = clientRequestHeaderData!=null & clientRequestHeaderData.PolicyStart != null ? Convert.ToDateTime(clientRequestHeaderData.PolicyStart).ToString("dd/MM/yyyy") : string.Empty;
                    clientVM.PolicyEndDate = string.Empty;

                }
               // clientVM.PolicyEndDate = clientRequestHeaderData !=null &  clientRequestHeaderData.PolicyEnd != null ? Convert.ToDateTime(clientRequestHeaderData.PolicyEnd).ToString("dd/MM/yyyy") : string.Empty;
                clientVM.CreatedBy = clientData !=null &  clientData.CreatedBy != null ? Convert.ToInt32(clientData.CreatedBy) : 0;
                clientVM.CreatedDate = clientData !=null &  clientData.CreatedDate != null ? clientData.CreatedDate.ToString() : string.Empty;
                clientVM.ModifiedBy = clientData !=null & clientData.ModifiedBy != null ? Convert.ToInt32(clientData.ModifiedBy) : 0;
                clientVM.ModifiedDate = clientData !=null &  clientData.ModifiedDate != null ? clientData.ModifiedDate.ToString() : string.Empty;

                return clientVM;
            }
            catch (Exception ex)
            {

                return null;
               // throw;
            }
        }




        public ClientVM GetBUPAClientByClientID(int clientID,string year)
        {
            try
            {

                var yernew = int.Parse(year);
                var clientData = unitOfWork.TblClientRHistoryRepository.Get(x=>x.ClientID== clientID  && x.ExtraInt1== yernew).ToList();
                
                ClientVM clientVM = new ClientVM();

                clientVM.CustomerType = clientData[0].CustomerType != null ? Convert.ToInt32(clientData[0].HomeCountryID) : 0;

                if (clientData[0].TitleID == null)
                    clientVM.TitleID = 0;
                else if ((int)clientData[0].TitleID < 0)
                    clientVM.TitleID = 0;
                else
                    clientVM.TitleID = (int)clientData[0].TitleID;

                // clientVM.TitleID = (int)clientData.TitleID < 0  || string.IsNullOrEmpty(clientData.TitleID.ToString()) || clientData.TitleID.ToString ().Length<=0 ? 0 : (int)clientData.TitleID;
                clientVM.ClientID = clientData[0].ClientID < 0 ? 0 : (int)clientData[0].ClientID;
                clientVM.ClientName = string.IsNullOrEmpty(clientData[0].ClientName) ? "" : clientData[0].ClientName;
                clientVM.ClientOtherName = string.IsNullOrEmpty(clientData[0].ExtraText) ? "" : clientData[0].ExtraText;
                clientVM.ClientAddress = string.IsNullOrEmpty(clientData[0].ClientAddress) ? "" : clientData[0].ClientAddress;
                clientVM.NIC = string.IsNullOrEmpty(clientData[0].NIC) ? "" : clientData[0].NIC;
                clientVM.ContactNo = string.IsNullOrEmpty(clientData[0].ContactNo) ? "" : clientData[0].ContactNo;
                clientVM.FixedLine = string.IsNullOrEmpty(clientData[0].FixedLine) ? "" : clientData[0].FixedLine;
                clientVM.Email = string.IsNullOrEmpty(clientData[0].Email) ? "" : clientData[0].Email;
                clientVM.DOB = clientData[0].DOB != null ? Convert.ToDateTime(clientData[0].DOB).ToString("dd/MM/yyyy") : string.Empty;
                clientVM.PPID = string.IsNullOrEmpty(clientData[0].PPID) ? "" : clientData[0].PPID;
                clientVM.FamilyDiscount = clientData[0].FamilyDiscount > 0 ? Convert.ToDecimal(clientData[0].FamilyDiscount) : 0;
                clientVM.AdditionalNote = string.IsNullOrEmpty(clientData[0].AdditionalNote) ? "" : clientData[0].AdditionalNote;
                clientVM.HomeCountryID = clientData[0].HomeCountryID > 0 ? Convert.ToInt32(clientData[0].HomeCountryID) : 0;
                clientVM.ResidentCountryID = clientData[0].ResidentCountryID > 0 ? Convert.ToInt32(clientData[0].ResidentCountryID) : 0;
                clientVM.PremiumAccept = clientData[0].PremiumAccept;
                clientVM.JoinDate = clientVM != null & clientData[0].JoinDate != null ? Convert.ToDateTime(clientData[0].JoinDate).ToString("dd/MM/yyyy") : string.Empty;
              //  clientVM.ClientStatus= string.IsNullOrEmpty(clientData[0].) ? "" : clientData[0].AdditionalNote;
                var FamilyDetailsData = unitOfWork.TblFamilyMemberRepository.Get(x => x.ClientID == clientID & x.ExtraText1 == year.ToString()).ToList();

                var FamilyDetailsDedudtion = unitOfWork.TblFamilyMemberRepository.Get(x => x.ClientID == clientID & x.IsActive == 1 & x.ExtraText1 == year.ToString()).ToList();
                List<FamilyMembersVM> clienttVM = new List<FamilyMembersVM>();




                decimal paysum = 0;


                List<DeductionDetailsVM> deductionDetailsVM = new List<DeductionDetailsVM>();

                var extratext = clientData[0].ExtraText;
                var ClientDeductionData = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == clientID & x.PremiumHolder == extratext & x.ExtraText1 == year.ToString());
                foreach (var deduction in ClientDeductionData)
                {
                    DeductionDetailsVM deductionVM = new DeductionDetailsVM();
                    deductionVM.DeductionID = deduction.DeductionID;
                    deductionVM.DeductionRate = deduction.DeductionRate;
                    deductionVM.LoadingRate = deduction.LodingRate;
                    deductionVM.PremiumHolder = deduction.PremiumHolder;
                    deductionVM.PremiumAmount = deduction.Premium;
                    deductionVM.NetPremium = deduction.NetPremium;
                    deductionVM.ClientID = deduction.ClientID;
                    deductionVM.Deductible = string.IsNullOrEmpty(deduction.Deductibles) ? "" : deduction.Deductibles;

                    deductionDetailsVM.Add(deductionVM);

                    paysum = (decimal)paysum + (decimal)deduction.NetPremium;
                }








                // List<BUPAPremiumVM> BUPAPremiumVM = new List<BUPAPremiumVM>();


                if (FamilyDetailsData.Count > 0)
                {

                    foreach (var family in FamilyDetailsData)
                    {
                        FamilyMembersVM familyDetailsVM = new FamilyMembersVM();
                        familyDetailsVM.FamilyMemberID = family.FamilyMemberID;




                        familyDetailsVM.TitleID = family.Title == null ? 0 : (int)family.Title;
                        familyDetailsVM.MemberName = string.IsNullOrEmpty(family.MemberName) ? "" : family.MemberName;
                        familyDetailsVM.MemberOtherName = string.IsNullOrEmpty(family.MemberOtherName) ? "" : family.MemberOtherName;
                        familyDetailsVM.MemberDOB = family.MemberDOB != null ? Convert.ToDateTime(family.MemberDOB).ToString("dd/MM/yyyy") : string.Empty;
                        familyDetailsVM.JoinDate = family.JoinDate != null ? Convert.ToDateTime(family.JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                        familyDetailsVM.NIC = string.IsNullOrEmpty(family.NICNo) ? "" : family.NICNo;
                        familyDetailsVM.ContactNo = string.IsNullOrEmpty(family.ContactNo) ? "" : family.ContactNo;
                        familyDetailsVM.RelationShipID = family.RelationShipID == null ? 0 : (int)family.RelationShipID;

                        familyDetailsVM.HomeCountryID = family.HomeCountry == null ? 0 : (int)family.HomeCountry;
                        familyDetailsVM.ResidentCountryID = family.CountryOfResident == null ? 0 : (int)family.CountryOfResident;


                        familyDetailsVM.GenderID = family.GenderID == null ? 0 : (int)family.GenderID;
                        familyDetailsVM.SchemeID = family.premiumID == null ? 0 : int.Parse(family.premiumID.ToString());
                        familyDetailsVM.IsActive = family.IsActive == null ? 0 : (int)family.IsActive;
                        familyDetailsVM.MembershipID = family.MembershipID == null ? "NA" : family.MembershipID;
                        familyDetailsVM.OptionalCover = family.OptionalCover == null ? "NA" : family.OptionalCover;
                        familyDetailsVM.Exclu = family.Exclu == null ? "NA" : family.Exclu;
                        clienttVM.Add(familyDetailsVM);

                        // var ObjBUPAPremiumVM = new BUPAPremiumVM();
                        // ObjBUPAPremiumVM.Primium= string.IsNullOrEmpty(family.MemberName) ? "" : family.MemberName;
                        // BUPAPremiumVM.Add(ObjBUPAPremiumVM);

                        var GrpFamilyData = unitOfWork.TblGrpFamilyDetailRepository.Get(x => x.FamilyMemberID == family.FamilyMemberID & x.ExtraText2 == year.ToString() & x.ClientID== clientID).ToList();
                        List<GroupFamilyMembersVM> GroupVM = new List<GroupFamilyMembersVM>();

                        //if (GrpFamilyData.Count > 0)
                        //{

                        foreach (var group in GrpFamilyData)
                        {
                            GroupFamilyMembersVM groupFamilyVM = new GroupFamilyMembersVM();
                            groupFamilyVM.TitleID = group.TitleID == null || group.TitleID < 0 ? 0 : (int)group.TitleID;
                            groupFamilyVM.FamilyMemberID = group.MemberID < 0 ? 0 : group.MemberID;

                            groupFamilyVM.MemberName = group.MemberName;
                            groupFamilyVM.Exclu = group.Exclu == null ? "NA" : group.Exclu;
                            groupFamilyVM.MemberOtherName = group.ExtraText1 == null ? "NA" : group.ExtraText1;
                            groupFamilyVM.OptionalCover = group.OptionalCovers == null ? "NA" : group.OptionalCovers;
                            groupFamilyVM.MemberDOB = group.MemberDOB != null ? Convert.ToDateTime(group.MemberDOB).ToString("dd/MM/yyyy") : string.Empty;
                            groupFamilyVM.JoinDate = group.JoinDate != null ? Convert.ToDateTime(group.JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                            groupFamilyVM.NIC = group.MemberNIC;
                            groupFamilyVM.ContactNo = group.MemberContact;
                            groupFamilyVM.MemberCountryID = group.MemberCountryID == null ? 0 : (int)group.MemberCountryID;
                            groupFamilyVM.ResidentCountryID = group.MemberResCountryID == null ? 0 : (int)group.MemberResCountryID;
                            groupFamilyVM.IsActive = group.IsActive == null ? 0 : (int)group.IsActive;
                            groupFamilyVM.MembershipID = group.MembershipID == null ? "NA" : group.MembershipID;
                            groupFamilyVM.RelationShipID = group.RelationShipID == null ? 0 : (int)group.RelationShipID;
                            //  groupFamilyVM=


                            //groupFamilyVM.RelationShipID = group.RelationShipID < 0 ? 0 : group.RelationShipID;
                            //groupFamilyVM.GenderID = group.GenderID < 0 ? 0 : group.GenderID;
                            //groupFamilyVM.MemberOtherName = string.IsNullOrEmpty(group.MemberOtherName) ? "" : group.MemberOtherName;

                            GroupVM.Add(groupFamilyVM);

                            //  var ObjBUPAPremiumVMGrp = new BUPAPremiumVM();
                            //  ObjBUPAPremiumVMGrp.Primium = group.MemberName;
                            //  BUPAPremiumVM.Add(ObjBUPAPremiumVMGrp);



                        }
                        familyDetailsVM.GroupMemberDetails = GroupVM;
                        //var DeductionData = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == clientID & x.PremiumHolder == family.MemberName).ToList();
                        //foreach (var deduction in DeductionData)
                        //{
                        //    DeductionDetailsVM deductionVM = new DeductionDetailsVM();
                        //    deductionVM.DeductionID = deduction.DeductionID;
                        //    deductionVM.DeductionRate = deduction.DeductionRate;
                        //    deductionVM.LoadingRate = deduction.LodingRate;
                        //    deductionVM.PremiumHolder = deduction.PremiumHolder;
                        //    deductionVM.PremiumAmount = deduction.Premium;
                        //    deductionVM.NetPremium = deduction.NetPremium;
                        //    deductionVM.ClientID = deduction.ClientID;
                        //    deductionVM.Deductible = string.IsNullOrEmpty(deduction.Deductibles) ? "" : deduction.Deductibles;

                        //    deductionDetailsVM.Add(deductionVM);

                        //    paysum = (decimal)paysum + (decimal)deduction.Premium;

                        //}











                        //}


                    }


                    clientVM.FamilyDetails = clienttVM;
                }







                if (FamilyDetailsDedudtion.Count > 0)
                {

                    foreach (var family in FamilyDetailsDedudtion)
                    {



                        var DeductionData = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == clientID & x.PremiumHolder == family.MemberName & x.ExtraText1 == year.ToString()).ToList();
                        foreach (var deduction in DeductionData)
                        {
                            DeductionDetailsVM deductionVM = new DeductionDetailsVM();
                            deductionVM.DeductionID = deduction.DeductionID;
                            deductionVM.DeductionRate = deduction.DeductionRate;
                            deductionVM.LoadingRate = deduction.LodingRate;
                            deductionVM.PremiumHolder = deduction.PremiumHolder;
                            deductionVM.PremiumAmount = deduction.Premium;
                            deductionVM.NetPremium = deduction.NetPremium;
                            deductionVM.ClientID = deduction.ClientID;
                            deductionVM.Deductible = string.IsNullOrEmpty(deduction.Deductibles) ? "" : deduction.Deductibles;

                            deductionDetailsVM.Add(deductionVM);

                            paysum = (decimal)paysum + (decimal)deduction.NetPremium;

                        }

                        var GrpFamilly = unitOfWork.TblGrpFamilyDetailRepository.Get(x => x.ClientID == clientID & x.FamilyMemberID == family.FamilyMemberID & x.IsActive == 1 & x.ExtraText2 == year.ToString()).ToList();


                        foreach (var grpFamillynew in GrpFamilly)
                        {
                            var DeductionDataGrp = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == clientID & x.PremiumHolder == grpFamillynew.MemberName & x.ExtraText1 == year.ToString()).ToList();
                            foreach (var deduction in DeductionDataGrp)
                            {
                                DeductionDetailsVM deductionVM = new DeductionDetailsVM();
                                deductionVM.DeductionID = deduction.DeductionID;
                                deductionVM.DeductionRate = deduction.DeductionRate;
                                deductionVM.LoadingRate = deduction.LodingRate;
                                deductionVM.PremiumHolder = deduction.PremiumHolder;
                                deductionVM.PremiumAmount = Math.Round((decimal)deduction.Premium, 2);
                                deductionVM.NetPremium = Math.Round((decimal)deduction.NetPremium, 2);
                                deductionVM.ClientID = deduction.ClientID;
                                deductionVM.Deductible = string.IsNullOrEmpty(deduction.Deductibles) ? "" : deduction.Deductibles;

                                deductionDetailsVM.Add(deductionVM);

                                paysum = (decimal)paysum + (decimal)deduction.NetPremium;

                            }

                        }






                        //}


                    }


                    // clientVM.FamilyDetails = clienttVM;
                }

                var PaymentData = unitOfWork.TblPaymentRepository.Get(x => x.ClientID == clientID).ToList();

                List<PaymentVM> paymenttVM = new List<PaymentVM>();
                foreach (var payment in PaymentData)
                {
                    PaymentVM paymentVM = new PaymentVM();
                    paymentVM.PaymentID = payment.PaymentID;
                    paymentVM.PaymentAmount = payment.PaymentAmount;

                    paymenttVM.Add(paymentVM);
                    clientVM.PaymentDetails = paymenttVM;
                }






                if (clientVM.PaymentDetails != null)
                    clientVM.PaymentDetails[0].PaymentAmount = Math.Round(paysum, 2);
                //List<DeductionDetailsVM> deductionDetailsVM = new List<DeductionDetailsVM>();




                clientVM.DeductionDetails = deductionDetailsVM;

                //if (clientData.PremiumAccept == "1")
                //{
                //    var ObjBUPAPremiumVMClient = new BUPAPremiumVM();
                //    ObjBUPAPremiumVMClient.Primium= string.IsNullOrEmpty(clientData.ClientName) ? "" : clientData.ClientName;


                //    BUPAPremiumVM.Add(ObjBUPAPremiumVMClient);
                //    clientVM.BUPAPremiumVM = BUPAPremiumVM;

                //}

                var BankTransData = unitOfWork.TblBankTransactionDetailRepository.Get(x => x.ClientID == clientID & x.Year == yernew).ToList();
                List<BankTransactionVM> bankTransactiontVM = new List<BankTransactionVM>();

                decimal paiedAmount = 0;
                foreach (var bank in BankTransData)
                {
                    BankTransactionVM bankTransactionVM = new BankTransactionVM();
                    bankTransactionVM.BankID = bank.BankID;
                    bankTransactionVM.PaymentMethodID = bank.PaymentID;
                    bankTransactionVM.DraftNo = bank.DraftNo;
                    bankTransactionVM.SGSAmount = bank.Amount;
                    bankTransactionVM.IBSAmount = bank.IBSAmount;
                    bankTransactionVM.BankDetailID = bank.BankDetailID;
                    bankTransactionVM.PaymentDate = bank.PaymentDate.ToString();

                    bankTransactiontVM.Add(bankTransactionVM);

                    paiedAmount = paiedAmount + (decimal)bank.Amount;
                }
                clientVM.BankTransactionDetails = bankTransactiontVM;
                clientVM.PaiedAmount = Math.Round(paysum - paiedAmount, 2);
                var PolicyBUPAData = unitOfWork.TblPolicyInformationBUPARepository.Get(x => x.ClientID == clientID).ToList();
                List<PolicyInfoBUPAVM> policyInfoBUPAtVM = new List<PolicyInfoBUPAVM>();
                foreach (var policy in PolicyBUPAData)
                {
                    PolicyInfoBUPAVM policyInfoBUPAVM = new PolicyInfoBUPAVM();
                    policyInfoBUPAVM.PolicyInfoID = policy.PolicyInfoID;
                    policyInfoBUPAVM.Premium = policy.Premium;
                    policyInfoBUPAVM.MemberID = policy.MemberID;
                    policyInfoBUPAVM.PolicyMethod = policy.PolicyMethod;
                    policyInfoBUPAVM.ClientID = policy.ClientID;



                    policyInfoBUPAtVM.Add(policyInfoBUPAVM);
                }
                clientVM.PolicyInfoBUPADetails = policyInfoBUPAtVM;

                if (clientVM.HomeCountryID > 0)
                {
                    clientVM.HomeCountryName = "NA";
                }

                clientVM.ResidentCountryID = clientData[0].ResidentCountryID != null ? Convert.ToInt32(clientData[0].ResidentCountryID) : 0;

                if (clientVM.ResidentCountryID > 0)
                {
                    clientVM.ResidentCountryName ="NA";
                }

                clientVM.BusinessUnitID = clientData[0].BUID != null ? Convert.ToInt32(clientData[0].BUID) : 0;
                
                if (clientVM.BusinessUnitID > 0)
                {

                    var Bus = unitOfWork.TblBussinessUnitRepository.GetByID(clientData[0].BUID);

                    clientVM.BusinessUnitName = Bus.BussinessUnit;
                }

                // var clientRequestHeaderData = unitOfWork.TblClientRequestHeaderRepository.GetByID(clientID);

                var clientRequestHeaderData = unitOfWork.TblClientRenewelRequestHeaderRepository.Get(x => x.ClientID == clientID &  x.Year==year).ToList();
                // List<FamilyMembersVM> clienttVM = new List<FamilyMembersVM>();





                if (clientRequestHeaderData != null)
                {

                    clientVM.PremiumID = clientRequestHeaderData == null || clientRequestHeaderData[0].PartnerID < 0 || string.IsNullOrEmpty(clientRequestHeaderData[0].PartnerID.ToString()) ? 0 : (int)clientRequestHeaderData[0].PartnerID;
                    clientVM.PolicyStartDate = clientRequestHeaderData == null || clientRequestHeaderData[0].PolicyStart == null ? string.Empty : Convert.ToDateTime(clientRequestHeaderData[0].PolicyStart).ToString("dd/MM/yyyy");
                    clientVM.Exclusions = string.IsNullOrEmpty(clientRequestHeaderData[0].Exclusions.ToString()) || clientRequestHeaderData == null ? 0 : float.Parse(clientRequestHeaderData[0].Exclusions.ToString());
                    clientVM.OptionalCovers = clientRequestHeaderData[0].OptionalCovers;
                    clientVM.Occupation = clientRequestHeaderData[0].Occupation;
                    clientVM.RequestedDate = clientRequestHeaderData == null || clientRequestHeaderData[0].RequestedDate == null ? string.Empty : Convert.ToDateTime(clientRequestHeaderData[0].RequestedDate).ToString("dd/MM/yyyy");
                    clientVM.MembershipID = string.IsNullOrEmpty(clientRequestHeaderData[0].MembershipID.ToString()) || clientRequestHeaderData == null ? "" : clientRequestHeaderData[0].MembershipID.ToString();
                    clientVM.PolicyEndDate = clientRequestHeaderData == null || clientRequestHeaderData[0].PolicyEnd == null ? string.Empty : Convert.ToDateTime(clientRequestHeaderData[0].PolicyEnd).ToString("dd/MM/yyyy");
                    clientVM.SchemeID = string.IsNullOrEmpty(clientRequestHeaderData[0].SchemeID.ToString()) || clientRequestHeaderData == null ? 0 : int.Parse(clientRequestHeaderData[0].SchemeID.ToString());
                    clientVM.Exclu = string.IsNullOrEmpty(clientRequestHeaderData[0].Exclu.ToString()) || clientRequestHeaderData == null ? "" : clientRequestHeaderData[0].Exclu.ToString();
                    clientVM.GroupID = string.IsNullOrEmpty(clientRequestHeaderData[0].GroupID.ToString()) || clientRequestHeaderData == null ? "" : clientRequestHeaderData[0].GroupID.ToString();
                    clientVM.FrequncyDID= string.IsNullOrEmpty(clientRequestHeaderData[0].FrequncyDID.ToString()) || clientRequestHeaderData == null ? "" : clientRequestHeaderData[0].FrequncyDID.ToString();
                }
                else
                {
                    clientVM.PremiumID = 0;
                    clientVM.PolicyStartDate = string.Empty;
                    clientVM.Exclusions = 0;
                    clientVM.OptionalCovers = string.Empty;
                    clientVM.Occupation = string.Empty;
                    clientVM.MembershipID = "";
                    clientVM.SchemeID = 0;
                    clientVM.Exclu = "";
                    clientVM.GroupID = "";
                    // clientVM.PolicyStartDate = clientRequestHeaderData!=null & clientRequestHeaderData.PolicyStart != null ? Convert.ToDateTime(clientRequestHeaderData.PolicyStart).ToString("dd/MM/yyyy") : string.Empty;
                    clientVM.PolicyEndDate = string.Empty;

                }
                // clientVM.PolicyEndDate = clientRequestHeaderData !=null &  clientRequestHeaderData.PolicyEnd != null ? Convert.ToDateTime(clientRequestHeaderData.PolicyEnd).ToString("dd/MM/yyyy") : string.Empty;
                clientVM.CreatedBy = clientData != null & clientData[0].CreatedBy != null ? Convert.ToInt32(clientData[0].CreatedBy) : 0;
                clientVM.CreatedDate = clientData != null &  clientData[0].CreatedDate != null ? clientData[0].CreatedDate.ToString() : string.Empty;
                clientVM.ModifiedBy = clientData != null & clientData[0].ModifiedBy != null ? Convert.ToInt32(clientData[0].ModifiedBy) : 0;
                clientVM.ModifiedDate = clientData != null & clientData[0].ModifiedDate != null ? clientData[0].ModifiedDate.ToString() : string.Empty;

                return clientVM;
            }
            catch (Exception ex)
            {

                return null;
                // throw;
            }
        }


        public ClientVM GetBUPAClientByClientID(int clientID, string year,string FrequncyID,string FrequncyDID)
        {
            try
            {

                var yernew = int.Parse(year);
                var fid = int.Parse(FrequncyID);
                var clientData = unitOfWork.TblClientRHistoryRepository.Get(x => x.ClientID == clientID && x.ExtraInt1 == yernew & x.FrequncyID== fid & x.FrequncyDID== FrequncyDID).ToList();

                ClientVM clientVM = new ClientVM();

                clientVM.CustomerType = clientData[0].CustomerType != null ? Convert.ToInt32(clientData[0].HomeCountryID) : 0;

                if (clientData[0].TitleID == null)
                    clientVM.TitleID = 0;
                else if ((int)clientData[0].TitleID < 0)
                    clientVM.TitleID = 0;
                else
                    clientVM.TitleID = (int)clientData[0].TitleID;

                // clientVM.TitleID = (int)clientData.TitleID < 0  || string.IsNullOrEmpty(clientData.TitleID.ToString()) || clientData.TitleID.ToString ().Length<=0 ? 0 : (int)clientData.TitleID;
                clientVM.ClientID = clientData[0].ClientID < 0 ? 0 : (int)clientData[0].ClientID;
                clientVM.ClientName = string.IsNullOrEmpty(clientData[0].ClientName) ? "" : clientData[0].ClientName;
                clientVM.ClientOtherName = string.IsNullOrEmpty(clientData[0].ExtraText) ? "" : clientData[0].ExtraText;
                clientVM.ClientAddress = string.IsNullOrEmpty(clientData[0].ClientAddress) ? "" : clientData[0].ClientAddress;
                clientVM.NIC = string.IsNullOrEmpty(clientData[0].NIC) ? "" : clientData[0].NIC;
                clientVM.ContactNo = string.IsNullOrEmpty(clientData[0].ContactNo) ? "" : clientData[0].ContactNo;
                clientVM.FixedLine = string.IsNullOrEmpty(clientData[0].FixedLine) ? "" : clientData[0].FixedLine;
                clientVM.Email = string.IsNullOrEmpty(clientData[0].Email) ? "" : clientData[0].Email;
                clientVM.DOB = clientData[0].DOB != null ? Convert.ToDateTime(clientData[0].DOB).ToString("dd/MM/yyyy") : string.Empty;
                clientVM.PPID = string.IsNullOrEmpty(clientData[0].PPID) ? "" : clientData[0].PPID;
                clientVM.FamilyDiscount = clientData[0].FamilyDiscount > 0 ? Convert.ToDecimal(clientData[0].FamilyDiscount) : 0;
                clientVM.AdditionalNote = string.IsNullOrEmpty(clientData[0].AdditionalNote) ? "" : clientData[0].AdditionalNote;
                clientVM.HomeCountryID = clientData[0].HomeCountryID > 0 ? Convert.ToInt32(clientData[0].HomeCountryID) : 0;
                clientVM.ResidentCountryID = clientData[0].ResidentCountryID > 0 ? Convert.ToInt32(clientData[0].ResidentCountryID) : 0;
                clientVM.PremiumAccept = clientData[0].PremiumAccept;
                clientVM.JoinDate = clientVM != null & clientData[0].JoinDate != null ? Convert.ToDateTime(clientData[0].JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                //  clientVM.ClientStatus= string.IsNullOrEmpty(clientData[0].) ? "" : clientData[0].AdditionalNote;
                clientVM.ClientStatus= string.IsNullOrEmpty(clientData[0].IsActive.ToString()) ?0 : (int)clientData[0].IsActive;


                var FamilyDetailsData = unitOfWork.TblFamilyMemberRepository.Get(x => x.ClientID == clientID & x.ExtraText1 == year.ToString() & x.FrequncyID == FrequncyID & x.FrequncyDID == FrequncyDID).ToList();

                var FamilyDetailsDedudtion = unitOfWork.TblFamilyMemberRepository.Get(x => x.ClientID == clientID & x.IsActive == 1 & x.ExtraText1 == year.ToString() & x.FrequncyID == FrequncyID & x.FrequncyDID == FrequncyDID).ToList();
                List<FamilyMembersVM> clienttVM = new List<FamilyMembersVM>();




                decimal paysum = 0;


                List<DeductionDetailsVM> deductionDetailsVM = new List<DeductionDetailsVM>();

                var extratext = clientData[0].ExtraText;
                var ClientDeductionData = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == clientID & x.PremiumHolder == extratext & x.ExtraText1 == year.ToString() & x.FrequncyID == FrequncyID & x.FrequncyDID == FrequncyDID);
                foreach (var deduction in ClientDeductionData)
                {
                    DeductionDetailsVM deductionVM = new DeductionDetailsVM();
                    deductionVM.DeductionID = deduction.DeductionID;
                    deductionVM.DeductionRate = deduction.DeductionRate;
                    deductionVM.LoadingRate = deduction.LodingRate;
                    deductionVM.PremiumHolder = deduction.PremiumHolder;
                    deductionVM.PremiumAmount = deduction.Premium;
                    deductionVM.NetPremium = deduction.NetPremium;
                    deductionVM.ClientID = deduction.ClientID;
                    deductionVM.Deductible = string.IsNullOrEmpty(deduction.Deductibles) ? "" : deduction.Deductibles;

                    deductionDetailsVM.Add(deductionVM);

                    paysum = (decimal)paysum + (decimal)deduction.NetPremium;
                }








                // List<BUPAPremiumVM> BUPAPremiumVM = new List<BUPAPremiumVM>();


                if (FamilyDetailsData.Count > 0)
                {

                    foreach (var family in FamilyDetailsData)
                    {
                        FamilyMembersVM familyDetailsVM = new FamilyMembersVM();
                        familyDetailsVM.FamilyMemberID = family.FamilyMemberID;




                        familyDetailsVM.TitleID = family.Title == null ? 0 : (int)family.Title;
                        familyDetailsVM.MemberName = string.IsNullOrEmpty(family.MemberName) ? "" : family.MemberName;
                        familyDetailsVM.MemberOtherName = string.IsNullOrEmpty(family.MemberOtherName) ? "" : family.MemberOtherName;
                        familyDetailsVM.MemberDOB = family.MemberDOB != null ? Convert.ToDateTime(family.MemberDOB).ToString("dd/MM/yyyy") : string.Empty;
                        familyDetailsVM.JoinDate = family.JoinDate != null ? Convert.ToDateTime(family.JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                        familyDetailsVM.NIC = string.IsNullOrEmpty(family.NICNo) ? "" : family.NICNo;
                        familyDetailsVM.ContactNo = string.IsNullOrEmpty(family.ContactNo) ? "" : family.ContactNo;
                        familyDetailsVM.RelationShipID = family.RelationShipID == null ? 0 : (int)family.RelationShipID;

                        familyDetailsVM.HomeCountryID = family.HomeCountry == null ? 0 : (int)family.HomeCountry;
                        familyDetailsVM.ResidentCountryID = family.CountryOfResident == null ? 0 : (int)family.CountryOfResident;


                        familyDetailsVM.GenderID = family.GenderID == null ? 0 : (int)family.GenderID;
                        familyDetailsVM.SchemeID = family.premiumID == null ? 0 : int.Parse(family.premiumID.ToString());
                        familyDetailsVM.IsActive = family.IsActive == null ? 0 : (int)family.IsActive;
                        familyDetailsVM.MembershipID = family.MembershipID == null ? "NA" : family.MembershipID;
                        familyDetailsVM.OptionalCover = family.OptionalCover == null ? "NA" : family.OptionalCover;
                        familyDetailsVM.Exclu = family.Exclu == null ? "NA" : family.Exclu;
                        clienttVM.Add(familyDetailsVM);

                        // var ObjBUPAPremiumVM = new BUPAPremiumVM();
                        // ObjBUPAPremiumVM.Primium= string.IsNullOrEmpty(family.MemberName) ? "" : family.MemberName;
                        // BUPAPremiumVM.Add(ObjBUPAPremiumVM);

                        var GrpFamilyData = unitOfWork.TblGrpFamilyDetailRepository.Get(x => x.FamilyMemberID == family.FamilyMemberID & x.ExtraText2 == year.ToString() & x.ClientID == clientID & x.FrequncyID == FrequncyID & x.FrequncyDID == FrequncyDID).ToList();
                        List<GroupFamilyMembersVM> GroupVM = new List<GroupFamilyMembersVM>();

                        //if (GrpFamilyData.Count > 0)
                        //{

                        foreach (var group in GrpFamilyData)
                        {
                            GroupFamilyMembersVM groupFamilyVM = new GroupFamilyMembersVM();
                            groupFamilyVM.TitleID = group.TitleID == null || group.TitleID < 0 ? 0 : (int)group.TitleID;
                            groupFamilyVM.FamilyMemberID = group.MemberID < 0 ? 0 : group.MemberID;

                            groupFamilyVM.MemberName = group.MemberName;
                            groupFamilyVM.Exclu = group.Exclu == null ? "NA" : group.Exclu;
                            groupFamilyVM.MemberOtherName = group.ExtraText1 == null ? "NA" : group.ExtraText1;
                            groupFamilyVM.OptionalCover = group.OptionalCovers == null ? "NA" : group.OptionalCovers;
                            groupFamilyVM.MemberDOB = group.MemberDOB != null ? Convert.ToDateTime(group.MemberDOB).ToString("dd/MM/yyyy") : string.Empty;
                            groupFamilyVM.JoinDate = group.JoinDate != null ? Convert.ToDateTime(group.JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                            groupFamilyVM.NIC = group.MemberNIC;
                            groupFamilyVM.ContactNo = group.MemberContact;
                            groupFamilyVM.MemberCountryID = group.MemberCountryID == null ? 0 : (int)group.MemberCountryID;
                            groupFamilyVM.ResidentCountryID = group.MemberResCountryID == null ? 0 : (int)group.MemberResCountryID;
                            groupFamilyVM.IsActive = group.IsActive == null ? 0 : (int)group.IsActive;
                            groupFamilyVM.MembershipID = group.MembershipID == null ? "NA" : group.MembershipID;
                            groupFamilyVM.RelationShipID = group.RelationShipID == null ? 0 : (int)group.RelationShipID;
                            //  groupFamilyVM=


                            //groupFamilyVM.RelationShipID = group.RelationShipID < 0 ? 0 : group.RelationShipID;
                            //groupFamilyVM.GenderID = group.GenderID < 0 ? 0 : group.GenderID;
                            //groupFamilyVM.MemberOtherName = string.IsNullOrEmpty(group.MemberOtherName) ? "" : group.MemberOtherName;

                            GroupVM.Add(groupFamilyVM);

                            //  var ObjBUPAPremiumVMGrp = new BUPAPremiumVM();
                            //  ObjBUPAPremiumVMGrp.Primium = group.MemberName;
                            //  BUPAPremiumVM.Add(ObjBUPAPremiumVMGrp);



                        }
                        familyDetailsVM.GroupMemberDetails = GroupVM;
                        //var DeductionData = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == clientID & x.PremiumHolder == family.MemberName).ToList();
                        //foreach (var deduction in DeductionData)
                        //{
                        //    DeductionDetailsVM deductionVM = new DeductionDetailsVM();
                        //    deductionVM.DeductionID = deduction.DeductionID;
                        //    deductionVM.DeductionRate = deduction.DeductionRate;
                        //    deductionVM.LoadingRate = deduction.LodingRate;
                        //    deductionVM.PremiumHolder = deduction.PremiumHolder;
                        //    deductionVM.PremiumAmount = deduction.Premium;
                        //    deductionVM.NetPremium = deduction.NetPremium;
                        //    deductionVM.ClientID = deduction.ClientID;
                        //    deductionVM.Deductible = string.IsNullOrEmpty(deduction.Deductibles) ? "" : deduction.Deductibles;

                        //    deductionDetailsVM.Add(deductionVM);

                        //    paysum = (decimal)paysum + (decimal)deduction.Premium;

                        //}











                        //}


                    }


                    clientVM.FamilyDetails = clienttVM;
                }







                if (FamilyDetailsDedudtion.Count > 0)
                {

                    foreach (var family in FamilyDetailsDedudtion)
                    {



                        var DeductionData = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == clientID & x.PremiumHolder == family.MemberName & x.ExtraText1 == year.ToString() & x.FrequncyID == FrequncyID & x.FrequncyDID == FrequncyDID).ToList();
                        foreach (var deduction in DeductionData)
                        {
                            DeductionDetailsVM deductionVM = new DeductionDetailsVM();
                            deductionVM.DeductionID = deduction.DeductionID;
                            deductionVM.DeductionRate = deduction.DeductionRate;
                            deductionVM.LoadingRate = deduction.LodingRate;
                            deductionVM.PremiumHolder = deduction.PremiumHolder;
                            deductionVM.PremiumAmount = deduction.Premium;
                            deductionVM.NetPremium = deduction.NetPremium;
                            deductionVM.ClientID = deduction.ClientID;
                            deductionVM.Deductible = string.IsNullOrEmpty(deduction.Deductibles) ? "" : deduction.Deductibles;

                            deductionDetailsVM.Add(deductionVM);

                            paysum = (decimal)paysum + (decimal)deduction.NetPremium;

                        }

                        var GrpFamilly = unitOfWork.TblGrpFamilyDetailRepository.Get(x => x.ClientID == clientID & x.FamilyMemberID == family.FamilyMemberID & x.IsActive == 1 & x.ExtraText2 == year.ToString() & x.FrequncyID == FrequncyID & x.FrequncyDID == FrequncyDID).ToList();


                        foreach (var grpFamillynew in GrpFamilly)
                        {
                            var DeductionDataGrp = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == clientID & x.PremiumHolder == grpFamillynew.MemberName & x.ExtraText1 == year.ToString() & x.FrequncyID == FrequncyID & x.FrequncyDID == FrequncyDID).ToList();
                            foreach (var deduction in DeductionDataGrp)
                            {
                                DeductionDetailsVM deductionVM = new DeductionDetailsVM();
                                deductionVM.DeductionID = deduction.DeductionID;
                                deductionVM.DeductionRate = deduction.DeductionRate;
                                deductionVM.LoadingRate = deduction.LodingRate;
                                deductionVM.PremiumHolder = deduction.PremiumHolder;
                                deductionVM.PremiumAmount = Math.Round((decimal)deduction.Premium, 2);
                                deductionVM.NetPremium = Math.Round((decimal)deduction.NetPremium, 2);
                                deductionVM.ClientID = deduction.ClientID;
                                deductionVM.Deductible = string.IsNullOrEmpty(deduction.Deductibles) ? "" : deduction.Deductibles;

                                deductionDetailsVM.Add(deductionVM);

                                paysum = (decimal)paysum + (decimal)deduction.NetPremium;

                            }

                        }






                        //}


                    }


                    // clientVM.FamilyDetails = clienttVM;
                }

                var PaymentData = unitOfWork.TblPaymentRepository.Get(x => x.ClientID == clientID).ToList();

                List<PaymentVM> paymenttVM = new List<PaymentVM>();
                foreach (var payment in PaymentData)
                {
                    PaymentVM paymentVM = new PaymentVM();
                    paymentVM.PaymentID = payment.PaymentID;
                    paymentVM.PaymentAmount = payment.PaymentAmount;

                    paymenttVM.Add(paymentVM);
                    clientVM.PaymentDetails = paymenttVM;
                }






                if (clientVM.PaymentDetails != null)
                    clientVM.PaymentDetails[0].PaymentAmount = Math.Round(paysum, 2);
                //List<DeductionDetailsVM> deductionDetailsVM = new List<DeductionDetailsVM>();




                clientVM.DeductionDetails = deductionDetailsVM;

                //if (clientData.PremiumAccept == "1")
                //{
                //    var ObjBUPAPremiumVMClient = new BUPAPremiumVM();
                //    ObjBUPAPremiumVMClient.Primium= string.IsNullOrEmpty(clientData.ClientName) ? "" : clientData.ClientName;


                //    BUPAPremiumVM.Add(ObjBUPAPremiumVMClient);
                //    clientVM.BUPAPremiumVM = BUPAPremiumVM;

                //}

                var BankTransData = unitOfWork.TblBankTransactionDetailRepository.Get(x => x.ClientID == clientID & x.Year == yernew & x.FrequncyID == FrequncyID & x.FrequncyDID == FrequncyDID).ToList();
                List<BankTransactionVM> bankTransactiontVM = new List<BankTransactionVM>();

                decimal paiedAmount = 0;
                foreach (var bank in BankTransData)
                {
                    BankTransactionVM bankTransactionVM = new BankTransactionVM();
                    bankTransactionVM.BankID = bank.BankID;
                    bankTransactionVM.PaymentMethodID = bank.PaymentID;
                    bankTransactionVM.DraftNo = bank.DraftNo;
                    bankTransactionVM.SGSAmount = bank.Amount;
                    bankTransactionVM.IBSAmount = bank.IBSAmount;
                    bankTransactionVM.BankDetailID = bank.BankDetailID;
                    bankTransactionVM.PaymentDate = bank.PaymentDate.ToString();

                    bankTransactiontVM.Add(bankTransactionVM);

                    paiedAmount = paiedAmount + (decimal)bank.Amount;
                }
                clientVM.BankTransactionDetails = bankTransactiontVM;
                clientVM.PaiedAmount = Math.Round(paysum - paiedAmount, 2);
                var PolicyBUPAData = unitOfWork.TblPolicyInformationBUPARepository.Get(x => x.ClientID == clientID).ToList();
                List<PolicyInfoBUPAVM> policyInfoBUPAtVM = new List<PolicyInfoBUPAVM>();
                foreach (var policy in PolicyBUPAData)
                {
                    PolicyInfoBUPAVM policyInfoBUPAVM = new PolicyInfoBUPAVM();
                    policyInfoBUPAVM.PolicyInfoID = policy.PolicyInfoID;
                    policyInfoBUPAVM.Premium = policy.Premium;
                    policyInfoBUPAVM.MemberID = policy.MemberID;
                    policyInfoBUPAVM.PolicyMethod = policy.PolicyMethod;
                    policyInfoBUPAVM.ClientID = policy.ClientID;



                    policyInfoBUPAtVM.Add(policyInfoBUPAVM);
                }
                clientVM.PolicyInfoBUPADetails = policyInfoBUPAtVM;

                if (clientVM.HomeCountryID > 0)
                {
                    clientVM.HomeCountryName = "NA";
                }

                clientVM.ResidentCountryID = clientData[0].ResidentCountryID != null ? Convert.ToInt32(clientData[0].ResidentCountryID) : 0;

                if (clientVM.ResidentCountryID > 0)
                {
                    clientVM.ResidentCountryName = "NA";
                }

                clientVM.BusinessUnitID = clientData[0].BUID != null ? Convert.ToInt32(clientData[0].BUID) : 0;

                if (clientVM.BusinessUnitID > 0)
                {

                    var Bus = unitOfWork.TblBussinessUnitRepository.GetByID(clientData[0].BUID);

                    clientVM.BusinessUnitName = Bus.BussinessUnit;
                }

                // var clientRequestHeaderData = unitOfWork.TblClientRequestHeaderRepository.GetByID(clientID);

                var clientRequestHeaderData = unitOfWork.TblClientRenewelRequestHeaderRepository.Get(x => x.ClientID == clientID & x.Year == year & x.FrequncyID == fid & x.FrequncyDID == FrequncyDID).ToList();
                // List<FamilyMembersVM> clienttVM = new List<FamilyMembersVM>();





                if (clientRequestHeaderData != null)
                {

                    clientVM.PremiumID = clientRequestHeaderData == null || clientRequestHeaderData[0].PartnerID < 0 || string.IsNullOrEmpty(clientRequestHeaderData[0].PartnerID.ToString()) ? 0 : (int)clientRequestHeaderData[0].PartnerID;
                    clientVM.PolicyStartDate = clientRequestHeaderData == null || clientRequestHeaderData[0].PolicyStart == null ? string.Empty : Convert.ToDateTime(clientRequestHeaderData[0].PolicyStart).ToString("dd/MM/yyyy");
                    clientVM.Exclusions = string.IsNullOrEmpty(clientRequestHeaderData[0].Exclusions.ToString()) || clientRequestHeaderData == null ? 0 : float.Parse(clientRequestHeaderData[0].Exclusions.ToString());
                    clientVM.OptionalCovers = clientRequestHeaderData[0].OptionalCovers;
                    clientVM.Occupation = clientRequestHeaderData[0].Occupation;
                    clientVM.RequestedDate = clientRequestHeaderData == null || clientRequestHeaderData[0].RequestedDate == null ? string.Empty : Convert.ToDateTime(clientRequestHeaderData[0].RequestedDate).ToString("dd/MM/yyyy");
                    clientVM.MembershipID = string.IsNullOrEmpty(clientRequestHeaderData[0].MembershipID.ToString()) || clientRequestHeaderData == null ? "" : clientRequestHeaderData[0].MembershipID.ToString();
                    clientVM.PolicyEndDate = clientRequestHeaderData == null || clientRequestHeaderData[0].PolicyEnd == null ? string.Empty : Convert.ToDateTime(clientRequestHeaderData[0].PolicyEnd).ToString("dd/MM/yyyy");
                    clientVM.SchemeID = string.IsNullOrEmpty(clientRequestHeaderData[0].SchemeID.ToString()) || clientRequestHeaderData == null ? 0 : int.Parse(clientRequestHeaderData[0].SchemeID.ToString());
                    clientVM.Exclu = string.IsNullOrEmpty(clientRequestHeaderData[0].Exclu.ToString()) || clientRequestHeaderData == null ? "" : clientRequestHeaderData[0].Exclu.ToString();
                    clientVM.GroupID = string.IsNullOrEmpty(clientRequestHeaderData[0].GroupID.ToString()) || clientRequestHeaderData == null ? "" : clientRequestHeaderData[0].GroupID.ToString();
                    clientVM.FrequncyDID = string.IsNullOrEmpty(clientRequestHeaderData[0].FrequncyDID.ToString()) || clientRequestHeaderData == null ? "" : clientRequestHeaderData[0].FrequncyDID.ToString();
                }
                else
                {
                    clientVM.PremiumID = 0;
                    clientVM.PolicyStartDate = string.Empty;
                    clientVM.Exclusions = 0;
                    clientVM.OptionalCovers = string.Empty;
                    clientVM.Occupation = string.Empty;
                    clientVM.MembershipID = "";
                    clientVM.SchemeID = 0;
                    clientVM.Exclu = "";
                    clientVM.GroupID = "";
                    // clientVM.PolicyStartDate = clientRequestHeaderData!=null & clientRequestHeaderData.PolicyStart != null ? Convert.ToDateTime(clientRequestHeaderData.PolicyStart).ToString("dd/MM/yyyy") : string.Empty;
                    clientVM.PolicyEndDate = string.Empty;

                }
                // clientVM.PolicyEndDate = clientRequestHeaderData !=null &  clientRequestHeaderData.PolicyEnd != null ? Convert.ToDateTime(clientRequestHeaderData.PolicyEnd).ToString("dd/MM/yyyy") : string.Empty;
                clientVM.CreatedBy = clientData != null & clientData[0].CreatedBy != null ? Convert.ToInt32(clientData[0].CreatedBy) : 0;
                clientVM.CreatedDate = clientData != null & clientData[0].CreatedDate != null ? clientData[0].CreatedDate.ToString() : string.Empty;
                clientVM.ModifiedBy = clientData != null & clientData[0].ModifiedBy != null ? Convert.ToInt32(clientData[0].ModifiedBy) : 0;
                clientVM.ModifiedDate = clientData != null & clientData[0].ModifiedDate != null ? clientData[0].ModifiedDate.ToString() : string.Empty;

                return clientVM;
            }
            catch (Exception ex)
            {

                return null;
                // throw;
            }
        }





        public List<ClientRequestHeaderVM> GetAllRequestsByBusinessUnitID(int businessUnitID)
        {
            try
            {
                var clientRequestHeaderData = unitOfWork.TblClientRequestHeaderRepository.Get(x => x.tblClient.BUID == businessUnitID).ToList();//.OrderByDescending(wef => wef.ClientID);

                List<ClientRequestHeaderVM> clientRequestHeaderList = new List<ClientRequestHeaderVM>();

                foreach (var clientReqHeader in clientRequestHeaderData)
                {
                    //temp code
                    //if (clientRequestHeaderList.Count == 10) return clientRequestHeaderList;

                    ClientRequestHeaderVM clientRequestHeaderVM = new ClientRequestHeaderVM();
                    clientRequestHeaderVM.ClientRequestHeaderID = clientReqHeader.ClientRequestHeaderID;
                    clientRequestHeaderVM.ClientID = clientReqHeader.ClientID != null ? Convert.ToInt32(clientReqHeader.ClientID) : 0;

                    if (clientRequestHeaderVM.ClientID > 0)
                    {
                        clientRequestHeaderVM.ClientName = clientReqHeader.tblClient.ClientName;
                        clientRequestHeaderVM.BusinessUnitID = clientReqHeader.tblClient.BUID != null ? Convert.ToInt32(clientReqHeader.tblClient.BUID) : 0;

                        if (clientRequestHeaderVM.BusinessUnitID > 0)
                        {
                            clientRequestHeaderVM.BusinessUnitName = clientReqHeader.tblClient.tblBussinessUnit.BussinessUnit;
                            clientRequestHeaderVM.CompanyID = clientReqHeader.tblClient.tblBussinessUnit.CompID != null ? Convert.ToInt32(clientReqHeader.tblClient.tblBussinessUnit.CompID) : 0;

                            if (clientRequestHeaderVM.CompanyID > 0)
                            {
                                clientRequestHeaderVM.CompanyName = clientReqHeader.tblClient.tblBussinessUnit.tblCompany.CompanyName;
                            }
                        }
                        var PaymentData = unitOfWork.TblPaymentRepository.Get(x => x.ClientID == clientReqHeader.ClientID).ToList();
                        List<PaymentVM> paymenttVM = new List<PaymentVM>();
                        foreach (var payment in PaymentData)
                        {
                            PaymentVM paymentVM = new PaymentVM();
                            paymentVM.PaymentID = payment.PaymentID;
                            paymentVM.PaymentAmount = payment.PaymentAmount;

                            paymenttVM.Add(paymentVM);
                        }
                        clientRequestHeaderVM.PaymentDetails = paymenttVM;
                    }

                    clientRequestHeaderVM.PartnerID = clientReqHeader.PartnerID != null ? Convert.ToInt32(clientReqHeader.PartnerID) : 0;

                    if (clientRequestHeaderVM.PartnerID > 0)
                    {
                        var PremiumData = unitOfWork.TblPremiumRepository.GetByID(clientRequestHeaderVM.PartnerID);
                        clientRequestHeaderVM.PremiumName = PremiumData.PremiumName;

                        //clientRequestHeaderVM.PartnerName = clientReqHeader.tblPartner.PartnerName;
                    }
                    if (clientRequestHeaderVM.AgentID > 0)
                    {
                        var AgentData = unitOfWork.TblAgentRepository.GetByID(clientRequestHeaderVM.AgentID);
                        clientRequestHeaderVM.AgentName = AgentData.AgentName;

                        //clientRequestHeaderVM.PartnerName = clientReqHeader.tblPartner.PartnerName;
                    }

                    clientRequestHeaderVM.PolicyEndDate = clientReqHeader.PolicyEnd != null ? Convert.ToDateTime(clientReqHeader.PolicyEnd).ToString("dd/MM/yyyy") : string.Empty;
                    clientRequestHeaderVM.PolicyStartDate = clientReqHeader.PolicyStart != null ? Convert.ToDateTime(clientReqHeader.PolicyStart).ToString("dd/MM/yyyy") : string.Empty;

                    clientRequestHeaderVM.RequestedDate = clientReqHeader.RequestedDate != null ? Convert.ToDateTime(clientReqHeader.RequestedDate).ToString("dd/MM/yyyy") : string.Empty;
                    clientRequestHeaderVM.IsQuotationCreated = clientReqHeader.IsQuotationCreated != null ? Convert.ToBoolean(clientReqHeader.IsQuotationCreated) : false;
                    clientRequestHeaderVM.CreatedBy = clientReqHeader.CreatedBy != null ? Convert.ToInt32(clientReqHeader.CreatedBy) : 0;
                    clientRequestHeaderVM.CreatedDate = clientReqHeader.CreatedDate != null ? clientReqHeader.CreatedDate.ToString() : string.Empty;
                   // clientRequestHeaderVM.ModifiedBy = clientReqHeader.ModifiedBy != null ? Convert.ToInt32(clientReqHeader.ModifiedBy) : 0;
                    clientRequestHeaderVM.ModifiedDate = clientReqHeader.ModifiedDate != null ? clientReqHeader.ModifiedDate.ToString() : string.Empty;
                    clientRequestHeaderVM.MembershipID = clientReqHeader.MembershipID == null ? "0" : clientReqHeader.MembershipID.Trim();
                    clientRequestHeaderVM.ClientOtherName = clientReqHeader.tblClient.ExtraText;
                    //Client Request Line Details
                    var clientRequestLineData = unitOfWork.TblClientRequestLineRepository.Get(x => x.ClientRequestHeaderID == clientReqHeader.ClientRequestHeaderID).ToList();

                    List<ClientRequestLineVM> clientRequesLinetList = new List<ClientRequestLineVM>();

                    foreach (var clientReqLine in clientRequestLineData)
                    {
                        ClientRequestLineVM clientRequestLineVM = new ClientRequestLineVM();
                        clientRequestLineVM.ClientRequestLineID = clientReqLine.ClientRequestLineID;
                        clientRequestLineVM.InsSubClassID = clientReqLine.InsSubClassID != null ? Convert.ToInt32(clientReqLine.InsSubClassID) : 0;

                        if (clientRequestLineVM.InsSubClassID > 0)
                        {
                            clientRequestLineVM.InsSubClassName = clientReqLine.tblInsSubClass.Description;
                            clientRequestLineVM.InsClassID = clientReqLine.tblInsSubClass.InsClassID != null ? Convert.ToInt32(clientReqLine.tblInsSubClass.InsClassID) : 0;

                            if (clientRequestLineVM.InsClassID > 0)
                            {
                                clientRequestLineVM.InsClassName = clientReqLine.tblInsSubClass.tblInsClass.Code;
                            }
                        }

                       
                        clientRequestLineVM.CreatedBy = clientReqLine.CreatedBy != null ? Convert.ToInt32(clientReqLine.CreatedBy) : 0;
                        clientRequestLineVM.CreatedDate = clientReqLine.CreatedDate != null ? clientReqLine.CreatedDate.ToString() : string.Empty;
                        clientRequestLineVM.ModifiedBy = clientReqLine.ModifiedBy != null ? Convert.ToInt32(clientReqLine.ModifiedBy) : 0;
                        clientRequestLineVM.ModifiedDate = clientReqLine.ModifiedDate != null ? clientReqLine.ModifiedDate.ToString() : string.Empty;

                        //Client Property Details
                        var clientPropertyData = unitOfWork.TblClientPropertyRepository.Get(x => x.ClientRequestLineID == clientReqLine.ClientRequestLineID).ToList();

                        List<ClientPropertyVM> clientPropertyList = new List<ClientPropertyVM>();

                        foreach (var clientProperty in clientPropertyData)
                        {
                            ClientPropertyVM clientPropertyVM = new ClientPropertyVM();
                            clientPropertyVM.ClientPropertyID = clientProperty.ClientPropertyID;
                            clientPropertyVM.ClientPropertyName = clientProperty.ClientPropertyName;
                            clientPropertyVM.BRNo = clientProperty.BRNo;
                            clientPropertyVM.VATNo = clientProperty.VATNo;
                            clientPropertyVM.CreatedBy = clientProperty.CreatedBy != null ? Convert.ToInt32(clientProperty.CreatedBy) : 0;
                            clientPropertyVM.CreatedDate = clientProperty.CreatedDate != null ? clientProperty.CreatedDate.ToString() : string.Empty;
                            clientPropertyVM.ModifiedBy = clientProperty.ModifiedBy != null ? Convert.ToInt32(clientProperty.ModifiedBy) : 0;
                            clientPropertyVM.ModifiedDate = clientProperty.ModifiedDate != null ? clientProperty.ModifiedDate.ToString() : string.Empty;

                            clientPropertyList.Add(clientPropertyVM);
                        }

                        //Client Request Insurance Sub Class Scope Details
                        var clientReqInsSubClassScopeData = unitOfWork.TblClientRequestInsSubClassScopeRepository.Get(x => x.ClientRequestLineID == clientReqLine.ClientRequestLineID).ToList();

                        List<ClientRequestInsSubClassScopeVM> clientReqInsSubClassScopeList = new List<ClientRequestInsSubClassScopeVM>();

                        foreach (var clientReqInsSubClassScope in clientReqInsSubClassScopeData)
                        {
                            ClientRequestInsSubClassScopeVM clientRequestInsSubClassScopeVM = new ClientRequestInsSubClassScopeVM();
                            clientRequestInsSubClassScopeVM.ClientRequestInsSubClassScopeID = clientReqInsSubClassScope.ClientRequestInsSubClassScopeID;
                            clientRequestInsSubClassScopeVM.CommonInsScopeID = clientReqInsSubClassScope.CommonInsScopeID != null ? Convert.ToInt32(clientReqInsSubClassScope.CommonInsScopeID) : 0;

                            if (clientRequestInsSubClassScopeVM.CommonInsScopeID > 0)
                            {
                                clientRequestInsSubClassScopeVM.CommonInsScopeName = clientReqInsSubClassScope.tblCommonInsScope.Description;
                            }

                            clientRequestInsSubClassScopeVM.CreatedBy = clientReqInsSubClassScope.CreatedBy != null ? Convert.ToInt32(clientReqInsSubClassScope.CreatedBy) : 0;
                            clientRequestInsSubClassScopeVM.CreatedDate = clientReqInsSubClassScope.CreatedDate != null ? clientReqInsSubClassScope.CreatedDate.ToString() : string.Empty;
                            clientRequestInsSubClassScopeVM.ModifiedBy = clientReqInsSubClassScope.ModifiedBy != null ? Convert.ToInt32(clientReqInsSubClassScope.ModifiedBy) : 0;
                            clientRequestInsSubClassScopeVM.ModifiedDate = clientReqInsSubClassScope.ModifiedDate != null ? clientReqInsSubClassScope.ModifiedDate.ToString() : string.Empty;

                            clientReqInsSubClassScopeList.Add(clientRequestInsSubClassScopeVM);
                        }

                        clientRequestLineVM.ClientPropertyDetails = clientPropertyList;
                        clientRequestLineVM.ClientRequestInsSubClassScopeDetails = clientReqInsSubClassScopeList;

                        clientRequesLinetList.Add(clientRequestLineVM);
                    }

                    clientRequestHeaderVM.ClientRequestLineDetails = clientRequesLinetList;
                    clientRequestHeaderVM.IsActive = clientReqHeader.IsActive;
                    clientRequestHeaderVM.InActiveEffectiveDate = clientReqHeader.InActiveEffectiveDate;
                    clientRequestHeaderList.Add(clientRequestHeaderVM);
                }

                return clientRequestHeaderList;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.ToString());
                return null;
            }
        }

        public List<ClientRequestHeaderVM> getAllHistoryClientRequestsByBUID(int businessUnitID)
        {
            try
            {
                var BussinussUnitClient = unitOfWork.TblClientRHistoryRepository.Get(x => x.BUID == businessUnitID);

                List<ClientRequestHeaderVM> clientRequestHeaderList = new List<ClientRequestHeaderVM>();
                foreach (var bUnitClient  in BussinussUnitClient)
                {
                   var clientRequestHeaderData = unitOfWork.TblClientRenewelRequestHeaderRepository.Get(x => x.ClientID == bUnitClient.ClientID ).ToList();
                                      

                    foreach (var clientReqHeader in clientRequestHeaderData)
                    {
                        ClientRequestHeaderVM clientRequestHeaderVM = new ClientRequestHeaderVM();
                        clientRequestHeaderVM.ClientRequestHeaderID =(int) clientReqHeader.ClientRequestHeaderID;
                        clientRequestHeaderVM.ClientID = clientReqHeader.ClientID != null ? Convert.ToInt32(clientReqHeader.ClientID) : 0;

                        if (clientRequestHeaderVM.ClientID > 0)
                        {
                            clientRequestHeaderVM.ClientName = bUnitClient.ClientName;
                            clientRequestHeaderVM.BusinessUnitID = businessUnitID;

                            if (clientRequestHeaderVM.BusinessUnitID > 0)
                            {
                                var BussinussUnit = unitOfWork.TblBussinessUnitRepository.GetByID( businessUnitID);

                                clientRequestHeaderVM.BusinessUnitName = BussinussUnit.BussinessUnit;
                                clientRequestHeaderVM.CompanyID = 1;

                                if (clientRequestHeaderVM.CompanyID > 0)
                                {
                                    clientRequestHeaderVM.CompanyName = "NA";
                                }
                            }
                            var PaymentData = unitOfWork.TblPaymentRepository.Get(x => x.ClientID == clientReqHeader.ClientID).ToList();
                            List<PaymentVM> paymenttVM = new List<PaymentVM>();
                            foreach (var payment in PaymentData)
                            {
                                PaymentVM paymentVM = new PaymentVM();
                                paymentVM.PaymentID = payment.PaymentID;
                                paymentVM.PaymentAmount = payment.PaymentAmount;

                                paymenttVM.Add(paymentVM);
                            }
                            clientRequestHeaderVM.PaymentDetails = paymenttVM;
                        }

                        clientRequestHeaderVM.PartnerID = clientReqHeader.PartnerID != null ? Convert.ToInt32(clientReqHeader.PartnerID) : 0;

                        if (clientRequestHeaderVM.PartnerID > 0)
                        {
                            var PremiumData = unitOfWork.TblPremiumRepository.GetByID(clientRequestHeaderVM.PartnerID);
                            clientRequestHeaderVM.PremiumName = PremiumData.PremiumName;

                            //clientRequestHeaderVM.PartnerName = clientReqHeader.tblPartner.PartnerName;
                        }
                        if (clientRequestHeaderVM.AgentID > 0)
                        {
                            var AgentData = unitOfWork.TblAgentRepository.GetByID(clientRequestHeaderVM.AgentID);
                            clientRequestHeaderVM.AgentName = AgentData.AgentName;

                            //clientRequestHeaderVM.PartnerName = clientReqHeader.tblPartner.PartnerName;
                        }

                        clientRequestHeaderVM.PolicyEndDate = clientReqHeader.PolicyEnd != null ? Convert.ToDateTime(clientReqHeader.PolicyEnd).ToString("dd/MM/yyyy") : string.Empty;
                      //  clientRequestHeaderVM.PolicyEndDate = ""



                        clientRequestHeaderVM.PolicyStartDate = clientReqHeader.PolicyStart != null ? Convert.ToDateTime(clientReqHeader.PolicyStart).ToString("dd/MM/yyyy") : string.Empty;
                        clientRequestHeaderVM.Year = bUnitClient.ExtraInt1.ToString();
                        clientRequestHeaderVM.FrequncyID = bUnitClient.FrequncyID==null?0:(int) bUnitClient.FrequncyID;
                        clientRequestHeaderVM.FrequncyDID = bUnitClient.FrequncyDID;
                        var fid= bUnitClient.FrequncyID == null ? 0 : (int)bUnitClient.FrequncyID;

                        var  fre=  unitOfWork.TblFrequncy.GetByID(fid);
                        clientRequestHeaderVM.Frequncy = fre==null?"NA": fre.Name;
                        var frecat = unitOfWork.TblFrequncyDetailRepository.Get(x=>x.FrequncyID== fid.ToString() & x.Code== bUnitClient.FrequncyDID).ToList();
                        clientRequestHeaderVM.FrequncyCat = frecat.Count==0?"NA": frecat[0].Description;

                        clientRequestHeaderVM.RequestedDate = clientReqHeader.RequestedDate != null ? Convert.ToDateTime(clientReqHeader.RequestedDate).ToString("dd/MM/yyyy") : string.Empty;
                        clientRequestHeaderVM.IsQuotationCreated = clientReqHeader.IsQuotationCreated != null ? Convert.ToBoolean(clientReqHeader.IsQuotationCreated) : false;
                        clientRequestHeaderVM.CreatedBy = clientReqHeader.CreatedBy != null ? Convert.ToInt32(clientReqHeader.CreatedBy) : 0;
                        clientRequestHeaderVM.CreatedDate = clientReqHeader.CreatedDate != null ? clientReqHeader.CreatedDate.ToString() : string.Empty;
                        // clientRequestHeaderVM.ModifiedBy = clientReqHeader.ModifiedBy != null ? Convert.ToInt32(clientReqHeader.ModifiedBy) : 0;
                        clientRequestHeaderVM.ModifiedDate = clientReqHeader.ModifiedDate != null ? clientReqHeader.ModifiedDate.ToString() : string.Empty;

                        //Client Request Line Details
                        var clientRequestLineData = unitOfWork.TblClientRequestLineRepository.Get(x => x.ClientRequestHeaderID == clientReqHeader.ClientRequestHeaderID).ToList();

                        List<ClientRequestLineVM> clientRequesLinetList = new List<ClientRequestLineVM>();

                        foreach (var clientReqLine in clientRequestLineData)
                        {
                            ClientRequestLineVM clientRequestLineVM = new ClientRequestLineVM();
                            clientRequestLineVM.ClientRequestLineID = clientReqLine.ClientRequestLineID;
                            clientRequestLineVM.InsSubClassID = clientReqLine.InsSubClassID != null ? Convert.ToInt32(clientReqLine.InsSubClassID) : 0;

                            if (clientRequestLineVM.InsSubClassID > 0)
                            {
                                clientRequestLineVM.InsSubClassName = clientReqLine.tblInsSubClass.Description;
                                clientRequestLineVM.InsClassID = clientReqLine.tblInsSubClass.InsClassID != null ? Convert.ToInt32(clientReqLine.tblInsSubClass.InsClassID) : 0;

                                if (clientRequestLineVM.InsClassID > 0)
                                {
                                    clientRequestLineVM.InsClassName = clientReqLine.tblInsSubClass.tblInsClass.Code;
                                }
                            }


                            clientRequestLineVM.CreatedBy = clientReqLine.CreatedBy != null ? Convert.ToInt32(clientReqLine.CreatedBy) : 0;
                            clientRequestLineVM.CreatedDate = clientReqLine.CreatedDate != null ? clientReqLine.CreatedDate.ToString() : string.Empty;
                            clientRequestLineVM.ModifiedBy = clientReqLine.ModifiedBy != null ? Convert.ToInt32(clientReqLine.ModifiedBy) : 0;
                            clientRequestLineVM.ModifiedDate = clientReqLine.ModifiedDate != null ? clientReqLine.ModifiedDate.ToString() : string.Empty;

                            //Client Property Details
                            var clientPropertyData = unitOfWork.TblClientPropertyRepository.Get(x => x.ClientRequestLineID == clientReqLine.ClientRequestLineID).ToList();

                            List<ClientPropertyVM> clientPropertyList = new List<ClientPropertyVM>();

                            foreach (var clientProperty in clientPropertyData)
                            {
                                ClientPropertyVM clientPropertyVM = new ClientPropertyVM();
                                clientPropertyVM.ClientPropertyID = clientProperty.ClientPropertyID;
                                clientPropertyVM.ClientPropertyName = clientProperty.ClientPropertyName;
                                clientPropertyVM.BRNo = clientProperty.BRNo;
                                clientPropertyVM.VATNo = clientProperty.VATNo;
                                clientPropertyVM.CreatedBy = clientProperty.CreatedBy != null ? Convert.ToInt32(clientProperty.CreatedBy) : 0;
                                clientPropertyVM.CreatedDate = clientProperty.CreatedDate != null ? clientProperty.CreatedDate.ToString() : string.Empty;
                                clientPropertyVM.ModifiedBy = clientProperty.ModifiedBy != null ? Convert.ToInt32(clientProperty.ModifiedBy) : 0;
                                clientPropertyVM.ModifiedDate = clientProperty.ModifiedDate != null ? clientProperty.ModifiedDate.ToString() : string.Empty;

                                clientPropertyList.Add(clientPropertyVM);
                            }

                            //Client Request Insurance Sub Class Scope Details
                            var clientReqInsSubClassScopeData = unitOfWork.TblClientRequestInsSubClassScopeRepository.Get(x => x.ClientRequestLineID == clientReqLine.ClientRequestLineID).ToList();

                            List<ClientRequestInsSubClassScopeVM> clientReqInsSubClassScopeList = new List<ClientRequestInsSubClassScopeVM>();

                            foreach (var clientReqInsSubClassScope in clientReqInsSubClassScopeData)
                            {
                                ClientRequestInsSubClassScopeVM clientRequestInsSubClassScopeVM = new ClientRequestInsSubClassScopeVM();
                                clientRequestInsSubClassScopeVM.ClientRequestInsSubClassScopeID = clientReqInsSubClassScope.ClientRequestInsSubClassScopeID;
                                clientRequestInsSubClassScopeVM.CommonInsScopeID = clientReqInsSubClassScope.CommonInsScopeID != null ? Convert.ToInt32(clientReqInsSubClassScope.CommonInsScopeID) : 0;

                                if (clientRequestInsSubClassScopeVM.CommonInsScopeID > 0)
                                {
                                    clientRequestInsSubClassScopeVM.CommonInsScopeName = clientReqInsSubClassScope.tblCommonInsScope.Description;
                                }

                                clientRequestInsSubClassScopeVM.CreatedBy = clientReqInsSubClassScope.CreatedBy != null ? Convert.ToInt32(clientReqInsSubClassScope.CreatedBy) : 0;
                                clientRequestInsSubClassScopeVM.CreatedDate = clientReqInsSubClassScope.CreatedDate != null ? clientReqInsSubClassScope.CreatedDate.ToString() : string.Empty;
                                clientRequestInsSubClassScopeVM.ModifiedBy = clientReqInsSubClassScope.ModifiedBy != null ? Convert.ToInt32(clientReqInsSubClassScope.ModifiedBy) : 0;
                                clientRequestInsSubClassScopeVM.ModifiedDate = clientReqInsSubClassScope.ModifiedDate != null ? clientReqInsSubClassScope.ModifiedDate.ToString() : string.Empty;

                                clientReqInsSubClassScopeList.Add(clientRequestInsSubClassScopeVM);
                            }

                            clientRequestLineVM.ClientPropertyDetails = clientPropertyList;
                            clientRequestLineVM.ClientRequestInsSubClassScopeDetails = clientReqInsSubClassScopeList;

                            clientRequesLinetList.Add(clientRequestLineVM);
                        }

                        clientRequestHeaderVM.ClientRequestLineDetails = clientRequesLinetList;

                        clientRequestHeaderList.Add(clientRequestHeaderVM);
                    }


                }



            

                return clientRequestHeaderList;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);

                return null;
            }
        }
                
        public ClientRequestHeaderVM GetRequestByID(int clientRequestHeaderID)
        {
            try
            {
                var clientRequestHeaderData = unitOfWork.TblClientRequestHeaderRepository.GetByID(clientRequestHeaderID);

                ClientRequestHeaderVM clientRequestHeaderVM = new ClientRequestHeaderVM();
                clientRequestHeaderVM.ClientRequestHeaderID = clientRequestHeaderData.ClientRequestHeaderID;
                clientRequestHeaderVM.ClientID = clientRequestHeaderData.ClientID != null ? Convert.ToInt32(clientRequestHeaderData.ClientID) : 0;

                var  ClientIDNew = clientRequestHeaderData.ClientID != null ? Convert.ToInt32(clientRequestHeaderData.ClientID) : 0;


                var curancyID = clientRequestHeaderData.CurrancyID==null?0: clientRequestHeaderData.CurrancyID;
                

                var ClientNew= unitOfWork.TblClientRepository.GetByID(ClientIDNew);
                var currancy = unitOfWork.TblCurrencyRepository.GetByID(curancyID);
               

                clientRequestHeaderVM.CurrancyCode= currancy==null?"N/A": currancy.CurrencyCode;
                clientRequestHeaderVM.Year = ClientNew.ExtraInt1.ToString();


                if (clientRequestHeaderVM.ClientID > 0)
                {
                    //ClientVM clientvm = new ClientVM();
                    clientRequestHeaderVM.ClientName = clientRequestHeaderData.tblClient.ClientName;
                    
                    clientRequestHeaderVM.BusinessUnitID = clientRequestHeaderData.tblClient.BUID != null ? Convert.ToInt32(clientRequestHeaderData.tblClient.BUID) : 0;
                    clientRequestHeaderVM.CurrancyID =(int) curancyID;
                    var FamilyDetailsData = unitOfWork.TblFamilyMemberRepository.Get(x => x.ClientID == clientRequestHeaderData.ClientID &&  x.ExtraText1== ClientNew.ExtraInt1.ToString()).ToList();
                    List<FamilyMembersVM> clienttVM = new List<FamilyMembersVM>();
                    foreach (var family in FamilyDetailsData)
                    {
                        FamilyMembersVM familyDetailsVM = new FamilyMembersVM();
                        familyDetailsVM.FamilyMemberID = family.FamilyMemberID;
                        familyDetailsVM.MemberName = family.MemberName;
                        familyDetailsVM.MemberDOB = family.MemberDOB != null ? Convert.ToDateTime(family.MemberDOB).ToString("dd/MM/yyyy") : string.Empty;
                        familyDetailsVM.MemberResCountryID = family.CountryOfResident != null ?(int) family.CountryOfResident : 0;
                        familyDetailsVM.MemberCountryID = family.HomeCountry!= null ? (int)family.HomeCountry : 0;

                        clienttVM.Add(familyDetailsVM);
                    }
                    clientRequestHeaderVM.FamilyDetails = clienttVM;

                    var PaymentData = unitOfWork.TblPaymentRepository.Get(x => x.ClientID == clientRequestHeaderData.ClientID).ToList();
                    List<PaymentVM> paymenttVM = new List<PaymentVM>();
                    foreach (var payment in PaymentData)
                    {
                        PaymentVM paymentVM = new PaymentVM();
                        paymentVM.PaymentID = payment.PaymentID;
                        paymentVM.PaymentAmount = payment.PaymentAmount;

                        paymenttVM.Add(paymentVM);
                    }
                    clientRequestHeaderVM.PaymentDetails = paymenttVM;

                    if (clientRequestHeaderVM.BusinessUnitID > 0)
                    {
                        clientRequestHeaderVM.BusinessUnitName = clientRequestHeaderData.tblClient.tblBussinessUnit.BussinessUnit;
                        clientRequestHeaderVM.CompanyID = clientRequestHeaderData.tblClient.tblBussinessUnit.CompID != null ? Convert.ToInt32(clientRequestHeaderData.tblClient.tblBussinessUnit.CompID) : 0;

                        if (clientRequestHeaderVM.CompanyID > 0)
                        {
                            clientRequestHeaderVM.CompanyName = clientRequestHeaderData.tblClient.tblBussinessUnit.tblCompany.CompanyName;
                        }
                    }
                }

                clientRequestHeaderVM.PartnerID = clientRequestHeaderData.PartnerID != null ? Convert.ToInt32(clientRequestHeaderData.PartnerID) : 0;

                if (clientRequestHeaderVM.PartnerID > 0)
                {
                    var PremiumData = unitOfWork.TblPremiumRepository.GetByID(clientRequestHeaderVM.PartnerID);
                    clientRequestHeaderVM.PremiumName = PremiumData.PremiumName;
                }
                //clientRequestHeaderVM.AgentID= clientRequestHeaderData.ModifiedBy != null ? Convert.ToInt32(clientRequestHeaderData.ModifiedBy) : 0;
                clientRequestHeaderVM.AgentID = clientRequestHeaderData.AgentID;

                if (clientRequestHeaderVM.AgentID > 0)
                {
                    var AgentData = unitOfWork.TblAgentRepository.GetByID(clientRequestHeaderVM.AgentID);
                    clientRequestHeaderVM.AgentName = AgentData.AgentName;

                    //clientRequestHeaderVM.PartnerName = clientReqHeader.tblPartner.PartnerName;
                }

                clientRequestHeaderVM.RequestedDate = clientRequestHeaderData.RequestedDate != null ? Convert.ToDateTime(clientRequestHeaderData.RequestedDate).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.IsQuotationCreated = clientRequestHeaderData.IsQuotationCreated != null ? Convert.ToBoolean(clientRequestHeaderData.IsQuotationCreated) : false;
                clientRequestHeaderVM.CreatedBy = clientRequestHeaderData.CreatedBy != null ? Convert.ToInt32(clientRequestHeaderData.CreatedBy) : 0;
                clientRequestHeaderVM.CreatedDate = clientRequestHeaderData.CreatedDate != null ? clientRequestHeaderData.CreatedDate.ToString() : string.Empty;
                clientRequestHeaderVM.ModifiedBy = clientRequestHeaderData.ModifiedBy != null ? Convert.ToInt32(clientRequestHeaderData.ModifiedBy) : 0;
                clientRequestHeaderVM.ModifiedDate = clientRequestHeaderData.ModifiedDate != null ? clientRequestHeaderData.ModifiedDate.ToString() : string.Empty;
                clientRequestHeaderVM.InspectionDate= clientRequestHeaderData.JoinDate != null ? Convert.ToDateTime(clientRequestHeaderData.JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.JoinDate = clientRequestHeaderData.JoinDate != null ? Convert.ToDateTime(clientRequestHeaderData.JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.FileNo = clientRequestHeaderData.FileNo != null ? clientRequestHeaderData.FileNo : string.Empty;
                // clientRequestHeaderVM.PilotPremiumID = clientRequestHeaderData.PilotPremiumID != null ? Convert.ToInt32(clientRequestHeaderData.PilotPremiumID) : 0;
                clientRequestHeaderVM.PilotPremiumID = clientRequestHeaderData.PilotPremiumID != null ? Convert.ToInt32(clientRequestHeaderData.PilotPremiumID) : 0;
                clientRequestHeaderVM.PartnerID = clientRequestHeaderData.PartnerID != null ? Convert.ToInt32(clientRequestHeaderData.PartnerID) : 0;
                clientRequestHeaderVM.DeductibleID = clientRequestHeaderData.DeductibleID != null ? Convert.ToInt32(clientRequestHeaderData.DeductibleID) : 0;
                clientRequestHeaderVM.PolicyStartDate= clientRequestHeaderData.PolicyStart != null ? Convert.ToDateTime(clientRequestHeaderData.PolicyStart).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.PolicyEndDate = clientRequestHeaderData.PolicyEnd != null ? Convert.ToDateTime(clientRequestHeaderData.PolicyEnd).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.Occupation = clientRequestHeaderData.Occupation;
                clientRequestHeaderVM.Exclusions = string.IsNullOrEmpty(clientRequestHeaderData.Exclusions.ToString()) || clientRequestHeaderData==null?0:float.Parse(clientRequestHeaderData.Exclusions.ToString());
                clientRequestHeaderVM.OptionalCovers = clientRequestHeaderData.OptionalCovers;
                clientRequestHeaderVM.CurrancyID = clientRequestHeaderData.CurrancyID==null?0:(int)clientRequestHeaderData.CurrancyID;
                clientRequestHeaderVM.FrequncyID =clientRequestHeaderData.FrequncyID==null?0:(int)clientRequestHeaderData.FrequncyID;
                clientRequestHeaderVM.Exclu= string.IsNullOrEmpty(clientRequestHeaderData.Exclu) || clientRequestHeaderData == null ? "Non" : clientRequestHeaderData.Exclu.ToString();
                clientRequestHeaderVM.MembershipID= string.IsNullOrEmpty(clientRequestHeaderData.MembershipID) || clientRequestHeaderData == null ? "NA" : clientRequestHeaderData.MembershipID.ToString();
                clientRequestHeaderVM.SchemeID = clientRequestHeaderData.SchemeID==null || clientRequestHeaderData == null ? 0 :(int) clientRequestHeaderData.SchemeID;
                clientRequestHeaderVM.GroupID = clientRequestHeaderData.GroupID == null || clientRequestHeaderData == null ? "0" : clientRequestHeaderData.GroupID;
                clientRequestHeaderVM.FrequncyDID= clientRequestHeaderData.FrequncyDID == null || clientRequestHeaderData == null ? "0" : clientRequestHeaderData.FrequncyDID;
                var fid= clientRequestHeaderData.FrequncyID == null || clientRequestHeaderData == null ?0 : clientRequestHeaderData.FrequncyID;
                var frequncyMaster = unitOfWork.TblFrequncy.GetByID(fid);
                clientRequestHeaderVM.Frequncy = frequncyMaster.Name==null?"NA": frequncyMaster.Name;
                var fDid = clientRequestHeaderData.FrequncyDID == null || clientRequestHeaderData == null ? "0" : clientRequestHeaderData.FrequncyDID;
                var frequncyDMaster = unitOfWork.TblFrequncyDetailRepository.Get(x=>x.FrequncyID==fid.ToString() & x.Code== fDid).ToList();
                clientRequestHeaderVM.FrequncyCat = frequncyDMaster[0].Description==null || frequncyDMaster==null?"NA": frequncyDMaster[0].Description;

                return clientRequestHeaderVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public ClientRequestHeaderVM GetRequestByID(int clientRequestHeaderID,int clientID,string  Year)
        {
            try
            {
                var clientRequestHeaderData = unitOfWork.TblClientRenewelRequestHeaderRepository.Get(x=> x.ClientRequestHeaderID== clientRequestHeaderID & x.Year== Year).ToList();
            
                ClientRequestHeaderVM clientRequestHeaderVM = new ClientRequestHeaderVM();
                clientRequestHeaderVM.ClientRequestHeaderID = (int)clientRequestHeaderData[0].ClientRequestHeaderID;
                clientRequestHeaderVM.ClientID = clientRequestHeaderData[0].ClientID != null ? Convert.ToInt32(clientRequestHeaderData[0].ClientID) : 0;

                var ClientIDNew = clientRequestHeaderData[0].ClientID != null ? Convert.ToInt32(clientRequestHeaderData[0].ClientID) : 0;


                var curancyID = clientRequestHeaderData[0].CurrancyID == null ? 0 : clientRequestHeaderData[0].CurrancyID;


                var ClientNew = unitOfWork.TblClientRHistoryRepository.Get(x=>x.ClientID==ClientIDNew).ToList();
                var currancy = unitOfWork.TblCurrencyRepository.GetByID(curancyID);


                clientRequestHeaderVM.CurrancyCode = currancy == null ? "N/A" : currancy.CurrencyCode;
                clientRequestHeaderVM.Year = Year;


                if (clientRequestHeaderVM.ClientID > 0)
                {
                    //ClientVM clientvm = new ClientVM();
                    clientRequestHeaderVM.ClientName = ClientNew[0].ClientName;

                    clientRequestHeaderVM.BusinessUnitID = ClientNew[0].BUID != null ?(int)ClientNew[0].BUID : 0;
                    clientRequestHeaderVM.CurrancyID = (int)curancyID;
                    var FamilyDetailsData = unitOfWork.TblFamilyMemberRepository.Get(x => x.ClientID == clientID && x.ExtraText1 == Year).ToList();
                    List<FamilyMembersVM> clienttVM = new List<FamilyMembersVM>();
                    foreach (var family in FamilyDetailsData)
                    {
                        FamilyMembersVM familyDetailsVM = new FamilyMembersVM();
                        familyDetailsVM.FamilyMemberID = family.FamilyMemberID;
                        familyDetailsVM.MemberName = family.MemberName;
                        familyDetailsVM.MemberDOB = family.MemberDOB != null ? Convert.ToDateTime(family.MemberDOB).ToString("dd/MM/yyyy") : string.Empty;
                        familyDetailsVM.MemberResCountryID = family.CountryOfResident != null ? (int)family.CountryOfResident : 0;
                        familyDetailsVM.MemberCountryID = family.HomeCountry != null ? (int)family.HomeCountry : 0;

                        clienttVM.Add(familyDetailsVM);
                    }
                    clientRequestHeaderVM.FamilyDetails = clienttVM;

                    var PaymentData = unitOfWork.TblPaymentRepository.Get(x => x.ClientID == clientID).ToList();
                    List<PaymentVM> paymenttVM = new List<PaymentVM>();
                    foreach (var payment in PaymentData)
                    {
                        PaymentVM paymentVM = new PaymentVM();
                        paymentVM.PaymentID = payment.PaymentID;
                        paymentVM.PaymentAmount = payment.PaymentAmount;

                        paymenttVM.Add(paymentVM);
                    }
                    clientRequestHeaderVM.PaymentDetails = paymenttVM;

                    if (clientRequestHeaderVM.BusinessUnitID > 0)
                    {

                        var BUnit = unitOfWork.TblBussinessUnitRepository.GetByID(ClientNew[0].BUID);

                        clientRequestHeaderVM.BusinessUnitName = BUnit.BussinessUnit;
                        clientRequestHeaderVM.CompanyID = 1;

                        if (clientRequestHeaderVM.CompanyID > 0)
                        {
                            clientRequestHeaderVM.CompanyName = "NA";
                        }
                    }
                }

                clientRequestHeaderVM.PartnerID = clientRequestHeaderData[0].PartnerID != null ? Convert.ToInt32(clientRequestHeaderData[0].PartnerID) : 0;

                if (clientRequestHeaderVM.PartnerID > 0)
                {
                    var PremiumData = unitOfWork.TblPremiumRepository.GetByID(clientRequestHeaderVM.PartnerID);
                    clientRequestHeaderVM.PremiumName = PremiumData.PremiumName;
                }
                clientRequestHeaderVM.AgentID = clientRequestHeaderData[0].ModifiedBy != null ? Convert.ToInt32(clientRequestHeaderData[0].ModifiedBy) : 0;

                if (clientRequestHeaderVM.AgentID > 0)
                {
                    var AgentData = unitOfWork.TblAgentRepository.GetByID(clientRequestHeaderVM.AgentID);
                    clientRequestHeaderVM.AgentName = AgentData.AgentName;

                    //clientRequestHeaderVM.PartnerName = clientReqHeader.tblPartner.PartnerName;
                }

                clientRequestHeaderVM.RequestedDate = clientRequestHeaderData[0].RequestedDate != null ? Convert.ToDateTime(clientRequestHeaderData[0].RequestedDate).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.IsQuotationCreated = clientRequestHeaderData[0].IsQuotationCreated != null ? Convert.ToBoolean(clientRequestHeaderData[0].IsQuotationCreated) : false;
                clientRequestHeaderVM.CreatedBy = clientRequestHeaderData[0].CreatedBy != null ? Convert.ToInt32(clientRequestHeaderData[0].CreatedBy) : 0;
                clientRequestHeaderVM.CreatedDate = clientRequestHeaderData[0].CreatedDate != null ? clientRequestHeaderData[0].CreatedDate.ToString() : string.Empty;
                clientRequestHeaderVM.ModifiedBy = clientRequestHeaderData[0].ModifiedBy != null ? Convert.ToInt32(clientRequestHeaderData[0].ModifiedBy) : 0;
                clientRequestHeaderVM.ModifiedDate = clientRequestHeaderData[0].ModifiedDate != null ? clientRequestHeaderData[0].ModifiedDate.ToString() : string.Empty;
                clientRequestHeaderVM.InspectionDate = clientRequestHeaderData[0].JoinDate != null ? Convert.ToDateTime(clientRequestHeaderData[0].JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.JoinDate = clientRequestHeaderData[0].JoinDate != null ? Convert.ToDateTime(clientRequestHeaderData[0].JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.FileNo = clientRequestHeaderData[0].FileNo != null ? clientRequestHeaderData[0].FileNo : string.Empty;
                // clientRequestHeaderVM.PilotPremiumID = clientRequestHeaderData.PilotPremiumID != null ? Convert.ToInt32(clientRequestHeaderData.PilotPremiumID) : 0;
                clientRequestHeaderVM.PilotPremiumID = clientRequestHeaderData[0].PilotPremiumID != null ? Convert.ToInt32(clientRequestHeaderData[0].PilotPremiumID) : 0;
                clientRequestHeaderVM.PartnerID = clientRequestHeaderData[0].PartnerID != null ? Convert.ToInt32(clientRequestHeaderData[0].PartnerID) : 0;
                clientRequestHeaderVM.DeductibleID = clientRequestHeaderData[0].DeductibleID != null ? Convert.ToInt32(clientRequestHeaderData[0].DeductibleID) : 0;
                clientRequestHeaderVM.PolicyStartDate = clientRequestHeaderData[0].PolicyStart != null ? Convert.ToDateTime(clientRequestHeaderData[0].PolicyStart).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.PolicyEndDate = clientRequestHeaderData[0].PolicyEnd != null ? Convert.ToDateTime(clientRequestHeaderData[0].PolicyEnd).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.Occupation = clientRequestHeaderData[0].Occupation;
                clientRequestHeaderVM.Exclusions = string.IsNullOrEmpty(clientRequestHeaderData[0].Exclusions.ToString()) || clientRequestHeaderData == null ? 0 : float.Parse(clientRequestHeaderData[0].Exclusions.ToString());
                clientRequestHeaderVM.OptionalCovers = clientRequestHeaderData[0].OptionalCovers;
                clientRequestHeaderVM.CurrancyID = clientRequestHeaderData[0].CurrancyID == null ? 0 : (int)clientRequestHeaderData[0].CurrancyID;
                clientRequestHeaderVM.FrequncyID = clientRequestHeaderData[0].FrequncyID == null ? 0 : (int)clientRequestHeaderData[0].FrequncyID;
                clientRequestHeaderVM.Exclu = string.IsNullOrEmpty(clientRequestHeaderData[0].Exclu) || clientRequestHeaderData == null ? "Non" : clientRequestHeaderData[0].Exclu.ToString();
                clientRequestHeaderVM.MembershipID = string.IsNullOrEmpty(clientRequestHeaderData[0].MembershipID) || clientRequestHeaderData == null ? "NA" : clientRequestHeaderData[0].MembershipID.ToString();
                clientRequestHeaderVM.SchemeID = clientRequestHeaderData[0].SchemeID == null || clientRequestHeaderData == null ? 0 : (int)clientRequestHeaderData[0].SchemeID;
                clientRequestHeaderVM.GroupID = clientRequestHeaderData[0].GroupID == null || clientRequestHeaderData == null ? "0" : clientRequestHeaderData[0].GroupID;
                return clientRequestHeaderVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ClientRequestHeaderVM GetRequestByID(int clientRequestHeaderID, int clientID, string Year,string FrequncyID,string FrequncyDID)
        {
            try
            {

                var fid = int.Parse(FrequncyID);
                var clientRequestHeaderData = unitOfWork.TblClientRenewelRequestHeaderRepository.Get(x => x.ClientRequestHeaderID == clientRequestHeaderID & x.Year == Year & x.FrequncyID== fid & x.FrequncyDID== FrequncyDID).ToList();

                ClientRequestHeaderVM clientRequestHeaderVM = new ClientRequestHeaderVM();
                clientRequestHeaderVM.ClientRequestHeaderID = (int)clientRequestHeaderData[0].ClientRequestHeaderID;
                clientRequestHeaderVM.ClientID = clientRequestHeaderData[0].ClientID != null ? Convert.ToInt32(clientRequestHeaderData[0].ClientID) : 0;

                var ClientIDNew = clientRequestHeaderData[0].ClientID != null ? Convert.ToInt32(clientRequestHeaderData[0].ClientID) : 0;


                var curancyID = clientRequestHeaderData[0].CurrancyID == null ? 0 : clientRequestHeaderData[0].CurrancyID;


                var ClientNew = unitOfWork.TblClientRHistoryRepository.Get(x => x.ClientID == ClientIDNew & x.FrequncyID== fid & x.FrequncyDID== FrequncyDID).ToList();
                var currancy = unitOfWork.TblCurrencyRepository.GetByID(curancyID);


                clientRequestHeaderVM.CurrancyCode = currancy == null ? "N/A" : currancy.CurrencyCode;
                clientRequestHeaderVM.Year = Year;


                if (clientRequestHeaderVM.ClientID > 0)
                {
                    //ClientVM clientvm = new ClientVM();
                    clientRequestHeaderVM.ClientName = ClientNew[0].ClientName;

                    clientRequestHeaderVM.BusinessUnitID = ClientNew[0].BUID != null ? (int)ClientNew[0].BUID : 0;
                    clientRequestHeaderVM.CurrancyID = (int)curancyID;
                    var FamilyDetailsData = unitOfWork.TblFamilyMemberRepository.Get(x => x.ClientID == clientID && x.ExtraText1 == Year & x.FrequncyID== FrequncyID & x.FrequncyDID== FrequncyDID).ToList();
                    List<FamilyMembersVM> clienttVM = new List<FamilyMembersVM>();
                    foreach (var family in FamilyDetailsData)
                    {
                        FamilyMembersVM familyDetailsVM = new FamilyMembersVM();
                        familyDetailsVM.FamilyMemberID = family.FamilyMemberID;
                        familyDetailsVM.MemberName = family.MemberName;
                        familyDetailsVM.MemberDOB = family.MemberDOB != null ? Convert.ToDateTime(family.MemberDOB).ToString("dd/MM/yyyy") : string.Empty;
                        familyDetailsVM.MemberResCountryID = family.CountryOfResident != null ? (int)family.CountryOfResident : 0;
                        familyDetailsVM.MemberCountryID = family.HomeCountry != null ? (int)family.HomeCountry : 0;

                        clienttVM.Add(familyDetailsVM);
                    }
                    clientRequestHeaderVM.FamilyDetails = clienttVM;

                    var PaymentData = unitOfWork.TblPaymentRepository.Get(x => x.ClientID == clientID).ToList();
                    List<PaymentVM> paymenttVM = new List<PaymentVM>();
                    foreach (var payment in PaymentData)
                    {
                        PaymentVM paymentVM = new PaymentVM();
                        paymentVM.PaymentID = payment.PaymentID;
                        paymentVM.PaymentAmount = payment.PaymentAmount;

                        paymenttVM.Add(paymentVM);
                    }
                    clientRequestHeaderVM.PaymentDetails = paymenttVM;

                    if (clientRequestHeaderVM.BusinessUnitID > 0)
                    {

                        var BUnit = unitOfWork.TblBussinessUnitRepository.GetByID(ClientNew[0].BUID);

                        clientRequestHeaderVM.BusinessUnitName = BUnit.BussinessUnit;
                        clientRequestHeaderVM.CompanyID = 1;

                        if (clientRequestHeaderVM.CompanyID > 0)
                        {
                            clientRequestHeaderVM.CompanyName = "NA";
                        }
                    }
                }

                clientRequestHeaderVM.PartnerID = clientRequestHeaderData[0].PartnerID != null ? Convert.ToInt32(clientRequestHeaderData[0].PartnerID) : 0;

                if (clientRequestHeaderVM.PartnerID > 0)
                {
                    var PremiumData = unitOfWork.TblPremiumRepository.GetByID(clientRequestHeaderVM.PartnerID);
                    clientRequestHeaderVM.PremiumName = PremiumData.PremiumName;
                }
                clientRequestHeaderVM.AgentID = clientRequestHeaderData[0].ModifiedBy != null ? Convert.ToInt32(clientRequestHeaderData[0].ModifiedBy) : 0;

                if (clientRequestHeaderVM.AgentID > 0)
                {
                    var AgentData = unitOfWork.TblAgentRepository.GetByID(clientRequestHeaderVM.AgentID);
                    clientRequestHeaderVM.AgentName = AgentData.AgentName;

                    //clientRequestHeaderVM.PartnerName = clientReqHeader.tblPartner.PartnerName;
                }

                clientRequestHeaderVM.RequestedDate = clientRequestHeaderData[0].RequestedDate != null ? Convert.ToDateTime(clientRequestHeaderData[0].RequestedDate).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.IsQuotationCreated = clientRequestHeaderData[0].IsQuotationCreated != null ? Convert.ToBoolean(clientRequestHeaderData[0].IsQuotationCreated) : false;
                clientRequestHeaderVM.CreatedBy = clientRequestHeaderData[0].CreatedBy != null ? Convert.ToInt32(clientRequestHeaderData[0].CreatedBy) : 0;
                clientRequestHeaderVM.CreatedDate = clientRequestHeaderData[0].CreatedDate != null ? clientRequestHeaderData[0].CreatedDate.ToString() : string.Empty;
                clientRequestHeaderVM.ModifiedBy = clientRequestHeaderData[0].ModifiedBy != null ? Convert.ToInt32(clientRequestHeaderData[0].ModifiedBy) : 0;
                clientRequestHeaderVM.ModifiedDate = clientRequestHeaderData[0].ModifiedDate != null ? clientRequestHeaderData[0].ModifiedDate.ToString() : string.Empty;
                clientRequestHeaderVM.InspectionDate = clientRequestHeaderData[0].JoinDate != null ? Convert.ToDateTime(clientRequestHeaderData[0].JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.JoinDate = clientRequestHeaderData[0].JoinDate != null ? Convert.ToDateTime(clientRequestHeaderData[0].JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.FileNo = clientRequestHeaderData[0].FileNo != null ? clientRequestHeaderData[0].FileNo : string.Empty;
                // clientRequestHeaderVM.PilotPremiumID = clientRequestHeaderData.PilotPremiumID != null ? Convert.ToInt32(clientRequestHeaderData.PilotPremiumID) : 0;
                clientRequestHeaderVM.PilotPremiumID = clientRequestHeaderData[0].PilotPremiumID != null ? Convert.ToInt32(clientRequestHeaderData[0].PilotPremiumID) : 0;
                clientRequestHeaderVM.PartnerID = clientRequestHeaderData[0].PartnerID != null ? Convert.ToInt32(clientRequestHeaderData[0].PartnerID) : 0;
                clientRequestHeaderVM.DeductibleID = clientRequestHeaderData[0].DeductibleID != null ? Convert.ToInt32(clientRequestHeaderData[0].DeductibleID) : 0;
                clientRequestHeaderVM.PolicyStartDate = clientRequestHeaderData[0].PolicyStart != null ? Convert.ToDateTime(clientRequestHeaderData[0].PolicyStart).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.PolicyEndDate = clientRequestHeaderData[0].PolicyEnd != null ? Convert.ToDateTime(clientRequestHeaderData[0].PolicyEnd).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.Occupation = clientRequestHeaderData[0].Occupation;
                clientRequestHeaderVM.Exclusions = string.IsNullOrEmpty(clientRequestHeaderData[0].Exclusions.ToString()) || clientRequestHeaderData == null ? 0 : float.Parse(clientRequestHeaderData[0].Exclusions.ToString());
                clientRequestHeaderVM.OptionalCovers = clientRequestHeaderData[0].OptionalCovers;
                clientRequestHeaderVM.CurrancyID = clientRequestHeaderData[0].CurrancyID == null ? 0 : (int)clientRequestHeaderData[0].CurrancyID;
                clientRequestHeaderVM.FrequncyID = clientRequestHeaderData[0].FrequncyID == null ? 0 : (int)clientRequestHeaderData[0].FrequncyID;
                clientRequestHeaderVM.Exclu = string.IsNullOrEmpty(clientRequestHeaderData[0].Exclu) || clientRequestHeaderData == null ? "Non" : clientRequestHeaderData[0].Exclu.ToString();
                clientRequestHeaderVM.MembershipID = string.IsNullOrEmpty(clientRequestHeaderData[0].MembershipID) || clientRequestHeaderData == null ? "NA" : clientRequestHeaderData[0].MembershipID.ToString();
                clientRequestHeaderVM.SchemeID = clientRequestHeaderData[0].SchemeID == null || clientRequestHeaderData == null ? 0 : (int)clientRequestHeaderData[0].SchemeID;
                clientRequestHeaderVM.GroupID = clientRequestHeaderData[0].GroupID == null || clientRequestHeaderData == null ? "0" : clientRequestHeaderData[0].GroupID;
                clientRequestHeaderVM.FrequncyDID= clientRequestHeaderData[0].FrequncyDID == null || clientRequestHeaderData == null ? "0" : clientRequestHeaderData[0].FrequncyDID;

                return clientRequestHeaderVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public ClientRequestHeaderVM GetPilotRequestByID(int clientRequestHeaderID)
        {
            try
            {
                var clientRequestHeaderData = unitOfWork.TblClientRequestHeaderRepository.GetByID(clientRequestHeaderID);

                ClientRequestHeaderVM clientRequestHeaderVM = new ClientRequestHeaderVM();
                clientRequestHeaderVM.ClientRequestHeaderID = clientRequestHeaderData.ClientRequestHeaderID;
                clientRequestHeaderVM.ClientID = clientRequestHeaderData.ClientID != null ? Convert.ToInt32(clientRequestHeaderData.ClientID) : 0;
                var curancyID = clientRequestHeaderData.CurrancyID == null ? 0 : clientRequestHeaderData.CurrancyID;



                var currancy = unitOfWork.TblCurrencyRepository.GetByID(curancyID);


                clientRequestHeaderVM.CurrancyCode = currancy == null ? "N/A" : currancy.CurrencyCode;



                if (clientRequestHeaderVM.ClientID > 0)
                {
                    //ClientVM clientvm = new ClientVM();
                    clientRequestHeaderVM.ClientName = clientRequestHeaderData.tblClient.ClientName;

                    clientRequestHeaderVM.BusinessUnitID = clientRequestHeaderData.tblClient.BUID != null ? Convert.ToInt32(clientRequestHeaderData.tblClient.BUID) : 0;
                    clientRequestHeaderVM.CurrancyID = (int)curancyID;
                    var FamilyDetailsData = unitOfWork.TblFamilyMemberRepository.Get(x => x.ClientID == clientRequestHeaderData.ClientID).ToList();
                    List<FamilyMembersVM> clienttVM = new List<FamilyMembersVM>();
                    foreach (var family in FamilyDetailsData)
                    {
                        FamilyMembersVM familyDetailsVM = new FamilyMembersVM();
                        familyDetailsVM.FamilyMemberID = family.FamilyMemberID;
                        familyDetailsVM.TitleID = (int)family.Title < 0 ? 0 : (int)family.Title;
                        familyDetailsVM.MemberName = string.IsNullOrEmpty(family.MemberName) ? "" : family.MemberName;
                        familyDetailsVM.MemberOtherName = string.IsNullOrEmpty(family.MemberOtherName) ? "" : family.MemberOtherName;
                        familyDetailsVM.MemberDOB = family.MemberDOB != null ? Convert.ToDateTime(family.MemberDOB).ToString("dd/MM/yyyy") : string.Empty;
                        familyDetailsVM.JoinDate = family.JoinDate != null ? Convert.ToDateTime(family.JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                        familyDetailsVM.NIC = string.IsNullOrEmpty(family.NICNo) ? "" : family.NICNo;
                        familyDetailsVM.ContactNo = string.IsNullOrEmpty(family.ContactNo) ? "" : family.ContactNo;
                        familyDetailsVM.RelationShipID = family.RelationShipID < 0 ? 0 : (int)family.RelationShipID;
                        familyDetailsVM.GenderID = family.GenderID < 0 ? 0 : (int)family.GenderID;
                        familyDetailsVM.MemberCountryID = family.HomeCountry == null ? 0 : (int)family.HomeCountry;
                        familyDetailsVM.MemberResCountryID = family.CountryOfResident == null ? 0 : (int)family.CountryOfResident;

                        clienttVM.Add(familyDetailsVM);
                    }
                    clientRequestHeaderVM.FamilyDetails = clienttVM;

                    var PaymentData = unitOfWork.TblPaymentRepository.Get(x => x.ClientID == clientRequestHeaderData.ClientID).ToList();
                    List<PaymentVM> paymenttVM = new List<PaymentVM>();
                    foreach (var payment in PaymentData)
                    {
                        PaymentVM paymentVM = new PaymentVM();
                        paymentVM.PaymentID = payment.PaymentID;
                        paymentVM.PaymentAmount = payment.PaymentAmount;

                        paymenttVM.Add(paymentVM);
                    }
                    clientRequestHeaderVM.PaymentDetails = paymenttVM;

                    if (clientRequestHeaderVM.BusinessUnitID > 0)
                    {
                        clientRequestHeaderVM.BusinessUnitName = clientRequestHeaderData.tblClient.tblBussinessUnit.BussinessUnit;
                        clientRequestHeaderVM.CompanyID = clientRequestHeaderData.tblClient.tblBussinessUnit.CompID != null ? Convert.ToInt32(clientRequestHeaderData.tblClient.tblBussinessUnit.CompID) : 0;

                        if (clientRequestHeaderVM.CompanyID > 0)
                        {
                            clientRequestHeaderVM.CompanyName = clientRequestHeaderData.tblClient.tblBussinessUnit.tblCompany.CompanyName;
                        }
                    }
                }

                clientRequestHeaderVM.PartnerID = clientRequestHeaderData.PartnerID != null ? Convert.ToInt32(clientRequestHeaderData.PartnerID) : 0;

                if (clientRequestHeaderVM.PartnerID > 0)
                {
                    var PremiumData = unitOfWork.TblPremiumRepository.GetByID(clientRequestHeaderVM.PartnerID);
                    clientRequestHeaderVM.PremiumName = PremiumData.PremiumName;
                }
                clientRequestHeaderVM.AgentID = clientRequestHeaderData.ModifiedBy != null ? Convert.ToInt32(clientRequestHeaderData.ModifiedBy) : 0;

                if (clientRequestHeaderVM.AgentID > 0)
                {
                    var AgentData = unitOfWork.TblAgentRepository.GetByID(clientRequestHeaderVM.AgentID);
                    clientRequestHeaderVM.AgentName = AgentData.AgentName;

                    //clientRequestHeaderVM.PartnerName = clientReqHeader.tblPartner.PartnerName;
                }

                clientRequestHeaderVM.RequestedDate = clientRequestHeaderData.RequestedDate != null ? Convert.ToDateTime(clientRequestHeaderData.RequestedDate).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.IsQuotationCreated = clientRequestHeaderData.IsQuotationCreated != null ? Convert.ToBoolean(clientRequestHeaderData.IsQuotationCreated) : false;
                clientRequestHeaderVM.CreatedBy = clientRequestHeaderData.CreatedBy != null ? Convert.ToInt32(clientRequestHeaderData.CreatedBy) : 0;
                clientRequestHeaderVM.CreatedDate = clientRequestHeaderData.CreatedDate != null ? clientRequestHeaderData.CreatedDate.ToString() : string.Empty;
                clientRequestHeaderVM.ModifiedBy = clientRequestHeaderData.ModifiedBy != null ? Convert.ToInt32(clientRequestHeaderData.ModifiedBy) : 0;
                clientRequestHeaderVM.ModifiedDate = clientRequestHeaderData.ModifiedDate != null ? clientRequestHeaderData.ModifiedDate.ToString() : string.Empty;
                clientRequestHeaderVM.InspectionDate = clientRequestHeaderData.JoinDate != null ? Convert.ToDateTime(clientRequestHeaderData.JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.JoinDate = clientRequestHeaderData.JoinDate != null ? Convert.ToDateTime(clientRequestHeaderData.JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.FileNo = clientRequestHeaderData.FileNo != null ? clientRequestHeaderData.FileNo : string.Empty;
                // clientRequestHeaderVM.PilotPremiumID = clientRequestHeaderData.PilotPremiumID != null ? Convert.ToInt32(clientRequestHeaderData.PilotPremiumID) : 0;
                clientRequestHeaderVM.PilotPremiumID = clientRequestHeaderData.PilotPremiumID != null ? Convert.ToInt32(clientRequestHeaderData.PilotPremiumID) : 0;
                clientRequestHeaderVM.PartnerID = clientRequestHeaderData.PartnerID != null ? Convert.ToInt32(clientRequestHeaderData.PartnerID) : 0;
                clientRequestHeaderVM.DeductibleID = clientRequestHeaderData.DeductibleID != null ? Convert.ToInt32(clientRequestHeaderData.DeductibleID) : 0;
                clientRequestHeaderVM.PolicyStartDate = clientRequestHeaderData.PolicyStart != null ? Convert.ToDateTime(clientRequestHeaderData.PolicyStart).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.PolicyEndDate = clientRequestHeaderData.PolicyEnd != null ? Convert.ToDateTime(clientRequestHeaderData.PolicyEnd).ToString("dd/MM/yyyy") : string.Empty;
                clientRequestHeaderVM.Occupation = clientRequestHeaderData.Occupation;
                clientRequestHeaderVM.Exclusions = string.IsNullOrEmpty(clientRequestHeaderData.Exclusions.ToString()) || clientRequestHeaderData == null ? 0 : float.Parse(clientRequestHeaderData.Exclusions.ToString());
                clientRequestHeaderVM.OptionalCovers = clientRequestHeaderData.OptionalCovers;
                clientRequestHeaderVM.CurrancyID = clientRequestHeaderData.CurrancyID == null ? 0 : (int)clientRequestHeaderData.CurrancyID;
                clientRequestHeaderVM.FrequncyID = clientRequestHeaderData.FrequncyID == null ? 0 : (int)clientRequestHeaderData.FrequncyID;
                clientRequestHeaderVM.Exclu = string.IsNullOrEmpty(clientRequestHeaderData.Exclu) || clientRequestHeaderData == null ? "Non" : clientRequestHeaderData.Exclu.ToString();
                clientRequestHeaderVM.MembershipID = string.IsNullOrEmpty(clientRequestHeaderData.MembershipID) || clientRequestHeaderData == null ? "NA" : clientRequestHeaderData.MembershipID.ToString();
                clientRequestHeaderVM.SchemeID = clientRequestHeaderData.SchemeID == null || clientRequestHeaderData == null ? 0 : (int)clientRequestHeaderData.SchemeID;
                clientRequestHeaderVM.GroupID = clientRequestHeaderData.GroupID == null || clientRequestHeaderData == null ? "0" : clientRequestHeaderData.GroupID;
                return clientRequestHeaderVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<FamilyDiscountVM> GetAllFamilyDiscount()
        {
            try
            {
                var FamilyDiscountData = unitOfWork.TblFamilyDiscountRepository.Get().ToList();

                List<FamilyDiscountVM> FamilyDiscountList = new List<FamilyDiscountVM>();

                foreach (var FamilyDiscount in FamilyDiscountData)
                {
                    FamilyDiscountVM FamilyDiscounts = new FamilyDiscountVM();
                    FamilyDiscounts.FamilyDiscountID = FamilyDiscount.FamilyDiscountID;
                    FamilyDiscounts.FamilyDiscount = FamilyDiscount.FamilyDiscount;
                    FamilyDiscounts.FamilyDiscountVal = FamilyDiscount.FamilyDiscountVal;
                    

                    FamilyDiscountList.Add(FamilyDiscounts);
                }

                return FamilyDiscountList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<PaymentMethodsVM> GetAllPaymentMethods()
        {
            try
            {
                var PaymentMethodsData = unitOfWork.TblPolicyMemberDetailRepository.Get().ToList();

                List<PaymentMethodsVM> PaymentMethodList = new List<PaymentMethodsVM>();

                foreach (var payment in PaymentMethodsData)
                {
                    PaymentMethodsVM Payments = new PaymentMethodsVM();
                    Payments.PolicyMemberID = payment.PolicyMemberID;
                    Payments.PolicyMemberName = payment.PolicyMemberName;


                    PaymentMethodList.Add(Payments);
                }

                return PaymentMethodList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<BankDetailsVM> GetAllBanksByBusinessUnitID(int BusinessUnitID)
        {
            try
            {
                var bankData = unitOfWork.TblBankDetailRepository.Get(x => x.BUID == BusinessUnitID).ToList();

                List<BankDetailsVM> BankList = new List<BankDetailsVM>();

                foreach (var bank in bankData)
                {
                    BankDetailsVM bankDetailsVM = new BankDetailsVM();
                    bankDetailsVM.BankID = bank.BankID;
                    bankDetailsVM.BUID = bank.BUID != null ? Convert.ToInt32(bank.BUID) : 0;
                    bankDetailsVM.BankName = bank.BankName;
                    bankDetailsVM.DiscountRatio = bank.DiscountRatio;


                    BankList.Add(bankDetailsVM);
                }

                return BankList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<DeductionDetailsVM> GetAllDeductionMethods(int BusinessUnitID)
        {
            try
            {
                var customerList = unitOfWork.TblClientRepository.Get(x => x.BUID == BusinessUnitID).ToList();
                //var deductionData = new List<tblDeduction>();
                List<DeductionDetailsVM> BankList = new List<DeductionDetailsVM>();
                foreach (var client in customerList)
                {
                    //deductionData = unitOfWork.TblDeductionRepository.Get(x => x.ClientID == client.ClientID).ToList();

                    DeductionDetailsVM deductionVM = new DeductionDetailsVM();
                     deductionVM.DeductionID = client.ClientID;
                      deductionVM.PremiumHolder = client.ClientName;
                    //    deductionVM.ClientID = deduction.ClientID != null ? Convert.ToInt32(deduction.ClientID) : 0;
                    //    deductionVM.DeductionRate = deduction.Deductibles != null ? Convert.ToInt32(deduction.ClientID) :0;
                    //    deductionVM.FamilyMemberID = deduction.FamilyMemberID != null ? Convert.ToInt32(deduction.FamilyMemberID) : 0;
                    //    deductionVM.GroupFamilyMemberID = deduction.GroupFamilyMemberID != null ? Convert.ToInt32(deduction.GroupFamilyMemberID) : 0;
                    //    deductionVM.LoadingRate = deduction.LodingRate < 0?0: deduction.LodingRate;
                    //    deductionVM.PremiumAmount = deduction.Premium;
                    //    deductionVM.NetPremium = deduction.NetPremium;
                      deductionVM.ClientID = client.ClientID;
                    BankList.Add(deductionVM);
                }


               

                //foreach (var deduction in deductionData)
                //{
                //    DeductionDetailsVM deductionVM = new DeductionDetailsVM();
                //    deductionVM.DeductionID = deduction.DeductionID;
                //    deductionVM.PremiumHolder = deduction.PremiumHolder;
                //    deductionVM.ClientID = deduction.ClientID != null ? Convert.ToInt32(deduction.ClientID) : 0;
                //    deductionVM.DeductionRate = deduction.Deductibles != null ? Convert.ToInt32(deduction.ClientID) :0;
                //    deductionVM.FamilyMemberID = deduction.FamilyMemberID != null ? Convert.ToInt32(deduction.FamilyMemberID) : 0;
                //    deductionVM.GroupFamilyMemberID = deduction.GroupFamilyMemberID != null ? Convert.ToInt32(deduction.GroupFamilyMemberID) : 0;
                //    deductionVM.LoadingRate = deduction.LodingRate < 0?0: deduction.LodingRate;
                //    deductionVM.PremiumAmount = deduction.Premium;
                //    deductionVM.NetPremium = deduction.NetPremium;
                //    deductionVM.ClientID = deduction.ClientID;
                //    deductionVM.PremiumHolderType = deduction.PremiumHolderType < 0 ? 0 : deduction.PremiumHolderType;
                //    deductionVM.JoinDate = deduction.JoinDate != null ? Convert.ToDateTime(deduction.JoinDate).ToString("dd/MM/yyyy") : string.Empty;
                //    deductionVM.Deductible = string.IsNullOrEmpty(deduction.Deductibles) ? "" : deduction.Deductibles;
                //    deductionVM.GroupFamilyMemberID = deduction.GroupFamilyMemberID < 0 ? 0 : deduction.GroupFamilyMemberID;
                //    deductionVM.PremiumID = deduction.PremiumID < 0 ? 0 : deduction.PremiumID;
                    

                //    BankList.Add(deductionVM);
                //}

                return BankList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<DeductionDetailsVM> GetPaymentMethodsid(int id)
        {
            try
            {
                
                var deductionData = new List<tblDeduction>();
                deductionData = unitOfWork.TblDeductionRepository.Get(x => x.DeductionID == id).ToList();
               


                List<DeductionDetailsVM> BankList = new List<DeductionDetailsVM>();

                foreach (var deduction in deductionData)
                {
                    DeductionDetailsVM deductionVM = new DeductionDetailsVM();
                    deductionVM.DeductionID = deduction.DeductionID;
                    deductionVM.PremiumHolder = deduction.PremiumHolder;
                    deductionVM.ClientID = deduction.ClientID != null ? Convert.ToInt32(deduction.ClientID) : 0;
                    deductionVM.Deductible = deduction.Deductibles != null ? deduction.Deductibles : "";
                    deductionVM.DeductionRate = deduction.Deductibles != null ? Convert.ToInt32(deduction.ClientID) : 0;
                    deductionVM.FamilyMemberID = deduction.FamilyMemberID != null ? Convert.ToInt32(deduction.FamilyMemberID) : 0;
                    deductionVM.GroupFamilyMemberID = deduction.GroupFamilyMemberID != null ? Convert.ToInt32(deduction.GroupFamilyMemberID) : 0;
                    BankList.Add(deductionVM);
                }

                return BankList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public List<PaymentTypeVM> GetPaymentMethods(int id)
        {
            try
            {

               
             var   paymethods = unitOfWork.TblPaymentTypeRepository.Get(x => x.BID == id).ToList();

               var PayList = new List<PaymentTypeVM>();



                foreach (var deduction in paymethods)
                {
                    var deductionVM = new PaymentTypeVM();
                    deductionVM.PaymentTypeID= deduction.PaymentTypeID;
                    deductionVM.Description = deduction.Description;
                  
                    PayList.Add(deductionVM);
                }

                return PayList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        public BankDetailsVM GetBankByID(int bankID)
        {
            try
            {
                var bankData = unitOfWork.TblBankDetailRepository.GetByID(bankID);

                BankDetailsVM bankVM = new BankDetailsVM();
                bankVM.BankID = bankData.BankID;
                bankVM.BankName = bankData.BankName;
                bankVM.DiscountRatio= bankData.DiscountRatio;
                bankVM.BankAddress = bankData.BankAddress;
                bankVM.BankCode = bankData.BankCode;
                return bankVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<BankTransactionVM> GetBankDetailsByPolicyID(int PolicyID)
        {
            try
            {
                var bankTrasData = unitOfWork.TblBankTransactionDetailRepository.Get(x => x.PolicyInfoRecID == PolicyID).ToList();

                List <BankTransactionVM> bankDetailsVM = new List<BankTransactionVM>();

                foreach (var bankData in bankTrasData)
                {
                    BankTransactionVM bankVM = new BankTransactionVM();
                    bankVM.BankID = bankData.BankID;
                    bankVM.BankAmount = bankData.Amount;
                    bankVM.DraftNo = bankData.DraftNo;
                    bankVM.PaymentMethodID = bankData.PaymentID;
                    bankVM.AgentID = bankData.AgentID;
                    bankVM.AgentAmount = bankData.AgentAmount;
                    bankVM.ClientID = bankData.ClientID;
                    //   Bank.IBSAmount = policyInfoPaymentVM.SGSAmount;
                    bankVM.RequestDate = bankData.RequestDate != null ? Convert.ToDateTime(bankData.RequestDate).ToString("dd/MM/yyyy") : string.Empty;
                    bankDetailsVM.Add(bankVM);
                }
                return bankDetailsVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<AllYearVM> GetAllYear()
        {
            try
            {
                var YearInfo = unitOfWork.TblYearRepository.Get().ToList();

                var YearList = new List<AllYearVM>();

                foreach (var payment in YearInfo)
                {
                    var Payments = new AllYearVM();
                    Payments.Year =(int) payment.Year;
                    Payments.Desc = payment.Description;


                    YearList.Add(Payments);
                }

                return YearList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<BUPABankTransactionVM> GetBUPABankDetailsByClient(int ClientID)
        {
            try
            {
                var bankTrasData = unitOfWork.TblBankTransactionDetailRepository.Get(x => x.ClientID == ClientID).ToList();

                List<BUPABankTransactionVM> bankDetailsVM = new List<BUPABankTransactionVM>();
                foreach (var bankData in bankTrasData)
                {
                    BUPABankTransactionVM bankVM = new BUPABankTransactionVM();
                    bankVM.BankID = bankData.BankID;
                    bankVM.BankAmount = bankData.Amount;
                    bankVM.BankName = bankData.BankName;
                    bankVM.DraftNo = bankData.DraftNo;
                    bankVM.PaymentMethodID = bankData.PaymentID;
                    bankVM.AgentID = bankData.AgentID;
                    bankVM.AgentAmount = bankData.AgentAmount;
                    bankVM.IBSAmount = bankData.IBSAmount;
                    bankVM.ClientID = bankData.ClientID;
                    bankVM.BankDetailID = bankData.BankDetailID;

                    var  Agent= unitOfWork.TblAgentRepository.Get(x => x.AgentID == bankData.AgentID).ToList();

                    if (Agent != null)
                        bankVM.AgentName= Agent[0].AgentName;
                    else
                        bankVM.AgentName = "NA";

                    var Currancy = unitOfWork.TblCurrencyRepository.Get(x => x.CurrencyID == bankData.currencyType).ToList();
                    if (Currancy != null)
                        bankVM.CurrancyCode = Currancy[0].CurrencyCode;
                    else
                        bankVM.CurrancyCode = "NA";


                    var PayID = bankData.PaymentID == null ? 0 : bankData.PaymentID;

                    var paymode = unitOfWork.TblPaymentTypeRepository.Get(x => x.PaymentTypeID == PayID).ToList();
                    if (paymode.Count>0)
                        bankVM.PayMode= paymode[0].Description;
                    else
                        bankVM.PayMode = "NA";




                    //   Bank.IBSAmount = policyInfoPaymentVM.SGSAmount;
                    bankVM.PaymentDate = bankData.PaymentDate != null ? Convert.ToDateTime(bankData.PaymentDate).ToString("dd/MM/yyyy") : string.Empty;
                    bankVM.Year = bankData.Year.ToString();
                    bankVM.PaiedAmount = bankData.Amount;
                    bankVM.TotalAmount = bankData.TotalAmount;
                    bankVM.SGSAmount = bankData.TotalAmount - bankData.Amount;

                    var freQ = unitOfWork.TblFrequncy.GetByID(int.Parse(bankData.FrequncyID));
                    bankVM.Frequncy = freQ.Name;

                    var freQDeatils = unitOfWork.TblFrequncyDetailRepository.Get(x=>x.FrequncyID==bankData.FrequncyID & x.Code== bankData.FrequncyDID).ToList();
                    bankVM.FrequncyCat = freQDeatils[0].Description;
                    bankDetailsVM.Add(bankVM);
                }
                return bankDetailsVM;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);

                return null;
            }
        }




        public bool SavePayment(ClientRequestHeaderVM paymentVM, int userID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    
                    //Save Debit Note
                    foreach (var debitNoteVM in paymentVM.DebitNoteDetails)
                    {
                        tblDebitNote debitNote = new tblDebitNote();
                        debitNote.TotalNonCommissionPremium = debitNoteVM.TotalNonCommissionPremium;
                        debitNote.TotalGrossPremium = debitNoteVM.TotalGrossPremium;
                        debitNote.CreatedBy = userID;
                        debitNote.CreatedDate = DateTime.Now;
                        unitOfWork.TblDebitNoteRepository.Insert(debitNote);
                        unitOfWork.Save();

                        //Save Policy Info Payments
                        //Save Policy Info Payment - Debit Note
                        //foreach (var policyInfoPaymentObj in debitNoteVM.PolicyInfoPaymentList)
                        //{
                            tblPolicyDebitNote policyDebitNote = new tblPolicyDebitNote();
                            policyDebitNote.PolicyInfoPaymentID = null;
                            policyDebitNote.DebitNoteID = debitNote.DebitNoteID;
                            policyDebitNote.PaymentID = paymentVM.PaymentID;
                            unitOfWork.TblPolicyDebitNoteRepository.Insert(policyDebitNote);
                            unitOfWork.Save();
                       //}
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

        public bool UpdatePayment(ClientRequestHeaderVM paymentVM, int userID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {

                    var policyDebitNoteData = unitOfWork.TblPolicyDebitNoteRepository.Get(x => x.PaymentID == paymentVM.PaymentID).ToList();
                    foreach (var policyDebitNote in policyDebitNoteData)
                    {
                        unitOfWork.TblDebitNoteRepository.Delete(policyDebitNote.DebitNoteID);
                        unitOfWork.Save();
                    }
                    foreach (var policyDebitNote in policyDebitNoteData)
                    {
                        unitOfWork.TblPolicyDebitNoteRepository.Delete(policyDebitNote);
                        unitOfWork.Save();
                    }



                    //Save Debit Note
                    foreach (var debitNoteVM in paymentVM.DebitNoteDetails)
                    {
                        tblDebitNote debitNote = new tblDebitNote();
                        debitNote.TotalNonCommissionPremium = debitNoteVM.TotalNonCommissionPremium;
                        debitNote.TotalGrossPremium = debitNoteVM.TotalGrossPremium;
                        debitNote.CreatedBy = userID;
                        debitNote.CreatedDate = DateTime.Now;
                        unitOfWork.TblDebitNoteRepository.Insert(debitNote);
                        unitOfWork.Save();

                        //Save Policy Info Payments
                        //Save Policy Info Payment - Debit Note
                        //foreach (var policyInfoPaymentObj in debitNoteVM.PolicyInfoPaymentList)
                        //{
                        tblPolicyDebitNote policyDebitNote = new tblPolicyDebitNote();
                        policyDebitNote.PolicyInfoPaymentID = null;
                        policyDebitNote.DebitNoteID = debitNote.DebitNoteID;
                        policyDebitNote.PaymentID = paymentVM.PaymentID;
                        unitOfWork.TblPolicyDebitNoteRepository.Insert(policyDebitNote);
                        unitOfWork.Save();
                        //}
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

        public bool SaveBankTransaction(BankTransactionVM bankVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {

                    if (bankVM.BankDetailID > 0)
                    {
                        tblBankTransactionDetail bank = unitOfWork.TblBankTransactionDetailRepository.GetByID(bankVM.BankDetailID);
                        bank.BankID = bankVM.BankID;
                        bank.DraftNo = bankVM.DraftNo;
                        bank.PaymentID = bankVM.BankRate;
                        bank.Amount = bankVM.BankAmount;
                        bank.AgentID = bankVM.AgentID;
                        bank.AgentAmount = bankVM.AgentAmount;
                        bank.ClientID = bankVM.ClientID;
                        bank.IBSAmount = bankVM.SGSAmount;
                        //Bank.PaymentID = bankVM.PaymentID;
                        bank.RequestDate = !string.IsNullOrEmpty(bankVM.RequestDate) ? DateTime.ParseExact(bankVM.RequestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        unitOfWork.TblBankTransactionDetailRepository.Update(bank);
                        unitOfWork.Save();
                    }
                    else {
                        tblBankTransactionDetail Bank = new tblBankTransactionDetail();
                        Bank.BankID = bankVM.BankID;
                        Bank.DraftNo = bankVM.DraftNo;
                        Bank.PaymentID = bankVM.BankRate;
                        Bank.Amount = bankVM.BankAmount;
                        Bank.AgentID = bankVM.AgentID;
                        Bank.AgentAmount = bankVM.AgentAmount;
                        Bank.ClientID = bankVM.ClientID;
                        Bank.IBSAmount = bankVM.SGSAmount;
                        //Bank.PaymentID = bankVM.PaymentID;
                        Bank.RequestDate = !string.IsNullOrEmpty(bankVM.RequestDate) ? DateTime.ParseExact(bankVM.RequestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        unitOfWork.TblBankTransactionDetailRepository.Insert(Bank);
                        unitOfWork.Save();
                    }
                        
                        //}
                   // }

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

        public bool SaveBUPABankTransaction(BankTransactionVM bankVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                var  client=    unitOfWork.TblClientRepository.GetByID(bankVM.ClientID);
                    var clientHeader = unitOfWork.TblClientRequestHeaderRepository.Get(x => x.ClientID == bankVM.ClientID).ToList();
                    //   var clientHeader = unitOfWork.TblClientRequestHeaderRepository.GetByID(client.ClientID);
                    tblBankTransactionDetail bank = new tblBankTransactionDetail();
                    //if (bankVM.BankDetailID > 0)
                    //{
                    //tblBankTransactionDetail bank = unitOfWork.TblBankTransactionDetailRepository.GetByID(bankVM.BankDetailID);
                        bank.BankID = bankVM == null?0:bankVM.BankID;
                        bank.DraftNo = bankVM.DraftNo;
                        bank.PaymentID = bankVM.PaymentMethodID;
                      //  bank.Amount = bankVM.SGSAmount;
                    bank.Amount = bankVM.IBSAmount;
                    bank.AgentID = clientHeader[0].AgentID==null?0: clientHeader[0].AgentID;
                        bank.AgentAmount = bankVM.AgentAmount;
                        bank.ClientID = bankVM.ClientID;
                        bank.IBSAmount = bankVM.SGSAmount;
                        bank.currencyType = clientHeader[0].CurrancyID == null ? 0 : clientHeader[0].CurrancyID;
                    bank.ExchangeRate = bankVM.ExchangeRate;
                    bank.BankName = bankVM.BankName;
                    //Bank.PaymentID = bankVM.PaymentID;
                    //  bank.RequestDate = !string.IsNullOrEmpty(bankVM.RequestDate) ? DateTime.ParseExact(bankVM.RequestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    bank.RequestDate = DateTime.Now;
                    bank.PaymentDate = !string.IsNullOrEmpty(bankVM.PaymentDate) ? DateTime.ParseExact(bankVM.PaymentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    bank.Year = client.ExtraInt1;
                    bank.Month = string.IsNullOrEmpty(bankVM.PaymentDate) ? 0 : DateTime.ParseExact(bankVM.PaymentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).Month;
                    bank.TotalAmount = bankVM.BalanceAmount;
                    bank.FrequncyID = client.FrequncyID.ToString();
                    bank.FrequncyDID = client.FrequncyDID;

                    unitOfWork.TblBankTransactionDetailRepository.Insert(bank);
                        unitOfWork.Save();
                    //}
                    //else
                    //{
                    //    tblBankTransactionDetail Bank = new tblBankTransactionDetail();
                    //    Bank.BankID = bankVM.BankID;
                    //    Bank.DraftNo = bankVM.DraftNo;
                    //    Bank.PaymentID = bankVM.PaymentMethodID;
                    //    Bank.Amount = bankVM.SGSAmount;
                    //    Bank.AgentID = clientHeader[0].AgentID == null ? 0 : clientHeader[0].AgentID;
                    //    Bank.AgentAmount = bankVM.AgentAmount;
                    //    Bank.ClientID = bankVM.ClientID;
                    //    Bank.IBSAmount = bankVM.IBSAmount;
                    //    Bank.currencyType = clientHeader[0].CurrancyID == null ? 0 : clientHeader[0].CurrancyID;
                    //    //Bank.PaymentID = bankVM.PaymentID;
                    //    Bank.RequestDate = !string.IsNullOrEmpty(bankVM.RequestDate) ? DateTime.ParseExact(bankVM.RequestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    //    unitOfWork.TblBankTransactionDetailRepository.Insert(Bank);
                    //    unitOfWork.Save();
                    //}

                    //}
                    // }

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

        public bool SaveBUPAReimburesmentTransaction(BankTransactionVM bankVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {

                    if (bankVM.BankDetailID > 0)
                    {
                        tblBankTransactionDetail bank = unitOfWork.TblBankTransactionDetailRepository.GetByID(bankVM.BankDetailID);
                        bank.BankID = bankVM.BankID;
                        bank.DraftNo = bankVM.DraftNo;
                        bank.PaymentID = bankVM.PaymentMethodID;
                        bank.Amount = bankVM.SGSAmount;
                        bank.AgentID = bankVM.AgentID;
                        bank.AgentAmount = bankVM.AgentAmount;
                        bank.ClientID = bankVM.ClientID;
                        bank.IBSAmount = bankVM.IBSAmount;
                        //Bank.PaymentID = bankVM.PaymentID;
                        bank.RequestDate = !string.IsNullOrEmpty(bankVM.RequestDate) ? DateTime.ParseExact(bankVM.RequestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        unitOfWork.TblBankTransactionDetailRepository.Update(bank);
                        unitOfWork.Save();
                    }
                    else
                    {
                        tblBankTransactionDetail Bank = new tblBankTransactionDetail();
                        Bank.BankID = bankVM.BankID;
                        Bank.DraftNo = bankVM.DraftNo;
                        Bank.PaymentID = bankVM.PaymentMethodID;
                        Bank.Amount = bankVM.SGSAmount;
                        Bank.AgentID = bankVM.AgentID;
                        Bank.AgentAmount = bankVM.AgentAmount;
                        Bank.ClientID = bankVM.ClientID;
                        Bank.IBSAmount = bankVM.IBSAmount;
                        //Bank.PaymentID = bankVM.PaymentID;
                        Bank.RequestDate = !string.IsNullOrEmpty(bankVM.RequestDate) ? DateTime.ParseExact(bankVM.RequestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        unitOfWork.TblBankTransactionDetailRepository.Insert(Bank);
                        unitOfWork.Save();
                    }

                    //}
                    // }

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
        #endregion

        #region Pilot/Nestle/Aviva
        public bool SaveCustomerRequest(ClientRequestVM clientRequestVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {



                    int clientID = 0;

                    if (clientRequestVM.IsClientAdded == false)
                    {
                        clientRequestVM.IsClientUpdated = true;
                    }

                    if (clientRequestVM.IsClientUpdated)
                    {
                        clientID = clientRequestVM.ClientDetails.ClientID;

                        tblClient client = unitOfWork.TblClientRepository.GetByID(clientID);
                        client.TitleID = clientRequestVM.ClientDetails.TitleID < 0 ? 0 : clientRequestVM.ClientDetails.TitleID;
                        client.ClientName = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientName) ? "" : clientRequestVM.ClientDetails.ClientName;
                        client.ExtraText = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientOtherName) ? "" : clientRequestVM.ClientDetails.ClientOtherName;
                        client.ClientAddress = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientAddress) ? "" : clientRequestVM.ClientDetails.ClientAddress;
                        client.NIC = string.IsNullOrEmpty(clientRequestVM.ClientDetails.NIC) ? "" : clientRequestVM.ClientDetails.NIC;
                        client.ContactNo = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ContactNo) ? "" : clientRequestVM.ClientDetails.ContactNo;
                        client.FixedLine = string.IsNullOrEmpty(clientRequestVM.ClientDetails.FixedLine) ? "" : clientRequestVM.ClientDetails.FixedLine;
                        client.Email = string.IsNullOrEmpty(clientRequestVM.ClientDetails.Email) ? "" : clientRequestVM.ClientDetails.Email;
                        client.DOB = !string.IsNullOrEmpty(clientRequestVM.ClientDetails.DOB) ? DateTime.ParseExact(clientRequestVM.ClientDetails.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;

                        client.PPID = string.IsNullOrEmpty(clientRequestVM.ClientDetails.PPID) ? "" : clientRequestVM.ClientDetails.PPID;
                        client.FamilyDiscount = clientRequestVM.ClientDetails.FamilyDiscount < 0 ? 0 : clientRequestVM.ClientDetails.FamilyDiscount;
                        client.AdditionalNote = string.IsNullOrEmpty(clientRequestVM.ClientDetails.AdditionalNote) ? "" : clientRequestVM.ClientDetails.AdditionalNote;
                        client.HomeCountryID = clientRequestVM.ClientDetails.HomeCountryID < 0 ? 0 : clientRequestVM.ClientDetails.HomeCountryID;
                        client.ResidentCountryID = clientRequestVM.ClientDetails.ResidentCountryID < 0 ? 0 : clientRequestVM.ClientDetails.ResidentCountryID;
                        client.BUID = clientRequestVM.ClientDetails.BusinessUnitID < 0 ? 0 : clientRequestVM.ClientDetails.BusinessUnitID;
                        client.ModifiedBy = clientRequestVM.UserID;
                        client.ModifiedDate = DateTime.Now;
                        client.TitleID = clientRequestVM.TitleID;

                        unitOfWork.TblClientRepository.Update(client);
                        unitOfWork.Save();
                        if (clientRequestVM.ClientDetails.FamilyDetails != null)
                        {
                            foreach (var requestLine in clientRequestVM.ClientDetails.FamilyDetails)
                            {
                                tblFamilyMember tblFamilyDetail = new tblFamilyMember();
                                tblFamilyDetail.ClientID = clientRequestVM.ClientDetails.ClientID;
                                tblFamilyDetail.MemberName = requestLine.MemberName;
                                tblFamilyDetail.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyDetail.NICNo = requestLine.NIC;
                                tblFamilyDetail.ContactNo = requestLine.ContactNo;
                                tblFamilyDetail.JoinDate= !string.IsNullOrEmpty(requestLine.JoinDate) ? DateTime.ParseExact(requestLine.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                //clientRequestLine.CreatedDate = DateTime.Now;
                                tblFamilyDetail.NICNo = !string.IsNullOrEmpty(requestLine.NIC) ? requestLine.NIC : "";
                                tblFamilyDetail.ContactNo = !string.IsNullOrEmpty(requestLine.ContactNo) ? requestLine.ContactNo : "";
                                tblFamilyDetail.HomeCountry = requestLine.MemberCountryID < 0 ? 0 : requestLine.MemberCountryID;
                                tblFamilyDetail.CountryOfResident = requestLine.MemberResCountryID < 0 ? 0 : requestLine.MemberResCountryID;
                                tblFamilyDetail.RelationShipID = requestLine.RelationShipID < 0 ? 0 : requestLine.RelationShipID;
                                tblFamilyDetail.GenderID = requestLine.GenderID < 0 ? 0 : requestLine.GenderID;
                                unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyDetail);
                                unitOfWork.Save();
                            }
                        }
                    }
                    else if (clientRequestVM.IsClientAdded)
                    {
                        //Save Client
                        tblClient client = new tblClient();
                        client.TitleID = clientRequestVM.ClientDetails.TitleID < 0 ? 0 : clientRequestVM.ClientDetails.TitleID;
                        client.ClientName = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientName) ?"": clientRequestVM.ClientDetails.ClientName;
                        client.ExtraText = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientOtherName) ? "": clientRequestVM.ClientDetails.ClientOtherName;
                        client.ClientAddress = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ClientAddress) ? "" : clientRequestVM.ClientDetails.ClientAddress;
                        client.NIC = string.IsNullOrEmpty(clientRequestVM.ClientDetails.NIC) ?"": clientRequestVM.ClientDetails.NIC;
                        client.ContactNo = string.IsNullOrEmpty(clientRequestVM.ClientDetails.ContactNo)?"": clientRequestVM.ClientDetails.ContactNo;
                        client.FixedLine = string.IsNullOrEmpty(clientRequestVM.ClientDetails.FixedLine)?"": clientRequestVM.ClientDetails.FixedLine;
                        client.Email = string.IsNullOrEmpty(clientRequestVM.ClientDetails.Email)?"": clientRequestVM.ClientDetails.Email;
                        client.DOB = !string.IsNullOrEmpty(clientRequestVM.ClientDetails.DOB) ? DateTime.ParseExact(clientRequestVM.ClientDetails.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        client.PPID = string.IsNullOrEmpty(clientRequestVM.ClientDetails.PPID)? "": clientRequestVM.ClientDetails.PPID;
                        client.FamilyDiscount = clientRequestVM.ClientDetails.FamilyDiscount <0?0: clientRequestVM.ClientDetails.FamilyDiscount;
                        client.AdditionalNote = string.IsNullOrEmpty(clientRequestVM.ClientDetails.AdditionalNote)? "": clientRequestVM.ClientDetails.AdditionalNote;
                        client.HomeCountryID = clientRequestVM.ClientDetails.HomeCountryID <0?0: clientRequestVM.ClientDetails.HomeCountryID;
                        client.ResidentCountryID = clientRequestVM.ClientDetails.ResidentCountryID <0? 0: clientRequestVM.ClientDetails.ResidentCountryID;
                        client.BUID = clientRequestVM.ClientDetails.BusinessUnitID<0? 0: clientRequestVM.ClientDetails.BusinessUnitID;
                        client.CreatedBy = clientRequestVM.UserID;
                        client.CreatedDate = DateTime.Now;
                       
                        unitOfWork.TblClientRepository.Insert(client);
                        unitOfWork.Save();




                        clientID = client.ClientID;
                        if (clientRequestVM.ClientDetails.FamilyDetails != null)
                        {
                            foreach (var requestLine in clientRequestVM.ClientDetails.FamilyDetails)
                            {
                                tblFamilyMember tblFamilyDetail = new tblFamilyMember();
                                tblFamilyDetail.Title = requestLine.TitleID < 0 ? 0 : requestLine.TitleID;
                                tblFamilyDetail.ClientID = client.ClientID<0 ?0: client.ClientID;
                                tblFamilyDetail.MemberName = string.IsNullOrEmpty(requestLine.MemberName)?"": requestLine.MemberName;
                                tblFamilyDetail.MemberOtherName = string.IsNullOrEmpty(requestLine.MemberOtherName) ? "" : requestLine.MemberOtherName;
                                tblFamilyDetail.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyDetail.JoinDate = !string.IsNullOrEmpty(requestLine.JoinDate) ? DateTime.ParseExact(requestLine.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyDetail.NICNo = !string.IsNullOrEmpty(requestLine.NIC) ? requestLine.NIC:"";
                                tblFamilyDetail.ContactNo = !string.IsNullOrEmpty(requestLine.ContactNo)? requestLine.ContactNo:"";
                                tblFamilyDetail.HomeCountry = requestLine.MemberCountryID < 0 ? 0 : requestLine.MemberCountryID;
                                tblFamilyDetail.CountryOfResident = requestLine.MemberResCountryID < 0 ? 0 : requestLine.MemberResCountryID;
                                tblFamilyDetail.RelationShipID= requestLine.RelationShipID < 0? 0:requestLine.RelationShipID;
                                tblFamilyDetail.GenderID = requestLine.GenderID < 0 ? 0 : requestLine.GenderID;

                                //clientRequestLine.CreatedDate = DateTime.Now;
                                unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyDetail);
                                unitOfWork.Save();

                                //int famId = tblFamilyDetail.FamilyMemberID;


                                //tblDeduction deduction = new tblDeduction();
                                //deduction.ClientID = clientID;
                                //deduction.LodingRate = clientRequestVM.ClientDetails.LoadnigRate;
                                //deduction.DeductionRate = 0;
                                //deduction.FamilyMemberID = famId;
                                //unitOfWork.TblDeductionRepository.Insert(deduction);
                                //unitOfWork.Save();
                            }

                        }



                    }
                    else
                    {
                        clientID = clientRequestVM.ClientDetails.ClientID;
                    }

                    //Save Client Request Header
                    tblClientRequestHeader clientRequestHeader = new tblClientRequestHeader();
                    clientRequestHeader.ClientID = clientID;
                    clientRequestHeader.PartnerID = clientRequestVM.ClientRequestHeaderDetails.PartnerID;
                    clientRequestHeader.RequestedDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.RequestedDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.RequestedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    clientRequestHeader.CreatedBy = clientRequestVM.UserID;
                    clientRequestHeader.CreatedDate = DateTime.Now;
                    clientRequestHeader.ModifiedBy = clientRequestVM.ClientRequestHeaderDetails.AgentID;
                    clientRequestHeader.InspectionDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.JoinDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    clientRequestHeader.JoinDate = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.JoinDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.JoinDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    clientRequestHeader.AdditionalNote = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.AdditionalNote) ? clientRequestVM.ClientRequestHeaderDetails.AdditionalNote : "";
                    clientRequestHeader.FileNo = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.FileNo) ? clientRequestVM.ClientRequestHeaderDetails.FileNo : "";
                    clientRequestHeader.PilotPremiumID = clientRequestVM.ClientRequestHeaderDetails.PilotPremiumID;
                    clientRequestHeader.PolicyStart = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyStartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    clientRequestHeader.PolicyEnd = !string.IsNullOrEmpty(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate) ? DateTime.ParseExact(clientRequestVM.ClientRequestHeaderDetails.PolicyEndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                    clientRequestHeader.DeductibleID = clientRequestVM.ClientRequestHeaderDetails.DeductibleID;
                    clientRequestHeader.SchemeID = clientRequestVM.ClientRequestHeaderDetails.PartnerID;

                    unitOfWork.TblClientRequestHeaderRepository.Insert(clientRequestHeader);
                    unitOfWork.Save();

                    //tblDeduction deduction = new tblDeduction();
                    //deduction.ClientID = clientID;
                    //deduction.LodingRate = clientRequestVM.ClientDetails.LoadnigRate;
                    //deduction.DeductionRate = clientRequestVM.ClientDetails.DeductionRate;
                    //unitOfWork.TblDeductionRepository.Insert(deduction);
                    //unitOfWork.Save();

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


        public bool UpdateCustomerRequest(ClientRequestHeaderVM clientRequestHeaderVM, bool isClientUpdated, bool isClientAdded, ClientVM clientObj, out string errorMessage)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    int clientID = 0;

                    if (clientRequestHeaderVM.IsQuotationCreated != true)
                    {
                        if (isClientUpdated)
                        {
                            clientID = clientObj.ClientID;

                            //Update Client
                            tblClient client = unitOfWork.TblClientRepository.GetByID(clientObj.ClientID);
                            //client.ClientName = clientObj.ClientName;
                            //client.ClientAddress = clientObj.ClientAddress;
                            //client.NIC = clientObj.NIC;
                            //client.ContactNo = clientObj.ContactNo;
                            //client.FixedLine = clientObj.FixedLine;
                            //client.Email = clientObj.Email;
                            //client.DOB = !string.IsNullOrEmpty(clientObj.DOB) ? DateTime.ParseExact(clientObj.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            //client.PPID = clientObj.PPID;
                            //client.FamilyDiscount = clientObj.FamilyDiscount;
                            //client.AdditionalNote = clientObj.AdditionalNote;
                            //client.HomeCountryID = clientObj.HomeCountryID;
                            //client.ResidentCountryID = clientObj.ResidentCountryID;
                            //client.BUID = clientObj.BusinessUnitID;


                            client.TitleID = clientObj.TitleID < 0 ? 0 : clientObj.TitleID;
                            client.ClientName = string.IsNullOrEmpty(clientObj.ClientName) ? "" :clientObj.ClientName;
                            client.ExtraText = string.IsNullOrEmpty(clientObj.ClientOtherName) ? "" :clientObj.ClientOtherName;
                            client.ClientAddress = string.IsNullOrEmpty(clientObj.ClientAddress) ? "" :clientObj.ClientAddress;
                            client.NIC = string.IsNullOrEmpty(clientObj.NIC) ? "" :clientObj.ClientAddress;
                            client.ContactNo = string.IsNullOrEmpty(clientObj.ContactNo) ? "" :clientObj.ContactNo;
                            client.FixedLine = string.IsNullOrEmpty(clientObj.FixedLine) ? "" :clientObj.FixedLine;
                            client.Email = string.IsNullOrEmpty(clientObj.Email) ? "" :clientObj.Email;
                            client.DOB = !string.IsNullOrEmpty(clientObj.DOB) ? DateTime.ParseExact(clientObj.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            client.PPID = string.IsNullOrEmpty(clientObj.PPID) ? "" :clientObj.PPID;
                            client.FamilyDiscount =clientObj.FamilyDiscount < 0 ? 0 :clientObj.FamilyDiscount;
                            client.AdditionalNote = string.IsNullOrEmpty(clientObj.AdditionalNote) ? "" :clientObj.AdditionalNote;
                            client.HomeCountryID =clientObj.HomeCountryID < 0 ? 0 :clientObj.HomeCountryID;
                            client.ResidentCountryID =clientObj.ResidentCountryID < 0 ? 0 :clientObj.ResidentCountryID;
                            client.BUID =clientObj.BusinessUnitID < 0 ? 0 :clientObj.BusinessUnitID;
                            client.ModifiedBy = clientRequestHeaderVM.ModifiedBy;
                            client.ModifiedDate = DateTime.Now;
                            unitOfWork.TblClientRepository.Update(client);
                            unitOfWork.Save();

                            var familyData = unitOfWork.TblFamilyMemberRepository.Get(x => x.ClientID == clientObj.ClientID).ToList();
                            foreach (var family in familyData)
                            {
                                unitOfWork.TblFamilyMemberRepository.Delete(family);
                                unitOfWork.Save();
                            }

                            foreach (var requestLine in clientRequestHeaderVM.FamilyDetails)
                            {
                                tblFamilyMember tblFamilyMember = new tblFamilyMember();
                                tblFamilyMember.ClientID = clientRequestHeaderVM.ClientID;
                                //tblFamilyMember.MemberName = requestLine.MemberName;
                                //tblFamilyMember.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null; ;
                                //tblFamilyMember.NICNo = requestLine.NIC;
                                //tblFamilyMember.ContactNo = requestLine.ContactNo;
                                //clientRequestLine.CreatedDate = DateTime.Now;
                                
                                //if (requestLine.FamilyMemberID > 0)
                                //{
                                //    tblFamilyMember family = unitOfWork.TblFamilyMemberRepository.GetByID(requestLine.FamilyMemberID);
                                //    family.ClientID = clientRequestHeaderVM.ClientID;
                                //    family.MemberName = requestLine.MemberName;
                                //    family.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null; ;
                                //    //clientRequestLine.CreatedDate = DateTime.Now;
                                //    unitOfWork.TblFamilyMemberRepository.Update(family);
                                //    unitOfWork.Save();
                                //}
                                //else
                                //{

                                // }

                                tblFamilyMember.Title = requestLine.TitleID < 0 ? 0 : requestLine.TitleID;
                                tblFamilyMember.MemberName = string.IsNullOrEmpty(requestLine.MemberName) ? "" : requestLine.MemberName;
                                tblFamilyMember.MemberOtherName = string.IsNullOrEmpty(requestLine.MemberOtherName) ? "" : requestLine.MemberOtherName;
                                tblFamilyMember.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyMember.JoinDate = !string.IsNullOrEmpty(requestLine.InceptionDate) ? DateTime.ParseExact(requestLine.InceptionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyMember.NICNo = !string.IsNullOrEmpty(requestLine.NIC) ? requestLine.NIC : "";
                                tblFamilyMember.ContactNo = !string.IsNullOrEmpty(requestLine.ContactNo) ? requestLine.ContactNo : "";
                                tblFamilyMember.HomeCountry = requestLine.MemberCountryID < 0 ? 0 : requestLine.MemberCountryID;
                                tblFamilyMember.CountryOfResident = requestLine.MemberResCountryID < 0 ? 0 : requestLine.MemberResCountryID;
                                tblFamilyMember.RelationShipID = requestLine.RelationShipID < 0 ? 0 : requestLine.RelationShipID;
                                tblFamilyMember.GenderID = requestLine.GenderID < 0 ? 0 : requestLine.GenderID;

                                unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyMember);
                                unitOfWork.Save();

                            }
                        }
                        else if (isClientAdded)
                        {
                            //Save Client
                            tblClient client = new tblClient();
                            client.TitleID = clientObj.TitleID < 0 ? 0 : clientObj.TitleID;
                            client.ClientName = string.IsNullOrEmpty(clientObj.ClientName) ? "" : clientObj.ClientName;
                            client.ExtraText = string.IsNullOrEmpty(clientObj.ClientOtherName) ? "" : clientObj.ClientOtherName;
                            client.ClientAddress = string.IsNullOrEmpty(clientObj.ClientAddress) ? "" : clientObj.ClientAddress;
                            client.NIC = string.IsNullOrEmpty(clientObj.NIC) ? "" : clientObj.ClientAddress;
                            client.ContactNo = string.IsNullOrEmpty(clientObj.ContactNo) ? "" : clientObj.ContactNo;
                            client.FixedLine = string.IsNullOrEmpty(clientObj.FixedLine) ? "" : clientObj.FixedLine;
                            client.Email = string.IsNullOrEmpty(clientObj.Email) ? "" : clientObj.Email;
                            client.DOB = !string.IsNullOrEmpty(clientObj.DOB) ? DateTime.ParseExact(clientObj.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                            client.PPID = string.IsNullOrEmpty(clientObj.PPID) ? "" : clientObj.PPID;
                            client.FamilyDiscount = clientObj.FamilyDiscount < 0 ? 0 : clientObj.FamilyDiscount;
                            client.AdditionalNote = string.IsNullOrEmpty(clientObj.AdditionalNote) ? "" : clientObj.AdditionalNote;
                            client.HomeCountryID = clientObj.HomeCountryID < 0 ? 0 : clientObj.HomeCountryID;
                            client.ResidentCountryID = clientObj.ResidentCountryID < 0 ? 0 : clientObj.ResidentCountryID;
                            client.BUID = clientObj.BusinessUnitID < 0 ? 0 : clientObj.BusinessUnitID;
                            client.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                            client.CreatedDate = DateTime.Now;
                            unitOfWork.TblClientRepository.Insert(client);
                            unitOfWork.Save();
                            foreach (var requestLine in clientRequestHeaderVM.FamilyDetails)
                            {
                                tblFamilyMember tblFamilyMember = new tblFamilyMember();
                                tblFamilyMember.ClientID = clientRequestHeaderVM.ClientID;
                                tblFamilyMember.Title = requestLine.TitleID < 0 ? 0 : requestLine.TitleID;
                                tblFamilyMember.MemberName = string.IsNullOrEmpty(requestLine.MemberName) ? "" : requestLine.MemberName;
                                tblFamilyMember.MemberOtherName = string.IsNullOrEmpty(requestLine.MemberOtherName) ? "" : requestLine.MemberOtherName;
                                tblFamilyMember.MemberDOB = !string.IsNullOrEmpty(requestLine.MemberDOB) ? DateTime.ParseExact(requestLine.MemberDOB, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyMember.JoinDate = !string.IsNullOrEmpty(requestLine.InceptionDate) ? DateTime.ParseExact(requestLine.InceptionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                                tblFamilyMember.NICNo = !string.IsNullOrEmpty(requestLine.NIC) ? requestLine.NIC : "";
                                tblFamilyMember.ContactNo = !string.IsNullOrEmpty(requestLine.ContactNo) ? requestLine.ContactNo : "";
                                tblFamilyMember.HomeCountry = requestLine.MemberCountryID < 0 ? 0 : requestLine.MemberCountryID;
                                tblFamilyMember.CountryOfResident = requestLine.MemberResCountryID < 0 ? 0 : requestLine.MemberResCountryID;
                                tblFamilyMember.RelationShipID = requestLine.RelationShipID < 0 ? 0 : requestLine.RelationShipID;
                                tblFamilyMember.GenderID = requestLine.GenderID < 0 ? 0 : requestLine.GenderID;
                                unitOfWork.TblFamilyMemberRepository.Insert(tblFamilyMember);
                                unitOfWork.Save();
                            }

                            clientID = client.ClientID;
                        }
                        else
                        {
                            clientID = clientObj.ClientID;
                        }

                        //Update Client Request Header
                        tblClientRequestHeader clientRequestHeader = unitOfWork.TblClientRequestHeaderRepository.GetByID(clientRequestHeaderVM.ClientRequestHeaderID);
                        clientRequestHeader.ClientID = clientRequestHeaderVM.ClientID;
                        clientRequestHeader.PartnerID = clientRequestHeaderVM.PartnerID;
                        clientRequestHeader.RequestedDate = !string.IsNullOrEmpty(clientRequestHeaderVM.RequestedDate) ? DateTime.ParseExact(clientRequestHeaderVM.RequestedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        clientRequestHeader.ModifiedBy = clientRequestHeaderVM.AgentID;
                        clientRequestHeader.ModifiedDate = DateTime.Now;
                        clientRequestHeader.InspectionDate = !string.IsNullOrEmpty(clientRequestHeaderVM.InspectionDate) ? DateTime.ParseExact(clientRequestHeaderVM.InspectionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
                        clientRequestHeader.AdditionalNote = !string.IsNullOrEmpty(clientRequestHeaderVM.FileNo) ? clientRequestHeaderVM.FileNo : "";
                        clientRequestHeader.PilotPremiumID = clientRequestHeaderVM.PilotPremiumID < 0?0: clientRequestHeaderVM.PilotPremiumID;
                        clientRequestHeader.DeductibleID = clientRequestHeaderVM.DeductibleID<0 ?0: clientRequestHeaderVM.DeductibleID;

                        unitOfWork.TblClientRequestHeaderRepository.Update(clientRequestHeader);
                        unitOfWork.Save();

                        if (clientObj.DeductionDetails.Count > 0)
                        {
                            foreach (var paymentLine in clientObj.DeductionDetails)
                            {
                                if (paymentLine.DeductionID > 0)
                                {

                                    tblDeduction deduction = new tblDeduction();

                                    deduction.ClientID = clientID;
                                    deduction.FamilyMemberID = paymentLine.FamilyMemberID < 0 ? 0 : paymentLine.FamilyMemberID;
                                    deduction.PremiumHolderType = paymentLine.PremiumHolderType < 0 ? 0 : paymentLine.PremiumHolderType;
                                    deduction.PremiumHolder = string.IsNullOrEmpty(paymentLine.PremiumHolder) ? "" : paymentLine.PremiumHolder;
                                    deduction.LodingRate = paymentLine.LoadingRate < 0 ? 0 : paymentLine.LoadingRate;
                                    deduction.DeductionRate = paymentLine.DeductionRate < 0 ? 0 : paymentLine.DeductionRate;
                                    deduction.Premium = paymentLine.PremiumAmount < 0 ? 0 : paymentLine.PremiumAmount;
                                    deduction.NetPremium = paymentLine.NetPremium < 0 ? 0 : paymentLine.NetPremium;
                                    deduction.Deductibles =string.IsNullOrEmpty(paymentLine.Deductible)? "": paymentLine.Deductible;
                                    deduction.GroupFamilyMemberID = paymentLine.GroupFamilyMemberID < 0 ? 0 : paymentLine.GroupFamilyMemberID;
                                    deduction.PremiumID = paymentLine.PremiumID < 0 ? 0 : paymentLine.PremiumID;
                                    deduction.DeductionAmount = 0;
                                    deduction.Exclusion = "";
                                    deduction.BusinessUnit = clientObj.BusinessUnitID;
                                    deduction.DeductionRemarks = "";
                                    deduction.OptionalAmount = "0";
                                    deduction.LoadingAmount = 0;
                                    deduction.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                                    deduction.CreatedDate = DateTime.Now;
                                    unitOfWork.TblDeductionRepository.Insert(deduction);
                                    unitOfWork.Save();
                                }
                                else
                                {
                                    tblDeduction deduction = unitOfWork.TblDeductionRepository.GetByID(clientObj.DeductionID);

                                    deduction.ClientID = clientID;
                                    deduction.FamilyMemberID = paymentLine.FamilyMemberID < 0 ? 0 : paymentLine.FamilyMemberID;
                                    deduction.PremiumHolder = string.IsNullOrEmpty(paymentLine.PremiumHolder) ? "" : paymentLine.PremiumHolder;
                                    deduction.LodingRate = paymentLine.LoadingRate < 0 ? 0 : paymentLine.LoadingRate;
                                    deduction.DeductionRate = paymentLine.DeductionRate < 0 ? 0 : paymentLine.DeductionRate;
                                    deduction.Premium = paymentLine.PremiumAmount < 0 ? 0 : paymentLine.PremiumAmount;
                                    deduction.NetPremium = paymentLine.NetPremium < 0 ? 0 : paymentLine.NetPremium;
                                    deduction.Deductibles = "";
                                    deduction.GroupFamilyMemberID = paymentLine.GroupFamilyMemberID < 0 ? 0 : paymentLine.GroupFamilyMemberID;
                                    deduction.PremiumID = 0;
                                    deduction.DeductionAmount = 0;
                                    deduction.Exclusion = "";
                                    deduction.BusinessUnit = clientObj.BusinessUnitID;
                                    deduction.DeductionRemarks = "";
                                    deduction.OptionalAmount = "0";
                                    deduction.LoadingAmount = 0;
                                    deduction.ModifiedBy = clientRequestHeaderVM.ModifiedBy;
                                    deduction.ModifiedDate = DateTime.Now;
                                    unitOfWork.TblDeductionRepository.Update(deduction);
                                    unitOfWork.Save();
                                }
                            }
                        }

                        //Update Deduction
                        //else
                        //{
                        //    tblDeduction deductions = unitOfWork.TblDeductionRepository.GetByID(clientObj.DeductionID);
                        //    deductions.ClientID = clientID;
                        //    deductions.LodingRate = clientObj.LoadnigRate;
                        //    deductions.DeductionRate = clientObj.DeductionRate;
                        //    unitOfWork.TblDeductionRepository.Update(deductions);
                        //    unitOfWork.Save();
                        //}


                        //Save Payment
                        if (clientRequestHeaderVM.PaymentID > 0)
                        {
                            tblPayment payments = unitOfWork.TblPaymentRepository.GetByID(clientRequestHeaderVM.PaymentID);
                            payments.ClientID = clientRequestHeaderVM.ClientID;
                            payments.PaymentAmount = clientRequestHeaderVM.PaymentAmount;
                            payments.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                            payments.CreatedDate = DateTime.Now;
                            unitOfWork.TblPaymentRepository.Update(payments);
                            unitOfWork.Save();
                        }
                        else
                        {
                            tblPayment payment = new tblPayment();
                            payment.ClientID = clientRequestHeaderVM.ClientID;
                            payment.PaymentAmount = clientRequestHeaderVM.PaymentAmount;
                            payment.CreatedBy = clientRequestHeaderVM.ModifiedBy;
                            payment.CreatedDate = DateTime.Now;
                            unitOfWork.TblPaymentRepository.Insert(payment);
                            unitOfWork.Save();
                        }


                        //Complete the Transaction
                        dbTransaction.Commit();

                        errorMessage = "No Error";
                        return true;
                    }
                    else
                    {
                        errorMessage = "Quotations are created based on this request. Therefore it cannot be modified";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();

                    errorMessage = "Update Failed";
                    return false;
                }
            }
        }




        public string  getAgentAccount(string  AgnetCode)
        {
            
                try
                {

                    
                        var   Agent = unitOfWork.TblAgentRepository.GetByID(AgnetCode);
                       
                 

                    //}
                    // }

                    //Complete the Transaction
                 
                    return Agent.Account;
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                   
                    return null;
                }
            
        }

        public string getBUDAccount(string BUDCode)
        {

            try
            {


                var BUD = unitOfWork.TblBussinessUnitRepository.GetByID(BUDCode);



                //}
                // }

                //Complete the Transaction

                return BUD.Account;
            }
            catch (Exception ex)
            {
                //Roll back the Transaction

                return null;
            }

        }


        public string getERPDB(string BUDCode)
        {

            try
            {


                var BUD = unitOfWork.TblBussinessUnitRepository.GetByID(BUDCode);



                //}
                // }

                //Complete the Transaction

                return BUD.ERPDB;
            }
            catch (Exception ex)
            {
                //Roll back the Transaction

                return null;
            }

        }

        #endregion




        public List<FrequncyDetailsVM> GetloadFrequncyData(string fequncyID)
        {
            try
            {
                var freinfo = unitOfWork.TblFrequncyDetailRepository.Get(x=>x.FrequncyID == fequncyID).ToList();

                var frelist = new List<FrequncyDetailsVM>();

                foreach (var payment in freinfo)
                {
                    var  flist = new FrequncyDetailsVM();
                    flist.RowID = payment.RowID;
                    flist.FrequncyID = payment.FrequncyID;
                    flist.Code = payment.Code;
                    flist.Description = payment.Description;
                    frelist.Add(flist);
                }

                return frelist;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<BupaPremiumClientVM> GetAllBupaClients()
        {
            try
            {
                PERFECTIBSEntities context = new PERFECTIBSEntities();
                var clientData = context.spGetAllBupaPremiumClient();

                List<BupaPremiumClientVM> clientList = new List<BupaPremiumClientVM>();

                foreach (var client in clientData)
                {
                    BupaPremiumClientVM clientVM = new BupaPremiumClientVM();
                    clientVM.ClientID = client.ClientID;
                    clientVM.ClientName = client.ClientName;
                    clientVM.ClientAddress = client.ClientAddress;
                    clientVM.NIC = client.NIC;
                    clientVM.ContactNo = client.ContactNo;
                    clientVM.Deductibles = client.Deductibles;
                    clientVM.DeductionAmount = client.DeductionAmount;
                    clientVM.DOB = client.DOB;
                    clientVM.Email = client.Email;
                    clientVM.Exclu = client.Exclu;
                    clientVM.ExtraInt1 = client.ExtraInt1;
                    clientVM.ExtraText = client.ExtraText;
                    clientVM.LoadingAmount = client.LoadingAmount;
                    clientVM.MembershipID = client.MembershipID;
                    clientVM.NetPremium = client.NetPremium;
                    clientVM.Occupation = client.Occupation;
                    clientVM.OptionalAmount = client.OptionalAmount;
                    clientVM.OptionalCovers= client.OptionalCovers;
                    clientVM.Premium = client.Premium;
                    clientList.Add(clientVM);
                }

                return clientList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public object GetNewClientList(FilterMapper filterMapper)
        {
            using (var db = new PERFECTIBSEntities())
            {
                try
                {
                    var oldClients = db.tblClientRewenelHistories.Select(s => s.ClientID).ToList();
                    var qry = (from q in db.tblClients
                               join crh in db.tblClientRequestHeaders on q.ClientID equals crh.ClientID 
                               where !oldClients.Contains(q.ClientID)
                               && q.CreatedDate >= filterMapper.fromDate && q.CreatedDate <= filterMapper.toDate
                               //&& q.BUID == 6
                               select new NewClientListMapper
                               {
                                   ClientAddress = q.ClientAddress.Trim(),
                                   ClientId = q.ClientID,
                                   ClientName = q.ClientName.Trim(),
                                   Email = q.Email.Trim(),
                                   NIC = q.NIC.Trim(),
                                   PhoneNo = q.ContactNo,
                                   PolicyStartDate = crh.PolicyStart,
                                   PolicyEndDate = crh.PolicyEnd,
                               }).ToList();
                    return qry;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        public bool ChangeActiveStatus(ChangeActiveStatusMapper mapper)
        {
            using (var db = new PERFECTIBSEntities())
            {
                try
                {
                    var request = db.tblClientRequestHeaders.Where(r => r.ClientRequestHeaderID == mapper.RequestId).FirstOrDefault();
                    request.IsActive = mapper.IsActive;
                    request.InActiveEffectiveDate = mapper.EffectiveDate;
                    request.ModifiedBy = mapper.UserId;
                    request.ModifiedDate = System.DateTime.Now;
                    db.Entry(request).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        
        public object GetCancelledRequest(FilterMapper filterMapper)
        {
            using (var db = new PERFECTIBSEntities())
            {
                var qry = (from q in db.tblClientRequestHeaders
                           join cl in db.tblClients on q.ClientID equals cl.ClientID
                           join us in db.tblUsers on q.CreatedBy equals us.UserID
                           where q.IsActive == false
                           && q.RequestedDate >= filterMapper.fromDate && q.RequestedDate <= filterMapper.toDate
                           select new
                           {
                               ClientId = q.ClientID,
                               ClientName = cl.ClientName,
                               NIC = cl.NIC.Trim(),
                               PhoneNo = cl.ContactNo,
                               Email = cl.Email.Trim(),
                               ClientAddress = cl.ClientAddress.Trim(),
                               CreatedUser = us.UserName,
                               InActiveDate = q.InActiveEffectiveDate,
                               PolicyStartDate = q.PolicyStart,
                               PolicyEndDate = q.PolicyEnd,

                           }).ToList();
                return qry;
            }
        }

    }
}
