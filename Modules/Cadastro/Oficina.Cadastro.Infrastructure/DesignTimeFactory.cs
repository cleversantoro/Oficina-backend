using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Oficina.Cadastro.Infrastructure.Persistence;

namespace Oficina.Cadastro.Infrastructure
{
    public class DesignTimeFactory : IDesignTimeDbContextFactory<OficinaDbContext>
    {
        public OficinaDbContext CreateDbContext(string[] args)
        {
            var cfg = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../ApiGateway/Oficina.Api"))
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var builder = new DbContextOptionsBuilder<OficinaDbContext>();
            builder.UseNpgsql(cfg.GetConnectionString("OficinaDb"),
                sql => sql.MigrationsAssembly(typeof(OficinaDbContext).Assembly.FullName));

            return new OficinaDbContext(builder.Options);
        }
    }
}
