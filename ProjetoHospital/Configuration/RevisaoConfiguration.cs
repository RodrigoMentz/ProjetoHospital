namespace ProjetoHospital.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ProjetoHospital.Entities;

    public class RevisaoConfiguration : IEntityTypeConfiguration<Revisao>
    {
        public void Configure(EntityTypeBuilder<Revisao> builder)
        {
            builder.ToTable("Revisoes");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Observacoes)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(r => r.NecessitaLimpeza)
                .HasDefaultValue(false)
                .IsRequired(true);

            builder.Property(l => l.DataSolicitacao)
                .IsRequired();

            builder.Property(l => l.DataInicioLimpeza)
                .IsRequired(false);

            builder.Property(l => l.DataFimLimpeza)
                .IsRequired(false);

            builder.Property(r => r.SolicitanteId)
                .IsRequired();

            builder.Property(r => r.ExecutanteId)
                .IsRequired(false);

            builder.Property(r => r.LimpezaId)
                .IsRequired();

            builder.Property(r => r.LeitoId)
                .IsRequired();

            builder.Property(r => r.SoftDelete)
                .HasDefaultValue(false);

            builder.HasOne(r => r.Leito)
                .WithMany()
                .HasForeignKey(r => r.LeitoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Limpeza)
                .WithMany()
                .HasForeignKey(r => r.LimpezaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Solicitante)
                .WithMany()
                .HasForeignKey(r => r.SolicitanteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Executante)
                .WithMany()
                .HasForeignKey(r => r.ExecutanteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}