using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Esfa.Recruit.Subscriptions.Functions
{
    public static class GenerateSubcriptionResult
    {
        [FunctionName("GenerateSubcriptionResult")]
        public static void Run([QueueTrigger("subscriptions", Connection = "AzureWebJobsStorage")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
