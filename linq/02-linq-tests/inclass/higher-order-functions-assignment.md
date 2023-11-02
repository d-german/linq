# Higher-Order Functions in C#

## Objective
Demonstrate your understanding of higher-order functions in C# by completing the provided unit tests, following the TODO comments as your guide.

## Starting Code

```csharp
[TestFixture]
public class HigherOrderFunctionsTests
{
    private static int Square(int x) => x * x;
    private static string Capitalize(string input) => input.ToUpperInvariant();

    private static T[] ApplyFunctionToArray<T>(IReadOnlyList<T> array, Func<T, T> operation)
    {
        var result = new T[array.Count];
        for (var i = 0; i < array.Count; i++)
        {
            result[i] = operation(array[i]);
        }
        return result;
    }

    // This test is complete and serves as an example.
    [Test]
    public void TestSquareFunction()
    {
        int[] numbers = { 1, 2, 3, 4, 5 };
        int[] expected = { 1, 4, 9, 16, 25 };
        
        var result = ApplyFunctionToArray(numbers, Square);
        Assert.That(result, Is.EqualTo(expected));
    }

    // TODO: Complete this test by addressing the TODO comments.
    [Test]
    public void TestCapitalizeFunction()
    {
        
        string[] words = { "hello", "world", "example" };
        string[] expected = { "HELLO", "WORLD", "EXAMPLE" };
    
        // TODO: Apply the Capitalize function to the input array of words and store the result
        // var result = ...
       
    
        // TODO: uncomment after using the ApplyFunctionToArray method with the Capitalize function
       // Assert.That(result, Is.EqualTo(expected));
    }
}
```

## Instructions

1. Review the `TestSquareFunction` method to understand how the `ApplyFunctionToArray` method is used.
2. Address the TODO comments in the `TestCapitalizeFunction` method. Your tasks are to:
    - Apply the `Capitalize` function to the `words` array using the `ApplyFunctionToArray` method.
    - Store the result in a variable named `result`.
    - Uncomment the assertion line to validate your result against the expected array.
3. Ensure your tests pass by running them in your development environment.

