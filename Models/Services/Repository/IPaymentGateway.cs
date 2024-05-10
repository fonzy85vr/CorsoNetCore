using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorsoNetCore.Models.Services.Repository
{
    public interface IPaymentGateway
    {
        Task<string> GetPaymentUrl();
        Task<bool> Confirm(string token);
    }
}