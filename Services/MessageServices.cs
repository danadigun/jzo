using MailKit.Net.Smtp;
using MailKit.Security;
using Mandrill;
using Mandrill.Model;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace jzo.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var api = new MandrillApi("Q9V8aMb3pLwJB84giUq1zQ");
            var _message = new MandrillMessage("jzo_account@digitalforte.ng", email,
                            subject, message);
            var result = await api.Messages.SendAsync(_message);
        }

        public async Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            await InfoBipService.sendMessage(number, message);
           
        }
    }
}
