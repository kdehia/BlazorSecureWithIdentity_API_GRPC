// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityProvider
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                        new IdentityResources.OpenId(),
                        new IdentityResources.Profile(),
                        new IdentityResources.Email(),
                        new IdentityResource("ApplicationRole",new string[] { "ApplicationRole"})
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> 
            {
                new ApiScope("companyApi", "CompanyEmployee API") ,
                new ApiScope("GRPC", "GRPC Client")
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
            new ApiResource("companyApi", "CompanyEmployee API")
                {
                    Scopes = { "companyApi" },
                    UserClaims = { "ApplicationRole" }

                },
            new ApiResource("GRPC", "GRPC Client")
                {
                    Scopes = { "GRPC" },
                    //UserClaims = { "ApplicationRole" }

                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "blazorWASM",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    
                    RequireClientSecret = false,
                    AllowedCorsOrigins = { "https://localhost:44336" },
                    AllowedScopes = { "openid", "profile","email" ,"companyApi","ApplicationRole","GRPC" },
                    RedirectUris = { "https://localhost:44336/authentication/login-callback" },
                    PostLogoutRedirectUris = {"https://localhost:44336/authentication/logout-callback"},//{ "https://localhost:44336/" },
                    Enabled = true,
                    RequireConsent = true
                },

            };
    }
}