namespace ProjetoHospitalWebAssembly.Pages.App
{
    using Blazored.LocalStorage;
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class QuartosParaLimpeza : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private ILimpezaService LimpezaService { get; set; }

        [Inject]
        private ISetorService SetorService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private ILocalStorageService LocalStorageService { get; set; }

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

        private async Task LimparLeito(LeitoStatusLimpezaViewModel leito)
        {
            try
            {
                this.isLoading = false;
                this.StateHasChanged();

                var IdUsuarioLogado = await this.LocalStorageService
                    .GetItemAsync<string>("IdUsuario")
                    .ConfigureAwait(true);

                if (leito.LeitoId == 0
                    || string.IsNullOrWhiteSpace(IdUsuarioLogado))
                {
                    this.NavigationManager
                        .NavigateTo($"/inicio");

                    return;
                }

                if (leito.PrecisaLimpezaConcorrente)
                {
                    var limpeza = new LimpezaConcorrenteViewModel();

                    limpeza.DataInicioLimpeza = DateTime.Now;
                    limpeza.LeitoId = leito.LeitoId;
                    limpeza.UsuarioId = IdUsuarioLogado;

                    var response = await LimpezaService
                        .CriarConcorrenteAsync(limpeza)
                        .ConfigureAwait(true);

                    if (response.Success)
                    {
                        limpeza.Id = response.Data.Id;

                        this.NavigationManager
                            .NavigateTo($"/limpeza-concorrente/{limpeza.Id}");
                    }
                    else
                    {
                        this.ToastService.ShowError(
                            "Erro: Erro inesperado contate o suporte");
                    } 
                }
                else if (leito.PrecisaLimpezaTerminal)
                {
                    var limpeza = new LimpezaTerminalViewModel();

                    limpeza.DataInicioLimpeza = DateTime.Now;
                    limpeza.LeitoId = leito.LeitoId;
                    limpeza.UsuarioId = IdUsuarioLogado;

                    var response = await LimpezaService
                        .CriarTerminalAsync(limpeza)
                        .ConfigureAwait(true);

                    if (response.Success)
                    {
                       limpeza.Id = response.Data.Id;

                        this.NavigationManager
                            .NavigateTo($"/limpeza-terminal/{limpeza.Id}");
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

            this.isLoading = false;
            this.StateHasChanged();
        }
    }
}