
namespace Core.Utilities.Mail
{
    public interface IEmailConfiguration
    {
        string SmtpServer { get; set; }

        int SmtpPort { get; set; }

        string SenderName { get; set; }

        string SenderEmail { get; set; }

        string UserName { get; set; }

        string Password { get; set; }

        string Url { get; set; }
    }
}
