using Infrastructure.Data.Interfaces.Usuarios;
using MediatR;

namespace Domain.Commands.v1.Usuarios.RemoverUsuario
{
    public class RemoverUsuarioCommandHandler : IRequestHandler<RemoverUsuarioCommand, Unit>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public RemoverUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Unit> Handle(RemoverUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(request.Id);

            if (usuario == null)
            {
                throw new KeyNotFoundException($"Usuario com ID {request.Id} não encontrado.");
            }

            await _usuarioRepository.RemoverAsync(usuario);

            return Unit.Value;
        }
    }
}
