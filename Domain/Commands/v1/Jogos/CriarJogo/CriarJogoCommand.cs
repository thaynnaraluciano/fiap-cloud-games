using Infrastructure.Data.Models.Jogos;
using MediatR;

namespace Domain.Commands.v1.Jogos.CriarJogo
{
    public class CriarJogoCommand : IRequest<CriarJogoCommandResponse>
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataLancamento { get; set; }
    }
}
