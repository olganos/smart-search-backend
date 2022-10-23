namespace Core.Entities;

public class Building : ISearchableEntity
{
    public Guid Id { get; set; }
    public string ShortCut { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public string GetFullDescription()
    {
        return $"Bulding: {ShortCut} - {Name} - {Description}";
    }
}

