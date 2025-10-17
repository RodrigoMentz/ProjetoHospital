namespace ProjetoHospitalWebAssembly.Pages.App
{
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class Revisoes : ComponentBase
    {
        [Inject]
        private IRevisaoService RevisaoService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private IUsuarioService UsuarioService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private bool isLoading = false;

        private List<NecessidadeDeRevisaoViewModel> revisoesPendentes = new();
        private UsuarioViewModel usuarioLocalStorage = new UsuarioViewModel();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            this.usuarioLocalStorage = await this.UsuarioService
                .ConsultarUsuarioLocalStorage()
                .ConfigureAwait(true);

            await this.ConsultarRevisoesPendentes()
                .ConfigureAwait(true);

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task ConsultarRevisoesPendentes()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                var usuario = new UsuarioViewModel(
                    this.usuarioLocalStorage.Id,
                    this.usuarioLocalStorage.Nome,
                    this.usuarioLocalStorage.Perfil,
                    this.usuarioLocalStorage.NumeroTelefone);

                var response = await this.RevisaoService
                    .ConsultarRevisoesPendentesAsync(usuario)
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.revisoesPendentes = response.Data;
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

        private async Task IniciarRevisao(NecessidadeDeRevisaoViewModel necessidadeRevisao)
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                if (necessidadeRevisao.RevisaoId != 0)
                {
                    if (necessidadeRevisao.TipoLimpeza == TipoLimpezaEnum.Concorrente)
                    {
                        this.NavigationManager
                            .NavigateTo($"/revisar/c/{necessidadeRevisao.LimpezaId}/{necessidadeRevisao.RevisaoId}");

                        return;
                    }
                    else if (necessidadeRevisao.TipoLimpeza == TipoLimpezaEnum.Terminal)
                    {
                        this.NavigationManager
                            .NavigateTo($"/revisar/t/{necessidadeRevisao.LimpezaId}/{necessidadeRevisao.RevisaoId}");

                        return;
                    }
                    else if (necessidadeRevisao.TipoLimpeza == TipoLimpezaEnum.Emergencial)
                    {
                        this.NavigationManager
                            .NavigateTo($"/revisar/e/{necessidadeRevisao.LimpezaId}/{necessidadeRevisao.RevisaoId}");

                        return;
                    }
                }

                var revisao = new RevisaoViewModel(
                    string.Empty,
                    false,
                    DateTime.Now,
                    this.usuarioLocalStorage.Id,
                    necessidadeRevisao.LimpezaId,
                    necessidadeRevisao.LeitoId);

                var response = await RevisaoService
                    .CriarAsync(revisao)
                    .ConfigureAwait(true);

                if (necessidadeRevisao.TipoLimpeza == TipoLimpezaEnum.Concorrente)
                {
                    if (response.Success)
                    {
                        this.NavigationManager
                            .NavigateTo($"/revisar/c/{necessidadeRevisao.LimpezaId}/{response.Data.Id}");
                    }
                    else
                    {
                        this.ToastService.ShowError(
                            "Erro: Erro inesperado contate o suporte");
                    }
                
                }
                else if (necessidadeRevisao.TipoLimpeza == TipoLimpezaEnum.Terminal)
                {
                    if (response.Success)
                    {
                        this.NavigationManager
                            .NavigateTo($"/revisar/t/{necessidadeRevisao.LimpezaId}/{response.Data.Id}");
                    }
                    else
                    {
                        this.ToastService.ShowError(
                            "Erro: Erro inesperado contate o suporte");
                    }
                }
                else if (necessidadeRevisao.TipoLimpeza == TipoLimpezaEnum.Emergencial)
                {
                    if (response.Success)
                    {
                        this.NavigationManager
                            .NavigateTo($"/revisar/e/{necessidadeRevisao.LimpezaId}/{response.Data.Id}");
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