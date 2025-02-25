using FluentMarkdown.Builders;

namespace FluentMarkdown.UnitTests.Builders.InlineStyledMarkdownBuilderTests;

[TestClass]
public class AddTextBoldShould
{
    [TestMethod]
    public void AddBoldTextGivenAction()
    {
        var builder = new InlineStyledMarkdownBuilder();
        var text = "Hello, World!";
        builder.AddTextBold(b =>
        {
            b.Add(text);
            b.AddLink("https://example.com", "Example");
        });
        Assert.AreEqual($"**{text}[Example](https://example.com)**", builder.ToString());
    }

    [TestMethod]
    public void AddBoldTextGivenString()
    {
        var builder = new InlineStyledMarkdownBuilder();
        var text = "Hello, World!";
        builder.AddTextBold(text);
        Assert.AreEqual($"**{text}**", builder.ToString());
    }

    [TestMethod]
    public void AddNothingGivenEmptyString()
    {
        var builder = new InlineStyledMarkdownBuilder();
        var text = string.Empty;
        builder.AddTextBold(text);
        Assert.AreEqual(string.Empty, builder.ToString());
    }

    [TestMethod]
    public void AddNothingGivenNullString()
    {
        var builder = new InlineStyledMarkdownBuilder();
        string text = null!;
        builder.AddTextBold(text);
        Assert.AreEqual(string.Empty, builder.ToString());
    }

    [TestMethod]
    public void ThrowExceptionGivenNullAction()
    {
        var builder = new InlineStyledMarkdownBuilder();
        Action<EmphasisMarkdownBuilder> action = null!;
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            builder.AddTextBold(action);
        });
    }
}