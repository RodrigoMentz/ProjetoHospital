namespace ProjetoHospitalWebAssembly.Pages.App
{
    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Components;
    using ProjetoHospitalShared.ViewModels;
    using ProjetoHospitalWebAssembly.Services;

    public partial class Perfis : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IUsuarioService UsuarioService { get; set; }

        [Inject]
        private ILocalStorageService localStorageService { get; set; }

        private bool isLoading = false;

        private List<PerfilViewModel> perfis = new();

        protected override async Task OnInitializedAsync()
        {
            this.isLoading = true;
            this.StateHasChanged();

            // TODO: implementar chamada para consultar perfis
            var response = await this.UsuarioService
                .GetPerfisAsync()
                .ConfigureAwait(true);

            var perfilLocalStorage = await this.localStorageService
                .GetItemAsync<PerfilViewModel>("perfil")
                .ConfigureAwait(true);

            if (response != null && response.Success)
            {
                this.perfis = response.Data;

                var perfilDoUsuario = this.perfis
                    .FirstOrDefault(p => p.Id == perfilLocalStorage?.Id);

                await this.SelecionarAsync(
                        perfilDoUsuario)
                    .ConfigureAwait(true);
            }

            this.isLoading = false;
            this.StateHasChanged();
        }

        private async Task SelecionarAsync(PerfilViewModel perfil)
        {
            if (perfil.Nome == "Limpeza")
            {
                this.NavigationManager
                    .NavigateTo("/quartos-para-limpar");
            }
            else if (perfil.Nome == "Recepcão/Enfermagem")
            {
                this.NavigationManager
                    .NavigateTo("/painel");
            }
            //else if (perfil.Id == 3)
            //{
            //    // TODO: implementar manutencao
            //} 
        }
    }
}