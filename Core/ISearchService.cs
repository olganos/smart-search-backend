using Core.Entities;

namespace Core
{
    public interface ISearchService
    {
        IEnumerable<GeneralizedSearchableEntity> Execute(string searchString);
    }
}
