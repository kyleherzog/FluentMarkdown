using FluentMarkdown.CodeBlocks;
using FluentMarkdown.Tables;

namespace FluentMarkdown.Builders;

/// <summary>
/// A full featured markdown builder.
/// </summary>
/// <typeparam name="T">
/// The type that will be returned by builder methods.
/// </typeparam>
public class MarkdownBuilder<T> : ParagraphMarkdownBuilder<T>
    where T : MarkdownBuilder<T>
{
    private string? defaultCodeLanguage = null;

    /// <summary>
    /// Gets the default code language to use for code blocks.
    /// </summary>
    public string DefaultCodeLanguage
    {
        get
        {
            if (defaultCodeLanguage is null)
            {
                return MarkdownConfiguration.Global.DefaultCodeLanguage;
            }

            return defaultCodeLanguage;
        }
    }

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
    public T AddBlockQuote(params string[] lines)
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
    /// Adds a block quotation to the content.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the builder that will generate the content inside the block quotation.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public T AddBlockQuote(Action<MarkdownBuilder<T>> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        Prefixes.PushBlockquote();
        addContent(this);
        Prefixes.Pop();
        return (T)this;
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
    public T AddCodeBlock(string code) => AddCodeBlock(b => b.Add(code));

    /// <summary>
    /// Adds a code block to the content.
    /// </summary>
    /// <param name="code">
    /// The lines of code to add to the code block.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public T AddCodeBlock(string[] code) => AddCodeBlock(DefaultCodeLanguage, code);

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
    public T AddCodeBlock(BlockLanguage language, string code) => AddCodeBlock(language.ToIdentifier(), code);

    /// <summary>
    /// Adds a code block to the content.
    /// </summary>
    /// <param name="language">
    /// The language of the code in the code block.
    /// </param>
    /// <param name="code">
    /// The lines of code to add to the code block.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public T AddCodeBlock(BlockLanguage language, params string[] code) => AddCodeBlock(language.ToIdentifier(), code);

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
    public T AddCodeBlock(string language, string code) => AddCodeBlock(language, b => b.Add(code));

    /// <summary>
    /// Adds a code block to the content.
    /// </summary>
    /// <param name="language">
    /// The language of the code in the code block.
    /// </param>
    /// <param name="code">
    /// The lines of code to add to the code block.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public T AddCodeBlock(string language, params string[] code)
    {
        return AddCodeBlock(language, b =>
        {
            foreach (var line in code)
            {
                b.AddLine(line);
            }
        });
    }

    /// <summary>
    /// Adds a code block to the content.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the builder that will generate the content for inside the code block.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public T AddCodeBlock(Action<CodeBlockMarkdownBuilder> addContent) => AddCodeBlock(DefaultCodeLanguage, addContent);

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
    public T AddCodeBlock(BlockLanguage language, Action<CodeBlockMarkdownBuilder> addContent) => AddCodeBlock(language.ToIdentifier(), addContent);

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
    public T AddCodeBlock(string language, Action<CodeBlockMarkdownBuilder> addContent)
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
    /// Adds a definition list item to the content.
    /// </summary>
    /// <param name="term">
    /// The term being defined.
    /// </param>
    /// <param name="definitions">
    /// The definitions of the term.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public T AddDefinitionListItem(string term, params string[] definitions)
    {
        return AddDefinitionListItem(b =>
        {
            b.WithTerm(term);
            foreach (var definition in definitions)
            {
                b.AddDefinition(definition);
            }
        });
    }

    /// <summary>
    /// Adds a definition list item to the content.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the builder that will generate the content inside the definition list item.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified action is <see langword="null"/>.
    /// </exception>
    public T AddDefinitionListItem(Action<DefinitionListItemBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        if (IsBuildingLine)
        {
            CompleteLine();
        }

        var builder = new DefinitionListItemBuilder(Prefixes);
        addContent(builder);
        Add(builder.ToString());
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
    /// <param name="id">
    /// The custom id to assign to the header.
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
    public T AddHeader(int level, Action<InlineStyledMarkdownBuilder> addContent, string? id = null)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        if (level < 1 || level > 6)
        {
            throw new ArgumentOutOfRangeException(nameof(level), "Level must be between 1 and 6");
        }

        return AddParagraph(b =>
        {
            Add(new string('#', level));
            Add(" ");
            var headerBuilder = new InlineStyledMarkdownBuilder();
            addContent(headerBuilder);
            if (!string.IsNullOrEmpty(id))
            {
                headerBuilder.Add($" {{#{id}}}");
            }

            b.Add(headerBuilder.ToString());
        });
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
    /// <param name="id">
    /// The custom id to assign to the header.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public T AddHeader(int level, string text, string? id = null) => string.IsNullOrEmpty(text) ? (T)this : AddHeader(level, x => x.Add(text), id);

    /// <summary>
    /// Adds a horizontal rule to the content.
    /// </summary>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public T AddHorizontalRule() => AddParagraph("---");

    /// <summary>
    /// Adds an ordered list to the content.
    /// </summary>
    /// <param name="items">
    /// The items to add to the ordered list.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public T AddOrderedList(params string[] items)
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
    /// Adds an ordered list to the content.
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
    public T AddOrderedList(Action<OrderedListMarkdownBuilder> addContent)
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

        return CompleteParagraph();
    }

    /// <summary>
    /// Adds an ordered list item to the content.
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
    public T AddOrderedListItem(int number, string text) => AddOrderedListItem(number, b => Add(text));

    /// <summary>
    /// Adds an ordered list item to the content.
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
    public T AddOrderedListItem(int number, Action<MarkdownBuilder<T>> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        return AddListItem($"{number}.", addContent);
    }

    /// <summary>
    /// Adds an ordered list item to the content.
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
    public T AddOrderedListItem(int number, string text, Action<MarkdownBuilder<T>> addContent)
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
    public T AddParagraph(string text) => AddParagraph(b => b.Add(text));

    /// <summary>
    /// Adds a paragraph to the content.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the content that will be inside the paragraph.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public T AddParagraph(Action<ParagraphMarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        StartParagraph();

        var paragraphBuilder = new ParagraphMarkdownBuilder();
        addContent(paragraphBuilder);
        Add(paragraphBuilder.ToString());
        return CompleteParagraph();
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
    public T AddTable(IEnumerable<IEnumerable<string>> data)
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
    public T AddTable(Action<TableMarkdownBuilder> addContent)
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
        return Add(builder.ToString());
    }

    /// <summary>
    /// Adds a task list item to the content.
    /// </summary>
    /// <param name="text">
    /// The text to add as a task list item.
    /// </param>
    /// <param name="isChecked">
    /// An optional value indicating whether the task list item is checked.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public T AddTaskListItem(string text, bool isChecked = false) => AddTaskListItem(b => b.Add(text), isChecked);

    /// <summary>
    /// Adds a task list item to the content.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the builder that will generate the content inside the task list item.
    /// </param>
    /// <param name="isChecked">
    /// An optional value indicating whether the task list item is checked.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified action is <see langword="null"/>.
    /// </exception>
    public T AddTaskListItem(Action<MarkdownBuilder<T>> addContent, bool isChecked = false)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        var bullet = isChecked ? "- [x]" : "- [ ]";
        return AddListItem(bullet, addContent);
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
    public T AddUnorderedList(params string[] items)
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
    /// Adds an unordered list to the content.
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
    public T AddUnorderedList(Action<MarkdownBuilder<T>> addContent)
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

        return CompleteParagraph();
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
    public T AddUnorderedListItem(string text) => AddUnorderedListItem(b => Add(text));

    /// <summary>
    /// Adds an unordered list item to the content.
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
    public T AddUnorderedListItem(string text, Action<MarkdownBuilder<T>> addContent)
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
    /// Adds an unordered list item to the content.
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
    public T AddUnorderedListItem(Action<MarkdownBuilder<T>> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        return AddListItem("-", addContent);
    }

    /// <summary>
    /// Sets the default code language to use for code blocks.
    /// </summary>
    /// <param name="language">
    /// The default code language to use for code blocks.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public T UsingCodeLanguage(BlockLanguage language) => UsingCodeLanguage(language.ToIdentifier());

    /// <summary>
    /// Sets the default code language to use for code blocks.
    /// </summary>
    /// <param name="language">
    /// The default code language to use for code blocks.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="MarkdownBuilder"/> class.
    /// </returns>
    public T UsingCodeLanguage(string? language)
    {
        defaultCodeLanguage = language;
        return (T)this;
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

    private T AddListItem(string bullet, Action<MarkdownBuilder<T>> addContent)
    {
        StartNewLine();
        Add($"{bullet} ");
        Prefixes.PushIndent();
        addContent(this);
        Prefixes.Pop();
        return CompleteLine();
    }

    private T CompleteParagraph()
    {
        if (IsBuildingLine)
        {
            CompleteLine();
        }

        return (T)this;
    }
}