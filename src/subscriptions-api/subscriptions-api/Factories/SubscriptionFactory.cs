using Esfa.Recruit.Subscriptions.Api.SubscriptionOuputs;
using System;
using System.Net.Mime;

namespace Esfa.Recruit.Subscriptions.Api.Factories
{
    public class SubscriptionOutputFactory
    {
        public static ISubscriptionOutput Create(ContentType contentType)
        {
            switch (contentType.MediaType)
            {
                case "text/html":
                    return new HtmlSubscriptionOutput();
                case "application/rss+xml":
                case "application/atom+xml":
                    return new RssSubscriptionOutput();
            }

            throw new InvalidOperationException();
        }
    }
}