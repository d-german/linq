namespace _02_linq_tests.inclass;

public class LazyEvaluationTests
{
    private static int Square(int x) => x * x;

    private static IEnumerable<T> ApplyFunctionToArrayLazyForLoop<T>(IReadOnlyList<T> list, Func<T, T> operation)
    {
        for (var i = 0; i < list.Count; i++)
        {
            // TODO: Apply the operation to the current element operation(array[i]) and yield return the result for lazy evaluation
            yield return operation(list[i]);
        }

        //TODO: Remove this line
        throw new NotImplementedException();
    }

    private static IEnumerable<T> ApplyFunctionToArrayLazyForeach<T>(IReadOnlyList<T> array, Func<T, T> operation)
    {
        
        foreach (var t in array)
        {
            // Apply the operation to the current element operation(t) and yield return the result for lazy evaluation
            yield return operation(t);
        }
    }

    [Test]
    public void TestSquareFunctionLazyForLoop()
    {
        var numbers = new[]
        {
            1, 2, 3, 4, 5
        };
        var expected = new[]
        {
            1, 4, 9, 16, 25
        }; // Expected results after squaring each number

        // Since ApplyFunctionToArrayLazy is lazy, we need to iterate over the results
        // to perform the actual calculations. ToList() will force immediate execution.
        var result = ApplyFunctionToArrayLazyForLoop(numbers, Square).ToList(); // Note not needed because of Is.EquivalentTo below

        // Assert that the result is equivalent to the expected sequence.
        // EquivalentTo is used here to ensure that both sequences contain the same elements,
        // regardless of their ordering. However, in this case, the order does matter, so you could also use Is.EqualTo if strict ordering is part of the requirement.
        Assert.That(result, Is.EquivalentTo(expected));
    }

    [Test]
    public void TestSquareFunctionLazyForeach()
    {
        var numbers = new[]
        {
            1, 2, 3, 4, 5
        };
        var expected = new[]
        {
            1, 4, 9
        }; // Expected results after squaring each number and taking the first 3

        
        var result = ApplyFunctionToArrayLazyForeach(numbers, Square).Take(3);

        // Assert that the result matches the expected sequence, with the order being significant.
        Assert.That(result, Is.EqualTo(expected));
    }
}