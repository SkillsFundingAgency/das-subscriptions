using Esfa.Recruit.Subscriptions.Web.SubscriptionOuputs;
using System;
using System.Net.Mime;

namespace Esfa.Recruit.Subscriptions.Web.Factories
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