using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oficina.Cadastro.Domain.Entities;

namespace Oficina.Cadastro.Infrastructure.Persistence.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> b)
    {
        b.ToTable("clientes");
        b.HasKey(x => x.Id);
        b.Property(x => x.Nome).IsRequired().HasMaxLength(160);
        b.Property(x => x.Documento).IsRequired().HasMaxLength(20);
        b.Property(x => x.Endereco).HasMaxLength(300);
        b.Property(x => x.CriadoEm).IsRequired();
        b.HasIndex(x => x.Documento).IsUnique();
    }
}
