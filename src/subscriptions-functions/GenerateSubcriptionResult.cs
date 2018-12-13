using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace Esfa.Recruit.Subscriptions.Functions
{
    public static class GenerateSubcriptionResult
    {
        [FunctionName("GenerateSubcriptionResult")]
        public static async Task Run([QueueTrigger("subscriptions", Connection = "AzureWebJobsStorage")]string myQueueItem, ILogger log,  [Table("subscriptionResults", Connection = "AzureWebJobsStorage")] CloudTable cloudTable)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            var value = JsonConvert.DeserializeObject<InputValue>(myQueueItem);

            var result = new ResultView();
            result.PartitionKey = "abc";
            result.RowKey = "1";
            result.Name = value.Name;

            var operation = TableOperation.InsertOrMerge(result);

            await cloudTable.ExecuteAsync(operation);
        }
    }

    public class InputValue
    {
        public string Name { get; set; }
    }

    public class ResultView : TableEntity
    {
        public string Name { get; set; }
    }
}
