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

        [Inject]
        private IUsuarioService UsuarioService { get; set; }

        private bool isLoading = false;

        private List<ManutencaoViewModel> manutencoes = new();

        private UsuarioViewModel usuarioLocalStorage = new();

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

            this.usuarioLocalStorage = await this.UsuarioService
                .ConsultarUsuarioLocalStorage()
                .ConfigureAwait(true);

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

        private void NavegarParaRealizarManutencao(int manutencaoId)
        {
            this.NavigationManager
                .NavigateTo($"/manutencoes/realizar/{manutencaoId}");
        }
    }
}