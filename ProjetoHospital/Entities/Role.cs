namespace ProjetoHospital.Entities
{
    using Microsoft.AspNetCore.Identity;

    public class Role : IdentityRole<string>
    {
        public virtual ICollection<UsuarioRole> UsuarioRoles { get; set; }
    }
}