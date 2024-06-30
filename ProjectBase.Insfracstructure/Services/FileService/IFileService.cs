using Amazon.S3;
using Microsoft.AspNetCore.Http;

namespace ProjectBase.Insfracstructure.Services.FileService
{
    public interface IFileService
    {
        Task CreateBucket(string bucket, IAmazonS3 client);
        Task UploadFileS3(string bucket, IFormFile file);
    }
}