using AutoMapper;
using Infrastructure.Data.Interfaces.Usuarios;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Usuarios.ListarUsuarios
{
    public class ListarUsuariosCommandHandler : IRequestHandler<ListarUsuariosCommand, IEnumerable<ListarUsuariosCommandResponse>>
    {
        public readonly IUsuarioRepository _usuarioRepository;
        public readonly IMapper _mapper;
        private readonly ILogger<ListarUsuariosCommandHandler> _logger;

        public ListarUsuariosCommandHandler(IUsuarioRepository usuarioRepository, IMapper mapper, ILogger<ListarUsuariosCommandHandler> logger)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ListarUsuariosCommandResponse>> Handle(ListarUsuariosCommand request, CancellationToken cancellation)
        {
            _logger.LogInformation("Listando usuários");
            var usuarios = await _usuarioRepository.ObterTodosAsync();
            return _mapper.Map<IEnumerable<ListarUsuariosCommandResponse>>(usuarios);
        }
    }
}
