using Common.Options;
using Microsoft.Extensions.Options;
using UserAPI.Providers.Interfaces;

namespace UserAPI.Providers;

public class FileProvider : IFileProvider
{
    private readonly IOptions<FilePaths> _options;

    public FileProvider(IOptions<FilePaths> options) => _options = options;

    /// <summary>
    /// Use this to save stream into file
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="nameOfFile">Name of file which will have your saved file</param>
    /// <param name="fileFormat">File format</param>
    /// <returns>Path where saved file</returns>
    /// <exception cref="System.Exception">Thrown when an error occurs while writing the content to the file</exception>
    public async Task<string> SaveFileAsync(MemoryStream stream, string nameOfFile, string fileFormat)
    {
        string path = Path.Combine(_options.Value.ArticleImagePath, Guid.NewGuid().ToString());
        try
        {
            await using Stream fileStream = new FileStream(path, FileMode.Create);
            await stream.CopyToAsync(fileStream);
            return path;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return string.Empty;
        }
    }

    /// <summary>
    ///  Use to save string in file
    /// </summary>
    /// <param name="content">content which you want to save in file</param>
    /// <param name="nameOfFile">Name of file which will have your saved file</param>
    /// <param name="fileFormat">file format in which you want to save this content</param>
    /// <returns>Path where saved file</returns>
    /// <exception cref="System.Exception">Thrown when an error occurs while writing the content to the file</exception>
    public async Task<string> SaveFileAsync(string content, string nameOfFile, string fileFormat)
    {
        string path = Path.Combine(_options.Value.ArticleContentPath, nameOfFile + fileFormat);
        try
        {
            await File.WriteAllTextAsync(path, content);
            return path;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Use to save image 
    /// </summary>
    /// <param name="file">File which you want to store</param>
    /// <param name="nameOfFile">Name of file which will have your saved file</param>
    /// <returns>Path where saved image or string.Empty if file wasn't saved</returns>
    /// <exception cref="System.Exception">Thrown when an error occurs while writing the content to the file</exception>
    public async Task<string> SaveFileAsync(IFormFile file, string nameOfFile)
    {
         string? path;
        // concatenate name of file and format  
        var name = nameOfFile + file.FileName[^4..];

        if (file.ContentType.Contains("image"))
            path = Path.Combine(_options.Value.ArticleImagePath, name);
        else
            path = Path.Combine(_options.Value.ArticleContentPath, name);

        try
        {
            await using Stream fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return path;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Use to delete file
    /// </summary>
    /// <param name="filePath">path to file</param>
    /// <returns></returns>
    /// <exception cref="System.Exception">Thrown when an error occurs while deleting file</exception>
    public async Task DeleteFileAsync(string filePath)
    {
        try
        {
            File.Delete(filePath);
        }
        catch (Exception e) 
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task DeleteFileAsync(IEnumerable<string> filePaths)
    {
        throw new NotImplementedException();
    }

    public Task<Stream> GetFileAsync(string filePath)
    {
        throw new NotImplementedException();
    }
}