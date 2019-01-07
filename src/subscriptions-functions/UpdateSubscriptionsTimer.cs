using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using NLog.Config;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Esfa.Recruit.Subscriptions.Functions
{
    public static class UpdateSubscriptionsTimer
    {
        [FunctionName("UpdateSubscriptionsTimer")]
        public static void Run(
            [TimerTrigger("0 0 */2 * * *", RunOnStartup = true)]TimerInfo myTimer,
            [CosmosDB(
                databaseName: "recruit-subscriptions",
                collectionName: "subscriptions",
                ConnectionStringSetting = "SubscriptionsCosmosDb",
                SqlQuery = "SELECT * FROM c")]
                IEnumerable<SubscriptionItem> subscriptions,
            ILogger log,
            [Queue("subscriptions", Connection = "AzureWebJobsStorage")]ICollector<string> output,
            ExecutionContext context)
        {
            var logger = CreateLogger(context);

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            logger.Debug("Re-generating all the Subscription results");
            
            var subscriptionItems = subscriptions.ToList();

            foreach(var item in subscriptions)
            {
                output.Add(JsonConvert.SerializeObject(item));
            }

            logger.Info("We've added {count} items to the queue", subscriptionItems.Count);
        }

        private static NLog.ILogger CreateLogger(ExecutionContext context)
        {
            LogManager.Configuration = new XmlLoggingConfiguration(context.FunctionAppDirectory + "/nlog.config");
            return  LogManager.GetCurrentClassLogger();
        }
    }
}
