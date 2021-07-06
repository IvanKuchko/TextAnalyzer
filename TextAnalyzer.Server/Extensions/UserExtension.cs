using System.Collections.Generic;
using System.Security.Claims;

namespace TextAnalyzer.Server.Extensions
{
    public static class UserExtension
    {
        public static string GetUserId(this IEnumerable<Claim> claims)
        {
            foreach (var claim in claims)
            {
                if (claim.Type == "Sub")
                    return claim.Value;
            }
            return string.Empty;
        }
    }
}
