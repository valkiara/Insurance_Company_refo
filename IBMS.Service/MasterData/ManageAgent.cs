using IBMS.Repository;
using IBMS.Shared.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBMS.Shared.ViewModel.Mapper;

namespace IBMS.Service.MasterData
{
    public class ManageAgent
    {
        private UnitOfWork unitOfWork;
        public ManageAgent()
        {
            unitOfWork = new UnitOfWork();
        }

        public bool SaveAgent(AgentVM agentVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblAgent agent = new tblAgent();
                    agent.CompID = agentVM.CompanyID;
                    agent.AgentName = agentVM.AgentName;
                    agent.Address1 = agentVM.Address1;
                    agent.Address2 = agentVM.Address2;
                    agent.Address3 = agentVM.Address3;
                    agent.RateValue = agentVM.RateValue;
                    agent.CreatedDate = DateTime.Now;
                    agent.CreatedBy = agentVM.CreatedBy;
                    agent.AgentCode = agentVM.AgentCode;
                    agent.AgentType = agentVM.AgentType;
                    agent.AgentNIC = agentVM.AgentNIC;
                    agent.AgentBRNo = agentVM.AgentBR;
                    unitOfWork.TblAgentRepository.Insert(agent);
                    unitOfWork.Save();

                    //Complete the Transaction
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }

        public bool UpdateAgent(AgentVM agentVM)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblAgent agent = unitOfWork.TblAgentRepository.GetByID(agentVM.AgentID);
                    agent.CompID = agentVM.CompanyID;
                    agent.AgentName = agentVM.AgentName;
                    agent.Address1 = agentVM.Address1;
                    agent.Address2 = agentVM.Address2;
                    agent.Address3 = agentVM.Address3;
                    agent.RateValue = agentVM.RateValue;
                    agent.ModifiedDate = DateTime.Now;
                    agent.ModifiedBy = agentVM.ModifiedBy;
                    agent.AgentType = agentVM.AgentType;
                    agent.AgentNIC = agentVM.AgentNIC;
                    agent.AgentBRNo = agentVM.AgentBR;
                    agent.AgentCode = agentVM.AgentCode;
                    unitOfWork.TblAgentRepository.Update(agent);
                    unitOfWork.Save();

                    //Complete the Transaction
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }

        public bool DeleteAgent(int agentID)
        {
            using (var dbTransaction = unitOfWork.dbContext.Database.BeginTransaction())
            {
                try
                {
                    tblAgent agent = unitOfWork.TblAgentRepository.GetByID(agentID);
                    unitOfWork.TblAgentRepository.Delete(agent);
                    unitOfWork.Save();

                    //Complete the Transaction
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //Roll back the Transaction
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }

        public List<AgentVM> GetAllAgents()
        {
            try
            {
                var agentData = unitOfWork.TblAgentRepository.Get().ToList();

                List<AgentVM> agentList = new List<AgentVM>();

                foreach (var agent in agentData)
                {
                    AgentVM agentVM = new AgentVM();
                    agentVM.AgentID = agent.AgentID;
                    agentVM.CompanyID = agent.CompID != null ? Convert.ToInt32(agent.CompID) : 0;

                    if (agentVM.CompanyID > 0)
                    {
                        agentVM.CompanyName = agent.tblCompany.CompanyName;
                    }

                    agentVM.AgentName = agent.AgentName;
                    agentVM.Address1 = agent.Address1;
                    agentVM.Address2 = agent.Address2;
                    agentVM.Address3 = agent.Address3;
                    agentVM.RateValue = agent.RateValue != null ? Convert.ToDouble(agent.RateValue) : 0;
                    agentVM.CreatedDate = agent.CreatedDate != null ? agent.CreatedDate.ToString() : string.Empty;
                    agentVM.ModifiedDate = agent.ModifiedDate != null ? agent.ModifiedDate.ToString() : string.Empty;
                    agentVM.CreatedBy = agent.CreatedBy != null ? Convert.ToInt32(agent.CreatedBy) : 0;
                    agentVM.ModifiedBy = agent.ModifiedBy != null ? Convert.ToInt32(agent.ModifiedBy) : 0;
                    agentVM.AgentType = agent.AgentType;
                    agentVM.AgentNIC = agent.AgentNIC;
                    agentVM.AgentBR = agent.AgentBRNo;
                    agentVM.AgentCode = agent.AgentCode;
                    agentList.Add(agentVM);
                }

                return agentList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<AgentVM> GetAgentsByCompanyID(int companyID)
        {
            try
            {
                var agentData = unitOfWork.TblAgentRepository.Get(x => x.tblCompany.CompID == companyID).ToList();

                List<AgentVM> agentList = new List<AgentVM>();

                foreach (var agent in agentData)
                {
                    AgentVM agentVM = new AgentVM();
                    agentVM.AgentID = agent.AgentID;
                    agentVM.CompanyID = agent.CompID != null ? Convert.ToInt32(agent.CompID) : 0;

                    if (agentVM.CompanyID > 0)
                    {
                        agentVM.CompanyName = agent.tblCompany.CompanyName;
                    }

                    agentVM.AgentName = agent.AgentName;
                    agentVM.Address1 = agent.Address1;
                    agentVM.Address2 = agent.Address2;
                    agentVM.Address3 = agent.Address3;
                    agentVM.RateValue = agent.RateValue != null ? Convert.ToDouble(agent.RateValue) : 0;
                    agentVM.CreatedDate = agent.CreatedDate != null ? agent.CreatedDate.ToString() : string.Empty;
                    agentVM.ModifiedDate = agent.ModifiedDate != null ? agent.ModifiedDate.ToString() : string.Empty;
                    agentVM.CreatedBy = agent.CreatedBy != null ? Convert.ToInt32(agent.CreatedBy) : 0;
                    agentVM.ModifiedBy = agent.ModifiedBy != null ? Convert.ToInt32(agent.ModifiedBy) : 0;
                    agentVM.AgentType = agent.AgentType;
                    agentVM.AgentNIC = agent.AgentNIC;
                    agentVM.AgentBR = agent.AgentBRNo;
                    agentVM.AgentCode = agent.AgentCode;
                    agentList.Add(agentVM);
                }

                return agentList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public AgentVM GetAgentByID(int agentID)
        {
            try
            {
                var agentData = unitOfWork.TblAgentRepository.GetByID(agentID);

                AgentVM agentVM = new AgentVM();
                agentVM.AgentID = agentData.AgentID;
                agentVM.CompanyID = agentData.CompID != null ? Convert.ToInt32(agentData.CompID) : 0;

                if (agentVM.CompanyID > 0)
                {
                    agentVM.CompanyName = agentData.tblCompany.CompanyName;
                }

                agentVM.AgentName = agentData.AgentName;
                agentVM.Address1 = agentData.Address1;
                agentVM.Address2 = agentData.Address2;
                agentVM.Address3 = agentData.Address3;
                agentVM.RateValue = agentData.RateValue != null ? Convert.ToDouble(agentData.RateValue) : 0;
                agentVM.CreatedDate = agentData.CreatedDate != null ? agentData.CreatedDate.ToString() : string.Empty;
                agentVM.ModifiedDate = agentData.ModifiedDate != null ? agentData.ModifiedDate.ToString() : string.Empty;
                agentVM.CreatedBy = agentData.CreatedBy != null ? Convert.ToInt32(agentData.CreatedBy) : 0;
                agentVM.ModifiedBy = agentData.ModifiedBy != null ? Convert.ToInt32(agentData.ModifiedBy) : 0;
                agentVM.AgentType = agentData.AgentType;
                agentVM.AgentNIC = agentData.AgentNIC;
                agentVM.AgentBR = agentData.AgentBRNo;
                agentVM.AgentCode = agentData.AgentCode;
                return agentVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsAgentAvailable(int? agentID, string agentName)
        {
            try
            {
                if (agentID != null && unitOfWork.TblAgentRepository.Get().Any(x => x.AgentName.ToLower() == agentName.ToLower() && x.AgentID != agentID))
                {
                    return true;
                }
                else if (agentID == null && unitOfWork.TblAgentRepository.Get().Any(x => x.AgentName.ToLower() == agentName.ToLower()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public object GetAgentCommission(FilterMapper map)
        {
            try
            {
                AgenCommissionRootMapper mapper = new AgenCommissionRootMapper();
                using (var db = new PERFECTIBSEntities())
                {
                    mapper.Ids = db.tblBankTransactionDetails.Where(t => t.IsAgentTrf == 0).Select(t => t.BankDetailID).ToArray();


                    var qry = (from tran in db.tblBankTransactionDetails
                               join cur in db.tblCurrencies on tran.currencyType equals cur.CurrencyID
                               join ag in db.tblAgents on tran.AgentID equals ag.AgentID
                               where tran.IsAgentTrf == 0 && tran.PaymentDate >= map.fromDate && tran.PaymentDate <= map.toDate
                           //    group tran by new { cur.CurrencyName, tran.currencyType, ag.AgentCode } into gr
                               select new AgenCommissionMapper
                               {
                                   //CurrencyType = gr.Key.CurrencyName,
                                   //Amount = gr.Sum(s => s.AgentAmount),
                                   //CurrencyRate = 0,
                                   //AgentCode = gr.Key.AgentCode
                                   PaymentDate = tran.PaymentDate,
                                   CurrencyType = cur.CurrencyName,
                                   Amount = tran.AgentAmount,
                                   CurrencyRate = 0,
                                   AgentCode = ag.AgentCode,
                      
                               }).AsQueryable();

                    qry = qry.Where(q => q.Amount > 0).AsQueryable();
                    if (map.type != null)
                    if (map.type != "0")
                    {
                            qry = qry.Where(q => q.CurrencyType == map.type).AsQueryable();
                    }
                    mapper.AgenCommissionMappers = new List<AgenCommissionMapper>();

                    mapper.AgenCommissionMappers.AddRange(qry.ToList());
                    return mapper;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public object GetAgentCommissionByDate(FilterMapper filterMapper)
        {
            try
            {                
                using (var db = new PERFECTIBSEntities())
                {
                    var qry = (from tran in db.tblBankTransactionDetails
                               join cur in db.tblCurrencies on tran.currencyType equals cur.CurrencyID
                               join ag in db.tblAgents on tran.AgentID equals ag.AgentID
                               join cl in db.tblClients on tran.ClientID equals cl.ClientID
                               where tran.IsAgentTrf == 0
                               && tran.PaymentDate >= filterMapper.fromDate && tran.PaymentDate <= filterMapper.toDate
                               //group tran by new { cur.CurrencyName, tran.currencyType, ag.AgentCode,tran.PaymentDate, cl.ClientName} into gr
                               select new CommissionDetailMapper
                               {
                                   //CurrencyType = gr.Key.CurrencyName,
                                   //Amount = gr.Sum(s => s.AgentAmount),
                                   //CurrencyRate = 0,
                                   //AgentCode = gr.Key.AgentCode,
                                   //PaidDate = gr.Key.PaymentDate,
                                   //ClientName = gr.Key.ClientName,
                                   CurrencyType = cur.CurrencyName,
                                   Amount = tran.AgentAmount,
                                   CurrencyRate = 0,
                                   AgentCode = ag.AgentCode,
                                   PaidDate = tran.PaymentDate,
                                   ClientName = cl.ClientName
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
