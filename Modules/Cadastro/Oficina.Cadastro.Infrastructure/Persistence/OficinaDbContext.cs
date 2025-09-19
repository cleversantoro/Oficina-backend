using Microsoft.EntityFrameworkCore;

namespace Oficina.Infrastructure.Persistence
{
    public class OficinaDbContext : DbContext
    {
        public OficinaDbContext(DbContextOptions<OficinaDbContext> options) : base(options) { }

        // Exemplo de DbSet para aquecimento
        public DbSet<Ping> Pings => Set<Ping>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ping>(cfg =>
            {
                cfg.ToTable("pings");
                cfg.HasKey(x => x.Id);
            });
        }
    }

    public class Ping
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
