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
        private NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        private ILeitoService LeitoService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        private bool isLoading = false;

        private List<SetorViewModel> setores = new()
        {
            new SetorViewModel(1, "SUS"),
            new SetorViewModel(2, "Maternidade"),
            new SetorViewModel(3, "Pediatria"),
            new SetorViewModel(4, "Emergência"),
            new SetorViewModel(5, "Sala vermelha"),
            new SetorViewModel(6, "Bloco cirúrgico"),
        };
        private List<LeitoViewModel> leitos = new();
        private List<LeitoViewModel> leitosFiltrados = new();

        private SetorViewModel setorSelecionado = new();

        private int quantidadeLeitosParaLimpezaConcorrente = 0;
        private int quantidadeLeitosParaLimpezaTerminal = 0;

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            await this.Consultarleitos()
                .ConfigureAwait(true);

            this.leitosFiltrados = this.leitos;

            //this.quantidadeLeitosParaLimpezaConcorrente = this.leitosFiltrados
            //    .Where(l => l.TipoLimpeza == TipoLimpezaEnum.Concorrente)
            //    .Count();

            //this.quantidadeLeitosParaLimpezaTerminal = this.leitosFiltrados
            //    .Where(l => l.TipoLimpeza == TipoLimpezaEnum.Terminal)
            //    .Count();

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

                var response = await this.LeitoService
                .GetAsync()
                .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.leitos = response.Data;
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
                .Where(l => l.Quarto.IdSetor == setor.Id)
                .ToList();
        }

        private void LimparLeito(LeitoViewModel leito)
        {
            //if (leito.TipoLimpeza == TipoLimpezaEnum.Concorrente)
            //{
            //    this.NavigationManager
            //        .NavigateTo($"/limpeza-concorrente/{leito.Id}");
            //}
            //else
            //{
            //    this.NavigationManager
            //        .NavigateTo($"/limpeza-terminal/{leito.Id}");
            //}
        }
    }
}