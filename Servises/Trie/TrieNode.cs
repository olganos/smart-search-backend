using Core;

namespace Servises.Trie
{
    public class TrieNode
    {
        const int NODE_LENGTH = 256;

        public TrieNode[] Children { get; set; }

        public Dictionary<Building, int> WeightedBuildings { get; set; }

        public TrieNode()
        {
            WeightedBuildings = new Dictionary<Building, int>();

            Children = new TrieNode[NODE_LENGTH];
        }
    }
}
