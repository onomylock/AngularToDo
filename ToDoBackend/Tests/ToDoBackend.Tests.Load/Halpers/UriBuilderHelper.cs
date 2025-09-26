namespace ToDoBackend.Tests.Load.Halpers;

internal static class UriBuilderHelper
{
    public static Uri GetUri(Uri apiBaseUrl, string relativePath)
    {
        return new Uri(apiBaseUrl, relativePath);
    }
}