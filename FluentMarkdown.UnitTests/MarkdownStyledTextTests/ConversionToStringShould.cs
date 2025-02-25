namespace FluentMarkdown.UnitTests.MarkdownStyledTextTests;

[TestClass]
public class ConversionToStringShould
{
    [TestMethod]
    public void ReturnTextEscapedGivenSpecialCharacters()
    {
        var text = "Hello, World! *This is a test*".WithBold().WithItalic();
        Assert.AreEqual("***Hello, World! \\*This is a test\\****", text);
    }

    [TestMethod]
    public void ReturnTextGivenBold()
    {
        var text = "Hello, World!".WithBold();
        Assert.AreEqual("**Hello, World!**", text);
    }

    [TestMethod]
    public void ReturnTextGivenHighlight()
    {
        var text = "Hello, World".WithHighlight();
        Assert.AreEqual("==Hello, World==", text);
    }

    [TestMethod]
    public void ReturnTextGivenItalic()
    {
        var text = "Hello, World!".WithItalic();
        Assert.AreEqual("*Hello, World!*", text);
    }

    [TestMethod]
    public void ReturnTextGivenMultipleStyles()
    {
        var text = "Hello, World".WithBold().WithItalic().WithStrikethrough().WithHighlight();
        Assert.AreEqual("~~***==Hello, World==***~~", text);
    }

    [TestMethod]
    public void ReturnTextGivenStrikethrough()
    {
        var text = "Hello, World!".WithStrikethrough();
        Assert.AreEqual("~~Hello, World!~~", text);
    }

    [TestMethod]
    public void ReturnTextGivenSubscript()
    {
        var text = "Hello, World".WithSubscript();
        Assert.AreEqual("~Hello, World~", text);
    }

    [TestMethod]
    public void ReturnTextGivenSuperscript()
    {
        var text = "Hello, World".WithSuperscript();
        Assert.AreEqual("^Hello, World^", text);
    }

    [TestMethod]
    public void ReturnTextTrimmedGivenEndWhitespace()
    {
        var text = "Hello, World!   ".WithBold();
        Assert.AreEqual("**Hello, World!**", text);
    }

    [TestMethod]
    public void ReturnTextTrimmedGivenStartWhitespace()
    {
        var text = "   Hello, World!".WithBold();
        Assert.AreEqual("**Hello, World!**", text);
    }

    [TestMethod]
    public void ReturnTextWithHtmlGivenSubscriptAndStrikethrough()
    {
        var text = "Hello, World".WithSubscript().WithStrikethrough();
        Assert.AreEqual("<sub>~~Hello, World~~</sub>", text);
    }

    [TestMethod]
    public void ReturnTextWithNoHtmlGivenStyleBetweenSubscriptAndStrikethrough()
    {
        var text = "Hello, World".WithSubscript().WithBold().WithStrikethrough();
        Assert.AreEqual("~~**~Hello, World~**~~", text);
    }

    [TestMethod]
    public void ReturnTextWithoutSubscriptGivenSuperscript()
    {
        var text = "Hello, World".WithSubscript().WithSuperscript();
        Assert.AreEqual("^Hello, World^", text);
    }

    [TestMethod]
    public void ReturnTextWithoutSuperscriptGivenSubscript()
    {
        var text = "Hello, World".WithSuperscript().WithSubscript();
        Assert.AreEqual("~Hello, World~", text);
    }
}