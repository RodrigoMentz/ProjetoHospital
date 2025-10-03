namespace ProjetoHospital.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ProjetoHospital.Services;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SetorController(
        ISetorService SetorService)
        : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync()
        {
            var resultado = await SetorService
                .GetAsync();

            return this.Ok(resultado);
        }

        [HttpPost("adicionar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> Criar(
            [FromBody] SetorViewModel setor)
        {
            var resultado = await SetorService
                .CriarAsync(setor);

            return this.Ok(resultado);
        }

        [HttpPut("atualizar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> Atualizar(
            [FromBody] SetorViewModel setor)
        {
            var resultado = await SetorService
                .AtualizarAsync(setor);

            return this.Ok(resultado);
        }

        [HttpPut("deletar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> Deletar(
            [FromBody] SetorViewModel setor)
        {
            var resultado = await SetorService
                .DeletarAsync(setor);

            return this.Ok(resultado);
        }
    }
}