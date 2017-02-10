using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jzo.Services
{
    public class MandrillService
    {
        public void SendEmail(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Jzo Fashion", "jzofashion@digitalforte.ng"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                client.LocalDomain = "digitalforte.ng";

                client.Connect("smtp.mandrillapp.com", 587, false);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                //Authenticate
                client.Authenticate("Digital Forte Enterprise Systems Limited", "Q9V8aMb3pLwJB84giUq1zQ");
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}
