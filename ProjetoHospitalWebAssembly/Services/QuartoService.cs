namespace ProjetoHospitalWebAssembly.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services.Http;

    public class QuartoService(
        IHttpService httpService)
        : IQuartoService
    {
        public async Task<ResponseModel<List<QuartoViewModel>>> GetAsync()
        {
            var url = $"Quarto";

            var resultado = await httpService
                .GetJsonAsync<ResponseModel<List<QuartoViewModel>>>(
                    url)
                .ConfigureAwait(false);

            return resultado;
        }

        public async Task<ResponseModel> CriarAsync(QuartoViewModel quarto)
        {
            var url = $"Quarto/adicionar";

            var response = await httpService
                .PostJsonAsync<QuartoViewModel, ResponseModel>(
                    url,
                    quarto)
                .ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseModel> AtualizarAsync(QuartoViewModel quarto)
        {
            var url = $"Quarto/atualizar";

            var response = await httpService
                 .PutJsonAsync<QuartoViewModel, ResponseModel>(
                     url,
                     quarto)
                 .ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseModel> DeletarAsync(QuartoViewModel quarto)
        {
            var url = $"Quarto/deletar";

            var response = await httpService
                 .PutJsonAsync<QuartoViewModel, ResponseModel>(
                     url,
                     quarto)
                 .ConfigureAwait(false);

            return response;
        }
    }
}