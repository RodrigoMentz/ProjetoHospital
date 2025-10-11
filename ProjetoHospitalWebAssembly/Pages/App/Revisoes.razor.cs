namespace ProjetoHospitalWebAssembly.Pages.App
{
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class Revisoes : ComponentBase
    {
        [Inject]
        private IRevisaoService RevisaoService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private IUsuarioService UsuarioService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private bool isLoading = false;

        private List<NecessidadeDeRevisaoViewModel> revisoesPendentes = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            await this.ConsultarRevisoesPendentes()
                .ConfigureAwait(true);

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task ConsultarRevisoesPendentes()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                var response = await this.RevisaoService
                    .GetRevisoesPendentesAsync()
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.revisoesPendentes = response.Data;
                }
            }
            catch (Exception e)
            {
                this.ToastService.ShowError(
                    "Erro: Erro inesperado contate o suporte");
            }

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task IniciarRevisao(NecessidadeDeRevisaoViewModel necessidadeRevisao)
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                var usuarioLocalStorage = await this.UsuarioService
                    .ConsultarUsuarioLocalStorage()
                    .ConfigureAwait(true);

                var revisao = new RevisaoViewModel(
                    string.Empty,
                    false,
                    DateTime.Now,
                    usuarioLocalStorage.Id,
                    necessidadeRevisao.LimpezaId,
                    necessidadeRevisao.LeitoId);

                var response = await RevisaoService
                    .CriarAsync(revisao)
                    .ConfigureAwait(true);

                if (necessidadeRevisao.TipoLimpeza == TipoLimpezaEnum.Concorrente)
                {
                    if (response.Success)
                    {
                        this.NavigationManager
                            .NavigateTo($"/revisar/c/{necessidadeRevisao.LimpezaId}/{response.Data.Id}");
                    }
                    else
                    {
                        this.ToastService.ShowError(
                            "Erro: Erro inesperado contate o suporte");
                    }
                
                }
                else if (necessidadeRevisao.TipoLimpeza == TipoLimpezaEnum.Terminal)
                {
                    if (response.Success)
                    {
                        this.NavigationManager
                            .NavigateTo($"/revisar/t/{necessidadeRevisao.LimpezaId}/{response.Data.Id}");
                    }
                    else
                    {
                        this.ToastService.ShowError(
                            "Erro: Erro inesperado contate o suporte");
                    }
                }
            }
            catch (Exception e)
            {
                this.ToastService.ShowError(
                    "Erro: Erro inesperado contate o suporte");
            }

            this.isLoading = false;
            this.StateHasChanged();
        }
    }
}