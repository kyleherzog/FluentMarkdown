namespace FluentMarkdown.Internals;

internal class LinePrefixStack
{
    private static int indentSize = 4;
    private readonly Stack<string> stack = new();

    public static int IndentSize
    {
        get => indentSize;
        set
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Indent size must be greater than 0");
            }

            indentSize = value;
        }
    }

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
        stack.Push(new string(' ', indentSize));
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