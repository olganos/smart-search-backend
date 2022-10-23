namespace Core;

public class Data
{
    public Building[] Buildings { get; set; }
    public Lock[] Locks { get; set; }
    public Group[] Groups { get; set; }
    public Medium[] Media { get; set; }
}

public class Building
{
    public Guid Id { get; set; }
    public string ShortCut { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public enum BuildingWeight
{
    ShortCut = 7,
    Name = 9,
    Description = 5,
}

public class Lock
{
    public Guid Id { get; set; }
    public string BuildingId { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SerialNumber { get; set; }
    public string Floor { get; set; }
    public string RoomNumber { get; set; }
}

public class Group
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class Medium
{
    public Guid Id { get; set; }
    public string GroupId { get; set; }
    public string Type { get; set; }
    public string Owner { get; set; }
    public string Description { get; set; }
    public string SerialNumber { get; set; }
}

