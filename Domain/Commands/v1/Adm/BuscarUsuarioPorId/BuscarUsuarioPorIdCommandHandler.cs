using AutoMapper;
using Infrastructure.Data.Interfaces.Usuarios;
using MediatR;

namespace Domain.Commands.v1.Adm.BuscarUsuarioPorId
{
    public class BuscarUsuarioPorIdCommandHandler : IRequestHandler<BuscarUsuarioPorIdCommand, BuscarUsuarioPorIdCommandResponse>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public BuscarUsuarioPorIdCommandHandler(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<BuscarUsuarioPorIdCommandResponse> Handle(BuscarUsuarioPorIdCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(request.Id);

            if (usuario == null)
            {
                throw new KeyNotFoundException($"Usuário com o Id {usuario.Id} não encontrado.");
            }

            return _mapper.Map<BuscarUsuarioPorIdCommandResponse>(usuario);
        }
    }
}
