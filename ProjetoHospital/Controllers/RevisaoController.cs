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
    public class RevisaoController(
        IRevisaoService RevisaoService)
        : ControllerBase
    {
        [HttpPost("detalhes")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<RevisaoViewModel>), 200)]
        public async Task<IActionResult> GetDetalhesDaManutencaoAsync(
            [FromBody] RevisaoViewModel revisao)
        {
            var resultado = await RevisaoService
                .GetDetalhesDaRevisaoAsync(revisao);

            return this.Ok(resultado);
        }

        [HttpGet("revisoes-necessitam-limpeza")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRevisoesQueNecessitamLimpezaAsync()
        {
            var resultado = await RevisaoService
                .GetRevisoesQueNecessitamLimpezaAsync();

            return this.Ok(resultado);
        }

        [HttpPost("revisoes-necessitam-limpeza-e-nao-foram-terminadas")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<List<RevisaoViewModel>>), 200)]
        public async Task<IActionResult> GetRevisoesQueNecessitamLimpezaENaoForamTerminadasPeloUsuarioAsync(
            [FromBody] UsuarioViewModel usuario)
        {
            var resultado = await RevisaoService
                .GetRevisoesQueNecessitamLimpezaENaoForamTerminadasPeloUsuarioAsync(
                    usuario);

            return this.Ok(resultado);
        }

        [HttpGet("revisoes-pendentes")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync()
        {
            var resultado = await RevisaoService
                .ConsultarLimpezasQuePrecisamDeRevisaoAsync();

            return this.Ok(resultado);
        }

        [HttpPost("adicionar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel<RevisaoViewModel>), 200)]
        public async Task<IActionResult> Criar(
            [FromBody] RevisaoViewModel revisao)
        {
            var resultado = await RevisaoService
                .CriarAsync(revisao);

            return this.Ok(resultado);
        }

        [HttpPut("atualizar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> Atualizar(
            [FromBody] RevisaoViewModel revisao)
        {
            var resultado = await RevisaoService
                .AtualizarAsync(revisao);

            return this.Ok(resultado);
        }

        [HttpPut("atender")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> Atender(
            [FromBody] RevisaoViewModel revisao)
        {
            var resultado = await RevisaoService
                .AtenderAsync(revisao);

            return this.Ok(resultado);
        }

        [HttpPut("finalizar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> Finalizar(
            [FromBody] RevisaoViewModel revisao)
        {
            var resultado = await RevisaoService
                .FinalizarAsync(revisao);

            return this.Ok(resultado);
        }

        [HttpPut("deletar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseModel), 200)]
        public async Task<IActionResult> Deletar(
            [FromBody] RevisaoViewModel revisao)
        {
            var resultado = await RevisaoService
                .DeletarAsync(revisao);

            return this.Ok(resultado);
        }
    }
}