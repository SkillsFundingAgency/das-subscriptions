using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
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
            var directory = Directory.GetCurrentDirectory();
            LogManager.Configuration = new XmlLoggingConfiguration("nlog.config");
            Logger = LogManager.GetCurrentClassLogger();
        }

        [FunctionName("UpdateSubscriptionsTimer")]
        // [return: Queue("subscriptions", Connection = "AzureWebJobsStorage")]
        public static void Run(
            [TimerTrigger("0 0 */2 * * *", RunOnStartup = true)]TimerInfo myTimer,
            ILogger log,
            [Queue("subscriptions", Connection = "AzureWebJobsStorage")]ICollector<string> output)
        {
            Logger.Debug("Oh no!!!");
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            string[] temp = new string[] {" { 'name': 'lee' }", "{ 'name': 'bob' }", "{ 'name': 'jim' }"};

            foreach(var item in temp)
            {
                output.Add(item);
            }
        }
    }
}
