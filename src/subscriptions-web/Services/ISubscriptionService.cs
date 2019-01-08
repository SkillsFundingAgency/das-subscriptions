using System.Net.Mime;
using System.Threading.Tasks;
using Esfa.Recruit.Subscriptions.Models;

namespace Esfa.Recruit.Subscriptions.Services
{
    public interface ISubscriptionService
    {
        Task<string> Create(SubscriptionRequest subscriptionRequest, ContentType contentType);
    }
}