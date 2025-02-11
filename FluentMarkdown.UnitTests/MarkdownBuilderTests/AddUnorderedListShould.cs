namespace FluentMarkdown.UnitTests.MarkdownBuilderTests;

[TestClass]
public class AddUnorderedListShould : VerifyBase
{
    [TestMethod]
    public Task AddListsGivenTwoLists()
    {
        var builder = new MarkdownBuilder();
        builder.AddUnorderedList(b =>
        {
            b.AddUnorderedListItem("Item 1");
            b.AddUnorderedListItem("Item 2");
        });
        builder.AddUnorderedList(b =>
        {
            b.AddUnorderedListItem("Item 1");
            b.AddUnorderedListItem("Item 2");
        });
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddsContentGivenAdditionalLineItemContent()
    {
        var builder = new MarkdownBuilder();
        builder.AddUnorderedList(bl =>
        {
            bl.AddUnorderedListItem("Item 1", bi =>
            {
                bi.AddLine("Extra content here.");
            });
            bl.AddUnorderedListItem("Item 2");
        });
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddsContentGivenNestedListsWithContent()
    {
        var builder = new MarkdownBuilder();
        builder.AddUnorderedList(b =>
        {
            b.AddUnorderedListItem("Item 1");
            b.AddUnorderedListItem("Item 2");
            b.AddUnorderedList(b2 =>
            {
                b2.AddUnorderedListItem(b3 =>
                {
                    b3.Add("Item 1a");
                    b3.AddBlockQuote("Extra content here.");
                });
                b2.AddUnorderedListItem("Item 2a");
            });
        });
        return Verify(builder.ToString());
    }

    [TestMethod]
    public void AddUnorderedListGivenOneLevel()
    {
        var builder = new MarkdownBuilder();
        builder.AddUnorderedList(
            "Item 1",
            "Item 2",
            "Item 3");
        Assert.AreEqual($"- Item 1{Environment.NewLine}- Item 2{Environment.NewLine}- Item 3{Environment.NewLine}", builder.ToString());
    }

    [TestMethod]
    public Task AddUnorderedListGivenTwoLevels()
    {
        var builder = new MarkdownBuilder();
        builder.AddUnorderedList(b =>
        {
            b.AddUnorderedListItem("Item 1");
            b.AddUnorderedListItem("Item 2");
            b.AddUnorderedList(b2 =>
            {
                b2.AddUnorderedListItem("Item 1a");
                b2.AddUnorderedListItem("Item 2a");
            });
        });

        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task StartsNewParagraphGivenLineInProgress()
    {
        var builder = new MarkdownBuilder();
        builder.Add("Line 1");
        builder.AddUnorderedList(b =>
        {
            b.AddUnorderedListItem("Item 1");
            b.AddUnorderedListItem("Item 2");
        });
        return Verify(builder.ToString());
    }

    [TestMethod]
    public void ThrowsExceptionGivenNullAction()
    {
        var builder = new MarkdownBuilder();
        Action<MarkdownBuilder> action = null!;
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            builder.AddUnorderedList(action);
        });
    }
}