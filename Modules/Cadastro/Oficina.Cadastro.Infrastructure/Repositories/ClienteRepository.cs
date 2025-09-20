using Microsoft.EntityFrameworkCore;
using Oficina.Cadastro.Domain.Entities;
using Oficina.Cadastro.Infrastructure.Persistence;

namespace Oficina.Cadastro.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly OficinaDbContext _db;
    public ClienteRepository(OficinaDbContext db) => _db = db;

    public async Task<Cliente?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _db.Clientes.FindAsync(new object?[] { id }, ct);

    public async Task<List<Cliente>> SearchAsync(string? termo, int skip, int take, CancellationToken ct = default)
    {
        var q = _db.Clientes.AsQueryable();
        if (!string.IsNullOrWhiteSpace(termo))
            q = q.Where(x => x.Nome.ToLower().Contains(termo.ToLower()) || x.Documento.Contains(termo));
        return await q.OrderBy(x => x.Nome).Skip(skip).Take(take).ToListAsync(ct);
    }

    public async Task AddAsync(Cliente entity, CancellationToken ct = default)
    {
        await _db.Clientes.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Cliente entity, CancellationToken ct = default)
    {
        _db.Clientes.Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Cliente entity, CancellationToken ct = default)
    {
        _db.Clientes.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }

    public Task<bool> DocumentoExisteAsync(string documento, Guid? ignoreId = null, CancellationToken ct = default)
    {
        var q = _db.Clientes.AsQueryable().Where(x => x.Documento == documento);
        if (ignoreId.HasValue) q = q.Where(x => x.Id != ignoreId.Value);
        return q.AnyAsync(ct);
    }
}
