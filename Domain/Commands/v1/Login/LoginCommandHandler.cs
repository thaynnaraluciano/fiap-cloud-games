using CrossCutting.Configuration.Extensoes;
using CrossCutting.Exceptions;
using Domain.Enums;
using Infrastructure.Data.Interfaces.Usuarios;
using Infrastructure.Services.Interfaces.v1;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands.v1.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly ILogger<LoginCommandHandler> _logger;

        private readonly ITokenService _tokenService;
        private readonly ICriptografiaService _criptografiaService;
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginCommandHandler(
            ILogger<LoginCommandHandler> logger,
            ICriptografiaService criptografiaService,
            ITokenService tokenService,
            IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _criptografiaService = criptografiaService;
            _tokenService = tokenService;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommand command, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Usuário iniciando login");

            var user = _usuarioRepository.ObterPorEmailAsync(command.Email!);
            var hashSenha = _criptografiaService.HashSenha(command.Senha);

            if (user == null || !user.Ativo || hashSenha != user.Senha)
                throw new ExcecaoNaoAutorizado("Não foi possível prosseguir com o login, verifique suas credenciais e se a conta está ativa.");

            _logger.LogInformation($"Gerando token jwt para {command.Email}");

            var token = _tokenService.GerarToken(command.Email!, ((PerfilUsuarioEnum)user!.PerfilUsuario).ObterDescricao());

            _logger.LogInformation("Usuário logado com sucesso");

            return new LoginCommandResponse(token);
        }
    }
}
