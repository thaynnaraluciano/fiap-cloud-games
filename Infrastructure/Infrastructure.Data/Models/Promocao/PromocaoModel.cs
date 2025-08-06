using Infrastructure.Data.Models.Jogos;

namespace Infrastructure.Data.Models.Promocao
{
    public class PromocaoModel
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string? Nome { get; private set; }
        public decimal Desconto { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }
        public ICollection<PromocaoJogoModel> PromocaoJogos { get; private set; }  = [];

        public PromocaoModel()
        {
            
        }

        public PromocaoModel(string nome, decimal desconto, DateTime dataInicio, DateTime dataFim)
        {
            Nome = nome;
            Desconto = desconto;
            DataInicio = dataInicio;
            DataFim = dataFim;
            PromocaoJogos = new List<PromocaoJogoModel>();
        }

        public void Atualizar(string? nome, decimal? desconto, DateTime? dataInicio, DateTime? dataFim)
        {
            if (!string.IsNullOrEmpty(nome)) 
                Nome = nome;

            if (desconto != null)
                Desconto = desconto.Value;

            if (dataInicio != null)
            DataInicio = dataInicio.Value;

            if (dataFim != null)
                DataFim = dataFim.Value;
        }

        public void AdicionarJogo(IEnumerable<JogoModel> jogos)
        {
            foreach (var jogo in jogos)
                AdicionarJogo(jogo);
        }

        public void RemoverTodosJogos()
        {
            PromocaoJogos.Clear();
        }

        public void AdicionarJogo(JogoModel jogo)
        {
            if (PromocaoJogos.Any(pj => pj.JogoId == jogo.Id))
                return;

            var promocaoJogo = new PromocaoJogoModel(this, jogo);
            PromocaoJogos.Add(promocaoJogo);
        }
    }
}
