namespace ProjetoHospitalWebAssembly.Layout
{
    public partial class MainLayout
    {
        private string abaAtiva = "painel";

        private void OnChangeAba(string aba)
        {
            this.abaAtiva = aba;
        }
    }
}