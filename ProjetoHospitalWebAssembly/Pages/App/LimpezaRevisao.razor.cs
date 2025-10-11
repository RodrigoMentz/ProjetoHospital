namespace ProjetoHospitalWebAssembly.Pages.App
{
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class LimpezaRevisao : ComponentBase
    {
        [Parameter]
        public string IdRevisao { get; set; } = string.Empty;

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IRevisaoService RevisaoService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        private bool isLoading = false;

        private RevisaoViewModel revisao = new();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                var revisaoComId = new RevisaoViewModel(int.Parse(IdRevisao));
                var response = await this.RevisaoService
                    .GetDetalhesDaRevisaoAsync(revisaoComId)
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.revisao = response.Data;
                }
                else
                {
                    this.ToastService.ShowError(
                        "Erro: Erro inesperado contate o suporte");
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

        private async Task FinalizarRevisao()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                this.revisao.DataFimLimpeza = DateTime.Now;

                var response = await this.RevisaoService
                    .FinalizarAsync(this.revisao)
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.ToastService.ShowSuccess(
                         "Sucesso: Limpeza finalizada com sucesso");

                    this.NavigationManager
                        .NavigateTo($"/quartos-para-limpar");
                }
                else
                {
                    this.ToastService.ShowError(
                        "Erro: Erro inesperado contate o suporte");
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