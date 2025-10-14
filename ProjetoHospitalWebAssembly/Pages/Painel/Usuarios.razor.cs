namespace ProjetoHospitalWebAssembly.Pages.Painel
{
    using Blazored.Modal;
    using Blazored.Modal.Services;
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Components.Modais;
    using ProjetoHospitalWebAssembly.Services;

    public partial class Usuarios : ComponentBase
    {
        [Inject]
        private IModalService ModalService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        [Inject]
        private IUsuarioService UsuarioService { get; set; }

        private bool isLoading = false;

        private List<UsuarioViewModel> usuarios = new();
        private UsuarioViewModel usuario = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            await this.ConsultarAsync()
                .ConfigureAwait(true);

            this.usuario = await this.UsuarioService
                .ConsultarUsuarioLocalStorage()
                .ConfigureAwait(true);

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task ConsultarAsync()
        {
            try
            {
                this.isLoading = true;
                this.StateHasChanged();

                var response = await this.UsuarioService
                    .GetUsuariosAsync()
                    .ConfigureAwait(true);

                if (response != null && response.Success)
                {
                    this.usuarios = response.Data;
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

        private async Task AdicionarAsync()
        {
            try
            {
                var options = new ModalOptions
                {
                    Position = ModalPosition.Middle,
                    Size = ModalSize.Large,
                };

                var retornoModal = await this.ModalService
                    .Show<ModalCadastroUsuario>(
                        "Cadastrar usuário",
                        options)
                    .Result
                    .ConfigureAwait(true);

                if (!retornoModal.Cancelled)
                {
                    var novoUsuario = (UsuarioViewModel)retornoModal.Data;

                    if (novoUsuario != null)
                    {
                        this.isLoading = true;
                        this.StateHasChanged();

                        novoUsuario.idUsuarioExecutante = this.usuario.Id;

                        var response = await this.UsuarioService
                            .CadastrarAsync(novoUsuario)
                            .ConfigureAwait(true);

                        if (response != null && response.Success)
                        {
                            this.ToastService.ShowSuccess(
                            "Sucesso: Cadastro de usuário realizado");
                        }
                        else if (response != null && response.Notifications.Any())
                        {
                            foreach (var notification in response.Notifications)
                            {
                                this.ToastService.ShowError(
                                    $"Erro: {notification.Message}");
                            }
                        }

                        await this.ConsultarAsync()
                            .ConfigureAwait(true);
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

        private async Task EditarAsync(UsuarioViewModel usuarioParaEdicao)
        {
            try
            {
                var options = new ModalOptions
                {
                    Position = ModalPosition.Middle,
                    Size = ModalSize.Large,
                };

                var parametros = new ModalParameters();

                parametros.Add(
                    nameof(ModalCadastroUsuario.Id),
                    usuarioParaEdicao.Id);
                parametros.Add(
                    nameof(ModalCadastroUsuario.Nome),
                    usuarioParaEdicao.Nome);
                parametros.Add(
                    nameof(ModalCadastroUsuario.Perfil),
                    usuarioParaEdicao.Perfil);
                parametros.Add(
                    nameof(ModalCadastroUsuario.NumeroTelefone),
                    usuarioParaEdicao.NumeroTelefone);
                parametros.Add(
                    nameof(ModalCadastroUsuario.Ativo),
                    usuarioParaEdicao.Ativo);

                var retornoModal = await this.ModalService
                    .Show<ModalCadastroUsuario>(
                        "Editar usuário",
                        parametros,
                        options)
                    .Result
                    .ConfigureAwait(true);

                if (!retornoModal.Cancelled)
                {
                    this.isLoading = true;
                    this.StateHasChanged();

                    var usuarioEditado = (UsuarioViewModel)retornoModal.Data;

                    if (usuarioEditado != null)
                    {
                        usuarioEditado.idUsuarioExecutante = this.usuario.Id;

                        var response = await this.UsuarioService
                            .AtualizarAsync(usuarioEditado)
                            .ConfigureAwait(true);

                        if (response != null && response.Success)
                        {
                            this.ToastService.ShowSuccess(
                                "Sucesso: Atualização de usuário realizada");
                        }
                        else if (response != null && response.Notifications.Any())
                        {
                            foreach (var notification in response.Notifications)
                            {
                                this.ToastService.ShowError(
                                    $"Erro: {notification.Message}");
                            }
                        }

                        await this.ConsultarAsync()
                           .ConfigureAwait(true);
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