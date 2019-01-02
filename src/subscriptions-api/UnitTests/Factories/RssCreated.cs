using Esfa.Recruit.Subscriptions.Api.Factories;
using Esfa.Recruit.Subscriptions.Api.SubscriptionOuputs;
using Esfa.Recruit.Subscriptions.Api.UnitTests.Scenarios;
using Xunit;

namespace Esfa.Recruit.Subscriptions.Api.UnitTests.Factories
{
    [Trait("SubscriptionFactory", "Rss Subscription created successfully")]
    public class RssCreated
    {
        private readonly ISubscriptionOutput _subscriptionOutput;

        public RssCreated()
        {
            var contentType = ContentTypeScenarios.Rss();
            _subscriptionOutput = SubscriptionOutputFactory.Create(contentType);
        }

        [Fact(DisplayName = "Correct Type returned")]
        public void CorrectTypeReturned() =>
            Assert.IsType<RssSubscriptionOutput>(_subscriptionOutput);
    }
}