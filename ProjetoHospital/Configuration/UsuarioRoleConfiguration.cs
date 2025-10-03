namespace ProjetoHospital.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ProjetoHospital.Entities;

    public class UsuarioRoleConfiguration : IEntityTypeConfiguration<UsuarioRole>
    {
        public void Configure(EntityTypeBuilder<UsuarioRole> builder)
        {
            builder.HasOne(e => e.Usuario)
                   .WithMany(e => e.UsuarioRoles)
                   .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Role)
                   .WithMany(e => e.UsuarioRoles)
                   .HasForeignKey(e => e.RoleId);
        }
    }
}