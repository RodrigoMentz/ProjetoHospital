namespace ProjetoHospital.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ProjetoHospital.Entities;

    public class QuartoConfiguration : IEntityTypeConfiguration<Quarto>
    {
        public void Configure(EntityTypeBuilder<Quarto> builder)
        {
            builder.ToTable("Quartos");

            builder.HasKey(q => q.Id);

            builder.Property(q => q.Nome)
                .IsRequired() 
                .HasMaxLength(100);

            builder.Property(q => q.Capacidade)
                .IsRequired();

            builder.Property(q => q.Ativo)
                .IsRequired();

            builder.HasOne(q => q.Setor)
                .WithMany()
                .HasForeignKey(q => q.IdSetor)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}