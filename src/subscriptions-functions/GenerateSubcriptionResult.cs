using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using NLog;
using NLog.Config;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Esfa.Recruit.Subscriptions.Functions
{
    public static class GenerateSubcriptionResult
    {
        private static NLog.ILogger Logger;

        static GenerateSubcriptionResult()
        {
            LogManager.Configuration = new XmlLoggingConfiguration("nlog.config");
            Logger = LogManager.GetCurrentClassLogger();
        }

        [FunctionName("GenerateSubcriptionResult")]
        public static async Task Run(
            [QueueTrigger("subscriptions", Connection = "AzureWebJobsStorage")]string myQueueItem, 
            ILogger log,  
            [Table("subscriptionResults", Connection = "AzureWebJobsStorage")] CloudTable cloudTable)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            var value = JsonConvert.DeserializeObject<SubscriptionItem>(myQueueItem);

            var result = new ResultView();
            result.PartitionKey = "search-results";
            result.RowKey = value.Id;
            result.Results = GenerateSearchResult(value);

            var operation = TableOperation.InsertOrMerge(result);

            await cloudTable.ExecuteAsync(operation);
        }

        public static string GenerateSearchResult(SubscriptionItem criteria)
        {
            var results = new List<dynamic>
            {
                new { Name = "SearchResult1" },
                new { Name = "SearchResult2" },
                new { Name = "SearchResult3" },
                new { Name = "SearchResult4" }
            };

            return JsonConvert.SerializeObject(results); 
        }

    }

    public class ResultView : TableEntity
    {
        public string Results { get; set; }
    }
}
