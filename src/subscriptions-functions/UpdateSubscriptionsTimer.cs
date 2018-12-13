using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Esfa.Recruit.Subscriptions.Functions
{
    public static class UpdateSubscriptionsTimer
    {
        [FunctionName("UpdateSubscriptionsTimer")]
        [return: Queue("subscriptions", Connection = "AzureWebJobsStorage")]
        public static void Run([TimerTrigger("0 0 */2 * * *", RunOnStartup = false)]TimerInfo myTimer, ILogger log, [Queue("subscriptions", Connection = "AzureWebJobsStorage")]ICollector<string> output)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            string[] temp = new string[] {" { 'name': 'lee' }", "{ 'name': 'bob' }", "{ 'name': 'jim' }"};

            foreach(var item in temp)
            {
                output.Add(item);
            }
        }
    }
}
