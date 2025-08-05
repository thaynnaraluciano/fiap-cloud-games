using AutoMapper;
using Infrastructure.Data.Interfaces.Usuarios;
using Infrastructure.Data.Models.Usuarios;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Usuarios.AlterarStatusUsuario
{
    public class AlterarStatusUsuarioCommandHandler : IRequestHandler<AlterarStatusUsuarioCommand, AlterarStatusUsuarioCommandResponse>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AlterarStatusUsuarioCommandHandler> _logger;

        public AlterarStatusUsuarioCommandHandler(IUsuarioRepository admRepository, IMapper mapper, ILogger<AlterarStatusUsuarioCommandHandler> logger)
        {
            _usuarioRepository = admRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<AlterarStatusUsuarioCommandResponse> Handle(AlterarStatusUsuarioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Alterando o status do usuário {request.cGuid}");

            var userAlterar = _mapper.Map<UsuarioModel>(request);
            var alteraStatusUserResult = await _usuarioRepository.AlterarStatusUsuario(userAlterar);

            _logger.LogInformation($"Status alterado para o usuário {request.cGuid}");
            return _mapper.Map<AlterarStatusUsuarioCommandResponse>(alteraStatusUserResult);
        }
    }
}
