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

        public TrieNode Insert(string text, int weight, int index, Building building)
        {
            return Insert(text, weight, index, building, Root);
        }

        public Dictionary<Building, int>? Search(string prefix)
        {
            return Search(prefix, Root);
        }

        private TrieNode Insert(string text, int weight, int index, Building building, TrieNode node)
        {
            int cur = text[index];

            if (node.Children[cur] == null)
            {
                node.Children[cur] = new TrieNode();
            }

            if (node.WeightedBuildings.ContainsKey(building))
            {
                node.WeightedBuildings[building] = Math.Max(node.WeightedBuildings[building], weight);
            }
            else
            {
                node.WeightedBuildings[building] = weight;
            }

            if (index + 1 != text.Length)
            {
                node.Children[cur] = Insert(text, weight, index + 1, building, node.Children[cur]);
            }

            return node;
        }

        private Dictionary<Building, int>? Search(string prefix, TrieNode root)
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

            return answer?.WeightedBuildings;
        }
    }
}
