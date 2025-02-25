namespace FluentMarkdown.UnitTests.MarkdownBuilderTests;

[TestClass]
public class UsingCodeLanguageShould
{
    [TestMethod]
    public void SetTheCodeLanguageGiveValue()
    {
        var builder = new MarkdownBuilder();
        builder.UsingCodeLanguage("csharp");
        Assert.AreEqual("csharp", builder.DefaultCodeLanguage);
    }

    [TestMethod]
    public void SetTheCodeLanguageToEmptyGivenNull()
    {
        var builder = new MarkdownBuilder();
        builder.UsingCodeLanguage(null!);
        Assert.AreEqual(string.Empty, builder.DefaultCodeLanguage);
    }
}