using CrossCutting.Exceptions;
using Infrastructure.Data.Interfaces.Usuarios;
using Infrastructure.Services.Interfaces.v1;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Usuarios.CriarSenha
{
    public class CriarSenhaCommandHandler : IRequestHandler<CriarSenhaCommand, Unit>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICriptografiaService _criptografiaService;
        private readonly ILogger<CriarSenhaCommandHandler> _logger;

        public CriarSenhaCommandHandler(IUsuarioRepository usuarioRepository, ICriptografiaService criptografiaService, ILogger<CriarSenhaCommandHandler> logger)
        {
            _usuarioRepository = usuarioRepository;
            _criptografiaService = criptografiaService;
            _logger = logger;
        }

        public async Task<Unit> Handle(CriarSenhaCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Criando senha para o usuário {command.Email}");

            var usuario = _usuarioRepository.ObterPorEmailAsync(command.Email);

            if (usuario == null)
            {
                _logger.LogError($"Usuário não encontrado, {command.Email}");
                throw new Exception("Usuário não encontrado.");
            }

            if (usuario.ConfirmadoEm.HasValue)
            {
                _logger.LogError($"Usuário já confirmou este email, {command.Email}");
                throw new ExcecaoBadRequest("Este usuário já foi confirmado.");
            }

            usuario.SenhaSalt = _criptografiaService.SaltSenha();

            command.Senha = String.Concat(usuario.SenhaSalt, command.Senha);

            usuario.SenhaHash = _criptografiaService.HashSenha(command.Senha);
            usuario.ConfirmadoEm = DateTime.Now;

            await _usuarioRepository.AtualizarAsync(usuario);

            _logger.LogInformation($"Senha criada com sucesso para {command.Email}");

            return Unit.Value;
        }
    }
}
