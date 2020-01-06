using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class AgentVM
    {
        public int AgentID { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string AgentName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public double RateValue { get; set; }
        public string AgentType { get; set; }
        public string AgentNIC { get; set; }
        public string AgentBR { get; set; }

        public string AgentCode { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        //GGUIGIGIUGIU
    }
}
