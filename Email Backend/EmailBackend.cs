using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Email_Backend
{
    public class EmailBackend
    {
        private static string senderSmtpAddress = "smtp.nus.edu.sg";
        private static int senderSmtpPort = 587;
        private static string senderEmail = "sender@nus.com";
        private static string senderUsername = @"your_domain\your_userid";
        private static string senderPassword = "******";

        // WARNING : THIS FUNCTION IS UNTESTED!
        public static void sendEmailSteps(String recipientEmail, String emailSubject, String emailContent)
        {
            new Thread(() =>
           {
               try
               {
                   SmtpClient client = new SmtpClient(senderSmtpAddress, senderSmtpPort);
                   client.Credentials = new System.Net.NetworkCredential(senderUsername, senderPassword);
                   client.EnableSsl = true;
                   client.DeliveryMethod = SmtpDeliveryMethod.Network;
                   MailMessage mm = new MailMessage(senderEmail, recipientEmail);
                   mm.Subject = emailSubject;
                   mm.Body = emailContent;
                   client.Send(mm);
               }
               catch (Exception e)
               {
                   Console.Error.WriteLine(e.Message);
               }

           }
                ).Start();
        }
    }
}
