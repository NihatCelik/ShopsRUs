namespace Core.Utilities.Mail
{
    public class EmailConfiguration : IEmailConfiguration
    {
        public string SmtpServer { get; set; }

        public int SmtpPort { get; set; }

        public string SenderName { get; set; }

        public string SenderEmail { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Url { get; set; }

        public MailKit.Security.SecureSocketOptions SecureSocketOption { get; set; }
    }
}
