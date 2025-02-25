namespace FluentMarkdown.Internals;

internal class LinePrefixStack
{
    private readonly Stack<string> stack = new();

    public void Pop()
    {
        stack.Pop();
    }

    public void PushBlockquote()
    {
        stack.Push(">");
    }

    public void PushIndent()
    {
        stack.Push(new string(' ', MarkdownConfiguration.Global.IndentSize));
    }

    public override string ToString()
    {
        if (stack.Count == 0)
        {
            return string.Empty;
        }

        var result = string.Join(string.Empty, stack.Reverse());

        if (!result.EndsWith(" "))
        {
            result += " ";
        }

        return result;
    }
}