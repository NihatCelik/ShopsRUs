using Core.Extensions;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Core.Utilities.UserRequest
{
    public class UserRequestManager : IUserRequestService
    {
        private IHttpContextAccessor _httpContextAccessor;
        public int RequestCompanyId
        {
            get
            {
                _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
                return _httpContextAccessor.HttpContext.User.GetClaimValue("companyid").ToInt32();
            }
        }
        public bool IsAdmin
        {
            get
            {
                _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
                var roles = _httpContextAccessor.HttpContext.User.GetClaimValue(ClaimTypes.Role);
                return !string.IsNullOrEmpty(roles) && roles.Contains("Admin");
            }
        }
        public bool IsCompanyAdmin
        {
            get
            {
                _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
                var roles = _httpContextAccessor.HttpContext.User.GetClaimValue(ClaimTypes.Role);
                return !string.IsNullOrEmpty(roles) && roles.Contains("Company");
            }
        }
        public bool IsCustomer
        {
            get
            {
                _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
                var roles = _httpContextAccessor.HttpContext.User.GetClaimValue(ClaimTypes.Role);
                return !string.IsNullOrEmpty(roles) && roles.Contains("Customer");
            }
        }
        public int RequestUserId
        {
            get
            {
                _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
                var nameIdentifier = _httpContextAccessor?.HttpContext?.User?.GetClaimValue(ClaimTypes.NameIdentifier);
                return nameIdentifier.StringIsNullOrEmpty() ? 0 : nameIdentifier.ToInt32();
            }
        }
    }
}
