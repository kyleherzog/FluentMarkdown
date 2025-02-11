using FluentMarkdown.Builders;

namespace FluentMarkdown.UnitTests.Builders.InlineStyledMarkdownBuilderTests;

[TestClass]
public class AddLinkShould
{
    [TestMethod]
    public void AddLinkGivenUrlAndText()
    {
        var builder = new InlineStyledMarkdownBuilder();
        var url = "https://example.com/my path";
        var text = "Hello, World!";
        builder.AddLink(url, text);
        Assert.AreEqual($"[{text}](https://example.com/my%20path)", builder.ToString());
    }

    [TestMethod]
    public void AddLinkGivenUrlOnly()
    {
        var builder = new InlineStyledMarkdownBuilder();
        var url = "https://example.com";
        builder.AddLink(url);
        Assert.AreEqual($"<{url}>", builder.ToString());
    }

    [TestMethod]
    public void AddLinkGivenUrlTextAndTitle()
    {
        var builder = new InlineStyledMarkdownBuilder();
        var url = "https://example.com";
        var text = "Hello, World!";
        var title = "Example";
        builder.AddLink(url, text, title);
        Assert.AreEqual($"[{text}]({url} \"{title}\")", builder.ToString());
    }
}