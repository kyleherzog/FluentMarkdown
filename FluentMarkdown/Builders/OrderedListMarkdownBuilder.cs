using FluentMarkdown.Internals;

namespace FluentMarkdown.Builders;

/// <summary>
/// A markdown builder that allows for the addition of ordered list content.
/// </summary>
public class OrderedListMarkdownBuilder : OrderedListMarkdownBuilder<OrderedListMarkdownBuilder>
{
    internal OrderedListMarkdownBuilder(int listLevel, LinePrefixStack prefixes)
        : base(listLevel, prefixes)
    {
    }
}