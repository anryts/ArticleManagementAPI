using System.Text;
using Amazon.S3;
using Amazon.S3.Model;
using Common.Options;
using Microsoft.Extensions.Options;
using UserAPI.Providers.Interfaces;

namespace UserAPI.Providers;

public class AmazonProvider : IFileProvider
{
    private readonly IOptions<AWSCredentials> _options;
    private readonly AmazonS3Client _s3Client;

    public AmazonProvider(IOptions<AWSCredentials> options)
    {
        _options = options;
        _s3Client = new AmazonS3Client();
    }

    public async Task DeleteFileAsync(string filePath)
    {
        await _s3Client.DeleteObjectAsync(_options.Value.S3BucketName, GetKeyName(filePath));
    }

    public async Task DeleteFileAsync(IEnumerable<string> filePaths)
    {
        var keysName = filePaths
            .Select(pathToFile => new KeyVersion { Key = GetKeyName(pathToFile) })
            .ToList();

        var deleteRequest = new DeleteObjectsRequest
        {
            BucketName = _options.Value.S3BucketName,
            Objects = keysName
        };
        await _s3Client.DeleteObjectsAsync(deleteRequest);
    }

    public async Task<Stream> GetFileAsync(string filePath)
    {
        var getRequest = new GetObjectRequest()
        {
            BucketName = _options.Value.S3BucketName,
            Key = GetKeyName(filePath)
        };

        var result = await _s3Client.GetObjectAsync(getRequest);
        return result.ResponseStream;
    }

    public async Task<string> SaveFileAsync(string content, string nameOfFile, string fileFormat)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(content);
        using var streamContent = new MemoryStream(bytes);
        return await SaveFileAsync(streamContent, nameOfFile, $"text/{fileFormat}");
    }

    public async Task<string> SaveFileAsync(IFormFile file, string nameOfFile)
    {
        var memoryStream = new MemoryStream();
        await file.OpenReadStream().CopyToAsync(memoryStream);
        return await SaveFileAsync(memoryStream, nameOfFile, file.ContentType);
    }

    public async Task<string> SaveFileAsync(MemoryStream stream, string nameOfFile, string contentType)
    {
        PutObjectRequest request = new PutObjectRequest
        {
            BucketName = _options.Value.S3BucketName,
            Key = nameOfFile,
            InputStream = stream,
            ContentType = contentType
        };
        await _s3Client.PutObjectAsync(request);
        return CreateUrl(nameOfFile);
    }

    private string CreateUrl(string nameOfFile)
    {
        string fileUrl =
            $"https://{_options.Value.S3BucketName}.s3.amazonaws.com/{nameOfFile}";
        return fileUrl;
    }

    /// <summary>
    /// retrieve keyName to file, including name of folder
    /// </summary>
    /// <param name="filePath">Url from db</param>
    /// <returns>name of folder and file name</returns>
    private string GetKeyName(string filePath)
    {
        // skip first symbol, which is '/'
        return new Uri(filePath).AbsolutePath[1..];
    }
}