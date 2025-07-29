using Domain.Commands.v1.Usuarios.BuscarUsuarioPorId;

namespace CommonTestUtilities.Commands.Usuarios
{
    public class BuscarUsuarioPorIdCommandBuilder
    {
        public static BuscarUsuarioPorIdCommand Build()
        {
            return new BuscarUsuarioPorIdCommand(Guid.NewGuid());
        }
    }
}
