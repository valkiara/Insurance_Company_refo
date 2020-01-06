using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class QuotationLineView
    {
        public int QuotationHeaderID { get; set; }
        public int InsClassID { get; set; }
        public int InsSubClassID { get; set; }
        public int CompID { get; set; }
        public bool IsRequested { get; set; }
        public DateTime RequestedDate { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }

    }
}
