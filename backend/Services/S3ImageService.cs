using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace Backend.Services;

public class S3ImageService : IS3ImageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly IConfiguration _configuration;
    private readonly string _bucketName;

    public S3ImageService(IAmazonS3 s3Client, IConfiguration configuration)
    {
        _s3Client = s3Client;
        _configuration = configuration;
        _bucketName = configuration["AWS:BucketName"]!;
    }

    /// <summary>
    /// Upload file len S3 va tra ve public URL
    /// </summary>
    public async Task<string> UploadImageAsync(IFormFile file, string folder = "products")
    {
        // * 1. Generate unique filename
        var fileExtension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{fileExtension}";
        var key = $"{folder}/{fileName}"; // e.g., "products/abc123.jpg"

        // * 2. Upload to S3
        using var stream = file.OpenReadStream();
        
        var uploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = stream,
            Key = key,
            BucketName = _bucketName,
            ContentType = file.ContentType
            // ! NOTE: Bo CannedACL neu bucket khong cho phep ACL
            // CannedACL = S3CannedACL.PublicRead
        };

        var transferUtility = new TransferUtility(_s3Client);
        await transferUtility.UploadAsync(uploadRequest);

        // * 3. Return public URL
        var region = _configuration["AWS:Region"];
        var imageUrl = $"https://{_bucketName}.s3.{region}.amazonaws.com/{key}";
        
        return imageUrl;
    }

    /// <summary>
    /// Xoa file khoi S3
    /// </summary>
    public async Task<bool> DeleteImageAsync(string imageUrl)
    {
        try
        {
            // * Extract key from URL
            // URL: https://bucket.s3.region.amazonaws.com/products/abc.jpg
            // Key: products/abc.jpg
            var uri = new Uri(imageUrl);
            var key = uri.AbsolutePath.TrimStart('/'); // Remove leading '/'

            // * Delete from S3
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = key
            };

            await _s3Client.DeleteObjectAsync(deleteRequest);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
