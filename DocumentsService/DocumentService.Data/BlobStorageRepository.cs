using DocumentService.Domain.DataTransferObjects;
using DocumentService.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace DocumentService.Data
{
    public class BlobStorageRepository : IBlobStorageRepository
    {
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;

        public BlobStorageRepository(IConfiguration configuration)
        {
            _storageConnectionString = configuration.GetSection("BlobConnectionString").Value;
            _storageContainerName = configuration.GetSection("BlobContainerName").Value;

        }

        public async Task<BlobManipulationDto> DeleteAsync(string blobFilename)
        {
            BlobContainerClient client = new BlobContainerClient(_storageConnectionString, _storageContainerName);
            BlobClient file = client.GetBlobClient(blobFilename);

            try
            {
                // Delete the file
                await file.DeleteAsync();
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                // File did not exist, log to console and return new response to requesting method
                return new BlobManipulationDto { Error = true, Status = $"File with name {blobFilename} not found." };
            }

            // Return a new BlobResponseDto to the requesting method
            return new BlobManipulationDto { Error = false, Status = $"File: {blobFilename} has been successfully deleted." };

        }

        public async Task<BlobDto?> DownloadAsync(string blobFilename)
        {
            // Get a reference to a container named in appsettings.json
            BlobContainerClient client = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            try
            {
                // Get a reference to the blob uploaded earlier from the API in the container from configuration settings
                BlobClient file = client.GetBlobClient(blobFilename);

                // Check if the file exists in the container
                if (await file.ExistsAsync())
                {
                    var data = await file.OpenReadAsync();
                    Stream blobContent = data;

                    // Download the file details async
                    var content = await file.DownloadContentAsync();

                    // Add data to variables in order to return a BlobDto
                    string name = blobFilename;
                    string contentType = content.Value.Details.ContentType;

                    // Create new BlobDto with blob data from variables
                    return new BlobDto { Content = blobContent, Name = name, ContentType = contentType };
                }
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                //log
                
            }

            // File does not exist, return null and handle that in requesting method
            return null;
        }

        public async Task<List<BlobDto>> ListAsync()
        {
            var blobServiceClient = new BlobServiceClient(_storageConnectionString);
            var container = blobServiceClient.GetBlobContainerClient(_storageContainerName);
            await container.CreateIfNotExistsAsync();

            // Create a new list object for 
            List<BlobDto> files = new List<BlobDto>();

            await foreach (BlobItem file in container.GetBlobsAsync())
            {
                // Add each file retrieved from the storage container to the files list by creating a BlobDto object
                string uri = container.Uri.ToString();
                var name = file.Name;
                var fullUri = $"{uri}/{name}";

                files.Add(new BlobDto
                {
                    Uri = fullUri,
                    Name = name,
                    ContentType = file.Properties.ContentType
                });
            }

            // Return all files to the requesting method
            return files;
        }

        public async Task<BlobManipulationDto> UploadAsync(IFormFile blob)
        {
            // Create new upload response object that we can return to the requesting method
            BlobManipulationDto response = new();

            // Get a reference to a container named in appsettings.json and then create it
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            // Create the container if it does not exist
            await container.CreateIfNotExistsAsync();

            try
            {
                // Get a reference to the blob just uploaded from the API in a container from configuration settings
                BlobClient client = container.GetBlobClient(blob.FileName);

                // Open a stream for the file we want to upload
                await using (Stream? data = blob.OpenReadStream())
                {
                    // Upload the file async
                    await client.UploadAsync(data);
                }

                // Everything is OK and file got uploaded
                response.Status = $"File {blob.FileName} Uploaded Successfully";
                response.Error = false;
                response.Blob.Uri = client.Uri.AbsoluteUri;
                response.Blob.Name = client.Name;

            }
            // If the file already exists, we catch the exception and do not upload it
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
            {
                response.Status = $"File with name {blob.FileName} already exists. Please use another name to store your file.";
                response.Error = true;
                return response;
            }
            // If we get an unexpected error, we catch it here and return the error message
            catch (RequestFailedException ex)
            {
                // Log error to console and create a new response we can return to the requesting method
                response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
                response.Error = true;
                return response;
            }

            // Return the BlobUploadResponse object
            return response;
        }
    }
}
