using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IBMS.Service.Errorlog
{
   public class ErrHandler
    {
        public static void WriteError(string errorMessage)
        {
            try
            {
                string path = "~/Error/" + DateTime.Today.ToString("dd-MM-yyyy") + ".txt";
                if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
                }
                using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    w.WriteLine("\r\nLog Entry : ");
                    w.WriteLine("{0}", DateTime.Now);
                    string err = "Error in: " + System.Web.HttpContext.Current.Request.Url + ".";
                    w.WriteLine(err);
                    err = "Error Message:" + errorMessage;
                    w.WriteLine(err);
                    w.WriteLine("__________________________");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                //WriteError(ex.Message);
            }

        }
    }
}
