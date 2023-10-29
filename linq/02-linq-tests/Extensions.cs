namespace _02_linq_tests;

internal static class Extensions
{
    // extension method that displays all elements separated by spaces
    public static string Dump<T>(this IEnumerable<T> data)
    {
        return string.Join(" ", data);
    }
}