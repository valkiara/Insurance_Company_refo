using System.Configuration;
using System.IO;

namespace IBMS.Web.API
{
    /// <summary>
    /// Developed By: Dhammika Devarathne
    /// Edited By: Dhammika Devarathne
    /// Edited Date: 15 Nov 2013
    /// Version: 1.0
    /// </summary>
    public class DBProvider
    {
        public static string strServer;
        public static int intTmp;

        public DBProvider()
        {  
            
            strServer = ConfigurationManager.AppSettings["Server"].ToString();
            
            //strLocalLoadingDB = sr.ReadLine().Trim();
        }

        public static string IntermediateConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["IntermediateDBConnection"].ToString();
                //return "Data Source=" + strServer + "; Initial Catalog=" + strLocalDB + "; User ID=" + strUid + "; Password=" + strPw + ";Connection Timeout=0;Max Pool Size=500";
            }
        }

       

        public static string GetIP()
        {
            string strHostName = "";
            //string strTmp = "";
            strHostName = System.Net.Dns.GetHostName();
            //IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
            //IPAddress[] addr = ipEntry.AddressList;
            //strTmp = addr[addr.Length - 3].ToString();
            //strTmp = addr[addr.Length - 2].ToString();
            //strTmp = addr[addr.Length - 1].ToString();
            return strHostName;
        }
    }
}
