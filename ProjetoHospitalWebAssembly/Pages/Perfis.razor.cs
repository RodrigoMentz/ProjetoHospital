namespace ProjetoHospitalWebAssembly.Pages
{
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class Perfis : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IUsuarioService UsuarioService { get; set; }

        private bool isLoading = false;

        private List<PerfilViewModel> perfis = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            // TODO: implementar chamada para consultar perfis
            var response = await this.UsuarioService
                .GetPerfisAsync()
                .ConfigureAwait(true);

            if (response != null && response.Success)
            {
                this.perfis = response.Data;
            }

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task SelecionarAsync(PerfilViewModel perfil)
        {
            // TODO: implementar chamada para selecionar perfil
            if (perfil.Nome == "Limpeza")
            {
                this.NavigationManager
                    .NavigateTo("/quartos-para-limpar");
            }
            else if (perfil.Nome == "Recepcão/Enfermagem")
            {
                this.NavigationManager
                    .NavigateTo("/painel");
            }
            //else if (perfil.Id == 3)
            //{
            //    // TODO: implementar manutencao
            //} 
        }
    }
}