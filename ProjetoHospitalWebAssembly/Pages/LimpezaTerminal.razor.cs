namespace ProjetoHospitalWebAssembly.Pages
{
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;

    public partial class LimpezaTerminal : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string IdLeito { get; set; } = string.Empty;

        private bool isLoading = false;

        private LimpezaTerminalViewModel limpeza = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            this.limpeza.LeitoId = int.Parse(this.IdLeito);

            this.isLoading = false;
            this.StateHasChanged();
        }

        private void MarcarTodosComoRealizado()
        {
            this.limpeza.TirarLixo = true;
            this.limpeza.VasoSanitario = true;
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
            this.limpeza.DataHoraFim = DateTime.Now;

            // TODO: Implementar a lógica de finalização da limpeza terminal
            this.NavigationManager
                .NavigateTo($"/quartos-para-limpar");
        }
    }
}