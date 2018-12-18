using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Esfa.Recruit.Subscriptions.Web.Infrastructure;
using Esfa.Recruit.Subscriptions.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Esfa.Recruit.Subscriptions.Web.Pages.Subscriptions
{
    public class Create : PageModel
    {
        private readonly IMediator _mediator;

        public Create(IMediator mediator) => _mediator = mediator;

        [BindProperty]
        public Command Data { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            await _mediator.Send(Data);

            return RedirectToPage("/Create");
        }

        public class Command : IRequest
        {
            public string Name { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile() =>
                CreateMap<Command, Subscription>();
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            private readonly IMapper _mapper;
            private readonly CosmosSubscriptionRepository _repository;
            private readonly ILogger<Handler> _logger;

            public Handler(ILogger<Handler> logger, IMapper mapper, CosmosSubscriptionRepository repository)
            {
                _mapper = mapper;
                _repository = repository;
                _logger = logger;
            }

            protected override async Task Handle(Command message, CancellationToken token)
            {
                var subscription = _mapper.Map<Command, Subscription>(message);

                await _repository.Create(subscription);

                _logger.LogDebug("Created Subscription for {subscriptionName}", message.Name);
            }
        }
    }
}