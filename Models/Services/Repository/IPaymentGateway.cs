using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorsoNetCore.Models.ViewModel;

namespace CorsoNetCore.Models.Services.Repository
{
    public interface IPaymentGateway
    {
        Task<string> GetPaymentUrl(CreateOrderModel model);
        Task<string> Confirm(string token);
    }
}