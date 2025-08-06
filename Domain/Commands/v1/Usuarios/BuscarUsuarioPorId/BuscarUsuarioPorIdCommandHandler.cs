using AutoMapper;
using Infrastructure.Data.Interfaces.Usuarios;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Usuarios.BuscarUsuarioPorId
{
    public class BuscarUsuarioPorIdCommandHandler : IRequestHandler<BuscarUsuarioPorIdCommand, BuscarUsuarioPorIdCommandResponse>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private ILogger<BuscarUsuarioPorIdCommandHandler> _logger;

        public BuscarUsuarioPorIdCommandHandler(IUsuarioRepository usuarioRepository, IMapper mapper, ILogger<BuscarUsuarioPorIdCommandHandler> logger)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BuscarUsuarioPorIdCommandResponse> Handle(BuscarUsuarioPorIdCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Buscando o usuário {request.Id}");

            var usuario = await _usuarioRepository.ObterPorIdAsync(request.Id);

            if (usuario == null)
            {
                _logger.LogError($"Usuário com o Id {usuario.Id} não encontrado.");
                throw new KeyNotFoundException($"Usuário com o Id {usuario.Id} não encontrado.");
            }

            _logger.LogInformation($"Usuário {request.Id} encontrado");
            return _mapper.Map<BuscarUsuarioPorIdCommandResponse>(usuario);
        }
    }
}
