using System.Text;
using Common.Options;
using HtmlAgilityPack;

namespace Common.Extensions;

public static class ShortIntroExtensions
{
    /// <summary>
    /// Use this method to get short intro from content in html format
    /// </summary>
    /// <param name="content">string in html format</param>
    /// <returns>A pure text without html tags</returns>
    public static string GetShortIntroFromContent(this string content)
    {
        // remove html tags from content to get pure text in tags
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(content);
        StringBuilder textWithoutHtml = new StringBuilder();
        var nodes = htmlDoc.DocumentNode.DescendantsAndSelf();
        foreach (var node in nodes.Where(n => AllowedTags.ALLOWED_TAGS.Contains(n.Name)))
        {
            textWithoutHtml.Append(' ' + node.InnerText);
        }

        var result = textWithoutHtml.ToString();
        if (result.Length > 500)
        {
            result = result.Length > 1000 ? result[..1000] : result;
        }
        else
        {
            result = result.Substring(0, result.Length);
        }            

        //check last space to get entire word
        int leftSpaceIndex = result.LastIndexOf(' ');
        return result[..leftSpaceIndex];
    }
}