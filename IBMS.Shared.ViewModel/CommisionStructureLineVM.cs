using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel
{
    public class CommisionStructureLineVM
    {
        public int CommisionStructureLineID { get; set; }
        public int CommisionStructureID { get; set; }
        public string CommisionStructureName { get; set; }
        public int RateCategoryID { get; set; }
        public string RateCategoryName { get; set; }
        public bool IsAgeConsider { get; set; }
        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
        public bool IsFixed { get; set; }
        public double RateValue { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
