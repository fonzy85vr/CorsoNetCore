using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CorsoNetCore.Models.Options;
using Microsoft.Extensions.Options;

namespace CorsoNetCore.Models.Services.Repository
{
    public abstract class PaymentGateway
    {
        protected readonly HttpClient _httpClient;
        protected IOptionsMonitor<PaymentOption> _options;

        public PaymentGateway(IOptionsMonitor<PaymentOption> options){
            _options = options;
            
            _httpClient = new HttpClient(){
                BaseAddress = new Uri(_options.CurrentValue.Url)
            };

            _httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");
        }
    }
}