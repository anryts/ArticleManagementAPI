using System.Text;

namespace UserAPI.Exthensions;

public static class GenerateCodeExtension
{
    /// <summary>
    /// Use this method to generate random code
    /// </summary>
    /// <param name="length">A length of your code</param>
    /// <returns>Code in string format</returns>
    public static string GenerateCode(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var stringBuilder = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            stringBuilder.Append(chars[random.Next(chars.Length)]);
        }
        return stringBuilder.ToString();
    }
}