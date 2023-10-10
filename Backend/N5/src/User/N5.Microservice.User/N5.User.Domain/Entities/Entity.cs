
using N5.User.Domain.Interfaces.Entities;

namespace N5.User.Domain.Entities;

/// <summary>
/// Abstract base of Entities
/// </summary>
public abstract class Entity<TId> : IEntity<TId>
{
    public TId Id { get; set; }

    object IEntity.Id
    {
        get => Id;
        set => Id = (TId)value;
    }

    public bool Enabled { get; set; } = true;

    public int? DeleteUserId { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime LastUpdate { get; set; } = DateTime.UtcNow;
}