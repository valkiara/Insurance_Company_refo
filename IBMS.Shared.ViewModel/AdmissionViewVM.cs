using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class AdmissionViewVM
    {
        public string ReferenceNo { get; set; }
        public string PatientName { get; set; }
        public string Hospital { get; set; }
        public string PassportNumber { get; set; }
        public string BHTNumber { get; set; }
        public DateTime AdmissionDate { get; set; }
    }
}
