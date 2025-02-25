using FluentMarkdown.Internals;

namespace FluentMarkdown.Builders;

/// <summary>
/// A builder for elements that can be nested within other elements.
/// </summary>
public class NestableElementBuilder
{
    internal NestableElementBuilder(LinePrefixStack prefixes)
    {
        Prefixes = prefixes;
    }

    internal LinePrefixStack Prefixes { get; }

    internal InlineMarkdownBuilder Builder { get; } = new();

    /// <inheritdoc/>
    public override string ToString()
    {
        return Builder.ToString();
    }
}