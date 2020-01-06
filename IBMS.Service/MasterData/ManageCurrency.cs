using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBMS.Shared.ViewModel.Mapper;
using IBMS.Repository;
namespace IBMS.Service.MasterData
{
    public class ManageCurrency
    {
        public List<CurrencyTypeMapper> GetCurrency()
        {
            using (PERFECTIBSEntities context = new PERFECTIBSEntities())
            {
                var qry = (from q in context.tblCurrencies
                           select new CurrencyTypeMapper
                           {
                               CurrencyCode = q.CurrencyCode,
                               CurrencyName = q.CurrencyName
                           }).ToList();
                return qry;
            }
        }
    }
}
