namespace ProjetoHospitalWebAssembly.Pages
{
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;

    public partial class Perfis : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private bool isLoading = false;

        private List<PerfilViewModel> perfis = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            // TODO: implementar chamada para consultar perfis
            this.perfis = new List<PerfilViewModel>
            {
                new PerfilViewModel(1, "Limpeza"),
                new PerfilViewModel(2, "Recepcão/Enfermagem"),
                new PerfilViewModel(3, "Manutenção"),
            };

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task SelecionarAsync(PerfilViewModel perfil)
        {
            // TODO: implementar chamada para selecionar perfil
            if (perfil.Id == 1)
            {
                this.NavigationManager
                    .NavigateTo("/quartos-para-limpar");
            }
            else if (perfil.Id == 2)
            {
                this.NavigationManager
                    .NavigateTo("/painel");
            }
            else if (perfil.Id == 3)
            {
                // TODO: implementar manutencao
            } 
        }
    }
}