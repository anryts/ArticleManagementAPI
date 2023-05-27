namespace Common.Options;

public static class AllowedTags
{
    /// <summary>
    /// Tags for retrieving text from content field 
    /// </summary>
    public static readonly string[] ALLOWED_TAGS =
    {
        "p",
        "h1", "h2", "h3", "h4", "h5", "h6",
        "a",
        "ul", "ol", "li",
        "table", "tr", "td", "th",
        "form", "input", "textarea", "select", "option",
        "strong", "em", "b", "i"
    };
}

public static class Delimiters
{
    public static readonly string[] DELIMITERS = { " ", ",", ".", "!", "?", ";", ":", "\n", "\r", "\t" };
}