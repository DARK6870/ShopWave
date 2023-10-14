using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.ProductPage.Queryes
{
    public record GetProductByIdQuery(int id) : IRequest<Product>;

    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly AppDBContext _context;
        public GetProductByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product? result = await _context.Products
                .Include(p => p.Categoriess)
                .Include(p => p.Statuses)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVariations)
                .FirstOrDefaultAsync(p => p.ProductId == request.id);

            return result;
        }
    }
}
