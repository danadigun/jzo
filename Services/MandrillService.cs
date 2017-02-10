using MailKit.Net.Smtp;
using MailKit.Security;
using Mandrill;
using Mandrill.Model;
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
            var api = new MandrillApi("Q9V8aMb3pLwJB84giUq1zQ");
            var _message = new MandrillMessage("jzo_account@digitalforte.ng", email,
                            subject, message);
            var result =  api.Messages.SendAsync(_message);
        }
       
    }
}
