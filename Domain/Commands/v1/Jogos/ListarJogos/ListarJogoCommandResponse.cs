namespace Domain.Commands.v1.Jogos.ListarJogos
{
    public class ListarJogoCommandResponse
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataLancamento { get; set; }
    }
}
