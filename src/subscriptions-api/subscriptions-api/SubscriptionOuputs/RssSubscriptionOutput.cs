using Esfa.Recruit.Subscriptions.Api.Models;
using System;
using WilderMinds.RssSyndication; // TODO AU Remove this reference and use the Spike package

namespace Esfa.Recruit.Subscriptions.Api.SubscriptionOuputs
{
    public class RssSubscriptionOutput : ISubscriptionOutput
    {
        public string GeneratePreview(SubscriptionItem subscriptionItem)
        {
            var feed = new Feed
            {
                Title = "Vacancies",
                Description = "The Find an apprenticeship service provides a list of new vacancies",
                Link = new Uri("https://www.findapprenticeship.service.gov.uk/"),
                Copyright = "RSS feed supplied by the Education & Skills Funding Agency"
            };

            var feedSerialised = feed.Serialize();

            return feedSerialised;
        }
    }
}