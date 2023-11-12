using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.ProductPage.Queryes
{
    public record GetNonAdmiteredProductsQuery() : IRequest<List<Product>>;

    public class GetNonAdmiteredProductsHandler : IRequestHandler<GetNonAdmiteredProductsQuery, List<Product>>
    {
        private readonly AppDBContext _context;
        public GetNonAdmiteredProductsHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> Handle(GetNonAdmiteredProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Include(p => p.Categoriess)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVariations)
                .Where(p => p.Admitered == false)
                .OrderByDescending(p => p.ProductDate)
                .ToListAsync();

            return products;
        }
    }
}
