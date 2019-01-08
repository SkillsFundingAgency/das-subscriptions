using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Esfa.Recruit.Subscriptions.Models
{
    public class Subscription
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
    }
}