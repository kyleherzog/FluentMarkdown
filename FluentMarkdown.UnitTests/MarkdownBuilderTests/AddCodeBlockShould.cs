using FluentMarkdown.Builders;

namespace FluentMarkdown.UnitTests.MarkdownBuilderTests;

[TestClass]
public class AddCodeBlockShould : VerifyBase
{
    [TestMethod]
    public Task AddCodeBlockGivenLanguageAndAction()
    {
        var builder = new MarkdownBuilder();
        builder.AddCodeBlock("csharp", cb =>
        {
            cb.AddLine("var x = 1;");
            cb.AddLine("var y = 2;");
        });
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddCodeBlockGivenLanguageMultipleLines()
    {
        var builder = new MarkdownBuilder();
        builder.AddCodeBlock("csharp", $"var x = 1;{Environment.NewLine}var y = 2;");
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddCodeBlockGivenLanguageOneLine()
    {
        var builder = new MarkdownBuilder();
        builder.AddCodeBlock("csharp", "var x = 1;");
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddCodeBlockGivenNoLanguageAndAction()
    {
        var builder = new MarkdownBuilder();
        builder.AddCodeBlock(cb =>
        {
            cb.AddLine("var x = 1;");
            cb.AddLine("var y = 2;");
        });
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddCodeBlockGivenNoLanguageMultipleLines()
    {
        var builder = new MarkdownBuilder();
        builder.AddCodeBlock($"var x = 1;{Environment.NewLine}var y = 2;");
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddCodeBlockGivenNoLanguageOneLine()
    {
        var builder = new MarkdownBuilder();
        builder.AddCodeBlock("var x = 1;");
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task StartNewLineGivenLineInProgress()
    {
        var builder = new MarkdownBuilder();
        builder.Add("Some text");
        builder.AddCodeBlock("var x = 1;");
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task StartTextAfterOnNewLineGivenBlockAdded()
    {
        var builder = new MarkdownBuilder();
        builder.AddCodeBlock("var x = 1;");
        builder.Add("Some text");
        return Verify(builder.ToString());
    }

    [TestMethod]
    public void ThrowArgumentNullExceptionGivenNullAction()
    {
        var builder = new MarkdownBuilder();
        Action<CodeBlockMarkdownBuilder> addContent = null!;
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            builder.AddCodeBlock(addContent);
        });
    }
}