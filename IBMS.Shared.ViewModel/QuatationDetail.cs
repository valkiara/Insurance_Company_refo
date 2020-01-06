using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class QuatationDetail
    {
        public int? InsClassID { get; set; }
        public int? InsSubClassID { get; set; }
        public int? CompID { get; set; }
        public bool? IsRequested { get; set; }
        public DateTime? RequestedDate { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public bool? IsReceived { get; set; }
        public DateTime? ReceivedDate { get; set; }

    }
}
