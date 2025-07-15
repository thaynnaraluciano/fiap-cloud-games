using Infrastructure.Data.Interfaces.Jogos;
using Infrastructure.Data.Models.Jogos;
using MediatR;

namespace Domain.Commands.v1.Jogos.CriarJogo
{
    public class CriarJogoCommandHandler : IRequestHandler<CriarJogoCommand, Unit>
    {
        private readonly IJogoRepository _jogoRepository;

        public CriarJogoCommandHandler(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<Unit> Handle(CriarJogoCommand request, CancellationToken cancellationToken)
        {
            var jogo = new JogoModel(request.Nome, request.Descricao, request.Preco, request.DataLancamento);

            await _jogoRepository.AdicionarAsync(jogo);

            return Unit.Value;  
        }
    }
}
