namespace ProjetoHospitalWebAssembly.Pages.App
{
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class RealizarRevisao : ComponentBase
    {
        [Parameter]
        public string Tipo { get; set; } = string.Empty;

        [Parameter]
        public string IdLimpeza { get; set; } = string.Empty;

        [Parameter]
        public string IdRevisao { get; set; } = string.Empty;

        [Inject]
        private IRevisaoService RevisaoService { get; set; }

        [Inject]
        private ILimpezaService LimpezaService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private bool isLoading = false;

        private TipoLimpezaEnum tipoLimpeza;
        private LimpezaConcorrenteViewModel limpezaConcorrente = new();
        private LimpezaTerminalViewModel limpezaTerminal = new();
        private LimpezaEmergencialViewModel limpezaEmergencial = new();
        private string observacoes = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            if (Tipo == string.Empty
                || IdLimpeza == string.Empty
                || IdRevisao == string.Empty) {
                this.ToastService.ShowError(
                    "Erro: Erro ao identificar limpeza");

                this.NavigationManager
                    .NavigateTo($"/revisoes");
            }

            await ConsultarLimpeza()
                .ConfigureAwait(true);

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task ConsultarLimpeza()
        {
            try
            {
                var limpeza = new LimpezaViewModel(
                    int.Parse(IdLimpeza));

                if (Tipo == "c")
                {
                    this.tipoLimpeza = TipoLimpezaEnum.Concorrente;

                    var response = await this.LimpezaService
                        .ConsultarConcorrenteAsync(limpeza)
                        .ConfigureAwait(true);

                    if (response.Success)
                    {
                        this.limpezaConcorrente = response.Data;
                    }
                    else
                    {
                        this.ToastService.ShowError(
                            "Erro: Erro inesperado contate o suporte");
                    }
                }
                else if (Tipo == "t")
                {
                    this.tipoLimpeza = TipoLimpezaEnum.Terminal;

                    var response = await this.LimpezaService
                        .ConsultarTerminalAsync(limpeza)
                        .ConfigureAwait(true);

                    if (response.Success)
                    {
                        this.limpezaTerminal = response.Data;
                    }
                    else
                    {
                        this.ToastService.ShowError(
                            "Erro: Erro inesperado contate o suporte");
                    }
                }
                else if (Tipo == "e")
                {
                    this.tipoLimpeza = TipoLimpezaEnum.Emergencial;

                    var response = await this.LimpezaService
                        .ConsultarEmergencialAsync(limpeza)
                        .ConfigureAwait(true);

                    if (response.Success)
                    {
                        this.limpezaEmergencial = response.Data;
                    }
                    else
                    {
                        this.ToastService.ShowError(
                            "Erro: Erro inesperado contate o suporte");
                    }
                }
            }
            catch (Exception e)
            {
                this.ToastService.ShowError(
                    "Erro: Erro inesperado contate o suporte");
            }
        }

        private async Task FinalizarRevisao(bool necessitaLimpeza)
        {
            try
            {
                var revisao = new RevisaoViewModel(
                    int.Parse(this.IdRevisao));

                revisao.NecessitaLimpeza = necessitaLimpeza;
                revisao.Observacoes = observacoes;

                var response = await this.RevisaoService
                    .AtualizarAsync(revisao);

                if (response.Success)
                {
                    this.ToastService.ShowSuccess(
                        "Sucesso: Revisão realizada");

                    this.NavigationManager
                        .NavigateTo($"/revisoes");
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