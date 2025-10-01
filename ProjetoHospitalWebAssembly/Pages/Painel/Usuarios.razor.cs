namespace ProjetoHospitalWebAssembly.Pages.Painel
{
    using Blazored.Modal;
    using Blazored.Modal.Services;
    using Blazored.Toast.Services;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Components.Modais;

    public partial class Usuarios : ComponentBase
    {
        [Inject]
        private IModalService ModalService { get; set; }

        [Inject]
        private IToastService ToastService { get; set; }

        private bool isLoading = false;

        private List<UsuarioViewModel> usuarios = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            var perfis = new List<PerfilViewModel>
            {
                new PerfilViewModel(1, "Limpeza"),
                new PerfilViewModel(2, "Recepcão/Enfermagem"),
                new PerfilViewModel(3, "Manutenção"),
            };

            this.usuarios = new List<UsuarioViewModel>
            {
                new UsuarioViewModel(1, "João da Silva", perfis.Where(p => p.Id == 1).FirstOrDefault(), "11912345678", true),
                new UsuarioViewModel(2, "Maria Oliveira", perfis.Where(p => p.Id == 1).FirstOrDefault(), "11923456789", true),
                new UsuarioViewModel(3, "Carlos Souza", perfis.Where(p => p.Id == 2).FirstOrDefault(), "11934567890", false),
                new UsuarioViewModel(4, "Ana Pereira", perfis.Where(p => p.Id == 2).FirstOrDefault(), "11945678901", true),
                new UsuarioViewModel(5, "Pedro Lima", perfis.Where(p => p.Id == 3).FirstOrDefault(), "11956789012", true),
            };
            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task AdicionarAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

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
                        this.ToastService.ShowSuccess(
                            "Sucesso: Cadastro de usuário realizado");
                        // TODO: chamada para adicionar usuario
                        // TODO: chamada para atualizar a lista de usuarios
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
            this.isLoading = true;
            this.StateHasChanged();

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
                    var usuarioEditado = (UsuarioViewModel)retornoModal.Data;

                    if (usuarioEditado != null)
                    {
                        this.ToastService.ShowSuccess(
                            "Sucesso: Atualização de usuário realizada");
                        // TODO: chamada para editar usuario
                        // TODO: chamada para atualizar a lista de usuarios
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