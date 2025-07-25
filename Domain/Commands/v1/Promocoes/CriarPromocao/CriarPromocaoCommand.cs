using MediatR;

namespace Domain.Commands.v1.Promocoes.CriarPromocao
{
    public class CriarPromocaoCommand : IRequest<CriarPromocaoCommandResponse>
    {
        public string? Nome { get; set; }
        public decimal Desconto { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }   
        public ICollection<Guid> JogosIds { get; set; } = [];
    }
}
