namespace ProjetoHospitalWebAssembly.Pages
{
    using Microsoft.AspNetCore.Components;

    public partial class Home : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private string numeroTelefone = string.Empty;
        private string senha = string.Empty;
        private bool exibirSenha = false;

        private void LoginAsync()
        {
            // TODO: Implementar a lógica de autenticação aqui
            this.NavigationManager
                .NavigateTo("/perfis");
        }
    }
}