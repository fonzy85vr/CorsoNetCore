using Newtonsoft.Json;

namespace CorsoNetCore.Models.DataTypes.PaypalModels
{
    public class OrderRequest
    {
        [JsonProperty("intent")]
        public string Intent { get; set; }
        [JsonProperty("purchase_units")]
        public List<PurchaseUnit> PurchaseUnits { get; set; }
        [JsonProperty("payment_source")]
        public Dictionary<string, PayPalSource> PaymentSource { get; set; }
    }

    public class PayPalSource
    {
        [JsonProperty("experience_context")]
        public ExperienceContext ExperienceContext { get; set; }
    }

    public class ExperienceContext
    {
        [JsonProperty("payment_method_preference")]
        public string PaymentMethodPreference { get; set; }
        [JsonProperty("brand_name")]
        public string BrandName { get; set; }
        [JsonProperty("locale")]
        public string Locale { get; set; }
        [JsonProperty("landing_page")]
        public string LandingPage { get; set; }
        [JsonProperty("shipping_preference")]
        public string ShippingPreference { get; set; }
        [JsonProperty("user_action")]
        public string UserAction { get; set; }
        [JsonProperty("return_url")]
        public string ReturnUrl { get; set; }
        [JsonProperty("cancel_url")]
        public string CancelUrl { get; set; }
    }

    public class PurchaseUnit
    {
        public string custom_id { get; set; }
        public string description { get; set; }
        public AmountWithBreakdown amount { get; set; }
        [JsonProperty("items")]
        public List<PurchasItem> Items { get; set; }
    }

    public class PurchasItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("quantity")]
        public string Quantity { get; set; }
        [JsonProperty("unit_amount")]
        public Amount UnitAmount { get; set; }
    }

    public class Amount
    {
        public string currency_code { get; set; }
        public string value { get; set; }
    }

    public class AmountWithBreakdown : Amount
    {
        [JsonProperty("breakdown")]
        public Breakdown Breakdown { get; set; }
    }

    public class Breakdown
    {
        [JsonProperty("item_total")]
        public Amount ItemTotal { get; set; }
    }
}