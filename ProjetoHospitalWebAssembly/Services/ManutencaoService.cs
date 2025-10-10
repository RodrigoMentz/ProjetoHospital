namespace ProjetoHospitalWebAssembly.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services.Http;

    public class ManutencaoService(
        IHttpService httpService)
        : IManutencaoService
    {
        public async Task<ResponseModel<List<ManutencaoViewModel>>> GetAsync()
        {
            var url = $"Manutencao";

            var resultado = await httpService
                .GetJsonAsync<ResponseModel<List<ManutencaoViewModel>>>(
                    url)
                .ConfigureAwait(false);

            return resultado;
        }
        public async Task<ResponseModel<ManutencaoViewModel>> GetDetalhesDaManutencaoAsync(
            ManutencaoViewModel manutencao)
        {
            var url = $"Manutencao/detalhes";

            var response = await httpService
                .PostJsonAsync<ManutencaoViewModel, ResponseModel<ManutencaoViewModel>>(
                    url,
                    manutencao)
                .ConfigureAwait(false);

            return response;
        }


        public async Task<ResponseModel> CriarAsync(
            ManutencaoViewModel manutencao)
        {
            var url = $"Manutencao/adicionar";

            var response = await httpService
                .PostJsonAsync<ManutencaoViewModel, ResponseModel>(
                    url,
                    manutencao)
                .ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseModel> AtualizarAsync(
            ManutencaoViewModel manutencao)
        {
            var url = $"Manutencao/atualizar";

            var response = await httpService
                 .PutJsonAsync<ManutencaoViewModel, ResponseModel>(
                     url,
                     manutencao)
                 .ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseModel> FinalizarAsync(
            ManutencaoViewModel manutencao)
        {
            var url = $"Manutencao/finalizar";

            var response = await httpService
                 .PutJsonAsync<ManutencaoViewModel, ResponseModel>(
                     url,
                     manutencao)
                 .ConfigureAwait(false);

            return response;
        }
    }
}