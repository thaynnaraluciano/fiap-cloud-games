using AutoMapper;
using Domain.Enums;
using Infrastructure.Data.Interfaces.Usuarios;
using Infrastructure.Data.Models.Usuarios;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Usuarios.CriarUsuario
{
    public class CriarUsuarioCommandHandler : IRequestHandler<CriarUsuarioCommand, CriarUsuarioCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<CriarUsuarioCommandHandler> _logger;

        public CriarUsuarioCommandHandler(IMapper mapper, IUsuarioRepository usuarioRepository, ILogger<CriarUsuarioCommandHandler> logger)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        public async Task<CriarUsuarioCommandResponse> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Criando novo usuário");

            var usuarioExistente = _usuarioRepository.ObterPorEmailAsync(request.Email);
            if (usuarioExistente != null)
            {
                _logger.LogError($"Já existe um usuário criado para o email {request.Email}");
                throw new Exception("Já existe um usuário criado para o e-mail fornecido");
            }

            var usuario = _mapper.Map<UsuarioModel>(request);

            var qtdUsuariosCadatrados = _usuarioRepository.ObterQtdUsuariosCadastradosAsync();

            usuario.PerfilUsuario = qtdUsuariosCadatrados > 0 ? 
                (int)PerfilUsuarioEnum.Usuario : (int)PerfilUsuarioEnum.Administrador;

            await _usuarioRepository.AdicionarAsync(usuario);

            _logger.LogInformation("Usuário cadastrado com sucesso");
            return _mapper.Map<CriarUsuarioCommandResponse>(usuario);
        }
    }
}
