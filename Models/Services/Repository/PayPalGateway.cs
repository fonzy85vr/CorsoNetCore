using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorsoNetCore.Models.DataTypes.PaypalModels;
using CorsoNetCore.Models.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CorsoNetCore.Models.Services.Repository
{
    public class PayPalGateway : PaymentGateway, IPaymentGateway
    {
        private readonly ILogger<PayPalGateway> _logger;

        public PayPalGateway(IOptionsMonitor<PaymentOption> options, ILogger<PayPalGateway> logger) : base(options)
        {
            _logger = logger;
        }

        private async Task<string> Authorize()
        {
            var token = "";

            try
            {
                var options = _options.CurrentValue;
                var credentials = System.Text.Encoding.UTF8.GetBytes($"{options.ClientId}:{options.Secret}");
                string credentialsBase64 = System.Convert.ToBase64String(credentials);
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentialsBase64);

                var requestParams = new List<KeyValuePair<string, string>>(){
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                };

                var response = await _httpClient.PostAsync("v1/oauth2/token", new FormUrlEncodedContent(requestParams));
                var content = await response.Content.ReadAsStreamAsync();
                using var stream = new StreamReader(content);
                var result = stream.ReadToEnd();

                var jsonParsed = JsonConvert.DeserializeObject<AuthenticationResponse>(result.ToString());

                if (jsonParsed != null)
                {
                    token = jsonParsed.access_token;
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return token;
        }

        public async Task<string> GetPaymentUrl()
        {
            var toRet = "";
            var request = new OrderRequest()
            {
                intent = "CAPTURE",
                purchase_units = new List<PurchaseUnit>(){
                    new PurchaseUnit() {
                        custom_id = $"order 1",
                        amount = new Amount{
                            currency_code = "EUR",
                            value = "10.00"
                        }
                    }
                },
                payment_source = new Dictionary<string, PayPalSource>(){
                    {
                        "paypal",
                        new PayPalSource {
                            experience_context = new ExperienceContext {
                                brand_name = "E-Learning",
                                cancel_url = "https://localhost:7192",
                                locale = "it-IT",
                                return_url = "https://localhost:7192/Courses/ConfirmPayment",
                                shipping_preference = "NO_SHIPPING"
                            }
                        }
                    }
                }
            };
            var token = await Authorize();

            try
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                _httpClient.DefaultRequestHeaders.Add("Prefer", $"return=representation");

                var response = await _httpClient.PostAsJsonAsync("v2/checkout/orders", request);
                var content = await response.Content.ReadAsStreamAsync();
                using var stream = new StreamReader(content);
                var result = stream.ReadToEnd();

                var jsonParsed = JsonConvert.DeserializeObject<OrderResponse>(result.ToString());

                if (jsonParsed != null)
                {
                    toRet = jsonParsed.links.Where(link => link.rel == "payer-action").FirstOrDefault()?.href;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return toRet;
        }

        public async Task<bool> Confirm(string token)
        {
            var authToken = await Authorize();

            try
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {authToken}");

                var requestParams = new List<KeyValuePair<string, string>>();

                var response = await _httpClient.PostAsJsonAsync<OrderRequest>($"v2/checkout/orders/{token}/capture", null);
                var content = await response.Content.ReadAsStreamAsync();
                using var stream = new StreamReader(content);
                var result = stream.ReadToEnd();

                var jsonParsed = JsonConvert.DeserializeObject<CaptureOrderResponse>(result.ToString());

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}