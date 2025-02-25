using System.Text;
using FluentMarkdown.Internals;

namespace FluentMarkdown;

/// <summary>
/// Represents an image in Markdown.
/// </summary>
public class MarkdownImage
{
    internal MarkdownImage(string address)
    {
        if (address is null)
        {
            throw new ArgumentNullException(nameof(address));
        }

        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ArgumentException("Address cannot be empty.", nameof(address));
        }

        Address = address;
    }

    /// <summary>
    /// Gets the address of the image.
    /// </summary>
    public string Address { get; }

    /// <summary>
    /// Gets the alternate text to be displayed for the image.
    /// </summary>
    public string? AltText { get; private set; }

    /// <summary>
    /// Gets the title of the image which is displayed when the image is hovered over.
    /// </summary>
    public string? Title { get; private set; }

    public static implicit operator string(MarkdownImage image)
    {
        return image.ToString();
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        var result = new StringBuilder();
        var encodedAddress = Address.EncodeUrl();
        result.Append($"![{AltText}]({encodedAddress}");
        if (!string.IsNullOrWhiteSpace(Title))
        {
            result.Append($" \"{Title}\"");
        }

        return result.Append(")").ToString();
    }

    /// <summary>
    /// Sets the alternate text to be displayed for the image.
    /// </summary>
    /// <param name="altText">
    /// The alternate text to be displayed for the image.
    /// </param>
    /// <returns>
    /// The current <see cref="MarkdownImage"/> instance.
    /// </returns>
    public MarkdownImage WithAltText(string? altText)
    {
        AltText = altText;
        return this;
    }

    /// <summary>
    /// Sets the title of the image which is displayed when the image is hovered over.
    /// </summary>
    /// <param name="title">
    /// The title of the image.
    /// </param>
    /// <returns>
    /// The current <see cref="MarkdownImage"/> instance.
    /// </returns>
    public MarkdownImage WithTitle(string? title)
    {
        Title = title;
        return this;
    }
}