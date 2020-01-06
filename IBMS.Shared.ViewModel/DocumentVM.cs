using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class DocumentVM
    {
        public int DocumentID { get; set; }
        public int InsuranceSubClassID { get; set; }
        public string InsuranceSubClassName { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
