using Domain.Commands.v1.Usuarios.RemoverUsuario;

namespace CommonTestUtilities.Commands.Usuarios
{
    public class RemoverUsuarioCommandBuilder
    {
        public static RemoverUsuarioCommand Build()
        {
            return new RemoverUsuarioCommand(Guid.NewGuid());
        }
    }
}
