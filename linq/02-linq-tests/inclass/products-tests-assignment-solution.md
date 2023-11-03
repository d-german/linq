```csharp
// note: were returning a named tuple here
public IEnumerable<(string? Name, decimal Price)> GetProductsCheaperThan(decimal maxPrice)
{
    return _products
        .Where(product => product.Price < maxPrice)
        .Select(product => (product.Name, product.Price));
}
```