using MediatR;
using ShopWave.Context;
using ShopWave.Entity;
using ShopWave.Pages.SellerHub.SellerProduct.ViewModels;

namespace ShopWave.Pages.SellerHub.SellerProduct.Commands
{
    public record PostNewVariationCommand(ProductVariation variation) : IRequest<bool>;

    public class PostNewVariationHandler : IRequestHandler<PostNewVariationCommand, bool>
    {
        private readonly AppDBContext _context;
        public PostNewVariationHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(PostNewVariationCommand request, CancellationToken cancellationToken)
        {
            await _context.ProductVariations.AddAsync(request.variation);
            int res = await _context.SaveChangesAsync();

            return res > 0;
        }
    }
}
