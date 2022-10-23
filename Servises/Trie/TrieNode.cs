using Core;

namespace Servises.Trie
{
    public class TrieNode
    {
        const int NODE_LENGTH = 256;

        public TrieNode[] Children { get; set; }

        public Dictionary<ISearchableEntity, int> WeightedEntities { get; set; }

        public TrieNode()
        {
            WeightedEntities = new Dictionary<ISearchableEntity, int>();

            Children = new TrieNode[NODE_LENGTH];
        }
    }
}
