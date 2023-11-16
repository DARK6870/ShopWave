using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;
using ShopWave.Pages.AccountPage.Queryes;
using ShopWave.Pages.SellerHub.SellerOrders.ViewModels;

namespace ShopWave.Pages.SellerHub.SellerOrders.Queryes
{
    public record GetAllStatusesQuery() : IRequest<List<Status>>;

    public class GetAllStatusesHandler : IRequestHandler<GetAllStatusesQuery, List<Status>>
    {
        private readonly AppDBContext _context;
        private readonly IAppCache _cache;
        public GetAllStatusesHandler(AppDBContext context, IAppCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<List<Status>> Handle(GetAllStatusesQuery request, CancellationToken cancellationToken)
        {
            List<Status> statuses = await _cache.GetOrAddAsync("status_data", async () =>
            {
                return await _context.Statuses.Where(p => p.StatusId != 5 && p.StatusId != 6).ToListAsync();
            }, TimeSpan.FromDays(1));

            return statuses;
        }
    }
}
