using Esfa.Recruit.Subscriptions.Api.Factories;
using Esfa.Recruit.Subscriptions.Api.SubscriptionOuputs;
using Esfa.Recruit.Subscriptions.Api.UnitTests.Scenarios;
using Xunit;

namespace Esfa.Recruit.Subscriptions.Api.UnitTests.Factories
{
    [Trait("SubscriptionFactory", "Html Subscription created successfully")]
    public class HtmlCreated
    {
        private readonly ISubscriptionOutput _subscriptionOutput;

        public HtmlCreated()
        {
            var contentType = ContentTypeScenarios.Html();
            _subscriptionOutput = SubscriptionOutputFactory.Create(contentType);
        }

        [Fact(DisplayName = "Correct Type returned")]
        public void CorrectTypeReturned() =>
            Assert.IsType<HtmlSubscriptionOutput>(_subscriptionOutput);
    }
}