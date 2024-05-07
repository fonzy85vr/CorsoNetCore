using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Security;

namespace CorsoNetCore.Models.Options
{
    public class SmtpOption
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public SecureSocketOptions Security { get; set; }
        public string Sender { get; set; }
    }
}