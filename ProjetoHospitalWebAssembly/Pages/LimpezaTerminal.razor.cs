namespace ProjetoHospitalWebAssembly.Pages
{
    using Blazored.LocalStorage;
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class LimpezaTerminal : ComponentBase
    {
        [Parameter]
        public string IdLeito { get; set; } = string.Empty;

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

                var IdUsuarioLogado = await this.LocalStorageService
                    .GetItemAsync<string>("IdUsuario")
                    .ConfigureAwait(true);

                if (string.IsNullOrWhiteSpace(this.IdLeito) ||
                    string.IsNullOrWhiteSpace(IdUsuarioLogado))
                {
                    this.NavigationManager
                        .NavigateTo($"/inicio");

                    return;
                }

                this.limpeza.DataInicioLimpeza = DateTime.Now;
                this.limpeza.LeitoId = int.Parse(this.IdLeito);
                this.limpeza.UsuarioId = IdUsuarioLogado;

                var response = await LimpezaService
                    .CriarTerminalAsync(this.limpeza)
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