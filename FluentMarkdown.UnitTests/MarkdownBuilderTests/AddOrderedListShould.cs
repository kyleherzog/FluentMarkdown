using FluentMarkdown.Builders;

namespace FluentMarkdown.UnitTests.MarkdownBuilderTests;

[TestClass]
public class AddOrderedListShould : VerifyBase
{
    [TestMethod]
    public Task AddListsGivenTwoLists()
    {
        var builder = new MarkdownBuilder();
        builder.AddOrderedList(b =>
        {
            b.AddOrderedListItem("Item 1");
            b.AddOrderedListItem("Item 2");
        });
        builder.AddOrderedList(b =>
        {
            b.AddOrderedListItem("Item 1");
            b.AddOrderedListItem("Item 2");
        });
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddOrderedListGivenOneLevel()
    {
        var builder = new MarkdownBuilder();
        builder.AddOrderedList(
            "Item 1",
            "Item 2");
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddOrderedListGivenSpecifiedNumbers()
    {
        var builder = new MarkdownBuilder();
        builder.AddOrderedList(b =>
        {
            b.AddOrderedListItem(1, "Item 1");
            b.AddOrderedListItem(2, "Item 2");
        });
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddsContentGivenAdditionalLineItemContentNotSpecifyingNumbers()
    {
        var builder = new MarkdownBuilder();
        builder.AddOrderedList(bl =>
        {
            bl.AddOrderedListItem("Item 1", bi =>
            {
                bi.AddLine("Extra content here.");
            });
            bl.AddOrderedListItem("Item 2");
        });
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddsContentGivenAdditionalLineItemContentSpecifyingNumbers()
    {
        var builder = new MarkdownBuilder();
        builder.AddOrderedList(bl =>
        {
            bl.AddOrderedListItem(1, "Item 1", bi =>
            {
                bi.AddLine("Extra content here.");
            });
            bl.AddOrderedListItem(2, "Item 2");
        });
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddsContentGivenNestedListsWithContent()
    {
        var builder = new MarkdownBuilder();
        builder.AddOrderedList(b =>
        {
            b.AddOrderedListItem("Item 1");
            b.AddOrderedListItem("Item 2");
            b.AddOrderedList(b2 =>
            {
                b2.AddOrderedListItem("Item 1a", b3 =>
                {
                    b3.AddLine("Extra content here.");
                });
                b2.AddOrderedListItem("Item 2a");
            });
        });
        return Verify(builder.ToString());
    }

    [TestMethod]
    public void ThrowArgumentNullExceptionGivenNullAction()
    {
        var builder = new MarkdownBuilder();
        var action = (Action<OrderedListMarkdownBuilder>)null!;
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            builder.AddOrderedList(action);
        });
    }
}