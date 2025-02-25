namespace FluentMarkdown.UnitTests.MarkdownBuilderTests;

[TestClass]
public class AddTableShould : VerifyBase
{
    [TestMethod]
    public Task AddTableGivenActions()
    {
        var builder = new MarkdownBuilder();
        builder.AddTable(tb =>
            tb.WithHeader(thb =>
                thb.AddColumn("Default Aligned")
                    .AddColumn(tcb => tcb.AddCode("Left Aligned"), TableCellAlignment.Left)
                    .AddColumn("Center Aligned", TableCellAlignment.Center)
                    .AddColumn("Right Aligned", TableCellAlignment.Right))
            .WithBody(tbb =>
                tbb.AddRow("R1C1", "R1C2", "R1C3", "R1C4")
                    .AddRow(trb => trb.AddCell("R2C1").AddCell(tcb => tcb.AddCode("R2C2")).AddCell("R2C3").AddCell("R2C4"))
                    .AddRow("R3C1", "R3C2", "R3C3", "R3C4")));
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddTableGivenStringValues()
    {
        var builder = new MarkdownBuilder();

        string[][] content =
        [
            ["R1C1", "R1C2", "R1C3"],
            ["R2C1", "R2C2", "R2C3"],
            ["R3C1", "R3C2", "R3C3"],
        ];
        builder.AddTable(tb => tb.WithHeader("Column 1", "Column 2", "Column 3")
                                    .WithBody(content));
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddTableIndentedGivenListItem()
    {
        string[][] tableContent =
        [
            ["R1C1", "R1C2"],
            ["R2C1", "R2C2"],
            ["R3C1", "R3C2"],
        ];
        var builder = new MarkdownBuilder();
        builder.AddUnorderedList(b => b.AddUnorderedListItem(bi => bi.Add("Item 1")
                                                                        .AddTable(tb => tb.WithHeader("Col1", "Col2")
                                                                                            .WithBody(tableContent))));
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddTableOnNewLineGivenLineInProgress()
    {
        var builder = new MarkdownBuilder();
        builder.Add("Starting content.")
            .AddTable(tb => tb.WithHeader("Column 1", "Column 2")
                                                .WithBody(tbb => tbb.AddRow("R1C1", "R1C2")));
        return Verify(builder.ToString());
    }
}