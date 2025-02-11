using FluentMarkdown.Interfaces;

namespace FluentMarkdown.Builders;

/// <summary>
/// A markdown builder that allows for the addition of inline styled content.
/// </summary>
/// <typeparam name="T">
/// The type that will be returned by builder methods.
/// </typeparam>
public class InlineStyledMarkdownBuilder<T> : InlineMarkdownBuilder<T>, IImageAddable<T>, ILinkable<T>
    where T : InlineStyledMarkdownBuilder<T>
{
    /// <summary>
    /// Adds an inline code block to the content.
    /// </summary>
    /// <param name="text">
    /// The text to add as the inline code block.
    /// </param>
    /// <returns>
    /// The current instance of the <typeparamref name="T"/> class.
    /// </returns>
    public T AddCode(string text) => string.IsNullOrEmpty(text) ? (T)this : AddCode(x => x.Add(text.Replace("`", "``")));

    /// <summary>
    /// Adds an inline code block to the content using the specified action.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the content that will be inside the inline code block.
    /// </param>
    /// <returns>
    /// The current instance of the <typeparamref name="T"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified action is <see langword="null"/>.
    /// </exception>
    public T AddCode(Action<CodeMarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        return AddInlineStyle("`", addContent);
    }

    /// <inheritdoc/>
    public T AddImage(string url, string altText, string? title = null) => Toolbox.AddImage(url, altText, title);

    /// <inheritdoc/>
    public T AddLink(string url) => Toolbox.AddLink(url);

    /// <inheritdoc/>
    public T AddLink(string url, string text, string? title = null) => Toolbox.AddLink(url, text, title);

    /// <inheritdoc/>
    public T AddLinkedImage(string destinationAddress, string imageAddress, string altText, string? title = null)
        => Toolbox.AddLinkedImage(destinationAddress, imageAddress, altText, title);

    /// <summary>
    /// Adds the specified text to the content in bold styling.
    /// </summary>
    /// <param name="text">
    /// The text to add to the content with bold styling.
    /// </param>
    /// <returns>
    /// The current instance of the <typeparamref name="T"/> class.
    /// </returns>
    public T AddTextBold(string text) => string.IsNullOrEmpty(text) ? (T)this : AddTextBold(x => x.Add(text));

    /// <summary>
    /// Adds bold text to the content using the specified action.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the content that will be bold.
    /// </param>
    /// <returns>
    /// The current instance of the <typeparamref name="T"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified action is <see langword="null"/>.
    /// </exception>
    public T AddTextBold(Action<EmphasisMarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        return AddInlineStyle("**", addContent);
    }

    /// <summary>
    /// Adds the specified text to the content in bold and italic styling.
    /// </summary>
    /// <param name="text">
    /// The text to add to the content with bold and italic styling.
    /// </param>
    /// <returns>
    /// The current instance of the <typeparamref name="T"/> class.
    /// </returns>
    public T AddTextBoldItalic(string text) => string.IsNullOrEmpty(text) ? (T)this : AddTextBoldItalic(x => x.Add(text));

    /// <summary>
    /// Adds bold and italic text to the content using the specified action.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the content that will be bold and italic.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public T AddTextBoldItalic(Action<EmphasisMarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        return AddInlineStyle("***", addContent);
    }

    /// <summary>
    /// Adds the specified text to the content in italic styling.
    /// </summary>
    /// <param name="text">
    /// The text to add to the content with italic styling.
    /// </param>
    /// <returns>
    /// The current instance of the <typeparamref name="T"/> class.
    /// </returns>
    public T AddTextItalic(string text) => string.IsNullOrEmpty(text) ? (T)this : AddTextItalic(x => x.Add(text));

    /// <summary>
    /// Applies italic styling to the content added by the specified action.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the content that will be italic.
    /// </param>
    /// <returns>
    /// The current instance of the <typeparamref name="T"/> class.
    /// </returns>
    public T AddTextItalic(Action<EmphasisMarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        return AddInlineStyle("*", addContent);
    }

    private T AddInlineStyle<TBuilder>(string wrapper, Action<TBuilder> addContent)
        where TBuilder : InlineMarkdownBuilder<TBuilder>, new()
    {
        Add(wrapper);
        var fragmentBuilder = new TBuilder();
        addContent(fragmentBuilder);
        Add(fragmentBuilder.ToString());
        return Add(wrapper);
    }
}