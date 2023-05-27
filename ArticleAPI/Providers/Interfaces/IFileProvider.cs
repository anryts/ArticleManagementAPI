namespace ArticleAPI.Providers.Interfaces;

public interface IFileProvider
{
    /// <summary>
    ///  Use to save string in file
    /// </summary>
    /// <param name="content">content which you want to save in file</param>
    /// <param name="nameOfFile">Name of file which will have your saved file</param>
    /// <param name="fileFormat">file format in which you want to save this content (by default it's .txt)</param>
    /// <returns>Path where saved file or string.Empty if image wasn't saved</returns>
    /// <exception cref="System.Exception">Thrown when an error occurs while writing the content to the file</exception>
    Task<string> SaveFileAsync(string content, string nameOfFile, string fileFormat = "txt");

    /// <summary>
    /// Use to save image 
    /// </summary>
    /// <param name="file">File which you want to store</param>
    /// <param name="nameOfFile">Name of file which will have your saved file</param>
    /// <returns>Path where saved file or string.Empty if image wasn't saved</returns>
    /// <exception cref="System.Exception">Thrown when an error occurs while writing the content to the file</exception>
    Task<string> SaveFileAsync(IFormFile file, string nameOfFile);

    /// <summary>
    /// Use this to save stream into file
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="nameOfFile">Name of file which will have your saved file</param>
    /// <param name="fileFormat">File format</param>
    /// <returns>Path where saved file or string.Empty if image wasn't saved</returns>
    /// <exception cref="System.Exception">Thrown when an error occurs while writing the content to the file</exception>
    Task<string> SaveFileAsync(MemoryStream stream, string nameOfFile, string fileFormat = "text/txt");

    /// <summary>
    /// Use to delete file
    /// </summary>
    /// <param name="filePath">path to file</param>
    /// <returns></returns>
    /// <exception cref="System.Exception">Thrown when an error occurs while deleting file</exception>
    Task DeleteFileAsync(string filePath);

    /// <summary>
    /// Use to delete a collection of files
    /// </summary>
    /// <param name="filePaths">path to files</param>
    /// <returns></returns>
    Task DeleteFileAsync(IEnumerable<string> filePaths);

    /// <summary>
    /// Use to get file via file path 
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    Task<Stream> GetFileAsync(string filePath);
}