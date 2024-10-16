using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using Atebion.Outlook;

namespace ProfessionalDocAnalyzer
{
    public class EmailAtebion
    {
        private string _ErrorMessage;

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _SourceName;

        public string SourceName
        {
            get { return _SourceName; }
            set { _SourceName = value; }
        }

        private string _Subject;

        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }

        private string _Body;

        public string Body
        {
            get { return _Body; }
            set { _Body = value; }
        }



        public bool SendEmail(bool TryOutlook)
        {
            if (TryOutlook)
            {
                if (SendEmailViaOutlook())
                    return true;
            }

            if (SendEmailViaHostMoster())
                return true;
            else
                return false;
        }

        private bool SendEmailViaOutlook()
        {
            try
            {

                Atebion.Outlook.Email email2 = new Email();
                bool emailSent = email2.SendEmail("Sales@ScionAnalytics.com", _Subject, _Body + Environment.NewLine + "New Edition - Notice: Sent via Outlook");
                if (emailSent)
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
                _ErrorMessage = string.Concat("Outlook Email Error: ", ex.Message);
                return false;
            }


        }

        private bool SendEmailViaHostMoster()
        {
            _ErrorMessage = string.Empty;

            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();

            try
            {
                m.From = new MailAddress("docanalyzer@atebionllc.com", _SourceName);
                m.To.Add(new MailAddress("Sales@ScionAnalytics.com", "Sales"));
                m.CC.Add(new MailAddress("sales@atebionllc.com", "Sales"));
                m.Bcc.Add(new MailAddress("Tom_W_Lipscomb@yahoo.com", "Tom Lipscomb"));

                m.Subject = _Subject;
                m.IsBodyHtml = false;
                m.Body = _Body + Environment.NewLine + "Notice: Sent via Atebion's Website";



                sc.Host = "mail.atebionllc.com";
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = true;
                sc.Port = 26;
                sc.Credentials = new System.Net.NetworkCredential("docanalyzer@atebionllc.com", "511t26l59");
                sc.EnableSsl = false;
                sc.Send(m);

                return true;

            }
            catch (Exception ex)
            {
                _ErrorMessage = string.Concat("New Edition - Website Email Error: ", ex.Message);
                return false;

            }

        }


    }
}
