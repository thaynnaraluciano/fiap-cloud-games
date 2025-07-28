using Bogus;
using Domain.Commands.v1.Jogos.CriarJogo;
using Domain.Commands.v1.Promocoes.CriarPromocao;

namespace CommonTestUtilities.Commands.Promocoes
{
    public class CriarPromocaoCommandBuilder
    {
        public static CriarPromocaoCommand Build()
        {
            return new Faker<CriarPromocaoCommand>()
            .RuleFor(c => c.Nome, f => f.Commerce.ProductName())
            .RuleFor(c => c.Desconto, f => f.Random.Decimal(1, 100))
            .RuleFor(c => c.DataInicio, f => f.Date.FutureOffset().DateTime)
            .RuleFor(c => c.DataFim, (f, c) => c.DataInicio.AddDays(f.Random.Int(1, 10)))
            .RuleFor(c => c.JogosIds, f => new List<Guid> { Guid.NewGuid() });
        }
    }
}
