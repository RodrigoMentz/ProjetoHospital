namespace ProjetoHospitalWebAssembly.Pages.App
{
    using Blazored.LocalStorage;
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class LimpezaTerminal : ComponentBase
    {
        [Parameter]
        public string IdLimpeza { get; set; } = string.Empty;

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ILimpezaService LimpezaService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private ILocalStorageService LocalStorageService { get; set; }

        private bool isLoading = false;

        private LimpezaTerminalViewModel limpeza = new();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                var idUsuarioLogado = await this.LocalStorageService
                   .GetItemAsync<string>("IdUsuario")
                   .ConfigureAwait(true);

                if (string.IsNullOrWhiteSpace(idUsuarioLogado))
                {
                    this.NavigationManager
                        .NavigateTo($"/inicio");

                    return;
                }

                this.limpeza.Id = int.Parse(IdLimpeza);
                this.limpeza.UsuarioId = idUsuarioLogado;
            }
            catch (Exception e)
            {
                this.ToastService.ShowError(
                    "Erro: Erro inesperado contate o suporte");
            }

            this.isLoading = false;
            this.StateHasChanged();
        }

        private void MarcarTodosComoRealizado()
        {
            this.limpeza.TirarLixo = true;
            this.limpeza.LimparVasoSanitario = true;
            this.limpeza.LimparBox = true;
            this.limpeza.RevisarMofo = true;
            this.limpeza.LimparPia = true;
            this.limpeza.LimparCama = true;
            this.limpeza.LimparEscadaCama = true;
            this.limpeza.LimparMesaCabeceira = true;
            this.limpeza.LimparArmario = true;
            this.limpeza.RecolherRoupaSuja = true;
            this.limpeza.RevisarPapelToalhaEHigienico = true;
            this.limpeza.LimparDispensers = true;
            this.limpeza.LimparLixeira = true;
            this.limpeza.LimparTeto = true;
            this.limpeza.LimparParedes = true;
            this.limpeza.LimparChao = true;
        }

        private async Task FinalizarLimpezaAsync()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                this.limpeza.DataFimLimpeza = DateTime.Now;

                var response = await LimpezaService
                    .FinalizarTerminalAsync(this.limpeza)
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.ToastService.ShowSuccess(
                        "Sucesso: Limpeza finalizada com sucesso");

                    await Task.Delay(3000)
                        .ConfigureAwait(true);

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