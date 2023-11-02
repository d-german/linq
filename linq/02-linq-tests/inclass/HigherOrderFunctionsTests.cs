namespace _02_linq_tests.inclass;

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
    
    [Test]
    public void TestSquareFunction()
    {
        int[] numbers = { 1, 2, 3, 4, 5 };
        int[] expected = { 1, 4, 9, 16, 25 };
        
        var result = ApplyFunctionToArray(numbers, Square);
        Assert.That(result, Is.EqualTo(expected));
    }

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