using FluentMarkdown.Builders;

namespace FluentMarkdown.UnitTests.Builders.InlineStyledMarkdownBuilderTests;

[TestClass]
public class AddTextItalicShould
{
    [TestMethod]
    public void AddItalicTextGivenAction()
    {
        var builder = new InlineStyledMarkdownBuilder();
        var text = "Hello, World!";
        builder.AddTextItalic(b =>
        {
            b.Add(text);
            b.AddLink("https://example.com", "Example");
        });
        Assert.AreEqual($"*{text}[Example](https://example.com)*", builder.ToString());
    }

    [TestMethod]
    public void AddItalicTextGivenString()
    {
        var builder = new InlineStyledMarkdownBuilder();
        var text = "Hello, World!";
        builder.AddTextItalic(text);
        Assert.AreEqual($"*{text}*", builder.ToString());
    }

    [TestMethod]
    public void AddNothingGivenEmptyString()
    {
        var builder = new InlineStyledMarkdownBuilder();
        var text = string.Empty;
        builder.AddTextItalic(text);
        Assert.AreEqual(string.Empty, builder.ToString());
    }

    [TestMethod]
    public void AddNothingGivenNullString()
    {
        var builder = new InlineStyledMarkdownBuilder();
        string text = null!;
        builder.AddTextItalic(text);
        Assert.AreEqual(string.Empty, builder.ToString());
    }

    [TestMethod]
    public void ThrowExceptionGivenNullAction()
    {
        var builder = new InlineStyledMarkdownBuilder();
        Action<EmphasisMarkdownBuilder> action = null!;
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            builder.AddTextItalic(action);
        });
    }
}