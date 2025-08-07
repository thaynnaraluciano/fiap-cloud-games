using CrossCutting.Configuration.Authorization;
using Domain.Commands.v1.Jogos.AtualizarJogo;
using Domain.Commands.v1.Jogos.BuscarJogo;
using Domain.Commands.v1.Jogos.ListarJogos;
using Domain.Commands.v1.Jogos.CriarJogo;
using Domain.Commands.v1.Jogos.RemoverJogo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Commands.v1.Jogos.BuscarJogoPorId;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/jogo")]
    public class JogoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JogoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = PoliticasDeAcesso.Usuario)]
        [ProducesResponseType(typeof(IEnumerable<ListarJogoCommandResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(
            Summary = "Obtém todos os jogos",
            Description = "Este endpoint retorna os dados completos de todos os jogos cadastrados, sem filtros."
        )]
        public async Task<IActionResult> BuscarTodos()
        {
            var query = new ListarJogosCommand();
            var jogos = await _mediator.Send(query);

            return Ok(jogos);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PoliticasDeAcesso.Usuario)]
        [ProducesResponseType(typeof(BuscarJogoPorIdCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(
            Summary = "Obtém um jogo por ID",
            Description = "Retorna os detalhes de um jogo específico com base no identificador GUID fornecido."
        )]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            var query = new BuscarJogoPorIdCommand(id);
            var jogo = await _mediator.Send(query);
            return Ok(jogo);
        }

        [HttpPost]
        [Authorize(Policy = PoliticasDeAcesso.Admin)]
        [ProducesResponseType(typeof(CriarJogoCommandResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [SwaggerOperation(
            Summary = "Cria um novo jogo",
            Description = "Cria um novo registro de jogo com os dados fornecidos no corpo da requisição. Requer permissão de administrador."
        )]

        public async Task<IActionResult> Criar([FromBody] CriarJogoCommand command)
        {
            var resultado = await _mediator.Send(command);
            return Created(string.Empty, resultado);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = PoliticasDeAcesso.Admin)]
        [ProducesResponseType(typeof(AtualizarJogoCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Atualiza um jogo existente",
            Description = "Atualiza os dados de um jogo com base no ID informado na URL. Requer permissão de administrador."
        )]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarJogoCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID do corpo e da URL não coincidem.");

            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = PoliticasDeAcesso.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Remove um jogo",
            Description = "Remove permanentemente um jogo do sistema com base no ID informado. Requer permissão de administrador."
        )]
        public async Task<IActionResult> Remover(Guid id)
        {
            var command = new RemoverJogoCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
