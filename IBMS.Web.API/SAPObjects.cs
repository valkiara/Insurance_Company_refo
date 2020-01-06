using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SAPbobsCOM;

namespace IBMS.Web.API
{
    sealed class SAPObjects
    {
        public static DataTable TableLines; // the datasource of the datagrid called DataLines.
        public static SAPbobsCOM.Documents oPurchaseOrder; //Purchase Order object
        public static SAPbobsCOM.Documents oHOReturn; //HO Return object
        //public static SAPbobsCOM.Documents oJE; // Order object
        public static SAPbobsCOM.Documents oInvoice; // Invoice Object
        //public static SAPbobsCOM.Recordset oRecordSet; // A recordset object
        //public static SAPbobsCOM.Company oCompany = new Company(); // The company object
        public static SAPbobsCOM.BoDataServerTypes oBoDataServerTypes = BoDataServerTypes.dst_MSSQL2014;
        public static SAPbobsCOM.Company oCompany = new Company(); // The company object
    }
}