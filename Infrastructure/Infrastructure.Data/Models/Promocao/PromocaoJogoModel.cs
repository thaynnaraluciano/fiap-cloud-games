using Infrastructure.Data.Models.Jogos;

namespace Infrastructure.Data.Models.Promocao
{
    public class PromocaoJogoModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public JogoModel Jogo { get; set; }
        public PromocaoModel Promocao { get; set; }
        public Guid PromocaoId { get; set; }
        public Guid JogoId { get; set; }

        public PromocaoJogoModel(PromocaoModel promocao, JogoModel jogo)
        {
            Jogo = jogo;
            Promocao = promocao;
            PromocaoId = promocao.Id;
            JogoId = jogo.Id;
        }

        public PromocaoJogoModel()
        {
            
        }
    }
}
