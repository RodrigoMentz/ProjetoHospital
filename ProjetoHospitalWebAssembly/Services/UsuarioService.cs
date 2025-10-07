namespace ProjetoHospitalWebAssembly.Services
{
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services.Http;

    public class UsuarioService(
        IHttpService httpService)
        : IUsuarioService
    {
        public async Task<ResponseModel<List<UsuarioViewModel>>> GetUsuariosAsync()
        {
            var url = $"Usuario";

            var response = await httpService
                .GetJsonAsync<ResponseModel<List<UsuarioViewModel>>>(
                    url)
                .ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseModel<List<PerfilViewModel>>> GetPerfisAsync()
        {
            var url = $"Usuario/perfis";

            var response = await httpService
                .GetJsonAsync<ResponseModel<List<PerfilViewModel>>>(
                    url)
                .ConfigureAwait(false);

            return response;
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

        public async Task<ResponseModel> AtualizarAsync(
            UsuarioViewModel usuario)
        {
            var url = $"Usuario/atualizar";
            var response = await httpService
                 .PostJsonAsync<UsuarioViewModel, ResponseModel>(
                     url,
                     usuario)
                 .ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseModel> ResetarSenhaAsync(
            ResetarSenhaViewModel resetarSenha)
        {
            var url = $"Usuario/resetar-senha";
            var response = await httpService
                 .PostJsonAsync<ResetarSenhaViewModel, ResponseModel>(
                     url,
                     resetarSenha)
                 .ConfigureAwait(false);

            return response;
        }
    }
}