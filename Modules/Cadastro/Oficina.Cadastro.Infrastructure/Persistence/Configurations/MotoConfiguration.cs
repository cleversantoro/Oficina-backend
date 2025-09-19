using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oficina.Cadastro.Domain.Entities;

namespace Oficina.Cadastro.Infrastructure.Persistence.Configurations;

public class MotoConfiguration : IEntityTypeConfiguration<Moto>
{
    public void Configure(EntityTypeBuilder<Moto> b)
    {
        b.ToTable("motos");
        b.HasKey(x => x.Id);
        b.Property(x => x.Marca).IsRequired().HasMaxLength(80);
        b.Property(x => x.Modelo).IsRequired().HasMaxLength(120);
        b.Property(x => x.Ano).IsRequired();
        b.Property(x => x.Chassi).HasMaxLength(30);
        b.Property(x => x.KmAtual);

        b.HasOne(x => x.Cliente)
            .WithMany()
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        b.HasIndex(x => x.Placa); // busca rápida por placa
    }
}
