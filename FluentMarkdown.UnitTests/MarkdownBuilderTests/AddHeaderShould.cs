using FluentMarkdown.Builders;

namespace FluentMarkdown.UnitTests.MarkdownBuilderTests;

[TestClass]
public class AddHeaderShould
{
    [TestMethod]
    public void AddHeaderGivenAction()
    {
        var builder = new MarkdownBuilder();
        var text = "Hello, World!";
        builder.AddHeader(1, b =>
        {
            b.Add(text)
                .AddTextBold(text);
        });
        Assert.AreEqual($"# {text}**{text}**{Environment.NewLine}", builder.ToString());
    }

    [DataTestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(3)]
    [DataRow(4)]
    [DataRow(5)]
    [DataRow(6)]
    public void AddHeaderGivenValidLevel(int level)
    {
        var builder = new MarkdownBuilder();
        var text = "Hello, World!";
        builder.AddHeader(level, text);
        var prefix = new string('#', level);
        Assert.AreEqual($"{prefix} {text}{Environment.NewLine}", builder.ToString());
    }

    [TestMethod]
    public void StartNewParagraphGivenNotAtLineStart()
    {
        var builder = new MarkdownBuilder();
        var text = "Hello, World!";
        builder.Add(text);
        builder.AddHeader(1, text);
        Assert.AreEqual($"{text}{Environment.NewLine}{Environment.NewLine}# {text}{Environment.NewLine}", builder.ToString());
    }

    [TestMethod]
    public void AddHeaderIdGivenId()
    {
        var builder = new MarkdownBuilder();
        var text = "Hello, World!";
        var id = "test";
        builder.AddHeader(1, text, id);
        Assert.AreEqual($"# {text} {{#{id}}}{Environment.NewLine}", builder.ToString());
    }

    [DataTestMethod]
    [DataRow(0)]
    [DataRow(7)]
    [DataRow(-99)]
    [DataRow(int.MaxValue)]
    [DataRow(int.MinValue)]
    public void ThrowExceptionGivenInvalidLevel(int level)
    {
        var builder = new MarkdownBuilder();
        var text = "Hello, World!";
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
        {
            builder.AddHeader(level, text);
        });
    }

    [TestMethod]
    public void ThrowExceptionGivenNullAction()
    {
        var builder = new MarkdownBuilder();
        Action<InlineStyledMarkdownBuilder> action = null!;
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            builder.AddHeader(1, action);
        });
    }
}