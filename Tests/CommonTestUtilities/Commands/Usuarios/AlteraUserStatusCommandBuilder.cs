using Bogus;
using Domain.Commands.v1.Usuarios.AlterarStatusUsuario;

namespace CommonTestUtilities.Commands.Usuarios
{
    public class AlteraUserStatusCommandBuilder
    {
        public static AlterarStatusUsuarioCommand Build()
        {
            return new Faker<AlterarStatusUsuarioCommand>()
                .RuleFor(c => c.cGuid, f => Guid.NewGuid())
                .RuleFor(c => c.bStatus, f => f.Random.Bool());
        }
    }
}
