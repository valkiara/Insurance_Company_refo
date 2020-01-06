using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class TaxTypeVM
    {
        public int TaxTypeID { get; set; }
        public string TaxTypeCode { get; set; }
        public string Description { get; set; }
        public double Percentage { get; set; }
        public string ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }
}
