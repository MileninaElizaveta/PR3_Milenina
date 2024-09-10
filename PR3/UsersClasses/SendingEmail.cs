using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace PR3.UsersClasses
{
    public class SendingEmail
    {
        private InfoEmailSending InFoEmailSending { get; set; }

        public SendingEmail(InfoEmailSending infoEmailSending)
        {
            InFoEmailSending = infoEmailSending
            ?? throw new ArgumentNullException(nameof(infoEmailSending));
        }
        public void Send()
        {
            try
            {
                SmtpClient mySmtpClient = new SmtpClient(InFoEmailSending.SmtpClientAdress);/*SmtpClient(InfoEmailSending.SmtpClientAdress);*/

                mySmtpClient.UseDefaultCredentials = false;
                mySmtpClient.EnableSsl = true;

                NetworkCredential basicAuthenticationInfo = new
                NetworkCredential(InFoEmailSending.EmailAdressFrom.EmailAdress, InFoEmailSending.EmailPassword);
                mySmtpClient.Credentials = basicAuthenticationInfo;


                MailAddress from = new MailAddress(
                InFoEmailSending.EmailAdressFrom.EmailAdress, InFoEmailSending.EmailAdressFrom.Name);

                MailAddress to = new MailAddress(
                InFoEmailSending.EmailAdressTo.EmailAdress,
                InFoEmailSending.EmailAdressTo.Name);
                MailMessage myMail = new MailMessage(from, to);

                MailAddress replyTo =
                new MailAddress(InFoEmailSending.EmailAdressFrom.EmailAdress);
                myMail.ReplyToList.Add(replyTo);

                Encoding encoding = Encoding.UTF8;

                myMail.Subject = InFoEmailSending.Subject;
                myMail.SubjectEncoding = encoding;

                myMail.Body = InFoEmailSending.Body;
                myMail.BodyEncoding = encoding;

                mySmtpClient.Send(myMail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
