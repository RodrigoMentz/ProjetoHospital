namespace ProjetoHospitalWebAssembly.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services.Http;

    public class UsuarioService(
        IHttpService httpService)
        : IUsuarioService
    {
        public async Task<ResponseModel<List<PerfilViewModel>>> GetPerfisAsync()
        {
            var url = $"Usuario/perfis";

            var resultado = await httpService
                .GetJsonAsync<ResponseModel<List<PerfilViewModel>>>(
                    url)
                .ConfigureAwait(false);

            return resultado;
        }

        public async Task<ResponseModel> CadastrarAsync(
            UsuarioViewModel usuario)
        {
            var url = $"Usuario/cadastrar";
            var response = await httpService
                 .PostJsonAsync<UsuarioViewModel, ResponseModel>(
                     url,
                     usuario)
                 .ConfigureAwait(false);
            return response;
        }
    }
}