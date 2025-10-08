namespace ProjetoHospital.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ProjetoHospital.Entities;

    public class ManutencaoConfiguration : IEntityTypeConfiguration<Manutencao>
    {
        public void Configure(EntityTypeBuilder<Manutencao> builder)
        {
            builder.ToTable("Manutencoes");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.NomeSolicitante)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.ContatoSolicitante)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.Turno)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.Descricao)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(m => m.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.DataDeSolicitacao)
                .IsRequired();

            builder.Property(m => m.NomeExecutante)
                .HasMaxLength(100);

            builder.Property(m => m.ContatoExecutante)
                .HasMaxLength(50);

            builder.Property(m => m.TurnoExecutante)
                .HasMaxLength(50);

            builder.Property(m => m.DataDeConclusao)
                .IsRequired(false);

            builder.HasOne(m => m.Setor)
                .WithMany(s => s.Manutencoes)
                .HasForeignKey(m => m.SetorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}