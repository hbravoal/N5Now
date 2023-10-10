namespace N5.User.Domain.Interfaces.Entities;

/// <summary>
/// Generic Interface for Entities
/// </summary>
public interface IEntity
{
    object Id { get; set; }
    public bool Enabled { get; set; }
    public int? DeleteUserId { get; set; }
    DateTime CreatedDate { get; set; }
    DateTime LastUpdate { get; set; }
}

/// <summary>
/// Generic Interface for Entities with Generic Id
/// </summary>
public interface IEntity<TId> : IEntity
{
    public new TId Id { get; set; }
}