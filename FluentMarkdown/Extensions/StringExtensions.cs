namespace FluentMarkdown;

/// <summary>
/// Provides extension methods for strings.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Converts a string to a <see cref="MarkdownImage"/>.
    /// </summary>
    /// <param name="address">
    /// The address of the image.
    /// </param>
    /// <returns>
    /// A newly created <see cref="MarkdownImage"/> instance.
    /// </returns>
    public static MarkdownImage AsImage(this string address)
    {
        return new MarkdownImage(address);
    }

    /// <summary>
    /// Converts a string to a <see cref="MarkdownLink"/> with the display text and destination matching the string value.
    /// </summary>
    /// <param name="destination">
    /// The destination URL of the link.
    /// </param>
    /// <returns>
    /// A newly created <see cref="MarkdownLink"/> instance.
    /// </returns>
    public static MarkdownLink AsLink(this string destination)
    {
        return new MarkdownLink(destination);
    }

    /// <summary>
    /// Converts a string to a <see cref="MarkdownLink"/> linked to the specified <paramref name="destination"/>.
    /// </summary>
    /// <param name="text">
    /// The text to display for the link.
    /// </param>
    /// <param name="destination">
    /// The destination URL of the link.
    /// </param>
    /// <returns>
    /// A newly created <see cref="MarkdownLink"/> instance.
    /// </returns>
    public static MarkdownLink WithLinkTo(this string text, string destination)
    {
        return new MarkdownLink(destination, text);
    }
}