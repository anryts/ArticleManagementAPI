using Common.Options;

namespace Common.Extensions;

public static class TransferString
{
    
    /// <summary>
    ///  Transfer string into pattern
    /// </summary>
    /// <param name="text">string which you wanna transform</param>
    /// <param name="symbol">can be |, &, or which you need</param>
    /// <returns></returns>
    public static string TransferStringIntoPattern(this string text, char symbol)
    {
        var searchKeys = text.Split(Delimiters.DELIMITERS,
            StringSplitOptions.RemoveEmptyEntries);
        
        return string.Join($" {symbol} ", searchKeys);
    }
}