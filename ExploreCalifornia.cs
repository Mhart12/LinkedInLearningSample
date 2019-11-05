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
using ExploreCaliforniaFunction;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public static class ExploreCaliforniaFunction
    {
        [FunctionName("ExploreCalifornia")]
        public async static Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, TraceWriter log)
        {
            var computerVisionClient = new ComputerVisionClient(new ApiKeyServiceClientCredentials(Constants.ComputerVisionApiKey));
            computerVisionClient.Endpoint = Constants.ComputerVisionEndpoint;

            var imageStream = req.Body;

            var result = await computerVisionClient.AnalyzeImageInStreamAsync(imageStream, details: new [] { Details.Landmarks });
            var landmark = result.Categories.FirstOrDefault(c => c.Detail != null && c.Detail.Landmarks.Any());

            return landmark != null ? (ActionResult)new OkObjectResult($"I think I see the {landmark.Detail.Landmarks.First().Name}")
                    : new BadRequestObjectResult("No landmark was found in this image");
            
        }
    }
}
