```csharp
[TestFixture]
public class CalculatorTests
{
    [Test]
    public void Add_ReturnsCorrectSum()
    {
        var addDelegate = (int x, int y) => x + y;

        var sum = addDelegate(5, 3);

        Assert.That(sum, Is.EqualTo(8));
    }

    [Test]
    public void Add_ReturnsCorrectSum2()
    {
        // this is another way to write the same test as above without using a local variable for the delegate
        var sum = ((Func<int, int, int>)((x, y) => x + y))(5, 3);

        Assert.That(sum, Is.EqualTo(8));
    }

    [Test]
    public void Subtract_ReturnsCorrectDifference()
    {
        var subtractDelegate = (int x, int y) => x - y;

        var difference = subtractDelegate(5, 3);

        Assert.That(difference, Is.EqualTo(2));
    }

    [Test]
    public void Multiply_ReturnsCorrectProduct()
    {
        var multiplyDelegate = (int x, int y) => x * y;

        var product = multiplyDelegate(5, 3);

        Assert.That(product, Is.EqualTo(15));
    }

    [Test]
    public void Divide_ReturnsCorrectQuotient()
    {
        var divideDelegate = (int x, int y) => x / y;

        var quotient = divideDelegate(6, 3);

        Assert.That(quotient, Is.EqualTo(2));
    }

    [Test]
    public void Divide_ByZero_ThrowsException()
    {
        var divideDelegate = (int x, int y) => x / y;

        Assert.Throws<DivideByZeroException>(() => divideDelegate(6, 0));
    }
}
```