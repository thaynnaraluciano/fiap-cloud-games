using Infrastructure.Data.Models.Jogos;
using MediatR;

namespace Domain.Commands.v1.Jogos.CriarJogo
{
    public class CriarJogoCommand : IRequest<Unit>
    {
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public decimal Preco { get; set; }
        public DateTime DataLancamento { get; set; }
    }
}
