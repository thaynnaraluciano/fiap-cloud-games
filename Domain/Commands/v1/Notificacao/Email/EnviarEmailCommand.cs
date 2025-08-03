using MediatR;

namespace Domain.Commands.v1.Notificacao.Email
{
    public class EnviarEmailCommand : IRequest<Unit>
    {
        public string? NomeDestinatario { get; set; }

        public string? EmailDestinatario { get; set; }

        public string? Assunto { get; set; }

        public string? Corpo { get; set; }

        public string? TipoCorpo { get; set; }
    }
}
