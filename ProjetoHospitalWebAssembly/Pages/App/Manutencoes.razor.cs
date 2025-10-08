namespace ProjetoHospitalWebAssembly.Pages.App
{
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class Manutencoes : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IManutencaoService ManutencaoService { get; set; }

        private List<ManutencaoViewModel> manutencoes = new();

        protected override async Task OnInitializedAsync()
        {
            var response = await this.ManutencaoService
                .GetAsync()
                .ConfigureAwait(true);

            if (response != null && response.Success)
            {
                this.manutencoes = response.Data;
            }
        }

        private void NavegarParaCriacao()
        {
            this.NavigationManager
                .NavigateTo("/manutencoes/criar");
        }
    }
}