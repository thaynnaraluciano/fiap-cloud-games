using CrossCutting.Configuration;
using Infrastructure.Services.Interfaces.v1;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Infrastructure.Data.Models.Notificacao.Email;

namespace Infrastructure.Services.Services.v1
{
    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<AppSettings> appSettings, ILogger<EmailService> logger)
        {
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
            _logger = logger;
        }

        public async Task EnviarEmail(EmailModel request)
        {
            try
            {
                _logger.LogInformation("Adding message configurations");

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_appSettings.Smtp.MailFromName, _appSettings.Smtp.MailFromEmail));
                message.To.Add(new MailboxAddress(request.NomeDestinatario, request.EmailDestinatario));
                message.Subject = request.Assunto;

                message.Body = new TextPart(request.TipoCorpo)
                {
                    Text = request.Corpo
                };

                _logger.LogInformation("Communicating with smtp client");

                using var client = new SmtpClient();
                await client.ConnectAsync(_appSettings.Smtp.Host, _appSettings.Smtp.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_appSettings.Smtp.MailFromEmail, _appSettings.Smtp.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                _logger.LogInformation("Email successfully sent");

                return;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending email: {ex.Message}");
                throw new Exception($"Error sending email: {ex.Message}");
            }
        }
    }
}
