namespace FluentMarkdown.CodeBlocks;

/// <summary>
/// A markdown builder designed to be used within an inline code block.
/// </summary>
/// <typeparam name="T">
/// The type that will be returned by builder methods.
/// </typeparam>
public class CodeMarkdownBuilder<T> : InlineMarkdownBuilder<T>
        where T : InlineMarkdownBuilder<T>
{
}