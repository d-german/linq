```csharp
/// <summary>
/// A service used to generate unique ids for lists that contain objects that
/// implement IDataIndexedById
/// </summary>
public interface IDataIdGeneratorService
{
    /// <summary>
    /// Set unique ids for any item that does not have an id (an id less than 0)
    /// </summary>
    /// <param name="items">The list of items</param>
    void GenerateUniqueIds(IEnumerable<IDataIndexedById> items);
}
```

```csharp
/// <summary>
/// Implementers have a unique ID
/// </summary>
public interface IDataIndexedById
{
    /// <summary>
    /// The ID for the object, unique among objects of the same type. 
    /// </summary>
    string Id { get; set; }
}
```

## DataIdGeneratorService Class Implementation

- Inherits from `IDataIdGeneratorService`.
- Starts IDs from `100` (as defined by the `StartId` constant).
- The `GenerateUniqueIds` method performs the following operations:
    1. Validates the input list is not null.
    2. Creates a new list that converts each `Id` to a nullable long for processing.
    3. Filters out the items that need new IDs (those with IDs less than 0).
    4. If there are items needing IDs, it finds the maximum existing ID from the list, starting from `StartId` if necessary.
    5. Iterates through each item that needs an ID and assigns a new unique ID by incrementing the maximum ID found.

This process modifies the list by ensuring each object has a valid and unique ID, which is necessary for maintaining data integrity and for later retrieval or reference of the items in the list.

```csharp
public class DataIdGeneratorService : IDataIdGeneratorService
{
    private const long StartId = 100;

    public void GenerateUniqueIds(IEnumerable<IDataIndexedById> dataIndexedByIds)
    {
        var items = Guard.VerifyArgumentNotNull(dataIndexedByIds, nameof(dataIndexedByIds))
            .Select(item => new
            {
                IDataIndexedById = item,
                NullableId = ToNullableId(item.Id)
            }).ToList();

        var newItems = items.Where(item => IsNewId(item.NullableId)).ToList();

        if (!newItems.Any()) return;

        var maxId = items
            .Where(item => item.NullableId.HasValue)
            .Select(item =>
            {
                    
                Debug.Assert(item.NullableId != null, $"{nameof(item.NullableId)} should not be null");
                return item.NullableId.Value;
            })
            .Append(StartId)
            .Max();

        foreach (var newItem in newItems)
        {
            newItem.IDataIndexedById.Id = (++maxId).ToString();
        }
    }

    private static long? ToNullableId(string str) => long.TryParse(str, out var temp) ? temp : null;

    private static bool IsNewId(long? s) => s < 0;
}
```


