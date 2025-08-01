using AutoMapper;
using Domain.Enums;
using Infrastructure.Data.Interfaces.Usuarios;
using Infrastructure.Data.Models.Usuarios;
using MediatR;

namespace Domain.Commands.v1.Usuarios.CriarUsuario
{
    public class CriarUsuarioCommandHandler : IRequestHandler<CriarUsuarioCommand, CriarUsuarioCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;

        public CriarUsuarioCommandHandler(IMapper mapper, IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<CriarUsuarioCommandResponse> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuarioExistente = _usuarioRepository.ObterPorEmailAsync(request.Email);
            if (usuarioExistente != null)
            {
                throw new Exception("Já existe um usuário criado para o e-mail fornecido");
            }

            var usuario = _mapper.Map<UsuarioModel>(request);

            var todosUsuarios = await _usuarioRepository.ObterTodosAsync();

            usuario.PerfilUsuario = todosUsuarios.Any() ? 
                (int)PerfilUsuarioEnum.Usuario : (int)PerfilUsuarioEnum.Administrador;

            await _usuarioRepository.AdicionarAsync(usuario);
            return _mapper.Map<CriarUsuarioCommandResponse>(usuario);
        }
    }
}
