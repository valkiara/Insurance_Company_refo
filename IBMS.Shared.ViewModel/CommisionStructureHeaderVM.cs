using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class CommisionStructureHeaderVM
    {
        public int CommisionStructureID { get; set; }
        public string CommisionStructureName { get; set; }
        public int BusinessUnitID { get; set; }
        public string BusinessUnitName { get; set; }
        public int PartnerID { get; set; }
        public string PartnerName { get; set; }
        public int InsuranceCompanyID { get; set; }
        public string InsuranceCompanyName { get; set; }
        public int InsuranceClassID { get; set; }
        public string InsuranceClassName { get; set; }
        public int InsuranceSubClassID { get; set; }
        public string InsuranceSubClassName { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public int CommisionStructureLineID { get; set; }
        public int RateCategoryID { get; set; }
        public string RateCategoryName { get; set; }
        public bool IsAgeConsider { get; set; }
        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
        public bool IsFixed { get; set; }
        public double RateValue { get; set; }
        public string LineCreatedDate { get; set; }
        public string LineModifiedDate { get; set; }
        public int LineCreatedBy { get; set; }
        public int LineModifiedBy { get; set; }

    }
}
