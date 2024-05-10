using Newtonsoft.Json;

namespace CorsoNetCore.Models.DataTypes.PaypalModels
{
    public class OrderResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("links")]
        public List<Links> Links { get; set; }
    }

    public class Links
    {
        [JsonProperty("href")]
        public string Href { get; set; }
        [JsonProperty("rel")]
        public string Rel { get; set; }
        [JsonProperty("method")]
        public string Method { get; set; }
    }
}