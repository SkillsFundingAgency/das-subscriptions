using System;

namespace Esfa.Recruit.Subscriptions.Api.Models
{
    public class SubscriptionItem
    {
        public SubscriptionItem(string id) => Id = id;

        public string Id { get; set; }
        public string ContentHash { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime LastChecked { get; set; }
        public DateTime LastUsedForWeb { get; set; }
        public DateTime LastUsedForFeed { get; set; }

        public override string ToString()
        {
            return null; // JsonConvert.SerializeObject(this);
        }

        public static implicit operator string(SubscriptionItem obj)
        {
            return obj.ToString();
        }
    }
}