namespace Infrastructure.Services.Interfaces.v1
{
    public interface ITokenService
    {
        string GerarToken(string email, string perfil);
    }
}
