using System;
using System.Net;
using System.Threading.Tasks;
using Esfa.Recruit.Subscriptions.Web.Configuration;
using Esfa.Recruit.Subscriptions.Web.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Esfa.Recruit.Subscriptions.Web.Infrastructure
{
    public class CosmosSubscriptionRepository : IDisposable
    {
        private readonly string _databaseName;
        private readonly string _collectionName;
        private readonly CosmosClient _cosmosClient;

        public CosmosSubscriptionRepository(IOptionsMonitor<SubscriptionsDataStoreDetails> storeConfig)
        {
            _databaseName = storeConfig.CurrentValue.DatabaseName;
            _collectionName = storeConfig.CurrentValue.CollectionName;

            _cosmosClient = new CosmosClient(storeConfig.CurrentValue.ConnectionString);
        }

        public async Task Create(Subscription subscription)
        {
            subscription.Id = (new Guid("a3bd8a9f-09b3-45f1-9c24-86839505b982")).ToString();
            var response = await _cosmosClient.Databases[_databaseName].Containers[_collectionName].Items.CreateItemAsync<Subscription>(subscription.Id, subscription);

            if (response.StatusCode != HttpStatusCode.Created)
                throw new Exception("Something went wrong");
        }

        public void Dispose()
        {
            _cosmosClient?.Dispose();
        }
    }
}