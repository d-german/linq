```csharp
[TestFixture]
public class CalculatorTests
{
    private delegate int CalculatorDelegate(int x, int y);
    
    [Test]
    public void Add_ReturnsCorrectSum()
    {
        CalculatorDelegate addDelegate = Calculator.Add;

        var sum = addDelegate(5, 3);

        Assert.That(sum, Is.EqualTo(8));
    }

    [Test]
    public void Subtract_ReturnsCorrectDifference()
    {
        CalculatorDelegate subtractDelegate = Calculator.Subtract;

        var difference = subtractDelegate(5, 3);

        Assert.That(difference, Is.EqualTo(2));
    }

    [Test]
    public void Multiply_ReturnsCorrectProduct()
    {
        CalculatorDelegate multiplyDelegate = Calculator.Multiply;

        var product = multiplyDelegate(5, 3);

        Assert.That(product, Is.EqualTo(15));
    }

    [Test]
    public void Divide_ReturnsCorrectQuotient()
    {
        CalculatorDelegate divideDelegate = Calculator.Divide;

        var quotient = divideDelegate(6, 3);

        Assert.That(quotient, Is.EqualTo(2));
    }

    [Test]
    public void Divide_ByZero_ThrowsException()
    {
        CalculatorDelegate divideDelegate = Calculator.Divide;

        Assert.Throws<DivideByZeroException>(() => divideDelegate(6, 0));
    }
}
```