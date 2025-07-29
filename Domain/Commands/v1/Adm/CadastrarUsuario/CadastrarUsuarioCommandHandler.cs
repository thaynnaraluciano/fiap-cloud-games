using AutoMapper;
using Infrastructure.Data.Interfaces.Usuarios;
using Infrastructure.Data.Models.Usuarios;
using MediatR;

namespace Domain.Commands.v1.Adm.CadastrarUsuario
{
    public class CadastrarUsuarioCommandHandler : IRequestHandler<CadastrarUsuarioCommand, CadastrarUsuarioCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;

        public CadastrarUsuarioCommandHandler(IMapper mapper, IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<CadastrarUsuarioCommandResponse> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuarioExistente = _usuarioRepository.ObterPorEmailAsync(request.Email);
            if (usuarioExistente != null) 
            {
                throw new Exception("Já existe um usuário criado para o e-mail fornecido");
            }

            var usuario = _mapper.Map<UsuarioModel>(request);
            
            await _usuarioRepository.AdicionarAsync(usuario);
            return _mapper.Map<CadastrarUsuarioCommandResponse>(usuario);
        }
    }
}
