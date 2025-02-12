using FluentMarkdown.Builders;

namespace FluentMarkdown.UnitTests.Builders.InlineStyledMarkdownBuilderTests;

[TestClass]
public class AddLinkedImageShould
{
    [TestMethod]
    public void AddLinkedImageGivenNoTitle()
    {
        var builder = new InlineStyledMarkdownBuilder();
        var imageAddress = "https://example.com/my path.jpg";
        var altText = "Hello, World!";
        var destinationAddress = "https://example.com";
        builder.AddLinkedImage(destinationAddress, imageAddress, altText);
        Assert.AreEqual($"[![{altText}](https://example.com/my%20path.jpg)](https://example.com)", builder.ToString());
    }

    [TestMethod]
    public void AddLinkedImageGivenTitle()
    {
        var builder = new InlineStyledMarkdownBuilder();
        var imageAddress = "https://example.com/my path.jpg";
        var altText = "Hello, World!";
        var destinationAddress = "https://example.com";
        var title = "Example";
        builder.AddLinkedImage(destinationAddress, imageAddress, altText, title);
        Assert.AreEqual($"[![{altText}](https://example.com/my%20path.jpg)](https://example.com \"{title}\")", builder.ToString());
    }
}