namespace ProjetoHospitalWebAssembly.Layout
{
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class LayoutAppRevisao
    {
        [Inject]
        private IUsuarioService UsuarioService { get; set; }

        private string nomeUsuario = string.Empty;
        private UsuarioViewModel usuarioLocalStorage = new();

        protected override async Task OnInitializedAsync()
        {
            this.usuarioLocalStorage = await this.UsuarioService
                .ConsultarUsuarioLocalStorage()
                .ConfigureAwait(true);

            if (this.usuarioLocalStorage.Perfil.Nome != "Inspeção da limpeza")
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
    }
}