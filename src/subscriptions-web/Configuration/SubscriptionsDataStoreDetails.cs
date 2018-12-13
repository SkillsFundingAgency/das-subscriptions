namespace Esfa.Recruit.Subscriptions.Web.Configuration
{
    public class SubscriptionsDataStoreDetails
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public int DatabaseThroughput { get; set; }
        public string CollectionName { get; set; }
    }
}