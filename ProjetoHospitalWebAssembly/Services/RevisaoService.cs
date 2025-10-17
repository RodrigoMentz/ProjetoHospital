namespace ProjetoHospitalWebAssembly.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services.Http;

    public class RevisaoService(
        IHttpService httpService)
        : IRevisaoService
    {
        public async Task<ResponseModel<RevisaoViewModel>> GetDetalhesDaRevisaoAsync(
            RevisaoViewModel revisao)
        {
            var url = $"Revisao/detalhes";

            var response = await httpService
                .PostJsonAsync<RevisaoViewModel, ResponseModel<RevisaoViewModel>>(
                    url,
                    revisao)
                .ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseModel<List<RevisaoViewModel>>> GetRevisoesQueNecessitamLimpezaAsync()
        {
            var url = $"Revisao/revisoes-necessitam-limpeza";

            var resultado = await httpService
                .GetJsonAsync<ResponseModel<List<RevisaoViewModel>>>(
                    url)
                .ConfigureAwait(false);

            return resultado;
        }

        public async Task<ResponseModel<List<RevisaoViewModel>>> GetRevisoesQueNecessitamLimpezaENaoForamTerminadasPeloUsuarioAsync(
            UsuarioViewModel usuario)
        {
            var url = $"Revisao/revisoes-necessitam-limpeza-e-nao-foram-terminadas";

            var resultado = await httpService
                .PostJsonAsync<UsuarioViewModel, ResponseModel<List<RevisaoViewModel>>>(
                    url,
                    usuario)
                .ConfigureAwait(false);

            return resultado;
        }

        public async Task<ResponseModel<List<NecessidadeDeRevisaoViewModel>>> ConsultarRevisoesPendentesAsync(
            UsuarioViewModel usuario)
        {
            var url = $"Revisao/revisoes-pendentes";

            var resultado = await httpService
                .PostJsonAsync<UsuarioViewModel, ResponseModel<List<NecessidadeDeRevisaoViewModel>>>(
                    url,
                    usuario)
                .ConfigureAwait(false);

            return resultado;
        }

        public async Task<ResponseModel<RevisaoViewModel>> CriarAsync(
            RevisaoViewModel revisao)
        {
            var url = $"Revisao/adicionar";

            var response = await httpService
                .PostJsonAsync<RevisaoViewModel, ResponseModel<RevisaoViewModel>>(
                    url,
                    revisao)
                .ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseModel> AtualizarAsync(
            RevisaoViewModel revisao)
        {
            var url = $"Revisao/atualizar";

            var response = await httpService
                 .PutJsonAsync<RevisaoViewModel, ResponseModel>(
                     url,
                     revisao)
                 .ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseModel> AtenderAsync(
            RevisaoViewModel revisao)
        {
            var url = $"Revisao/atender";

            var response = await httpService
                 .PutJsonAsync<RevisaoViewModel, ResponseModel>(
                     url,
                     revisao)
                 .ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseModel> FinalizarAsync(
            RevisaoViewModel revisao)
        {
            var url = $"Revisao/finalizar";

            var response = await httpService
                 .PutJsonAsync<RevisaoViewModel, ResponseModel>(
                     url,
                     revisao)
                 .ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseModel> DeletarAsync(
            RevisaoViewModel revisao)
        {
            var url = $"Revisao/deletar";

            var response = await httpService
                 .PutJsonAsync<RevisaoViewModel, ResponseModel>(
                     url,
                     revisao)
                 .ConfigureAwait(false);

            return response;
        }
    }
}