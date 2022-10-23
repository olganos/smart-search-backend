using Core;

namespace Servises.Trie
{
    // todo: make internal
    public class WeightedTrie
    {
        public TrieNode Root { get; }

        public WeightedTrie()
        {
            Root = new TrieNode();
        }

        public TrieNode Insert(string text, int weight, int index, ISearchableEntity searchableEntity)
        {
            return Insert(text, weight, index, searchableEntity, Root);
        }

        public Dictionary<ISearchableEntity, int>? Search(string prefix)
        {
            return Search(prefix, Root);
        }

        private TrieNode Insert(string text, int weight, int index, ISearchableEntity searchableEntity, TrieNode node)
        {
            if(string.IsNullOrWhiteSpace(text))
            {
                return node;
            }

            int cur = text[index];

            if (node.Children[cur] == null)
            {
                node.Children[cur] = new TrieNode();
            }

            if (node.WeightedEntities.ContainsKey(searchableEntity))
            {
                node.WeightedEntities[searchableEntity] = Math.Max(node.WeightedEntities[searchableEntity], weight);
            }
            else
            {
                node.WeightedEntities[searchableEntity] = weight;
            }

            if (index + 1 != text.Length)
            {
                node.Children[cur] = Insert(text, weight, index + 1, searchableEntity, node.Children[cur]);
            }

            return node;
        }

        private Dictionary<ISearchableEntity, int>? Search(string prefix, TrieNode root)
        {
            int index = 0;
            TrieNode? answer = null;
            int n = prefix.Length;

            // Searching the prefix in TRie.
            while (index < n)
            {
                int cur = prefix[index];

                if (root.Children[cur] == null)
                    break;

                answer = root.Children[cur];
                root = root.Children[cur];
                ++index;
            }

            return answer?.WeightedEntities;
        }
    }
}
