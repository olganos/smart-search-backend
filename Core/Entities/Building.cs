namespace Core.Entities;

public class Building : ISearchableEntity
{
    public Guid Id { get; set; }
    public string ShortCut { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public string[] Fields => new[]
    {
        $"ShortCat: {ShortCut}",
        $"Name: {Name}",
        $"Description: {Description}",
    };

    public string EntityType => "Building";
}

