using Core;

using Data;

using Servises;

namespace UnitTests
{
    public class SearchServiceTests
    {
        private ISearchService _searchService;
        //private 

        [SetUp]
        public void Setup()
        {
            var fileName = "sv_lsm_data.json";
            _searchService = new SearchService(new DataInitialiser(fileName));
        }

        [Test]
        public void Execute_SearchPartialMatch_FieldWeigth()
        {
            var searchString = "Head";
            var result = _searchService.Execute(searchString);

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Weight, Is.EqualTo((int)BuildingWeight.Name));
        }

        [Test]
        public void Execute_SearchExactMatch_FieldWeigthPlus10()
        {
            var searchString = "Head Office";
            var result = _searchService.Execute(searchString);

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Weight, Is.EqualTo((int)BuildingWeight.Name + (int)ExactMatchWeight.Value));
        }

        [Test]
        public void Execute_SearchPartialAndExactMatch_DifferentWeights()
        {
            var searchString = "Logistikzentrum I";
            var result = _searchService.Execute(searchString);

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Weight, Is.EqualTo((int)BuildingWeight.Name + (int)ExactMatchWeight.Value));
            Assert.That(result.Last().Weight, Is.EqualTo((int)BuildingWeight.Name));
        }
    }
}