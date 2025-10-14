namespace ProjetoHospital.Entities
{
    using Microsoft.AspNetCore.Identity;

    public class Usuario : IdentityUser
    {
        public Usuario()
        {
        }

        public Usuario(
            string nome,
            string phoneNumber,
            bool ativo)
        {
            this.Nome = nome;
            this.PhoneNumber = phoneNumber;
            this.Ativo = ativo;
        }

        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public virtual ICollection<UsuarioRole> UsuarioRoles { get; set; }

        public void Modify(
            string nome,
            string numeroCelular,
            bool ativo)
        {
            this.Nome = nome;
            this.PhoneNumber = numeroCelular;
            this.Ativo = ativo;
        }
    }
}