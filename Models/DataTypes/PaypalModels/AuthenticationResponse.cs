namespace CorsoNetCore.Models.DataTypes.PaypalModels
{
    public class AuthenticationResponse
    {
        public string scope { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string app_id { get; set; }
        public int expired_in { get; set; }
        public string nonce { get; set; }
    }
}