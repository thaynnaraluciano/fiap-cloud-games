namespace Infrastructure.Services.Interfaces.v1
{
    public interface IEmailTemplateService
    {
        string GerarEmailDeConfirmacao(string receiverName, string token);
    }
}
