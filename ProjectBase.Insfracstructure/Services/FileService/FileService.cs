using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Http;
using ProjectBase.Domain.Configuration;
using System.Net;

namespace ProjectBase.Insfracstructure.Services.FileService
{
    public class FileService : IFileService
    {
        AppSettingConfiguration _setting;
        private readonly IAmazonS3 _s3Client;
        public FileService(AppSettingConfiguration setting, IAmazonS3 s3Client)
        {
            _setting = setting;
            _s3Client = s3Client;   
        }
        public async Task UploadFileS3(string bucket, IFormFile file)
        {
            /*var config = new AmazonS3Config() 
            { 
                ServiceURL = _setting.AWSSection.S3Url ,
                RegionEndpoint = RegionEndpoint.USEast1
            };
            var client = new AmazonS3Client(_setting.AWSSection.AccessKey, _setting.AWSSection.Secret, config);*/
            await CreateBucket(bucket, _s3Client);


            var objectRequest = new PutObjectRequest()
            {
                BucketName = bucket,
                Key = file.FileName,
                InputStream = file.OpenReadStream(),
            };
            await _s3Client.PutObjectAsync(objectRequest);
        }

        public async Task CreateBucket(string bucket, IAmazonS3 client)
        {
            var buckExists = await AmazonS3Util.DoesS3BucketExistV2Async(client, bucket);
            if(!buckExists)
            {
                var bucketRequest = new PutBucketRequest()
                {
                    BucketName = bucket,
                    UseClientRegion = true,
                };
                var response = await client.PutBucketAsync(bucketRequest);
                if (response.HttpStatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Create bucket failed!");
                }
            }
        }
    }
}
