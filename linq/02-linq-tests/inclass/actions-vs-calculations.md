# Actions vs. Calculations Assignment

In this assignment, you'll be presented with several C# methods. Your task is to determine whether each method is an *action* or a *calculation*. Remember, *actions* are impure functions that produce side effects, while *calculations* are pure functions that do not produce any side effects.

## Instructions

For each method provided below, write down whether it's an action or a calculation and explain your reasoning.

### Method 1: `CalculateArea`

```csharp
public static double CalculateArea(double radius)
{
    return Math.PI * radius * radius;
}
```

### Method 2: `PrintMessage`

```csharp
public void PrintMessage(string message)
{
    Console.WriteLine(message);
}
```

### Method 3: `AddToDatabase`

```csharp
public bool AddToDatabase(string record)
{
    try
    {
        database.Add(record);
        return true;
    }
    catch (Exception)
    {
        return false;
    }
}
```

### Method 4: `FindMaximum`

```csharp
public static int FindMaximum(int[] numbers)
{
    return numbers.Max();
}
```

### Method 5: `SendMessage`

```csharp
public bool SendMessage(string user, string message)
{
    if (CanSendMessage(user))
    {
        messageService.Send(user, message);
        return true;
    }
    return false;
}
```


### Method 6: `GetOrCreateUserProfile`

```csharp
public UserProfile GetOrCreateUserProfile(string userId)
{
    var profile = database.GetUserProfile(userId);
    if (profile == null)
    {
        profile = new UserProfile(userId);
        database.SaveUserProfile(profile);
    }
    return profile;
}
```