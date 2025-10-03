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
    public class LeitoController(
        ILeitoService LeitoService)
        : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync()
        {
            var resultado = await LeitoService
                .GetAsync();

            return this.Ok(resultado);
        }

        [HttpPost("adicionar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> Criar(
            [FromBody] LeitoViewModel leito)
        {
            var resultado = await LeitoService
                .CriarAsync(leito);

            return this.Ok(resultado);
        }

        [HttpPut("atualizar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> Atualizar(
            [FromBody] LeitoViewModel leito)
        {
            var resultado = await LeitoService
                .AtualizarAsync(leito);

            return this.Ok(resultado);
        }

        [HttpPut("deletar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> Deletar(
            [FromBody] LeitoViewModel leito)
        {
            var resultado = await LeitoService
                .DeletarAsync(leito);

            return this.Ok(resultado);
        }
    }
}