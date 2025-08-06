using Bogus;
using Domain.Commands.v1.Usuarios.AtualizarUsuario;
using Domain.Enums;

namespace CommonTestUtilities.Commands.Usuarios
{
    public class AtualizarUsuarioCommandBuilder
    {
        public static AtualizarUsuarioCommand Build()
        {
            return new Faker<AtualizarUsuarioCommand>()
            .RuleFor(c => c.Id, _ => Guid.NewGuid())
            .RuleFor(c => c.Nome, f => f.Name.FullName())
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.PerfilUsuario, f => f.PickRandom<PerfilUsuarioEnum>());
        }
    }
}
