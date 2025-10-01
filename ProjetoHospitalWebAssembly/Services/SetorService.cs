namespace ProjetoHospitalWebAssembly.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services.Http;

    public class SetorService(
        IHttpService httpService)
        : ISetorService
    {

        public async Task<ResponseModel<List<SetorViewModel>>> GetAsync()
        {
            var url = $"Setor";

            var resultado = await httpService
                .GetJsonAsync <ResponseModel<List<SetorViewModel>>> (
                    url)
                .ConfigureAwait(false);

            return resultado;
        }

        public async Task<ResponseModel> CriarAsync(SetorViewModel setor)
        {
            var url = $"Setor/adicionar";

            var response = await httpService
                .PostJsonAsync<SetorViewModel, ResponseModel>(
                    url,
                    setor)
                .ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseModel> AtualizarAsync(SetorViewModel setor)
        {
            var url = $"Setor/atualizar";

            var response = await httpService
                 .PutJsonAsync<SetorViewModel, ResponseModel>(
                     url,
                     setor)
                 .ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseModel> DeletarAsync(SetorViewModel setor)
        {
            var url = $"Setor/deletar";

            var response = await httpService
                 .PutJsonAsync<SetorViewModel, ResponseModel>(
                     url,
                     setor)
                 .ConfigureAwait(false);

            return response;
        }
    }
}