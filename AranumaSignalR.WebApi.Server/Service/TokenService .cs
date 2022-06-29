using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AranumaSignalR.WebApi.Server.Service
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);
    }

    public class TokenService : ITokenService
    {
        private DiscoveryDocumentResponse _discDocument { get; set; }
        public TokenService()
        {
            using (var client = new HttpClient())
            {
                _discDocument = client.GetDiscoveryDocumentAsync("http://localhost:5000/.well-known/openid-configuration").Result;
            }
        }
        public async Task<TokenResponse> GetToken(string scope= "chat")
        {
            using (var client = new HttpClient())
            {
                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = _discDocument.TokenEndpoint,
                    ClientId = "AranumaCo",
                    Scope = scope,
                    ClientSecret = "signalRclientsAuth",
                    GrantType= "client_credentials",
                    
                });
                if (tokenResponse.IsError)
                {
                    throw new Exception("Token Error");
                }
                return tokenResponse;
            }
        }
    }
}
