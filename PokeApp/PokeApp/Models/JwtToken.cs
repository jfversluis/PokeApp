using Newtonsoft.Json;

namespace PokeApp.Models
{
    internal class JwtToken
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("access_token")]
        public string Token { get; set; }
    }
}