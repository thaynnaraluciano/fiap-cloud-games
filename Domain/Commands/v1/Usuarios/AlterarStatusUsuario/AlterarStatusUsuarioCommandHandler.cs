using AutoMapper;
using Infrastructure.Data.Interfaces.Usuarios;
using Infrastructure.Data.Models.Usuarios;
using MediatR;

namespace Domain.Commands.v1.Usuarios.AlterarStatusUsuario
{
    public class AlterarStatusUsuarioCommandHandler : IRequestHandler<AlterarStatusUsuarioCommand, AlterarStatusUsuarioCommandResponse>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public AlterarStatusUsuarioCommandHandler(IUsuarioRepository admRepository, IMapper mapper)
        {
            _usuarioRepository = admRepository;
            _mapper = mapper;
        }
        public async Task<AlterarStatusUsuarioCommandResponse> Handle(AlterarStatusUsuarioCommand request, CancellationToken cancellationToken)
        {
            var UserAlterar = _mapper.Map<UsuarioModel>(request);
            var AlteraStatusUserResult = await _usuarioRepository.AlterarStatusUsuario(UserAlterar);
            return _mapper.Map<AlterarStatusUsuarioCommandResponse>(AlteraStatusUserResult);
        }
    }
}
