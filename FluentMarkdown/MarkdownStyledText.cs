using System.Text;

namespace FluentMarkdown;

/// <summary>
/// Represents a text with Markdown styling.
/// </summary>
public class MarkdownStyledText
{
    internal MarkdownStyledText(string text)
    {
        if (text is null)
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Text cannot be empty.", nameof(text));
        }

        Text = text.Trim();
    }

    private bool IsBold { get; set; }

    private bool IsHighlighted { get; set; }

    private bool IsItalic { get; set; }

    private bool IsStrikethrough { get; set; }

    private bool IsSubscript { get; set; }

    private bool IsSuperscript { get; set; }

    private string Text { get; }

    public static implicit operator string(MarkdownStyledText text)
    {
        return text.ToString();
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        var builder = new StringBuilder();

        // keep this first and subscript last to limit need for html tags
        if (IsStrikethrough)
        {
            builder.Append("~~");
        }

        if (IsBold)
        {
            builder.Append("**");
        }

        if (IsItalic)
        {
            builder.Append('*');
        }

        if (IsHighlighted)
        {
            builder.Append("==");
        }

        if (IsSuperscript)
        {
            builder.Append('^');
        }

        // keep this one last to limit need for html tags
        var needsSubscriptTags = false;
        if (IsSubscript)
        {
            if (IsStrikethrough && builder.Length == 2)
            {
                needsSubscriptTags = true;
            }
            else
            {
                builder.Append('~');
            }
        }

        var result = builder.ToString();
        var suffix = new string(result.Reverse().ToArray());
        builder.Append(EscapeFormattingCharacters(Text, new HashSet<char>(builder.ToString())));
        builder.Append(suffix);
        return needsSubscriptTags
            ? $"<sub>{builder}</sub>"
            : builder.ToString();
    }

    /// <summary>
    /// Applies the desired bold styling to the text.
    /// </summary>
    /// <param name="isBold">
    /// An optional value indicating whether the text should be bold.
    /// </param>
    /// <returns>
    /// The original <see cref="MarkdownStyledText"/> instance with the specified bold styling.
    /// </returns>
    public MarkdownStyledText WithBold(bool isBold = true)
    {
        IsBold = isBold;
        return this;
    }

    /// <summary>
    /// Applies the desired highlight styling to the text.
    /// </summary>
    /// <param name="isHighlighted">
    /// An optional value indicating whether the text should be highlighted.
    /// </param>
    /// <returns>
    /// The original <see cref="MarkdownStyledText"/> instance with the specified highlight styling.
    /// </returns>
    public MarkdownStyledText WithHighlight(bool isHighlighted = true)
    {
        IsHighlighted = isHighlighted;
        return this;
    }

    /// <summary>
    /// Applies the desired italic styling to the text.
    /// </summary>
    /// <param name="isItalic">
    /// An optional value indicating whether the text should be italic.
    /// </param>
    /// <returns>
    /// The original <see cref="MarkdownStyledText"/> instance with the specified italic styling.
    /// </returns>
    public MarkdownStyledText WithItalic(bool isItalic = true)
    {
        IsItalic = isItalic;
        return this;
    }

    /// <summary>
    /// Applies the desired strike-through styling to the text.
    /// </summary>
    /// <param name="isStrikethrough">
    /// An optional value indicating whether the text should be styled with strike-through.
    /// </param>
    /// <returns>
    /// The original <see cref="MarkdownStyledText"/> instance with the specified strike-through styling.
    /// </returns>
    public MarkdownStyledText WithStrikethrough(bool isStrikethrough = true)
    {
        IsStrikethrough = isStrikethrough;
        return this;
    }

    /// <summary>
    /// Applies the desired subscript styling to the text.
    /// </summary>
    /// <param name="isSubscript">
    /// An optional value indicating whether the text should be styled with subscript.
    /// </param>
    /// <returns>
    /// The original <see cref="MarkdownStyledText"/> instance with the specified subscript styling.
    /// </returns>
    public MarkdownStyledText WithSubscript(bool isSubscript = true)
    {
        IsSubscript = isSubscript;
        if (IsSuperscript)
        {
            IsSuperscript = false;
        }

        return this;
    }

    /// <summary>
    /// Applies the desired superscript styling to the text.
    /// </summary>
    /// <param name="isSuperscript">
    /// An optional value indicating whether the text should be styled with superscript.
    /// </param>
    /// <returns>
    /// The original <see cref="MarkdownStyledText"/> instance with the specified superscript styling.
    /// </returns>
    public MarkdownStyledText WithSuperscript(bool isSuperscript = true)
    {
        IsSuperscript = isSuperscript;
        if (IsSubscript)
        {
            IsSubscript = false;
        }

        return this;
    }

    private static string EscapeFormattingCharacters(string text, IEnumerable<char> charactersToEscape)
    {
        foreach (var character in charactersToEscape)
        {
            text = text.Replace(character.ToString(), $"\\{character}");
        }

        return text;
    }
}