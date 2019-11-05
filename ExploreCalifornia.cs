using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Company.Function
{
    public static class ExploreCaliforniaFunction
    {
        [FunctionName("ExploreCalifornia")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            var computerVisionClient = new ComputerVisionClient(new ApiKeyServiceClientCredentials(Constants.ComputerVisionApiKey));
            computerVisionClient.Endpoint = Constants.ComputerVisionEndpoint;
        }
    }
}
