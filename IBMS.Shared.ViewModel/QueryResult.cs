using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
   public class QueryResult
    {
        public int RetID { get; set; }
        public int LastID { get; set; }
        public float LastResult { get; set; }
        public string LastResultString { get; set; }
        public string RetText { get; set; }
    }
}
