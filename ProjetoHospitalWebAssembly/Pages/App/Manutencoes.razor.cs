namespace ProjetoHospitalWebAssembly.Pages.App
{
    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class Manutencoes : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IManutencaoService ManutencaoService { get; set; }

        [Inject]
        private ILocalStorageService localStorageService { get; set; }

        private bool isLoading = false;

        private List<ManutencaoViewModel> manutencoes = new();

        private string idUsuarioLocalStorage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            var response = await this.ManutencaoService
                .GetAsync()
                .ConfigureAwait(true);

            if (response != null && response.Success)
            {
                this.manutencoes = response.Data;
            }

            this.idUsuarioLocalStorage = await this.localStorageService
                .GetItemAsync<string>("IdUsuario")
                .ConfigureAwait(true);

            if (idUsuarioLocalStorage == null)
            {
                this.NavigationManager
                    .NavigateTo($"/inicio");
            }

            this.isLoading = false;
            this.StateHasChanged();
        }

        private void NavegarParaCriacao()
        {
            this.NavigationManager
                .NavigateTo("/manutencoes/criar");
        }

        private void NavegarParaEdicao(int manutencaoId)
        {
            this.NavigationManager
                .NavigateTo($"/manutencoes/editar/{manutencaoId}");
        }
    }
}