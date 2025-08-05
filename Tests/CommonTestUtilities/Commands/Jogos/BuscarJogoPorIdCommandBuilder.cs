using Bogus;
using Domain.Commands.v1.Jogos.BuscarJogo;

namespace CommonTestUtilities.Commands.Jogos
{
    public class BuscarJogoPorIdCommandBuilder
    {
        public static BuscarJogoPorIdCommand Build()
        {
            return new Faker<BuscarJogoPorIdCommand>()
                .CustomInstantiator(faker => new BuscarJogoPorIdCommand(Guid.NewGuid()));
        }

        public static BuscarJogoPorIdCommand BuildComId(Guid id)
        {
            return new BuscarJogoPorIdCommand(id);
        }
    }
}
