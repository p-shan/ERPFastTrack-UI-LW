using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ERPFastTrack.API.Internals.EmailSenders
{
    public class ERPFastTrackEmailSender //: IEmailSender
    {
        private readonly IConfiguration _configuration;
        public ERPFastTrackEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string MailServer = _configuration["EmailSettings:MailServer"];
            string FromEmail = _configuration["EmailSettings:FromEmail"];
            string Password = _configuration["EmailSettings:Password"];
            int Port = int.Parse(_configuration["EmailSettings:MailPort"]);
            var client = new SmtpClient(MailServer, Port)
            {
                Credentials = new NetworkCredential(FromEmail, Password),
                EnableSsl = true,
            };
            MailMessage mailMessage = new MailMessage(FromEmail, email, subject, htmlMessage)
            {
                IsBodyHtml = false
            };
            return client.SendMailAsync(mailMessage);
        }
    }
}
