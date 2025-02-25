using FluentMarkdown.CodeBlocks;

namespace FluentMarkdown.UnitTests.MarkdownBuilderTests;

[TestClass]
public class AddCodeBlockShould : VerifyBase
{
    [TestMethod]
    public Task AddCodeBlockGivenArrayOfCodeLines()
    {
        var builder = new MarkdownBuilder();
        builder.AddCodeBlock(["var x = 1;", "var y = 2;"]);
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddCodeBlockGivenArrayOfCodeLinesWithLanguage()
    {
        var builder = new MarkdownBuilder();
        builder.AddCodeBlock(BlockLanguage.CSharp, "var x = 1;", "var y = 2;");
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddCodeBlockGivenGlobalDefaultSet()
    {
        try
        {
            MarkdownConfiguration.Global.UsingCodeLanguage(BlockLanguage.HTML);
            var builder = new MarkdownBuilder();
            builder.AddCodeBlock("<html></html>");
            return Verify(builder.ToString());
        }
        finally
        {
            // reset the default code language
            MarkdownConfiguration.Global.UsingCodeLanguage(BlockLanguage.None);
        }
    }

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
    public Task AddCodeBlockGivenLanguageEnum()
    {
        var builder = new MarkdownBuilder();
        builder.AddCodeBlock(BlockLanguage.HTML, "<html></html>");
        return Verify(builder.ToString());
    }

    [TestMethod]
    public Task AddCodeBlockGivenLanguageMappedEnum()
    {
        var builder = new MarkdownBuilder();
        builder.AddCodeBlock(BlockLanguage.VisualBasic, "Dim x As Integer = 1");
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
    public Task AddCodeBlockGivenUsingCodeLanguage()
    {
        var builder = new MarkdownBuilder();
        builder.UsingCodeLanguage("csharp")
            .AddCodeBlock(cb =>
            {
                cb.AddLine("var x = 1;");
                cb.AddLine("var y = 2;");
            });
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
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            builder.AddCodeBlock(addContent);
        });
    }
}