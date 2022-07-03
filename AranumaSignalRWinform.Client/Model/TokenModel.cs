using System.Text.Json.Serialization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AranumaSignalRWinform.Client.Model
{
    public class TokenModel
    {
        [JsonPropertyName("access_token")]
        //[JsonProperty("access_token")]
        public string AccessToken { get; set; }
        //public string access_token { get; set; }


        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }

    }
}
