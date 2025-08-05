using CrossCutting.Configuration.Authorization;
using Domain.Commands.v1.Promocoes.AtualizarPromocao;
using Domain.Commands.v1.Promocoes.BuscarPromocaoPorId;
using Domain.Commands.v1.Promocoes.CriarPromocao;
using Domain.Commands.v1.Promocoes.ListarPromocoes;
using Domain.Commands.v1.Promocoes.RemoverPromocao;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/promocao")]
    public class PromocaoController : ControllerBase
    {
        private IMediator _mediator;

        public PromocaoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = PoliticasDeAcesso.Admin)]
        [ProducesResponseType(typeof(IEnumerable<ListarPromocoesCommandResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ListarTodos()
        {
            var query = new ListarPromocoesCommand();
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PoliticasDeAcesso.Admin)]
        [ProducesResponseType(typeof(BuscarPromocaoPorIdCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            var promocao = new BuscarPromocaoPorIdCommand(id);
            var resultado = await _mediator.Send(promocao);

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }

        [HttpPost]
        [Authorize(Policy = PoliticasDeAcesso.Admin)]
        [ProducesResponseType(typeof(CriarPromocaoCommandResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Criar([FromBody] CriarPromocaoCommand command)
        {
            var resultado = await _mediator.Send(command);

            if (resultado == null)
                return BadRequest();

            return Created(string.Empty, resultado);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = PoliticasDeAcesso.Admin)]
        [ProducesResponseType(typeof(AtualizarPromocaoCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarPromocaoCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID da URL não confere com o corpo da requisição.");

            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = PoliticasDeAcesso.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Remover(Guid id)
        {
            var command = new RemoverPromocaoCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
