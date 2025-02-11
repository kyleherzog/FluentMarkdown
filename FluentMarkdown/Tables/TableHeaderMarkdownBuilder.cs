using FluentMarkdown.Builders;

namespace FluentMarkdown.Tables;

/// <summary>
/// A markdown builder designed to be used to generate table headers.
/// </summary>
public class TableHeaderMarkdownBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TableHeaderMarkdownBuilder"/> class.
    /// </summary>
    /// <param name="tableBuilder">
    /// The table builder being used to generate the table.
    /// </param>
    internal TableHeaderMarkdownBuilder(TableMarkdownBuilder tableBuilder)
    {
        TableBuilder = tableBuilder;
    }

    internal TableMarkdownBuilder TableBuilder { get; }

    /// <summary>
    /// Adds a column to the table.
    /// </summary>
    /// <param name="content">
    /// The content of the column header.
    /// </param>
    /// <param name="alignment">
    /// The alignment of the column.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="TableHeaderMarkdownBuilder"/> class.
    /// </returns>
    public TableHeaderMarkdownBuilder AddColumn(string content, TableCellAlignment? alignment = null) => AddColumn(b => b.Add(content), alignment);

    /// <summary>
    /// Adds a column to the table.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the builder that will generate the content of the column header.
    /// </param>
    /// <param name="alignment">
    /// The alignment of the column.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="TableHeaderMarkdownBuilder"/> class.
    /// </returns>
    public TableHeaderMarkdownBuilder AddColumn(Action<InlineStyledMarkdownBuilder> addContent, TableCellAlignment? alignment = null)
    {
        TableBuilder.Cells.Add(alignment);

        if (TableBuilder.Builder.Length == 0)
        {
            TableBuilder.Builder.Add(TableBuilder.Prefixes.ToString());
            TableBuilder.Builder.Add("|");
        }

        TableBuilder.Builder.Add(" ");

        var columnBuilder = new InlineStyledMarkdownBuilder();
        addContent(columnBuilder);
        TableBuilder.Builder.Add(columnBuilder.ToString());

        TableBuilder.Builder.Add(" |");

        return this;
    }
}