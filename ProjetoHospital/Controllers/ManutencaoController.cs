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
    public class ManutencaoController(
        IManutencaoService ManutencaoService)
        : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync()
        {
            var resultado = await ManutencaoService
                .GetAsync();

            return this.Ok(resultado);
        }

        [HttpPost("detalhes")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<ManutencaoViewModel>), 200)]
        public async Task<IActionResult> GetDetalhesDaManutencaoAsync(
            [FromBody] ManutencaoViewModel manutencao)
        {
            var resultado = await ManutencaoService
                .GetDetalhesDaManutencaoAsync(manutencao);

            return this.Ok(resultado);
        }

        [HttpPost("adicionar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> Criar(
            [FromBody] ManutencaoViewModel manutencao)
        {
            var resultado = await ManutencaoService
                .CriarAsync(manutencao);

            return this.Ok(resultado);
        }

        [HttpPut("atualizar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> Atualizar(
            [FromBody] ManutencaoViewModel manutencao)
        {
            var resultado = await ManutencaoService
                .AtualizarAsync(manutencao);

            return this.Ok(resultado);
        }
    }
}