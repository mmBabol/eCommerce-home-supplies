using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Security.Principal;

namespace _3MA.Extensions
{
    public static class IdentityExtensions
    {
        public static int GetCustomerSuite(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Suite");
            // Test for null to avoid issues during local testing
            return (claim != null) ? Convert.ToInt32(claim.Value) : 0;
        }

        public static string GetCustomerFloorPlan(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Plan");
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}