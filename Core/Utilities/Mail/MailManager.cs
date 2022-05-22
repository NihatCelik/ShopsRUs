using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System.Collections.Generic;
using System.Linq;

namespace Core.Utilities.Mail
{
    public class MailManager : IMailService
    {
        private readonly IConfiguration _configuration;
        public MailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Send(EmailMessage emailMessage)
        {
            var mailConfiguration = _configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();

            var message = new MimeMessage();
            var from = new List<EmailAddress>
            {
                new EmailAddress
                {
                    Name = mailConfiguration.SenderEmail, Address = mailConfiguration.SenderEmail
                }
            };

            message.From.AddRange(from.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

            message.Subject = emailMessage.Subject;

            var messageBody = emailMessage.Content;

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = messageBody
            };
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect(mailConfiguration.SmtpServer, mailConfiguration.SmtpPort, mailConfiguration.SecureSocketOption);
                emailClient.Authenticate(mailConfiguration.UserName, mailConfiguration.Password);
                emailClient.Send(message);
                emailClient.Disconnect(true);
            }
        }
    }
}
