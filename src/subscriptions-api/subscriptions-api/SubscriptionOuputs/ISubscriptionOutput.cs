using Esfa.Recruit.Subscriptions.Api.Models;

namespace Esfa.Recruit.Subscriptions.Api.SubscriptionOuputs
{
    public interface ISubscriptionOutput
    {
        string GeneratePreview(SubscriptionItem subscriptionItem);
    }
}