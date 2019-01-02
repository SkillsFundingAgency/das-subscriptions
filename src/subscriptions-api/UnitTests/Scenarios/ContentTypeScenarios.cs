using System.Net.Mime;

namespace Esfa.Recruit.Subscriptions.Api.UnitTests.Scenarios
{
    internal class ContentTypeScenarios
    {
        internal static ContentType Html()
        {
            var contentType = new ContentType("text/html");

            return contentType;
        }

        internal static ContentType Rss()
        {
            var contentType = new ContentType("application/rss+xml");

            return contentType;
        }
    }
}