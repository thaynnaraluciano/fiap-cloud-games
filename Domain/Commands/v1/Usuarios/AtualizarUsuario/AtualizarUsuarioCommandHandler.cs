using AutoMapper;
using Infrastructure.Data.Interfaces.Usuarios;
using Infrastructure.Data.Models.Usuarios;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Domain.Commands.v1.Usuarios.AtualizarUsuario
{
    public class AtualizarUsuarioCommandHandler : IRequestHandler<AtualizarUsuarioCommand, AtualizarUsuarioCommandResponse>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private ILogger<AtualizarUsuarioCommandHandler> _logger;

        public AtualizarUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IMapper mapper, ILogger<AtualizarUsuarioCommandHandler> logger)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AtualizarUsuarioCommandResponse> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Atualizando usuário {request.Id}");

            var usuario = await _usuarioRepository.ObterPorIdAsync(request.Id);
            if (usuario == null)
            {
                _logger.LogError($"Usuário com ID {request.Id} não encontrado.");
                throw new Exception($"Usuário com ID {request.Id} não encontrado.");
            }

            var usuarioComMesmoEmail = _usuarioRepository.ObterPorEmailAsync(request.Email!);
            if (usuarioComMesmoEmail != null)
            {
                _logger.LogError($"Já existe um usuário criado para o e-mail {request.Email}.");
                throw new Exception("Já existe um usuário criado para o e-mail fornecido");
            }

            var usuarioRequest = _mapper.Map<UsuarioModel>(request);

            usuario.Atualizar(request.Nome, request.Email, (int)request.PerfilUsuario);

            await _usuarioRepository.AtualizarAsync(usuario);

            _logger.LogInformation($"Usuário atualizado {request.Id}");

            return _mapper.Map<AtualizarUsuarioCommandResponse>(usuario);
        }
    }
}
