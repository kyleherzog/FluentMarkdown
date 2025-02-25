using FluentMarkdown.Builders;

namespace FluentMarkdown.UnitTests.MarkdownBuilderTests;

[TestClass]
public class AddTaskListItemShould : VerifyBase
{
    [TestMethod]
    public Task AddTaskListItemGivenMultipleNestedLevels()
    {
        var builder = new MarkdownBuilder();
        builder.AddUnorderedList(ul =>
        {
            ul.AddTaskListItem("Checked Item", true)
                .AddUnorderedList(ul2 =>
                {
                    ul2.AddTaskListItem("Unchecked Item", false);
                });
        });

        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddTaskListItemGivenNestedInUnorderedList()
    {
        var builder = new MarkdownBuilder();
        builder.AddUnorderedList(ul =>
        {
            ul.AddTaskListItem("Checked Item", true)
                .AddTaskListItem("Unchecked Item", false);
        });

        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddTaskListItemGivenNotNested()
    {
        var builder = new MarkdownBuilder();
        builder.AddTaskListItem("Checked Item", true)
            .AddTaskListItem("Unchecked Item", false);
        return Verify(builder.ToString());
    }

    [TestMethod]
    public void ThrowsArgumentNullExceptionGivenNullAction()
    {
        var builder = new MarkdownBuilder();
        Action<MarkdownBuilder<MarkdownBuilder>> action = null!;
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            builder.AddTaskListItem(action);
        });
    }
}