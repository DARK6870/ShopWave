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
        private readonly IMediator _mediator;
        public GetAllProductsHandler(AppDBContext context, IAppCache cache, IMediator mediator)
        {
            _context = context;
            _cache = cache;
            _mediator = mediator;
        }

        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _cache.GetOrAddAsync("products_data", async () =>
            {
                var result = await _context.Products
                .Include(p => p.Categoriess)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVariations)
                .Include(p => p.Reviews)
                .Where(p => p.Admitered)
                .ToListAsync();

                foreach (var item in result)
                {
                    if (item.Reviews != null && item.Reviews.Any())
                    {
                        item.AvgStars = Math.Round((decimal)item.Reviews.Average(r => r.NumOfStarts), 1);
                    }
                    else
                    {
                        item.AvgStars = 0;
                    }

                    item.OrderCounts = await _mediator.Send(new GetOrderCountByProductIdQuery(item.ProductId));
                }
                await _context.SaveChangesAsync();

                return result;
            }, TimeSpan.FromHours(24));

            return products;
        }
    }
}
