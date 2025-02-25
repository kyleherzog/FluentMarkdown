using FluentMarkdown.Internals;

namespace FluentMarkdown.UnitTests.Internals.LinePrefixStackTests;

[TestClass]
public class ToStringShould
{
    [TestMethod]
    public void ReturnCorrectlyGivenMultipleItemsEndingWithBlockquote()
    {
        var stack = new LinePrefixStack();
        stack.PushBlockquote();
        stack.PushBlockquote();
        stack.PushBlockquote();
        stack.PushIndent();
        stack.PushBlockquote();

        var result = stack.ToString();
        Assert.AreEqual(">>>    > ", result);
    }

    [TestMethod]
    public void ReturnCorrectlyGivenMultipleItemsEndingWithIndent()
    {
        var stack = new LinePrefixStack();
        stack.PushBlockquote();
        stack.PushBlockquote();
        stack.PushBlockquote();
        stack.PushIndent();

        var result = stack.ToString();
        Assert.AreEqual(">>>    ", result);
    }
}