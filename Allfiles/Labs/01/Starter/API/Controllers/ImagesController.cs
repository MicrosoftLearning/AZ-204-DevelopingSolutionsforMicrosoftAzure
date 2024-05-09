using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class ImagesController : ControllerBase
    {
        private HttpClient _httpClient;
        private Options _options;

        public ImagesController(HttpClient httpClient, Options options)
        {
            _httpClient = httpClient;
            _options = options;
        }

        private async Task<BlobContainerClient> GetCloudBlobContainer(string containerName)
        {
            BlobServiceClient serviceClient = new BlobServiceClient(_options.StorageConnectionString);
            BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
            return containerClient;
        }

        [Route("/")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            BlobContainerClient containerClient = await GetCloudBlobContainer(_options.FullImageContainerName);

            BlobClient blobClient;
            BlobSasBuilder blobSasBuilder;

            List<string> results = new List<string>();
            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {

                blobClient = containerClient.GetBlobClient(blobItem.Name);
                blobSasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = _options.FullImageContainerName,
                    BlobName = blobItem.Name,
                    ExpiresOn = DateTime.UtcNow.AddMinutes(5),//Let SAS token expire after 5 minutes.
                    Protocol = SasProtocol.Https
                };
                blobSasBuilder.SetPermissions(BlobSasPermissions.Read);


                results.Add(blobClient.GenerateSasUri(blobSasBuilder).AbsoluteUri);

            }
            Console.Out.WriteLine("Got Images");
            return Ok(results);
        }

        [Route("/")]
        [HttpPost]
        public async Task<ActionResult> Post()
        {
            Stream image = Request.Body;
            BlobContainerClient containerClient = await GetCloudBlobContainer(_options.FullImageContainerName);
            string blobName = Guid.NewGuid().ToString().ToLower().Replace("-", String.Empty);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(image);
            return Created(blobClient.Uri, null);
        }
    }
}