# Programming Assignment: Implementing Lazy Evaluation with `yield return`

## Objective
Implement the method `ApplyFunctionToArrayLazyForLoop` using lazy evaluation with `yield return` to complete the provided unit tests.

## Starting Code
You will begin with a partially implemented class `LazyEvaluationTests`. Your task is to complete the `ApplyFunctionToArrayLazyForLoop` method based on the TODO comments.

```csharp
namespace _02_linq_tests.inclass
{
    public class LazyEvaluationTests
    {
        private static int Square(int x) => x * x;

        private static IEnumerable<T> ApplyFunctionToArrayLazyForLoop<T>(IReadOnlyList<T> list, Func<T, T> operation)
        {
            for (var i = 0; i < list.Count; i++)
            {                
                yield return operation(list[i]);
            }           
        }

        private static IEnumerable<T> ApplyFunctionToArrayLazyForeach<T>(IReadOnlyList<T> array, Func<T, T> operation)
        {
            foreach (var t in array)
            {
                yield return operation(t);
            }
        }

        // Test methods are provided for you.
        // Your implemented methods will be tested against these when you're done.
    }
}
```

## Instructions

1. Inside the `ApplyFunctionToArrayLazyForLoop` method, remove the `throw new NotImplementedException();` line.
2. Ensure that the `TestSquareFunctionLazyForLoop` unit test passes after your implementation. This will validate your lazy evaluation method.
