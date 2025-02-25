using FluentMarkdown.Internals;

namespace FluentMarkdown.Builders;

/// <summary>
/// A builder for definition list items.
/// </summary>
public class DefinitionListItemBuilder : NestableElementBuilder
{
    internal DefinitionListItemBuilder(LinePrefixStack prefixes)
        : base(prefixes)
    {
    }

    /// <summary>
    /// Adds a definition to the definition list item.
    /// </summary>
    /// <param name="definition">
    /// The definition to add to the definition list item.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="DefinitionListItemBuilder"/> class.
    /// </returns>
    public DefinitionListItemBuilder AddDefinition(string definition) => AddDefinition(b => b.Add(definition));

    /// <summary>
    /// Adds a definition to the definition list item.
    /// </summary>
    /// <param name="buildDefinition">
    /// The action to apply to the builder that will generate the content inside the definition list item.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="DefinitionListItemBuilder"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified action is <see langword="null"/>.
    /// </exception>
    public DefinitionListItemBuilder AddDefinition(Action<InlineStyledMarkdownBuilder> buildDefinition)
    {
        if (buildDefinition is null)
        {
            throw new ArgumentNullException(nameof(buildDefinition));
        }

        var definitionBuilder = new InlineStyledMarkdownBuilder();
        buildDefinition(definitionBuilder);
        Builder.Add(Prefixes.ToString())
            .Add(": ")
            .Add(definitionBuilder.ToString())
            .Add("  ")
            .Add(Environment.NewLine);
        return this;
    }

    /// <summary>
    /// Specifies the term for the definition list item.
    /// </summary>
    /// <param name="term">
    /// The term to add to the definition list item.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="DefinitionListItemBuilder"/> class.
    /// </returns>
    public DefinitionListItemBuilder WithTerm(string term) => WithTerm(b => b.Add(term));

    /// <summary>
    /// Specifies the term for the definition list item.
    /// </summary>
    /// <param name="buildTerm">
    /// The action to apply to the builder that will generate the content inside the term of the definition list item.
    /// </param>
    /// <returns>
    /// The current instance of the <see cref="DefinitionListItemBuilder"/> class.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the specified action is <see langword="null"/>.
    /// </exception>
    public DefinitionListItemBuilder WithTerm(Action<InlineStyledMarkdownBuilder> buildTerm)
    {
        if (buildTerm is null)
        {
            throw new ArgumentNullException(nameof(buildTerm));
        }

        var termBuilder = new InlineStyledMarkdownBuilder();
        buildTerm(termBuilder);
        Builder.Add(Prefixes.ToString());
        Builder.Add(termBuilder.ToString());
        Builder.Add(Environment.NewLine);
        return this;
    }
}