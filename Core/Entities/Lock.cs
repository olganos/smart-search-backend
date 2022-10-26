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

    public string[] Fields => new[]
    {
        $"Type: {Type}",
        $"Name: {Name}",
        $"Description: {Description}",
        $"SerialNumber: {SerialNumber}",
        $"Floor: {Floor}",
        $"RoomNumber: {RoomNumber}",
        $"Building ShortCat: {Building?.ShortCut}",
        $"Building Name: {Building?.Name}",
        $"Building Description: {Building?.Description}",
    };

    public string EntityType => "Lock";
}

