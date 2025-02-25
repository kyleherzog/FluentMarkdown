using FluentMarkdown.Internals;

namespace FluentMarkdown.Builders;

/// <summary>
/// A markdown builder for adding content to a paragraph.
/// </summary>
/// <typeparam name="T">
/// The type that will be returned by builder methods.
/// </typeparam>
public class ParagraphMarkdownBuilder<T> : InlineStyledMarkdownBuilder<T>
    where T : ParagraphMarkdownBuilder<T>
{
    internal ParagraphMarkdownBuilder()
    {
        Prefixes = new();
    }

    internal LinePrefixStack Prefixes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the builder is currently in the process of building a line.
    /// </summary>
    protected bool IsBuildingLine { get; set; }

    /// <inheritdoc/>
    public override T Add(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return (T)this;
        }

        if (!text!.EndsWith(Environment.NewLine))
        {
            IsBuildingLine = true;
        }

        return base.Add(text);
    }

    /// <summary>
    /// Appends the specified text to the content followed by the default line terminator.
    /// </summary>
    /// <param name="text">
    /// The text to append to the content.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public T AddLine(string? text) => AddLine(b => b.Add(text));

    /// <summary>
    /// Appends the specified text to the content followed by two spaces and the default line terminator. If not at the beginning of a line,
    /// a line terminator is added before the text.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to builder for the content that will be added to the line.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified action is <see langword="null"/>.
    /// </exception>
    public T AddLine(Action<InlineStyledMarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        StartNewLine();
        var lineBuilder = new InlineStyledMarkdownBuilder();
        addContent(lineBuilder);
        var content = lineBuilder.ToString().TrimEnd();
        Add(content);
        Add("  ");

        return CompleteLine();
    }

    /// <summary>
    /// Appends the default line terminator to the content.
    /// </summary>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public T CompleteLine()
    {
        IsBuildingLine = false;
        return base.Add(Environment.NewLine);
    }

    internal void StartNewLine()
    {
        if (IsBuildingLine)
        {
            CompleteLine();
        }

        Add(Prefixes.ToString());
    }
}