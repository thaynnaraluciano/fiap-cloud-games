using MediatR;

namespace Domain.Commands.v1.Biblioteca.ComprarJogo
{
    public class ComprarJogoCommand : IRequest<ComprarJogoCommandResponse>
    {
        public Guid IdUsuario { get; set; }
        public Guid IdJogo { get; set; }
        public decimal Preco { get; set; }
        public DateTime DtAdquirido { get; set; } = DateTime.Now;
    }
}