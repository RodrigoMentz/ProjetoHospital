namespace ProjetoHospitalShared.ViewModels
{
    public class ResetarSenhaViewModel
    {
        public ResetarSenhaViewModel()
        {
        }

        public ResetarSenhaViewModel(
            string idUsuario,
            string senha,
            string confirmarSenha)
        {
            this.Id = idUsuario;
            this.Senha = senha;
            this.ConfirmarSenha = confirmarSenha;
        }

        public ResetarSenhaViewModel(
            string senha,
            string confirmarSenha)
        {
            this.Senha = senha;
            this.ConfirmarSenha = confirmarSenha;
        }

        public string? Id { get; set; }

        public string Senha { get; set; }

        public string ConfirmarSenha { get; set; }
    }
}