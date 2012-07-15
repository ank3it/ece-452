using System;
using System.Net;
using System.Threading;
using System.Net.Mail;
using System.ComponentModel;

namespace ConsoleApplication1
{
    class EmailClient
    {
        public static void SendEmail(string destEmail, string subject, string body)
        {
            using (SmtpClient mailClient = new SmtpClient("smtp.gmail.com", 587))
            {
                mailClient.EnableSsl = true;
                mailClient.Credentials = new System.Net.NetworkCredential("eatonthego1", "ece452project");
                MailAddress addrFrom = new MailAddress("eatonthego1@gmail.com", "Eat on the Go");
                MailAddress addrTo = new MailAddress(destEmail);
                using (MailMessage msgEmail = new MailMessage(addrFrom, addrTo))
                {
                    msgEmail.Subject = subject;
                    msgEmail.Body = body;
                    mailClient.Send(msgEmail);
                }
            }
        } 
    }
}
