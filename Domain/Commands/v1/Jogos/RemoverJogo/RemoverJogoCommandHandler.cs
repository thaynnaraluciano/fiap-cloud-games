using Infrastructure.Data.Interfaces.Jogos;
using MediatR;

namespace Domain.Commands.v1.Jogos.RemoverJogo
{
    public class RemoverJogoCommandHandler : IRequestHandler<RemoverJogoCommand, Unit>
    {
        private readonly IJogoRepository _jogoRepository;

        public RemoverJogoCommandHandler(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<Unit> Handle(RemoverJogoCommand request, CancellationToken cancellationToken)
        {
            var jogo = await _jogoRepository.ObterPorIdAsync(request.Id);
            
            if (jogo == null)
            {
                throw new Exception($"Jogo com ID {request.Id} não encontrado.");
            }

            await _jogoRepository.RemoverAsync(jogo);
            return Unit.Value;
        }
    }
}
