﻿namespace Core.Utilities.FileUpload
{
    public class FtpConfiguration : IFtpConfiguration
    {
        public string Address { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
