using FluentMarkdown.Builders;

namespace FluentMarkdown.UnitTests.Builders.ParagraphBuilderTests;

[TestClass]
public class AddLineShould
{
    [TestMethod]
    public void AddLineGivenAction()
    {
        var builder = new ParagraphMarkdownBuilder();
        builder.AddLine(b =>
        {
            b.Add("This is a line ");
            b.AddTextBold("in bold");
        });
        Assert.AreEqual($"This is a line **in bold**  {Environment.NewLine}", builder.ToString());
    }

    [TestMethod]
    public void AddLineGivenEmptyText()
    {
        var builder = new ParagraphMarkdownBuilder();
        builder.AddLine(string.Empty);
        Assert.AreEqual($"  {Environment.NewLine}", builder.ToString());
    }

    [TestMethod]
    public void AddLineGivenNullText()
    {
        var builder = new ParagraphMarkdownBuilder();
        string? content = null;
        builder.AddLine(content);
        Assert.AreEqual($"  {Environment.NewLine}", builder.ToString());
    }

    [TestMethod]
    public void AddLineGivenSingleLineOfText()
    {
        var builder = new ParagraphMarkdownBuilder();
        builder.AddLine("This is a line");
        Assert.AreEqual($"This is a line  {Environment.NewLine}", builder.ToString());
    }

    [TestMethod]
    public void StartNewLineGivenNotAtStartOfLine()
    {
        var builder = new ParagraphMarkdownBuilder();
        builder.Add("This is a line");
        builder.AddLine("This is a new line");
        Assert.AreEqual($"This is a line{Environment.NewLine}This is a new line  {Environment.NewLine}", builder.ToString());
    }

    [TestMethod]
    public void ThrowArgumentNullExceptionGivenNullAction()
    {
        var builder = new ParagraphMarkdownBuilder();
        Action<InlineStyledMarkdownBuilder> action = null!;
        Assert.ThrowsExactly<ArgumentNullException>(() => builder.AddLine(action));
    }
}