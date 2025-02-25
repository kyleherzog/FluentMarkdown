using FluentMarkdown.Builders;

namespace FluentMarkdown.UnitTests.MarkdownBuilderTests;

[TestClass]
public class AddDefinitionListItemShould : VerifyBase
{
    [TestMethod]
    public Task AddItemGivenMultipleDefinitions()
    {
        var builder = new MarkdownBuilder();
        builder.AddDefinitionListItem("Term 1", "Definition 1", "Definition 2");
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddItemGivenOneDefinition()
    {
        var builder = new MarkdownBuilder();
        builder.AddDefinitionListItem("Term 1", "Definition 1");
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddItemsGivenMultipleItems()
    {
        var builder = new MarkdownBuilder();
        builder.AddDefinitionListItem("Term 1", "Definition 1")
               .AddDefinitionListItem("Term 2", "Definition 2");
        return Verify(builder.ToString());
    }

    [TestMethod]
    public void ThrowArgumentNullExceptionGivenNullAction()
    {
        var builder = new MarkdownBuilder();
        Action<DefinitionListItemBuilder> action = null!;
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            builder.AddDefinitionListItem(action);
        });
    }
}