using System.Text;
using FluentMarkdown.Internals;

namespace FluentMarkdown;

/// <summary>
/// Represents a link in Markdown.
/// </summary>
public class MarkdownLink
{
    internal MarkdownLink(string url, string? displayText = null)
    {
        if (url is null)
        {
            throw new ArgumentNullException(nameof(url));
        }

        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException("URL cannot be empty.", nameof(url));
        }

        DisplayText = displayText;
        Destination = url;
    }

    /// <summary>
    /// Gets the text to be displayed for the link.
    /// </summary>
    public string? DisplayText { get; }

    /// <summary>
    /// Gets the title of the link which is displayed when the link is hovered over.
    /// </summary>
    public string? Title { get; private set; }

    /// <summary>
    /// Gets the URL of the link to the destination.
    /// </summary>
    public string Destination { get; }

    public static implicit operator string(MarkdownLink link)
    {
        return link.ToString();
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        var result = new StringBuilder();

        var encodedDestination = Destination.EncodeUrl();
        var displayText = DisplayText ?? Destination;

        if (string.IsNullOrEmpty(DisplayText) && encodedDestination == displayText)
        {
            return $"<{Destination}>";
        }

        result.Append($"[{displayText}]({encodedDestination}");
        if (!string.IsNullOrWhiteSpace(Title))
        {
            result.Append($" \"{Title}\"");
        }

        result.Append(')');
        return result.ToString();
    }

    /// <summary>
    /// Sets the title of the link which is displayed when the link is hovered over.
    /// </summary>
    /// <param name="title">
    /// The title of the link.
    /// </param>
    /// <returns>
    /// The <see cref="MarkdownLink"/> instance with the specified title.
    /// </returns>
    public MarkdownLink WithTitle(string? title)
    {
        Title = title;
        return this;
    }
}