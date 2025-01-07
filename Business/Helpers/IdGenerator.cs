namespace Business.Helpers;

/// <summary>
/// A utility class for generating unique identifiers.
/// </summary>
public static class IdGenerator
{
    /// <summary>
    /// Generates a new unique identifier as a string.
    /// </summary>
    /// <returns>A unique identifier (GUID).</returns>
    public static string Generate() => Guid.NewGuid().ToString();
}