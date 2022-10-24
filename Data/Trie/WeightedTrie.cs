using Core;
using Core.Entities;

namespace Data.Trie;

public class WeightedTrie
{
    public TrieNode Root { get; }

    public WeightedTrie()
    {
        Root = new TrieNode();
    }

    public TrieNode Insert(string text, int weight, int index, ISearchableEntity searchableEntity)
    {
        return Insert(UnifyString(text), weight, index, searchableEntity, Root);
    }

    public Dictionary<ISearchableEntity, int>? Search(string prefix)
    {
        return Search(prefix, Root);
    }

    private TrieNode Insert(string text, int weight, int index, ISearchableEntity searchableEntity, TrieNode node)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return node;
        }

        var cur = text[index];

        if (!node.Children.ContainsKey(cur))
        {
            node.Children[cur] = new TrieNode();
        }

        var lastIndex = index + 1 == text.Length;

        if (node.WeightedEntities.ContainsKey(searchableEntity))
        {
            var maxWeight = Math.Max(node.WeightedEntities[searchableEntity], weight);
            node.WeightedEntities[searchableEntity] = lastIndex ? weight * (int)ExactMatchRatio.Value : maxWeight;
        }
        else
        {
            node.WeightedEntities[searchableEntity] = lastIndex ? weight * (int)ExactMatchRatio.Value : weight;
        }

        if (!lastIndex)
        {
            node.Children[cur] = Insert(text, weight, index + 1, searchableEntity, node.Children[cur]);
        }

        return node;
    }

    private Dictionary<ISearchableEntity, int> Search(string searchString, TrieNode root)
    {
        int index = 0;
        TrieNode? answer = null;
        searchString = UnifyString(searchString);
        int n = searchString.Length;

        // Searching the prefix in TRie.
        while (index < n)
        {
            var cur = searchString[index];

            if (!root.Children.ContainsKey(cur))
                return new Dictionary<ISearchableEntity, int>();

            answer = root;
            root = root.Children[cur];
            index++;
        }

        return answer?.WeightedEntities ?? new Dictionary<ISearchableEntity, int>();
    }

    private string UnifyString(string source)
    {
        return source?.ToLower() ?? string.Empty;
    }
}
