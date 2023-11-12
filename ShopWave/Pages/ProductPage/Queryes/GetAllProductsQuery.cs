using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.ProductPage.Queryes
{
    public record GetAllProductsQuery() : IRequest<List<Product>>;

    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly AppDBContext _context;
        private readonly IAppCache _cache;
        public GetAllProductsHandler(AppDBContext context, IAppCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _cache.GetOrAddAsync("products_data", async () =>
            {
                var result = await _context.Products
                .Include(p => p.Categoriess)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVariations)
                .Where(p => p.Admitered)
                .ToListAsync();

                return result;
            }, TimeSpan.FromHours(24));

            return products;
        }
    }
}
