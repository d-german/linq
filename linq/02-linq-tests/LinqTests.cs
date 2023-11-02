using System.Diagnostics.CodeAnalysis;

namespace _02_linq_tests;

public class LinqTests
{
    private List<Person> _persons = null!;

    private List<int> _values = null!;

    [SetUp]
    public void Init()
    {
        _persons = new List<Person>
        {
            new()
            {
                Name = "Tom",
                Age = 10
            },
            new()
            {
                Name = "Dick",
                Age = 5
            },
            new()
            {
                Name = "Harry",
                Age = 5
            },
            new()
            {
                Name = "Mary",
                Age = 5
            },
            new()
            {
                Name = "Jay",
                Age = 20
            },
            new()
            {
                Name = "George",
                Age = 20
            }
        };

        _values = new List<int>
        {
            3,
            10,
            6,
            1,
            4,
            8,
            2,
            5,
            9,
            7
        };
    }

    [Test]
    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    public void ImperativeSumValues()
    {
        var sum = 0;

        for (var counter = 0; counter < _values.Count; counter++)
        {
            sum += _values[counter];
        }

        Assert.That(sum, Is.EqualTo(55));
    }

    [Test]
    public void DeclarativeSumValues()
    {
        var sum = _values.Sum();

        Assert.That(sum, Is.EqualTo(55));
    }

    [Test]
    public void AggregateSumValues()
    {
        var sum = _values.Aggregate(0, (result, value) => result + value);

        Assert.That(sum, Is.EqualTo(55));
    }

    [Test]
    public void AggregateSumPersonsAges()
    {
        // Adding the age values from each person
        var sum = _persons.Aggregate(0, (result, person) => result + person.Age);
        Assert.That(sum, Is.EqualTo(65));
    }

    [Test]
    public void AggregateSumValuesPerson()
    {
        // Accumulate Bobs age starting at 0 using the items in the _values
        var person = _values.Aggregate(new Person
        {
            Age = 0,
            Name = "Bob"
        }, (person, value) => person with { Age = person.Age + value });
        
        Assert.Multiple(() =>
        {
            Assert.That(person.Age, Is.EqualTo(55));
            Assert.That(person.Name, Is.EqualTo("Bob"));
        });
    }

    [Test]
    public void AggregateProductValues()
    {
        var product = _values.Aggregate(1, (x, y) => x * y);
        Assert.That(product, Is.EqualTo(3628800));
    }

    [Test]
    public void FilterAndOrderEvenValues()
    {
        var evenValueQuery = _values
            .Where(value => value % 2 == 0) // find even integers
            .OrderBy(value => value);

        CollectionAssert.AreEqual(new[]
        {
            2, 4, 6, 8, 10
        }, evenValueQuery.ToArray());
    }

    [Test]
    public void FirstTest()
    {
        var evenValueQuery = _values
            .Where(value => value % 2 == 0) // find even integers
            .OrderBy(value => value)
            .First();

        Assert.That(evenValueQuery, Is.EqualTo(2));
    }

    [Test]
    public void FirstFailTest()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            _ = _values
                .Where(v => v > 100)
                .OrderBy(v => v)
                .First();
        });
    }

    [Test]
    public void FirstOrDefaultTest()
    {
        var value = _values
            .Where(v => v > 100)
            .OrderBy(v => v)
            .FirstOrDefault();

        Assert.That(value, Is.EqualTo(0));
    }

    [Test]
    public void OddValuesMulBy10AndOrder()
    {
        var query = _values
            .Where(value => value % 2 != 0) // find odd integers
            .Select(value => value * 10) // multiply each by 10
            .OrderByDescending(value => value); // sort the values

        CollectionAssert.AreEqual(new[]
        {
            90, 70, 50, 30, 10
        }, query.ToArray());
    }

    [Test]
    public void MapValuesOrderToPersons()
    {
        var query = _values
            .Where(n => n > 6)
            .OrderBy(value => value)
            .Select(n => new Person
            {
                Age = n * 2,
                Name = $"person #{n}"
            }).ToArray();

        const string expected = "Person { Name = person #7, Age = 14 } " +
                                "Person { Name = person #8, Age = 16 } " +
                                "Person { Name = person #9, Age = 18 } " +
                                "Person { Name = person #10, Age = 20 }";
        var actual = query.Dump();
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void AnonymousTypes()
    {
        const int currentYear = 2018;
        var query = _persons
            .Select(p => new
            {
                p.Name,
                BirthYear = currentYear - p.Age
            })
            .OrderBy(p => p.BirthYear)
            .ThenBy(p => p.Name);

        const string expected = "{ Name = George, BirthYear = 1998 } " +
                                "{ Name = Jay, BirthYear = 1998 } " +
                                "{ Name = Tom, BirthYear = 2008 } " +
                                "{ Name = Dick, BirthYear = 2013 } " +
                                "{ Name = Harry, BirthYear = 2013 } " +
                                "{ Name = Mary, BirthYear = 2013 }";
        var actual = query.Dump();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void NamesBiggerThan3LettersTest()
    {
        var query = _persons
            .Select(person => new
            {
                nameLength = person.Name.Length,
                person
            })
            .Where(x => x.nameLength > 3)
            .OrderBy(x => x.nameLength)
            .ThenBy(x => x.person.Age)
            .Select(x => x.person);

        const string expected = "Person { Name = Dick, Age = 5 } " +
                                "Person { Name = Mary, Age = 5 } " +
                                "Person { Name = Harry, Age = 5 } " +
                                "Person { Name = George, Age = 20 }";
        var actual = query.Dump();
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TakeZipToPersons()
    {
        var query = _values
            .OrderBy(value => value)
            .Take(5)
            .Zip(_persons, (v, p) =>
                    p with { Age = v } // Use 'with' to create a new record with the updated Age
            );
    
        const string expected = "Person { Name = Tom, Age = 1 } " +
                                "Person { Name = Dick, Age = 2 } " +
                                "Person { Name = Harry, Age = 3 } " +
                                "Person { Name = Mary, Age = 4 } " +
                                "Person { Name = Jay, Age = 5 }";
        var actual = query.Dump();
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void DistinctTest()
    {
        var nums = new[]
        {
            1, 1, 1, 1
        };

        var distinctValue = nums
            .Distinct()
            .FirstOrDefault();

        Assert.That(distinctValue, Is.EqualTo(1));
    }

    [Test]
    public void ToDictionaryTest()
    {
        var d = _persons.ToDictionary(key => key.Name, value => value.Age);

        Assert.That(d["Tom"], Is.EqualTo(10));
    }

    [Test]
    public void ToLookupTest()
    {
        var lookup = _persons.ToLookup(key => key.Age, value => value.Name);
        var names = lookup[5].ToArray();

        CollectionAssert.AreEqual(new[]
        {
            "Dick", "Harry", "Mary"
        }, names);
    }

    // Query syntax expression vs method syntax expression
    [Test]
    public void QueryVsMethodSyntaxTest()
    {
        // Data source.
        int[] scores =
        {
            90, 71, 82, 93, 75, 82
        };

        // Query Expression.
        var q1 =
            from score in scores
            where score > 80
            orderby score descending
            select score;

        // Method chain.
        var q2 =
            scores
                .Where(score => score > 80)
                .OrderByDescending(score => score)
                .Select(score => score);

        CollectionAssert.AreEqual(q1.ToArray(), q2.ToArray());
    }

    [Test]
    public void CapturedVariables()
    {
        Assert.AreEqual("20 40", CapturedEnumerable<int>().Dump());
    }

    private static IEnumerable<T> CapturedEnumerable<T>()
    {
        int[] numbers =
        {
            1, 2
        };

        var factor = 10;

        // ReSharper disable once AccessToModifiedClosure
        var query = numbers.Select(n => n * factor);

        factor = 20;

        return (IEnumerable<T>)query;
    }
}