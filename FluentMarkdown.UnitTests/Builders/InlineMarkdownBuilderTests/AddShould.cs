using FluentMarkdown.Builders;

namespace FluentMarkdown.UnitTests.Builders.InlineMarkdownBuilderTests;

[TestClass]
public class AddShould
{
    [TestMethod]
    public void AddNothingGivenEmptyText()
    {
        var builder = new InlineMarkdownBuilder();
        builder.Add(string.Empty);
        Assert.AreEqual(string.Empty, builder.ToString());
    }

    [TestMethod]
    public void AddNothingGivenNullText()
    {
        var builder = new InlineMarkdownBuilder();
        builder.Add((string)null!);
        Assert.AreEqual(string.Empty, builder.ToString());
    }

    [TestMethod]
    public void AddToBuilderGivenMultipleCalls()
    {
        var builder = new InlineMarkdownBuilder();
        builder.Add("Hello, ");
        builder.Add("World!");
        Assert.AreEqual("Hello, World!", builder.ToString());
    }

    [TestMethod]
    public void AddToBuilderGivenText()
    {
        var builder = new InlineMarkdownBuilder();
        builder.Add("Hello, World!");
        Assert.AreEqual("Hello, World!", builder.ToString());
    }

    [TestMethod]
    public void ThrowExceptionGivenNullBuilder()
    {
        var builder = new InlineMarkdownBuilder();
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            builder.Add((InlineMarkdownBuilder)null!);
        });
    }
}