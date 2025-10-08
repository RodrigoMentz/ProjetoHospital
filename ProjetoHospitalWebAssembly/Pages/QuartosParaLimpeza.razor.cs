using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using ProjetoHospitalShared;
using ProjetoHospitalShared.ViewModels;
using ProjetoHospitalWebAssembly.Services;

namespace ProjetoHospitalWebAssembly.Pages
{
    public partial class QuartosParaLimpeza
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ILimpezaService LimpezaService { get; set; }

        [Inject]
        private ISetorService SetorService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        private bool isLoading = false;

        private List<SetorViewModel> setores = new();
        private List<LeitoStatusLimpezaViewModel> leitos = new();
        private List<LeitoStatusLimpezaViewModel> leitosFiltrados = new();

        private SetorViewModel setorSelecionado = new();

        private int quantidadeLeitosParaLimpezaConcorrente = 0;
        private int quantidadeLeitosParaLimpezaTerminal = 0;

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

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

                return;
            }

                await this.Consultarleitos()
                .ConfigureAwait(true);

            this.leitosFiltrados = this.leitos;

            this.quantidadeLeitosParaLimpezaConcorrente = this.leitosFiltrados
                .Where(l => l.PrecisaLimpezaConcorrente)
                .Count();

            this.quantidadeLeitosParaLimpezaTerminal = this.leitosFiltrados
                .Where(l => l.PrecisaLimpezaTerminal)
                .Count();

            var setorTodos = new SetorViewModel(0, "Todos");
            this.setores.Insert(0, setorTodos);
            this.setorSelecionado = this.setores.First();

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task Consultarleitos()
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
                    this.leitos = response.Data
                        .Where(l => l.PrecisaLimpezaTerminal
                            || l.PrecisaLimpezaConcorrente)
                        .ToList();
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

        private void OnSetorChange(SetorViewModel setor)
        {
            this.setorSelecionado = setor;

            if (setor.Id == 0)
            {
                this.leitosFiltrados = this.leitos;

                return;
            }

            this.leitosFiltrados = this.leitos
                .Where(l => l.SetorId == setor.Id)
                .ToList();
        }

        private void LimparLeito(LeitoStatusLimpezaViewModel leito)
        {
            if (leito.PrecisaLimpezaConcorrente)
            {
                this.NavigationManager
                    .NavigateTo($"/limpeza-concorrente/{leito.LeitoId}");
            }
            else
            {
                this.NavigationManager
                    .NavigateTo($"/limpeza-terminal/{leito.LeitoId}");
            }
        }
    }
}