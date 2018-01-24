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
        private static bool SendEmailForReal = false;

        private static string senderSmtpAddress = "smtp.sendgrid.net";
        private static int senderSmtpPort = 587;
        private static string senderEmail = "e0224940@u.nus.edu";
        private static string senderUsername = @"apikey";
        private static string senderPassword = "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";

        // WARNING : THIS FUNCTION IS UNTESTED!
        public static bool sendEmailStep(String recipientEmail, String emailSubject, String emailContent)
        {
            // Default to true if we are not sending email for real
            bool result = false; 

            if (SendEmailForReal)
            {
                try
                {
                    SmtpClient client = new SmtpClient(senderSmtpAddress, Convert.ToInt32(senderSmtpPort));
                    client.Credentials = new System.Net.NetworkCredential(senderUsername, senderPassword);
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Timeout = 10000;

                    MailMessage mm = new MailMessage(senderEmail, recipientEmail);
                    mm.Subject = emailSubject;
                    mm.Body = emailContent;

                    client.Send(mm);

                    result = true;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.Message);
                    result = false;
                }
            }
            else
            {
                // We are not actually sending email, just passing success for now
                result = true;
            }

            return result;
        }
    }
}
