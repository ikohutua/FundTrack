using System;
using FundTrack.BLL.Abstract;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace FundTrack.BLL.Concrete
{
    public sealed class AzureImageManagementService : IImageManagementService
    {
        private static string _connectionString = "StorageConnectionString";
        private static string _containerName = "images";

        //TODO: Insert correct Url from Azure 
        private static string _baseUrl = "";

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

        public void DeleteImage(string fileName)
        {
            //TODO: Add logic for deliting images
        }

        public void DeleteRangeOfImages(IEnumerable<string> files)
        {
            foreach (var item in files)
            {
                DeleteImage(item);
            }
        }

        public string GetImageUrl(string imageName)
        {
            return _baseUrl + imageName;
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