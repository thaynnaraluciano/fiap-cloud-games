using Bogus;
using Domain.Commands.v1.Jogos.AtualizarJogo;

namespace CommonTestUtilities.Commands.Jogos
{
    public class AtualizarJogoCommandBuilder
    {
        public static AtualizarJogoCommand Build()
        {
            return new Faker<AtualizarJogoCommand>()
                .RuleFor(c => c.Id, _ => Guid.NewGuid())
                .RuleFor(c => c.Nome, f => f.Commerce.ProductName())
                .RuleFor(c => c.Descricao, f => f.Lorem.Sentence(10))
                .RuleFor(c => c.Preco, f => f.Random.Decimal(1, 500))
                .RuleFor(r => r.DataLancamento, faker => faker.Date.Future(5));
        }

        public static AtualizarJogoCommand BuildWithId(Guid id)
        {
            var faker = Build();
            faker.Id = id;
            return faker;
        }
    }
}
