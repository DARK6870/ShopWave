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
        public GetAllProductsHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Include(p => p.Categoriess)
                .Include(p => p.Statuses)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVariations)
                .ToListAsync();

            return products;
        }
    }
}
