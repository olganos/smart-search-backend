namespace Core.Entities;

public class Group : ISearchableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public string GetFullDescription()
    {
        return $"Group: {Name} - {Description}";
    }
}

