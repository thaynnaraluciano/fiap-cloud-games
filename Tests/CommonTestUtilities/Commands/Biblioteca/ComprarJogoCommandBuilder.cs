using Bogus;
using Domain.Commands.v1.Biblioteca.ComprarJogo;
using Domain.Commands.v1.Jogos.AtualizarJogo;

namespace CommonTestUtilities.Commands.Biblioteca
{
    public class ComprarJogoCommandBuilder
    {
        public static ComprarJogoCommand Build()
        {
            return new Faker<ComprarJogoCommand>()
                .RuleFor(c => c.IdUsuario, _ => Guid.NewGuid())
                .RuleFor(c => c.IdJogo, _ => Guid.NewGuid());
        }
        public static ComprarJogoCommand BuildWithId(Guid IdUsuario,Guid IdJogo)
        {
            var faker = Build();
            faker.IdUsuario = IdUsuario;
            faker.IdJogo = IdJogo;
            return faker;
        }

    }
}
