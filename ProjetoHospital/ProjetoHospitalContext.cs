namespace ProjetoHospital
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using ProjetoHospital.Entities;

    public partial class ProjetoHospitalContext : IdentityDbContext
        <
            Usuario,
            Role,
            string,
            IdentityUserClaim<string>,
            UsuarioRole,
            IdentityUserLogin<string>,
            IdentityRoleClaim<string>,
            IdentityUserToken<string>
        >
    {
        public ProjetoHospitalContext(DbContextOptions<ProjetoHospitalContext> options)
            : base(options)
        {
        }

        public DbSet<Setor> Setores { get; set; }
        public DbSet<Quarto> Quartos { get; set; }

        public DbSet<Leito> Leitos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var assembly = this.GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}