using Newtonsoft.Json;

namespace CorsoNetCore.Models.DataTypes.PaypalModels
{
    public class AuthenticationResponse
    {
        [JsonProperty("scope")]
        public string Scope { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("app_id")]
        public string AppId { get; set; }
        [JsonProperty("expired_in")]
        public int ExpiredIn { get; set; }
        [JsonProperty("nonce")]
        public string Nonce { get; set; }
    }
}