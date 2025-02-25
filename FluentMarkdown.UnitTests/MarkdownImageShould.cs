namespace FluentMarkdown.UnitTests;

[TestClass]
public class MarkdownImageShould
{
    [TestMethod]
    public void ConvertToStringGivenAddress()
    {
        var image = "https://example.com/image.png".AsImage();
        Assert.AreEqual("![](https://example.com/image.png)", image);
    }

    [TestMethod]
    public void ConvertToStringGivenAddressAndAltText()
    {
        var image = "https://example.com/image.png".AsImage().WithAltText("My Image");
        Assert.AreEqual("![My Image](https://example.com/image.png)", image);
    }

    [TestMethod]
    public void ConvertToStringGivenAddressAltTextAndTitle()
    {
        var image = "https://example.com/image.png".AsImage().WithAltText("My Image").WithTitle("Hover over text here.");
        Assert.AreEqual("![My Image](https://example.com/image.png \"Hover over text here.\")", image);
    }

    [TestMethod]
    public void ThrowArgumentExceptionGivenEmptyAddress()
    {
        Assert.ThrowsExactly<ArgumentException>(() =>
        {
            string.Empty.AsImage();
        });
    }

    [TestMethod]
    public void ThrowArgumentNullExceptionGivenNullAddress()
    {
        string value = null!;
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            value.AsImage();
        });
    }
}