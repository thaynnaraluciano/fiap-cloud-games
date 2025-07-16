namespace Domain.Commands.v1.Jogos.AtualizarJogo
{
    public class AtualizarJogoCommandResponse
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public decimal Preco { get; set; }
    }
}
