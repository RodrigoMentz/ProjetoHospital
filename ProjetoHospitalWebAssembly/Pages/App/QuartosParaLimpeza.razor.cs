namespace ProjetoHospitalWebAssembly.Pages.App
{
    using Blazored.LocalStorage;
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared;
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
        private IUsuarioService UsuarioService { get; set; }

        private bool isLoading = false;

        private List<SetorViewModel> setores = new();
        private List<LeitoStatusLimpezaViewModel> leitos = new();
        private List<LeitoStatusLimpezaViewModel> leitosFiltrados = new();
        private List<LimpezaViewModel> limpezasNaoEncerradas = new();
        private List<LimpezaViewModel> limpezasNaoEncerradasFiltradas = new();

        private SetorViewModel setorSelecionado = new();
        private UsuarioViewModel usuarioLocalStorage = new();

        private int quantidadeLeitosParaLimpezaConcorrente = 0;
        private int quantidadeLeitosParaLimpezaTerminal = 0;

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            var responseSetores = await this.SetorService
                .GetAsync()
                .ConfigureAwait(true);

            this.usuarioLocalStorage = await UsuarioService
                .ConsultarUsuarioLocalStorage()
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

            await ConsultarLimpezasNaoEncerradasDoUsuario()
                .ConfigureAwait(true);

            this.leitosFiltrados = this.leitos;
            this.limpezasNaoEncerradasFiltradas = this.limpezasNaoEncerradas;

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

        private async Task ConsultarLimpezasNaoEncerradasDoUsuario()
        {
            var response = await this.LimpezaService
                .ConsultarLimpezasNaoEncerradasDoUsuario(
                    this.usuarioLocalStorage)
                .ConfigureAwait(true);

            if (response != null && response.Success)
            {
                this.limpezasNaoEncerradas = response.Data;
            }
            else
            {
                this.ToastService.ShowError(
                    "Erro: Erro inesperado contate o suporte");

                return;
            }
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

            this.limpezasNaoEncerradasFiltradas = this.limpezasNaoEncerradas
                .Where(l => l.IdSetor == setor.Id)
                .ToList();
        }

        private async Task LimparLeito(LeitoStatusLimpezaViewModel leito)
        {
            try
            {
                this.isLoading = false;
                this.StateHasChanged();

                if (leito.LeitoId == 0)
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
                    limpeza.UsuarioId = this.usuarioLocalStorage.Id;

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
                    limpeza.UsuarioId = this.usuarioLocalStorage.Id;

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

        private void ContinuarLimpeza(LimpezaViewModel limpeza)
        {
            if (limpeza.TipoLimpeza == ProjetoHospitalShared.TipoLimpezaEnum.Concorrente)
            {
                this.NavigationManager
                            .NavigateTo($"/limpeza-concorrente/{limpeza.Id}");
            }
            else if (limpeza.TipoLimpeza == ProjetoHospitalShared.TipoLimpezaEnum.Terminal)
            {
                this.NavigationManager
                    .NavigateTo($"/limpeza-terminal/{limpeza.Id}");
            }
        }
    }
}