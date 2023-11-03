# Programming Assignment: Implementing LINQ Query for Product Filtering

## Objective
Complete the implementation of the `GetProductsCheaperThan` method in the `Inventory` class to return products priced below a specified value using LINQ.

### C# Named Tuples

Named tuples in C# group related values without defining a formal class or struct. They provide semantic meaning to tuple fields through naming.

### Why Named Tuples?
- **Clarity**: Access tuple elements by name instead of position for clearer code.
- **Convenience**: Simplify methods that need to return multiple values.

### Example
```csharp
var book = (Title: "1984", Author: "Orwell");
Console.WriteLine(book.Title); // Outputs "1984"
```

Named tuples are ideal for lightweight data structures where temporary grouping of items is needed with the benefit of named properties for easy access.

## Assignment Details
Given a `Product` record and an `Inventory` class with predefined products, your task is to implement the `GetProductsCheaperThan` method. This method should use LINQ to filter products by price and return their name and price.

### Starting Code
```csharp
public record Product
{
    public string? Name { get; init; }
    public decimal Price { get; init; }
}

public class Inventory
{
    private List<Product> _products = new()
    {
        new Product { Name = "Notebook", Price = 3.99m },
        new Product { Name = "Pen", Price = 0.99m },
        new Product { Name = "Pencil", Price = 0.49m },
        // ... Other products
    };

    // Implement this method using LINQ.
    public IEnumerable<(string? Name, decimal Price)> GetProductsCheaperThan(decimal maxPrice)
    {
        // TODO: Use LINQ to filter _products and return a sequence of named tuples (Name, Price).
        throw new NotImplementedException();
    }
}
```

### Your Task
Implement the `GetProductsCheaperThan` method to return an `IEnumerable` of named tuples. Each tuple should contain the `Name` and `Price` of each product cheaper than the `maxPrice` parameter.