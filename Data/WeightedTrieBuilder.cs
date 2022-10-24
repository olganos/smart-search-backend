using Core.Entities;
using Core;

using Data.Trie;
using Data.Abstraction;

namespace Data
{
    public class WeightedTrieBuilder
    {
        public WeightedTrie WeightedTrie { get; }

        public WeightedTrieBuilder(IDataInitialiser dataInitialiser)
        {
            _entitySet = dataInitialiser.EntitySet;

            WeightedTrie = new WeightedTrie();
            ConnectLinkedEntities();
            BuildWeightedTrie();
        }

        private readonly EntitySet? _entitySet;

        private void ConnectLinkedEntities()
        {
            if (_entitySet == null)
            {
                return;
            }

            ConnectLocksWithBuildings();
            ConnectMediaWithGroups();
        }

        private void ConnectLocksWithBuildings()
        {
            if (_entitySet?.Locks == null || _entitySet?.Buildings == null)
            {
                return;
            }

            var buildings = new Dictionary<Guid, Building>(_entitySet.Buildings.Select(x => new KeyValuePair<Guid, Building>(x.Id, x)));

            foreach (var item in _entitySet.Locks)
            {
                if (buildings.ContainsKey(item.BuildingId))
                {
                    item.Building = buildings[item.BuildingId];
                }
            }
        }

        private void ConnectMediaWithGroups()
        {
            if (_entitySet?.Media == null || _entitySet?.Groups == null)
            {
                return;
            }

            var groups = new Dictionary<Guid, Group>(_entitySet.Groups.Select(x => new KeyValuePair<Guid, Group>(x.Id, x)));

            foreach (var item in _entitySet.Media)
            {
                if (groups.ContainsKey(item.GroupId))
                {
                    item.Group = groups[item.GroupId];
                }
            }
        }

        private void BuildWeightedTrie()
        {
            if (_entitySet == null)
            {
                return;
            }

            if (_entitySet.Buildings != null && _entitySet.Buildings.Any())
            {
                BuildBuildingsTrie(_entitySet.Buildings);
            }

            if (_entitySet.Media != null && _entitySet.Media.Any())
            {
                BuildMediumsTrie(_entitySet.Media);
            }

            if (_entitySet.Locks != null && _entitySet.Locks.Any())
            {
                BuildLocksTrie(_entitySet.Locks);
            }

            if (_entitySet.Groups != null && _entitySet.Groups.Any())
            {
                BuildGroupsTrie(_entitySet.Groups);
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
                WeightedTrie.Insert(mediums[i].Group?.Name, (int)MediumWeight.GroupName, 0, mediums[i]);
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
                WeightedTrie.Insert(locks[i].Description, (int)LockWeight.Description, 0, locks[i]);
                WeightedTrie.Insert(locks[i].Building?.ShortCut, (int)LockWeight.BuildingShortcut, 0, locks[i]);
                WeightedTrie.Insert(locks[i].Building?.Name, (int)LockWeight.BuildingName, 0, locks[i]);
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
}
