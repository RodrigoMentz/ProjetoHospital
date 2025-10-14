namespace ProjetoHospital.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ProjetoHospital.Entities;

    public class AlteracoesDeUsuarioConfiguration : IEntityTypeConfiguration<AlteracoesDeUsuario>
    {
        public void Configure(EntityTypeBuilder<AlteracoesDeUsuario> builder)
        {
            builder.ToTable("AlteracoesDeUsuario");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.idUsuarioExecutante)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.idUsuarioAfetado)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.Descricao)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.DataAlteracao)
                .IsRequired();

            builder.HasOne(m => m.Executante)
                .WithMany()
                .HasForeignKey(m => m.idUsuarioExecutante)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Afetado)
                .WithMany()
                .HasForeignKey(m => m.idUsuarioAfetado)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}