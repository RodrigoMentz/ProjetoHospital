using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoHospital.Services;
using ProjetoHospitalShared;
using ProjetoHospitalShared.ViewModels;

namespace ProjetoHospital.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class QuartoController(
        IQuartoService QuartoService)
        : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync()
        {
            var resultado = await QuartoService
                .GetAsync();

            return this.Ok(resultado);
        }

        [HttpPost("adicionar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> Criar(
            [FromBody] QuartoViewModel quarto)
        {
            var resultado = await QuartoService
                .CriarAsync(quarto);

            return this.Ok(resultado);
        }

        [HttpPut("atualizar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> Atualizar(
            [FromBody] QuartoViewModel quarto)
        {
            var resultado = await QuartoService
                .AtualizarAsync(quarto);

            return this.Ok(resultado);
        }

        [HttpPut("deletar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> Deletar(
            [FromBody] QuartoViewModel quarto)
        {
            var resultado = await QuartoService
                .DeletarAsync(quarto);

            return this.Ok(resultado);
        }
    }
}