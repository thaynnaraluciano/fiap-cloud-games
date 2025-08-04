using Infrastructure.Data.Interfaces.Usuarios;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Usuarios.RemoverUsuario
{
    public class RemoverUsuarioCommandHandler : IRequestHandler<RemoverUsuarioCommand, Unit>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<RemoverUsuarioCommandHandler> _logger;

        public RemoverUsuarioCommandHandler(IUsuarioRepository usuarioRepository, ILogger<RemoverUsuarioCommandHandler> logger) 
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(RemoverUsuarioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Removendo o usuário {request.Id}");

            var usuario = await _usuarioRepository.ObterPorIdAsync(request.Id);

            if (usuario == null)
            {
                _logger.LogError($"Usuario com ID {request.Id} não encontrado.");
                throw new KeyNotFoundException($"Usuario com ID {request.Id} não encontrado.");
            }

            await _usuarioRepository.RemoverAsync(usuario);

            _logger.LogInformation($"Usuário {request.Id} removido");

            return Unit.Value;
        }
    }
}
