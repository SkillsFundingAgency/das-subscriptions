using System.Net.Mime;
using System.Threading.Tasks;
using Esfa.Recruit.Subscriptions.Models;
using Esfa.Recruit.Subscriptions.Services;
using Microsoft.AspNetCore.Mvc;

/// TODO: The Api is a work in progress and requires more work even as a POC
namespace Esfa.Recruit.Subscriptions.Web.Controllers
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