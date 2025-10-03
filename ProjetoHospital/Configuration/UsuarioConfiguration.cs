using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoHospital.Entities;

namespace ProjetoHospital.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(us => us.Id);

            builder.Property(e => e.Ativo)
                .IsRequired();
        }
    }
}