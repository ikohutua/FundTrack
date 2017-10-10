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
        private static string _connectionString = "StorageConnectionString";
        private static string _containerName = "images";

        CloudStorageAccount _storageAccount;
        CloudBlobClient _blobClient;
        CloudBlobContainer _container;

        public AzureImageManagementService()
        {
            // Parse the connection string and return a reference to the storage account.
            // _storageAccount = CloudStorageAccount.Parse(_connectionString);

            //_blobClient = _storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            //_container = _blobClient.GetContainerReference(_containerName);
        }

        public async Task<string> UploadImage(byte[] file)
        {
            string imageName = Guid.NewGuid().ToString();

            CloudBlockBlob blockBlob = _container.GetBlockBlobReference(imageName);

            using (MemoryStream ms = new MemoryStream(file))
            {
               await blockBlob.UploadFromStreamAsync(ms);
            }

            return imageName;
        }
    }
}