namespace ProjetoHospitalWebAssembly.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services.Http;

    public class LeitoService(
        IHttpService httpService)
        : ILeitoService
    {
        public async Task<ResponseModel<List<LeitoViewModel>>> GetAsync()
        {
            var url = $"Leito";

            var resultado = await httpService
                .GetJsonAsync<ResponseModel<List<LeitoViewModel>>>(
                    url)
                .ConfigureAwait(false);

            return resultado;
        }

        public async Task<ResponseModel> CriarAsync(LeitoViewModel leito)
        {
            var url = $"Leito/adicionar";

            var response = await httpService
                .PostJsonAsync<LeitoViewModel, ResponseModel>(
                    url,
                    leito)
                .ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseModel> AtualizarAsync(LeitoViewModel leito)
        {
            var url = $"Leito/atualizar";

            var response = await httpService
                 .PutJsonAsync<LeitoViewModel, ResponseModel>(
                     url,
                     leito)
                 .ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseModel> DeletarAsync(LeitoViewModel leito)
        {
            var url = $"Leito/deletar";

            var response = await httpService
                 .PutJsonAsync<LeitoViewModel, ResponseModel>(
                     url,
                     leito)
                 .ConfigureAwait(false);

            return response;
        }
    }
}