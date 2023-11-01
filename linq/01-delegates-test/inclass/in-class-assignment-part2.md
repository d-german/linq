# Programming Assignment: Direct Lambda Expressions in Unit Tests

## Objective
Refactor unit tests for arithmetic operations by using lambda expressions directly within the test methods, eliminating the need for separate method definitions or delegate types.

## Instructions

Given the example of a refactored test for the `Subtract` operation using a lambda expression directly, apply the same approach to the `Add`, `Multiply`, and `Divide` operations. Additionally, handle the division by zero case.

Your task is to complete the unit test suite by implementing the remaining tests using direct lambda expressions. Here's how you've refactored the `Subtract` test:

```csharp
[TestFixture]
public class CalculatorTests
{
    [Test]
    public void Subtract_ReturnsCorrectDifference()
    {
        Func<int, int, int> subtract = (x, y) => x - y;

        var difference = subtract(5, 3);

        Assert.That(difference, Is.EqualTo(2));
    }

    // TODO: Refactor the remaining test methods to use direct lambda expressions
}
```

## Tasks

- Implement `Add_ReturnsCorrectSum` using a lambda expression.
- Implement `Multiply_ReturnsCorrectProduct` using a lambda expression.
- Implement `Divide_ReturnsCorrectQuotient` using a lambda expression, ensuring to handle non-zero denominators.
- Implement `Divide_ByZero_ThrowsException` using a lambda expression, ensuring to handle the division by zero appropriately.

For each task, invoke the lambda expression with sample values and assert the expected result.

## Example Test Refactor

Here's an example of how to refactor the `Add` operation:

```csharp
[Test]
public void Add_ReturnsCorrectSum()
{
    Func<int, int, int> add = (x, y) => x + y;

    var sum = add(5, 3);

    Assert.That(sum, Is.EqualTo(8));
}
```

## Submission
Submit your completed `CalculatorTests` class with all unit tests passing, each utilizing direct lambda expressions according to the instructions above.


This assignment will demonstrate to the students how lambda expressions can be used directly to perform computations within unit tests, simplifying the code by removing the need for a separate class or delegate definitions.