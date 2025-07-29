using Bogus;
using Domain.Commands.v1.Promocoes.AtualizarPromocao;

namespace CommonTestUtilities.Commands.Promocoes
{
    public class AtualizarPromocaoCommandBuilder
    {
        public static AtualizarPromocaoCommand Build()
        {
            return new Faker<AtualizarPromocaoCommand>()
                .RuleFor(c => c.Id, _ => Guid.NewGuid())
                .RuleFor(c => c.Nome, f => f.Commerce.ProductName())
                .RuleFor(c => c.Desconto, f => f.Random.Decimal(1, 80))
                .RuleFor(c => c.DataInicio, f => f.Date.FutureOffset(1).DateTime)
                .RuleFor(c => c.DataFim, (f, c) => c.DataInicio.AddDays(f.Random.Int(1, 30)))
                .RuleFor(c => c.JogosIds, _ => new List<Guid> { Guid.NewGuid(), Guid.NewGuid() });
        }
    }
}
