using Bogus;
using Domain.Commands.v1.Jogos.CriarJogo;

namespace CommonTestUtilities.Commands.Jogos
{
    public class CriarJogoCommandBuilder
    {
        public static CriarJogoCommand Build()
        {
            return new Faker<CriarJogoCommand>()
                .RuleFor(r => r.Nome, faker => faker.Commerce.ProductName())
                .RuleFor(r => r.Descricao, faker => faker.Commerce.ProductDescription())
                .RuleFor(r => r.Preco, faker => faker.Finance.Amount(10, 500))
                .RuleFor(r => r.DataLancamento, faker => DateTime.Today);
        }
    }
}
