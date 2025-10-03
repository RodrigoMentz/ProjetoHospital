using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoHospital.Entities;

namespace ProjetoHospital.Configuration
{
    public class LeitoConfiguration : IEntityTypeConfiguration<Leito>
    {
        public void Configure(EntityTypeBuilder<Leito> builder)
        {
            builder.ToTable("Leitos");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(l => l.Ativo)
                .IsRequired();

            builder.HasOne(l => l.Quarto)
                .WithMany(q => q.Leitos)
                .HasForeignKey(l => l.IdQuarto)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}