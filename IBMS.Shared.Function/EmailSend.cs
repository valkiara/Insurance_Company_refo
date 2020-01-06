using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IBMS.Shared.Function
{
    public class EmailSend
    {
        SmtpClient MailClient;
        public EmailSend()
        {
            //CREDENTIALS
            string strMailServer = CommonResources.EMAIL_SERVER.Trim();
            string strMailPort = CommonResources.EMAIL_SERVER_PORT.Trim();
            string strMailServerUser = CommonResources.EMAIL_SERVER_USERNAME.Trim();
            string strMailServerPassword = CommonResources.EMAIL_SERVER_PASSWORD.Trim();
            string strAdminEmail = CommonResources.EMAIL_ADMIN_EMAIL.Trim();
            bool sslEnable = Convert.ToBoolean(CommonResources.EMAIL_ENABLE_SSL);

            MailClient = new SmtpClient(strMailServer, int.Parse(strMailPort));
            MailClient.Credentials = new System.Net.NetworkCredential(strMailServerUser, strMailServerPassword);
            MailClient.EnableSsl = sslEnable;
            MailClient.Timeout = 100000;
        }

        public bool SendHTMLEmail(string toEmail, string unm, string body, string emailTitle)
        {
            MailAddress mFrom = new MailAddress(CommonResources.EMAIL_ADMIN_EMAIL.Trim().ToString(), CommonResources.EMAIL_ADMIN_NAME.ToString());
            MailAddress mTo = new MailAddress(toEmail, unm);
            MailMessage msg = new MailMessage(mFrom, mTo);

            msg.Subject = string.Format(emailTitle);
            msg.IsBodyHtml = true;
            msg.Body = body;
            msg.Priority = MailPriority.High;
          //  msg.Attachments.Add(new Attachment("D:\\myfile.txt"));

            try
            {
                MailClient.Send(msg);
                return true;
            }
            catch (Exception EX)
            {
                return false;
            }
        }

        public string TemplateReader(string type)
        {
            string encode = "";
            string original = "";
            string html = "";
            string url = "";

            try
            {
                switch (type)
                {
                    case "generalEmail":
                        url = "\\EmailTemplates\\general_email_send.html";
                        break;
                }

                html = File.ReadAllText(HttpContext.Current.Server.MapPath(url));
                encode = WebUtility.HtmlEncode(html);
                original = WebUtility.HtmlDecode(encode);
            }
            catch (Exception EX)
            {

            }

            return original;
        }
    }
}
