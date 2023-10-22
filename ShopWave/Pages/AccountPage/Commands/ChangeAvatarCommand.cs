using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.AccountPage.Commands
{
    public record ChangeAvatarCommand(Avatar avatar) : IRequest<bool>;

    public class ChangeAvatarHandler : IRequestHandler<ChangeAvatarCommand, bool>
    {
        private readonly AppDBContext _context;
        private readonly IAppCache _cache;
        public ChangeAvatarHandler(AppDBContext context, IAppCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<bool> Handle(ChangeAvatarCommand request, CancellationToken cancellationToken)
        {
            var avatarData = await _context.Avatars.FirstOrDefaultAsync(p => p.AppUserId == request.avatar.AppUserId);
            if (avatarData != null)
            {
                avatarData.Data = request.avatar.Data;
            }
            else
            {
                await _context.Avatars.AddAsync(request.avatar);
            }

            int result = await _context.SaveChangesAsync();

            _cache.Remove("avatars_data");

            return result > 0;
        }
    }
}
