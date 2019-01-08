using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog;
using NLog.Web;
using System;
using System.Threading.Tasks;

namespace Esfa.Recruit.Subscriptions.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Info("Starting up host");
                var host = CreateWebHostBuilder(args).Build();

                logger.Info("Initialising Data store");
                await InitializeDataStore(host.Services, logger);

                host.Run();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Fatal(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        private static async Task InitializeDataStore(IServiceProvider serviceProvider, Logger logger)
        {
            try
            {
                var initializer = (DataStoreInitializer) serviceProvider.GetService(typeof(DataStoreInitializer));
                await initializer.Initialise();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error initializing data store");
            }
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureKestrel(c => c.AddServerHeader = false)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:5035")
                .UseNLog();

    }
}
