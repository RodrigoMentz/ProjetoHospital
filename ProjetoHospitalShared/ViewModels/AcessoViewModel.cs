namespace ProjetoHospitalShared.ViewModels
{
    public class AcessoViewModel
    {
        public AcessoViewModel()
        {
        }

        public AcessoViewModel(
            string token,
            string numTelefone,
            PerfilViewModel perfil,
            string usuarioId,
            string usuarioNome,
            bool numTelefoneConfirmado)
        {
            this.Token = token;
            this.NumTelefone = numTelefone;
            this.Perfil = perfil;
            this.UsuarioId = usuarioId;
            this.UsuarioNome = usuarioNome;
            this.NumTelefoneConfirmado = numTelefoneConfirmado;
        }

        public string Token { get; set; }

        public string NumTelefone { get; set; }

        public PerfilViewModel Perfil { get; set; }

        public string UsuarioId { get; set; }

        public string UsuarioNome { get; set; }

        public bool NumTelefoneConfirmado { get; set; }
    }
}