using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;
using ShopWave.Pages.ProductPage.Queryes;

namespace ShopWave.Pages.AdminCategoryPage.Queryes
{
    public record GetAllCategoryesQuery() : IRequest<List<Categories>>;

    public class GetAllCategoryesHandler : IRequestHandler<GetAllCategoryesQuery, List<Categories>>
    {
        private readonly AppDBContext _context;
        private readonly IAppCache _cache;
        public GetAllCategoryesHandler(AppDBContext context, IAppCache cache)
        {
            _cache = cache;
            _context = context;
        }

        public async Task<List<Categories>> Handle(GetAllCategoryesQuery request, CancellationToken cancellationToken)
        {
            var categoryes = await _cache.GetOrAddAsync("category_data", async () =>
            {
                var res = await _context.Categories.ToListAsync();
                return res;
            }, TimeSpan.FromDays(5));

            return categoryes;
        }
    }
}
