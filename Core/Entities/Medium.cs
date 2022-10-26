namespace Core.Entities;

public class Medium : ISearchableEntity
{
    public Guid Id { get; set; }
    public Guid GroupId { get; set; }
    public string Type { get; set; }
    public string Owner { get; set; }
    public string Description { get; set; }
    public string SerialNumber { get; set; }

    public Group Group { get; set; }

    public string[] Fields => new[]
    {
        $"Type: {Type}",
        $"Owner: {Owner}",
        $"Description: {Description}",
        $"SerialNumber: {SerialNumber}",
        $"Group Name: {Group?.Name}",
        $"Group Description: {Group?.Description}",
    };

    public string EntityType => "Medium";
}

