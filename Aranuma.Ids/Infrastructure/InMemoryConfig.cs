using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace Aranuma.Ids.Infrastructure
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
                new ApiScope("chat"),
                new ApiScope("sensor"),
            };

        public static IEnumerable<ApiResource> ApiResources() =>
            new ApiResource[]
            {
                new ApiResource("Aranuma.SignalR.Api")
                {
                    Scopes = new List<string>{ "chat", "sensor" },
                }
            };

        public static List<TestUser> GetUsers() =>
           new List<TestUser>
           {
               new TestUser
              {
                  SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4",
                  Username = "saied",
                  Password = "rahimi!@",
                  Claims = new List<Claim>
                  {
                      new Claim("given_name", "saied"),
                      new Claim("family_name", "rahimi"),
                      
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
                new Client
                   {
                        ClientId = "AranumaCo",
                        ClientSecrets = new [] { new Secret("signalRclientsAuth".Sha512()) },
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                        //AllowedGrantTypes = GrantTypes.ClientCredentials,//"client_credentials"
                        //AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "myApi.read" }
                        AllowedScopes = { "chat","sensor" }
                    }

            };
    }
}
