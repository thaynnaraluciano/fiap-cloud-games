using Infrastructure.Data.Interfaces.Promocoes;
using MediatR;

namespace Domain.Commands.v1.Promocoes.RemoverPromocao
{
    public class RemoverPromocaoCommandHandler : IRequestHandler<RemoverPromocaoCommand, Unit>  
    {
        private readonly IPromocaoRepository _promocaoRepository;

        public RemoverPromocaoCommandHandler(IPromocaoRepository promocaoRepository)
        {
            _promocaoRepository = promocaoRepository;
        }

        public async Task<Unit> Handle(RemoverPromocaoCommand request, CancellationToken cancellationToken)
        {
            var promocao = await _promocaoRepository.BuscarPorIdAsync(request.Id);
            if (promocao == null)
            {
                throw new Exception("Promoção não encontrada.");
            }

            await _promocaoRepository.RemoverAsync(promocao);
            return Unit.Value;
        }
    }
}
