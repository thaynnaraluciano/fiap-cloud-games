using Bogus;
using Domain.Commands.v1.Usuarios.CriarUsuario;
using Domain.Enums;

namespace CommonTestUtilities.Commands.Usuarios
{
    public class CriarUsuarioCommandBuilder
    {
        public static CriarUsuarioCommand Build()
        {
            return new Faker<CriarUsuarioCommand>()
                .RuleFor(c => c.Nome, f => f.Name.FullName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.PerfilUsuario, f => PerfilUsuarioEnum.Administrador);
        }
    }
}
