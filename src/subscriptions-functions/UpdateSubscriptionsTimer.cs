using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private static NLog.ILogger Logger;

        static UpdateSubscriptionsTimer()
        {
            LogManager.Configuration = new XmlLoggingConfiguration("nlog.config");
            Logger = LogManager.GetCurrentClassLogger();
        }

        [FunctionName("UpdateSubscriptionsTimer")]
        public static void Run(
            [TimerTrigger("0 0 */2 * * *", RunOnStartup = true)]TimerInfo myTimer,
            [CosmosDB(
                databaseName: "recruit-subscriptions",
                collectionName: "subscriptions",
                ConnectionStringSetting = "SubscriptionsCosmosAccount",
                SqlQuery = "SELECT * FROM c")]
                IEnumerable<SubscriptionItem> subscriptions,
            ILogger log,
            [Queue("subscriptions", Connection = "AzureWebJobsStorage")]ICollector<string> output)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            Logger.Debug("We have triggered the trigger");
            
            var subscriptionItems = subscriptions.ToList();

            foreach(var item in subscriptions)
            {
                output.Add(JsonConvert.SerializeObject(item));
            }

            Logger.Info("We've added {count} items to the queue", subscriptionItems.Count);
        }
    }
}
