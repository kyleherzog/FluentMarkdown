using FluentMarkdown.CodeBlocks;

namespace FluentMarkdown;

/// <summary>
/// Holds configuration values for FluentMarkdown.
/// </summary>
public class MarkdownConfiguration
{
    /// <summary>
    /// Gets the global instance of the <see cref="MarkdownConfiguration"/> class.
    /// </summary>
    public static MarkdownConfiguration Global { get; } = new();

    /// <summary>
    /// Gets the default code language to use when adding code blocks.
    /// </summary>
    public string DefaultCodeLanguage { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the number of spaces to use for indenting lines for nested elements.
    /// </summary>
    public int IndentSize { get; private set; } = 4;

    /// <summary>
    /// Sets the default code language to use when adding code blocks.
    /// </summary>
    /// <param name="defaultCodeLanguage">
    /// The default code language to use when adding code blocks.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownConfiguration"/> class.
    /// </returns>
    public MarkdownConfiguration UsingCodeLanguage(BlockLanguage defaultCodeLanguage) => UsingCodeLanguage(defaultCodeLanguage.ToIdentifier());

    /// <summary>
    /// Sets the default code language to use when adding code blocks.
    /// </summary>
    /// <param name="defaultCodeLanguage">
    /// The default code language to use when adding code blocks.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownConfiguration"/> class.
    /// </returns>
    public MarkdownConfiguration UsingCodeLanguage(string defaultCodeLanguage)
    {
        DefaultCodeLanguage = defaultCodeLanguage;
        return this;
    }

    /// <summary>
    /// Sets the number of spaces to use for indenting lines for nested elements.
    /// </summary>
    /// <param name="indentSize">
    /// The number of spaces to use for indenting lines for nested elements.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownConfiguration"/> class.
    /// </returns>
    public MarkdownConfiguration WithIndentSize(int indentSize)
    {
        if (indentSize < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(indentSize), "Indent size must be greater than 0");
        }

        IndentSize = indentSize;
        return this;
    }
}