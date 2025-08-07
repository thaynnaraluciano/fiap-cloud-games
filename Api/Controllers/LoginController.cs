using Domain.Commands.v1.Login;
using Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/login")]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        private AppDbContext context;

        public LoginController(IMediator mediator, AppDbContext context)
        {
            _mediator = mediator;
            this.context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(
            Summary = "Realiza login do usuário",
            Description = "Autentica um usuário com base nas credenciais fornecidas e retorna o token de acesso (JWT) em caso de sucesso."
        )]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

    }
}
