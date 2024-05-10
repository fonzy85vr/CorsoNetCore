using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorsoNetCore.Models.Options;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CorsoNetCore.Models.Services.Repository
{
    public class MailService : IEmailSender
    {
        private readonly ILogger<MailService> _logger;
        private readonly IOptionsMonitor<SmtpOption> _mailOption;

        public MailService (IOptionsMonitor<SmtpOption> mailOption, ILogger<MailService> logger) {
            _logger = logger;
            _mailOption = mailOption;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try{
                var option = _mailOption.CurrentValue;
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(option.Sender));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = subject;
                message.Body = new TextPart("html"){
                    Text = htmlMessage
                };

                using var mailClient = new SmtpClient();
                mailClient.Connect(option.Host, option.Port, option.Security);
                
                await mailClient.AuthenticateAsync(option.Username, option.Password);
                await mailClient.SendAsync(message);
                await mailClient.DisconnectAsync(true);

            } catch (Exception ex){
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}