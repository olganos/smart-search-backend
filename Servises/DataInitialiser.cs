using Core;

using Servises.Trie;

using System.Text.Json;

namespace Servises;

public class DataInitialiser
{
    public Data? Data { get; }

    public WeightedTrie? WeightedTrie { get; }

    public DataInitialiser(string dataFilePath)
    {
        string jsonString = File.ReadAllText(dataFilePath);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        Data = JsonSerializer.Deserialize<Data>(jsonString, options);

        if (Data != null && Data.Buildings.Any())
        {
            WeightedTrie = BuildBuildingsTrie(Data.Buildings);
        }
    }

    private WeightedTrie BuildBuildingsTrie(Building[] buildings)
    {
        var buildingTrie = new WeightedTrie();

        for (int i = 0; i < buildings.Count(); i++)
        {
            buildingTrie.Insert(buildings[i].ShortCut, (int)BuildingWeight.ShortCut, 0, buildings[i]);
            buildingTrie.Insert(buildings[i].Name, (int)BuildingWeight.Name, 0, buildings[i]);
            buildingTrie.Insert(buildings[i].Description, (int)BuildingWeight.Description, 0, buildings[i]);
        }

        return buildingTrie;
    }
}
