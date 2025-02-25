using FluentMarkdown.Builders;

namespace FluentMarkdown.Tables;

/// <summary>
/// A markdown builder for creating table rows.
/// </summary>
public class TableRowMarkdownBuilder
{
    private int cellCount;

    internal TableRowMarkdownBuilder(TableMarkdownBuilder tableBuilder)
    {
        TableBuilder = tableBuilder;
    }

    internal TableMarkdownBuilder TableBuilder { get; }

    /// <summary>
    /// Adds a cell to the row.
    /// </summary>
    /// <param name="content">
    /// The content of the cell.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="TableRowMarkdownBuilder"/> class.
    /// </returns>
    public TableRowMarkdownBuilder AddCell(string content) => AddCell(b => b.Add(content));

    /// <summary>
    /// Adds a cell to the row.
    /// </summary>
    /// <param name="addContent">
    /// The action to apply to the builder that will generate the content of the cell.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="TableRowMarkdownBuilder"/> class.
    /// </returns>
    public TableRowMarkdownBuilder AddCell(Action<InlineStyledMarkdownBuilder> addContent)
    {
        if (cellCount == 0)
        {
            TableBuilder.Builder.Add("|");
        }

        TableBuilder.Builder.Add(" ");
        var builder = new InlineStyledMarkdownBuilder();
        addContent(builder);
        TableBuilder.Builder.Add(builder.ToString());
        TableBuilder.Builder.Add(" |");
        cellCount++;
        return this;
    }
}