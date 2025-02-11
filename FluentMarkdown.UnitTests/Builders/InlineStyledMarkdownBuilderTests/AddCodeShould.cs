using FluentMarkdown.Builders;

namespace FluentMarkdown.UnitTests.Builders.InlineStyledMarkdownBuilderTests;

[TestClass]
public class AddCodeShould
{
    [TestMethod]
    public void AddCodeGivenAction()
    {
        var builder = new InlineStyledMarkdownBuilder();
        var code = "Hello, World!";
        builder.AddCode(b =>
        {
            b.Add(code);
            b.Add(code);
        });
        Assert.AreEqual($"`{code}{code}`", builder.ToString());
    }

    [TestMethod]
    public void AddCodeGivenString()
    {
        var builder = new InlineStyledMarkdownBuilder();
        var code = "World `o Hello `o!";
        builder.AddCode(code);
        Assert.AreEqual("`World ``o Hello ``o!`", builder.ToString());
    }

    [TestMethod]
    public void AddNothingGivenEmptyString()
    {
        var builder = new InlineStyledMarkdownBuilder();
        var code = string.Empty;
        builder.AddCode(code);
        Assert.AreEqual(string.Empty, builder.ToString());
    }

    [TestMethod]
    public void AddNothingGivenNullString()
    {
        var builder = new InlineStyledMarkdownBuilder();
        string code = null!;
        builder.AddCode(code);
        Assert.AreEqual(string.Empty, builder.ToString());
    }

    [TestMethod]
    public void ThrowExceptionGivenNullAction()
    {
        var builder = new InlineStyledMarkdownBuilder();
        Action<CodeMarkdownBuilder> action = null!;
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            builder.AddCode(action);
        });
    }
}