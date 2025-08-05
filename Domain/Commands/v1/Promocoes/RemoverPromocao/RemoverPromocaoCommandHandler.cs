using Infrastructure.Data.Interfaces.Promocoes;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Promocoes.RemoverPromocao
{
    public class RemoverPromocaoCommandHandler : IRequestHandler<RemoverPromocaoCommand, Unit>  
    {
        private readonly IPromocaoRepository _promocaoRepository;
        private readonly ILogger<RemoverPromocaoCommandHandler> _logger;

        public RemoverPromocaoCommandHandler(IPromocaoRepository promocaoRepository, ILogger<RemoverPromocaoCommandHandler> logger)
        {
            _promocaoRepository = promocaoRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(RemoverPromocaoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Removendo a promoção {request.Id}");

            var promocao = await _promocaoRepository.BuscarPorIdAsync(request.Id);
            if (promocao == null)
            {
                _logger.LogError("Promoção não encontrada");
                throw new Exception("Promoção não encontrada.");
            }

            await _promocaoRepository.RemoverAsync(promocao);

            _logger.LogInformation("Promoção removida com sucesso");
            return Unit.Value;
        }
    }
}
