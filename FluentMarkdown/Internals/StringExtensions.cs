namespace FluentMarkdown.Internals;

internal static class StringExtensions
{
    public static string EncodeUrl(this string value)
    {
        return value.Replace(" ", "%20")
            .Replace("(", "%28")
            .Replace(")", "%29");
    }
}