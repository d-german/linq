namespace _01_delegates_test.inclass;

public static class Calculator
{
    public static int Add(int x, int y)
    {
        return x + y;
    }

    public static int Subtract(int x, int y)
    {
        return x - y;
    }

    public static int Multiply(int x, int y)
    {
        return x * y;
    }

    public static int Divide(int x, int y)
    {
        if (y == 0) throw new DivideByZeroException();
        return x / y;
    }
}

[TestFixture]
public class CalculatorTests
{
    // TODO: Define a delegate that can point to methods with two int parameters returning an int
  
    
    [Test]
    public void Add_ReturnsCorrectSum()
    {
        // TODO: Create an instance of the CalculatorDelegate pointing to the Add method
        //CalculatorDelegate addDelegate = Calculator.Add;

        // TODO: Invoke the delegate with sample values to get the sum
        //var sum = addDelegate(5, 3);

        // TODO: Assert that the result from the delegate invocation is as expected
        //Assert.That(sum, Is.EqualTo(8));
    }

    [Test]
    public void Subtract_ReturnsCorrectDifference()
    {
        // TODO: Create an instance of the CalculatorDelegate pointing to the Subtract method
      

        // TODO: Invoke the delegate with sample values to get the difference
       

        // TODO: Assert that the result from the delegate invocation is as expected
        
    }

    [Test]
    public void Multiply_ReturnsCorrectProduct()
    {
        // TODO: Create an instance of the CalculatorDelegate pointing to the Multiply method
       

        // TODO: Invoke the delegate with sample values to get the product
        
       
        // TODO: Assert that the result from the delegate invocation is as expected
       
    }

    [Test]
    public void Divide_ReturnsCorrectQuotient()
    {
        // TODO: Create an instance of the CalculatorDelegate pointing to the Divide method
       

        // TODO: Invoke the delegate with sample values to get the quotient
       

        // TODO: Assert that the result from the delegate invocation is as expected
       
    }

    [Test]
    public void Divide_ByZero_ThrowsException()
    {
        // TODO: Create an instance of the CalculatorDelegate pointing to the Divide method
        // CalculatorDelegate divideDelegate = Calculator.Divide;

        // TODO: Assert that invoking the delegate with a divisor of zero throws a DivideByZeroException
        //Assert.Throws<DivideByZeroException>(() => divideDelegate(6, 0));
    }
}
