using Infrastructure.Data.Models.Notificacao.Email;

namespace Infrastructure.Services.Interfaces.v1
{
    public interface IEmailService
    {
        Task EnviarEmail(EmailModel request);
    }
}
