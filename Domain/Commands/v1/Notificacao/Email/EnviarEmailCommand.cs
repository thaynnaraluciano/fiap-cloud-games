using MediatR;
using System.Text.Json.Serialization;

namespace Domain.Commands.v1.Notificacao.Email
{
    public class EnviarEmailCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public string? NomeDestinatario { get; set; }

        public string? EmailDestinatario { get; set; }

        [JsonIgnore]
        public string? Assunto { get; set; }

        [JsonIgnore]
        public string? Corpo { get; set; }

        [JsonIgnore]
        public string? TipoCorpo { get; set; }
    }
}
