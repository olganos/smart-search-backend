using Core;

using System.Text.Json;

namespace Servises;

public class DataInitialiser
{
    public Data? Data { get; }

	public DataInitialiser(string dataFilePath)
	{
        string fileName = dataFilePath;
        string jsonString = File.ReadAllText(fileName);
        Data = JsonSerializer.Deserialize<Data>(jsonString);
    }
}
