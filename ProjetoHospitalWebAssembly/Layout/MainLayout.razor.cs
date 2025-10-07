namespace ProjetoHospitalWebAssembly.Layout
{
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalWebAssembly.Services;

    public partial class MainLayout
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IUsuarioService UsuarioService { get; set; }


        private string abaAtiva = "painel";

        private void OnChangeAba(string aba)
        {
            this.abaAtiva = aba;
        }

        private async Task FazerLogout()
        {
            await this.UsuarioService
                .LogoutAsync()
                .ConfigureAwait(true);

            this.NavigationManager
                .NavigateTo("/inicio");
        }
    }
}