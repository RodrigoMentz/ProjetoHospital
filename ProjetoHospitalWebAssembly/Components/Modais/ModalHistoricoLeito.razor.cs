namespace ProjetoHospitalWebAssembly.Components.Modais
{
    using Blazored.Modal;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class ModalHistoricoLeito : ComponentBase
    {
        [CascadingParameter]
        public BlazoredModalInstance ModalInstance { get; set; }

        [Parameter]
        public int IdLeito { get; set; }

        [Inject]
        private ILimpezaService LimpezaService { get; set; }

        private bool isLoading = false;

        private List<LimpezaViewModel> limpezas = new();
        private List<LimpezaViewModel> limpezasFiltradas = new();
        private int quantidadeDias = 30;
        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            var leito = new LeitoViewModel(IdLeito);

            var response = await this.LimpezaService
                .ConsultarLimpezasDoLeitoAsync(leito)
                .ConfigureAwait(true);

            if (response != null && response.Success)
            {
                this.limpezas = response.Data;
                this.limpezasFiltradas = this.limpezas;
            }

            this.isLoading = false;
            this.StateHasChanged();
        }

        private void FiltrarPorDias(int quantidadeDias)
        {
            this.quantidadeDias = quantidadeDias;
            if (quantidadeDias == 30)
            {
                this.limpezasFiltradas = this.limpezas;
            }
            else
            {
                this.limpezasFiltradas = this.limpezas
                    .Where(l => l.DataInicioLimpeza.Date >= DateTime.Now.AddDays(-quantidadeDias).Date)
                    .ToList();
            }
        }
    }
}