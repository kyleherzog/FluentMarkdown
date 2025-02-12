namespace FluentMarkdown.UnitTests.MarkdownLinkTests;

[TestClass]
public class ConversionToStringShould
{
    [TestMethod]
    public void ConvertToStringGivenDisplayText()
    {
        var link = "Example".WithLinkTo("https://example.com");
        Assert.AreEqual("[Example](https://example.com)", link);
    }

    [TestMethod]
    public void ConvertToStringGivenNoDisplayText()
    {
        var link = "https://example.com".AsLink();
        Assert.AreEqual("<https://example.com>", link);
    }

    [TestMethod]
    public void ConvertToStringGivenDisplayTextAndTitle()
    {
        var link = "Example".WithLinkTo("https://example.com").WithTitle("Hover over text here.");
        Assert.AreEqual("[Example](https://example.com \"Hover over text here.\")", link);
    }

    [TestMethod]
    public void ThrowArgumentExceptionGivenEmptyDestination()
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {
            string.Empty.AsLink();
        });
    }

    [TestMethod]
    public void ThrowArgumentNullExceptionGivenNullDestination()
    {
        string value = null!;
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            value.AsLink();
        });
    }
}