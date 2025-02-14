namespace FluentMarkdown;

/// <summary>
/// Provides extension methods for strings.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Formats as string value as inline code.
    /// </summary>
    /// <param name="code">
    /// The text to format as code.
    /// </param>
    /// <returns>
    /// The text in inline code format.
    /// </returns>
    public static string AsCode(this string code)
    {
        return $"`{code}`";
    }

    /// <summary>
    /// Converts a string to a <see cref="MarkdownImage"/>.
    /// </summary>
    /// <param name="address">
    /// The address of the image.
    /// </param>
    /// <returns>
    /// A newly created <see cref="MarkdownImage"/> instance.
    /// </returns>
    public static MarkdownImage AsImage(this string address)
    {
        return new MarkdownImage(address);
    }

    /// <summary>
    /// Converts a string to a <see cref="MarkdownLink"/> with the display text and destination matching the string value.
    /// </summary>
    /// <param name="destination">
    /// The destination URL of the link.
    /// </param>
    /// <returns>
    /// A newly created <see cref="MarkdownLink"/> instance.
    /// </returns>
    public static MarkdownLink AsLink(this string destination)
    {
        return new MarkdownLink(destination);
    }

    /// <summary>
    /// Applies the desired bold styling to the text.
    /// </summary>
    /// <param name="text">
    /// The text to style.
    /// </param>
    /// <param name="isBold">
    /// An optional value indicating whether the text should be bold.
    /// </param>
    /// <returns>
    /// The new <see cref="MarkdownStyledText"/> instance with the specified bold styling.
    /// </returns>
    public static MarkdownStyledText WithBold(this string text, bool isBold = true)
    {
        return new MarkdownStyledText(text).WithBold(isBold);
    }

    /// <summary>
    /// Applies the desired highlight styling to the text.
    /// </summary>
    /// <param name="text">
    /// The text to style.
    /// </param>
    /// <param name="isHighlighted">
    /// An optional value indicating whether the text should be highlighted.
    /// </param>
    /// <returns>
    /// The new <see cref="MarkdownStyledText"/> instance with the specified highlight styling.
    /// </returns>
    public static MarkdownStyledText WithHighlight(this string text, bool isHighlighted = true)
    {
        return new MarkdownStyledText(text).WithHighlight(isHighlighted);
    }

    /// <summary>
    /// Applies the desired italic styling to the text.
    /// </summary>
    /// <param name="text">
    /// The text to style.
    /// </param>
    /// <param name="isItalic">
    /// An optional value indicating whether the text should be italic.
    /// </param>
    /// <returns>
    /// The new <see cref="MarkdownStyledText"/> instance with the specified italic styling.
    /// </returns>
    public static MarkdownStyledText WithItalic(this string text, bool isItalic = true)
    {
        return new MarkdownStyledText(text).WithItalic(isItalic);
    }

    /// <summary>
    /// Converts a string to a <see cref="MarkdownLink"/> linked to the specified <paramref name="destination"/>.
    /// </summary>
    /// <param name="text">
    /// The text to display for the link.
    /// </param>
    /// <param name="destination">
    /// The destination URL of the link.
    /// </param>
    /// <returns>
    /// A newly created <see cref="MarkdownLink"/> instance.
    /// </returns>
    public static MarkdownLink WithLinkTo(this string text, string destination)
    {
        return new MarkdownLink(destination, text);
    }

    /// <summary>
    /// Applies the desired strike-through styling to the text.
    /// </summary>
    /// <param name="text">
    /// The text to style.
    /// </param>
    /// <param name="isStrikethrough">
    /// An optional value indicating whether the text should be styled with strike-through.
    /// </param>
    /// <returns>
    /// The new <see cref="MarkdownStyledText"/> instance with the specified strike-through styling.
    /// </returns>
    public static MarkdownStyledText WithStrikethrough(this string text, bool isStrikethrough = true)
    {
        return new MarkdownStyledText(text).WithStrikethrough(isStrikethrough);
    }

    /// <summary>
    /// Creates a <see cref="MarkdownStyledText"/> instance with the specified text.
    /// </summary>
    /// <param name="text">
    /// The text to style.
    /// </param>
    /// <returns>
    /// A <see cref="MarkdownStyledText"/> instance with the specified text.
    /// </returns>
    public static MarkdownStyledText WithStyling(this string text)
    {
        return new MarkdownStyledText(text);
    }

    /// <summary>
    /// Applies the desired subscript styling to the text.
    /// </summary>
    /// <param name="text">
    /// The text to style.
    /// </param>
    /// <param name="isSubscript">
    /// An optional value indicating whether the text should be styled with subscript.
    /// </param>
    /// <returns>
    /// The new <see cref="MarkdownStyledText"/> instance with the specified subscript styling.
    /// </returns>
    public static MarkdownStyledText WithSubscript(this string text, bool isSubscript = true)
    {
        return new MarkdownStyledText(text).WithSubscript(isSubscript);
    }

    /// <summary>
    /// Applies the desired superscript styling to the text.
    /// </summary>
    /// <param name="text">
    /// The text to style.
    /// </param>
    /// <param name="isSuperscript">
    /// An optional value indicating whether the text should be styled with superscript.
    /// </param>
    /// <returns>
    /// The new <see cref="MarkdownStyledText"/> instance with the specified superscript styling.
    /// </returns>
    public static MarkdownStyledText WithSuperscript(this string text, bool isSuperscript = true)
    {
        return new MarkdownStyledText(text).WithSuperscript(isSuperscript);
    }
}