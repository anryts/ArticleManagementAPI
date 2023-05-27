namespace Common.Extensions;

public static class TransferStream 
{
    /// <summary>
    /// Use this method to get string from stream
    /// </summary>
    /// <param name="stream"></param>
    /// <returns>Transferred string from stream</returns>
    public static async Task<string> TransferStreamIntoText(this Stream stream)
    {
        try
        {
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            string response = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}