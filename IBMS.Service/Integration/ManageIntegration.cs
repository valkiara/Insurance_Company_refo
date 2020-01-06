using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBMS.Shared.ViewModel.Mapper;
using System.Data.Entity;
using System.Transactions;

namespace IBMS.Service.Integration
{
    public class ManageIntegration
    {

        public AmountMapper GetBupaAmount(FilterMapper map)
        {
            try
            {
                using (var db = new PERFECTIBSEntities())
                {
                    var custCode = (from b in db.tblBussinessUnits
                                    where b.BussinessUnit == "BUPA"
                                    select new
                                    {
                                        b.ERPCustomerCode,
                                    }).FirstOrDefault();

                    int[] ids = db.tblBankTransactionDetails.Where(t => t.IsInvoiceTrf == 0).Select(t => t.BankDetailID).ToArray();

                    var qry = (from tran in db.tblBankTransactionDetails
                               join cur in db.tblCurrencies on tran.currencyType equals cur.CurrencyID
                               where tran.IsInvoiceTrf == 0 && tran.PaymentDate >= map.fromDate && tran.PaymentDate <= map.toDate
                               group tran by new { cur.CurrencyName, tran.currencyType } into gr
                               select new AmountDetailMapper
                               {
                                   CurrencyType = gr.Key.CurrencyName,
                                   Amount = gr.Sum(s => s.IBSAmount),
                                   CurrencyRate = 0,
                                   AgentCode = custCode.ERPCustomerCode,
                               }).AsQueryable();

                    qry = qry.Where(q => q.Amount > 0).AsQueryable();
                    if (map != null)
                    {
                        if (map.type != null)
                            if (map.type != "0")
                                qry = qry.Where(q => q.CurrencyType == map.type).AsQueryable();
                    }

                    AmountMapper mapper = new AmountMapper();
                    mapper.Ids = ids;
                    mapper.AmountDetailMappers = new List<AmountDetailMapper>();
                    mapper.AmountDetailMappers.AddRange(qry.ToList());
                    return mapper;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool Insert(IntegrationViewModel integrationViewModel)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (var db = new PERFECTIBSEntities())
                    {
                        var transactions = new List<tblTransactionDetail>();
                        foreach (AmountInfo model in integrationViewModel.amountInfo)
                        {
                            var transaction = new tblTransactionDetail();
                            transaction.CurrencyCode = model.CurrencyType;
                            transaction.Amount = model.Amount;
                            transaction.PaymentDate = integrationViewModel.paymentDate;
                            transaction.Rate = model.CurrencyRate;
                            transaction.Type = integrationViewModel.type;
                            transaction.AgentCode = model.AgentCode;
                            transaction.IsTransfer = false;
                            transactions.Add(transaction);


                        }
                        db.tblTransactionDetails.AddRange(transactions);
                        db.SaveChanges();
                        bool isUpdated = false;
                        if (integrationViewModel.type.Trim() == "BupaInvoice")
                        {
                            isUpdated = UpdateBupaInvoiceFlag(integrationViewModel.ids);
                        }
                        else if (integrationViewModel.type.Trim() == "AgentCommssion")
                        {
                            isUpdated = UpdateBupaAmountFlag(integrationViewModel.ids);
                        }
                        if (isUpdated) scope.Complete();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateBupaInvoiceFlag(int[] ids)
        {
            try
            {
                using (var db = new PERFECTIBSEntities())
                {
                    string id = string.Empty;
                    for (int i = 0; i < ids.Length; i++)
                    {
                        id += ids[i] + ",";
                    }

                    id = id.Remove(id.Length - 1);
                    string qry = $"UPDATE tblBankTransactionDetails SET IsInvoiceTrf = 1 WHERE BankDetailID IN ({id})";

                    var query = db.Database.ExecuteSqlCommand(qry);

                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateBupaAmountFlag(int[] ids)
        {
            try
            {
                using (var db = new PERFECTIBSEntities())
                {
                    string id = string.Empty;
                    for (int i = 0; i < ids.Length; i++)
                    {
                        id += ids[i] + ",";
                    }
                    id = id.Remove(id.Length - 1);
                    string qry = $"UPDATE tblBankTransactionDetails SET IsAgentTrf = 1 WHERE BankDetailID IN ({id})";
                    var query = db.Database.ExecuteSqlCommand(qry);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object GetBUPACommissionByDate(FilterMapper filterMapper)
        {
            try
            {

                using (var db = new PERFECTIBSEntities())
                {
                    var qry = (from tran in db.tblBankTransactionDetails
                               join cur in db.tblCurrencies on tran.currencyType equals cur.CurrencyID
                               join ag in db.tblAgents on tran.AgentID equals ag.AgentID
                               join cl in db.tblClients on tran.ClientID equals cl.ClientID
                               where tran.IsInvoiceTrf == 0
                               && tran.PaymentDate >= filterMapper.fromDate && tran.PaymentDate <= filterMapper.toDate
                               group tran by new { cur.CurrencyName, tran.currencyType, ag.AgentCode, tran.PaymentDate, cl.ClientName } into gr
                               select new CommissionDetailMapper
                               {
                                   AgentCode = gr.Key.AgentCode,
                                   ClientName = gr.Key.ClientName,
                                   PaidDate = gr.Key.PaymentDate,
                                   CurrencyType = gr.Key.CurrencyName,
                                   Amount = gr.Sum(s => s.AgentAmount),
                                   CurrencyRate = 0,
                               }).ToList();
                    return qry;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<AgenCommissionMapper> GetBUPACommissionDetailByDate(FilterMapper filterMapper)
        {
            try
            {
                using (var db = new PERFECTIBSEntities())
                {
                    var qry = (from tran in db.tblBankTransactionDetails
                               join cur in db.tblCurrencies on tran.currencyType equals cur.CurrencyID
                               join ag in db.tblAgents on tran.AgentID equals ag.AgentID
                               join cl in db.tblClients on tran.ClientID equals cl.ClientID
                               where tran.IsInvoiceTrf == 0
                               && tran.PaymentDate >= filterMapper.fromDate && tran.PaymentDate <= filterMapper.toDate
                               select new AgenCommissionMapper
                               {
                                   AgentCode = ag.AgentCode.Trim(),
                                   PaymentDate = tran.PaymentDate,
                                   Amount = tran.Amount,
                                   CurrencyType = cur.CurrencyName,
                                   CurrencyRate = 0,
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
