namespace Domain.Commands.v1.Jogos.ListarJogosCommand
{
    public class ListarJogoCommandResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public DateTime DataLancamento { get; set; }
    }
}
