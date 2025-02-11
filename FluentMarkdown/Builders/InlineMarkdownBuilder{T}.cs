using System.Text;
using FluentMarkdown.Toolboxes;

namespace FluentMarkdown;

/// <summary>
/// A markdown builder designed to be used to build inline markdown.
/// </summary>
/// <typeparam name="T">
/// The type that will be returned by builder methods.
/// </typeparam>
public class InlineMarkdownBuilder<T>
    where T : InlineMarkdownBuilder<T>
{
    private readonly StringBuilder builder = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="InlineMarkdownBuilder{T}"/> class.
    /// </summary>
    public InlineMarkdownBuilder()
    {
        Toolbox = new(this);
    }

    /// <summary>
    /// Gets the length of the content.
    /// </summary>
    public int Length => builder.Length;

    /// <summary>
    /// Gets the toolbox that can be used by inheritors to add markdown to the content.
    /// </summary>
    protected InlineMarkdownBuilderToolbox<T> Toolbox { get; }

    /// <summary>
    /// Appends the specified text to the content.
    /// </summary>
    /// <param name="text">
    /// The text to append to the content.
    /// </param>
    /// <returns>
    /// The current instance of the <typeparamref name="T"/> class.
    /// </returns>
    public virtual T Add(string? text)
    {
        builder.Append(text);
        return (T)this;
    }

    /// <summary>
    /// Appends the specified text to the content.
    /// </summary>
    /// <param name="markdownBuilder">
    /// Another instance of a builder from which to append to this instance.
    /// </param>
    /// <returns>
    /// The current instance of the <typeparamref name="T"/> class.
    /// </returns>
    public virtual T Add(InlineMarkdownBuilder<T> markdownBuilder)
    {
        if (markdownBuilder is null)
        {
            throw new ArgumentNullException(nameof(markdownBuilder));
        }

        builder.Append(markdownBuilder.builder);
        return (T)this;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return builder.ToString();
    }
}