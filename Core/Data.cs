namespace Core;

public class Data
{
    public Building[] buildings { get; set; }
    public Lock[] locks { get; set; }
    public Group[] groups { get; set; }
    public Medium[] media { get; set; }
}

public class Building
{
    public string id { get; set; }
    public string shortCut { get; set; }
    public string name { get; set; }
    public string description { get; set; }
}

public class Lock
{
    public string id { get; set; }
    public string buildingId { get; set; }
    public string type { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string serialNumber { get; set; }
    public string floor { get; set; }
    public string roomNumber { get; set; }
}

public class Group
{
    public string id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
}

public class Medium
{
    public string id { get; set; }
    public string groupId { get; set; }
    public string type { get; set; }
    public string owner { get; set; }
    public string description { get; set; }
    public string serialNumber { get; set; }
}

