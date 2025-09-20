using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oficina.Cadastro.Domain.Entities;

namespace Oficina.Cadastro.Infrastructure.Persistence.Configurations;

public class ProfissionalConfiguration : IEntityTypeConfiguration<Profissional>
{
    public void Configure(EntityTypeBuilder<Profissional> builder)
    {
        builder.ToTable("profissionais");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(x => x.Nome)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Especialidade)
            .HasMaxLength(100);
    }
}
