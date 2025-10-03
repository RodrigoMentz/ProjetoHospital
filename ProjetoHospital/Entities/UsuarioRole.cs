namespace ProjetoHospital.Entities
{
    using Microsoft.AspNetCore.Identity;

    public class UsuarioRole : IdentityUserRole<string>
    {
        public virtual Usuario Usuario { get; set; }

        public virtual Role Role { get; set; }
    }
}