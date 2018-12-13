using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Esfa.Recruit.Subscriptions.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            await _mediator.Send(new Command());

            return RedirectToPage("/Create");
        }

        public class Command : IRequest<int>
        {
            [IgnoreMap]
            public int Number { get; set; }
            public string Name { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile() =>
                CreateMap<Command, Course>(MemberList.Source)
                    .ForSourceMember(c => c.Number, opt => opt.DoNotValidate());
        }


        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IMapper _mapper;

            public Handler(IMapper mapper)
            {
                _mapper = mapper;
            }

            public Task<int> Handle(Command message, CancellationToken token)
            {
                var course = _mapper.Map<Command, Course>(message);
                course.Id = message.Number;

                return Task.FromResult(course.Id);
            }
        }
    }
}