namespace Domain.Commands.v1.Promocoes.BuscarPromocaoPorId
{
    public class BuscarPromocaoPorIdCommandResponse
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public decimal Desconto { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public ICollection<Guid> JogosIds { get; set; } = [];
    }
}
