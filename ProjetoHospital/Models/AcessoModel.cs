namespace ProjetoHospital.Models
{
    using ProjetoHospital.Entities;

    public class AcessoModel
    {
        public AcessoModel(
            string token,
            string numTelefone,
            Usuario usuario,
            Role role,
            bool numTelefoneConfirmado)
        {
            Token = token;
            NumTelefone = numTelefone;
            Usuario = usuario;
            Role = role;
            NumTelefoneConfirmado = numTelefoneConfirmado;
        }

        public string Token { get; internal set; }

        public string NumTelefone { get; internal set; }

        public Usuario Usuario { get; set; }

        public Role Role { get; set; }

        public bool NumTelefoneConfirmado { get; set; }
    }
}