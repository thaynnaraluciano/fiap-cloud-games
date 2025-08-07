using MediatR;
using System.Text.Json.Serialization;

namespace Domain.Commands.v1.Biblioteca.ComprarJogo
{
    public class ComprarJogoCommand : IRequest<ComprarJogoCommandResponse>
    {
        public Guid IdUsuario { get; set; }
        public Guid IdJogo { get; set; }
        [JsonIgnore]
        public decimal Preco { get; set; }
        [JsonIgnore]
        public DateTime DtAdquirido { get; set; }
    }
}