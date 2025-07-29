using AutoMapper;
using Infrastructure.Data.Interfaces.Usuarios;
using MediatR;

namespace Domain.Commands.v1.Adm.ListarUsuarios
{
    public class ListarUsuariosCommandHandler : IRequestHandler<ListarUsuariosCommand, IEnumerable<ListarUsuariosCommandResponse>>
    {
        public readonly IUsuarioRepository _usuarioRepository;
        public readonly IMapper _mapper;

        public ListarUsuariosCommandHandler(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ListarUsuariosCommandResponse>> Handle(ListarUsuariosCommand request, CancellationToken cancellation)
        {
            var usuarios = await _usuarioRepository.ObterTodosAsync();
            return _mapper.Map<IEnumerable<ListarUsuariosCommandResponse>>(usuarios);
        }
    }
}
