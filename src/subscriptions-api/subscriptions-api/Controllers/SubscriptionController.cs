using System.Net.Mime;
using System.Threading.Tasks;
using Esfa.Recruit.Subscriptions.Api.Models;
using Esfa.Recruit.Subscriptions.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Esfa.Recruit.Subscriptions.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> Generate([FromQuery] SubscriptionRequest subscriptionRequest)
        {
            var contentType = new ContentType(Request.ContentType);

            var result = await _subscriptionService.Create(subscriptionRequest, contentType);

            return Content(result, contentType.MediaType);
        }
    }
}