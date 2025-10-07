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
            }

            this.isLoading = false;
            this.StateHasChanged();
        }
    }
}