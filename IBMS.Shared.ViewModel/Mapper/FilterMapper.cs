using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel.Mapper
{
    public class FilterMapper
    {
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string type { get; set; }
    }
}
