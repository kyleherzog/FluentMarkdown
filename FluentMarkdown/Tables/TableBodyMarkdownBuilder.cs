namespace FluentMarkdown.Tables;

/// <summary>
/// A markdown builder designed to be used to generate table bodies.
/// </summary>
public class TableBodyMarkdownBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TableBodyMarkdownBuilder"/> class.
    /// </summary>
    /// <param name="tableBuilder">
    /// The table builder being used to generate the table.
    /// </param>
    internal TableBodyMarkdownBuilder(TableMarkdownBuilder tableBuilder)
    {
        TableBuilder = tableBuilder;
    }

    internal TableMarkdownBuilder TableBuilder { get; }

    /// <summary>
    /// Adds a row to the table.
    /// </summary>
    /// <param name="cells">
    /// The content of the cells in the row.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="TableBodyMarkdownBuilder"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified cells are <see langword="null"/>.
    /// </exception>
    public TableBodyMarkdownBuilder AddRow(params string[] cells)
    {
        if (cells is null)
        {
            throw new ArgumentNullException(nameof(cells));
        }

        return AddRow(b =>
        {
            foreach (var cell in cells)
            {
                b.AddCell(cell);
            }
        });
    }

    /// <summary>
    /// Adds a row to the table.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the builder that will generate the content of the row.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="TableBodyMarkdownBuilder"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified action is <see langword="null"/>.
    /// </exception>
    public TableBodyMarkdownBuilder AddRow(Action<TableRowMarkdownBuilder> addContent)
    {
        if (addContent is null)
        {
            throw new ArgumentNullException(nameof(addContent));
        }

        TableBuilder.Builder.Add(TableBuilder.Prefixes.ToString());
        var rowBuilder = new TableRowMarkdownBuilder(TableBuilder);
        addContent(rowBuilder);
        TableBuilder.Builder.Add(Environment.NewLine);
        return this;
    }
}