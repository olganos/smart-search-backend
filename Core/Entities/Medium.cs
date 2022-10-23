namespace Core.Entities;

public class Medium : ISearchableEntity
{
    public Guid Id { get; set; }
    public string GroupId { get; set; }
    public string Type { get; set; }
    public string Owner { get; set; }
    public string Description { get; set; }
    public string SerialNumber { get; set; }

    public string GetFullDescription()
    {
        return $"Medium: {Type} - {Owner} - {Description} - {SerialNumber}";
    }
}

