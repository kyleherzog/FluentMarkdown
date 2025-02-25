namespace FluentMarkdown.UnitTests.MarkdownBuilderTests;

[TestClass]
public class AddParagraphShould : VerifyBase
{
    [TestMethod]
    public void AddParagraphGivenText()
    {
        var builder = new MarkdownBuilder();
        builder.AddParagraph("Hello, World!");
        Assert.AreEqual($"Hello, World!{Environment.NewLine}", builder.ToString());
    }

    [TestMethod]
    public Task AddParagraphGivenMultipleCalls()
    {
        var builder = new MarkdownBuilder();
        builder.AddParagraph("Hello, ");
        builder.AddParagraph("World!");
        return Verify(builder.ToString());
    }
}