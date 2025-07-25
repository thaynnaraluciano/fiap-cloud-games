using MediatR;

namespace Domain.Commands.v1.Promocoes.ListarPromocoes
{
    public class ListarPromocoesCommand : IRequest<IEnumerable<ListarPromocoesCommandResponse>>
    {
    }
}
