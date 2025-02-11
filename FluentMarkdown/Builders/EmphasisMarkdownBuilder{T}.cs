using FluentMarkdown.Interfaces;

namespace FluentMarkdown.Builders;

/// <summary>
/// A markdown builder designed to be used within a text emphasis block.
/// </summary>
/// <typeparam name="T">
/// The type that will be returned by builder methods.
/// </typeparam>
public class EmphasisMarkdownBuilder<T> : InlineMarkdownBuilder<T>, IImageAddable<T>, ILinkable<T>
    where T : InlineMarkdownBuilder<T>
{
    /// <inheritdoc/>
    public T AddImage(string url, string altText, string? title = null) => Toolbox.AddImage(url, altText, title);

    /// <inheritdoc/>
    public T AddLink(string url, string text, string? title = null) => Toolbox.AddLink(url, text, title);

    /// <inheritdoc/>
    public T AddLink(string url) => Toolbox.AddLink(url);

    /// <inheritdoc/>
    public T AddLinkedImage(string destinationAddress, string imageAddress, string altText, string? title = null)
        => Toolbox.AddLinkedImage(destinationAddress, imageAddress, altText, title);
}