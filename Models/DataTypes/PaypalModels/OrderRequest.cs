using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoNetCore.Models.DataTypes.PaypalModels
{
    public class OrderRequest
    {
        public string intent { get; set; }
        public List<PurchaseUnit> purchase_units { get; set; }
        public Dictionary<string, PayPalSource> payment_source { get; set; }
    }

    public class PayPalSource
    {
        public ExperienceContext experience_context { get; set; }
    }

    public class ExperienceContext
    {
        public string payment_method_preference { get; set; }
        public string brand_name { get; set; }
        public string locale { get; set; }
        public string landing_page { get; set; }
        public string shipping_preference { get; set; }
        public string user_action { get; set; }
        public string return_url { get; set; }
        public string cancel_url { get; set; }
    }

    public class PurchaseUnit
    {
        public string custom_id { get; set; }
        public Amount amount { get; set; }
    }

    public class Amount
    {
        public string currency_code { get; set; }
        public string value { get; set; }
    }
}