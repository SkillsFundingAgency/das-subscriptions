using Newtonsoft.Json;

namespace Esfa.Recruit.Subscriptions.Functions
{
    public class SubscriptionItem
    {
        public SubscriptionItem(string id) => Id = id;

        public string Id { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static implicit operator string(SubscriptionItem obj)
        {
            return obj.ToString();
        }
    }
}
