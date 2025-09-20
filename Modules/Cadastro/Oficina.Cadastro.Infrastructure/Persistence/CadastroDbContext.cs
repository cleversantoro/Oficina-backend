using Microsoft.EntityFrameworkCore;
using Oficina.Cadastro.Domain.Entities;

namespace Oficina.Cadastro.Infrastructure.Persistence;

public class CadastroDbContext : DbContext
{
    public const string Schema = "cadastro";

    public CadastroDbContext(DbContextOptions<CadastroDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Moto> Motos => Set<Moto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CadastroDbContext).Assembly);

        // Exemplo de conversores simples para VOs
        modelBuilder.Entity<Cliente>().OwnsOne(x => x.Email, b =>
        {
            b.Property(p => p.Value).HasColumnName("Email").HasMaxLength(160);
        });

        modelBuilder.Entity<Cliente>().OwnsOne(x => x.Telefone, b =>
        {
            b.Property(p => p.Value).HasColumnName("Telefone").HasMaxLength(20);
        });

        modelBuilder.Entity<Moto>().OwnsOne(x => x.Placa, b =>
        {
            b.Property(p => p.Value).HasColumnName("Placa").HasMaxLength(8);
        });
    }
}
