using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.UserAvatarPage.Queryes
{
    public record GetAllAvatarsQuery() : IRequest<List<Avatar>>;

    public class GetAllAvatarsHandler : IRequestHandler<GetAllAvatarsQuery, List<Avatar>>
    {
        private readonly AppDBContext _context;
        private readonly IAppCache _cache;
        public GetAllAvatarsHandler(AppDBContext context, IAppCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<List<Avatar>> Handle(GetAllAvatarsQuery request, CancellationToken cancellationToken)
        {
            var avatars = await _cache.GetOrAddAsync("avatars_data", async () =>
            {
                return await _context.Avatars.ToListAsync();
            }, TimeSpan.FromHours(24));

            return avatars;
        }
    }
}
