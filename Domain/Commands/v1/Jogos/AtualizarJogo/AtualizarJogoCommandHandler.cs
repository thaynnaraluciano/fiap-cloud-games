using Infrastructure.Data.Interfaces.Jogos;
using MediatR;

namespace Domain.Commands.v1.Jogos.AtualizarJogo
{
    public class AtualizarJogoCommandHandler : IRequestHandler<AtualizarJogoCommand, Unit>
    {
        private readonly IJogoRepository _jogoRepository;

        public AtualizarJogoCommandHandler(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<Unit> Handle(AtualizarJogoCommand request, CancellationToken cancellationToken)
        {
            var jogoExistente = await _jogoRepository.ObterPorIdAsync(request.Id);
            
            if (jogoExistente == null)
            {
                throw new Exception($"Jogo com ID {request.Id} não encontrado.");
            }

            jogoExistente.Atualizar(request.Nome, request.Descricao, request.Preco, request.DataLancamento);

            await _jogoRepository.AtualizarAsync(jogoExistente);

            return Unit.Value;
        }
    }
}
