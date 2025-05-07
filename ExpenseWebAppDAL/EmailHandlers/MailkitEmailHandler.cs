using ExpenseWebAppDAL.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace ExpenseWebAppDAL.EmailHandlers
{
    public class MailkitEmailHandler : IEmailHandler
    {
        private readonly IConfiguration _configuration;
        public MailkitEmailHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(string email, string subject, string body)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_configuration["EmailSettings:ApplicationName"], _configuration["EmailSettings:Email"]));
            message.To.Add(MailboxAddress.Parse(email));

            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_configuration["EmailSettings:Email"], _configuration["EmailSettings:Password"]); 
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
    }
}
