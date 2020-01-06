using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Shared.ViewModel.Mapper
{
    public class ChangeActiveStatusMapper
    {
        public int RequestId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int UserId { get; set; }
    }
}
