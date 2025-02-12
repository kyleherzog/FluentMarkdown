namespace FluentMarkdown.UnitTests.MarkdownBuilderTests;

[TestClass]
public class AddBlockQuoteShould
{
    [TestMethod]
    public void AddBlockQuoteGivenAction()
    {
        var builder = new MarkdownBuilder();
        builder.AddBlockQuote(b =>
        {
            b.AddLine("Line 1");
            b.AddLine("Line 2");
        });
        Assert.AreEqual($"> Line 1  {Environment.NewLine}> Line 2  {Environment.NewLine}", builder.ToString());
    }

    [TestMethod]
    public void AddBlockQuoteGivenMultipleLinesOfText()
    {
        var builder = new MarkdownBuilder();
        builder.AddBlockQuote("This is a block quote", "This is another block quote");
        Assert.AreEqual($"> This is a block quote  {Environment.NewLine}> This is another block quote  {Environment.NewLine}", builder.ToString());
    }

    [TestMethod]
    public void AddBlockQuoteGivenSingleLineOfText()
    {
        var builder = new MarkdownBuilder();
        builder.AddBlockQuote("This is a block quote");
        Assert.AreEqual($"> This is a block quote  {Environment.NewLine}", builder.ToString());
    }

    [TestMethod]
    public void ThrowArgumentNullExceptionGivenNullAction()
    {
        var builder = new MarkdownBuilder();
        Action<MarkdownBuilder> action = null!;
        Assert.ThrowsException<ArgumentNullException>(() => builder.AddBlockQuote(action));
    }
}