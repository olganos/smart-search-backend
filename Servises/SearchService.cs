using Core;

using Data;
using Data.Trie;

using System.Xml.Linq;

namespace Servises;

public class SearchService : ISearchService
{
    private readonly WeightedTrie _weightedTrie;

    public SearchService(DataInitialiser dataInitialiser)
    {
        _weightedTrie = dataInitialiser.WeightedTrie;
    }

    public IEnumerable<GeneralizedSearchableEntity> Execute(string searchString)
    {
        return _weightedTrie.Search(searchString)
            .OrderByDescending(x => x.Value)
            .Select(x => new GeneralizedSearchableEntity
            {
                Id = x.Key.Id,
                FullDescription = x.Key.GetFullDescription(),
                Weight = x.Value
            });
    }
}
