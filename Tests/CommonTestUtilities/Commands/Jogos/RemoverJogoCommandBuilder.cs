using Domain.Commands.v1.Jogos.RemoverJogo;

namespace CommonTestUtilities.Commands.Jogos
{
    public class RemoverJogoCommandBuilder
    {
        public static RemoverJogoCommand Build()
        {
            return new RemoverJogoCommand(Guid.NewGuid());
        }
    }
}
