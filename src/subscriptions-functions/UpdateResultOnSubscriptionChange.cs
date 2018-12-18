using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Esfa.Recruit.Subscriptions.Functions
{
    public static class UpdateResultOnSubscriptionChange
    {
        [FunctionName("UpdateResultOnSubscriptionChange")]
        [ return: Queue("subscriptions", Connection = "AzureWebJobsStorage")]
        public static void Run([CosmosDBTrigger(
            databaseName: "recruit-subscriptions",
            collectionName: "subscriptions",
            ConnectionStringSetting = "lee-cosmos_DOCUMENTDB",
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input, ILogger log,
            [Queue("subscriptions", Connection = "AzureWebJobsStorage")]ICollector<string> output)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);

                //var myItem = JsonConvert.DeserializeObject<MyItem>(input[0].ToString());

                foreach (var change in input)
                {
                    output.Add(new SubscriptionChange(change.Id));
                }
            }
        }
    }

    public class SubscriptionChange
    {
        public SubscriptionChange(string id) => Id = id;

        public string Id { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static implicit operator string(SubscriptionChange obj)
        {
            return obj.ToString();
        }
    }
}
