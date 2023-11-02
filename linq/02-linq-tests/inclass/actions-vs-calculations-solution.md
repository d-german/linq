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
### Answer: `CalculateArea` is a Calculation

### Method 2: `PrintMessage`

```csharp
public void PrintMessage(string message)
{
    Console.WriteLine(message);
}
```
### Answer: `PrintMessage` is an Action

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
### Answer: `AddToDatabase` is an Action

### Method 4: `FindMaximum`

```csharp
public static int FindMaximum(int[] numbers)
{
    return numbers.Max();
}
```
### Answer: `FindMaximum` is a Calculation

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
### Answer: `SendMessage` is an Action

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


### Answer: `GetOrCreateUserProfile` is an Action
This method is tricky because:

- It calculates the user profile by attempting to retrieve it from the database, which would be a calculation if the profile exists (pure function).
- However, it also has the side effect of creating a new user profile and saving it to the database if it does not exist, which is an action (impure function).


### Refined Discussion on Method 6: `GetOrCreateUserProfile`

The issue with `GetOrCreateUserProfile` is that it combines two distinct operations into one method. This complicates our ability to distinguish between a calculation and an action, as the method both retrieves a user profile and creates a new one if it doesn't exist. While this may seem like a violation of the Single Responsibility Principle (SRP), it's important to note that Robert C. Martin, who formulated the SOLID principles, clarified that SRP applies to classes and not methods. However, he also emphasized that methods should do only one thing.

To improve the design of our method according to this clarification, we can refactor it into two separate methods. Each method will have a single, clear purpose, making it easier to identify whether it's performing a calculation or an action.

```csharp
public UserProfile RetrieveUserProfile(string userId)
{
    return database.GetUserProfile(userId);
}

public UserProfile EnsureUserProfileExists(string userId)
{
    var profile = RetrieveUserProfile(userId);
    if (profile == null)
    {
        profile = new UserProfile(userId);
        database.SaveUserProfile(profile);
    }
    return profile;
}
```

Now, `RetrieveUserProfile` is clearly a calculation(or is it?*). It simply retrieves and returns the `UserProfile` associated with the given `userId`, if it exists. The `EnsureUserProfileExists` method, while still performing two operations, has a singular higher-level purpose: to guarantee the existence of a user profile, thus making it easier to understand and maintain the separation between retrieving data (calculation) and modifying state (action).


This refactoring maintains the original intent of ensuring a user profile exists but does so with two separate methods, each with a clear and distinct responsibility. The `RetrieveUserProfile` method handles the calculation, and the `EnsureUserProfileExists` method wraps the action and calculation in a process that ensures the user profile is available.

### Further Discussion on `RetrieveUserProfile`

While `RetrieveUserProfile` does not change the state of the system and appears to be a calculation, it's worth noting that its output can change based on the state of the database. If the method is called once and returns null because the `userId` does not exist, and if subsequently the user is created and the method is called again with the same `userId`, it will return a different result. This characteristic means that, strictly speaking, `RetrieveUserProfile` does not always perform a pure calculation, as its output is not solely dependent on its input parameters but also on the state of the database at the time of execution.
