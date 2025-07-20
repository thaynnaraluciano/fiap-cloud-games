using AutoMapper;
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
            //TO DO - confirmar qual a chave única pra colocar na validação de usuário existente

            var usuario = _mapper.Map<UsuarioModel>(request);
            
            await _usuarioRepository.AdicionarAsync(usuario);
            return _mapper.Map<CriarUsuarioCommandResponse>(usuario);
        }
    }
}
