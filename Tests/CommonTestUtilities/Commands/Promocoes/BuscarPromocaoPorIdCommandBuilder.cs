using Domain.Commands.v1.Promocoes.BuscarPromocaoPorId;

namespace CommonTestUtilities.Commands.Promocoes
{
    public class BuscarPromocaoPorIdCommandBuilder
    {
        public static BuscarPromocaoPorIdCommand Build()
        {
            return new BuscarPromocaoPorIdCommand(Guid.NewGuid());
        }
    }
}
