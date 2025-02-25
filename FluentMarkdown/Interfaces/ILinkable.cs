namespace FluentMarkdown.Interfaces;

/// <summary>
/// Flags a builder as being able to add a link.
/// </summary>
/// <typeparam name="T">
/// The type that will be returned by builder methods.
/// </typeparam>
public interface ILinkable<T>
    where T : InlineMarkdownBuilder<T>
{
    /// <summary>
    /// Adds a linked URL to the content.
    /// </summary>
    /// <param name="url">
    /// The destination URL to add as a link.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    T AddLink(string url);

    /// <summary>
    /// Adds a linked text to the content.
    /// </summary>
    /// <param name="url">
    /// The destination URL.
    /// </param>
    /// <param name="text">
    /// The text to display as the link.
    /// </param>
    /// <param name="title">
    /// An optional title of the link that will be displayed as a tooltip when hovered over.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    T AddLink(string url, string text, string? title = null);

    /// <summary>
    /// Adds a linked image to the content.
    /// </summary>
    /// <param name="destinationAddress">
    /// The destination URL for the link.
    /// </param>
    /// <param name="imageAddress">
    /// The URL of the image to add.
    /// </param>
    /// <param name="altText">
    /// The alternative text to display if the image cannot be loaded.
    /// </param>
    /// <param name="title">
    /// An optional title of the image that will be displayed as a tooltip when hovered over.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    T AddLinkedImage(string destinationAddress, string imageAddress, string altText, string? title = null);
}