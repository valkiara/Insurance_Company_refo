using IBMS.Shared.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMS.Service.TransactionData
{
    public class ManageEmail
    {
        public bool SendGeneralEmail(string emailAddress, string userName, string emailHeader, string emailContent)
        {
            try
            {
                var emailHelper = new EmailSend();
                var emailBody = emailHelper.TemplateReader("generalEmail");
                emailBody = emailBody.Replace("@emailHeader", emailHeader.ToString());
                emailBody = emailBody.Replace("@emailContent", emailContent.ToString());

                bool status = emailHelper.SendHTMLEmail(emailAddress, userName, emailBody, "General Email");

                return status;
            }
            catch (Exception EX)
            {
                return false;
            }
        }
    }
}
