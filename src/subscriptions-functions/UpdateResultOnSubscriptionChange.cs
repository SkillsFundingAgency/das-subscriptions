using System.Collections.Generic;
using System.IO;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using NLog.Config;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Esfa.Recruit.Subscriptions.Functions
{ 
    public static class UpdateResultOnSubscriptionChange
    {
        private static NLog.ILogger Logger;

        static UpdateResultOnSubscriptionChange()
        {
            var directory = Directory.GetCurrentDirectory();
            LogManager.Configuration = new XmlLoggingConfiguration("nlog.config");
            Logger = LogManager.GetCurrentClassLogger();
        }

        [FunctionName("UpdateResultOnSubscriptionChange")]
        [ return: Queue("subscriptions", Connection = "AzureWebJobsStorage")]
        public static void Run([CosmosDBTrigger(
            databaseName: "recruit-subscriptions",
            collectionName: "das-subscriptions",
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
