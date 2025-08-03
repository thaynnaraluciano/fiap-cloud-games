namespace Infrastructure.Data.Models.Notificacao.Email
{
    public class EmailModel
    {
        public string? NomeDestinatario { get; set; }

        public string? EmailDestinatario { get; set; }

        public string? Assunto { get; set; }

        public string? Corpo { get; set; }

        public string? TipoCorpo { get; set; }
    }
}
