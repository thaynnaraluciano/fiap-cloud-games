namespace Domain.Commands.v1.Jogos.CriarJogo
{
    public class CriarJogoCommandResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
    }
}
