using CrossCutting.Configuration;
using Infrastructure.Services.Interfaces.v1;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.Services.v1
{
    public class EmailTemplateService : IEmailTemplateService
    {
        public EmailTemplateService(IOptions<AppSettings> appSettings)
        {
        }

        public string GerarEmailDeConfirmacao(string receiverName, string verificationCode)
        {
            return $@"
                <table width=""100%"" height=""100%"" style=""border-collapse: collapse; text-align: center; background-color: #f4f4f4;"">
                    <tr>
                        <td>
                            <div style=""margin: 0 auto; padding: 20px; border-radius: 8px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); text-align: center; background-color: white;"">
                                <h1 style=""font-size: 24px; color: #333;"">{receiverName},</h1>
                                <p style=""font-size: 16px; color: #666;"">Utilize o token abaixo para criar a senha:</p>
                                <div style=""background-color: #007BFF; color: white; text-decoration: none; display: inline-block; padding: 10px 20px; font-size: 16px; border-radius: 5px; margin-top: 20px;"">{verificationCode}</div>
                            </div>
                            <div style=""font-size: 12px; color: #666; margin-top: 20px; text-align: center;"">
                                <p>
                                    Este e-mail e seus anexos são confidenciais e destinados exclusivamente ao(s) destinatário(s) indicado(s).
                                    Se você recebeu esta mensagem por engano, por favor, informe o remetente imediatamente e exclua o e-mail.
                                    É proibida qualquer divulgação, cópia ou uso não autorizado do conteúdo.
                                </p>
                                <p>
                                    <strong>Contato:</strong> fiapcloudgames@example.com | Telefone: +55 99 99999-9999<br>
                                </p>
                            </div>
                        </td>
                    </tr>
                </table>";
        }
    }
}
