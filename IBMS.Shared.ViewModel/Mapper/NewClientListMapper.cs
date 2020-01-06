using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel.Mapper
{
    public class NewClientListMapper
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string PhoneNo { get; set; }
        public string ClientAddress { get; set; }
        public string NIC { get; set ; }
        public string Email { get; set; }
        public DateTime? PolicyStartDate { get; set; }
        public DateTime? PolicyEndDate { get; set; }
    }
}