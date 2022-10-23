namespace Core;

public interface ISearchableEntity
{
    Guid Id { get; set; }

    string GetFullDescription();
}

public class Data
{
    public Building[] Buildings { get; set; }
    public Lock[] Locks { get; set; }
    public Group[] Groups { get; set; }
    public Medium[] Media { get; set; }
}

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

public enum BuildingWeight
{
    ShortCut = 7,
    Name = 9,
    Description = 5,
}

public class Lock : ISearchableEntity
{
    public Guid Id { get; set; }
    public string BuildingId { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SerialNumber { get; set; }
    public string Floor { get; set; }
    public string RoomNumber { get; set; }

    public string GetFullDescription()
    {
        return $"Lock: {Type} - {Name} - {Description} - {SerialNumber} - {Floor} - {RoomNumber}";
    }
}

public enum LockWeight
{
    Type = 3,
    Name = 10,
    SerialNumber = 8,
    Floor = 6,
    RoomNumber = 6,
    Description = 6,
}

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

public enum GroupWeight
{
    Name = 9,
    Description = 5,
}

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

public enum MediumWeight
{
    Type = 3,
    Owner = 10,
    SerialNumber = 8,
    Description = 6,
}

