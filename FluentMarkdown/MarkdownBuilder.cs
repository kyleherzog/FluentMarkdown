using FluentMarkdown.Builders;
using FluentMarkdown.Tables;

namespace FluentMarkdown;

/// <summary>
/// A class for building Markdown content in a fluent manner.
/// </summary>
public class MarkdownBuilder : ParagraphMarkdownBuilder<MarkdownBuilder>
{
    internal int ListLevel { get; set; }

    /// <summary>
    /// Adds a block quotation to the content.
    /// </summary>
    /// <param name="lines">
    /// The lines of to add inside of the block quotation.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public MarkdownBuilder AddBlockQuote(params string[] lines)
    {
        return AddBlockQuote(b =>
        {
            foreach (var line in lines)
            {
                b.AddLine(line);
            }
        });
    }

    /// <summary>
    /// Adds a block quotation to the content using the specified action.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the builder that will generate the content inside the block quotation.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public MarkdownBuilder AddBlockQuote(Action<MarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        Prefixes.PushBlockquote();
        addContent(this);
        Prefixes.Pop();
        return this;
    }

    /// <summary>
    /// Adds a code block to the content.
    /// </summary>
    /// <param name="code">
    /// The code to add to the code block.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public MarkdownBuilder AddCodeBlock(string code) => AddCodeBlock(b => b.Add(code));

    /// <summary>
    /// Adds a code block to the content.
    /// </summary>
    /// <param name="language">
    /// The language of the code in the code block.
    /// </param>
    /// <param name="code">
    /// The code to add to the code block.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public MarkdownBuilder AddCodeBlock(string language, string code) => AddCodeBlock(language, b => b.Add(code));

    /// <summary>
    /// Adds a code block to the content.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the builder that will generate the content for inside the code block.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public MarkdownBuilder AddCodeBlock(Action<CodeBlockMarkdownBuilder> addContent) => AddCodeBlock(string.Empty, addContent);

    /// <summary>
    /// Adds a code block to the content.
    /// </summary>
    /// <param name="language">
    /// The language of the code in the code block.
    /// </param>
    /// <param name="addContent">
    /// The action to apply to the builder that will generate the content for inside the code block.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified action is <see langword="null"/>.
    /// </exception>
    public MarkdownBuilder AddCodeBlock(string language, Action<CodeBlockMarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        CompleteParagraph();
        Add("```");
        Add(language);
        CompleteLine();
        var builder = new CodeBlockMarkdownBuilder();
        addContent(builder);
        Add(builder.ToString());
        CompleteParagraph();
        Add("```");
        return CompleteLine();
    }

    /// <summary>
    /// Adds a header to the content.
    /// </summary>
    /// <param name="level">
    /// The level of the header to add.
    /// </param>
    /// <param name="addContent">
    /// The action to apply to the content that will be inside the header.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified action is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the specified level is not between 1 and 6.
    /// </exception>
    public MarkdownBuilder AddHeader(int level, Action<InlineStyledMarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        if (level < 1 || level > 6)
        {
            throw new ArgumentOutOfRangeException(nameof(level), "Level must be between 1 and 6");
        }

        AddParagraph(b =>
        {
            Add(new string('#', level));
            Add(" ");
            var headerBuilder = new InlineStyledMarkdownBuilder();
            addContent(headerBuilder);
            b.Add(headerBuilder.ToString());
        });

        return this;
    }

    /// <summary>
    /// Adds a header to the content.
    /// </summary>
    /// <param name="level">
    /// The level of the header to add.
    /// </param>
    /// <param name="text">
    /// The text to add to the header.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public MarkdownBuilder AddHeader(int level, string text) => string.IsNullOrEmpty(text) ? this : AddHeader(level, x => x.Add(text));

    /// <summary>
    /// Adds a horizontal rule to the content.
    /// </summary>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public MarkdownBuilder AddHorizontalRule() => AddParagraph("---");

    /// <summary>
    /// Adds an ordered list to the content.
    /// </summary>
    /// <param name="items">
    /// The items to add to the ordered list.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public MarkdownBuilder AddOrderedList(params string[] items)
    {
        return AddOrderedList(b =>
        {
            for (var i = 0; i < items.Length; i++)
            {
                AddOrderedListItem(i + 1, items[i]);
            }
        });
    }

    /// <summary>
    /// Adds an ordered list to the content using the specified action to fill the items in the list.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the builder that will generate the content inside the ordered list.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified action is <see langword="null"/>.
    /// </exception>
    public MarkdownBuilder AddOrderedList(Action<OrderedListMarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        ListLevel++;
        if (ListLevel > 1)
        {
            Prefixes.PushIndent();
        }
        else
        {
            StartParagraph();
        }

        var builder = new OrderedListMarkdownBuilder(ListLevel, Prefixes);
        addContent(builder);
        Add(builder.ToString());

        ListLevel--;
        if (ListLevel > 1)
        {
            Prefixes.Pop();
        }

        CompleteParagraph();
        return this;
    }

    /// <summary>
    /// Adds an ordered list item to the content using the specified number.
    /// </summary>
    /// <param name="number">
    /// The number to use for the ordered list item.
    /// </param>
    /// <param name="text">
    /// The text to add as an ordered list item.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public MarkdownBuilder AddOrderedListItem(int number, string text) => AddOrderedListItem(number, b => Add(text));

    /// <summary>
    /// Adds an ordered list item to the content using the specified number.
    /// </summary>
    /// <param name="number">
    /// The number to use for the ordered list item.
    /// </param>
    /// <param name="addContent">
    /// The action to apply to the builder that will generate the content inside the ordered list item.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified action is <see langword="null"/>.
    /// </exception>
    public MarkdownBuilder AddOrderedListItem(int number, Action<MarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        return AddListItem($"{number}.", addContent);
    }

    /// <summary>
    /// Adds an ordered list item to the content using the specified number.
    /// </summary>
    /// <param name="number">
    /// The number to use for the ordered list item.
    /// </param>
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
    public MarkdownBuilder AddOrderedListItem(int number, string text, Action<MarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        return AddOrderedListItem(number, b =>
        {
            b.Add(text);
            StartParagraph();
            addContent(b);
        });
    }

    /// <summary>
    /// Adds a paragraph to the content.
    /// </summary>
    /// <param name="text">
    /// The text to add as the paragraph.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public MarkdownBuilder AddParagraph(string text) => AddParagraph(b => b.Add(text));

    /// <summary>
    /// Adds a paragraph to the content using the specified action.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the content that will be inside the paragraph.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public MarkdownBuilder AddParagraph(Action<ParagraphMarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        StartParagraph();

        var paragraphBuilder = new ParagraphMarkdownBuilder();
        addContent(paragraphBuilder);
        Add(paragraphBuilder.ToString());
        CompleteParagraph();
        return this;
    }

    /// <summary>
    /// Adds a table to the content.
    /// </summary>
    /// <param name="data">
    /// The data to add to the table. The first row is the header row and the remaining rows are the body rows.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public MarkdownBuilder AddTable(IEnumerable<IEnumerable<string>> data)
    {
        var headerData = data.First();
        var bodyData = data.Skip(1);
        return AddTable(b =>
           b.WithHeader(headerData.ToArray())
            .WithBody(bodyData));
    }

    /// <summary>
    /// Adds a table to the content.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the builder that will generate the content inside the table.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified action is <see langword="null"/>.
    /// </exception>
    public MarkdownBuilder AddTable(Action<TableMarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        if (IsBuildingLine)
        {
            CompleteLine();
        }

        var builder = new TableMarkdownBuilder(Prefixes);
        addContent(builder);
        Add(builder.ToString());
        return this;
    }

    /// <summary>
    /// Adds an unordered list to the content.
    /// </summary>
    /// <param name="items">
    /// Add the items to the unordered list.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public MarkdownBuilder AddUnorderedList(params string[] items)
    {
        return AddUnorderedList(b =>
        {
            foreach (var item in items)
            {
                AddUnorderedListItem(item);
            }
        });
    }

    /// <summary>
    /// Adds an unordered list to the content using the specified action to fill the items in the list.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the builder that will generate the content inside the unordered list.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified action is <see langword="null"/>.
    /// </exception>
    public MarkdownBuilder AddUnorderedList(Action<MarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        ListLevel++;
        if (ListLevel > 1)
        {
            Prefixes.PushIndent();
        }
        else
        {
            StartParagraph();
        }

        addContent(this);

        ListLevel--;
        if (ListLevel > 1)
        {
            Prefixes.Pop();
        }

        CompleteParagraph();
        return this;
    }

    /// <summary>
    /// Adds an unordered list item to the content.
    /// </summary>
    /// <param name="text">
    /// The text to add as an unordered list item.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public MarkdownBuilder AddUnorderedListItem(string text) => AddUnorderedListItem(b => Add(text));

    /// <summary>
    /// Adds an unordered list item to the content using the text and the specified action.
    /// </summary>
    /// <param name="text">
    /// The text to add as an unordered list item.
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
    public MarkdownBuilder AddUnorderedListItem(string text, Action<MarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        return AddUnorderedListItem(b =>
        {
            b.Add(text);
            StartParagraph();
            addContent(b);
        });
    }

    /// <summary>
    /// Adds an unordered list item to the content using the specified action.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the builder that will populate the unordered list item.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified action is <see langword="null"/>.
    /// </exception>
    public MarkdownBuilder AddUnorderedListItem(Action<MarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        return AddListItem("-", addContent);
    }

    /// <summary>
    /// Starts a new paragraph if one is not already in progress.
    /// </summary>
    protected void StartParagraph()
    {
        if (IsBuildingLine)
        {
            CompleteLine();
        }

        if (Length > 0)
        {
            CompleteLine();
        }
    }

    private MarkdownBuilder AddListItem(string bullet, Action<MarkdownBuilder> addContent)
    {
        StartNewLine();
        Add($"{bullet} ");
        Prefixes.PushIndent();
        addContent(this);
        Prefixes.Pop();
        CompleteLine();
        return this;
    }

    private void CompleteParagraph()
    {
        if (IsBuildingLine)
        {
            CompleteLine();
        }
    }
}