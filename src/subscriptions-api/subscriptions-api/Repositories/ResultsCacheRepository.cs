using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace Esfa.Recruit.Subscriptions.Api.Repositories
{
    public class ResultsCacheRepository : IResultsCacheRepository
    {
        private const string PartitionKey = "search-results";
        private const string TableName = "subscriptionResults";

        public async Task Get(string rowKey)
        {
            var connectionString = ""; // TODO AU Set connection string
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var client = storageAccount.CreateCloudTableClient();

            var table = client.GetTableReference(TableName);

            var retrieveOperation = TableOperation.Retrieve(PartitionKey, rowKey);

            var retrievedResult = await table.ExecuteAsync(retrieveOperation);
        }
    }
}