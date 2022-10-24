namespace Core.Entities;

public class Lock : ISearchableEntity
{
    public Guid Id { get; set; }
    public Guid BuildingId { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SerialNumber { get; set; }
    public string Floor { get; set; }
    public string RoomNumber { get; set; }

    public Building Building { get; set; }

    public string GetFullDescription()
    {
        return $"Lock: {Type} - {Name} - {Description} - {SerialNumber} - {Floor} - {RoomNumber} - {Building?.GetFullDescription()}";
    }
}

