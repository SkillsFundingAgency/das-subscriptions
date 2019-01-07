using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
        [FunctionName("UpdateResultOnSubscriptionChange")]
        [return: Queue("subscriptions", Connection = "AzureWebJobsStorage")]
        public static void Run([CosmosDBTrigger(
            databaseName: "recruit-subscriptions",
            collectionName: "subscriptions",
            ConnectionStringSetting = "SubscriptionsCosmosDb",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> input, ILogger log,
            [Queue("subscriptions", Connection = "AzureWebJobsStorage")]ICollector<string> output,
            ExecutionContext context)
        {
            var logger = CreateLogger(context);

            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                logger.Debug("Documents modified {count}", input.Count);
                
                log.LogInformation("First document Id " + input[0].Id);

                //var myItem = JsonConvert.DeserializeObject<MyItem>(input[0].ToString());

                foreach (var change in input)
                {
                    logger.Debug("Adding change to queue for subscription id {subscriptionId}", change.Id);
                    output.Add(new SubscriptionItem(change.Id));
                }
            }
        }

        private static NLog.ILogger CreateLogger(ExecutionContext context)
        {
            LogManager.Configuration = new XmlLoggingConfiguration(context.FunctionAppDirectory + "/nlog.config");
            return  LogManager.GetCurrentClassLogger();
        }
    }
}
