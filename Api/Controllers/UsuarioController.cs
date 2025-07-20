using Domain.Commands.v1.Usuarios.AtualizarUsuario;
using Domain.Commands.v1.Usuarios.BuscarUsuarioPorId;
using Domain.Commands.v1.Usuarios.CriarUsuario;
using Domain.Commands.v1.Usuarios.ListarUsuarios;
using Domain.Commands.v1.Usuarios.RemoverUsuario;

//using Domain.Commands.v1.Usuarios.RemoverUsuario;
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

        [HttpGet]
        //[Authorize(Policy = "PoliticaDeAdmin")]
        public async Task<IActionResult> BuscarTodos()
        {
            var query = new ListarUsuariosCommand();
            var usuarios = await _mediator.Send(query);

            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarUsuarioCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            var query = new BuscarUsuarioPorIdCommand(id);
            var usuario = await _mediator.Send(query);

            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarUsuarioCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(RemoverUsuarioCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
