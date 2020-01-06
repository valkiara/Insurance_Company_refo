using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBMS.Shared.ViewModel;
using IBMS.Shared.ViewModel.Mapper;
using IBMS.Repository;

namespace IBMS.Service.TransactionData
{
    public class ManageRenewal
    {
        public IEnumerable<RenewalListMapper> GetClientExpireInfo(FilterMapper filterMapper)
        {
            try
            {
                using (var db = new PERFECTIBSEntities())
                {
                    var qry = (from crh in db.tblClientRequestHeaders
                               join c in db.tblClients on crh.ClientID equals c.ClientID
                               where filterMapper.fromDate <= crh.PolicyEnd && filterMapper.toDate >= crh.PolicyEnd
                               select new RenewalListMapper
                               {
                                   ClientId = c.ClientID,
                                   ClientName = c.ClientName,
                                   ContactNo = c.ContactNo,
                                   Address = c.ClientAddress,
                                   ExpirationDate = crh.PolicyEnd,
                               }).ToList();
                    return qry;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
