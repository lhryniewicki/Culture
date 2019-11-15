using Culture.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Culture.Utilities.ExtensionMethods
{
    public static class JwtExtensionMethods
    {
        public static string GetClaim(this ClaimsPrincipal claimsPrincipal, JwtTypes jwtClaim)
        {
            var claim = claimsPrincipal.Claims.Where(c => c.Type == jwtClaim.ToString()).FirstOrDefault();

            if (claim != null)
            {
                return claim.Value;
            }

            return null;
        }
    }
}
