using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class CauseOfLossVM
    {
        public int CauseOfLossID { get; set; }
        public string CauseOfLoss { get; set; }
        public int InsClassID { get; set; }
        public string InsClassCode { get; set; }
        public int InsSubClassID { get; set; }
        public string InsSubClassDescription { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }
}
