using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class EndorsementVM
    {
        public int DebitNoteID { get; set; }
        public int PolicyInfoRecID { get; set; }
        public string PolicyNumber { get; set; }
        public decimal NewSumInsured { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        
        //TESTING
    }
}
