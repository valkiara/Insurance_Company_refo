using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class RoleVM
    {
        public int AccessLevelTypeID { get; set; }
        public string AccessLevelTypeName { get; set; }
        public List<FunctionVM> MasterFunctionDetails { get; set; }
        public List<FunctionVM> TransactionFunctionDetails { get; set; }
        public List<FunctionVM> EnquiryFunctionDetails { get; set; }
        public List<FunctionVM> ReportFunctionDetails { get; set; }
        public List<FunctionVM> AdminFunctionDetails { get; set; }
    }

    public class FunctionVM
    {
        public int FunctionID { get; set; }
        public int FunctionNumber { get; set; }
        public string FunctionName { get; set; }
        public bool IsChecked { get; set; }
    }
}
