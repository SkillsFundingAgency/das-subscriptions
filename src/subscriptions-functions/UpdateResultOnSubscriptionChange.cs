using System.Collections.Generic;
using System.IO;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
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
            LogManager.Configuration = new XmlLoggingConfiguration("nlog.config");
            Logger = LogManager.GetCurrentClassLogger();
        }

        [FunctionName("UpdateResultOnSubscriptionChange")]
        [return: Queue("subscriptions", Connection = "AzureWebJobsStorage")]
        public static void Run([CosmosDBTrigger(
            databaseName: "recruit-subscriptions",
            collectionName: "subscriptions",
            ConnectionStringSetting = "SubscriptionsCosmosDb",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> input, ILogger log,
            [Queue("subscriptions", Connection = "AzureWebJobsStorage")]ICollector<string> output)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                Logger.Debug("Documents modified {count}", input.Count);
                
                log.LogInformation("First document Id " + input[0].Id);

                //var myItem = JsonConvert.DeserializeObject<MyItem>(input[0].ToString());

                foreach (var change in input)
                {
                    Logger.Debug("Adding change to queue for subscription id {subscriptionId}", change.Id);
                    output.Add(new SubscriptionItem(change.Id));
                }
            }
        }
    }
}
