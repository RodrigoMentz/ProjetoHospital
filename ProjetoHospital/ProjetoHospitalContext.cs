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

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Limpeza> Limpezas { get; set; }

        public DbSet<LimpezaConcorrente> LimpezasConcorrentes { get; set; }

        public DbSet<LimpezaTerminal> LimpezasTerminais { get; set; }

        public DbSet<LimpezaEmergencial> LimpezasEmergenciais { get; set; }

        public DbSet<Manutencao> Manutencoes { get; set; }

        public DbSet<Revisao> Revisoes { get; set; }

        public DbSet<Revisao> AlteracoesDeUsuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var assembly = this.GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}