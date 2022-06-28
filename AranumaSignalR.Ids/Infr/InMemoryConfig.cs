using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace AranumaSignalR.Ids.Infr
{
    public static class InMemoryConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
          new List<IdentityResource>
          {
          new IdentityResources.OpenId(),
          new IdentityResources.Profile(),

          };

        public static IEnumerable<ApiScope> ApiScopes() =>
            new ApiScope[]
            {
                new ApiScope("myApi.read"),
                new ApiScope("myApi.write"),
            };

        public static IEnumerable<ApiResource> ApiResources() =>
            new ApiResource[]
            {
                new ApiResource("myApi")
                {
                    Scopes = new List<string>{ "myApi.read","myApi.write" },
                    ApiSecrets = new List<Secret>{ new Secret("signalRclientsAuth".Sha256()) }
                }
            };

        public static List<TestUser> GetUsers() =>
           new List<TestUser>
           {
              //new TestUser
              //{
              //    SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4",
              //    Username = "Mick",
              //    Password = "MickPassword",
              //    Claims = new List<Claim>
              //    {
              //        new Claim("given_name", "Mick"),
              //        new Claim("family_name", "Mining")
              //    }
              //},
               new TestUser
              {
                  SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4",
                  Username = "saied",
                  Password = "rahimi!@",
                  Claims = new List<Claim>
                  {
                      new Claim("given_name", "saied"),
                      new Claim("family_name", "rahimi"),
                      new Claim(JwtClaimTypes.WebSite, "http://codewithmukesh.com"),
                  }
              },
              new TestUser
              {
                  SubjectId = "c95ddb8c-79ec-488a-a485-fe57a1462340",
                  Username = "Jane1",
                  Password = "JanePassword1",
                  Claims = new List<Claim>
                  {
                      new Claim("given_name", "Jane"),
                      new Claim("family_name", "Downing")
                  }
              }
           };

        public static IEnumerable<Client> GetClients() =>
    new List<Client>
    {
       //new Client
       //{
       //     ClientId = "company-employee",
       //     ClientSecrets = new [] { new Secret("codemazesecret".Sha512()) },
       //     AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
       //     AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId }
       // },
        new Client
       {
            ClientId = "AranumaCo",
            ClientSecrets = new [] { new Secret("signalRclientsAuth".Sha512()) },
            //AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            //AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "myApi.read" }
            AllowedScopes = { "myApi.read" }
        },
        new Client
        {
            ClientId = "cwm.client",
            ClientName = "Client Credentials Client",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = { new Secret("signalRclientsAuth".Sha256()) },
            AllowedScopes = { "myApi.read" }
        },

    };
    }
}
