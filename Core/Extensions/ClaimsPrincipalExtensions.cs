using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }

        public static string GetClaimValue(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var claimValue = claimsPrincipal?.Claims(claimType).FirstOrDefault();
            if (string.IsNullOrEmpty(claimValue))
            {
                return null;
            }
            return claimValue;
        }
    }
}
