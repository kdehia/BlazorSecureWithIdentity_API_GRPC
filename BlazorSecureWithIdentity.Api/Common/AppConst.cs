using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSecureWithIdentity.Api.Common
{
    public static class AppConst
    {
        public class Policy
        {
            
            private Policy()
            {

            }
            public const string Founder  = "FOUNDERS";
        }

        public static class Claim
        {
            
            public const string ApplicationRole = "ApplicationRole";
            
        }

    }
}
