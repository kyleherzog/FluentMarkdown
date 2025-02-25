namespace FluentMarkdown.CodeBlocks;

/// <summary>
/// Provides extension methods for <see cref="BlockLanguage"/> objects.
/// </summary>
public static class BlockLanguageExtensions
{
    private static readonly Dictionary<BlockLanguage, string> LanguageMap = new()
    {
        { BlockLanguage.None, string.Empty },
        { BlockLanguage.CiscoIOS, "cisco_ios" },
        { BlockLanguage.CommonLisp, "common_lisp" },
        { BlockLanguage.ConfigFile, "conf" },
        { BlockLanguage.CSVSchema, "csvs" },
        { BlockLanguage.GHCCmm, "ghc-cmm" },
        { BlockLanguage.GHCCore, "ghc-core" },
        { BlockLanguage.HyLang, "hylang" },
        { BlockLanguage.IDL, "idlang" },
        { BlockLanguage.JsonDoc, "json-doc" },
        { BlockLanguage.LiterateCoffeeScript, "literate_coffeescript" },
        { BlockLanguage.LiterateHaskell, "literate_haskell" },
        { BlockLanguage.ObjectiveC, "objective_c" },
        { BlockLanguage.ObjectiveCPP, "objective_cpp" },
        { BlockLanguage.RobotFramework, "robot_framework" },
        { BlockLanguage.SPARQL, "sparql" },
        { BlockLanguage.SSHConfigFile, "ssh" },
        { BlockLanguage.Shell, "shell" },
        { BlockLanguage.Turtle, "turtle" },
        { BlockLanguage.TypeScript, "typescript" },
        { BlockLanguage.VHDL, "vhdl" },
        { BlockLanguage.VisualBasic, "vb" },
    };

    /// <summary>
    /// Gets the text identifier for the specified block language.
    /// </summary>
    /// <param name="language">
    /// The block language for which to get the identifier.
    /// </param>
    /// <returns>
    /// The text identifier for the specified block language.
    /// </returns>
    public static string ToIdentifier(this BlockLanguage language)
    {
        if (LanguageMap.TryGetValue(language, out var value))
        {
            return value;
        }

        return language.ToString().ToLower();
    }
}