using Domain.Commands.v1.Promocoes.RemoverPromocao;

namespace CommonTestUtilities.Commands.Promocoes
{
    public class RemoverPromocaoCommandBuilder
    {
        public static RemoverPromocaoCommand Build()
        {
            return new RemoverPromocaoCommand(Guid.NewGuid());
        }
    }
}
