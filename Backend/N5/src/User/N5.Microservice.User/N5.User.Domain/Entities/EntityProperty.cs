namespace N5.User.Domain.Entities;

/// <summary>
/// TODO: This must be in Architecture.
/// Entity for updates of properties of the entities
/// </summary>
/// <param name="Name"></param>
/// <param name="Value"></param>
public record EntityProperty(string Name, string? Value);