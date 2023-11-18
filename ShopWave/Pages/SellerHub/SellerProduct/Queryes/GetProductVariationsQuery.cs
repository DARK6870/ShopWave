using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.SellerHub.SellerProduct.Queryes
{
    public record GetProductVariationsQuery(int id) : IRequest<List<ProductVariation>>;

    public class GetProductVariationsHandler : IRequestHandler<GetProductVariationsQuery, List<ProductVariation>>
    {
        private readonly AppDBContext _context;
        public GetProductVariationsHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<ProductVariation>> Handle(GetProductVariationsQuery request, CancellationToken cancellationToken)
        {
            List<ProductVariation> variations = await _context.ProductVariations.
                Where(p => p.ProductId == request.id).
                ToListAsync();

            return variations;
        }
    }
}
