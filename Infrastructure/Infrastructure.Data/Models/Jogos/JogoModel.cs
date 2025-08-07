using Infrastructure.Data.Models.Biblioteca;
using Infrastructure.Data.Models.Promocao;

namespace Infrastructure.Data.Models.Jogos
{
    public class JogoModel
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public DateTime DataLancamento { get; private set; }

        public ICollection<PromocaoJogoModel> PromocaoJogos { get; private set; } = new List<PromocaoJogoModel>();

        public ICollection<BibliotecaModel> Bibliotecas { get; set; } = new List<BibliotecaModel>();

        public JogoModel() { }

        public JogoModel(string nome, string descricao, decimal preco, DateTime dataLancamento)
        {
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            DataLancamento = dataLancamento;
        }

        public void Atualizar(string? nome, string? descricao, decimal? preco, DateTime? dataLancamento)
        { 
            if (!string.IsNullOrEmpty(nome))
                Nome = nome!;

            if (!string.IsNullOrEmpty(descricao))
                Descricao = descricao!;

            if (preco.HasValue && preco.Value > 0)
                Preco = preco.Value;

            if (dataLancamento != null)
                DataLancamento = dataLancamento.Value;
        }
    }
}
