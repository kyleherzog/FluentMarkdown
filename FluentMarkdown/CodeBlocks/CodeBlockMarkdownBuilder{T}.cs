namespace FluentMarkdown.CodeBlocks;

/// <summary>
/// A markdown builder designed to generate content within a code block.
/// </summary>
/// <typeparam name="T">
/// The type that will be returned by builder methods.
/// </typeparam>
public class CodeBlockMarkdownBuilder<T> : InlineMarkdownBuilder<T>
    where T : InlineMarkdownBuilder<T>
{
    /// <summary>
    /// Adds a line to the code block.
    /// </summary>
    /// <param name="line">
    /// The line to add to the code block.
    /// </param>
    /// <returns>
    /// The current instance of the <typeparamref name="T"/> class.
    /// </returns>
    public T AddLine(string line)
    {
        return Add($"{line}{Environment.NewLine}");
    }
}