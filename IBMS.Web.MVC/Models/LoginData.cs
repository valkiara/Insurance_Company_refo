using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBMS.Web.MVC.Models
{
    public class LoginData
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }
        public int DesignationID { get; set; }
        public string DesignationName { get; set; }
        public int AccessLevelType { get; set; }
        public string AccessLevelTypeName { get; set; }
        public int BusinessUnitID { get; set; }
        public string BusinessUnitName { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public DateTime LastLogin { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<int> AllowedFunctionList { get; set; }
        public int MasterCount { get; set; }
        public int TransactionCount { get; set; }
        public int EnquiriesCount { get; set; }
        public int ReportsCount { get; set; }
        public int AdministrationCount { get; set; }
    }
}