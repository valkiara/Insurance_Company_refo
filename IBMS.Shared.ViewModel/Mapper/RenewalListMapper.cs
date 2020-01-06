using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel.Mapper
{
    public class RenewalListMapper
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
