using Esfa.Recruit.Subscriptions.Api.Factories;
using Esfa.Recruit.Subscriptions.Api.Models;
using Esfa.Recruit.Subscriptions.Api.Repositories;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Esfa.Recruit.Subscriptions.Api.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IResultsCacheRepository _resultsCacheRepository;
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        public SubscriptionService(IResultsCacheRepository resultsCacheRepository,
            ISubscriptionsRepository subscriptionsRepository)
        {
            _resultsCacheRepository = resultsCacheRepository;
            _subscriptionsRepository = subscriptionsRepository;
        }

        public async Task<string> Create(SubscriptionRequest subscriptionRequest, ContentType contentType)
        {
            var subscriptionOutput = SubscriptionOutputFactory.Create(contentType);

            var subscriptionId = subscriptionRequest.SubscriptionId;

            await _resultsCacheRepository.Get(subscriptionId);

            var subscriptionItem = new SubscriptionItem("1");

            // TODO AU Update usages
            var result = subscriptionOutput.GeneratePreview(subscriptionItem);

            return result;
        }
    }
}