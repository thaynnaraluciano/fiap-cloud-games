using AutoMapper;
using Infrastructure.Data.Interfaces.Promocoes;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Promocoes.BuscarPromocaoPorId
{
    public class BuscarPromocaoPorIdCommandHandler : IRequestHandler<BuscarPromocaoPorIdCommand, BuscarPromocaoPorIdCommandResponse>
    {
        private readonly IPromocaoRepository _promocaoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BuscarPromocaoPorIdCommandHandler> _logger;

        public BuscarPromocaoPorIdCommandHandler(IPromocaoRepository promocaoRepository, IMapper mapper, ILogger<BuscarPromocaoPorIdCommandHandler> logger)
        {
            _promocaoRepository = promocaoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BuscarPromocaoPorIdCommandResponse> Handle(BuscarPromocaoPorIdCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Buscando promoção {request.Id}");

            var promocao = await _promocaoRepository.BuscarPorIdAsync(request.Id);

            if (promocao == null)
            {
                _logger.LogError($"Promoção com ID {request.Id} não encontrada.");
                throw new KeyNotFoundException($"Promoção com ID {request.Id} não encontrada.");
            }

            _logger.LogInformation($"Promoção {request.Id} encontrada.");
            return _mapper.Map<BuscarPromocaoPorIdCommandResponse>(promocao);
        }
    }
}
