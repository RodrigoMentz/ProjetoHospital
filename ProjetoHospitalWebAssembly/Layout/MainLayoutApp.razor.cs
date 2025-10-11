namespace ProjetoHospitalWebAssembly.Layout
{
    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
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
        private UsuarioViewModel usuarioLocalStorage = new();

        protected override async Task OnInitializedAsync()
        {
            this.usuarioLocalStorage = await this.UsuarioService
                .ConsultarUsuarioLocalStorage()
                .ConfigureAwait(true);

            this.nomeUsuario = this.usuarioLocalStorage.Nome ?? string.Empty;
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

        private bool MostrarMenuInferior()
        {
            if (this.usuarioLocalStorage.Perfil != null
                && !string.IsNullOrEmpty(this.usuarioLocalStorage.Perfil.Nome)
                && this.usuarioLocalStorage.Perfil.Nome == "Limpeza")
            {
                return true;
            }

            return false;
        }
    }
}