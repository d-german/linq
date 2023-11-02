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
   
    public IEnumerable<(string? Name, decimal Price)> GetProductsCheaperThan(decimal maxPrice)
    {
        return _products
            .Where(product => product.Price < maxPrice)
            .Select(product => (product.Name, product.Price));
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