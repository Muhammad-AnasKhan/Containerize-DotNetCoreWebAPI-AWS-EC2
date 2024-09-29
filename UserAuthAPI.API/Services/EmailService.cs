using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace UserAuthAPI.API.Services
{

    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration) {
            _configuration = configuration;
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var EmailCred = _configuration.GetSection("SmtpCredentials");
            var smtpClient = new SmtpClient(EmailCred["client"])
            {
                Port = 587,
                Credentials = new NetworkCredential(EmailCred["UserName"], EmailCred["Pwd"]),
                EnableSsl = true,
                
            };
            
            MailMessage mailMessage = new MailMessage()
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };
            mailMessage.From = new MailAddress("ans@company.com");
            mailMessage.To.Add(new MailAddress(email));

            return smtpClient.SendMailAsync(mailMessage);
        }
    }

}
