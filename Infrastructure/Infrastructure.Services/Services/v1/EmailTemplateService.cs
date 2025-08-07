using CrossCutting.Configuration;
using Infrastructure.Services.Interfaces.v1;
using Microsoft.Extensions.Options;
using System.IO;

namespace Infrastructure.Services.Services.v1
{
    public class EmailTemplateService : IEmailTemplateService
    {
        const string nomeEmail = "emailToken.html";
        public EmailTemplateService(IOptions<AppSettings> appSettings)
        {
        }
        public string GerarEmailDeConfirmacao(string receiverName, string verificationCode)
        {
            string caminhoTemplate = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "Email", nomeEmail);
            string corpoEmail = File.ReadAllText(caminhoTemplate);

            corpoEmail = corpoEmail.Replace("@NomeUser@",receiverName.Trim().Split(' ')[0]);
            corpoEmail = corpoEmail.Replace("@TOKEN@", verificationCode);
            return corpoEmail;
        }
    }
}
