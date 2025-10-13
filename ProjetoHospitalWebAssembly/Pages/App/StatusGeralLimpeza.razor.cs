namespace ProjetoHospitalWebAssembly.Pages.App
{
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class StatusGeralLimpeza : ComponentBase
    {
        [Inject]
        private ILimpezaService LimpezaService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private ISetorService SetorService { get; set; }

        private bool isLoading = false;

        private List<LeitoStatusLimpezaViewModel> statusLeitos = new();
        private List<LeitoStatusLimpezaViewModel> statusLeitosFiltrados = new();
        private List<SetorViewModel> setores = new();
        private SetorViewModel setorSelecionado = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            await this.ConsultarStatus()
                .ConfigureAwait(true);

            await this.ConsultarSetores()
                .ConfigureAwait(true);

            var setorTodos = new SetorViewModel(0, "Todos");
            this.setores.Insert(0, setorTodos);
            this.setorSelecionado = this.setores.First();

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task ConsultarSetores()
        {
            try
            {
                var responseSetores = await this.SetorService
                    .GetAsync()
                    .ConfigureAwait(true);

                if (responseSetores != null && responseSetores.Success)
                {
                    this.setores = responseSetores.Data;
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

        private async Task ConsultarStatus()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                var response = await this.LimpezaService
                    .ConsultarListaStatusLimpezaAsync()
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.statusLeitos = response.Data;
                    this.statusLeitosFiltrados = this.statusLeitos;
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

        private void OnFiltroChange(SetorViewModel setor)
        {
            this.setorSelecionado = setor;

            var statusLeitosFiltradosSetor = this.statusLeitos;

            if (setor.Id != 0)
            {
                statusLeitosFiltradosSetor = this.statusLeitos
                    .Where(l => l.SetorId == setor.Id)
                    .ToList();
            }

            this.statusLeitosFiltrados = statusLeitosFiltradosSetor;
        }
    }
}