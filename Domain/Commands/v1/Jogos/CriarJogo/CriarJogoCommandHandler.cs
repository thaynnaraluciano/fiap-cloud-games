using AutoMapper;
using Infrastructure.Data.Interfaces.Jogos;
using Infrastructure.Data.Models.Jogos;
using MediatR;

namespace Domain.Commands.v1.Jogos.CriarJogo
{
    public class CriarJogoCommandHandler : IRequestHandler<CriarJogoCommand, CriarJogoCommandResponse>
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IMapper _mapper;

        public CriarJogoCommandHandler(IJogoRepository jogoRepository, IMapper mapper)
        {
            _jogoRepository = jogoRepository;
            _mapper = mapper;
        }

        public async Task<CriarJogoCommandResponse> Handle(CriarJogoCommand request, CancellationToken cancellationToken)
        {
            var jogo = _mapper.Map<JogoModel>(request);

            await _jogoRepository.AdicionarAsync(jogo);
            return _mapper.Map<CriarJogoCommandResponse>(jogo);
        }
    }
}
