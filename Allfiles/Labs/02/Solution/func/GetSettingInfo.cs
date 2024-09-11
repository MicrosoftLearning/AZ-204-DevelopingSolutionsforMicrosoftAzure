using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace func
{
    public class GetSettingInfo
    {
        private readonly ILogger _logger;

        public GetSettingInfo(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetSettingInfo>();
        }

        [Function("GetSettingInfo")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req, 
            [BlobInput("content/settings.json", Connection = "AzureWebJobsStorage")] string blobContent
            )
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            _logger.LogInformation($"{blobContent}");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            await response.WriteStringAsync($"{blobContent}");

            return response;
        }
    }
}
