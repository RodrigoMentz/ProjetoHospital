namespace ProjetoHospitalWebAssembly.Layout
{
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class LayoutAppLimpeza
    {
        [Inject]
        private IUsuarioService UsuarioService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private string nomeUsuario = string.Empty;
        private UsuarioViewModel usuarioLocalStorage = new();

        protected override async Task OnInitializedAsync()
        {
            this.usuarioLocalStorage = await this.UsuarioService
                .ConsultarUsuarioLocalStorage()
                .ConfigureAwait(true);

            if (usuarioLocalStorage.Perfil.Nome != "Limpeza")
            {
                await this.UsuarioService
                    .LogoutAsync()
                    .ConfigureAwait(true);
            }

            this.nomeUsuario = this.usuarioLocalStorage.Nome ?? string.Empty;
        }

        private async Task Logout()
        {
            await this.UsuarioService
                .LogoutAsync()
                .ConfigureAwait(true);
        }

        private void NavegarParaLimpezas()
        {
            this.NavigationManager
                .NavigateTo("/quartos-para-limpar");
        }

        private void NavegarParaGeral()
        {
            this.NavigationManager
                .NavigateTo("/status-geral");
        }

        private void NavegarParaManutencoes()
        {
            this.NavigationManager
                .NavigateTo("/manutencoes");
        }
    }
}