using Core.Entities;

namespace Data.Trie
{
    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; set; }

        public Dictionary<ISearchableEntity, int> WeightedEntities { get; set; }

        public TrieNode()
        {
            WeightedEntities = new Dictionary<ISearchableEntity, int>();

            Children = new Dictionary<char, TrieNode>();
        }
    }
}
