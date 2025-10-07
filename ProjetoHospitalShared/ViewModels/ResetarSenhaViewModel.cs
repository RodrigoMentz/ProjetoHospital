namespace ProjetoHospitalShared.ViewModels
{
    public class ResetarSenhaViewModel
    {
        public ResetarSenhaViewModel(
            string senha,
            string confirmarSenha)
        {
            this.Senha = senha;
            this.ConfirmarSenha = confirmarSenha;
        }

        public string Id { get; set; }

        public string Senha { get; set; }

        public string ConfirmarSenha { get; set; }
    }
}