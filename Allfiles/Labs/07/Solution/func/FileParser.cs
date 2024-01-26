using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace func
{
    public class FileParser
    {
        private readonly ILogger _logger;

        public FileParser(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FileParser>();
        }

        [Function("FileParser")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);

            string connectionString = Environment.GetEnvironmentVariable("StorageConnectionString");

            /* Create a new instance of the BlobClient class by passing in your
               connectionString variable, a  "drop" string value, and a
               "records.json" string value to the constructor */
            BlobClient blob = new BlobClient(connectionString, "drop", "records.json");

            // Download the content of the referenced blob 
            BlobDownloadResult downloadResult = blob.DownloadContent();

             // Retrieve the value of the downloaded blob and convert it to string
            response.WriteString(downloadResult.Content.ToString());
            
            //return the response
            return response;
        }
    }
}
