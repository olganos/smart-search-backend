namespace Core;

public class GeneralizedSearchableEntity
{
    public Guid Id { get; set; }

    public string[] Fields { get; set; }

    public string Type { get; set; }

    public int Weight { get; set; }
}