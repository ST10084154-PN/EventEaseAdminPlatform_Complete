
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EventEaseAdminPlatform.Services
{
    public class AzureBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName = "eventease-images";

        public AzureBlobStorageService(IConfiguration config)
        {
            _blobServiceClient = new BlobServiceClient(config["AzureBlobStorage:ConnectionString"]);
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            await containerClient.CreateIfNotExistsAsync();
            var blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
            await blobClient.UploadAsync(file.OpenReadStream());
            return blobClient.Uri.ToString();
        }
    }
}
