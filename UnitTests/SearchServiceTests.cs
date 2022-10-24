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
                            Id = Guid.NewGuid(),
                            ShortCut = "HOFF",
                            Name = "Head Office",
                            Description = "Head Office, Feringastraße 4, 85774 Unterföhring"
                        },
                        new Building  {
                            Id = Guid.NewGuid(),
                            ShortCut = "PROD",
                            Name = "Produktionsstätte",
                            Description = "Produktionsstätte, Lindauer Str. 6, 06721 Osterfeld"
                        },
                        new Building  {
                            Id = Guid.NewGuid(),
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
                            Id = Guid.NewGuid(),
                            ShortCut = "HOFF",
                            Name = "Head Office",
                            Description = "Head Office, Feringastraße 4, 85774 Unterföhring"
                        },
                        new Building  {
                            Id = Guid.NewGuid(),
                            ShortCut = "PROD",
                            Name = "Produktionsstätte",
                            Description = "Produktionsstätte, Lindauer Str. 6, 06721 Osterfeld"
                        },
                        new Building  {
                            Id = Guid.NewGuid(),
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
                            Id = Guid.NewGuid(),
                            ShortCut = "HOFF",
                            Name = "Head Office",
                            Description = "Head Office, Feringastraße 4, 85774 Unterföhring"
                        },
                        new Building  {
                            Id = Guid.NewGuid(),
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

        [Test]
        public void Execute_SearchExactMatchInLessWeightedField_FieldWeigthX10()
        {
            var mock = new Mock<IDataInitialiser>();
            mock.Setup(initialiser => initialiser.EntitySet).Returns(
                new EntitySet
                {
                    Buildings = new Building[]
                    {
                        new Building {
                            Id = Guid.NewGuid(),
                            ShortCut = "HOFF",
                            Name = "Head Office, Feringastraße 4, 85774 Unterföhring", // 9
                            Description = "Head Office" //5 * 10
                        }
                    }
                });

            var weightedTrieBuilder = new WeightedTrieBuilder(mock.Object);
            var searchService = new SearchService(weightedTrieBuilder);

            var searchString = "Head Office";
            var result = searchService.Execute(searchString);

            Assert.That(result.Count(), Is.EqualTo(1));
            // Name's weight is more than description, but in description is full match
            Assert.That(result.First().Weight, Is.EqualTo(50));

            // If not exact match, the weight should be from the more weighted field (name)
            searchString = "Head Offic";
            result = searchService.Execute(searchString);

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Weight, Is.EqualTo(9));

            searchString = "Head Office,";
            result = searchService.Execute(searchString);

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Weight, Is.EqualTo(9));
        }

        [Test]
        public void Execute_SearchPartialAndExactMatchIndifferentEntities_ShowSortedByWeight()
        {
            var mock = new Mock<IDataInitialiser>();
            mock.Setup(initialiser => initialiser.EntitySet).Returns(
                new EntitySet
                {
                    Buildings = new Building[]
                    {
                        new Building {
                            Id = Guid.NewGuid(),
                            ShortCut = "HOFF",
                            Name = "Head Office",
                            Description = "Head Office, Feringastraße 4, 85774 Unterföhring"
                        },
                        new Building  {
                            Id = Guid.NewGuid(),
                            ShortCut = "PROD",
                            Name = "Produktionsstätte",
                            Description = "Produktionsstätte, Lindauer Str. 6, 06721 Osterfeld"
                        },
                        new Building  {
                            Id = Guid.NewGuid(),
                            ShortCut = "LOG-1",
                            Name = "Logistikzentrum I",
                            Description = "Logistikzentrum, 81677 München"
                        },
                        new Building  {
                            Id = Guid.NewGuid(),
                            ShortCut = "LOG-2",
                            Name = "Logistikzentrum II",
                            Description = "Logistikzentrum, 81335 München"
                        }
                    }
                });

            var weightedTrieBuilder = new WeightedTrieBuilder(mock.Object);
            var searchService = new SearchService(weightedTrieBuilder);

            var searchString = "Logistikzentrum I";
            var result = searchService.Execute(searchString);

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Weight, Is.EqualTo(90));
            Assert.That(result.Last().Weight, Is.EqualTo(9));
        }

        [Test]
        public void Execute_NotExistingString_ShowEmptyResult()
        {
            var mock = new Mock<IDataInitialiser>();
            mock.Setup(initialiser => initialiser.EntitySet).Returns(
                new EntitySet
                {
                    Buildings = new Building[]
                    {
                        new Building {
                            Id = Guid.NewGuid(),
                            ShortCut = "HOFF",
                            Name = "Head Office",
                            Description = "Head Office, Feringastraße 4, 85774 Unterföhring"
                        },
                    }
                });

            var weightedTrieBuilder = new WeightedTrieBuilder(mock.Object);
            var searchService = new SearchService(weightedTrieBuilder);

            var searchString = "Empty";
            var result = searchService.Execute(searchString);

            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Execute_SearchWithLowCase_ReturnCaseInsentive()
        {
            var mock = new Mock<IDataInitialiser>();
            mock.Setup(initialiser => initialiser.EntitySet).Returns(
                new EntitySet
                {
                    Buildings = new Building[]
                    {
                        new Building {
                            Id = Guid.NewGuid(),
                            ShortCut = "HOFF",
                            Name = "Head Office",
                            Description = "Head Office, Feringastraße 4, 85774 Unterföhring"
                        },
                    }
                });

            var weightedTrieBuilder = new WeightedTrieBuilder(mock.Object);
            var searchService = new SearchService(weightedTrieBuilder);

            var searchString = "head";
            var result = searchService.Execute(searchString);

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Weight, Is.EqualTo(9));
        }

        [Test]
        public void Execute_SearchWithNonLetters_Success()
        {
            var mock = new Mock<IDataInitialiser>();
            mock.Setup(initialiser => initialiser.EntitySet).Returns(
                new EntitySet
                {
                    Locks = new Lock[]
                    {
                        new Lock {
                            Id = Guid.NewGuid(),
                            Floor= "3.OG",
                            RoomNumber = "340"
                        },
                    }
                });

            var weightedTrieBuilder = new WeightedTrieBuilder(mock.Object);
            var searchService = new SearchService(weightedTrieBuilder);

            var searchString = "3.O";
            var result = searchService.Execute(searchString);

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Weight, Is.EqualTo(6));
        }

        [Test]
        public void Execute_NestedObjectsLocks_ShowByWeights()
        {
            var buildingId = Guid.NewGuid();

            var mock = new Mock<IDataInitialiser>();
            mock.Setup(initialiser => initialiser.EntitySet).Returns(
                new EntitySet
                {
                    Buildings = new Building[]
                    {
                        new Building {
                            Id = buildingId,
                            ShortCut = "HOFF",
                            Name = "Head Office", // 9
                            Description = "Head Office, Feringastraße 4, 85774 Unterföhring"
                        },
                    },
                    Locks = new Lock[]
                    {
                        new Lock {
                            Id = Guid.NewGuid(),
                            BuildingId = buildingId, // 8
                            Type = "Cylinder",
                            Name = "WC Herren 3.OG süd",
                            Floor= "3.OG",
                            RoomNumber = "340"
                        },
                        new Lock {
                            Id = Guid.NewGuid(),
                            BuildingId = buildingId,
                            Type = "Cylinder",
                            Name = "Head Herren 3.OG süd", // 10
                            Floor= "3.OG",
                            RoomNumber = "340"
                        },
                        new Lock {
                            Id = Guid.NewGuid(),
                            Floor= "3.OG",
                            RoomNumber = "340"
                        },
                    }
                });

            var weightedTrieBuilder = new WeightedTrieBuilder(mock.Object);
            var searchService = new SearchService(weightedTrieBuilder);

            var searchString = "Head";
            var result = searchService.Execute(searchString);

            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result.First().Weight, Is.EqualTo(10));
            Assert.That(result.Skip(1).First().Weight, Is.EqualTo(9));
            Assert.That(result.Last().Weight, Is.EqualTo(8));
        }

        [Test]
        public void Execute_NestedObjectsMedia_ShowByWeights()
        {
            var groupId = Guid.NewGuid();

            var mock = new Mock<IDataInitialiser>();
            mock.Setup(initialiser => initialiser.EntitySet).Returns(
                new EntitySet
                {
                    Media = new Medium[]
                    {
                        new Medium {
                            Id = Guid.NewGuid(),
                            GroupId = groupId, // name = 8
                            Type = "Card",
                            SerialNumber = "UID-378D17F6"
                        },
                    },
                    Groups = new Group[]
                    {
                        new Group {
                            Id = groupId,
                            Name = "Buchhaltung", // 9
                            Description = "Hardware"
                        },
                    }
                });

            var weightedTrieBuilder = new WeightedTrieBuilder(mock.Object);
            var searchService = new SearchService(weightedTrieBuilder);

            var searchString = "buch";
            var result = searchService.Execute(searchString);

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Weight, Is.EqualTo(9));
            Assert.That(result.Last().Weight, Is.EqualTo(8));
        }

        [Test]
        public void Execute_NestedObjectsMediaExactMatch_ShowByWeights()
        {
            var groupId = Guid.NewGuid();

            var mock = new Mock<IDataInitialiser>();
            mock.Setup(initialiser => initialiser.EntitySet).Returns(
                new EntitySet
                {
                    Media = new Medium[]
                    {
                        new Medium {
                            Id = Guid.NewGuid(),
                            GroupId = groupId, // name = 8 * 10
                            Type = "Card",
                            SerialNumber = "UID-378D17F6"
                        },
                    },
                    Groups = new Group[]
                    {
                        new Group {
                            Id = groupId,
                            Name = "Buchhaltung", // 9 * 10
                            Description = "Hardware"
                        },
                    }
                });

            var weightedTrieBuilder = new WeightedTrieBuilder(mock.Object);
            var searchService = new SearchService(weightedTrieBuilder);

            var searchString = "Buchhaltung";
            var result = searchService.Execute(searchString);

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Weight, Is.EqualTo(90));
            Assert.That(result.Last().Weight, Is.EqualTo(80));
        }
    }
}