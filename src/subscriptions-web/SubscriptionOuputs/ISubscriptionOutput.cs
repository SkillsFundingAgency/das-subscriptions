using Esfa.Recruit.Subscriptions.Models;

namespace Esfa.Recruit.Subscriptions.Web.SubscriptionOuputs
{
    public interface ISubscriptionOutput
    {
        string GeneratePreview(SubscriptionItem subscriptionItem);
    }
}