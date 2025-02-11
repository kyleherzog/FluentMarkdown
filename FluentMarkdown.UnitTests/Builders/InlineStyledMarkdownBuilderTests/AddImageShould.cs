using FluentMarkdown.Builders;

namespace FluentMarkdown.UnitTests.Builders.InlineStyledMarkdownBuilderTests;

[TestClass]
public class AddImageShould
{
    [TestMethod]
    public void AddImageGivenUrlAltTextAndTitle()
    {
        var builder = new InlineStyledMarkdownBuilder();
        var url = "https://example.com/my path.jpg";
        var altText = "Hello, World!";
        var title = "Example";
        builder.AddImage(url, altText, title);
        Assert.AreEqual($"![{altText}](https://example.com/my%20path.jpg \"{title}\")", builder.ToString());
    }

    [TestMethod]
    public void AddImageGivenUrlAndAltText()
    {
        var builder = new InlineStyledMarkdownBuilder();
        var url = "https://example.com/my path.jpg";
        var altText = "Hello, World!";
        builder.AddImage(url, altText);
        Assert.AreEqual($"![{altText}](https://example.com/my%20path.jpg)", builder.ToString());
    }
}