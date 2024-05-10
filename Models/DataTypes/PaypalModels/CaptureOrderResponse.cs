using Newtonsoft.Json;

namespace CorsoNetCore.Models.DataTypes.PaypalModels
{
    public class CaptureOrderResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("purchase_units")]
        public List<CapturePurchaseUnit> PurchaseUnits { get; set; }
    }

    public class CapturePurchaseUnit
    {
        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }

        [JsonProperty("payments")]
        public Payment Payments { get; set; }
    }

    public class Payment
    {
        [JsonProperty("captures")]
        public List<Capture> Captures { get; set; }
    }

    public class Capture
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("amount")]
        public Amount Amount { get; set; }
        [JsonProperty("custom_id")]
        public string CustomId { get; set; }
    }
}