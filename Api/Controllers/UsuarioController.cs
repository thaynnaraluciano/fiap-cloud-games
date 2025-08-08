using CrossCutting.Configuration.Authorization;
using Domain.Commands.v1.Usuarios.ListarUsuarios;
using Domain.Commands.v1.Usuarios.RemoverUsuario;
using Domain.Commands.v1.Usuarios.AtualizarUsuario;
using Domain.Commands.v1.Usuarios.BuscarUsuarioPorId;
using Domain.Commands.v1.Usuarios.CriarSenha;
using Domain.Commands.v1.Usuarios.CriarUsuario;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Commands.v1.Usuarios.AlterarStatusUsuario;
using CrossCutting.Exceptions;
using System.Security.Claims;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(
            Summary = "Cria um novo usuário",
            Description = "Cria um novo usuário no sistema com os dados fornecidos no corpo da requisição. Endpoint público."
        )]
        public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioCommand command)
        {
            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
        }

        [Route(template: "CriarSenha")]
        [HttpPost]
        [Authorize(Policy = PoliticasDeAcesso.CriarSenha)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Cria a senha do usuário",
            Description = "Permite que um usuário autenticado crie sua senha inicial usando o token recebido por e-mail."
        )]
        public async Task<IActionResult> CriarSenha([FromBody] CriarSenhaCommand command)
        {
            var email = User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (email != command.Email)
                throw new ExcecaoBadRequest("Autenticação inválida para o email informado");

            var response = await _mediator.Send(command);
            return Created(string.Empty, response);
        }

        [HttpGet]
        [Route(template: "BuscarUsuarios")]
        [Authorize(Policy = PoliticasDeAcesso.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(
            Summary = "Lista todos os usuários",
            Description = "Retorna uma lista com todos os usuários cadastrados. Requer permissão de administrador."
        )]
        public async Task<IActionResult> BuscarUsuarios()
        {
            var query = new ListarUsuariosCommand();
            var usuarios = await _mediator.Send(query);

            return Ok(usuarios);
        }

        [HttpGet]
        [Route(template: "BuscarUsuarioPorId/{id}")]
        [Authorize(Policy = PoliticasDeAcesso.Admin)]
        [ProducesResponseType(typeof(BuscarUsuarioPorIdCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(
            Summary = "Busca usuário por ID",
            Description = "Retorna os dados de um usuário específico, identificado pelo GUID informado. Requer permissão de administrador."
        )]
        public async Task<IActionResult> BuscarUsuarioPorId(Guid id)
        {
            var query = new BuscarUsuarioPorIdCommand(id);
            var usuario = await _mediator.Send(query);

            return Ok(usuario);
        }

        [HttpPut]
        [Route(template: "AtualizarUsuario/{id}")]
        [Authorize(Policy = PoliticasDeAcesso.Admin)]
        [ProducesResponseType(typeof(AtualizarUsuarioCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(
            Summary = "Atualiza dados do usuário",
            Description = "Atualiza as informações de um usuário existente. Requer permissão de administrador."
        )]
        public async Task<IActionResult> AtualizarUsuario([FromBody] AtualizarUsuarioCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        [Route(template: "AlterarStatusUsuario")]
        [Authorize(Policy = PoliticasDeAcesso.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(
            Summary = "Altera o status do usuário",
            Description = "Altera o status (ativo/inativo) de um usuário. Requer permissão de administrador."
        )]
        public async Task<IActionResult> AlterarStatusUsuario([FromBody] AlterarStatusUsuarioCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        [Route(template: "RemoverUsuario/{id}")]
        [Authorize(Policy = PoliticasDeAcesso.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(
            Summary = "Remove um usuário",
            Description = "Remove um usuário do sistema com base no ID informado. Requer permissão de administrador."
        )]
        public async Task<IActionResult> RemoverUsuario(Guid id)
        {
            var command = new RemoverUsuarioCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
