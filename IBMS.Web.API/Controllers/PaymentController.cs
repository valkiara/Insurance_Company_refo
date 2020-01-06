using IBMS.Service.TransactionData;
using IBMS.Shared.ViewModel;
using IBMS.Web.API.Security;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using SAPbobsCOM;

namespace IBMS.Web.API.Controllers
{
    [BasicAuthentication]
    public class PaymentController : ApiController
    {
        ManagePayment managePayment = new ManagePayment();

        [HttpPost()]
        [ActionName("SavePayment")]
        public IHttpActionResult SavePayment([FromBody]JObject data)
        {
            try
            {
                PaymentVM paymentVM = data.SelectToken("PaymentObj").ToObject<PaymentVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                bool status = managePayment.SavePayment(paymentVM, userID);

                if (status)
                {
                  //  AddInvoicetoERP(paymentVM);
                    return Json(new { status = true, message = "Successfully Saved" });
                }
                else
                {
                    return Json(new { status = false, message = "Save Failed" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("UpdatePayment")]
        public IHttpActionResult UpdatePayment([FromBody]JObject data)
        {
            try
            {
                PaymentVM paymentVM = data.SelectToken("PaymentObj").ToObject<PaymentVM>();
                int userID = !string.IsNullOrEmpty(data.SelectToken("UserID").Value<string>()) ? Convert.ToInt32(data.SelectToken("UserID").Value<string>()) : 0;

                bool status = managePayment.UpdatePayment(paymentVM, userID);

                if (status)
                {
                    return Json(new { status = true, message = "Successfully Updated" });
                }
                else
                {
                    return Json(new { status = false, message = "Update Failed" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllPayments")]
        public IHttpActionResult GetAllPayments()
        {
            try
            {
                var paymentList = managePayment.GetAllPayments();
                return Json(new
                {
                    status = true,
                    data = paymentList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetAllPaymentsByBUID")]
        public IHttpActionResult GetAllPaymentsByBUID([FromBody]JObject data)
        {
            try
            {
                int businessUnitID = !string.IsNullOrEmpty(data.SelectToken("BusinessUnitID").Value<string>()) ? Convert.ToInt32(data.SelectToken("BusinessUnitID").Value<string>()) : 0;
                var paymentList = managePayment.GetAllPaymentsByBusinessUnitID(businessUnitID);
                return Json(new
                {
                    status = true,
                    data = paymentList
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        [HttpPost()]
        [ActionName("GetPaymentByID")]
        public IHttpActionResult GetPaymentByID([FromBody]JObject data)
        {
            try
            {
                int paymentID = !string.IsNullOrEmpty(data.SelectToken("PaymentID").Value<string>()) ? Convert.ToInt32(data.SelectToken("PaymentID").Value<string>()) : 0;
                var payment = managePayment.GetPaymentByID(paymentID);
                return Json(new
                {
                    status = true,
                    data = payment
                });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Unknown error occurred" });
            }
        }

        public int AddInvoicetoERP(PaymentVM paymentVM)
        {
            
            DalLogUserEvents _oDalLogUser = new DalLogUserEvents();
            LogUserEvents _oLogUser = new LogUserEvents();
            int tblID = Convert.ToInt32(ConfigurationSettings.AppSettings["PURCHASEORDERMASTERTBLID"].ToString());
            _oLogUser.TableID = tblID.ToString();
            _oLogUser.DocumentID = "SAP-Invoice Transfer Start";
            _oLogUser.CreateDate = DateTime.Now;
            _oLogUser.UpdateDate = DateTime.Now;
            _oLogUser.CreateUser = 1;
            _oLogUser.UpdateUser = 1;

            try
            {

                //DataSet ds = new DataSet();
                //PurchaseOrderLayer oPurchaseLayer = new PurchaseOrderLayer();
                //ds = oPurchaseLayer.GetPOInterDB(1, "");

                // if (ds.Tables[0].Rows.Count > 0)
                //   MessageBox.Show(" Header data");


                string Type = "";
                int Count = 0;
                //PurchaseOrderLayer orderservice = new PurchaseOrderLayer();
                int lRetCode = -1;
                SAPObjects.oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2014;
                //SAPObjects.oCompany.Server = @"" + _objPropertyCon.Data_Source;
                //SAPObjects.oCompany.UserName = _objPropertyCon.User_ID;
                //SAPObjects.oCompany.Password = _objPropertyCon.Password;
                SAPObjects.oCompany.language = SAPbobsCOM.BoSuppLangs.ln_English;
                SAPObjects.oCompany.UseTrusted = false;
                // SAPObjects.oCompany.CompanyDB = _objPropertyCon.Initial_Catalog;
                SAPObjects.oCompany.DbUserName = ConfigurationSettings.AppSettings["ERPSERVERDB_USER"].ToString();
                SAPObjects.oCompany.DbPassword = ConfigurationSettings.AppSettings["ERPSERVERDB_PASS"].ToString();
                SAPObjects.oCompany.LicenseServer = ConfigurationSettings.AppSettings["ERPSERVERDB_LICENSE"].ToString();
                //OrderApp.oCompany.DbPassword = "Admin1234";
                //OrderApp.oCompany.LicenseServer = "Win8";

                //   MessageBox.Show(ConfigurationSettings.AppSettings["ERPSERVERDB_LICENSE"].ToString());

                lRetCode = SAPObjects.oCompany.Connect();
                string str = SAPObjects.oCompany.GetLastErrorDescription();

                // MessageBox.Show(str);


                if (lRetCode != 0)
                {
                    //Error("Can't Connect to ERP  " + str);

                    _oLogUser.Messages = "FAILED PO TRANSFER ON CONNECT - " + str;
                    _oDalLogUser.SaveTransactionLog(_oLogUser);

                    return lRetCode;

                    // MessageBox.Show(" FAILED PO TRANSFER ON CONNECT");
                }
                else
                {
                    SAPObjects.oCompany.StartTransaction();


                    //START SQL TRANSACTION 
                    int AllTransfered = 1;

                    //Read PURCHASE Header Data

                    //  MessageBox.Show(" Susscced PO TRANSFER ON CONNECT");
                    foreach (var debitNoteVM in paymentVM.DebitNoteList)
                    {
                        SAPObjects.oPurchaseOrder = (SAPbobsCOM.Documents)SAPObjects.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);//define sap business object type

                        SAPObjects.oPurchaseOrder.CardCode = paymentVM.ClientID.ToString();
                        SAPObjects.oPurchaseOrder.CardName = paymentVM.ClientName.ToString();
                        SAPObjects.oPurchaseOrder.BPChannelCode = debitNoteVM.DebitNoteID.ToString();
                        SAPObjects.oPurchaseOrder.NumAtCard = debitNoteVM.TotalGrossPremium.ToString();
                        SAPObjects.oPurchaseOrder.DocDate = Convert.ToDateTime(debitNoteVM.CreatedDate);
                        SAPObjects.oPurchaseOrder.TaxDate = Convert.ToDateTime(debitNoteVM.ModifiedDate);
                        //SAPObjects.oPurchaseOrder.DocDueDate = Convert.ToDateTime(row["DueDate"].ToString());
                        SAPObjects.oPurchaseOrder.Reference1 = "SFA";
                        SAPObjects.oPurchaseOrder.Comments = "SFA";
                        
                        DalLogUserEvents _oDalLogUserPO = new DalLogUserEvents();
                        LogUserEvents _oLogUserPO = new LogUserEvents();

                        _oLogUserPO.TableID = tblID.ToString();
                        //_oLogUserPO.DocumentID = row["POID"].ToString();
                        _oLogUserPO.CreateDate = DateTime.Now;
                        _oLogUserPO.UpdateDate = DateTime.Now;
                        _oLogUserPO.CreateUser = 1;
                        _oLogUserPO.UpdateUser = 1;


                        //GET  PURCHASE DETAILS LINES


                        //DataSet dsl = new DataSet();
                        //dsl = oPurchaseLayer.GetPOInterDB(2, row["POID"].ToString());
                        //if (dsl.Tables[0].Rows.Count > 0)
                        //    //     MessageBox.Show(" details ");
                        //    foreach (DataRow rowD in dsl.Tables[0].Rows)
                        //    {
                        //        double price = 0.00;
                        //        double qty = 0;

                        //        SAPObjects.oPurchaseOrder.Lines.ItemCode = rowD["ItemCode"].ToString();
                        //        // MessageBox.Show(rowD["ItemCode"].ToString());
                        //        DataSet _ds = new ProductMasterLayer().GetProductWareHouse(rowD["ItemCode"].ToString());

                        //        string wareHouse = _ds.Tables[0].Rows[0]["DfltWH"].ToString();
                        //        //  MessageBox.Show(wareHouse);
                        //        if (string.IsNullOrEmpty(wareHouse))
                        //        {
                        //            //MessageBox.Show(rowD["ItemCode"].ToString() + "don't have warehouse");
                        //            return 0;
                        //        }


                        //        SAPObjects.oPurchaseOrder.Lines.ItemDescription = rowD["ItemDescription"].ToString();

                        //        price = double.Parse(rowD["Price"].ToString());
                        //        qty = double.Parse(rowD["Qty"].ToString());

                        //        SAPObjects.oPurchaseOrder.Lines.Quantity = Convert.ToDouble(rowD["Qty"].ToString());
                        //        SAPObjects.oPurchaseOrder.Lines.Price = double.Parse(rowD["Price"].ToString());

                        //        //  SAPObjects.oPurchaseOrder.Lines.WarehouseCode = rowD["WarehouseCode"].ToString();
                        //        SAPObjects.oPurchaseOrder.Lines.WarehouseCode = wareHouse;
                        //        //SAPObjects.oPurchaseOrder.Lines.DiscountPercent = Convert.ToDouble(rowD["LineDiscount"].ToString());

                        //        SAPObjects.oPurchaseOrder.Lines.DiscountPercent = 0.00;
                        //        SAPObjects.oPurchaseOrder.Lines.TaxCode = rowD["TaxCode"].ToString() == "" ? "EXEMPT" : rowD["TaxCode"].ToString();
                        //        SAPObjects.oPurchaseOrder.Lines.LineTotal = price * qty;
                        //        SAPObjects.oPurchaseOrder.Lines.Add();
                        //    }

                        //MessageBox.Show(" Line ok ");
                        lRetCode = SAPObjects.oPurchaseOrder.Add();
                        // MessageBox.Show(lRetCode.ToString());
                        string strError = SAPObjects.oCompany.GetLastErrorDescription();
                        // MessageBox.Show(strError);

                        if (lRetCode != 0)
                        {
                            _oLogUserPO.Messages = "SAGE TRANSFER FAIL - " + lRetCode.ToString() + " - " + strError;
                            _oDalLogUserPO.SaveTransactionLog(_oLogUserPO);

                            //ADD EVENT LOG TO TABLE
                            //var _POERPMsg = new PurchaseOrderERPMsg();

                            //_POERPMsg.POID = row["POID"].ToString();
                            //_POERPMsg.SAPReference = lRetCode.ToString();
                            //_POERPMsg.ErrorMsg = strError;
                            //_POERPMsg.CreateDate = DateTime.Now;
                            ////_POERPMsg.UpdateDate="";
                            //_POERPMsg.CreateUser = 1;//change to dynamic value is a must
                            //_POERPMsg.UpdateUser=;

                            //PurchaseOrderLayer orderservice = new PurchaseOrderLayer();
                            //int lRetLog = orderservice.SaveERPPOTransactionLog(_POERPMsg, 1, tblID);//1 = INSERT OF A NEW RECORD


                            int lErrCode = 0;
                            string sErrMsg = "";

                            int temp_int = lErrCode;
                            string temp_string = sErrMsg;
                            SAPObjects.oCompany.GetLastError(out temp_int, out temp_string);
                            if (lErrCode != -4006)
                            {
                                SAPObjects.oCompany.Disconnect();
                            }
                            //NOT ALL POs going to save user must check error log
                            AllTransfered = 0;
                        }
                        else
                        {
                            _oLogUserPO.Messages = "SAGE TRANSFER OK";
                            _oDalLogUserPO.SaveTransactionLog(_oLogUserPO);

                            //PurchaseOrderLayer oOrderService = new PurchaseOrderLayer();
                            //POHeader OH = new POHeader();
                            //OH.IsSendtoERP = Convert.ToInt32(TransactionStatusEnum.Proceed);
                            //OH.POID = row["POID"].ToString();
                            //int sDbStatus = oOrderService.UpdatePOHeader(OH);

                        }

                    }
                    SAPObjects.oCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                    SAPObjects.oCompany.Disconnect();
                    return AllTransfered;
                }
            }
            catch (Exception ex)
            {

                _oLogUser.Messages = "PO TRANSFER FAILED - " + ex.Message.ToString() + " - " + ex.ToString();
                _oDalLogUser.SaveTransactionLog(_oLogUser);


                return 0;
            }
        }
    }
}
