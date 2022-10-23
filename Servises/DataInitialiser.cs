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
        Data = JsonSerializer.Deserialize<Data>(
            jsonString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        WeightedTrie = new WeightedTrie();
        BuildWeightedTrie();
    }

    private void BuildWeightedTrie()
    {
        if (Data == null)
        {
            return;
        }

        if (Data.Buildings.Any())
        {
            BuildBuildingsTrie(Data.Buildings);
        }

        if (Data.Media.Any())
        {
            BuildMediumsTrie(Data.Media);
        }

        if (Data.Locks.Any())
        {
            BuildLocksTrie(Data.Locks);
        }

        if (Data.Groups.Any())
        {
            BuildGroupsTrie(Data.Groups);
        }
    }

    private void BuildBuildingsTrie(Building[] buildings)
    {
        for (int i = 0; i < buildings.Count(); i++)
        {
            WeightedTrie.Insert(buildings[i].ShortCut, (int)BuildingWeight.ShortCut, 0, buildings[i]);
            WeightedTrie.Insert(buildings[i].Name, (int)BuildingWeight.Name, 0, buildings[i]);
            WeightedTrie.Insert(buildings[i].Description, (int)BuildingWeight.Description, 0, buildings[i]);
        }
    }

    private void BuildMediumsTrie(Medium[] mediums)
    {
        for (int i = 0; i < mediums.Count(); i++)
        {
            WeightedTrie.Insert(mediums[i].Type, (int)MediumWeight.Type, 0, mediums[i]);
            WeightedTrie.Insert(mediums[i].Owner, (int)MediumWeight.Owner, 0, mediums[i]);
            WeightedTrie.Insert(mediums[i].SerialNumber, (int)MediumWeight.SerialNumber, 0, mediums[i]);
            WeightedTrie.Insert(mediums[i].Description, (int)MediumWeight.Description, 0, mediums[i]);
        }
    }

    private void BuildLocksTrie(Lock[] locks)
    {
        for (int i = 0; i < locks.Count(); i++)
        {
            WeightedTrie.Insert(locks[i].Type, (int)LockWeight.Type, 0, locks[i]);
            WeightedTrie.Insert(locks[i].Name, (int)LockWeight.Name, 0, locks[i]);
            WeightedTrie.Insert(locks[i].SerialNumber, (int)LockWeight.SerialNumber, 0, locks[i]);
            WeightedTrie.Insert(locks[i].Floor, (int)LockWeight.Floor, 0, locks[i]);
            WeightedTrie.Insert(locks[i].RoomNumber, (int)LockWeight.RoomNumber, 0, locks[i]);
            WeightedTrie.Insert(locks[i].Description, (int)MediumWeight.Description, 0, locks[i]);
        }
    }

    private void BuildGroupsTrie(Group[] groups)
    {
        for (int i = 0; i < groups.Count(); i++)
        {
            WeightedTrie.Insert(groups[i].Name, (int)GroupWeight.Name, 0, groups[i]);
            WeightedTrie.Insert(groups[i].Description, (int)GroupWeight.Description, 0, groups[i]);
        }
    }
}
