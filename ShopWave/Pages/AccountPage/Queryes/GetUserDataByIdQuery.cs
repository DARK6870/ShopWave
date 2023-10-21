using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;

namespace ShopWave.Pages.AccountPage.Queryes
{
    public record GetUserByIdQuery(string id) : IRequest<IdentityUser>;

    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, IdentityUser>
    {
        private readonly AppDBContext _context;
        public GetUserByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IdentityUser> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == request.id);

            return user;
        }
    }
}
