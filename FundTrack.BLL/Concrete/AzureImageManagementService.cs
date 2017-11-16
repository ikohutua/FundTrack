using System;
using FundTrack.BLL.Abstract;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using System.IO;

namespace FundTrack.BLL.Concrete
{
    public sealed class AzureImageManagementService : IImageManagementService
    {
        private const string _connectionString = "DefaultEndpointsProtocol=https;AccountName=fundrackss;AccountKey=J8wWP5PN2zILEbqOybOQ8WDPO4U6SytraRQD5HBMdCbNtVVHY0Z9aX3btjsu3GwTWIlzNh1/mPLkFxdyv2fBaQ==;EndpointSuffix=core.windows.net";
        private const string _containerName = "fundtrackssimages";

        CloudStorageAccount _storageAccount;
        CloudBlobClient _blobClient;
        CloudBlobContainer _container;

        public AzureImageManagementService()
        {
            //Parse the connection string and return a reference to the storage account.
            _storageAccount = CloudStorageAccount.Parse(_connectionString);

            _blobClient = _storageAccount.CreateCloudBlobClient();

            //Retrieve a reference to a container.
            _container = _blobClient.GetContainerReference(_containerName);

            PrepareContainer();
        }

        public async void PrepareContainer()
        {
            var wasCreated = await _container.CreateIfNotExistsAsync();

            if (wasCreated)
            {
                await _container.SetPermissionsAsync(
                    new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }
        }

        public async Task<string> UploadImageAsync(byte[] file, string imageExtension)
        {
            string imageName = $"{Guid.NewGuid().ToString()}.{imageExtension}";

            CloudBlockBlob blockBlob = _container.GetBlockBlobReference(imageName);

            using (MemoryStream ms = new MemoryStream(file))
            {
                await blockBlob.UploadFromStreamAsync(ms);
            }

            return imageName;
        }

        public async Task DeleteImageAsync(string name)
        {
            CloudBlockBlob blockBlob = _container.GetBlockBlobReference(name);
            await  blockBlob.DeleteIfExistsAsync();
        }
    }
}