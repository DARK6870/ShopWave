using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.SellerHub.SellerProduct.Queryes
{
    public record GetProductsBySellerIdQuery(string id) : IRequest<List<Product>>;

    public class GetProductsBySellerIdHandler : IRequestHandler<GetProductsBySellerIdQuery, List<Product>>
    {
        private readonly AppDBContext _context;
        public GetProductsBySellerIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> Handle(GetProductsBySellerIdQuery request, CancellationToken cancellationToken)
        {
            List<Product> products = await _context.Products
                .Include(p => p.Categoriess)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVariations)
                .Where(p => p.AppUserId == request.id)
                .OrderByDescending(p => p.ProductDate)
                .ToListAsync();

            return products;
        }
    }
}
