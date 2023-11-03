using MediatR;
using ShopWave.Context;
using ShopWave.Entity;

namespace ShopWave.Pages.AdminProductVariation.Queryes
{
    public record GetProductVariationByIdQuery(int id) : IRequest<ProductVariation>;

    public class GetProductVariationByIdHandler : IRequestHandler<GetProductVariationByIdQuery, ProductVariation>
    {
        private readonly AppDBContext _context;
        public GetProductVariationByIdHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<ProductVariation> Handle(GetProductVariationByIdQuery request, CancellationToken cancellationToken)
        {
            ProductVariation? result = await _context.ProductVariations.FindAsync(request.id);

            return result;
        }
    }
}
