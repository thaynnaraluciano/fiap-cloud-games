using Domain.Commands.v1.Usuarios.CriarSenha;
using Domain.Commands.v1.Usuarios.CriarUsuario;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route(template: "CriarUsuario")]
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioCommand command)
        {
            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
        }

        [Route(template: "CriarSenha")]
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CriarSenha([FromBody] CriarSenhaCommand command)
        {
            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
        }
    }
}
