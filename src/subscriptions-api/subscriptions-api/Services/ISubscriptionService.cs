using System.Net.Mime;
using System.Threading.Tasks;
using Esfa.Recruit.Subscriptions.Api.Models;

namespace Esfa.Recruit.Subscriptions.Api.Services
{
    public interface ISubscriptionService
    {
        Task<string> Create(SubscriptionRequest subscriptionRequest, ContentType contentType);
    }
}