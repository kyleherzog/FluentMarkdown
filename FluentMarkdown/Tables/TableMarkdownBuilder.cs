using FluentMarkdown.Builders;
using FluentMarkdown.Internals;

namespace FluentMarkdown.Tables;

/// <summary>
/// A markdown builder for creating tables.
/// </summary>
public class TableMarkdownBuilder : NestableElementBuilder
{
    private bool hasHeader;

    internal TableMarkdownBuilder(LinePrefixStack prefixes)
        : base(prefixes)
    {
    }

    internal IList<TableCellAlignment?> Cells { get; } = new List<TableCellAlignment?>();

    /// <summary>
    /// Configures the table body.
    /// </summary>
    /// <param name="bodyContent">
    /// The content to add to the table body.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="TableMarkdownBuilder"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified body content is <see langword="null"/>.
    /// </exception>
    public TableMarkdownBuilder WithBody(IEnumerable<IEnumerable<string>> bodyContent)
    {
        if (bodyContent is null)
        {
            throw new ArgumentNullException(nameof(bodyContent));
        }

        return WithBody(tb =>
        {
            foreach (var row in bodyContent)
            {
                tb.AddRow(row.ToArray());
            }
        });
    }

    /// <summary>
    /// Configures the table body.
    /// </summary>
    /// <param name="buildBody">
    /// The action to apply to the builder that will generate the content inside the table body.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="TableMarkdownBuilder"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified action is <see langword="null"/>.
    /// </exception>
    public TableMarkdownBuilder WithBody(Action<TableBodyMarkdownBuilder> buildBody)
    {
        if (buildBody is null)
        {
            throw new ArgumentNullException(nameof(buildBody));
        }

        var body = new TableBodyMarkdownBuilder(this);
        buildBody(body);
        return this;
    }

    /// <summary>
    /// Configures the table header.
    /// </summary>
    /// <param name="values">
    /// The values to add to the table header.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="TableMarkdownBuilder"/> class.
    /// </returns>
    public TableMarkdownBuilder WithHeader(params string[] values)
    {
        return WithHeader(thb =>
        {
            foreach (var value in values)
            {
                thb.AddColumn(value);
            }
        });
    }

    /// <summary>
    /// Configures the table header.
    /// </summary>
    /// <param name="buildHeader">
    /// The action to apply to the builder that will generate the content inside the table header.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="TableMarkdownBuilder"/> class.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the table header has already been defined.
    /// </exception>
    public TableMarkdownBuilder WithHeader(Action<TableHeaderMarkdownBuilder> buildHeader)
    {
        if (hasHeader)
        {
            throw new InvalidOperationException("Table header has already been defined.");
        }

        hasHeader = true;
        var header = new TableHeaderMarkdownBuilder(this);
        buildHeader(header);
        Builder.Add(Environment.NewLine);
        Builder.Add(Prefixes.ToString());
        Builder.Add("|");
        for (var i = 0; i < Cells.Count; i++)
        {
            if (Cells[i] <= TableCellAlignment.Center)
            {
                Builder.Add(":");
            }

            Builder.Add("---");

            if (Cells[i] >= TableCellAlignment.Center)
            {
                Builder.Add(":");
            }

            Builder.Add("|");
        }

        Builder.Add(Environment.NewLine);
        return this;
    }
}