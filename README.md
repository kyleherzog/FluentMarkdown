# FluentMarkdown
Provides support for writing markdown documents in a fluent manner.

## Getting Started
The `MarkdownBuilder` class is the entry point for creating markdown documents. It provides a fluent API for creating markdown documents.
```csharp
var builder = new MarkdownBuilder();
builder
    .AddHeader(1, "Fluent Markdown")
    .AddParagraph("This is a paragraph of text.")
    .AddHeader("Another Header", 2)
    .AddParagraph("This is another paragraph of text.")
    .AddCodeBlock("csharp", "Console.WriteLine(\"Hello, World!\");")
    .AddUnorderedList(["First item", "Second item", "Third item"]);
    );
```

## Text Formatting
The `MarkdownBuilder` class provides methods for adding text with some formatting options.
```csharp
builder.AddTextBold("This text is bold.")
    .AddTextItalic("This text is italic.")
    .AddTextBoldItalic("This text is bold and italic.")
    .AddCode("This text is code.");
```

String extension methods are also provided for formatting text in a variety of ways.
```csharp
builder.AddParagraph("Hello, World!".WithBold().WithItalic().WithStrikethrough());
```

## Links and Images
The `MarkdownBuilder` class provides methods for adding links and images.
```csharp
builder.AddLink("https://fluentmarkdown.com", "Fluent Markdown")
    .AddImage("https://placehold.co/600x400", "Placeholder");
```

String extension methods are also provided for adding links and images.
```csharp
builder.AddParagraph("Google".WithLinkTo("https://www.google.com"));
builder.AddParagraph("https://placehold.co/600x400".AsImage().WithLinkTo("https://fluentmarkdown.com"));
```

## Nested Elements
Some elements can be nested within others. For example, a paragraph can contain multiple child elements.  Elements that can have child elements will have an overload that accepts a `Action` delegate.
```csharp
builder.AddParagraph(p =>
{
    p.AddText("This is a paragraph with ");
    p.AddTextBold("bold");
    p.AddText(" text.");
});
```

## Tables
Tables can be added to the document using the `AddTable` method.
```csharp
var tableContent = new string[][]
{
    new string[] { "Row 1, Column 1", "Row 1, Column 2" },
    new string[] { "Row 2, Column 1", "Row 2, Column 2" }
};
builder.AddTable(tb => tb.WithHeader("Column 1", "Column 2")
                        .WithBody(tableContent));
```

## Code Blocks
Code blocks can be added to the document using the `AddCodeBlock()` method.
```csharp
builder.AddCodeBlock("Console.WriteLine(\"Hello, World!\");");
```

The language of the code block can be specified.
```csharp
builder.AddCodeBlock("csharp", "Console.WriteLine(\"Hello, World!\");");
```

A default language can be set for all code blocks.  This can be done at the builder instance level.
```csharp
builder.UsingCodeLanguage(BlockLanguage.CSharp);
```

The default language can be set globally for all instances of `MarkdownBuilder`.
```csharp
MarkdownConfiguration.Global.UsingCodeLanguage(BlockLanguage.CSharp);
```

## Lists
Ordered and unordered lists can be added to the document using the `AddOrderedList()` and `AddUnorderedList()` methods.
```csharp
builder.AddUnorderedList("First item", "Second item", "Third item");
```

Task lists can be added by leveraging the `AddTaskListItem()` method.
```csharp
builder.AddUnorderedList(ul =>
{
    ul.AddTaskListItem("Task 1", true); // checked
    ul.AddTaskListItem("Task 2", false); // unchecked
});
```

Term1
: Definition1

