using Microsoft.EntityFrameworkCore;
using Oficina.Cadastro.Domain.Entities;
using Oficina.Cadastro.Infrastructure.Persistence.Configurations;
using Npgsql; // opcional se precisar de mapeios extras
using NpgsqlTypes;
using Oficina.Cadastro.Domain;

namespace Oficina.Cadastro.Infrastructure.Persistence;

public class OficinaDbContext : DbContext
{
    public const string Schema = "oficina";

    public OficinaDbContext(DbContextOptions<OficinaDbContext> options) : base(options) { }

    public DbSet<Moto> Motos => Set<Moto>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Profissional> Profissionais => Set<Profissional>();
    public DbSet<Servico> Servicos => Set<Servico>();
    public DbSet<OrdemServico> OrdensServico => Set<OrdemServico>();
    public DbSet<ItemOrdemServico> ItensOrdemServico => Set<ItemOrdemServico>();
    public DbSet<Peca> Pecas => Set<Peca>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto"); // para gen_random_uuid()

        modelBuilder.ApplyConfiguration(new MotoConfiguration());
        modelBuilder.ApplyConfiguration(new ClienteConfiguration());
        modelBuilder.ApplyConfiguration(new ProfissionalConfiguration());
        modelBuilder.ApplyConfiguration(new ServicoConfiguration());
        modelBuilder.ApplyConfiguration(new PecaConfiguration());
        modelBuilder.ApplyConfiguration(new OrdemServicoConfiguration());
        modelBuilder.ApplyConfiguration(new ItemOrdemServicoConfiguration());
    }
}
