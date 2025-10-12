namespace ProjetoHospitalWebAssembly.Pages.App
{
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class LimpezaEmergencial : ComponentBase
    {
        [Parameter]
        public string IdLimpeza { get; set; } = string.Empty;

        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private ILimpezaService LimpezaService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private bool isLoading = false;

        private LimpezaEmergencialViewModel limpeza = new();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                await this.ConsultarLimpeza()
                    .ConfigureAwait(true);

            }
            catch (Exception e)
            {
                this.ToastService.ShowError(
                    "Erro: Erro inesperado contate o suporte");
            }

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task ConsultarLimpeza()
        {
            try
            {
                var limpeza = new LimpezaViewModel(
                    int.Parse(IdLimpeza));

                var response = await this.LimpezaService
                    .ConsultarEmergencialAsync(limpeza)
                    .ConfigureAwait(true);

                if (response.Success)
                {
                    this.limpeza = response.Data;
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
        }

        private async Task FinalizarLimpeza()
        {
            try
            {
                var response = await this.LimpezaService
                    .FinalizarEmergencialAsync(limpeza);

                if (response.Success)
                {
                    this.ToastService.ShowSuccess(
                        "Sucesso: Limpeza realizada");

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
        }
    }
}