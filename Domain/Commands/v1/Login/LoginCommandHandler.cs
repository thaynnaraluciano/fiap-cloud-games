using CrossCutting.Exceptions;
using Domain.Enums;
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
        // TO DO: injetar dependência do repositório de usuários

        public LoginCommandHandler(
            ILogger<LoginCommandHandler> logger,
            ICriptografiaService criptografiaService,
            ITokenService tokenService)
        {
            _logger = logger;
            _criptografiaService = criptografiaService;
            _tokenService = tokenService;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommand command, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Usuário iniciando login");

            //TO DO: consultar usuário utilizando o repositório
            var user = new
            {
                Senha = "senhaDoUsuario",
                Perfil = PerfilUsuario.Usuario
            };

            var hashSenha = _criptografiaService.HashSenha(command.Senha);

            if (user == null || hashSenha != user.Senha)
                throw new ExcecaoNaoAutorizado("Usuário ou senha inválidos.");

            _logger.LogInformation($"Gerando token jwt para {command.Email}");

            var token = _tokenService.GerarToken(command.Email!, user.Perfil.ToString());

            _logger.LogInformation("Usuário logado com sucesso");

            return new LoginCommandResponse(token);
        }
    }
}
