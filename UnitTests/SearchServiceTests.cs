using Core;
using Core.Entities;

using Data;
using Data.Abstraction;
using Moq;

using Servises;

namespace UnitTests
{
    public class SearchServiceTests
    {
        [Test]
        public void Execute_PartialMatchIn2Fields1Entity_ShowMoreWeight()
        {
            var mock = new Mock<IDataInitialiser>();
            mock.Setup(initialiser => initialiser.EntitySet).Returns(
                new EntitySet
                {
                    Buildings = new Building[]
                    {
                        new Building {
                            Id = new Guid("0cccab2b-bc8d-44c5-b248-8a9ca6d7e899"),
                            ShortCut = "HOFF",
                            Name = "Head Office",
                            Description = "Head Office, Feringastraße 4, 85774 Unterföhring"
                        },
                        new Building  {
                            Id = new Guid("9605186f-7eb4-4f40-967e-2885d9a8b3c4"),
                            ShortCut = "PROD",
                            Name = "Produktionsstätte",
                            Description = "Produktionsstätte, Lindauer Str. 6, 06721 Osterfeld"
                        },
                        new Building  {
                            Id = new Guid("3116849e-e18d-4afd-8058-156d8d96b593"),
                            ShortCut = "LOG-1",
                            Name = "Logistikzentrum I",
                            Description = "Logistikzentrum, 81677 München"
                        }
                    }
                });

            var weightedTrieBuilder = new WeightedTrieBuilder(mock.Object);
            var searchService = new SearchService(weightedTrieBuilder);

            var searchString = "Head";
            var result = searchService.Execute(searchString);

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Weight, Is.EqualTo(9));
        }

        [Test]
        public void Execute_PartialMatchInDefferentFields2Entities_ShowSortedByWeight()
        {
            var mock = new Mock<IDataInitialiser>();
            mock.Setup(initialiser => initialiser.EntitySet).Returns(
                new EntitySet
                {
                    Buildings = new Building[]
                    {
                        new Building {
                            Id = new Guid("0cccab2b-bc8d-44c5-b248-8a9ca6d7e899"),
                            ShortCut = "HOFF",
                            Name = "Head Office",
                            Description = "Head Office, Feringastraße 4, 85774 Unterföhring"
                        },
                        new Building  {
                            Id = new Guid("9605186f-7eb4-4f40-967e-2885d9a8b3c4"),
                            ShortCut = "PROD",
                            Name = "Produktionsstätte",
                            Description = "Produktionsstätte, Lindauer Str. 6, 06721 Osterfeld"
                        },
                        new Building  {
                            Id = new Guid("3116849e-e18d-4afd-8058-156d8d96b593"),
                            ShortCut = "LOG-1",
                            Name = "Office",
                            Description = "Head Office, Logistikzentrum, 81677 München"
                        }
                    }
                });

            var weightedTrieBuilder = new WeightedTrieBuilder(mock.Object);
            var searchService = new SearchService(weightedTrieBuilder);

            var searchString = "Head";
            var result = searchService.Execute(searchString);

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Weight, Is.EqualTo(9));
            Assert.That(result.Last().Weight, Is.EqualTo(5));
        }

        [Test]
        public void Execute_PartialMatchInDefferentEntities_ShowSortedByWeight()
        {
            var mock = new Mock<IDataInitialiser>();
            mock.Setup(initialiser => initialiser.EntitySet).Returns(
                new EntitySet
                {
                    Buildings = new Building[]
                    {
                        new Building {
                            Id = new Guid("0cccab2b-bc8d-44c5-b248-8a9ca6d7e899"),
                            ShortCut = "HOFF",
                            Name = "Head Office",
                            Description = "Head Office, Feringastraße 4, 85774 Unterföhring"
                        },
                        new Building  {
                            Id = new Guid("9605186f-7eb4-4f40-967e-2885d9a8b3c4"),
                            ShortCut = "PROD",
                            Name = "Produktionsstätte",
                            Description = "Produktionsstätte, Lindauer Str. 6, 06721 Osterfeld"
                        },
                    },
                    Groups = new Group[]
                    {
                        new Group
                        {
                            Id = Guid.NewGuid(),
                            Name = "Vorstand",
                            Description = "Head group where all transponders"
                        }
                    }
                });

            var weightedTrieBuilder = new WeightedTrieBuilder(mock.Object);
            var searchService = new SearchService(weightedTrieBuilder);

            var searchString = "Head";
            var result = searchService.Execute(searchString);

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Weight, Is.EqualTo(9));
            Assert.That(result.Last().Weight, Is.EqualTo(5));
        }

        //[Test]
        //public void Execute_SearchExactMatch_FieldWeigthPlus10()
        //{
        //    var searchString = "Head Office";
        //    var result = _searchService.Execute(searchString);

        //    Assert.That(result.Count(), Is.EqualTo(1));
        //    Assert.That(result.First().Weight, Is.EqualTo((int)BuildingWeight.Name * (int)ExactMatchRatio.Value));
        //}

        //[Test]
        //public void Execute_SearchPartialAndExactMatch_DifferentWeights()
        //{
        //    var searchString = "Logistikzentrum I";
        //    var result = _searchService.Execute(searchString);

        //    Assert.That(result.Count(), Is.EqualTo(2));
        //    Assert.That(result.First().Weight, Is.EqualTo((int)BuildingWeight.Name * (int)ExactMatchRatio.Value));
        //    Assert.That(result.Last().Weight, Is.EqualTo((int)BuildingWeight.Name));
        //}
    }
}