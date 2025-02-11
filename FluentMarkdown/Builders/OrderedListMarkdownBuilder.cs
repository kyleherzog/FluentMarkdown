using FluentMarkdown.Internals;

namespace FluentMarkdown.Builders;

/// <summary>
/// A markdown builder that allows for the addition of ordered list content.
/// </summary>
public class OrderedListMarkdownBuilder : MarkdownBuilder
{
    private int nextItemNumber = 1;

    internal OrderedListMarkdownBuilder(int listLevel, LinePrefixStack prefixes)
    {
        ListLevel = listLevel;
        Prefixes = prefixes;
    }

    /// <summary>
    /// Adds an ordered list item to the content.
    /// </summary>
    /// <param name="text">
    /// The text to add as an ordered list item.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public MarkdownBuilder AddOrderedListItem(string text)
    {
        AddOrderedListItem(nextItemNumber, text);
        nextItemNumber++;
        return this;
    }

    /// <summary>
    /// Adds an ordered list item to the content using the specified action.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the builder that will generate the content inside the ordered list item.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified action is <see langword="null"/>.
    /// </exception>
    public MarkdownBuilder AddOrderedListItem(Action<MarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        AddOrderedListItem(nextItemNumber, addContent);
        nextItemNumber++;
        return this;
    }

    /// <summary>
    /// Adds an ordered list item to the content using the next number in the ordered list.
    /// </summary>
    /// <param name="text">
    /// The text to add as an ordered list item.
    /// </param>
    /// <param name="addContent">
    /// The action to apply to the builder that will generate additional content beyond the text provided.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified action is <see langword="null"/>.
    /// </exception>
    public MarkdownBuilder AddOrderedListItem(string text, Action<MarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        AddOrderedListItem(nextItemNumber, b =>
        {
            b.Add(text);
            StartParagraph();
            addContent(b);
        });
        nextItemNumber++;

        return this;
    }
}