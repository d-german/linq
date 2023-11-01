# Programming Assignment: Delegates and Unit Testing

## Objective
Your task is to implement a series of delegate-based unit tests for a simple `Calculator` class. You will need to address every `TODO` comment in the provided starter code, uncomment the necessary lines, and ensure that the unit tests pass successfully.

## Starter Code
Below is the provided starter code that you will use as a starting point for your assignment:

```csharp
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

    // Repeat for the Subtract, Multiply, Divide, and Divide_ByZero_ThrowsException tests
    // Remember to uncomment and complete each section.
}
```

## Instructions

1. **Define the Delegate**: Uncomment the first `TODO` and define a delegate named `CalculatorDelegate` that matches the signature of the operations in the `Calculator` class.

2. **Implement Test Methods**: For each test method:
    - Uncomment the `TODO` lines.
    - Create an instance of `CalculatorDelegate` that points to the corresponding `Calculator` method.
    - Invoke the delegate with appropriate sample values.
    - Assert that the result from the delegate invocation matches the expected outcome.

3. **Handle Exceptions**: In the `Divide_ByZero_ThrowsException` test, ensure that the test correctly asserts that a `DivideByZeroException` is thrown when dividing by zero.

Ensure that you have addressed all `TODO` comments and that each test method verifies the correct behavior of the `Calculator` operations using delegates.

