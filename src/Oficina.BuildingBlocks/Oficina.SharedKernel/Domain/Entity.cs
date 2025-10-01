namespace Oficina.SharedKernel.Domain;
public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }
    public void Touch() => UpdatedAt = DateTime.UtcNow;
}
