namespace ProjetoHospital.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ProjetoHospital.Entities;
    using ProjetoHospitalShared;

    public class LimpezaConfiguration : IEntityTypeConfiguration<Limpeza>
    {
        public void Configure(EntityTypeBuilder<Limpeza> builder)
        {
            builder.ToTable("Limpezas");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.DataInicioLimpeza)
                .IsRequired();

            builder.Property(l => l.Revisado)
                .HasDefaultValue(false);

            builder.Property(l => l.LeitoId)
                .IsRequired();

            builder.Property(l => l.UsuarioId)
                .IsRequired();

            builder
                .HasOne(l => l.Leito)
                .WithMany()
                .HasForeignKey(l => l.LeitoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(l => l.Usuario)
                .WithMany()
                .HasForeignKey(l => l.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasDiscriminator<TipoLimpezaEnum>("TipoLimpeza")
                .HasValue<LimpezaConcorrente>(TipoLimpezaEnum.Concorrente)
                .HasValue<LimpezaTerminal>(TipoLimpezaEnum.Terminal)
                .HasValue<LimpezaEmergencial>(TipoLimpezaEnum.Emergencial);
        }
    }
}