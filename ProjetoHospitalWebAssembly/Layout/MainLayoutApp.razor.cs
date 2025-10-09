namespace ProjetoHospitalWebAssembly.Layout
{
    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalWebAssembly.Services;

    public partial class MainLayoutApp
    {
        [Inject]
        private IUsuarioService UsuarioService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ILocalStorageService LocalStorageService { get; set; }

        private string nomeUsuario = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            this.nomeUsuario = await this.LocalStorageService
                .GetItemAsync<string>("nomeUsuario")
                .ConfigureAwait(true) ?? string.Empty;
        }

        private async Task Logout()
        {
            await this.UsuarioService
                .LogoutAsync()
                .ConfigureAwait(true);

            this.NavigationManager
                .NavigateTo("/inicio");
        }

        private void NavegarParaLimpezas()
        {
            this.NavigationManager
                .NavigateTo("/quartos-para-limpar");
        }

        private void NavegarParaGeral()
        {
            this.NavigationManager
                .NavigateTo("/");
        }

        private void NavegarParaManutencoes()
        {
            this.NavigationManager
                .NavigateTo("/manutencoes");
        }
    }
}