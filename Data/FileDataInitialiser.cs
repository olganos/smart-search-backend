using Core.Entities;

using Data.Abstraction;

using System.Text.Json;

namespace Data;

public class FileDataInitialiser : IDataInitialiser
{
    private readonly EntitySet? _entitySet;
    public EntitySet? EntitySet => _entitySet;

    public FileDataInitialiser(string dataFilePath)
    {
        string jsonString = File.ReadAllText(dataFilePath);
        _entitySet = JsonSerializer.Deserialize<EntitySet>(
            jsonString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );
    }
}
