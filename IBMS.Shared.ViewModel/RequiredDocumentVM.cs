using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class RequiredDocumentVM
    {
        public int RequiredDocID { get; set; }
        public int InsuranceClassID { get; set; }
        public string InsuranceClassName { get; set; }
        public int InsuranceSubClassID { get; set; }
        public string InsuranceSubClassName { get; set; }
        public int DocCategoryID { get; set; }
        public string DocCategoryName { get; set; }
        public string DocumentName { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
