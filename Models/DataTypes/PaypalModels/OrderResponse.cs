namespace CorsoNetCore.Models.DataTypes.PaypalModels
{
    public class OrderResponse
    {
        public string id { get; set; }
        public string status { get; set; }
        public List<Links> links { get; set; }
    }

    public class Links
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string method { get; set; }
    }
}