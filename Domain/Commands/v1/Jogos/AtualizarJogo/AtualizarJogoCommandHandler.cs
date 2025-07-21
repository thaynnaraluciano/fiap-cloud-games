using AutoMapper;
using Infrastructure.Data.Interfaces.Jogos;
using MediatR;

namespace Domain.Commands.v1.Jogos.AtualizarJogo
{
    public class AtualizarJogoCommandHandler : IRequestHandler<AtualizarJogoCommand, AtualizarJogoCommandResponse>
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IMapper _mapper;

        public AtualizarJogoCommandHandler(IJogoRepository jogoRepository, IMapper mapper)
        {
            _jogoRepository = jogoRepository;
            _mapper = mapper;
        }

        public async Task<AtualizarJogoCommandResponse> Handle(AtualizarJogoCommand request, CancellationToken cancellationToken)
        {
            var jogoExistente = await _jogoRepository.ObterPorIdAsync(request.Id);
            
            if (jogoExistente == null)
            {
                throw new Exception($"Jogo com ID {request.Id} não encontrado.");
            }

            jogoExistente.Atualizar(request.Nome, request.Descricao, request.Preco, request.DataLancamento);

            await _jogoRepository.AtualizarAsync(jogoExistente);

            return _mapper.Map<AtualizarJogoCommandResponse>(jogoExistente);
        }
    }
}
