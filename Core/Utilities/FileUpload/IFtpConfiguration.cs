namespace Core.Utilities.FileUpload
{
    public interface IFtpConfiguration
    {
        string Address { get; set; }

        string UserName { get; set; }

        string Password { get; set; }
    }
}
