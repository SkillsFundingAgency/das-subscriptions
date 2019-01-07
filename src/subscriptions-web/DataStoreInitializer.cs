using System;
using System.Threading.Tasks;
using Esfa.Recruit.Subscriptions.Web.Configuration;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Esfa.Recruit.Subscriptions.Web
{
    public class DataStoreInitializer
    {
        private CosmosClient _cosmosClient;
        private CosmosDatabase _database;
        private CosmosContainer _container;
        private readonly SubscriptionsDataStoreDetails _storeConfig;
        private readonly ILogger<DataStoreInitializer> _logger;

        public DataStoreInitializer(ILogger<DataStoreInitializer> logger, IOptionsMonitor<SubscriptionsDataStoreDetails> storeConfig)
        {
            _storeConfig = storeConfig.CurrentValue;
            _logger = logger;
        }

        public async Task Initialise()
        {
            try
            {
                await GetStartedDemoAsync();
            }
            catch (CosmosException ex)
            {
                _logger.LogError(ex, "An cosmos error occured when trying to initialize the data store.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occured when trying to initialize the data store.");
                throw;
            }
        }

        public async Task GetStartedDemoAsync()
        {
            _cosmosClient = new CosmosClient(_storeConfig.ConnectionString);

            await CreateDatabase(); 
            await CreateContainer();
        }

        private async Task CreateDatabase()
        {
            _database = await _cosmosClient.Databases.CreateDatabaseIfNotExistsAsync(_storeConfig.DatabaseName);
        }

        private async Task CreateContainer()
        {
            _container = await _database.Containers.CreateContainerIfNotExistsAsync(_storeConfig.CollectionName, "/id");
        }
    }
}