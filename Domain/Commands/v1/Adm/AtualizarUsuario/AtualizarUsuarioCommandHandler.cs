using AutoMapper;
using Infrastructure.Data.Interfaces.Usuarios;
using MediatR;

namespace Domain.Commands.v1.Adm.AtualizarUsuario
{
    public class AtualizarUsuarioCommandHandler : IRequestHandler<AtualizarUsuarioCommand, AtualizarUsuarioCommandResponse>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public AtualizarUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<AtualizarUsuarioCommandResponse> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuarioExistente = await _usuarioRepository.ObterPorIdAsync(request.Id);

            if (usuarioExistente == null)
            {
                throw new Exception($"Usuário com ID {request.Id} não encontrado.");
            }

            usuarioExistente.Atualizar(request.Nome, request.Email, (int)request.PerfilUsuario);

            await _usuarioRepository.AtualizarAsync(usuarioExistente);

            return _mapper.Map<AtualizarUsuarioCommandResponse>(usuarioExistente);
        }
    }
}
