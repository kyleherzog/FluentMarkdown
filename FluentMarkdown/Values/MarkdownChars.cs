namespace FluentMarkdown;

/// <summary>
/// Named special characters used in Markdown.
/// </summary>
public static class MarkdownChars
{
    /// <summary>
    /// The characters used to denote HTML in Markdown.
    /// </summary>
    public static readonly char[] AngleBrackets = { '<', '>' };

    /// <summary>
    /// The characters used to denote custom header IDs in Markdown.
    /// </summary>
    public static readonly char[] CurlyBrackets = { '{', '}' };

    /// <summary>
    /// The characters used to build links and images in Markdown.
    /// </summary>
    public static readonly char[] Parentheses = { '(', ')' };

    /// <summary>
    /// The characters used to build links and images in Markdown.
    /// </summary>
    public static readonly char[] SquareBrackets = { '[', ']' };
}