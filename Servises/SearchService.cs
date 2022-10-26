using Core;

using Data;
using Data.Trie;

namespace Servises;

public class SearchService : ISearchService
{
    private readonly WeightedTrie _weightedTrie;

    public SearchService(WeightedTrieBuilder weightedTreeBuilder)
    {
        _weightedTrie = weightedTreeBuilder.WeightedTrie;
    }

    public IEnumerable<GeneralizedSearchableEntity> Execute(string searchString)
    {
        return _weightedTrie.Search(searchString)
            .OrderByDescending(x => x.Value)
            .Select(x => new GeneralizedSearchableEntity
            {
                Id = x.Key.Id,
                Type = x.Key.EntityType,
                Fields = x.Key.Fields,
                Weight = x.Value
            });
    }
}
