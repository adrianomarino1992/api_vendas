using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra;
using SistemaCompra.Application.SolicitacaoCompra.Query.ObterCompra;
using System.Threading.Tasks;
using System;

namespace SistemaCompra.API.SolicitacaoCompra
{
    public class SolicitacaoCompraController : Controller
    {
        private readonly IMediator _mediator;

        public SolicitacaoCompraController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, Route("compra/nova")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RegistrarSolicitacao([FromBody] RegistrarSolicitacaoCompraCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Sucesso)
                return Ok(result);

            if (!result.Sucesso && result.Exception is null)
                return BadRequest(result.Message);

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                return StatusCode(500, result.Exception);
            else
                return StatusCode(500, "Erro interno do servidor");

        }

        [HttpGet, Route("compra/obter/")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ObterPorId([FromQuery] ObterSolicitacaoCompraPorIdQuery query)
        {
            var result = await _mediator.Send(query);

            if (result.Sucesso)
                return Ok(result);

            if (!result.Sucesso && result.Exception is null)
                return BadRequest(result.Message);

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                return StatusCode(500, result.Exception);
            else
                return StatusCode(500, "Erro interno do servidor");

        }

    }
}
