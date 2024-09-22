namespace VoidWalker.Engine.Database.Interfaces.Entities;

public interface IBaseDbEntity
{
    Guid Id { get; set; }

    DateTime CreatedAt { get; set; }

    DateTime UpdatedAt { get; set; }
}
