namespace FluentMarkdown.Interfaces;

/// <summary>
/// Flags an object as being able to add an image.
/// </summary>
/// <typeparam name="T">
/// The type that will be returned by builder methods.
/// </typeparam>
public interface IImageAddable<T>
    where T : InlineMarkdownBuilder<T>
{
    /// <summary>
    /// Adds an image to the content.
    /// </summary>
    /// <param name="url">
    /// The URL of the image to add.
    /// </param>
    /// <param name="altText">
    /// The alternative text to display if the image cannot be loaded.
    /// </param>
    /// <param name="title">
    /// An optional title of the image that will be displayed as a tooltip when hovered over.
    /// </param>
    /// <returns>
    ///  The current instance of the <typeparamref name="T"/> class.
    /// </returns>
    public T AddImage(string url, string altText, string? title = null);
}