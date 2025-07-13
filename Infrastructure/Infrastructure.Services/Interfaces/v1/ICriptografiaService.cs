namespace Infrastructure.Services.Interfaces.v1
{
    public interface ICriptografiaService
    {
        string HashSenha(string? senha);
    }
}
