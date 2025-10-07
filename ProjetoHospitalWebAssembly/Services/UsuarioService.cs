namespace ProjetoHospitalWebAssembly.Services
{
    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Components.Authorization;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services.Http;
    using System.Reflection.Metadata;

    public class UsuarioService(
        IHttpService httpService,
        AuthenticationStateProvider authenticationStateProvider,
        ILocalStorageService localStorageService)
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

        public async Task<ResponseModel<AcessoViewModel>> LoginAsync(
            LoginViewModel login)
        {
            var url = $"Usuario/login";
            var response = await httpService
                 .PostJsonAsync<LoginViewModel, ResponseModel<AcessoViewModel>> (
                     url,
                     login)
                 .ConfigureAwait(false);

            if (response.Success)
            {
                await localStorageService
                    .SetItemAsync(
                        "authToken",
                        response.Data.Token)
                    .ConfigureAwait(false);

                httpService.SetBearerToken(response.Data.Token);

                ((ApiAuthenticationStateProvider)authenticationStateProvider)
                    .MarkUserAsAuthenticated(response.Data.Token);

                await localStorageService
                    .SetItemAsync(
                        "nomeUsuario",
                        response.Data.UsuarioNome)
                    .ConfigureAwait(false);

                await localStorageService
                    .SetItemAsync(
                        "IdUsuario",
                        response.Data.UsuarioId)
                    .ConfigureAwait(false);

                await localStorageService
                    .SetItemAsync(
                        "perfil",
                        response.Data.Perfil)
                    .ConfigureAwait(false);
            }

            return response;
        }

        public async Task LogoutAsync()
        {
            httpService.RemoveBearerToken();

            await localStorageService
                .RemoveItemAsync("authToken")
                .ConfigureAwait(false);

            await localStorageService.ClearAsync();

            await ((ApiAuthenticationStateProvider)authenticationStateProvider)
                .MarkUserAsLoggedOut()
                .ConfigureAwait(false);
        }
    }
}