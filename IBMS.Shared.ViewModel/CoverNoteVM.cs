using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class CoverNoteVM
    {
        public int CoverNoteID { get; set; }
        public int QuotationHeaderID { get; set; }
        public int InsuranceSubClassID { get; set; }
        public string InsuranceSubClassName { get; set; }
        public string CoverNoteNo { get; set; }
        public string ConfirmationMethod { get; set; }
        public string PendingDocItem { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string IssuedDate { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }
}
