using MediatR;
using Microsoft.AspNetCore.Identity;
using ShopWave.Context;
using ShopWave.Pages.UserAvatarPage.Queryes;

namespace ShopWave.Pages.AccountPage.Queryes
{
    public record GetUserAvatarByIdQuery(string id) : IRequest<string>;

    public class GetUserAvatarByIdHandler : IRequestHandler<GetUserAvatarByIdQuery, string>
    {
        private readonly IMediator _mediator;
        public GetUserAvatarByIdHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<string> Handle(GetUserAvatarByIdQuery request, CancellationToken cancellationToken)
        {
            var users = await _mediator.Send(new GetAllAvatarsQuery());

            var user = users.FirstOrDefault(p => p.AppUserId == request.id);
            return user.Data;
        }
    }
}
