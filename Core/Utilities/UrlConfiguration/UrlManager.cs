using Microsoft.Extensions.Configuration;

namespace Core.Utilities.UrlConfiguration
{
    public class UrlManager : IUrlService
    {
        private readonly IConfiguration _configuration;
        public UrlManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetImageUrl(string imagePath)
        {
            var urlConfiguration = _configuration.GetSection("URLConfiguration").Get<UrlConfiguration>();
            return urlConfiguration.ImageUrl + imagePath;
        }
    }
}
