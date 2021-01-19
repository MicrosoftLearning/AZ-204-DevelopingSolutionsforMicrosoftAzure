using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

public static class FileParser
{
    [FunctionName("FileParser")]
    public static async Task<IActionResult> Run(
        [HttpTrigger("GET")] HttpRequest request)
    {
        string connectionString = Environment.GetEnvironmentVariable("StorageConnectionString");
        BlobClient blob = new BlobClient(connectionString, "drop", "records.json");
        var response = await blob.DownloadAsync();
        return new FileStreamResult(response?.Value?.Content, response?.Value?.ContentType);
    }
}