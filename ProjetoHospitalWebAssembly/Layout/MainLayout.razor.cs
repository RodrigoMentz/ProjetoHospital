namespace ProjetoHospitalWebAssembly.Layout
{
    using Microsoft.AspNetCore.Components;

    public partial class MainLayout
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private string abaAtiva = "painel";

        private void OnChangeAba(string aba)
        {
            this.abaAtiva = aba;
        }

        private void FazerLogout()
        {
            // TODO: logica para logout
            this.NavigationManager
                .NavigateTo("/inicio");
        }
    }
}