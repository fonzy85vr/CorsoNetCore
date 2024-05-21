using System.Globalization;
using System.Text;
using CorsoNetCore.Models.DataTypes.PaypalModels;
using CorsoNetCore.Models.Options;
using CorsoNetCore.Models.ViewModel;
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
                    token = jsonParsed.AccessToken;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return token;
        }

        public async Task<string> GetPaymentUrl(CreateOrderModel model)
        {
            var paymentUrl = "";
            var request = new OrderRequest()
            {
                Intent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnit>(){
                    new PurchaseUnit() {
                        custom_id = $"{model.UserId}:{model.CourseId}",
                        amount = new AmountWithBreakdown{
                            currency_code = model.Amount.Currency.ToString(),
                            value = model.Amount.Amount.ToString(CultureInfo.InvariantCulture),
                            Breakdown = new Breakdown {
                                ItemTotal = new Amount {
                                    currency_code = model.Amount.Currency.ToString(),
                                    value = model.Amount.Amount.ToString(CultureInfo.InvariantCulture)
                                }
                            }
                        },
                        description = model.Description,
                        Items = new List<PurchasItem> {
                            new PurchasItem {
                                Name = model.Description,
                                Quantity = "1",
                                UnitAmount = new Amount {
                                    currency_code = model.Amount.Currency.ToString(),
                                    value = model.Amount.Amount.ToString(CultureInfo.InvariantCulture)
                                }
                            }
                        }
                    }
                },
                PaymentSource = new Dictionary<string, PayPalSource>(){
                    {
                        "paypal",
                        new PayPalSource {
                            ExperienceContext = new ExperienceContext {
                                BrandName = "E-Learning",
                                CancelUrl = "https://localhost:7192",
                                Locale = "it-IT",
                                ReturnUrl = "https://localhost:7192/Courses/ConfirmPayment",
                                ShippingPreference = "NO_SHIPPING"
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

                var response = await _httpClient.PostAsync("v2/checkout/orders", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
                var content = await response.Content.ReadAsStreamAsync();
                using var stream = new StreamReader(content);
                var result = stream.ReadToEnd();

                var jsonParsed = JsonConvert.DeserializeObject<OrderResponse>(result.ToString());

                if (jsonParsed != null)
                {
                    paymentUrl = jsonParsed.Links.Where(link => link.Rel == "payer-action").FirstOrDefault()?.Href ?? "";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return paymentUrl;
        }

        public async Task<string> Confirm(string token)
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

                return jsonParsed!.PurchaseUnits.FirstOrDefault()!.Payments.Captures!.FirstOrDefault()!.CustomId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return "";
            }
        }
    }
}