namespace ProjetoHospital.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ProjetoHospital.Entities;

    public class LimpezaEmergencialConfiguration : IEntityTypeConfiguration<LimpezaEmergencial>
    {
        public void Configure(EntityTypeBuilder<LimpezaEmergencial> builder)
        {
            builder.Property(l => l.DataInicioLimpeza);

            builder.Property(l => l.Descricao)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(l => l.UsuarioId)
                .IsRequired(false);

            builder.Property(m => m.IdSolicitante)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(m => m.Solicitante)
                .WithMany()
                .HasForeignKey(m => m.IdSolicitante)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}