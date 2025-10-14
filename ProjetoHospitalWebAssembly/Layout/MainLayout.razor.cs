namespace ProjetoHospitalWebAssembly.Layout
{
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalWebAssembly.Services;

    public partial class MainLayout
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IUsuarioService UsuarioService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var usuarioLocalStorage = await UsuarioService
                    .ConsultarUsuarioLocalStorage()
                    .ConfigureAwait(true);

                if (usuarioLocalStorage.Perfil.Nome != "Recepcão/Enfermagem")
                {
                    await this.UsuarioService
                        .LogoutAsync()
                        .ConfigureAwait(true);
                }
            }
            catch (Exception e)
            {
                this.ToastService.ShowError(
                    "Erro: Erro inesperado contate o suporte");
            }
        }
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