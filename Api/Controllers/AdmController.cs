using AutoMapper;
using CrossCutting.Configuration.Authorization;
using Domain.Commands.v1.Adm.AlteraStatusUser;
using Domain.Commands.v1.Adm.AtualizarUsuario;
using Domain.Commands.v1.Adm.BuscarUsuarioPorId;
using Domain.Commands.v1.Adm.CadastrarUsuario;
using Domain.Commands.v1.Adm.ListarUsuarios;
using Domain.Commands.v1.Adm.RemoverUsuario;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize(Policy = PoliticasDeAcesso.Admin)]
    public class AdmController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AdmController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        [Route(template: "AlteraStatusUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AlteraStatusUser([FromBody] AlteraUserStatusCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [Route(template: "BuscarUsuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> BuscarUsuarios()
        {
            var query = new ListarUsuariosCommand();
            var usuarios = await _mediator.Send(query);

            return Ok(usuarios);
        }

        [HttpPost]
        [Route(template: "CadastrarUsuario")]
        [ProducesResponseType(typeof(CadastrarUsuarioCommandResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CadastrarUsuario([FromBody] CadastrarUsuarioCommand command)
        {
            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
        }

        [HttpGet]
        [Route(template: "BuscarUsuarioPorId/{id}")]
        [ProducesResponseType(typeof(BuscarUsuarioPorIdCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> BuscarUsuarioPorId(Guid id)
        {
            var query = new BuscarUsuarioPorIdCommand(id);
            var usuario = await _mediator.Send(query);

            return Ok(usuario);
        }

        [HttpPut]
        [Route(template: "AtualizarUsuario/{id}")]
        [ProducesResponseType(typeof(AtualizarUsuarioCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AtualizarUsuario([FromBody] AtualizarUsuarioCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        [Route(template: "RemoverUsuario/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> RemoverUsuario(Guid id)
        {
            var command = new RemoverUsuarioCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
