using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.AccountPage.Commands
{
    public record ChangeUsernameCommand(string Id, string username) : IRequest<bool>;

    public class ChangeUsernameHandler : IRequestHandler<ChangeUsernameCommand, bool>
    {
        private readonly AppDBContext _context;
        public ChangeUsernameHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ChangeUsernameCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == request.Id);
            user.UserName = request.username;

            int result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
