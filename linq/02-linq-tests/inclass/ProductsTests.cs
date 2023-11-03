namespace _02_linq_tests.inclass;

public record Product
{
    public string? Name { get; init; }
    public decimal Price { get; init; }
}

public class Inventory
{
    private readonly List<Product> _products  = new()
    {
        new Product { Name = "Notebook", Price = 3.99m },
        new Product { Name = "Pen", Price = 0.99m },
        new Product { Name = "Pencil", Price = 0.49m },
        new Product { Name = "Laptop", Price = 599.99m },
        new Product { Name = "Desktop", Price = 799.99m },
        new Product { Name = "Monitor", Price = 129.99m },
        // ... Other products
    };
   
    // note: were returning a named tuple here
    public IEnumerable<(string? Name, decimal Price)> GetProductsCheaperThan(decimal maxPrice)
    {
        // TODO: Use LINQ to filter _products and return a sequence of named tuples (Name, Price).
        throw new NotImplementedException();
    }
}

public class ProductsTests
{
    [Test]
    public void GetProductsCheaperThan_Should_ReturnCorrectResults()
    {
        var inventory = new Inventory();

        var affordableProducts = inventory.GetProductsCheaperThan(10.00m).ToList();

        var expectedProducts = new List<(string Name, decimal Price)>
        {
            ("Notebook", 3.99m),
            ("Pen", 0.99m),
            ("Pencil", 0.49m)
        };

        CollectionAssert.AreEquivalent(expectedProducts, affordableProducts, "The affordable products list did not match the expected list of products.");
    }

}