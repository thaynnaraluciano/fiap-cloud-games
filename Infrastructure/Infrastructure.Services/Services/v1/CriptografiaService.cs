using Infrastructure.Services.Interfaces.v1;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services.Services.v1
{
    public class CriptografiaService : ICriptografiaService
    {
        public string HashSenha(string? senha)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(senha!);

                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder hashBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashBuilder.Append(b.ToString("x2"));
                }

                return hashBuilder.ToString();
            }
        }
    }
}
