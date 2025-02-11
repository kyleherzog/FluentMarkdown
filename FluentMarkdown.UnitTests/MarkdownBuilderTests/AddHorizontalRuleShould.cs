namespace FluentMarkdown.UnitTests.MarkdownBuilderTests;

[TestClass]
public class AddHorizontalRuleShould
{
    [TestMethod]
    public void AddHorizontalRuleGivenFirstAddition()
    {
        var builder = new MarkdownBuilder();
        builder.AddHorizontalRule();
        Assert.AreEqual($"---{Environment.NewLine}", builder.ToString());
    }

    [TestMethod]
    public void AddHorizontalRuleGivenMultipleAdditions()
    {
        var builder = new MarkdownBuilder();
        builder.AddHorizontalRule();
        builder.AddHorizontalRule();
        builder.AddHorizontalRule();
        Assert.AreEqual($"---{Environment.NewLine}{Environment.NewLine}---{Environment.NewLine}{Environment.NewLine}---{Environment.NewLine}", builder.ToString());
    }
}