using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IBMS.Shared.ViewModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace IBMS.Web.API
{
    public class DalLogUserEvents
    {
        public static readonly object DocLock = new object();
        private static DalLogUserEvents _instance;

        public static DalLogUserEvents GetInstance
        {
            get
            {
                lock (DocLock)
                {
                    return _instance ?? (_instance = new DalLogUserEvents());
                }
            }
        }

        public int SaveTransactionLog(LogUserEvents _oUserLog)
        {
            SqlConnection sqlConnLogUser = new SqlConnection();

            sqlConnLogUser.ConnectionString = ConfigurationManager.ConnectionStrings["IntermediateDBConnection"].ToString(); ;
            sqlConnLogUser.Open();
            try
            {
                SqlTransaction sqlTransLog = sqlConnLogUser.BeginTransaction();

                QueryResult queryResult = new QueryResult();

                SqlParameter[] parm = new SqlParameter[8];
                parm[0] = new SqlParameter("@TableID", SqlDbType.Int) { Value = _oUserLog.TableID };//1 =Insert New Record
                parm[1] = new SqlParameter("@DocumentID", SqlDbType.VarChar, 32) { Value = _oUserLog.DocumentID };
                parm[2] = new SqlParameter("@Messages", SqlDbType.VarChar, 2000) { Value = _oUserLog.Messages };
                parm[3] = new SqlParameter("@CreateDate", SqlDbType.DateTime, 32) { Value = _oUserLog.CreateDate };
                parm[4] = new SqlParameter("@UpdateDate", SqlDbType.DateTime, 32) { Value = _oUserLog.UpdateDate };
                parm[5] = new SqlParameter("@CreateUser", SqlDbType.Int, 32) { Value = _oUserLog.CreateUser };
                parm[6] = new SqlParameter("@UpdateUser", SqlDbType.Int, 32) { Value = _oUserLog.UpdateUser };
                parm[7] = new SqlParameter("@RetID", SqlDbType.Int) { Direction = ParameterDirection.Output };

                SqlCommand sqlcommand = new SqlCommand();

                SqlHelper.ExecuteNonQuery(DBProvider.IntermediateConnectionString, CommandType.StoredProcedure, "InterDBLogUserEvents", parm);
                sqlTransLog.Commit();
                sqlConnLogUser.Close();

                return queryResult.RetID;
            }
            catch (Exception ex)
            {
                sqlConnLogUser.Close();
                return 0;
            }

        }
    }
}