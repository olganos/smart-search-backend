namespace Core.Entities;

public interface ISearchableEntity
{
    Guid Id { get; set; }

    string GetFullDescription();
}

