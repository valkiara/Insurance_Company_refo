using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public  class ReceiveQuotationVM
    {
        public int QuotationHeaderId { get; set; }
        public int CompanyId { get; set; }
        public int ClassId { get; set; }
        public int SubClassId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public DateTime ReceivedDate{ get; set; }
        public string ReceivedUser { get; set; }
    }
}
