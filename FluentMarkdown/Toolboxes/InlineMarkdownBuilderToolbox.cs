using FluentMarkdown.Interfaces;
using FluentMarkdown.Internals;

namespace FluentMarkdown.Toolboxes;

/// <summary>
/// A provider of helper methods for adding markdown to an inline markdown builder.
/// </summary>
/// <typeparam name="T">
/// The type that will be returned by builder methods.
/// </typeparam>
public class InlineMarkdownBuilderToolbox<T> : IImageAddable<T>, ILinkable<T>
    where T : InlineMarkdownBuilder<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InlineMarkdownBuilderToolbox{T}"/> class.
    /// </summary>
    /// <param name="builder">
    /// The builder which will be used to add the markdown.
    /// </param>
    public InlineMarkdownBuilderToolbox(InlineMarkdownBuilder<T> builder)
    {
        Builder = builder;
    }

    /// <summary>
    /// Gets the builder which will be used to add the markdown.
    /// </summary>
    public InlineMarkdownBuilder<T> Builder { get; }

    /// <inheritdoc/>
    public T AddLink(string url)
    {
        return Builder.Add($"<{url}>");
    }

    /// <inheritdoc/>
    public T AddLink(string url, string text, string? title = null)
    {
        var encodedUrl = url.EncodeUrl();
        Builder.Add($"[{text}]({encodedUrl}");
        if (!string.IsNullOrWhiteSpace(title))
        {
            Builder.Add($" \"{title}\"");
        }

        return Builder.Add(")");
    }

    /// <inheritdoc/>
    public T AddLinkedImage(string destinationAddress, string imageAddress, string altText, string? title = null)
    {
        var encodedDestinationAddress = destinationAddress.EncodeUrl();
        var encodedImageAddress = imageAddress.EncodeUrl();
        Builder.Add($"[![{altText}]({encodedImageAddress}");
        if (!string.IsNullOrWhiteSpace(title))
        {
            Builder.Add($" \"{title}\"");
        }

        return Builder.Add($")]({encodedDestinationAddress})");
    }

    /// <inheritdoc/>
    public T AddImage(string url, string altText, string? title = null)
    {
        var encodedUrl = url.EncodeUrl();
        Builder.Add($"![{altText}]({encodedUrl}");
        if (!string.IsNullOrWhiteSpace(title))
        {
            Builder.Add($" \"{title}\"");
        }

        return Builder.Add(")");
    }
}